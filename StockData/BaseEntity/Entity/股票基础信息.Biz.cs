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
using NewLife.Serialization;
using NewLife.Threading;
using NewLife.Web;
using XCode;
using XCode.Cache;
using XCode.Configuration;
using XCode.DataAccessLayer;
using XCode.Membership;

namespace StockData.Entity
{
    /// <summary>股票基础信息</summary>
    public partial class StockInfo : Entity<StockInfo>
    {
        #region 对象操作
        static StockInfo()
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

        //    if (XTrace.Debug) XTrace.WriteLine("开始初始化StockInfo[股票基础信息]数据……");

        //    var entity = new StockInfo();
        //    entity.Code = "abc";
        //    entity.Name = "abc";
        //    entity.Kind = "abc";
        //    entity.StartDate = DateTime.Now;
        //    entity.CreateDate = DateTime.Now;
        //    entity.Insert();

        //    if (XTrace.Debug) XTrace.WriteLine("完成初始化StockInfo[股票基础信息]数据！"
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
        public static StockInfo FindByCode(String code)
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
        public static IList<StockInfo> FindByName(String name)
        {
            // 实体缓存
            if (Meta.Count < 1000) return Meta.Cache.Entities.Where(e => e.Name == name).ToList();

            return FindAll(_.Name == name);
        }
        #endregion

        #region 高级查询
        #endregion

        #region 业务操作
        public static void FecthAll()
        {
            
            string url = @"http://88.push2.eastmoney.com/api/qt/clist/get?cb=jQuery112406682232142629425_1612422563756&pn=1&pz=20&po=1&np=1&ut=bd1d9ddb04089700cf9c27f6f7426281&fltt=2&invt=2&fid=f3&fs=m:0+t:6,m:0+t:13,m:0+t:80,m:1+t:2,m:1+t:23&fields=f12,f14&_=1612422563774";

            var doc = url.GetHtmlByUrl();
            var text = doc.DocumentNode.InnerText;
            //移除前后干扰
            var fir = text.IndexOf("[")+1;
            text = text.Remove(0, fir);
            var last = text.LastIndexOf("]");
            text = text.Remove(last, text.Length - last);
            //分割为kv数组
            var list = text.Replace("\""," ").Split("{", "},{", "}")
                        .Select(n => n.SplitAsDictionary(":").ToList())
                        .Select(n=> new KeyValuePair<string, string>( n[0].Value.Trim(),n[1].Value.Trim()))
                        .ToList();
            List<StockInfo> addlist = new List<StockInfo>();
            for (int i = 0; i < list.Count; i++)
            {
                XTrace.WriteLine("当前进度：{0}-{1}",i+1,list.Count);
                var model = StockInfo.FindByCode(list[i].Key);
                if (model != null) continue;
                model = new StockInfo()
                {
                    Code = list[i].Key,
                    Name = list[i].Value,                    
                    CreateDate = DateTime.Now
                };
                addlist.Add(model);
            }
            addlist.Save();
        }
        #endregion
    }
}