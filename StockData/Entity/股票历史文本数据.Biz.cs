using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Serialization;
using NewLife;
using NewLife.Data;
using NewLife.Log;
using NewLife.Model;
using NewLife.Reflection;
using NewLife.Threading;
using NewLife.Web;
using XCode;
using XCode.Cache;
using XCode.Configuration;
using XCode.DataAccessLayer;
using XCode.Membership;
using HtmlAgilityPack;
using NewLife.Serialization;

namespace StockData.Entity
{
    /// <summary>股票历史文本数据</summary>
    public partial class StockHisText : Entity<StockHisText>
    {
        #region 对象操作
        static StockHisText()
        {
            // 累加字段
            //Meta.Factory.AdditionalFields.Add(__.Logins);

            // 过滤器 UserModule、TimeModule、IPModule
        }

        /// <summary>验证数据，通过抛出异常的方式提示验证失败。</summary>
        /// <param name="isNew">是否插入</param>
        public override void Valid(Boolean isNew)
        {
            // 如果没有脏数据，则不需要进行任何处理
            if (!HasDirty) return;

            // 在新插入数据或者修改了指定字段时进行修正
        }

        ///// <summary>首次连接数据库时初始化数据，仅用于实体类重载，用户不应该调用该方法</summary>
        //[EditorBrowsable(EditorBrowsableState.Never)]
        //protected override void InitData()
        //{
        //    // InitData一般用于当数据表没有数据时添加一些默认数据，该实体类的任何第一次数据库操作都会触发该方法，默认异步调用
        //    if (Meta.Count > 0) return;

        //    if (XTrace.Debug) XTrace.WriteLine("开始初始化StockHisText[股票历史文本数据]数据……");

        //    var entity = new StockHisText();
        //    entity.Code = "abc";
        //    entity.Name = "abc";
        //    entity.Start = DateTime.Now;
        //    entity.End = DateTime.Now;
        //    entity.Insert();

        //    if (XTrace.Debug) XTrace.WriteLine("完成初始化StockHisText[股票历史文本数据]数据！"
        //}

        ///// <summary>已重载。基类先调用Valid(true)验证数据，然后在事务保护内调用OnInsert</summary>
        ///// <returns></returns>
        //public override Int32 Insert()
        //{
        //    return base.Insert();
        //}

        ///// <summary>已重载。在事务保护范围内处理业务，位于Valid之后</summary>
        ///// <returns></returns>
        //protected override Int32 OnDelete()
        //{
        //    return base.OnDelete();
        //}
        #endregion

        #region 扩展属性
        #endregion

        #region 扩展查询
        /// <summary>根据股票编码查找</summary>
        /// <param name="code">股票编码</param>
        /// <returns>实体对象</returns>
        public static StockHisText FindByCode(String code)
        {
            if (code.IsNullOrEmpty()) return null;

            // 实体缓存
            if (Meta.Count < 1000) return Meta.Cache.Entities.FirstOrDefault(e => e.Code == code);

            // 单对象缓存
            //return Meta.SingleCache[code];

            return Find(_.Code == code);
        }
        #endregion

        #region 高级查询
        #endregion

