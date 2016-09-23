using System;
using System.Collections.Generic;
using System.Linq;
using CTM.Core.Data;
using CTM.Core.Domain.Account;
using CTM.Core.Domain.Department;
using CTM.Core.Domain.TradeRecord;
using CTM.Core.Domain.User;

namespace CTM.Services.TradeRecord
{
    public partial class DailyRecordService : IDailyRecordService
    {
        #region Fields

        private readonly IRepository<DailyRecord> _dailyRepository;
        private readonly IRepository<UserInfo> _userRepository;
        private readonly IRepository<AccountInfo> _accountRepository;
        private readonly IRepository<DepartmentInfo> _deptRepository;

        #endregion Fields

        #region Constructors

        public DailyRecordService(
            IRepository<DailyRecord> dailyRepository,
            IRepository<UserInfo> userRepository,
            IRepository<AccountInfo> accountRepository,
            IRepository<DepartmentInfo> deptRepository
            )
        {
            this._dailyRepository = dailyRepository;
            this._userRepository = userRepository;
            this._accountRepository = accountRepository;
            this._deptRepository = deptRepository;
        }

        #endregion Constructors

        #region Methods

        public virtual IList<DailyRecord> GetDailyRecords(
            int[] accountIds = null,
            int tradeType = 0,
            string[] beneficiaries = null,
            DateTime? tradeDateFrom = null,
            DateTime? tradeDateTo = null
            )
        {
            var query = _dailyRepository.TableNoTracking;

            if (accountIds != null)
                query = query.Where(x => accountIds.Contains(x.AccountId));
            if (tradeType > 0)
                query = query.Where(x => x.TradeType == tradeType);
            if (tradeDateFrom.HasValue)
                query = query.Where(x => x.TradeDate >= tradeDateFrom);
            if (tradeDateTo.HasValue)
                query = query.Where(x => x.TradeDate <= tradeDateTo);
            if (beneficiaries != null)
                query = query.Where(x => beneficiaries.Contains(x.Beneficiary));

            return query.ToList();
        }

        public virtual IList<DailyRecord> GetDailyRecordsBySearchCondition(
            string stockCode = null,
            int accountId = 0,
            int tradeType = 0,
            string beneficiary = null,
            string operatorCode = null,
            bool? dealFlag = null,
            DateTime? tradeDateFrom = null,
            DateTime? tradeDateTo = null,
            string importUserCode = null,
            DateTime? importDateFrom = null,
            DateTime? importDateTo = null
            )
        {
            var query = _dailyRepository.TableNoTracking;

            if (!string.IsNullOrEmpty(stockCode))
                query = query.Where(x => x.StockCode == stockCode);

            if (accountId > 0)
                query = query.Where(x => x.AccountId == accountId);

            if (tradeType > 0)
                query = query.Where(x => x.TradeType == tradeType);

            if (!string.IsNullOrEmpty(beneficiary))
                query = query.Where(x => x.Beneficiary == beneficiary);

            if (!string.IsNullOrEmpty(operatorCode))
                query = query.Where(x => x.OperatorCode == operatorCode);

            if (dealFlag.HasValue)
                query = query.Where(x => x.DealFlag == dealFlag.Value);

            if (tradeDateFrom.HasValue)
                query = query.Where(x => x.TradeDate >= tradeDateFrom);
            if (tradeDateTo.HasValue)
                query = query.Where(x => x.TradeDate <= tradeDateTo);

            if (!string.IsNullOrEmpty(importUserCode))
                query = query.Where(x => x.ImportUser == importUserCode);

            if (importDateFrom.HasValue)
                query = query.Where(x => x.ImportTime >= importDateFrom);
            if (importDateTo.HasValue)
            {
                var to = importDateTo.Value.AddDays(1);
                query = query.Where(x => x.ImportTime < to);
            }
            query = query.OrderByDescending(x => x.TradeDate);

            return query.ToList();
        }

