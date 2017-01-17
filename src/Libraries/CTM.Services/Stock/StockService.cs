using System;
using System.Collections.Generic;
using System.Linq;
using CTM.Core;
using CTM.Core.Data;
using CTM.Core.Domain.Stock;
using CTM.Core.Domain.User;
using CTM.Services.Common;

namespace CTM.Services.Stock
{
    public partial class StockService : IStockService
    {
        #region Fields

        private readonly IRepository<StockInfo> _stockInfoRepository;
        private readonly IRepository<StockPoolInfo> _stockPoolInfoRepository;
        private readonly IRepository<StockPoolEntry> _stockPoolEntryRepository;
        private readonly IRepository<StockPoolLog> _stockPoolLogRepository;
        private readonly IRepository<UserInfo> _userInfoRepository;
        private readonly IRepository<StockTransferInfo> _stockTransferInfoRepository;
        private readonly IRepository<StockTransferRecord> _stockTransferRecordRepository;

        private readonly ICommonService _commonService;

        #endregion Fields

        #region Constructors

        public StockService(
            IRepository<StockInfo> stockInfoRepository,
            IRepository<StockPoolInfo> stockPoolInfoRepository,
            IRepository<StockPoolEntry> stockPoolEntryRepository,
            IRepository<StockPoolLog> stockPoolLogRepository,
            IRepository<UserInfo> userInfoRepository,
            IRepository<StockTransferInfo> stockTransferInfoRepository,
            IRepository<StockTransferRecord> stockTransferRecordRepository,
            ICommonService commonService
            )
        {
            this._stockInfoRepository = stockInfoRepository;
            this._stockPoolInfoRepository = stockPoolInfoRepository;
            this._stockPoolEntryRepository = stockPoolEntryRepository;
            this._stockPoolLogRepository = stockPoolLogRepository;
            this._userInfoRepository = userInfoRepository;
            this._stockTransferInfoRepository = stockTransferInfoRepository;
            this._stockTransferRecordRepository = stockTransferRecordRepository;

            this._commonService = commonService;
        }

        #endregion Constructors

        #region Methods

        public virtual StockInfo GetStockInfoById(int id)
        {
            return _stockInfoRepository.GetById(id);
        }

        public virtual IList<StockPoolInfo> GetAllStockPoolInfo()
        {
            var query = _stockPoolInfoRepository.Table;

            return query.ToList();
        }

        public virtual IList<StockPoolInfo> GetAllStockPoolDetail()
        {
            var query = from pool in _stockPoolInfoRepository.TableNoTracking
                        join s in _stockInfoRepository.Table
                        on pool.StockId equals s.Id into temp
                        from stock in temp.DefaultIfEmpty()
                        join t in _userInfoRepository.Table
                         on pool.TargetPrincipal equals t.Code into temp2
                        from target in temp2.DefaultIfEmpty()
                        join b in _userInfoRepository.Table
                        on pool.BandPrincipal equals b.Code into temp3
                        from band in temp3.DefaultIfEmpty()
                        select new { pool, stock, TargetName = target.Name, BandName = band.Name };

            var result = query.ToList().Select(x => new StockPoolInfo
            {
                Id = x.pool.Id,
                StockId = x.pool.StockId,
                StockInfo = x.stock,
                BandName = x.BandName,
                BandPrincipal = x.pool.BandPrincipal,
                TargetName = x.TargetName,
                TargetPrincipal = x.pool.TargetName,
                Remarks = x.pool.Remarks,
            }
               ).ToList();

            return result;
        }

        public virtual IList<StockInfo> GetAllStocks(bool showDeleted = false)
        {
            var query = _stockInfoRepository.TableNoTracking;

            if (!showDeleted)
                query = query.Where(x => !x.IsDeleted);

            var infos = from s in query
                        join p in _stockPoolInfoRepository.Table
                        on s.Id equals p.StockId into temp
                        from pool in temp.DefaultIfEmpty()
                        select new { s, pool };

            var result = infos.ToList().Select(x => new StockInfo
            {
                Id = x.s.Id,
                Code = x.s.Code,
                FullCode = x.s.FullCode,
                Name = x.s.Name,
                IsDeleted = x.s.IsDeleted,
                Remarks = x.s.Remarks,
                IsInPool = x.pool == null ? false : true,
            });

            return result.ToList();
        }

        public virtual void AddStockPoolInfo(StockPoolInfo stockPool)
        {
            if (stockPool == null)
                throw new ArgumentNullException(nameof(stockPool));

            _stockPoolInfoRepository.Insert(stockPool);

            var poolRecord = new StockPoolEntry
            {
                StockId = stockPool.StockId,
                AddFlag = true,
                BandPrincipal = stockPool.BandPrincipal,
                TargetPrincipal = stockPool.TargetPrincipal,
                FromDate = _commonService.GetCurrentServerTime(),
                ToDate = null,
                Remarks = null,
            };

            _stockPoolEntryRepository.Insert(poolRecord);
        }

