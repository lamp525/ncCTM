using System;
using System.Collections.Generic;
using System.Linq;
using CTM.Core;
using CTM.Core.Data;
using CTM.Core.Domain.Account;
using CTM.Core.Domain.Dictionary;
using CTM.Core.Domain.Industry;
using CTM.Core.Domain.User;
using CTM.Data;

namespace CTM.Services.Account
{
    public partial class AccountService : IAccountService
    {
        #region Fields

        private readonly IDbContext _dbContext;

        private readonly IRepository<AccountInfo> _accountInfoRepository;
        private readonly IRepository<AccountFundTransfer> _accountFundTransferRepository;
        private readonly IRepository<AccountInitialFund> _accountInitialFundRepository;
        private readonly IRepository<AccountOperator> _accountOperatorRepository;
        private readonly IRepository<DictionaryInfo> _dictionaryInfoRepository;
        private readonly IRepository<IndustryInfo> _industryInfoRepository;
        private readonly IRepository<UserInfo> _userInfoRepository;

        #endregion Fields

        #region Constructors

        public AccountService(
            IRepository<AccountInfo> accountInfoRepository,
            IRepository<AccountFundTransfer> accountFundTransferRepository,
            IRepository<AccountInitialFund> accountInitialFundRepository,
            IRepository<DictionaryInfo> dictionaryInfoRepository,
            IRepository<AccountOperator> accountOperatorRepository,
            IRepository<UserInfo> userInfoRepository,
            IRepository<IndustryInfo> industryRepository,
            IDbContext dbContext
            )
        {
            this._accountInfoRepository = accountInfoRepository;
            this._accountFundTransferRepository = accountFundTransferRepository;
            this._accountInitialFundRepository = accountInitialFundRepository;
            this._dictionaryInfoRepository = dictionaryInfoRepository;
            this._accountOperatorRepository = accountOperatorRepository;
            this._userInfoRepository = userInfoRepository;
            this._industryInfoRepository = industryRepository;

            this._dbContext = dbContext;
        }

        #endregion Constructors

        #region Methods

        public virtual void DisableAccount(int[] accountIds)
        {
            if (accountIds == null)
                throw new ArgumentNullException(nameof(accountIds));

            var query = _accountInfoRepository.Table;
            query = query.Where(x => accountIds.Contains(x.Id));

            query.ToList().ForEach(x =>
            {
                x.IsDisabled = true;
            });

            _accountInfoRepository.Update(query);
        }

        public virtual AccountInfo GetAccountInfoById(int accountId)
        {
            return _accountInfoRepository.GetById(accountId);
        }

        public virtual AccountEntity GetAccountDetailById(int accountId)
        {
            if (accountId == 0) return null;

            var result = new AccountEntity();
            var details = GetAccountDetails(accountIds: new int[] { accountId }, showDisabled: true);

            return details.SingleOrDefault();
        }
        public virtual IList<string> GetAllAccountNames(bool showDisabled = false)
        {
            var query =  _accountInfoRepository.TableNoTracking ;

            if (!showDisabled)
                query = query.Where(x => x.IsDisabled == false);

            var result = query.Select(x => x.Name).Distinct().ToList();

            return result;
        }

        public virtual IList<AccountEntity> GetAccountDetails(
            int[] accountIds = null,
            int industryId = 0,
            int securityCompanyCode = 0,
            int attributeCode = 0,
            int planCode = 0,
            int typeCode = 0,
            bool onlyNeedAccounting = false,
            bool showDisabled = false,
            bool tableNoTracking = false)
        {
            string commaSeparatedAccountIds = null;
            if (accountIds?.Length > 0)
                commaSeparatedAccountIds = @"'" + string.Join(",", accountIds) + @"'";
            else
                commaSeparatedAccountIds = @"''";

            var commanText = $@"EXEC [dbo].[sp_GetAccountDetail]
                                        @IndustyId = {industryId},
		                                @AccountIds = {commaSeparatedAccountIds},
		                                @SecurityCode = {securityCompanyCode},
		                                @AttributeCode = {attributeCode},
		                                @PlanCode = {planCode},
		                                @TypeCode = {typeCode},
		                                @OnlyNeedAccounting = {onlyNeedAccounting},
		                                @ShowDisabled = {showDisabled}";

            var query = this._dbContext.SqlQuery<AccountEntity>(commanText);

            return query.ToList();
        }