        public virtual IList<DailyRecord> GetDailyRecordsDetail(
           string stockCode = null,
           int accountId = 0,
           int dataType = 0,
           int tradeType = 0,
           string beneficiary = null,
           string operatorCode = null,
           bool? dealFlag = null,
           DateTime? tradeDateFrom = null,
           DateTime? tradeDateTo = null
           )
        {
            var query = _dailyRepository.TableNoTracking;

            if (!string.IsNullOrEmpty(operatorCode) && !string.IsNullOrEmpty(beneficiary))
                query = query.Where(x => x.Beneficiary == beneficiary || x.OperatorCode == operatorCode);
            else
            {
                if (!string.IsNullOrEmpty(operatorCode))
                    query = query.Where(x => x.OperatorCode == operatorCode);
                if (!string.IsNullOrEmpty(beneficiary))
                    query = query.Where(x => x.Beneficiary == beneficiary);
            }

            if (!string.IsNullOrEmpty(stockCode))
                query = query.Where(x => x.StockCode == stockCode);

            if (accountId > 0)
                query = query.Where(x => x.AccountId == accountId);

            if (dataType > 0)
                query = query.Where(x => x.DataType == dataType);

            if (tradeType > 0)
                query = query.Where(x => x.TradeType == tradeType);

            if (dealFlag.HasValue)
                query = query.Where(x => x.DealFlag == dealFlag.Value);

            if (tradeDateFrom.HasValue)
                query = query.Where(x => x.TradeDate >= tradeDateFrom);
            if (tradeDateTo.HasValue)
                query = query.Where(x => x.TradeDate <= tradeDateTo);

            query = query.OrderByDescending(x => x.TradeDate);

            var infos = from d in query
                        join a in _accountRepository.Table
                        on d.AccountId equals a.Id into temp1
                        from account in temp1.DefaultIfEmpty()
                        join u1 in _userRepository.Table
                        on d.ImportUser equals u1.Code into temp2
                        from importUser in temp2.DefaultIfEmpty()
                        join u2 in _userRepository.Table
                        on d.OperatorCode equals u2.Code into temp3
                        from operateUser in temp3.DefaultIfEmpty()
                        join u3 in _userRepository.Table
                        on d.Beneficiary equals u3.Code into temp4
                        from beneficiaryUser in temp4.DefaultIfEmpty()
                        join u4 in _userRepository.Table
                        on d.UpdateUser equals u4.Code into temp5
                        from updateUser in temp5.DefaultIfEmpty()
                        select new { d, account, importUser, operateUser, beneficiaryUser, updateUser };

            var result = infos.ToList().Select(x => new DailyRecord
            {
                Id = x.d.Id,
                AccountId = x.d.AccountId,
                AccountName = x.account == null ? null : x.account.Name + " - " + x.account.SecurityCompanyName + " - " + x.account.AttributeName,
                ActualAmount = x.d.ActualAmount,
                AuditFlag = x.d.AuditFlag,
                AuditNo = x.d.AuditNo,
                AuditTime = x.d.AuditTime,
                Beneficiary = x.d.Beneficiary,
                BeneficiaryName = x.beneficiaryUser == null ? null : x.beneficiaryUser.Name,
                Commission = x.d.Commission,
                ContractNo = x.d.ContractNo,
                DataType = x.d.DataType,
                DealAmount = x.d.DealAmount,
                DealFlag = x.d.DealFlag,
                DealNo = x.d.DealNo,
                DealPrice = x.d.DealPrice,
                DealVolume = x.d.DealVolume,
                ImportTime = x.d.ImportTime,
                ImportUser = x.importUser == null ? null : x.importUser.Code,
                ImportUserName = x.importUser.Name,
                Incidentals = x.d.Incidentals,
                OperatorCode = x.d.OperatorCode,
                OperatorName = x.operateUser == null ? null : x.operateUser.Name,
                Remarks = x.d.Remarks,
                SplitNo = x.d.SplitNo,
                StampDuty = x.d.StampDuty,
                StockCode = x.d.StockCode,
                StockHolderCode = x.d.StockHolderCode,
                StockName = x.d.StockName,
                TradeDate = x.d.TradeDate,
                TradeTime = x.d.TradeTime,
                TradeType = x.d.TradeType,
                UpdateTime = x.d.UpdateTime,
                UpdateUser = x.d.UpdateUser,
                UpdateUserName = x.updateUser == null ? null : x.updateUser.Name,
            }
            )
            .ToList();

            return result;
        }