        public virtual void UpdateStockInfo(StockInfo stock)
        {
            if (stock == null)
                throw new ArgumentNullException(nameof(stock));

            _stockInfoRepository.Update(stock);
        }

        public virtual void AddStockInfo(StockInfo stock)
        {
            if (stock == null)
                throw new ArgumentNullException(nameof(stock));

            _stockInfoRepository.Insert(stock);
        }

        public virtual void DeleteStockPoolInfoByStockId(int stockId)
        {
            var stockPool = _stockPoolInfoRepository.Table.Where(x => x.StockId == stockId).SingleOrDefault();

            if (stockPool == null) return;

            _stockPoolInfoRepository.Delete(stockPool);

            var preivousRecord = _stockPoolEntryRepository.Table.Where(x => x.StockId == stockPool.StockId).OrderByDescending(x => x.FromDate).FirstOrDefault();

            if (preivousRecord != null)
            {
                preivousRecord.ToDate = _commonService.GetCurrentServerTime();

                _stockPoolEntryRepository.Update(preivousRecord);
            }
        }

        public virtual StockPoolInfo GetStockPoolInfoByStockId(int stockId)
        {
            var query = _stockPoolInfoRepository.Table.Where(x => x.StockId == stockId);

            var result = query.SingleOrDefault();

            return result;
        }

        public virtual void UpdateStockPoolInfo(StockPoolInfo stockPool)
        {
            if (stockPool == null)
                throw new ArgumentNullException(nameof(stockPool));

            _stockPoolInfoRepository.Update(stockPool);

            var preivousRecord = _stockPoolEntryRepository.Table.Where(x => x.StockId == stockPool.StockId).OrderByDescending(x => x.FromDate).FirstOrDefault();

            if (preivousRecord != null)
            {
                if (stockPool.TargetPrincipal != preivousRecord.TargetPrincipal || stockPool.BandPrincipal != preivousRecord.BandPrincipal)
                {
                    var poolRecord = new StockPoolEntry
                    {
                        StockId = stockPool.StockId,
                        AddFlag = false,
                        BandPrincipal = stockPool.BandPrincipal,
                        TargetPrincipal = stockPool.TargetPrincipal,
                        FromDate = _commonService.GetCurrentServerTime(),
                        ToDate = null,
                        Remarks = null,
                    };

                    _stockPoolEntryRepository.Insert(poolRecord);

                    preivousRecord.ToDate = _commonService.GetCurrentServerTime();

                    _stockPoolEntryRepository.Update(preivousRecord);
                }
            }
        }

        public virtual void DeleteStockInfoByIds(int[] ids)
        {
            if (ids == null)
                throw new ArgumentNullException(nameof(ids));

            foreach (var id in ids)
            {
                var stockInfo = _stockInfoRepository.GetById(id);

                if (stockInfo == null) continue;

                stockInfo.IsDeleted = true;
                _stockInfoRepository.Update(stockInfo);
            }
        }

        public virtual int? GetStockIdByCodeAndName(string code, string name)
        {
            if (string.IsNullOrEmpty(code) || string.IsNullOrEmpty(name))
                return null;

            var info = _stockInfoRepository.Table.Where(x => x.Code == code && x.Name == name).SingleOrDefault();

            if (info == null)
                return null;
            else
                return info.Id;
        }

        public virtual IList<UserInfo> GetBandPrincipalsByStockId(int[] stockIds)
        {
            if (stockIds == null)
                throw new ArgumentNullException(nameof(stockIds));

            var stockIdFilter = stockIds.Distinct().ToArray();
            var bandPrincipals = _stockPoolEntryRepository.Table.Where(x => stockIdFilter.Contains(x.StockId)).Select(x => x.BandPrincipal).Distinct().ToArray();

            var users = _userInfoRepository.Table.Where(u => bandPrincipals.Contains(u.Code));

            return users.ToList();
        }

        public virtual IList<UserInfo> GetTargetPrincipalsByStockId(int[] stockIds)
        {
            if (stockIds == null)
                throw new ArgumentNullException(nameof(stockIds));

            var stockIdFilter = stockIds.Distinct().ToArray();
            var targetPrincipals = _stockPoolEntryRepository.Table.Where(x => stockIdFilter.Contains(x.StockId)).Select(x => x.TargetPrincipal).Distinct().ToArray();

            var users = _userInfoRepository.Table.Where(u => targetPrincipals.Contains(u.Code));

            return users.ToList();
        }

