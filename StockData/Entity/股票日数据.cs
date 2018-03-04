using System;
using System.Collections.Generic;
using System.ComponentModel;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace StockData.Entity
{
    /// <summary>股票日数据</summary>
    [Serializable]
    [DataObject]
    [Description("股票日数据")]
    [BindTable("StockDayData", Description = "股票日数据", ConnName = "stock_day", DbType = DatabaseType.None)]
    public partial class StockDayData : IStockDayData
    {
        #region 属性
        private String _ID;
        /// <summary>编号。code+日期</summary>
        [DisplayName("编号")]
        [Description("编号。code+日期")]
        [DataObjectField(true, false, true, 50)]
        [BindColumn("ID", "编号。code+日期", "")]
        public String ID { get { return _ID; } set { if (OnPropertyChanging(__.ID, value)) { _ID = value; OnPropertyChanged(__.ID); } } }

        private String _Code;
        /// <summary>股票编码</summary>
        [DisplayName("股票编码")]
        [Description("股票编码")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("Code", "股票编码", "")]
        public String Code { get { return _Code; } set { if (OnPropertyChanging(__.Code, value)) { _Code = value; OnPropertyChanged(__.Code); } } }

        private DateTime _StatDate;
        /// <summary>数据日期</summary>
        [DisplayName("数据日期")]
        [Description("数据日期")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("StatDate", "数据日期", "")]
        public DateTime StatDate { get { return _StatDate; } set { if (OnPropertyChanging(__.StatDate, value)) { _StatDate = value; OnPropertyChanged(__.StatDate); } } }

        private Double _StartPrice;
        /// <summary>开盘价格</summary>
        [DisplayName("开盘价格")]
        [Description("开盘价格")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("StartPrice", "开盘价格", "")]
        public Double StartPrice { get { return _StartPrice; } set { if (OnPropertyChanging(__.StartPrice, value)) { _StartPrice = value; OnPropertyChanged(__.StartPrice); } } }

        private Double _EndPrice;
        /// <summary>收盘价格</summary>
        [DisplayName("收盘价格")]
        [Description("收盘价格")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("EndPrice", "收盘价格", "")]
        public Double EndPrice { get { return _EndPrice; } set { if (OnPropertyChanging(__.EndPrice, value)) { _EndPrice = value; OnPropertyChanged(__.EndPrice); } } }

        private Double _ChangePrice;
        /// <summary>涨跌金额</summary>
        [DisplayName("涨跌金额")]
        [Description("涨跌金额")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("ChangePrice", "涨跌金额", "")]
        public Double ChangePrice { get { return _ChangePrice; } set { if (OnPropertyChanging(__.ChangePrice, value)) { _ChangePrice = value; OnPropertyChanged(__.ChangePrice); } } }

        private Double _ChangeRatio;
        /// <summary>涨跌幅度</summary>
        [DisplayName("涨跌幅度")]
        [Description("涨跌幅度")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("ChangeRatio", "涨跌幅度", "")]
        public Double ChangeRatio { get { return _ChangeRatio; } set { if (OnPropertyChanging(__.ChangeRatio, value)) { _ChangeRatio = value; OnPropertyChanged(__.ChangeRatio); } } }

        private Double _LowPrice;
        /// <summary>最低价格</summary>
        [DisplayName("最低价格")]
        [Description("最低价格")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("LowPrice", "最低价格", "")]
        public Double LowPrice { get { return _LowPrice; } set { if (OnPropertyChanging(__.LowPrice, value)) { _LowPrice = value; OnPropertyChanged(__.LowPrice); } } }

        private Double _HighPrice;
        /// <summary>最高价格</summary>
        [DisplayName("最高价格")]
        [Description("最高价格")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("HighPrice", "最高价格", "")]
        public Double HighPrice { get { return _HighPrice; } set { if (OnPropertyChanging(__.HighPrice, value)) { _HighPrice = value; OnPropertyChanged(__.HighPrice); } } }

        private Int32 _TotalHand;
        /// <summary>总手</summary>
        [DisplayName("总手")]
        [Description("总手")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("TotalHand", "总手", "")]
        public Int32 TotalHand { get { return _TotalHand; } set { if (OnPropertyChanging(__.TotalHand, value)) { _TotalHand = value; OnPropertyChanged(__.TotalHand); } } }

        private Double _TotalAmount;
        /// <summary>总金额(万)</summary>
        [DisplayName("总金额")]
        [Description("总金额(万)")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("TotalAmount", "总金额(万)", "")]
        public Double TotalAmount { get { return _TotalAmount; } set { if (OnPropertyChanging(__.TotalAmount, value)) { _TotalAmount = value; OnPropertyChanged(__.TotalAmount); } } }

        private Double _HandRate;
        /// <summary>换手率</summary>
        [DisplayName("换手率")]
        [Description("换手率")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("HandRate", "换手率", "")]
        public Double HandRate { get { return _HandRate; } set { if (OnPropertyChanging(__.HandRate, value)) { _HandRate = value; OnPropertyChanged(__.HandRate); } } }

        private DateTime _UpdateDate;
        /// <summary>更新日期</summary>
        [DisplayName("更新日期")]
        [Description("更新日期")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("UpdateDate", "更新日期", "")]
        public DateTime UpdateDate { get { return _UpdateDate; } set { if (OnPropertyChanging(__.UpdateDate, value)) { _UpdateDate = value; OnPropertyChanged(__.UpdateDate); } } }
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
                    case __.StatDate : return _StatDate;
                    case __.StartPrice : return _StartPrice;
                    case __.EndPrice : return _EndPrice;
                    case __.ChangePrice : return _ChangePrice;
                    case __.ChangeRatio : return _ChangeRatio;
                    case __.LowPrice : return _LowPrice;
                    case __.HighPrice : return _HighPrice;
                    case __.TotalHand : return _TotalHand;
                    case __.TotalAmount : return _TotalAmount;
                    case __.HandRate : return _HandRate;
                    case __.UpdateDate : return _UpdateDate;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.ID : _ID = Convert.ToString(value); break;
                    case __.Code : _Code = Convert.ToString(value); break;
                    case __.StatDate : _StatDate = Convert.ToDateTime(value); break;
                    case __.StartPrice : _StartPrice = Convert.ToDouble(value); break;
                    case __.EndPrice : _EndPrice = Convert.ToDouble(value); break;
                    case __.ChangePrice : _ChangePrice = Convert.ToDouble(value); break;
                    case __.ChangeRatio : _ChangeRatio = Convert.ToDouble(value); break;
                    case __.LowPrice : _LowPrice = Convert.ToDouble(value); break;
                    case __.HighPrice : _HighPrice = Convert.ToDouble(value); break;
                    case __.TotalHand : _TotalHand = Convert.ToInt32(value); break;
                    case __.TotalAmount : _TotalAmount = Convert.ToDouble(value); break;
                    case __.HandRate : _HandRate = Convert.ToDouble(value); break;
                    case __.UpdateDate : _UpdateDate = Convert.ToDateTime(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得股票日数据字段信息的快捷方式</summary>
        public partial class _
        {
            /// <summary>编号。code+日期</summary>
            public static readonly Field ID = FindByName(__.ID);

            /// <summary>股票编码</summary>
            public static readonly Field Code = FindByName(__.Code);

            /// <summary>数据日期</summary>
            public static readonly Field StatDate = FindByName(__.StatDate);

            /// <summary>开盘价格</summary>
            public static readonly Field StartPrice = FindByName(__.StartPrice);

            /// <summary>收盘价格</summary>
            public static readonly Field EndPrice = FindByName(__.EndPrice);

            /// <summary>涨跌金额</summary>
            public static readonly Field ChangePrice = FindByName(__.ChangePrice);

            /// <summary>涨跌幅度</summary>
            public static readonly Field ChangeRatio = FindByName(__.ChangeRatio);

            /// <summary>最低价格</summary>
            public static readonly Field LowPrice = FindByName(__.LowPrice);

            /// <summary>最高价格</summary>
            public static readonly Field HighPrice = FindByName(__.HighPrice);

            /// <summary>总手</summary>
            public static readonly Field TotalHand = FindByName(__.TotalHand);

            /// <summary>总金额(万)</summary>
            public static readonly Field TotalAmount = FindByName(__.TotalAmount);

            /// <summary>换手率</summary>
            public static readonly Field HandRate = FindByName(__.HandRate);

            /// <summary>更新日期</summary>
            public static readonly Field UpdateDate = FindByName(__.UpdateDate);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得股票日数据字段名称的快捷方式</summary>
        public partial class __
        {
            /// <summary>编号。code+日期</summary>
            public const String ID = "ID";

            /// <summary>股票编码</summary>
            public const String Code = "Code";

            /// <summary>数据日期</summary>
            public const String StatDate = "StatDate";

            /// <summary>开盘价格</summary>
            public const String StartPrice = "StartPrice";

            /// <summary>收盘价格</summary>
            public const String EndPrice = "EndPrice";

            /// <summary>涨跌金额</summary>
            public const String ChangePrice = "ChangePrice";

            /// <summary>涨跌幅度</summary>
            public const String ChangeRatio = "ChangeRatio";

            /// <summary>最低价格</summary>
            public const String LowPrice = "LowPrice";

            /// <summary>最高价格</summary>
            public const String HighPrice = "HighPrice";

            /// <summary>总手</summary>
            public const String TotalHand = "TotalHand";

            /// <summary>总金额(万)</summary>
            public const String TotalAmount = "TotalAmount";

            /// <summary>换手率</summary>
            public const String HandRate = "HandRate";

            /// <summary>更新日期</summary>
            public const String UpdateDate = "UpdateDate";
        }
        #endregion
    }

    /// <summary>股票日数据接口</summary>
    public partial interface IStockDayData
    {
        #region 属性
        /// <summary>编号。code+日期</summary>
        String ID { get; set; }

        /// <summary>股票编码</summary>
        String Code { get; set; }

        /// <summary>数据日期</summary>
        DateTime StatDate { get; set; }

        /// <summary>开盘价格</summary>
        Double StartPrice { get; set; }

        /// <summary>收盘价格</summary>
        Double EndPrice { get; set; }

        /// <summary>涨跌金额</summary>
        Double ChangePrice { get; set; }

        /// <summary>涨跌幅度</summary>
        Double ChangeRatio { get; set; }

        /// <summary>最低价格</summary>
        Double LowPrice { get; set; }

        /// <summary>最高价格</summary>
        Double HighPrice { get; set; }

        /// <summary>总手</summary>
        Int32 TotalHand { get; set; }

        /// <summary>总金额(万)</summary>
        Double TotalAmount { get; set; }

        /// <summary>换手率</summary>
        Double HandRate { get; set; }

        /// <summary>更新日期</summary>
        DateTime UpdateDate { get; set; }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        Object this[String name] { get; set; }
        #endregion
    }
}