        public virtual IList<DailyRecord> GetDailyRecordsDetailBySearchCondition(
            bool IsAdmin = true,
            string stockCode = null,
            int accountId = 0,
            int dataType = 0,
            int tradeType = 0,
            string beneficiary = null,
            string operatorCode = null,
            bool? dealFlag = null,
            DateTime? tradeDateFrom = null,
            DateTime? tradeDateTo = null,
            string importUserCode = null,
            DateTime? importDateFrom = null,
            DateTime? importDateTo = null
            )
        {
            var query = _dailyRepository.TableNoTracking;

            if (IsAdmin)
            {
                if (!string.IsNullOrEmpty(beneficiary))
                    query = query.Where(x => x.Beneficiary == beneficiary);

                if (!string.IsNullOrEmpty(operatorCode))
                    query = query.Where(x => x.OperatorCode == operatorCode);

                if (!string.IsNullOrEmpty(importUserCode))
                    query = query.Where(x => x.ImportUser == importUserCode);
            }
            else
                query = query.Where(x => x.Beneficiary == beneficiary || x.OperatorCode == operatorCode || x.ImportUser == importUserCode);

            if (!string.IsNullOrEmpty(stockCode))
                query = query.Where(x => x.StockCode == stockCode);

            if (accountId > 0)
                query = query.Where(x => x.AccountId == accountId);

            if (dataType > 0)
                query = query.Where(x => x.DataType == dataType);

            if (tradeType > 0)
                query = query.Where(x => x.TradeType == tradeType);

            if (dealFlag.HasValue)
                query = query.Where(x => x.DealFlag == dealFlag.Value);

            if (tradeDateFrom.HasValue)
                query = query.Where(x => x.TradeDate >= tradeDateFrom);
            if (tradeDateTo.HasValue)
                query = query.Where(x => x.TradeDate <= tradeDateTo);

            if (importDateFrom.HasValue)
                query = query.Where(x => x.ImportTime >= importDateFrom);
            if (importDateTo.HasValue)
            {
                var to = importDateTo.Value.AddDays(1);
                query = query.Where(x => x.ImportTime < to);
            }

            query = query.OrderByDescending(x => x.TradeDate);

            var infos = from d in query
                        join a in _accountRepository.Table
                        on d.AccountId equals a.Id into temp1
                        from account in temp1.DefaultIfEmpty()
                        join u1 in _userRepository.Table
                        on d.ImportUser equals u1.Code into temp2
                        from importUser in temp2.DefaultIfEmpty()
                        join u2 in _userRepository.Table
                        on d.OperatorCode equals u2.Code into temp3
                        from operateUser in temp3.DefaultIfEmpty()
                        join u3 in _userRepository.Table
                        on d.Beneficiary equals u3.Code into temp4
                        from beneficiaryUser in temp4.DefaultIfEmpty()
                        join u4 in _userRepository.Table
                        on d.UpdateUser equals u4.Code into temp5
                        from updateUser in temp5.DefaultIfEmpty()
                        select new { d, account, importUser, operateUser, beneficiaryUser, updateUser };

            var result = infos.ToList().Select(x => new DailyRecord
            {
                Id = x.d.Id,
                AccountId = x.d.AccountId,
                AccountName = x.account == null ? null : x.account.Name + " - " + x.account.SecurityCompanyName + " - " + x.account.AttributeName,
                ActualAmount = x.d.ActualAmount,
                AuditFlag = x.d.AuditFlag,
                AuditNo = x.d.AuditNo,
                AuditTime = x.d.AuditTime,
                Beneficiary = x.d.Beneficiary,
                BeneficiaryName = x.beneficiaryUser == null ? null : x.beneficiaryUser.Name,
                Commission = x.d.Commission,
                ContractNo = x.d.ContractNo,
                DataType = x.d.DataType,
                DealAmount = x.d.DealAmount,
                DealFlag = x.d.DealFlag,
                DealNo = x.d.DealNo,
                DealPrice = x.d.DealPrice,
                DealVolume = x.d.DealVolume,
                ImportTime = x.d.ImportTime,
                ImportUser = x.importUser == null ? null : x.importUser.Code,
                ImportUserName = x.importUser.Name,
                Incidentals = x.d.Incidentals,
                OperatorCode = x.d.OperatorCode,
                OperatorName = x.operateUser == null ? null : x.operateUser.Name,
                Remarks = x.d.Remarks,
                SplitNo = x.d.SplitNo,
                StampDuty = x.d.StampDuty,
                StockCode = x.d.StockCode,
                StockHolderCode = x.d.StockHolderCode,
                StockName = x.d.StockName,
                TradeDate = x.d.TradeDate,
                TradeTime = x.d.TradeTime,
                TradeType = x.d.TradeType,
                UpdateTime = x.d.UpdateTime,
                UpdateUser = x.d.UpdateUser,
                UpdateUserName = x.updateUser == null ? null : x.updateUser.Name,
            }
            )
            .ToList();

            return result;
        }

