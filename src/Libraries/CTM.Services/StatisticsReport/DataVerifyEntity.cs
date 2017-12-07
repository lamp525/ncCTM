using System;

namespace CTM.Services.StatisticsReport
{
    public class DataVerifyEntity
    {
        #region DeliveryData

        public int AccountId { get; set; }
        public string AccountInfo { get; set; }

        public Int64 SerialNo { get; set; }

        public DateTime? DE_TradeDate { get; set; }

        public string DE_StockCode { get; set; }

        public string DE_StockName { get; set; }

        public bool? DE_DealFlag { get; set; }

        public string DE_DealName { get; set; }

        public decimal? DE_TotalActualAmount { get; set; }

        public decimal? DE_TotalDealVolume { get; set; }

        #endregion DeliveryData

        public decimal AmountDiff { get; set; }

        public decimal VolumeDiff { get; set; }

        #region DailyData

        public DateTime? DA_TradeDate { get; set; }

        public string DA_StockCode { get; set; }

        public string DA_StockName { get; set; }

        public bool? DA_DealFlag { get; set; }

        public string DA_DealName { get; set; }

        public decimal? DA_TotalActualAmount { get; set; }

        public decimal? DA_TotalDealVolume { get; set; }

        #endregion DailyData
    }
}