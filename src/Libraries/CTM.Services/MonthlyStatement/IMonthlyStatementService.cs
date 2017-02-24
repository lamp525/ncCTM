using System.Collections.Generic;
using CTM.Core.Domain.MonthlyStatement;

namespace CTM.Services.MonthlyStatement
{
    public partial interface IMonthlyStatementService : IBaseService
    {
        MIAccountFund GetMIAccountFund(int accountId, int year, int month);

        void SaveMIAccountFund(MIAccountFund entity);

        void AddMIAccountPosition(int accountId, string accountCode, int year,int month, string stockCode, string stockName);

        void UpdateMIAccountPosition(int positionId, decimal positionVolume);

        void DeleteMIAccountPosition(int positionId);

        IList<MIAccountPosition> GetMIAccountPosition(int accountId, int year, int month);
    }
}