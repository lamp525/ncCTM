using System;
using System.Collections.Generic;
using System.Linq;
using CTM.Core;
using CTM.Core.Data;
using CTM.Core.Domain.Account;
using CTM.Core.Domain.Department;
using CTM.Core.Domain.Dictionary;
using CTM.Core.Domain.MarginTrading;
using CTM.Core.Domain.User;

namespace CTM.Services.MarginTrading
{
    public partial class MarginTradingService : IMarginTradingService
    {
        #region Fields

        private readonly IRepository<DictionaryInfo> _dictionaryInfoRepository;
        private readonly IRepository<AccountInfo> _accountInfoRepository;
        private readonly IRepository<UserInfo> _userInfoRepository;
        private readonly IRepository<DepartmentInfo> _departmentInfoRepository;
        private readonly IRepository<MarginTradingInfo> _marginInfoRepository;

        #endregion Fields

        #region Constructors

        public MarginTradingService(
            IRepository<DictionaryInfo> dictionaryInfoRepository,
            IRepository<AccountInfo> accountInfoRepository,
            IRepository<UserInfo> userInfoRepository,
            IRepository<DepartmentInfo> departmentInfoRepository,
            IRepository<MarginTradingInfo> marginInfoRepository
            )
        {
            this._dictionaryInfoRepository = dictionaryInfoRepository;
            this._accountInfoRepository = accountInfoRepository;
            this._userInfoRepository = userInfoRepository;
            this._departmentInfoRepository = departmentInfoRepository;
            this._marginInfoRepository = marginInfoRepository;
        }

        #endregion Constructors

        #region Enums

        private enum MarginQueryMode
        {
            All,
            In,
            Out,
        }

        #endregion Enums

        #region Utilities

        private string GetOperateName(bool isRepay, bool isFinancing)
        {
            var name = string.Empty;

            name = (isRepay ? "还" : "融") + (isFinancing ? "资" : "券");

            return name;
        }

        private IQueryable<MarginTradingInfo> GetMarginInfoQueryInfo(MarginQueryMode mode, string[] investorCodes, int tradeType, DateTime? dateFrom, DateTime? dateTo)
        {
            var query = _marginInfoRepository.TableNoTracking;

            if (dateFrom.HasValue)
                query = query.Where(x => x.MarginDate >= dateFrom);
            if (dateTo.HasValue)
                query = query.Where(x => x.MarginDate <= dateTo);
            if (tradeType > 0)
                query = query.Where(x => x.TradeType == tradeType);

            if (investorCodes != null)
            {
                switch (mode)
                {
                    case MarginQueryMode.All:
                        query = query.Where(x => investorCodes.Contains(x.InvestorCode) || investorCodes.Contains(x.LoanOwnerCode));
                        break;

                    case MarginQueryMode.In:
                        query = query.Where(x => investorCodes.Contains(x.InvestorCode));
                        break;

                    case MarginQueryMode.Out:
                        query = query.Where(x => investorCodes.Contains(x.LoanOwnerCode));
                        break;

                    default:
                        query = query.Where(x => investorCodes.Contains(x.InvestorCode) || investorCodes.Contains(x.LoanOwnerCode));
                        break;
                }
            }

            return query;
        }

