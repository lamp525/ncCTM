using System;
using System.Collections.Generic;
using System.Linq;
using CTM.Core;
using CTM.Core.Domain.TKLine;
using CTM.Core.Util;
using CTM.Data;

namespace CTM.Services.TKLine
{
    public partial class TKLineService : ITKLineService
    {
        #region Fields

        private readonly IDbContext _dbContext;

        #endregion Fields

        #region Constructors

        public TKLineService(IDbContext context)
        {
            this._dbContext = context;
        }

        #endregion Constructors

        #region Methods

        public virtual IList<TKLineToday> GetStockClosePrices(IList<DateTime> queryDates, IList<string> stockFullCodes)
        {
            var result = new List<TKLineToday>();

            if (queryDates == null || !queryDates.Any()) return result;

            string stockCodeConditionString = string.Empty;
            string sql = @" SELECT [Id] , [StockCode] , [TradeDate] , [Close]  FROM  [dbo].[TKLineToday] WHERE [TradeDate] BETWEEN '{0}' AND '{1}' ";

            if (stockFullCodes != null && stockFullCodes.Any())
            {
                sql += @" AND [StockCode] IN ({2}) ";
                stockCodeConditionString = CommonHelper.ArrayListToSqlConditionString(stockFullCodes);
            }

            var commandText = stockFullCodes == null ? string.Format(sql, queryDates.Min(), queryDates.Max()) : string.Format(sql, queryDates.Min(), queryDates.Max(), stockCodeConditionString);
            var query = _dbContext.SqlQuery<TKLineToday>(commandText);

            result.AddRange(query.ToList());

            return result;
        }

        /// <summary>
        /// 取得股票交易日的收盘价
        /// </summary>
        /// <param name="queryDates"></param>
        /// <returns></returns>
        public virtual IList<TKLineToday> GetStockClosePrices(DateTime queryDate, IList<string> stockFullCodes)
        {
            var result = new List<TKLineToday>();

            string stockCodeConditionString = string.Empty;
            string sql = @" SELECT [Id] , [StockCode] , [TradeDate] , [Close]  FROM  [dbo].[TKLineToday] WHERE [TradeDate] = '{0}' ";

            if (stockFullCodes != null && stockFullCodes.Any())
            {
                sql += @" AND [StockCode] IN ({1}) ";
                stockCodeConditionString = CommonHelper.ArrayListToSqlConditionString(stockFullCodes);
            }

            var commandText = stockFullCodes == null ? string.Format(sql, queryDate.Date) : string.Format(sql, queryDate.Date, stockCodeConditionString);
            var query = _dbContext.SqlQuery<TKLineToday>(commandText);

            result = query.ToList();

            return result;
        }

        #endregion Methods
    }
}