using System;
using System.Collections.Generic;
using System.ComponentModel;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace StockData.Entity
{
    /// <summary>股票历史文本数据</summary>
    [Serializable]
    [DataObject]
    [Description("股票历史文本数据")]
    [BindTable("StockHisText", Description = "股票历史文本数据", ConnName = "stock_his", DbType = DatabaseType.None)]
    public partial class StockHisText : IStockHisText
    {
        #region 属性
        private String _Code;
        /// <summary>股票编码</summary>
        [DisplayName("股票编码")]
        [Description("股票编码")]
        [DataObjectField(true, false, true, 50)]
        [BindColumn("Code", "股票编码", "")]
        public String Code { get { return _Code; } set { if (OnPropertyChanging(__.Code, value)) { _Code = value; OnPropertyChanged(__.Code); } } }

        private DateTime _Start;
        /// <summary>起始日期</summary>
        [DisplayName("起始日期")]
        [Description("起始日期")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("Start", "起始日期", "")]
        public DateTime Start { get { return _Start; } set { if (OnPropertyChanging(__.Start, value)) { _Start = value; OnPropertyChanged(__.Start); } } }

        private DateTime _End;
        /// <summary>结束日期</summary>
        [DisplayName("结束日期")]
        [Description("结束日期")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("End", "结束日期", "")]
        public DateTime End { get { return _End; } set { if (OnPropertyChanging(__.End, value)) { _End = value; OnPropertyChanged(__.End); } } }

        private String _HisText;
        /// <summary>历史数据文本</summary>
        [DisplayName("历史数据文本")]
        [Description("历史数据文本")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("HisText", "历史数据文本", "Text")]
        public String HisText { get { return _HisText; } set { if (OnPropertyChanging(__.HisText, value)) { _HisText = value; OnPropertyChanged(__.HisText); } } }
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
                    case __.Start : return _Start;
                    case __.End : return _End;
                    case __.HisText : return _HisText;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.Code : _Code = Convert.ToString(value); break;
                    case __.Start : _Start = Convert.ToDateTime(value); break;
                    case __.End : _End = Convert.ToDateTime(value); break;
                    case __.HisText : _HisText = Convert.ToString(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得股票历史文本数据字段信息的快捷方式</summary>
        public partial class _
        {
            /// <summary>股票编码</summary>
            public static readonly Field Code = FindByName(__.Code);

            /// <summary>起始日期</summary>
            public static readonly Field Start = FindByName(__.Start);

            /// <summary>结束日期</summary>
            public static readonly Field End = FindByName(__.End);

            /// <summary>历史数据文本</summary>
            public static readonly Field HisText = FindByName(__.HisText);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得股票历史文本数据字段名称的快捷方式</summary>
        public partial class __
        {
            /// <summary>股票编码</summary>
            public const String Code = "Code";

            /// <summary>起始日期</summary>
            public const String Start = "Start";

            /// <summary>结束日期</summary>
            public const String End = "End";

            /// <summary>历史数据文本</summary>
            public const String HisText = "HisText";
        }
        #endregion
    }

    /// <summary>股票历史文本数据接口</summary>
    public partial interface IStockHisText
    {
        #region 属性
        /// <summary>股票编码</summary>
        String Code { get; set; }

        /// <summary>起始日期</summary>
        DateTime Start { get; set; }

        /// <summary>结束日期</summary>
        DateTime End { get; set; }

        /// <summary>历史数据文本</summary>
        String HisText { get; set; }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        Object this[String name] { get; set; }
        #endregion
    }
}