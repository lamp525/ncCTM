using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using CTM.Core.Util;
using CTM.Data;

namespace CTM.Win.Util
{
    public class TKLineHelper
    {
        #region Fields

        private static readonly string _connectionString = ConfigurationManager.ConnectionStrings["FinancialCenter"].ConnectionString;

        #endregion Fields

        #region Methods

        /// <summary>
        /// 取得指定交易日的股票收盘价
        /// </summary>
        /// <param name="tradeDate"></param>
        /// <param name="stockFullCode"></param>
        /// <returns></returns>
        public static decimal GetStockClosePriceByDateAndCode(DateTime tradeDate, string stockFullCode)
        {
            decimal closePrice = 0;

            var query = string.Format(@"select [TradeDate], [StockCode], [Close] from TKLine_Today where TradeDate <='{0}' and StockCode ='{1}' order by TradeDate desc", tradeDate, stockFullCode);

            var ds = SqlHelper.ExecuteDataset(_connectionString, CommandType.Text, query);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                closePrice = decimal.Parse(ds.Tables[0].Rows[0]["Close"].ToString());

            return closePrice;
        }

        /// <summary>
        /// 取得股票交易日的收盘价
        /// </summary>
        /// <param name="queryDates"></param>
        /// <returns></returns>
        public static DataSet GetStockClosePrices(IList<DateTime> queryDates, IList<string> stockFullCodes = null)
        {
            var result = new DataSet();

            string query = string.Empty;
            string stockCodeConditionString = string.Empty;

            queryDates = queryDates.Distinct().ToList();

            if (stockFullCodes == null)
                query = @"select * from
                                        (
                                        select  [StockCode] ,[TradeDate] , [Close]  ,row_number() over(partition by StockCode order by TradeDate desc) RowNumber
                                        from TKLine_Today where [TradeDate] < '{0}'
                                        ) t
                                        where t.RowNumber =1";
            else
            {
                query = @"select * from
                                        (
                                        select  [StockCode] ,[TradeDate] , [Close]  ,row_number() over(partition by StockCode order by TradeDate desc) RowNumber
                                        from TKLine_Today where [TradeDate] < '{0}' and [StockCode] in ({1})
                                        ) t
                                        where t.RowNumber =1";

                stockCodeConditionString = CommonHelper.ArrayListToSqlConditionString(stockFullCodes);
            }

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                foreach (var date in queryDates)
                {
                    var queryDate = date.AddDays(1);
                    var commandText = stockFullCodes == null ? string.Format(query, queryDate) : string.Format(query, queryDate, stockCodeConditionString);

                    var ds = SqlHelper.ExecuteDataset(connection, CommandType.Text, commandText);

                    if (ds != null && ds.Tables.Count > 0)
                    {
                        var dt = ds.Tables[0].Copy();
                        dt.TableName = date.ToString();
                        result.Tables.Add(dt);
                    }
                }

                connection.Close();
            }

            return result;
        }

        /// <summary>
        /// 取得时间区间的所有股票收盘价
        /// </summary>
        /// <param name="queryDates"></param>
        /// <returns></returns>
        public static DataSet GetDailyStockClosePrices(IList<DateTime> queryDates)
        {
            var result = new DataSet();

            var query = @"select * from
                                        (
                                        select  [StockCode] ,[TradeDate] , [Close]  ,row_number() over(partition by StockCode order by TradeDate desc) RowNumber
                                        from TKLine_Today where [TradeDate] < '{0}'
                                        ) t
                                    where t.RowNumber =1";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                foreach (var date in queryDates)
                {
                    var queryDate = date.AddDays(1);
                    var commandText = string.Format(query, queryDate);

                    var ds = SqlHelper.ExecuteDataset(connection, CommandType.Text, commandText);

                    if (ds != null && ds.Tables.Count > 0)
                    {
                        var dt = ds.Tables[0].Copy();
                        dt.TableName = date.ToString();
                        result.Tables.Add(dt);
                    }
                }

                connection.Close();
            }
            return result;
        }

        #endregion Methods
    }
}