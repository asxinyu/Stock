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
    /// <summary>板块分类</summary>
    public partial class GroupKind : Entity<GroupKind>
    {
        #region 对象操作
        static GroupKind()
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

        //    if (XTrace.Debug) XTrace.WriteLine("开始初始化GroupKind[板块分类]数据……");

        //    var entity = new GroupKind();
        //    entity.ID = "abc";
        //    entity.Name = "abc";
        //    entity.Kind = 0;
        //    entity.Total = 0;
        //    entity.CreateDate = DateTime.Now;
        //    entity.Insert();

        //    if (XTrace.Debug) XTrace.WriteLine("完成初始化GroupKind[板块分类]数据！"
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
        /// <summary>根据编码查找</summary>
        /// <param name="id">编码</param>
        /// <returns>实体对象</returns>
        public static GroupKind FindByID(String id)
        {
            if (id.IsNullOrEmpty()) return null;

            // 实体缓存
            if (Meta.Count < 1000) return Meta.Cache.Entities.FirstOrDefault(e => e.ID == id);

            // 单对象缓存
            //return Meta.SingleCache[id];

            return Find(_.ID == id);
        }
                
        /// <summary>根据板块名称查找</summary>
        /// <param name="name">板块名称</param>
        /// <returns>实体列表</returns>
        public static IList<GroupKind> FindByName(String name)
        {
            // 实体缓存
            if (Meta.Count < 1000) return Meta.Cache.Entities.Where(e => e.Name == name).ToList();

            return FindAll(_.Name == name);
        }
        #endregion

        #region 高级查询
        #endregion

        #region 业务操作
        public static void ParseGroupInfo()
        {
            var text = File.ReadAllText("板块信息.txt");
            var doc = new HtmlDocument();
            doc.LoadHtml(text);
            var value = doc.DocumentNode.InnerText;

            Dictionary<string, string> nodeDic = new Dictionary<string, string>()
            {
                {"行业分类",@"/html[1]/body[1]/div[4]/div[1]/div[4]/table[1]/thead[1]" },
                {"地域板块",@"/html[1]/body[1]/div[4]/div[1]/div[5]/table[1]/thead[1]" },
                {"概念板块",@"/html[1]/body[1]/div[4]/div[1]/div[6]/table[1]/thead[1]" }
            };

            foreach (var node in nodeDic)
            {
                var res = doc.DocumentNode.SelectSingleNode(node.Value).SelectNodes(@"tr");
                if (res.Count > 1)
                {
                    for (int i = 1; i < res.Count; i++)
                    {
                        #region 解析每个板块
                        var tda = res[i].SelectSingleNode(@"td[2]/a[1]");
                        if(tda !=null)
                        {
                            var id = tda.Attributes["href"].Value.Trim();
                            var link = "{0}{1}".F(@"http://q.stock.sohu.com/cn/", id);
                            var name = tda.InnerText;

                            GroupKind et = new GroupKind()
                            {
                                ID = id.Replace(".shtml",""),
                                Name = name,
                                Kind = node.Key,
                                CreateDate = DateTime.Now 
                            };
                            et.Save();
                            //抓取每个板块的股票信息
                            StockGroup.ParseStockGroup(et,link);
                        }
                        #endregion
                    }
                }
            }
        }
        #endregion
    }
}