        public virtual int[] GetStockIdsByStockCode(string[] stockCodes)
        {
            if (stockCodes == null)
                throw new ArgumentNullException(nameof(stockCodes));

            var stockCodeFilter = stockCodes.Distinct().ToArray();
            var query = _stockInfoRepository.Table;

            var stockIds = query.Where(x => stockCodeFilter.Contains(x.Code)).Select(x => x.Id).ToArray();

            return stockIds;
        }

        public virtual IList<StockInfo> GetStockInfosByStockCode(string[] stockCodes)
        {
            if (stockCodes == null)
                throw new ArgumentNullException(nameof(stockCodes));

            var stockCodeFilter = stockCodes.Distinct().ToArray();
            var query = _stockInfoRepository.Table;

            var infos = query.Where(x => stockCodeFilter.Contains(x.Code)).ToArray();

            return infos;
        }

        public virtual StockInfo GetStockInfoByCode(string stockCode)
        {
            if (string.IsNullOrEmpty(stockCode))
                throw new ArgumentNullException(nameof(stockCode));

            var info = _stockInfoRepository.Table.Where(x => stockCode == x.Code || stockCode == x.FullCode).FirstOrDefault();

            return info;
        }

        public virtual StockInfo GetStockInfoByName(string stockName)
        {
            if (string.IsNullOrEmpty(stockName))
                throw new ArgumentNullException(nameof(stockName));

            var info = _stockInfoRepository.Table.Where(x => stockName == x.Name).FirstOrDefault();

            return info;
        }

        public virtual IList<StockPoolLog> GetStockPoolLogs(int stockId, int logNumber)
        {
            var query = _stockPoolLogRepository.Table;

            if (stockId > 0)
                query = query.Where(x => x.StockId == stockId);

            if (logNumber > 0)
                query = query.OrderByDescending(x => x.OperateTime).Take(logNumber);

            var infos = from log in query
                        join stock in _stockInfoRepository.Table
                        on log.StockId equals stock.Id into temp1
                        from stockInfo in temp1.DefaultIfEmpty()
                        join u1 in _userInfoRepository.Table
                        on log.OperatorCode equals u1.Code into temp2
                        from operatorInfo in temp2.DefaultIfEmpty()
                        join u2 in _userInfoRepository.Table
                        on log.BandPrincipal equals u2.Code into temp3
                        from bandInfo in temp3.DefaultIfEmpty()
                        join u3 in _userInfoRepository.Table
                        on log.TargetPrincipal equals u3.Code into temp4
                        from targetInfo in temp4.DefaultIfEmpty()
                        select (new { log, stockInfo, targetName = targetInfo.Name, bandName = bandInfo.Name, operatorName = operatorInfo.Name });

            var result = infos.ToList().Select(x => new StockPoolLog
            {
                Id = x.log.Id,

                BandPricipalName = x.bandName,
                BandPrincipal = x.log.BandPrincipal,
                OperatorCode = x.log.OperatorCode,
                OperatorName = x.operatorName,
                OperateTime = x.log.OperateTime,
                StockFullCode = x.stockInfo.FullCode,
                StockId = x.log.StockId,
                StockName = x.stockInfo.Name,
                TargetPricipalName = x.targetName,
                TargetPrincipal = x.log.TargetPrincipal,
                Type = x.log.Type,
            }
            ).ToList();

            return result;
        }

        public virtual void AddStockPoolLog(StockPoolLog entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _stockPoolLogRepository.Insert(entity);
        }

        public virtual void AddStockTransferInfo(StockTransferInfo entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _stockTransferInfoRepository.Insert(entity);
        }

        public virtual void AddStockTransferRecord(IList<StockTransferRecord> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            _stockTransferRecordRepository.Insert(entities);
        }

        public virtual IList<StockTransferInfo> GetStockTransferInfo(string holderCode = null, string receiverCode = null)
        {
            IList<StockTransferInfo> result = new List<StockTransferInfo>();

            var query = _stockTransferInfoRepository.Table;

            if (!string.IsNullOrEmpty(holderCode))
                query = query.Where(x => x.Holder == holderCode);

            if (!string.IsNullOrEmpty(receiverCode))
                query = query.Where(x => x.Receiver == receiverCode);

            result = query.ToList();

            return result;
        }

        public virtual void DeleteStockPool(IList<int> stockIds, string operateCode)
        {
            if (stockIds == null)
                throw new NullReferenceException(nameof(stockIds));

            foreach (var stockId in stockIds)
            {
                DeleteStockPoolInfoByStockId(stockId);

                var logModel = new StockPoolLog
                {
                    StockId = stockId,
                    Type = (int)EnumLibrary.OperateType.Delete,
                    OperatorCode = operateCode,
                    OperateTime = _commonService.GetCurrentServerTime(),
                };

                AddStockPoolLog(logModel);
            }
        }

        #endregion Methods
    }
}