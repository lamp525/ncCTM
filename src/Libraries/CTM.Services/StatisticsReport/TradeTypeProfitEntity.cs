using System;

namespace CTM.Services.StatisticsReport
{
    public class TradeTypeProfitEntity
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public int DataType { get; set; }
        public string InvestorCode { get; set; }
        public string InvestorName { get; set; }
        public int TradeType { get; set; }
        public string TradeTypeName { get; set; }
        public DateTime TradeDate { get; set; }
        public double CurValue { get; set; }
        public double MondayValue { get; set; }
        public double DayFund { get; set; }
        public double DayProfit { get; set; }
        public double DayRate { get; set; }
        public double YearAvgFund { get; set; }
        public double YearProfit { get; set; }
        public double YearRate { get; set; }
        public double AccAvgFund { get; set; }
        public double AccProfit { get; set; }
        public double AccRate { get; set; }
    }
}