        public virtual IList<AccountInfo> GetAccountInfos(int[] accountIds = null, int industryId = 0, int securityCompanyCode = 0, int attributeCode = 0, int planCode = 0, int typeCode = 0, bool onlyNeedAccounting = false, bool showDisabled = false, bool tableNoTracking = false)
        {
            var query = tableNoTracking ? _accountInfoRepository.TableNoTracking : _accountInfoRepository.Table;

            if (accountIds != null && accountIds.Count() > 0)
                query = query.Where(x => accountIds.Contains(x.Id));
            if (industryId > 0)
                query = query.Where(x => industryId == x.IndustryId);
            if (securityCompanyCode > 0)
                query = query.Where(x => securityCompanyCode == x.SecurityCompanyCode);
            if (attributeCode > 0)
                query = query.Where(x => attributeCode == x.AttributeCode);
            if (planCode > 0)
                query = query.Where(x => planCode == x.PlanCode);
            if (typeCode > 0)
                query = query.Where(x => typeCode == x.TypeCode);
            if (onlyNeedAccounting)
                query = query.Where(x => x.NeedAccounting == true);
            if (!showDisabled)
                query = query.Where(x => x.IsDisabled == false);

            return query.ToList();
        }

        public virtual IList<UserInfo> GetAccountOperatorsByAccountId(int accountId)
        {
            var result = new List<UserInfo>();

            var query = from u in _userInfoRepository.Table
                        join ao in _accountOperatorRepository.Table
                        on u.Id equals ao.OperatorId
                        where ao.AccountId == accountId && !u.IsDeleted
                        select u;

            result = query.ToList();

            return result;
        }

        public virtual void AddAccountInfo(AccountInfo accountEntity)
        {
            if (accountEntity == null)
                throw new ArgumentNullException(nameof(accountEntity));

            _accountInfoRepository.Insert(accountEntity);
        }

        public virtual void UpdateAccountInfo(AccountInfo accountEntity)
        {
            if (accountEntity == null)
                throw new ArgumentNullException(nameof(accountEntity));

            _accountInfoRepository.Update(accountEntity);
        }

        public virtual bool IsExistedAccount(string accountName, int securityCompanyCode, int attributeCode, int accountId = 0)
        {
            var query = _accountInfoRepository.Table;

            if (accountId > 0)
                query = query.Where(x => x.Id != accountId);

            query = query.Where(x => x.Name == accountName && x.SecurityCompanyCode == securityCompanyCode && x.AttributeCode == attributeCode);

            var info = query.FirstOrDefault();

            return info == null ? false : true;
        }

        public virtual void AddAccountOperator(IList<AccountOperator> accountOperators)
        {
            if (accountOperators == null)
                throw new ArgumentNullException(nameof(accountOperators));

            _accountOperatorRepository.Insert(accountOperators);
        }

        public virtual void AddAccountOperator(AccountOperator accountOperator)
        {
            if (accountOperator == null)
                throw new ArgumentNullException(nameof(accountOperator));

            _accountOperatorRepository.Insert(accountOperator);
        }

        public virtual AccountOperator GetAccountOperatorByAccountIdAndOperatorId(int accountId, int operatorId)
        {
            var info = _accountOperatorRepository.Table.Where(x => x.AccountId == accountId && x.OperatorId == operatorId).FirstOrDefault();

            return info;
        }

        public virtual void DeleteAccountOperator(AccountOperator accountOperator)
        {
            if (accountOperator == null)
                throw new ArgumentNullException(nameof(accountOperator));

            _accountOperatorRepository.Delete(accountOperator);
        }

        public virtual IList<int> GetAccountIdByOperatorId(int operatorId)
        {
            var accountIds = (from ao in _accountOperatorRepository.Table
                              where ao.OperatorId == operatorId
                              select ao.AccountId
                        ).ToList();

            return accountIds;
        }

