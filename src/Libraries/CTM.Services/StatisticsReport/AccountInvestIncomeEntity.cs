﻿using System;

namespace CTM.Services.StatisticsReport
{
    public class AccountInvestIncomeEntity
    {
        public string AccountName { get; set; }

        public string SecurityCompanyName { get; set; }

        public string AccountAttributeName { get; set; }

        public string AccountTypeName { get; set; }

        public DateTime TradeTime { get; set; }

        public decimal MondayPositionValue { get; set; }

        public decimal CurrentAsset { get; set; }

        public decimal AccumulatedProfit { get; set; }

        public decimal AccumulatedIncomeRate { get; set; }

        public decimal CurrentProfit { get; set; }

        public decimal CurrentIncomeRate { get; set; }

        public decimal PositionValue { get; set; }

        public decimal AllotFund { get; set; }

        public decimal FundOccupyAmount { get; set; }

        public decimal PositionRate { get; set; }

        public decimal AnnualProfit { get; set; }

        public decimal AnnualIncomeRate { get; set; }
    }
}