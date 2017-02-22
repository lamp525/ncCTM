using System.Collections.Generic;
using CTM.Core.Domain.MonthlyProcess;

namespace CTM.Services.MonthlyProcess
{
    public partial interface IMonthEndProcessService : IBaseService
    {
        AccountMonthlyFund GetAccountMonthlyFund(int accountId, int yearMonth);

        void SaveAccountMonthlyFund(AccountMonthlyFund entity);

        void AddAccountMonthlyPosition(int accountId, string accountCode, int yearMonth, string stockCode, string stockName);

        void UpdateAccountMonthlyPosition(int positionId, decimal positionVolume);

        void DeleteAccountMonthlyPosition(int positionId);

        IList<AccountMonthlyPosition> GetAccountMonthlyPosition(int accountId, int yearMonth);
    }
}