        private IList<MarginTradingEntity> GetQueryResultDetail(IQueryable<MarginTradingInfo> query)
        {
            var infos = from s in query
                        join investorInfo in _userInfoRepository.Table
                        on s.InvestorCode equals investorInfo.Code
                        join a in _accountInfoRepository.Table
                        on s.AccountId equals a.Id
                        join attribute in _dictionaryInfoRepository.Table
                        on new { f1 = a.AttributeCode, f2 = (int)EnumLibrary.DictionaryType.AccountAttribute } equals new { f1 = attribute.Code, f2 = attribute.TypeId } into temp1
                        from attributeDic in temp1.DefaultIfEmpty()
                        join security in _dictionaryInfoRepository.Table
                        on new { f1 = a.SecurityCompanyCode, f2 = (int)EnumLibrary.DictionaryType.SecurityCompay } equals new { f1 = security.Code, f2 = security.TypeId } into temp2
                        from securityDic in temp2.DefaultIfEmpty()
                        join tt in _dictionaryInfoRepository.Table
                        on new { f1 = s.TradeType, f2 = (int)EnumLibrary.DictionaryType.TradeType } equals new { f1 = tt.Code, f2 = tt.TypeId } into temp3
                        from tradeTypeDic in temp3.DefaultIfEmpty()
                        join dept in _departmentInfoRepository.Table
                        on s.DepartmentId equals dept.Id into temp5
                        from deptInfo in temp5.DefaultIfEmpty()
                        join u in _userInfoRepository.Table
                        on s.LoanOwnerCode equals u.Code into temp6
                        from loanOwnerInfo in temp6.DefaultIfEmpty()
                        select new { MarginInfo = s, TradeTypeName = tradeTypeDic.Name, LoanOwnerName = loanOwnerInfo.Name, AccountId = a.Id, AccountInfo = a.Name + " - " + securityDic.Name + " - " + attributeDic.Name, InvestorName = investorInfo.Name, DepartmentName = deptInfo.Name };

            var result = infos.ToList().Select(x => new MarginTradingEntity
            {
                Id = x.MarginInfo.Id,
                AccountId = x.AccountId,
                AccountInfo = x.AccountInfo,
                Amount = x.MarginInfo.Amount,
                DepartmentId = x.MarginInfo.DepartmentId,
                DepartmentName = x.DepartmentName,
                InvestorCode = x.MarginInfo.InvestorCode,
                InvestorName = x.InvestorName,
                IsFinancing = x.MarginInfo.IsFinancing,
                IsRepay = x.MarginInfo.IsRepay,
                LoanOwnerCode = x.MarginInfo.LoanOwnerCode,
                LoanOwnerName = x.LoanOwnerName,
                LoanVolume = x.MarginInfo.LoanVolume,
                MarginDate = x.MarginInfo.MarginDate,
                OperateName = this.GetOperateName(x.MarginInfo.IsRepay, x.MarginInfo.IsFinancing),
                StockFullCode = x.MarginInfo.StockFullCode,
                StockName = x.MarginInfo.StockName,
                TradeType = x.MarginInfo.TradeType,
                TradeTypeName = x.TradeTypeName,
            }
            ).ToList();

            return result;
        }

        #endregion Utilities

        #region Methods

        public virtual void DeleteMarginTradingInfo(IList<int> ids)
        {
            if (ids == null)
                throw new ArgumentNullException(nameof(ids));

            var query = _marginInfoRepository.Table.Where(x => ids.Contains(x.Id));

            _marginInfoRepository.Delete(query.ToArray());
        }

        public virtual void InsertMarginTradingInfo(MarginTradingInfo marginTradingEntity)
        {
            if (marginTradingEntity == null)
                throw new ArgumentNullException(nameof(marginTradingEntity));

            _marginInfoRepository.Insert(marginTradingEntity);
        }

        public virtual IList<MarginTradingInfo> GetUserAllMarginTradingInfo(string[] investorCodes = null, int tradeType = 0, DateTime? dateFrom = null, DateTime? dateTo = null)
        {
            var query = this.GetMarginInfoQueryInfo(MarginQueryMode.All, investorCodes, tradeType, dateFrom, dateTo);

            return query.ToList();
        }

        public virtual IList<MarginTradingInfo> GetUserOutMarginTradingInfo(string[] investorCodes = null, int tradeType = 0, DateTime? dateFrom = null, DateTime? dateTo = null)
        {
            var query = this.GetMarginInfoQueryInfo(MarginQueryMode.Out, investorCodes, tradeType, dateFrom, dateTo);

            return query.ToList();
        }

        public virtual IList<MarginTradingInfo> GetUserInMarginTradingInfo(string[] investorCodes = null, int tradeType = 0, DateTime? dateFrom = null, DateTime? dateTo = null)
        {
            var query = this.GetMarginInfoQueryInfo(MarginQueryMode.In, investorCodes, tradeType, dateFrom, dateTo);

            return query.ToList();
        }

        public virtual IList<MarginTradingEntity> GetUserAllMarginTradingDetails(string[] investorCodes = null, int tradeType = 0, DateTime? dateFrom = null, DateTime? dateTo = null)
        {
            var query = this.GetMarginInfoQueryInfo(MarginQueryMode.All, investorCodes, tradeType, dateFrom, dateTo);
            var result = GetQueryResultDetail(query);

            return result;
        }

        public virtual IList<MarginTradingEntity> GetUserOutMarginTradingDetails(string[] investorCodes = null, int tradeType = 0, DateTime? dateFrom = null, DateTime? dateTo = null)
        {
            var query = this.GetMarginInfoQueryInfo(MarginQueryMode.Out, investorCodes, tradeType, dateFrom, dateTo);
            var result = GetQueryResultDetail(query);

            return result;
        }

        public virtual IList<MarginTradingEntity> GetUserInMarginTradingDetails(string[] investorCodes = null, int tradeType = 0, DateTime? dateFrom = null, DateTime? dateTo = null)
        {
            var query = this.GetMarginInfoQueryInfo(MarginQueryMode.In, investorCodes, tradeType, dateFrom, dateTo);
            var result = GetQueryResultDetail(query);

            return result;
        }

        #endregion Methods
    }
}