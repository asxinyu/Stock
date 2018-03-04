using System;
using System.Collections.Generic;
using System.ComponentModel;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace StockData.Entity
{
    /// <summary>指数信息</summary>
    [Serializable]
    [DataObject]
    [Description("指数信息")]
    [BindIndex("IX_IndexInfo_Name", false, "Name")]
    [BindTable("IndexInfo", Description = "指数信息", ConnName = "stock_base", DbType = DatabaseType.None)]
    public partial class IndexInfo : IIndexInfo
    {
        #region 属性
        private String _Code;
        /// <summary>股票编码</summary>
        [DisplayName("股票编码")]
        [Description("股票编码")]
        [DataObjectField(true, false, true, 50)]
        [BindColumn("Code", "股票编码", "")]
        public String Code { get { return _Code; } set { if (OnPropertyChanging(__.Code, value)) { _Code = value; OnPropertyChanged(__.Code); } } }

        private String _Name;
        /// <summary>名称</summary>
        [DisplayName("名称")]
        [Description("名称")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("Name", "名称", "", Master = true)]
        public String Name { get { return _Name; } set { if (OnPropertyChanging(__.Name, value)) { _Name = value; OnPropertyChanged(__.Name); } } }

        private DateTime _CreateDate;
        /// <summary>创建时间</summary>
        [DisplayName("创建时间")]
        [Description("创建时间")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("CreateDate", "创建时间", "")]
        public DateTime CreateDate { get { return _CreateDate; } set { if (OnPropertyChanging(__.CreateDate, value)) { _CreateDate = value; OnPropertyChanged(__.CreateDate); } } }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        public override Object this[String name]
        {
            get
            {
                switch (name)
                {
                    case __.Code : return _Code;
                    case __.Name : return _Name;
                    case __.CreateDate : return _CreateDate;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.Code : _Code = Convert.ToString(value); break;
                    case __.Name : _Name = Convert.ToString(value); break;
                    case __.CreateDate : _CreateDate = Convert.ToDateTime(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得指数信息字段信息的快捷方式</summary>
        public partial class _
        {
            /// <summary>股票编码</summary>
            public static readonly Field Code = FindByName(__.Code);

            /// <summary>名称</summary>
            public static readonly Field Name = FindByName(__.Name);

            /// <summary>创建时间</summary>
            public static readonly Field CreateDate = FindByName(__.CreateDate);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得指数信息字段名称的快捷方式</summary>
        public partial class __
        {
            /// <summary>股票编码</summary>
            public const String Code = "Code";

            /// <summary>名称</summary>
            public const String Name = "Name";

            /// <summary>创建时间</summary>
            public const String CreateDate = "CreateDate";
        }
        #endregion
    }

    /// <summary>指数信息接口</summary>
    public partial interface IIndexInfo
    {
        #region 属性
        /// <summary>股票编码</summary>
        String Code { get; set; }

        /// <summary>名称</summary>
        String Name { get; set; }

        /// <summary>创建时间</summary>
        DateTime CreateDate { get; set; }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        Object this[String name] { get; set; }
        #endregion
    }
}