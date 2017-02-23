using System;
using System.Collections.Generic;
using System.Linq;
using CTM.Core;
using CTM.Core.Data;
using CTM.Core.Domain.MonthlyProcess;
using CTM.Services.TradeRecord;

namespace CTM.Services.MonthlyProcess
{
    public partial class MonthEndProcessService : IMonthEndProcessService
    {
        #region Fields

        private readonly IRepository<AccountMonthlyFund> _accountMonthlyFundRepo;
        private readonly IRepository<AccountMonthlyPosition> _accountMonthlyPositionRepo;

        private readonly IDeliveryRecordService _deliveryRecordService;

        #endregion Fields

        #region Constructors

        public MonthEndProcessService(
            IRepository<AccountMonthlyFund> accountMonthlyFundRepo,
            IRepository<AccountMonthlyPosition> accountMonthlyPositionRepo,
            IDeliveryRecordService deliveryRecordService)
        {
            this._accountMonthlyFundRepo = accountMonthlyFundRepo;
            this._accountMonthlyPositionRepo = accountMonthlyPositionRepo;
            this._deliveryRecordService = deliveryRecordService;
        }

        #endregion Constructors

        #region Methods

        public virtual AccountMonthlyFund GetAccountMonthlyFund(int accountId, int yearMonth)
        {
            var query = _accountMonthlyFundRepo.Table;

            var result = query.FirstOrDefault(x => x.AccountId == accountId && x.YearMonth == yearMonth);

            return result;
        }

        public virtual void SaveAccountMonthlyFund(AccountMonthlyFund entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var fundInfo = _accountMonthlyFundRepo.Table.FirstOrDefault(x => x.AccountId == entity.AccountId && x.YearMonth == entity.YearMonth);

            if (fundInfo == null)
                _accountMonthlyFundRepo.Insert(entity);
            else
            {
                fundInfo.AvailableFund = entity.AvailableFund;
                fundInfo.FinancedAmount = entity.FinancedAmount;
                fundInfo.FinancingLimit = entity.FinancingLimit;
                fundInfo.PositionValue = entity.PositionValue;
                fundInfo.TotalAsset = entity.TotalAsset;

                _accountMonthlyFundRepo.Update(fundInfo);
            }
        }

        public virtual void AddAccountMonthlyPosition(int accountId, string accountCode, int yearMonth, string stockCode, string stockName)
        {
            var stockPosition = _accountMonthlyPositionRepo.TableNoTracking.FirstOrDefault(x => x.AccountId == accountId && x.YearMonth == yearMonth && x.StockCode == stockCode);

            if (stockPosition == null)
            {
                var dateFrom = new DateTime(yearMonth / 100, Convert.ToInt32(yearMonth.ToString().Substring(4, 2)), 1);
                var dateTo = dateFrom.AddMonths(1).AddDays(-1);

                var deliveryRecords = _deliveryRecordService.GetDeliveryRecordsDetail(stockCode, accountId, null, dateFrom, dateTo, null, null, null);

                decimal deliveryPositionVolume = deliveryRecords.Sum(x => x.DealVolume);

                var positionInfo = new AccountMonthlyPosition
                {
                    AccountCode = accountCode,
                    AccountId = accountId,
                    PositionVolume = deliveryPositionVolume,
                    StockCode = stockCode,
                    StockName = stockName,
                    YearMonth = yearMonth,
                };

                _accountMonthlyPositionRepo.Insert(positionInfo);
            }
        }

        public virtual void UpdateAccountMonthlyPosition(int positionId, decimal positionVolume)
        {
            var positionInfo = _accountMonthlyPositionRepo.Table.FirstOrDefault(x => x.Id == positionId);

            if (positionInfo != null)
            {
                positionInfo.PositionVolume = positionVolume;

                _accountMonthlyPositionRepo.Update(positionInfo);
            }
        }

        public virtual void DeleteAccountMonthlyPosition(int positionId)
        {
            var positionInfo = _accountMonthlyPositionRepo.GetById(positionId);

            if (positionInfo != null)
                _accountMonthlyPositionRepo.Delete(positionInfo);
        }

        public virtual IList<AccountMonthlyPosition> GetAccountMonthlyPosition(int accountId, int yearMonth)
        {
            var positionInfoCount = _accountMonthlyPositionRepo.TableNoTracking.Count(x => x.AccountId == accountId && x.YearMonth == yearMonth);

            if (positionInfoCount == 0)
            {
                var dateFrom = new DateTime(yearMonth / 100, Convert.ToInt32(yearMonth.ToString().Substring(4, 2)), 1);
                var dateTo = dateFrom.AddMonths(1).AddDays(-1);

                var deliveryRecords = _deliveryRecordService.GetDeliveryRecordsDetail(null, accountId, null, dateFrom, dateTo, null, null, null).GroupBy(x => x.StockCode);

                foreach (var recordByStockCode in deliveryRecords)
                {
                    var firstRecord = recordByStockCode.First();

                    var entity = new AccountMonthlyPosition
                    {
                        AccountCode = null,
                        AccountId = accountId,
                        PositionVolume = recordByStockCode.Sum(x => x.DealVolume),
                        StockCode = firstRecord.StockCode,
                        StockName = firstRecord.StockName,
                        YearMonth = yearMonth,
                    };

                    _accountMonthlyPositionRepo.Insert(entity);
                }
            }

            var query = _accountMonthlyPositionRepo.Table.Where(x => x.AccountId == accountId && x.YearMonth == yearMonth);

            return query.ToList();
        }

        #endregion Methods
    }
}