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

namespace StockData.Entity
{
    /// <summary>指数信息</summary>
    public partial class IndexInfo : Entity<IndexInfo>
    {
        #region 对象操作
        static IndexInfo()
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

        //    if (XTrace.Debug) XTrace.WriteLine("开始初始化IndexInfo[指数信息]数据……");

        //    var entity = new IndexInfo();
        //    entity.Code = "abc";
        //    entity.Name = "abc";
        //    entity.CreateDate = DateTime.Now;
        //    entity.Insert();

        //    if (XTrace.Debug) XTrace.WriteLine("完成初始化IndexInfo[指数信息]数据！"
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
        public static IndexInfo FindByCode(String code)
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
        public static IList<IndexInfo> FindByName(String name)
        {
            // 实体缓存
            if (Meta.Count < 1000) return Meta.Cache.Entities.Where(e => e.Name == name).ToList();

            return FindAll(_.Name == name);
        }
        #endregion

        #region 高级查询
        #endregion

        #region 业务操作
        static Dictionary<string, string> IndexDic = new Dictionary<string, string>()
        {
            {"000001","上证指数" },{ "000002","A股指数"},{"000003","B股指数" }, { "000004","工业指数"},
            {"000005","商业指数" },{ "000006","地产指数"},{"000007","公用指数"}, { "000008","综合指数" },
            {"399001","深证成指" },{ "399002","深成指R"},{"399003","成份B指" },{ "000300","沪深300"},
            {"000903","中证100" }, { "000905","中证500"},{"000906","中证800" },{ "399007","深证300"},
            {"399008","中小300" }, { "399101","中小板综"},{"399106","深证综指" },{ "399107","深证A指"},
            {"399108","深证B指" }, { "399300","沪深300T"}
        };

        public static void GetIndexInfo()
        {
            foreach (var item in IndexDic)
            {
                IndexInfo et = new IndexInfo()
                {
                    Code = item.Key,
                    Name = item.Value,
                    CreateDate = DateTime.Now
                };
                et.Save();
            }
        }
        #endregion
    }
}