        public void BatchInsertDailyRecords(IList<DailyRecord> dailyRecords)
        {
            if (dailyRecords == null)
                throw new ArgumentNullException(nameof(dailyRecords));

            _dailyRepository.BatchInsert(dailyRecords, 1000);
        }

        public virtual void InsertDailyRecords(IList<DailyRecord> dailyRecords)
        {
            if (dailyRecords == null)
                throw new ArgumentNullException(nameof(dailyRecords));

            _dailyRepository.Insert(dailyRecords);
        }

        public virtual DailyRecord GetDailyRecordById(int id)
        {
            return _dailyRepository.GetById(id);
        }

        public virtual IList<DailyRecord> GetDailyRecordsByIds(int[] recordIds)
        {
            if (recordIds == null)
                throw new ArgumentNullException(nameof(recordIds));

            var query = _dailyRepository.Table;
            query = query.Where(x => recordIds.Contains(x.Id));

            return query.ToList();
        }

        public virtual void UpdateDailyRecords(IList<DailyRecord> dailyRecords)
        {
            if (dailyRecords == null)
                throw new ArgumentNullException(nameof(dailyRecords));

            _dailyRepository.Update(dailyRecords);
        }

        public void DeleteDailyRecords(int[] recordIds)
        {
            if (recordIds == null)
                throw new ArgumentNullException(nameof(recordIds));

            var query = _dailyRepository.Table;
            query = query.Where(x => recordIds.Contains(x.Id));

            _dailyRepository.Delete(query.ToList());
        }

        public void DeleteDailyRecords(IList<DailyRecord> dailyRecords)
        {
            if (dailyRecords == null)
                throw new ArgumentNullException(nameof(dailyRecords));

            _dailyRepository.Delete(dailyRecords);
        }

        public void InsertDailyRecord(DailyRecord dailyRecord)
        {
            if (dailyRecord == null)
                throw new ArgumentNullException(nameof(dailyRecord));

            _dailyRepository.Insert(dailyRecord);
        }

        public void UpdateDailyRecord(DailyRecord dailyRecord)
        {
            if (dailyRecord == null)
                throw new ArgumentNullException(nameof(dailyRecord));

            _dailyRepository.Update(dailyRecord);
        }

        #endregion Methods
    }
}