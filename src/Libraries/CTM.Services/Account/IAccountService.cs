using System;
using System.Collections.Generic;
using CTM.Core.Domain.Account;
using CTM.Core.Domain.User;

namespace CTM.Services.Account
{
    public partial interface IAccountService : IBaseService
    {
        IList<AccountEntity> GetAccountDetails(int[] accountIds = null, int industryId = 0, int securityCompanyCode = 0, int attributeCode = 0, int planCode = 0, int typeCode = 0, bool onlyNeedAccounting = false, bool showDisabled = false, bool tableNoTracking = false);

        IList<AccountInfo> GetAccountInfos(int[] accountIds = null, int industryId = 0, int securityCompanyCode = 0, int attributeCode = 0, int planCode = 0, int typeCode = 0, bool onlyNeedAccounting = false, bool showDisabled = false, bool tableNoTracking = false);

        AccountInfo GetAccountInfoById(int accountId);

        AccountEntity GetAccountDetailById(int accountId);

        IList<UserInfo> GetAccountOperatorsByAccountId(int accountId);

        void DisableAccount(int[] accountIds);

        bool IsExistedAccount(string accountName, int securityCompanyCode, int attributeCode, int accountId = 0);

        void AddAccountInfo(AccountInfo accountEntity);

        void UpdateAccountInfo(AccountInfo accountEntity);

        void AddAccountOperator(IList<AccountOperator> accountOperators);

        void AddAccountOperator(AccountOperator accountOperator);

        void DeleteAccountOperator(AccountOperator accountOperator);

        void AddAccuntFundTransfer(AccountFundTransfer entity);

        void DeleteAccountFundTransfer(IList<int> ids);

        IList<AccountFundTransferEntity> GetAccountFundTransferInfo(int accountId = 0, DateTime? transferDateFrom = null, DateTime? transferDateTo = null, DateTime? operateDate = null);

        AccountOperator GetAccountOperatorByAccountIdAndOperatorId(int accountId, int operatorId);

        IList<int> GetAccountIdByOperatorId(int operatorId);
    }
}