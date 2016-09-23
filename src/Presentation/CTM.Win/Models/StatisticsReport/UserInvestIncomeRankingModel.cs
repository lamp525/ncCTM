using System;
using System.Collections.Generic;
using System.Linq;
using CTM.Core.Domain.User;

namespace CTM.Win.Models
{
    public class UserInvestIncomeRankingModel
    {
        public UserInfo Investor { get; set; }

        public decimal DayIncomeRate { get; set; }

        public decimal AverageDayIncomeRateOfWeek { get; set; }

        public decimal AverageDayIncomeRateOfMonth { get; set; }

        public decimal AccumulatedIncomeRate { get; set; }
    }
}