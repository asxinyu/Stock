using HtmlAgilityPack;
using NewLife.Log;
using NewLife.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewLife.Serialization;
using StockData.Entity;
using XCode;
using StockData.Analysis.Entity;

namespace StockData
{
    /// <summary>
    /// 数据分析
    /// </summary>
    public static class StockHelper
    {
        #region 辅助处理
        /// <summary>
        /// 校验股票代码是否在分析范围内：只分析上海/深圳的A股股票
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public static Boolean CheckCode(this string code)
        {
            //https://baike.so.com/doc/4974613-5197406.html
            var sh = code.StartsWith("600") || code.StartsWith("601") || code.StartsWith("603");
            var sz = code.StartsWith("000") || code.StartsWith("002") ;

            return sh || sz ;
        }
       
        public static void Test()
        {
            var all = StockBaseInfo.FindAll();
            List<StockBaseInfo> list = new List<StockBaseInfo>();
            foreach (var item in all)
            {
                if(item.Name.Contains("ST")) //(item.Code.CheckCode())
                {
                    item.Kind = 0;
                    list.Add(item);
                }
            }
            list.Save(true);
        }
       
        /// <summary>
        /// 拆分数据分库
        /// </summary>
        public static void SpliteDB()
        {
            var all = StockBaseInfo.FindAll(StockBaseInfo._.Kind, 1);
            int index = 0;
            foreach (var item in all)
            {
                index++;
                if(item.Kind == 1)
                {
                    StockDayData.Meta.ConnName = "stock_day";
                    var data = StockDayData.FindAll(StockDayData._.Code, item.Code);
                    StockDayData.Meta.ConnName = "stock_"+item.Code;
                    data.Save(true);
                    XTrace.WriteLine("进度：{0}/{1},Code={2},记录数:{3}",index,all.Count,item.Code,data.Count);
                }
            }
        }
        #endregion

        #region 初步分析
        /// <summary>
        /// 同步股票上市时间，最早取2000年，以第一次有数据开始计算
        /// </summary>
        public static void ScanStockStartTime()
        {
            var all = StockBaseInfo.FindAll(StockBaseInfo._.Kind, 1);
            int index = 1;
            foreach (var item in all)
            {
                XTrace.WriteLine("进度:{0}/{1}",index ++,all.Count);
                //"stock_"+item.Code;
                //if (item.StartDate > new DateTime(2000, 1, 2)) continue;
                if (index < 2095) continue;
                var entity = StockDayData.FindAll(StockDayData._.Code == item.Code,
                                             StockDayData._.StatDate.Desc(),null,0,1);
                if(entity !=null && entity.Count>0)
                {
                    if(entity[0].StatDate<new DateTime(2018,1,1))
                    {
                        item.Kind = 0;
                        item.Update();
                    }
                    //item.StartDate = entity[0].StatDate;
                    //item.Update();2758

                }
            }
        }
        #endregion

        #region 10年数据初步分析
        public static void AnalysisFirst()
        {
            DateTime year5 = new DateTime(2013, 1, 1);
            DateTime year2 = new DateTime(2016, 1, 1);

            var all = StockBaseInfo.FindAll(StockBaseInfo._.Kind, 1);
            int index = 1;
            Parallel.For(0, all.Count, new ParallelOptions() { MaxDegreeOfParallelism = 8 }, i =>
            {
                XTrace.WriteLine("进度:{0}/{1}", index++, all.Count);
                StockDayData.Meta.ConnName = "stock_" + all[i].Code;
                var list = StockDayData.FindAll(StockDayData._.Code == all[i].Code,
                                                StockDayData._.StatDate.Asc(), null, 0, 0);
                if (list != null && list.Count > 0)
                {
                    StockElementInfo et = new StockElementInfo();
                    et.Code = all[i].Code;
                    et.Name = all[i].Name;
                    et.Price = list.Last().EndPrice;
                    et.MaxPrice = list.Where(n => n.StatDate > year5).Max(n => n.EndPrice); //list.Max(n => n.EndPrice);
                    et.MinPrice = list.Where(n => n.StatDate > year5).Min(n => n.EndPrice);// list.Min(n => n.EndPrice);
                    et.MaxRate = et.MaxPrice / et.MinPrice;
                    et.Max5year = list.Where(n => n.StatDate > year2).Max(n => n.EndPrice);
                    et.Min5year = list.Where(n => n.StatDate > year2).Min(n => n.EndPrice);
                    et.Max5Rate = et.Max5year / et.Min5year;
                    et.Rate = et.Max5year / et.Price;
                    et.UpdateDate = DateTime.Now;
                    et.Save();
                }
                //SELECT * from StockElementInfo WHERE Price > 8 and MaxRate >8 and Max5Rate > 4
            });
        }
        #endregion
    }
}
