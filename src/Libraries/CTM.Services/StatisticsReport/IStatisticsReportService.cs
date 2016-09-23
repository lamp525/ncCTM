using System;
using System.Collections.Generic;
using System.Linq;
using CTM.Core.Domain.TKLine;
using CTM.Core.Domain.TradeRecord;
using CTM.Core.Domain.User;

namespace CTM.Services.StatisticsReport
{
    public partial interface IStatisticsReportService : IBaseService
    {
        IList<UserInvestIncomeEntity> CalculateUserInvestIncome(UserInfo investorInfo, IList<string> statisticalInvestorCodes, IList<DailyRecord> tradeRecords, IList<DateTime> statisticalDates, IList<TKLineToday> stockClosePrices);
    }
}