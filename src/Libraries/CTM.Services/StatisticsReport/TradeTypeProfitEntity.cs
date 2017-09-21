using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTM.Services.StatisticsReport
{
   public struct  TradeTypeProfitEntity
    {
        public int TeamId;
        public string TeamName;
        public int DataType;
        public string InvestorCode;
        public string InvestorName;
        public int TradeType;
        public string TradeTypeName;
        public DateTime TradeDate;
        public double CurValue;
        public double MondayValue;
        public double DayFund;
        public double DayProfit;
        public double DayRate;
        public double YearAvgFund;
        public double YearProfit;
        public double YearRate;
        public double AccAvgFund;
        public double AccProfit;
        public double AccRate;
    }
}
