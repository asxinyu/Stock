using System;
using System.Collections.Generic;
using System.ComponentModel;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace StockData.Entity
{
    /// <summary>板块分类</summary>
    [Serializable]
    [DataObject]
    [Description("板块分类")]
    [BindIndex("IX_GroupKind_Kind", false, "Kind")]
    [BindIndex("IX_GroupKind_Name", false, "Name")]
    [BindTable("GroupKind", Description = "板块分类", ConnName = "stock_base", DbType = DatabaseType.None)]
    public partial class GroupKind : IGroupKind
    {
        #region 属性
        private String _ID;
        /// <summary>编码。url相关</summary>
        [DisplayName("编码")]
        [Description("编码。url相关")]
        [DataObjectField(true, false, true, 50)]
        [BindColumn("ID", "编码。url相关", "")]
        public String ID { get { return _ID; } set { if (OnPropertyChanging(__.ID, value)) { _ID = value; OnPropertyChanged(__.ID); } } }

        private String _Name;
        /// <summary>板块名称</summary>
        [DisplayName("板块名称")]
        [Description("板块名称")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("Name", "板块名称", "", Master = true)]
        public String Name { get { return _Name; } set { if (OnPropertyChanging(__.Name, value)) { _Name = value; OnPropertyChanged(__.Name); } } }

        private String _Kind;
        /// <summary>分类。1.行业，2地域，3.概念</summary>
        [DisplayName("分类")]
        [Description("分类。1.行业，2地域，3.概念")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("Kind", "分类。1.行业，2地域，3.概念", "")]
        public String Kind { get { return _Kind; } set { if (OnPropertyChanging(__.Kind, value)) { _Kind = value; OnPropertyChanged(__.Kind); } } }

        private Int32 _Total;
        /// <summary>总数</summary>
        [DisplayName("总数")]
        [Description("总数")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Total", "总数", "")]
        public Int32 Total { get { return _Total; } set { if (OnPropertyChanging(__.Total, value)) { _Total = value; OnPropertyChanged(__.Total); } } }

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
                    case __.Name : return _Name;
                    case __.Kind : return _Kind;
                    case __.Total : return _Total;
                    case __.CreateDate : return _CreateDate;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.ID : _ID = Convert.ToString(value); break;
                    case __.Name : _Name = Convert.ToString(value); break;
                    case __.Kind : _Kind = Convert.ToString(value); break;
                    case __.Total : _Total = Convert.ToInt32(value); break;
                    case __.CreateDate : _CreateDate = Convert.ToDateTime(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得板块分类字段信息的快捷方式</summary>
        public partial class _
        {
            /// <summary>编码。url相关</summary>
            public static readonly Field ID = FindByName(__.ID);

            /// <summary>板块名称</summary>
            public static readonly Field Name = FindByName(__.Name);

            /// <summary>分类。1.行业，2地域，3.概念</summary>
            public static readonly Field Kind = FindByName(__.Kind);

            /// <summary>总数</summary>
            public static readonly Field Total = FindByName(__.Total);

            /// <summary>创建时间</summary>
            public static readonly Field CreateDate = FindByName(__.CreateDate);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得板块分类字段名称的快捷方式</summary>
        public partial class __
        {
            /// <summary>编码。url相关</summary>
            public const String ID = "ID";

            /// <summary>板块名称</summary>
            public const String Name = "Name";

            /// <summary>分类。1.行业，2地域，3.概念</summary>
            public const String Kind = "Kind";

            /// <summary>总数</summary>
            public const String Total = "Total";

            /// <summary>创建时间</summary>
            public const String CreateDate = "CreateDate";
        }
        #endregion
    }

    /// <summary>板块分类接口</summary>
    public partial interface IGroupKind
    {
        #region 属性
        /// <summary>编码。url相关</summary>
        String ID { get; set; }

        /// <summary>板块名称</summary>
        String Name { get; set; }

        /// <summary>分类。1.行业，2地域，3.概念</summary>
        String Kind { get; set; }

        /// <summary>总数</summary>
        Int32 Total { get; set; }

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