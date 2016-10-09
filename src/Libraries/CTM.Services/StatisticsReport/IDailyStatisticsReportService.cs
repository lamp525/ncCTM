using System;
using System.Collections.Generic;
using CTM.Core.Domain.TKLine;
using CTM.Core.Domain.TradeRecord;
using CTM.Core.Domain.User;
using CTM.Services.Account;

namespace CTM.Services.StatisticsReport
{
    public partial interface IDailyStatisticsReportService : IBaseService
    {
        IList<UserInvestIncomeEntity> CalculateUserInvestIncome(UserInfo investorInfo, IList<string> statisticalInvestorCodes, IList<DailyRecord> tradeRecords, IList<DateTime> statisticalDates, IList<TKLineToday> stockClosePrices);

        IList<AccountInvestIncomeEntity> CalculateAccountInvestIncome(IList<DailyRecord> records, IList<DateTime> queryDates, IList<TKLineToday> stockClosePrices, AccountEntity accountInfo);
    }
}