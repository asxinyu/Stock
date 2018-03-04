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
    /// <summary>股票基础信息</summary>
    public partial class StockBaseInfo : Entity<StockBaseInfo>
    {
        #region 对象操作
        static StockBaseInfo()
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

        //    if (XTrace.Debug) XTrace.WriteLine("开始初始化StockBaseInfo[股票基础信息]数据……");

        //    var entity = new StockBaseInfo();
        //    entity.Code = "abc";
        //    entity.Name = "abc";
        //    entity.Exchange = "abc";
        //    entity.StartDate = DateTime.Now;
        //    entity.CreateDate = DateTime.Now;
        //    entity.Insert();

        //    if (XTrace.Debug) XTrace.WriteLine("完成初始化StockBaseInfo[股票基础信息]数据！"
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
        public static StockBaseInfo FindByCode(String code)
        {
            if (code.IsNullOrEmpty()) return null;

            // 实体缓存
            if (Meta.Count < 1000) return Meta.Cache.Entities.FirstOrDefault(e => e.Code == code);

            // 单对象缓存
            //return Meta.SingleCache[code];

            return Find(_.Code == code);
        }

        /// <summary>根据名称查找</summary>
        /// <param name="name">名称</param>
        /// <returns>实体列表</returns>
        public static IList<StockBaseInfo> FindByName(String name)
        {
            // 实体缓存
            if (Meta.Count < 1000) return Meta.Cache.Entities.Where(e => e.Name == name).ToList();

            return FindAll(_.Name == name);
        }
        #endregion

        #region 高级查询
        #endregion

        #region 业务操作
        /// <summary>获取所有股票代码和名称基础信息</summary>
        public static void ReadAllStockBaseInfo()
        {
            //上海：/html[1]/body[1]/div[9]/div[2]/div[1]/ul[1]
            //下级是li列表 ，Text值就是股票名称和代码  XXX()
            //深圳：上海：/html[1]/body[1]/div[9]/div[2]/div[1]/ul[2]
            string url = @"http://quote.eastmoney.com/stocklist.html";

            HtmlWeb htmlweb = new HtmlWeb();
            htmlweb.OverrideEncoding = Encoding.GetEncoding(936);
            HtmlDocument doc = htmlweb.Load(url);

            Dictionary<string, string> dic = new Dictionary<string, string>()
            {
                {"上海",@"/html[1]/body[1]/div[9]/div[2]/div[1]/ul[1]" },
                {"深圳",@"/html[1]/body[1]/div[9]/div[2]/div[1]/ul[2]" }
            };

            #region 获取
            Dictionary<String, StockBaseInfo> list = new Dictionary<string, StockBaseInfo>();
            foreach (var item in dic)
            {
                //获取所有子节点
                var res = doc.DocumentNode.SelectSingleNode(item.Value).SelectNodes(@"li");
                if (res.Count > 0)
                {
                    foreach (var node in res)
                    {
                        //获取名称和代码
                        var name = node.InnerText.Trim();
                        if (name.IsNullOrEmpty()) continue;
                        var str = name.Split('(', ')');
                        if (str.Length < 2) continue;

                        StockBaseInfo et = new StockBaseInfo()
                        {
                            Code = str[1],
                            Name = str[0],
                            Exchange = item.Key,
                            StartDate = new DateTime(2000, 1, 1),
                            CreateDate = DateTime.Now 
                        };
                        if(!list.ContainsKey(et.Code))
                        {
                            list.Add(et.Code,et);
                        }
                    }
                }
            }
            list.ToValueArray().Insert(true);
            #endregion
        }
        #endregion


    }
}