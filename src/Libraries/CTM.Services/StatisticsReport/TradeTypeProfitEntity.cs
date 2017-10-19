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

        public decimal CurValue { get; set; }

        public decimal FridayYearProfit { get; set; }      

        public decimal DayFund { get; set; }

        public decimal DayProfit { get; set; }

        public decimal DayRate { get; set; }

        public decimal YearAvgFund { get; set; }

        public decimal YearProfit { get; set; }

        public decimal YearRate { get; set; }

        public decimal AccAvgFund { get; set; }

        public decimal AccProfit { get; set; }

        public decimal AccRate { get; set; }
        public decimal InvestFund { get; set; }
    }
}