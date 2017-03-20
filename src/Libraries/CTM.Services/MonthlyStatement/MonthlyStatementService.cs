using System;
using System.Collections.Generic;
using System.Linq;
using CTM.Core;
using CTM.Core.Data;
using CTM.Core.Domain.MonthlyStatement;
using CTM.Services.TradeRecord;

namespace CTM.Services.MonthlyStatement
{
    public partial class MonthlyStatementService : IMonthlyStatementService
    {
        #region Fields

        private readonly IRepository<MIAccountFund> _MIFundRepo;
        private readonly IRepository<MIAccountPosition> _MIPositionRepo;

        private readonly IDeliveryRecordService _deliveryRecordService;

        #endregion Fields

        #region Constructors

        public MonthlyStatementService(
            IRepository<MIAccountFund> MIFundRepo,
            IRepository<MIAccountPosition> MIPositionRepo,
            IDeliveryRecordService deliveryRecordService)
        {
            this._MIFundRepo = MIFundRepo;
            this._MIPositionRepo = MIPositionRepo;
            this._deliveryRecordService = deliveryRecordService;
        }

        #endregion Constructors

        #region Methods

        public virtual MIAccountFund GetMIAccountFund(int accountId, int year, int month)
        {
            var query = _MIFundRepo.Table;

            var result = query.FirstOrDefault(x => x.AccountId == accountId && x.YearMonth == (year * 100 + month));

            return result;
        }

        public virtual void SaveMIAccountFund(MIAccountFund entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var fundInfo = _MIFundRepo.Table.FirstOrDefault(x => x.AccountId == entity.AccountId && x.YearMonth == entity.YearMonth);

            if (fundInfo == null)
                _MIFundRepo.Insert(entity);
            else
            {
                fundInfo.AvailableFund = entity.AvailableFund;
                fundInfo.FinancedAmount = entity.FinancedAmount;
                fundInfo.FinancingLimit = entity.FinancingLimit;
                fundInfo.PositionValue = entity.PositionValue;
                fundInfo.TotalAsset = entity.TotalAsset;

                _MIFundRepo.Update(fundInfo);
            }
        }

        public virtual void ImportPositionInfoFromDelivery(int accountId, int year, int month, bool clearExisted)
        {
            var yearMonth = year * 100 + month;

            if (clearExisted)
            {
                var existedPositions = _MIPositionRepo.Table.Where(x => x.AccountId == accountId && x.YearMonth == yearMonth);

                _MIPositionRepo.Delete(existedPositions);
            }

            var dateFrom = new DateTime(year, month, 1).AddMonths(-1);
            var dateTo = new DateTime(year, month, 1).AddDays(-1);
            var deliveryRecords = _deliveryRecordService.GetDeliveryRecordsDetail(null, accountId, null, null, dateTo, null, null, null).GroupBy(x => x.StockCode);

            foreach (var recordByStockCode in deliveryRecords)
            {
                var firstRecord = recordByStockCode.First();

                var positionVolume = recordByStockCode.Sum(x => x.DealVolume);

                if (positionVolume != 0)
                {
                    var entity = new MIAccountPosition
                    {
                        AccountCode = null,
                        AccountId = accountId,
                        PositionVolume = positionVolume,
                        StockCode = firstRecord.StockCode,
                        StockName = firstRecord.StockName,
                        YearMonth = yearMonth,
                    };

                    _MIPositionRepo.Insert(entity);
                }
            }
        }

        public virtual void AddMIAccountPosition(int accountId, string accountCode, int year, int month, string stockCode, string stockName)
        {
            var yearMonth = year * 100 + month;

            var stockPosition = _MIPositionRepo.TableNoTracking.FirstOrDefault(x => x.AccountId == accountId && x.YearMonth == yearMonth && x.StockCode == stockCode);

            if (stockPosition == null)
            {
                var dateFrom = new DateTime(year, month, 1).AddMonths(-1);
                var dateTo = new DateTime(year, month, 1).AddDays(-1);

                var lastInitPosition = _MIPositionRepo.TableNoTracking.FirstOrDefault(x => x.AccountId == accountId && x.YearMonth == (dateFrom.Year * 100 + dateFrom.Month) && x.StockCode == stockCode);

                decimal deliveryPositionVolume = 0;

                if (lastInitPosition == null)
                    deliveryPositionVolume = _deliveryRecordService.GetDeliveryRecordsDetail(stockCode, accountId, null, null, dateTo, null, null, null).Sum(x => x.DealVolume);
                else
                    deliveryPositionVolume = lastInitPosition.PositionVolume + _deliveryRecordService.GetDeliveryRecordsDetail(stockCode, accountId, null, dateFrom, dateTo, null, null, null).Sum(x => x.DealVolume);

                var positionInfo = new MIAccountPosition
                {
                    AccountCode = accountCode,
                    AccountId = accountId,
                    PositionVolume = deliveryPositionVolume,
                    StockCode = stockCode,
                    StockName = stockName,
                    YearMonth = yearMonth,
                };

                _MIPositionRepo.Insert(positionInfo);
            }
        }

        public virtual void UpdateMIAccountPosition(int positionId, decimal positionVolume)
        {
            var positionInfo = _MIPositionRepo.Table.FirstOrDefault(x => x.Id == positionId);

            if (positionInfo != null)
            {
                positionInfo.PositionVolume = positionVolume;

                _MIPositionRepo.Update(positionInfo);
            }
        }

        public virtual void DeleteMIAccountPosition(IList<int> positionIds)
        {
            if (positionIds == null)
                throw new ArgumentNullException(nameof(positionIds));

            var positionInfos = _MIPositionRepo.Table.Where(x => positionIds.Contains(x.Id));

            _MIPositionRepo.Delete(positionInfos);
        }

        public virtual IList<MIAccountPosition> GetMIAccountPosition(int accountId, int year, int month)
        {
            var query = _MIPositionRepo.Table.Where(x => x.AccountId == accountId && x.YearMonth == year * 100 + month);

            return query.ToList();
        }

        #endregion Methods
    }
}