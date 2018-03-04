using System;
using System.Collections.Generic;
using System.ComponentModel;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace StockData.Analysis.Entity
{
    /// <summary>股票分析元数据</summary>
    [Serializable]
    [DataObject]
    [Description("股票分析元数据")]
    [BindTable("StockElementInfo", Description = "股票分析元数据", ConnName = "stock_analysis", DbType = DatabaseType.None)]
    public partial class StockElementInfo : IStockElementInfo
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

        private Double _Price;
        /// <summary>当前价格</summary>
        [DisplayName("当前价格")]
        [Description("当前价格")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Price", "当前价格", "")]
        public Double Price { get { return _Price; } set { if (OnPropertyChanging(__.Price, value)) { _Price = value; OnPropertyChanged(__.Price); } } }

        private Double _Rate;
        /// <summary>当前倍数</summary>
        [DisplayName("当前倍数")]
        [Description("当前倍数")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Rate", "当前倍数", "")]
        public Double Rate { get { return _Rate; } set { if (OnPropertyChanging(__.Rate, value)) { _Rate = value; OnPropertyChanged(__.Rate); } } }

        private Double _MaxPrice;
        /// <summary>历史最高价格</summary>
        [DisplayName("历史最高价格")]
        [Description("历史最高价格")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("MaxPrice", "历史最高价格", "")]
        public Double MaxPrice { get { return _MaxPrice; } set { if (OnPropertyChanging(__.MaxPrice, value)) { _MaxPrice = value; OnPropertyChanged(__.MaxPrice); } } }

        private Double _MinPrice;
        /// <summary>历史最低价格</summary>
        [DisplayName("历史最低价格")]
        [Description("历史最低价格")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("MinPrice", "历史最低价格", "")]
        public Double MinPrice { get { return _MinPrice; } set { if (OnPropertyChanging(__.MinPrice, value)) { _MinPrice = value; OnPropertyChanged(__.MinPrice); } } }

        private Double _MaxRate;
        /// <summary>历史倍数</summary>
        [DisplayName("历史倍数")]
        [Description("历史倍数")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("MaxRate", "历史倍数", "")]
        public Double MaxRate { get { return _MaxRate; } set { if (OnPropertyChanging(__.MaxRate, value)) { _MaxRate = value; OnPropertyChanged(__.MaxRate); } } }

        private Double _Max5year;
        /// <summary>近5年最高</summary>
        [DisplayName("近5年最高")]
        [Description("近5年最高")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Max5year", "近5年最高", "")]
        public Double Max5year { get { return _Max5year; } set { if (OnPropertyChanging(__.Max5year, value)) { _Max5year = value; OnPropertyChanged(__.Max5year); } } }

        private Double _Min5year;
        /// <summary>近5年最低</summary>
        [DisplayName("近5年最低")]
        [Description("近5年最低")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Min5year", "近5年最低", "")]
        public Double Min5year { get { return _Min5year; } set { if (OnPropertyChanging(__.Min5year, value)) { _Min5year = value; OnPropertyChanged(__.Min5year); } } }

        private Double _Max5Rate;
        /// <summary>近5年倍数</summary>
        [DisplayName("近5年倍数")]
        [Description("近5年倍数")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Max5Rate", "近5年倍数", "")]
        public Double Max5Rate { get { return _Max5Rate; } set { if (OnPropertyChanging(__.Max5Rate, value)) { _Max5Rate = value; OnPropertyChanged(__.Max5Rate); } } }

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
                    case __.Code : return _Code;
                    case __.Name : return _Name;
                    case __.Price : return _Price;
                    case __.Rate : return _Rate;
                    case __.MaxPrice : return _MaxPrice;
                    case __.MinPrice : return _MinPrice;
                    case __.MaxRate : return _MaxRate;
                    case __.Max5year : return _Max5year;
                    case __.Min5year : return _Min5year;
                    case __.Max5Rate : return _Max5Rate;
                    case __.UpdateDate : return _UpdateDate;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.Code : _Code = Convert.ToString(value); break;
                    case __.Name : _Name = Convert.ToString(value); break;
                    case __.Price : _Price = Convert.ToDouble(value); break;
                    case __.Rate : _Rate = Convert.ToDouble(value); break;
                    case __.MaxPrice : _MaxPrice = Convert.ToDouble(value); break;
                    case __.MinPrice : _MinPrice = Convert.ToDouble(value); break;
                    case __.MaxRate : _MaxRate = Convert.ToDouble(value); break;
                    case __.Max5year : _Max5year = Convert.ToDouble(value); break;
                    case __.Min5year : _Min5year = Convert.ToDouble(value); break;
                    case __.Max5Rate : _Max5Rate = Convert.ToDouble(value); break;
                    case __.UpdateDate : _UpdateDate = Convert.ToDateTime(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得股票分析元数据字段信息的快捷方式</summary>
        public partial class _
        {
            /// <summary>股票编码</summary>
            public static readonly Field Code = FindByName(__.Code);

            /// <summary>名称</summary>
            public static readonly Field Name = FindByName(__.Name);

            /// <summary>当前价格</summary>
            public static readonly Field Price = FindByName(__.Price);

            /// <summary>当前倍数</summary>
            public static readonly Field Rate = FindByName(__.Rate);

            /// <summary>历史最高价格</summary>
            public static readonly Field MaxPrice = FindByName(__.MaxPrice);

            /// <summary>历史最低价格</summary>
            public static readonly Field MinPrice = FindByName(__.MinPrice);

            /// <summary>历史倍数</summary>
            public static readonly Field MaxRate = FindByName(__.MaxRate);

            /// <summary>近5年最高</summary>
            public static readonly Field Max5year = FindByName(__.Max5year);

            /// <summary>近5年最低</summary>
            public static readonly Field Min5year = FindByName(__.Min5year);

            /// <summary>近5年倍数</summary>
            public static readonly Field Max5Rate = FindByName(__.Max5Rate);

            /// <summary>更新日期</summary>
            public static readonly Field UpdateDate = FindByName(__.UpdateDate);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得股票分析元数据字段名称的快捷方式</summary>
        public partial class __
        {
            /// <summary>股票编码</summary>
            public const String Code = "Code";

            /// <summary>名称</summary>
            public const String Name = "Name";

            /// <summary>当前价格</summary>
            public const String Price = "Price";

            /// <summary>当前倍数</summary>
            public const String Rate = "Rate";

            /// <summary>历史最高价格</summary>
            public const String MaxPrice = "MaxPrice";

            /// <summary>历史最低价格</summary>
            public const String MinPrice = "MinPrice";

            /// <summary>历史倍数</summary>
            public const String MaxRate = "MaxRate";

            /// <summary>近5年最高</summary>
            public const String Max5year = "Max5year";

            /// <summary>近5年最低</summary>
            public const String Min5year = "Min5year";

            /// <summary>近5年倍数</summary>
            public const String Max5Rate = "Max5Rate";

            /// <summary>更新日期</summary>
            public const String UpdateDate = "UpdateDate";
        }
        #endregion
    }

    /// <summary>股票分析元数据接口</summary>
    public partial interface IStockElementInfo
    {
        #region 属性
        /// <summary>股票编码</summary>
        String Code { get; set; }

        /// <summary>名称</summary>
        String Name { get; set; }

        /// <summary>当前价格</summary>
        Double Price { get; set; }

        /// <summary>当前倍数</summary>
        Double Rate { get; set; }

        /// <summary>历史最高价格</summary>
        Double MaxPrice { get; set; }

        /// <summary>历史最低价格</summary>
        Double MinPrice { get; set; }

        /// <summary>历史倍数</summary>
        Double MaxRate { get; set; }

        /// <summary>近5年最高</summary>
        Double Max5year { get; set; }

        /// <summary>近5年最低</summary>
        Double Min5year { get; set; }

        /// <summary>近5年倍数</summary>
        Double Max5Rate { get; set; }

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