        public virtual void AddAccuntFundTransfer(AccountFundTransfer entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _accountFundTransferRepository.Insert(entity);
        }

        public virtual void DeleteAccountFundTransfer(IList<int> ids)
        {
            if (ids == null)
                throw new ArgumentNullException(nameof(ids));

            var query = _accountFundTransferRepository.Table.Where(x => ids.Contains(x.Id));

            _accountFundTransferRepository.Delete(query.ToArray());
        }

        public virtual IList<AccountFundTransferEntity> GetAccountFundTransferInfo(int accountId = 0, DateTime? transferDateFrom = default(DateTime?), DateTime? transferDateTo = default(DateTime?), DateTime? operateDate = default(DateTime?))
        {
            var query = _accountFundTransferRepository.Table;

            if (accountId > 0)
                query = query.Where(x => x.AccountId == accountId);

            if (transferDateFrom.HasValue)
                query = query.Where(x => x.TransferDate >= transferDateFrom.Value);

            if (transferDateTo.HasValue)
                query = query.Where(x => x.TransferDate <= transferDateTo.Value);

            if (operateDate.HasValue)
            {
                var operateDateTo = operateDate.Value.AddDays(1);
                query = query.Where(x => x.OperateTime > operateDate.Value && x.OperateTime < operateDateTo);
            }

            var info = from t in query
                       join a in _accountInfoRepository.Table
                       on t.AccountId equals a.Id
                       join u in _userInfoRepository.Table
                       on t.Operator equals u.Code
                       select new AccountFundTransferEntity
                       {
                           Id = t.Id,
                           AccountId = t.AccountId,
                           AccountCode = t.AccountCode,
                           AccountDetail = a.Name + " - " + a.SecurityCompanyName + " - " + a.AttributeName,
                           FlowFlag = t.FlowFlag,
                           FlowFlagName = t.FlowFlag ? "转入" : "转出",
                           OperateTime = t.OperateTime,
                           Operator = u.Name,
                           Remarks = t.Remarks,
                           TargetAccountCode = t.TargetAccountCode,
                           TargetAccountId = t.TargetAccountId,
                           TransferAmount = t.TransferAmount,
                           TransferDate = t.TransferDate,
                           TransferType = t.TransferType,
                       };

            return info.ToList();
        }

        public virtual KeyValuePair<int, bool> GetLatestAccountFundInitialInfo()
        {
            int latestInitialMonth = 0;
            bool canRevoke = false;

            var query = _accountInitialFundRepository.Table;

            if (query.Any())
            {
                var info = query.Where(x => x.IsInitial == false);

                if (info.Any())
                    latestInitialMonth = info.Max(x => x.Month);

                if (latestInitialMonth == 0)
                    latestInitialMonth = query.Where(x => x.IsInitial == true).Min(x => x.Month);

                canRevoke = query.Count(x => x.Month == latestInitialMonth && x.IsInitial == false) > 0 ? true : false;
            }

            return new KeyValuePair<int, bool>(latestInitialMonth, canRevoke);
        }

        public void AccountFundSettleProcess()
        {
            _dbContext.ExecuteSqlCommand(@"EXEC [dbo].[sp_AccountFundSettleProcess]");
        }

        public void AccountFundRevokeProcess()
        {
            _dbContext.ExecuteSqlCommand(@"EXEC [dbo].[sp_AccountFundRevokeProcess]");
        }

        public virtual IList<int> GetAccountIds(string accountName, int securityCode, int attributeCode)
        {
            var query = _accountInfoRepository.Table;

            query = query.Where(x => !x.IsDisabled);

            if (!string.IsNullOrEmpty(accountName))
                query = query.Where(x => x.Name == accountName);

            if (securityCode > 0)
                query = query.Where(x => x.SecurityCompanyCode == securityCode);

            if (attributeCode > 0)
                query = query.Where(x => x.AttributeCode == attributeCode);

            var result = query.Select(x => x.Id).ToList();

            return result;
        }


        #endregion Methods
    }
}