        #region 获取2000年到2018年2.10日的历史数据
        public static void GetAllHistroyText()
        {
            DateTime start = new DateTime(2000, 1, 1);
            DateTime end = new DateTime(2018, 2, 10);
            var all = StockBaseInfo.FindAll();
            int index = 1;
            foreach (var item in all)
            {
                XTrace.WriteLine("进度:{0}/{1}",index ++,all.Count);
                GetHistoryFromWeb(item.Code, start, end);
            }
        }
        /// <summary>
        /// 获取指数信息
        /// </summary>
        public static void GetAllIndexText()
        {
            DateTime start = new DateTime(2000, 1, 1);
            DateTime end = new DateTime(2018, 2, 10);
            var all = IndexInfo.FindAll();
            int index = 1;
            foreach (var item in all)
            {
                XTrace.WriteLine("进度:{0}/{1}", index++, all.Count);
                GetHistoryFromWeb(item.Code, start, end,"zs");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="stockCode"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="type">cn为股票，zs为指数</param>
        public static void GetHistoryFromWeb(string stockCode, DateTime start, DateTime end,string type="cn")
        {
            string url = @"http://q.stock.sohu.com/hisHq?code={3}_{0}&start={1}&end={2}".F(stockCode, start.ToString("yyyyMMdd"),
                end.ToString("yyyyMMdd"),type);
            WebClientX client = new WebClientX();
            client.Timeout = 1000 * 120;
            var text = client.GetHtml(url);
            var doc = new HtmlDocument();
            doc.LoadHtml(text);
            var value = doc.DocumentNode.InnerText;
            var et = new StockHisText()
            {
                Code = stockCode,
                Start = start,
                End = end,
                HisText = value
            };
            try
            {
                if (type == "zs") et.Code = "{0}_{1}".F("Index",et.Code);//加前缀区分
                et.Insert();
            }
            catch(Exception err)
            {
                XTrace.WriteException(err);
            }
        }
        #endregion

        #region 历史数据文本解析到正式数据库
        public static void PraseHistoryData()
        {
            var all = FindAll();
            int index = 1;
            Parallel.For(0, all.Count, new ParallelOptions() { MaxDegreeOfParallelism = 1 }, i => 
            {
                XTrace.WriteLine("进度:{0}/{1}",index ++,all.Count);
                #region 单个文本解析
                JsonParser jp = new JsonParser(all[i].HisText);
                var decode = (List<object>)jp.Decode();
                if (decode.Count < 1) return;
                var main = (Dictionary<string, object>)decode[0];//字典
                if (main.ContainsKey("hq"))
                {
                    var obj = (List<object>)main["hq"];
                    if (obj.Count > 0)
                    {
                        List<StockDayData> res = new List<StockDayData>();
                        foreach (var item in obj)
                        {
                            #region 单条记录解析
                            //item是一个10个元素的数组
                            //日期，今开价格，今天收盘价格，涨跌金额，涨跌幅度，最低价格，最高价格，总手，总金额(万)，换手率
                            //"2018-02-09", "31.46", "31.46", "2.86", "10.00%", "31.46", "31.46", "303", "95.32", "0.15%"
                            var list = (List<object>)item;
                            StockDayData sd = new StockDayData()
                            {
                                Code = all[i].Code,
                                StatDate = list[0].ToDateTime(),
                                StartPrice = list[1].ToDouble(),
                                EndPrice = list[2].ToDouble(),
                                ChangePrice = list[3].ToDouble(),
                                ChangeRatio = ((string)list[4]).Replace("%", "").ToDouble(),
                                LowPrice = list[5].ToDouble(),
                                HighPrice = list[6].ToDouble(),
                                TotalHand = list[7].ToInt(),
                                TotalAmount = list[8].ToDouble(),
                                HandRate = ((string)list[9]).Replace("%", "").ToDouble(),
                                UpdateDate = DateTime.Now
                            };
                            sd.ID = "{0}_{1}".F(sd.Code, sd.StatDate.ToString("yyyyMMdd"));
                            #endregion

                            res.Add(sd);
                        }
                        res.Save(true);
                    }
                }
                #endregion
            });
        }

        public static void PraseHistoryDataV2()
        {
            var all = StockBaseInfo.FindAll(StockBaseInfo._.Kind, 1);
            int index = 1;

            Parallel.For(0, all.Count, new ParallelOptions() { MaxDegreeOfParallelism =16 }, i =>
            {
                var es = FindByCode(all[i].Code);
                if(es !=null)
                {
                    var list = ParseText(all[i].Code, es.HisText);
                    if(list.Count>0)
                    {
                        StockDayData.Meta.ConnName = "stock_" + all[i].Code;
                        list.Save(true);
                    }
                }
                XTrace.WriteLine("进度：{0}/{1},Code={2}", index++, all.Count,all[i].Code);
            });
            
        }

        static List<StockDayData> ParseText(string code,string text)
        {
            #region 单个文本解析
            JsonParser jp = new JsonParser(text);
            var decode = (List<object>)jp.Decode();
            if (decode.Count < 1) return new List<StockDayData>();
            var main = (Dictionary<string, object>)decode[0];//字典
            if (main.ContainsKey("hq"))
            {
                var obj = (List<object>)main["hq"];
                if (obj.Count > 0)
                {
                    List<StockDayData> res = new List<StockDayData>();
                    foreach (var item in obj)
                    {
                        #region 单条记录解析
                        //item是一个10个元素的数组
                        //日期，今开价格，今天收盘价格，涨跌金额，涨跌幅度，最低价格，最高价格，总手，总金额(万)，换手率
                        //"2018-02-09", "31.46", "31.46", "2.86", "10.00%", "31.46", "31.46", "303", "95.32", "0.15%"
                        var list = (List<object>)item;
                        StockDayData sd = new StockDayData()
                        {
                            Code = code ,
                            StatDate = list[0].ToDateTime(),
                            StartPrice = list[1].ToDouble(),
                            EndPrice = list[2].ToDouble(),
                            ChangePrice = list[3].ToDouble(),
                            ChangeRatio = ((string)list[4]).Replace("%", "").ToDouble(),
                            LowPrice = list[5].ToDouble(),
                            HighPrice = list[6].ToDouble(),
                            TotalHand = list[7].ToInt(),
                            TotalAmount = list[8].ToDouble(),
                            HandRate = ((string)list[9]).Replace("%", "").ToDouble(),
                            UpdateDate = DateTime.Now
                        };
                        sd.ID = "{0}_{1}".F(sd.Code, sd.StatDate.ToString("yyyyMMdd"));
                        #endregion

                        res.Add(sd);
                    }
                    return res;
                    //res.Save(true);
                }
            }
            return new List<StockDayData>();
            #endregion
        }
        #endregion
    }
}
