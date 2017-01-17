using System.Collections.Generic;
using CTM.Core.Domain.Stock;
using CTM.Core.Domain.User;

namespace CTM.Services.Stock
{
    public partial interface IStockService : IBaseService
    {
        #region StockInfo

        void UpdateStockInfo(StockInfo stock);

        void AddStockInfo(StockInfo stock);

        void DeleteStockInfoByIds(int[] ids);

        int? GetStockIdByCodeAndName(string code, string name);

        int[] GetStockIdsByStockCode(string[] stockCodes);

        StockInfo GetStockInfoById(int id);

        StockInfo GetStockInfoByCode(string stockCode);

        StockInfo GetStockInfoByName(string stockName);

        IList<StockInfo> GetStockInfosByStockCode(string[] stockCodes);

        IList<StockInfo> GetAllStocks(bool showDeleted = false);

        #endregion StockInfo

        #region StockPool

        void DeleteStockPoolInfoByStockId(int stockId);

        void AddStockPoolInfo(StockPoolInfo stockPool);

        void UpdateStockPoolInfo(StockPoolInfo stockPool);

        StockPoolInfo GetStockPoolInfoByStockId(int stockId);

        IList<UserInfo> GetBandPrincipalsByStockId(int[] stockIds);

        IList<UserInfo> GetTargetPrincipalsByStockId(int[] stockIds);

        IList<StockPoolInfo> GetAllStockPoolInfo();

        IList<StockPoolInfo> GetAllStockPoolDetail();

        void DeleteStockPool(IList<int> stockIds, string operateCode);

        #endregion StockPool

        #region StockPoolLog

        void AddStockPoolLog(StockPoolLog entity);

        IList<StockPoolLog> GetStockPoolLogs(int stockId, int logNumber);

        #endregion StockPoolLog

        #region StockTransfer

        void AddStockTransferInfo(StockTransferInfo entity);

        void AddStockTransferRecord(IList<StockTransferRecord> entities);

        IList<StockTransferInfo> GetStockTransferInfo(string holderCode = null, string receiverCode = null);

        #endregion StockTransfer
    }
}