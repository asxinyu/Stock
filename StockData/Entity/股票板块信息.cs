using System;
using System.Collections.Generic;
using System.ComponentModel;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace StockData.Entity
{
    /// <summary>股票板块信息</summary>
    [Serializable]
    [DataObject]
    [Description("股票板块信息")]
    [BindIndex("IX_StockGroup_GroupID", false, "GroupID")]
    [BindIndex("IX_StockGroup_Code", false, "Code")]
    [BindTable("StockGroup", Description = "股票板块信息", ConnName = "stock_base", DbType = DatabaseType.None)]
    public partial class StockGroup : IStockGroup
    {
        #region 属性
        private String _ID;
        /// <summary>编号。groupid+stockid</summary>
        [DisplayName("编号")]
        [Description("编号。groupid+stockid")]
        [DataObjectField(true, false, true, 50)]
        [BindColumn("ID", "编号。groupid+stockid", "")]
        public String ID { get { return _ID; } set { if (OnPropertyChanging(__.ID, value)) { _ID = value; OnPropertyChanged(__.ID); } } }

        private String _GroupID;
        /// <summary>板块ID</summary>
        [DisplayName("板块ID")]
        [Description("板块ID")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("GroupID", "板块ID", "")]
        public String GroupID { get { return _GroupID; } set { if (OnPropertyChanging(__.GroupID, value)) { _GroupID = value; OnPropertyChanged(__.GroupID); } } }

        private String _Kind;
        /// <summary>分类。1.行业，2地域，3.概念</summary>
        [DisplayName("分类")]
        [Description("分类。1.行业，2地域，3.概念")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("Kind", "分类。1.行业，2地域，3.概念", "")]
        public String Kind { get { return _Kind; } set { if (OnPropertyChanging(__.Kind, value)) { _Kind = value; OnPropertyChanged(__.Kind); } } }

        private String _Code;
        /// <summary>股票代码</summary>
        [DisplayName("股票代码")]
        [Description("股票代码")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("Code", "股票代码", "")]
        public String Code { get { return _Code; } set { if (OnPropertyChanging(__.Code, value)) { _Code = value; OnPropertyChanged(__.Code); } } }

        private String _StockName;
        /// <summary>股票名称</summary>
        [DisplayName("股票名称")]
        [Description("股票名称")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("StockName", "股票名称", "")]
        public String StockName { get { return _StockName; } set { if (OnPropertyChanging(__.StockName, value)) { _StockName = value; OnPropertyChanged(__.StockName); } } }

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
                    case __.ID : return _ID;
                    case __.GroupID : return _GroupID;
                    case __.Kind : return _Kind;
                    case __.Code : return _Code;
                    case __.StockName : return _StockName;
                    case __.CreateDate : return _CreateDate;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.ID : _ID = Convert.ToString(value); break;
                    case __.GroupID : _GroupID = Convert.ToString(value); break;
                    case __.Kind : _Kind = Convert.ToString(value); break;
                    case __.Code : _Code = Convert.ToString(value); break;
                    case __.StockName : _StockName = Convert.ToString(value); break;
                    case __.CreateDate : _CreateDate = Convert.ToDateTime(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得股票板块信息字段信息的快捷方式</summary>
        public partial class _
        {
            /// <summary>编号。groupid+stockid</summary>
            public static readonly Field ID = FindByName(__.ID);

            /// <summary>板块ID</summary>
            public static readonly Field GroupID = FindByName(__.GroupID);

            /// <summary>分类。1.行业，2地域，3.概念</summary>
            public static readonly Field Kind = FindByName(__.Kind);

            /// <summary>股票代码</summary>
            public static readonly Field Code = FindByName(__.Code);

            /// <summary>股票名称</summary>
            public static readonly Field StockName = FindByName(__.StockName);

            /// <summary>创建时间</summary>
            public static readonly Field CreateDate = FindByName(__.CreateDate);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得股票板块信息字段名称的快捷方式</summary>
        public partial class __
        {
            /// <summary>编号。groupid+stockid</summary>
            public const String ID = "ID";

            /// <summary>板块ID</summary>
            public const String GroupID = "GroupID";

            /// <summary>分类。1.行业，2地域，3.概念</summary>
            public const String Kind = "Kind";

            /// <summary>股票代码</summary>
            public const String Code = "Code";

            /// <summary>股票名称</summary>
            public const String StockName = "StockName";

            /// <summary>创建时间</summary>
            public const String CreateDate = "CreateDate";
        }
        #endregion
    }

    /// <summary>股票板块信息接口</summary>
    public partial interface IStockGroup
    {
        #region 属性
        /// <summary>编号。groupid+stockid</summary>
        String ID { get; set; }

        /// <summary>板块ID</summary>
        String GroupID { get; set; }

        /// <summary>分类。1.行业，2地域，3.概念</summary>
        String Kind { get; set; }

        /// <summary>股票代码</summary>
        String Code { get; set; }

        /// <summary>股票名称</summary>
        String StockName { get; set; }

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