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

        private readonly IRepository<MIAccountFund> _AMIFundRepo;
        private readonly IRepository<MIAccountPosition> _AMIPositionRepo;

        private readonly IDeliveryRecordService _deliveryRecordService;

        #endregion Fields

        #region Constructors

        public MonthlyStatementService(
            IRepository<MIAccountFund> accountMonthlyFundRepo,
            IRepository<MIAccountPosition> accountMonthlyPositionRepo,
            IDeliveryRecordService deliveryRecordService)
        {
            this._AMIFundRepo = accountMonthlyFundRepo;
            this._AMIPositionRepo = accountMonthlyPositionRepo;
            this._deliveryRecordService = deliveryRecordService;
        }

        #endregion Constructors

        #region Methods

        public virtual MIAccountFund GetMIAccountFund(int accountId, int year, int month)
        {
            var query = _AMIFundRepo.Table;

            var result = query.FirstOrDefault(x => x.AccountId == accountId && x.Year == year && x.Month == month);

            return result;
        }

        public virtual void SaveMIAccountFund(MIAccountFund entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var fundInfo = _AMIFundRepo.Table.FirstOrDefault(x => x.AccountId == entity.AccountId && x.Year == entity.Year && x.Month == entity.Month);

            if (fundInfo == null)
                _AMIFundRepo.Insert(entity);
            else
            {
                fundInfo.AvailableFund = entity.AvailableFund;
                fundInfo.FinancedAmount = entity.FinancedAmount;
                fundInfo.FinancingLimit = entity.FinancingLimit;
                fundInfo.PositionValue = entity.PositionValue;
                fundInfo.TotalAsset = entity.TotalAsset;

                _AMIFundRepo.Update(fundInfo);
            }
        }

        public virtual void AddMIAccountPosition(int accountId, string accountCode, int year, int month, string stockCode, string stockName)
        {
            var stockPosition = _AMIPositionRepo.TableNoTracking.FirstOrDefault(x => x.AccountId == accountId && x.Year == year && x.Month == month && x.StockCode == stockCode);

            if (stockPosition == null)
            {
                var dateFrom = new DateTime(year, month, 1);
                var dateTo = dateFrom.AddMonths(1).AddDays(-1);

                var deliveryRecords = _deliveryRecordService.GetDeliveryRecordsDetail(stockCode, accountId, null, dateFrom, dateTo, null, null, null);

                decimal deliveryPositionVolume = deliveryRecords.Sum(x => x.DealVolume);

                var positionInfo = new MIAccountPosition
                {
                    AccountCode = accountCode,
                    AccountId = accountId,
                    Month = month,
                    PositionVolume = deliveryPositionVolume,
                    StockCode = stockCode,
                    StockName = stockName,
                    Year = year,
                };

                _AMIPositionRepo.Insert(positionInfo);
            }
        }

        public virtual void UpdateMIAccountPosition(int positionId, decimal positionVolume)
        {
            var positionInfo = _AMIPositionRepo.Table.FirstOrDefault(x => x.Id == positionId);

            if (positionInfo != null)
            {
                positionInfo.PositionVolume = positionVolume;

                _AMIPositionRepo.Update(positionInfo);
            }
        }

        public virtual void DeleteMIAccountPosition(int positionId)
        {
            var positionInfo = _AMIPositionRepo.GetById(positionId);

            if (positionInfo != null)
                _AMIPositionRepo.Delete(positionInfo);
        }

        public virtual IList<MIAccountPosition> GetMIAccountPosition(int accountId, int year, int month)
        {
            var positionInfoCount = _AMIPositionRepo.TableNoTracking.Count(x => x.AccountId == accountId && x.Year == year && x.Month == month);

            if (positionInfoCount == 0)
            {
                var dateFrom = new DateTime(year, month, 1);
                var dateTo = dateFrom.AddMonths(1).AddDays(-1);

                var deliveryRecords = _deliveryRecordService.GetDeliveryRecordsDetail(null, accountId, null, dateFrom, dateTo, null, null, null).GroupBy(x => x.StockCode);

                foreach (var recordByStockCode in deliveryRecords)
                {
                    var firstRecord = recordByStockCode.First();

                    var entity = new MIAccountPosition
                    {
                        AccountCode = null,
                        AccountId = accountId,
                        Month = month,
                        PositionVolume = recordByStockCode.Sum(x => x.DealVolume),
                        StockCode = firstRecord.StockCode,
                        StockName = firstRecord.StockName,
                        Year = year,
                    };

                    _AMIPositionRepo.Insert(entity);
                }
            }

            var query = _AMIPositionRepo.Table.Where(x => x.AccountId == accountId && x.Year == year && x.Month == month);

            return query.ToList();
        }

        #endregion Methods
    }
}