using System;

namespace CTM.Services.TradeRecord
{
    public class DataVerifyEntity
    {
        #region DeliveryData

        public DateTime? DE_TradeDate { get; set; }

        public string DE_StockCode { get; set; }

        public string DE_StockName { get; set; }

        public bool? DE_DealFlag { get; set; }

        public string DE_DealName { get; set; }

        public decimal? DE_TotalActualAmount { get; set; }

        public int? DE_TotalDealVolume { get; set; }

        #endregion DeliveryData

        public decimal AmountDiff { get; set; }

        public int VolumeDiff { get; set; }

        #region DailyData

        public DateTime? DA_TradeDate { get; set; }

        public string DA_StockCode { get; set; }

        public string DA_StockName { get; set; }

        public bool? DA_DealFlag { get; set; }

        public string DA_DealName { get; set; }

        public decimal? DA_TotalActualAmount { get; set; }

        public int? DA_TotalDealVolume { get; set; }

        #endregion DailyData
    }
}