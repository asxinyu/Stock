using System;
using System.Collections.Generic;
using System.ComponentModel;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace StockData.Entity
{
    /// <summary>重要财务指标</summary>
    [Serializable]
    [DataObject]
    [Description("重要财务指标")]
    [BindIndex("IX_KeyFinaIndex_Date", false, "Date")]
    [BindIndex("IX_KeyFinaIndex_Code", false, "Code")]
    [BindTable("KeyFinaIndex", Description = "重要财务指标", ConnName = "AssistInfo", DbType = DatabaseType.None)]
    public partial class KeyFinaIndex : IKeyFinaIndex
    {
        #region 属性
        private String _ID;
        /// <summary>ID。股票名称+年月日</summary>
        [DisplayName("ID")]
        [Description("ID。股票名称+年月日")]
        [DataObjectField(true, false, true, 50)]
        [BindColumn("ID", "ID。股票名称+年月日", "")]
        public String ID { get { return _ID; } set { if (OnPropertyChanging(__.ID, value)) { _ID = value; OnPropertyChanged(__.ID); } } }

        private String _Code;
        /// <summary>股票代码</summary>
        [DisplayName("股票代码")]
        [Description("股票代码")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("Code", "股票代码", "")]
        public String Code { get { return _Code; } set { if (OnPropertyChanging(__.Code, value)) { _Code = value; OnPropertyChanged(__.Code); } } }

        private DateTime _Date;
        /// <summary>财报周期，年月日形式</summary>
        [DisplayName("财报周期")]
        [Description("财报周期，年月日形式")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("Date", "财报周期，年月日形式", "")]
        public DateTime Date { get { return _Date; } set { if (OnPropertyChanging(__.Date, value)) { _Date = value; OnPropertyChanged(__.Date); } } }

        private Double _Revenue;
        /// <summary>主营收入。万元</summary>
        [DisplayName("主营收入")]
        [Description("主营收入。万元")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Revenue", "主营收入。万元", "")]
        public Double Revenue { get { return _Revenue; } set { if (OnPropertyChanging(__.Revenue, value)) { _Revenue = value; OnPropertyChanged(__.Revenue); } } }

        private Double _NetIncome;
        /// <summary>净利润。万元</summary>
        [DisplayName("净利润")]
        [Description("净利润。万元")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("NetIncome", "净利润。万元", "")]
        public Double NetIncome { get { return _NetIncome; } set { if (OnPropertyChanging(__.NetIncome, value)) { _NetIncome = value; OnPropertyChanged(__.NetIncome); } } }

        private Double _Profit;
        /// <summary>每股收益。元</summary>
        [DisplayName("每股收益")]
        [Description("每股收益。元")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Profit", "每股收益。元", "")]
        public Double Profit { get { return _Profit; } set { if (OnPropertyChanging(__.Profit, value)) { _Profit = value; OnPropertyChanged(__.Profit); } } }

        private Double _Assets;
        /// <summary>总资产。万元</summary>
        [DisplayName("总资产")]
        [Description("总资产。万元")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Assets", "总资产。万元", "")]
        public Double Assets { get { return _Assets; } set { if (OnPropertyChanging(__.Assets, value)) { _Assets = value; OnPropertyChanged(__.Assets); } } }

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
                    case __.Code : return _Code;
                    case __.Date : return _Date;
                    case __.Revenue : return _Revenue;
                    case __.NetIncome : return _NetIncome;
                    case __.Profit : return _Profit;
                    case __.Assets : return _Assets;
                    case __.CreateDate : return _CreateDate;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.ID : _ID = Convert.ToString(value); break;
                    case __.Code : _Code = Convert.ToString(value); break;
                    case __.Date : _Date = Convert.ToDateTime(value); break;
                    case __.Revenue : _Revenue = Convert.ToDouble(value); break;
                    case __.NetIncome : _NetIncome = Convert.ToDouble(value); break;
                    case __.Profit : _Profit = Convert.ToDouble(value); break;
                    case __.Assets : _Assets = Convert.ToDouble(value); break;
                    case __.CreateDate : _CreateDate = Convert.ToDateTime(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得重要财务指标字段信息的快捷方式</summary>
        public partial class _
        {
            /// <summary>ID。股票名称+年月日</summary>
            public static readonly Field ID = FindByName(__.ID);

            /// <summary>股票代码</summary>
            public static readonly Field Code = FindByName(__.Code);

            /// <summary>财报周期，年月日形式</summary>
            public static readonly Field Date = FindByName(__.Date);

            /// <summary>主营收入。万元</summary>
            public static readonly Field Revenue = FindByName(__.Revenue);

            /// <summary>净利润。万元</summary>
            public static readonly Field NetIncome = FindByName(__.NetIncome);

            /// <summary>每股收益。元</summary>
            public static readonly Field Profit = FindByName(__.Profit);

            /// <summary>总资产。万元</summary>
            public static readonly Field Assets = FindByName(__.Assets);

            /// <summary>创建时间</summary>
            public static readonly Field CreateDate = FindByName(__.CreateDate);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得重要财务指标字段名称的快捷方式</summary>
        public partial class __
        {
            /// <summary>ID。股票名称+年月日</summary>
            public const String ID = "ID";

            /// <summary>股票代码</summary>
            public const String Code = "Code";

            /// <summary>财报周期，年月日形式</summary>
            public const String Date = "Date";

            /// <summary>主营收入。万元</summary>
            public const String Revenue = "Revenue";

            /// <summary>净利润。万元</summary>
            public const String NetIncome = "NetIncome";

            /// <summary>每股收益。元</summary>
            public const String Profit = "Profit";

            /// <summary>总资产。万元</summary>
            public const String Assets = "Assets";

            /// <summary>创建时间</summary>
            public const String CreateDate = "CreateDate";
        }
        #endregion
    }

    /// <summary>重要财务指标接口</summary>
    public partial interface IKeyFinaIndex
    {
        #region 属性
        /// <summary>ID。股票名称+年月日</summary>
        String ID { get; set; }

        /// <summary>股票代码</summary>
        String Code { get; set; }

        /// <summary>财报周期，年月日形式</summary>
        DateTime Date { get; set; }

        /// <summary>主营收入。万元</summary>
        Double Revenue { get; set; }

        /// <summary>净利润。万元</summary>
        Double NetIncome { get; set; }

        /// <summary>每股收益。元</summary>
        Double Profit { get; set; }

        /// <summary>总资产。万元</summary>
        Double Assets { get; set; }

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