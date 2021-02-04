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


namespace StockData.Entity
{
    /// <summary>重要财务指标</summary>
    public partial class KeyFinaIndex : Entity<KeyFinaIndex>
    {
        #region 对象操作
        static KeyFinaIndex()
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

        //    if (XTrace.Debug) XTrace.WriteLine("开始初始化KeyFinaIndex[重要财务指标]数据……");

        //    var entity = new KeyFinaIndex();
        //    entity.ID = "abc";
        //    entity.Code = "abc";
        //    entity.Date = DateTime.Now;
        //    entity.Revenue = 0;
        //    entity.NetIncome = 0;
        //    entity.Profit = 0.0;
        //    entity.Assets = 0;
        //    entity.CreateDate = DateTime.Now;
        //    entity.Insert();

        //    if (XTrace.Debug) XTrace.WriteLine("完成初始化KeyFinaIndex[重要财务指标]数据！"
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
        /// <summary>根据ID查找</summary>
        /// <param name="id">ID</param>
        /// <returns>实体对象</returns>
        public static KeyFinaIndex FindByID(String id)
        {
            if (id.IsNullOrEmpty()) return null;

            // 实体缓存
            if (Meta.Count < 1000) return Meta.Cache.Entities.FirstOrDefault(e => e.ID == id);

            // 单对象缓存
            //return Meta.SingleCache[id];

            return Find(_.ID == id);
        }

        /// <summary>根据股票代码查找</summary>
        /// <param name="code">股票代码</param>
        /// <returns>实体列表</returns>
        public static IList<KeyFinaIndex> FindByCode(String code)
        {
            // 实体缓存
            if (Meta.Count < 1000) return Meta.Cache.Entities.Where(e => e.Code == code).ToList();

            return FindAll(_.Code == code);
        }
        #endregion

        #region 高级查询
        #endregion

        #region 采集单支股票财务信息
        /// <summary>
        /// 000929测试
        /// </summary>
        /// <param name="stockCode"></param>
        public static void FecthByStockCode(string stockCode)
        {
            string url = @"https://q.stock.sohu.com/cn/{0}/cwzb.shtml".F(stockCode);
            var doc = url.GetHtmlByUrl();
            //主营收入成长(万元) 1-4都是表格，tr行信息为主，td列信息其次
            string xpath1 = @"/html[1]/body[1]/div[4]/div[2]/div[2]/div[2]/div[1]/div[1]/div[1]/div[1]/table[1]";
            //净利润成长(万元)
            string xpath2 = @"/html[1]/body[1]/div[4]/div[2]/div[2]/div[2]/div[1]/div[1]/div[1]/div[2]/table[1]";
            //每股收益成长(元)
            string xpath3 = @"/html[1]/body[1]/div[4]/div[2]/div[2]/div[2]/div[1]/div[1]/div[2]/div[1]/table[1]";
            //总资产成长(万元)
            string xpath4 = @"/html[1]/body[1]/div[4]/div[2]/div[2]/div[2]/div[1]/div[1]/div[2]/div[2]/table[1]";
            
            //报告期,tr 行 --- th（标题）/td列单元格
            string xpath = @"/html[1]/body[1]/div[4]/div[2]/div[2]/div[2]/div[1]/div[2]/table[1]";

            var dic1 = ParseTable(doc, xpath1);
            var dic2 = ParseTable(doc, xpath2);
            var dic3 = ParseTable(doc, xpath3);
            var dic4 = ParseTable(doc, xpath4);

            if (dic1 == null || dic2 == null || dic3 == null || dic4 == null) throw new Exception("数据异常");
            //组合为一条数据,先找出所有的Key列表，排序
            //DateTime dt = 
            List<KeyFinaIndex> modelList = new List<KeyFinaIndex>();
            foreach (var item in dic1)
            {
                modelList.Add
                    (
                    new KeyFinaIndex()
                    {
                        ID = $"{item.Key}_{stockCode}",
                        Code = stockCode ,
                        Date = DateTime.ParseExact(item.Key, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture),
                        CreateDate = DateTime.Now
                    });
            }

        }
        static Dictionary<string, string> ParseTable(HtmlDocument doc,string path)
        {
            var nodes = doc.GetInitialNodesByDoc(path, @"tr");
            if (nodes == null) return null;
            List<string[]> res = new List<string[]>();
            for (int i = 0; i < nodes.Count; i++)
            {
                var str = new string[4];
                var next = i == 0 ? @"th" : @"td";
                var nextNodes = nodes[i].SelectNodes(next);
                if(nextNodes.Count == 4)
                {
                    str[0] = nextNodes[0].InnerText.Replace("(","").Replace(")","").Replace("--","").Trim();
                    str[1] = nextNodes[1].InnerText.Replace("(", "").Replace(")", "").Replace("--", "").Trim();
                    str[2] = nextNodes[2].InnerText.Replace("(", "").Replace(")", "").Replace("--", "").Trim();
                    str[3] = nextNodes[3].InnerText.Replace("(", "").Replace(")", "").Replace("--", "").Trim();
                }
            }
            //5行4列进行组合，进行转换
            Dictionary<string, string> keys = new Dictionary<string, string>();
            for (int i = 1; i < 4; i++)
            {
                for (int j = 1; j < 5; j++)
                {
                    keys.Add($"{res[0][i]}{res[j][0]}",res[j][i]);
                }
            }
            return keys;
        }
        
        static List<KeyFinaIndex> SetEntity(Dictionary<string,string> res,string columnName, List<KeyFinaIndex> list)
        {
            foreach (var item in list)
            {
                var key = item.Date.ToString("yyyyMMdd");
                if (res.ContainsKey(key))
                {
                    item[columnName] = res[key].ToDouble();
                }
            }
            return list;
        }
        #endregion
    }
}