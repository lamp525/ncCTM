using System;
using System.Collections.Generic;
using System.Linq;
using CTM.Core;
using CTM.Core.Domain.TKLine;
using CTM.Core.Domain.TradeRecord;
using CTM.Services.StatisticsReport;

namespace CTM.Services.TradeRecord
{
    public static class DailyRecordExtensions
    {

        /// <summary>
        /// 取得交易记录的每日投资统计共通信息
        /// </summary>
        /// <param name="source"></param>
        /// <param name="stockClosePrices"></param>
        /// <returns></returns>
        public static InvestStatisticsCommonEntity GetInvestStatisticsCommonInfo(this IList<DailyRecord> source, IList<TKLineToday> stockClosePrices)
        {
            if (source?.Count == 0 || stockClosePrices?.Count == 0) return new InvestStatisticsCommonEntity();

            //成交额
            decimal dealAmount = source.ToList().CalculateDealAmount();

            //持仓市值
            decimal positionValue = 0;
            //持仓收益
            decimal positionProfit = 0;
            //累计发生金额
            decimal accumulatedActualAmount = 0;
            //累计收益额
            decimal accumulatedProfit = 0;

            accumulatedActualAmount = source.Sum(x => x.ActualAmount);

            var recordsByStock = source.GroupBy(x => x.StockCode);
            foreach (var stockGroup in recordsByStock)
            {
                ////各只股票发生金额
                //decimal actualAmountPerStock = stockGroup.Sum(x => x.ActualAmount);
                //accumulatedActualAmount += actualAmountPerStock;

                //各只股票的持股数
                decimal holdingVolume = stockGroup.Sum(x => x.DealVolume);

                decimal closePrice = 0;

                if (holdingVolume != 0 && stockClosePrices.Any())
                {
                    //System.Diagnostics.Debug.WriteLine(date.ToString() + ":   " + stockGroup.Key);
                    closePrice = (stockClosePrices.LastOrDefault(x => x.StockCode.Trim() == stockGroup.Key) ?? new TKLineToday()).Close;
                }

                //持仓市值
                positionValue += Math.Abs(holdingVolume) * closePrice;

                //持仓收益
                positionProfit += holdingVolume * closePrice;
            }
            accumulatedProfit = accumulatedActualAmount + positionProfit;

            var info = new InvestStatisticsCommonEntity()
            {
                DealAmount = dealAmount,
                PositionValue = positionValue,
                PositionProfit = positionProfit,
                AccumulatedActualAmount = accumulatedActualAmount,
                AccumulatedProfit = accumulatedProfit,
            };

            return info;
        }

        /// <summary>
        /// 取得交易记录的每日投资统计共通信息
        /// </summary>
        /// <param name="source"></param>
        /// <param name="tradeDates"></param>
        /// <param name="stockClosePrices"></param>
        /// <returns></returns>
        public static IList<InvestStatisticsCommonEntity> GetDailyInvestStatisticsCommonInfo(this IList<DailyRecord> source, IList<DateTime> tradeDates, IList<TKLineToday> stockClosePrices)
        {
            var result = new List<InvestStatisticsCommonEntity>();

            if (source == null || !source.Any() || tradeDates == null) return result;

            tradeDates = tradeDates.OrderBy(x => x).ToList();

            foreach (var date in tradeDates)
            {
                var currentRecords = source.Where(x => x.TradeDate < date.AddDays(1));
                var currentStockClosePrices = stockClosePrices.Where(x => x.TradeDate == date);

                //成交额
                decimal dealAmount = currentRecords.Where(x => x.TradeDate == date).ToList().CalculateDealAmount();

                //持仓市值
                decimal positionValue = 0;
                //持仓收益
                decimal positionProfit = 0;
                //累计发生金额
                decimal accumulatedActualAmount = 0;
                //累计收益额
                decimal accumulatedProfit = 0;

                accumulatedActualAmount = currentRecords.Sum(x => x.ActualAmount);

                var recordsByStock = currentRecords.GroupBy(x => x.StockCode);
                foreach (var stockGroup in recordsByStock)
                {
                    ////各只股票发生金额
                    //decimal actualAmountPerStock = stockGroup.Sum(x => x.ActualAmount);
                    //accumulatedActualAmount += actualAmountPerStock;

                    //各只股票的持股数
                    decimal holdingVolume = stockGroup.Sum(x => x.DealVolume);

                    decimal closePrice = 0;

                    if (holdingVolume != 0 && currentStockClosePrices.Any())
                    {
                        //System.Diagnostics.Debug.WriteLine(date.ToString() + ":   " + stockGroup.Key);
                        closePrice = (currentStockClosePrices.LastOrDefault(x => x.StockCode.Trim() == stockGroup.Key) ?? new TKLineToday()).Close;
                    }

                    //持仓市值
                    positionValue += Math.Abs(holdingVolume) * closePrice;

                    //持仓收益
                    positionProfit += holdingVolume * closePrice;
                }
                accumulatedProfit = accumulatedActualAmount + positionProfit;

                var info = new InvestStatisticsCommonEntity()
                {
                    TradeDate = date,
                    DealAmount = dealAmount,
                    PositionValue = positionValue,
                    PositionProfit = positionProfit,
                    AccumulatedActualAmount = accumulatedActualAmount,
                    AccumulatedProfit = accumulatedProfit,
                };

                result.Add(info);
            }
            return result;
        }

        /// <summary>
        /// 成交额计算
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static decimal CalculateDealAmount(this IList<DailyRecord> source)
        {
            decimal dealAmount = 0;

            if (source != null && source.Any())
            {
                var sellAmount = source.Where(x => x.DealFlag == false).Sum(x => x.DealAmount);
                var buyAmount = source.Where(x => x.DealFlag == true).Sum(x => x.DealAmount);

                dealAmount = sellAmount > buyAmount ? sellAmount : buyAmount;
            }

            return dealAmount;
        }

        /// <summary>
        /// 设置交易记录的共通字段
        /// </summary>
        /// <param name="dailyRecord"></param>
        /// <param name="recordImportOperationInfo"></param>
        public static void SetTradeRecordCommonFields(this DailyRecord dailyRecord, RecordImportOperationEntity recordImportOperationInfo)
        {
            if (dailyRecord == null)
                throw new ArgumentNullException(nameof(dailyRecord));

            if (recordImportOperationInfo == null)
                throw new ArgumentNullException(nameof(recordImportOperationInfo));

            dailyRecord.DataType = (int)recordImportOperationInfo.DataType;
            dailyRecord.AccountId = recordImportOperationInfo.AccountId;
            dailyRecord.OperatorCode = recordImportOperationInfo.OperatorCode;
            dailyRecord.ImportUser = recordImportOperationInfo.ImportUserCode;
            dailyRecord.ImportTime = recordImportOperationInfo.ImportTime;
            dailyRecord.UpdateUser = recordImportOperationInfo.ImportUserCode;
            dailyRecord.UpdateTime = recordImportOperationInfo.ImportTime;
            if (recordImportOperationInfo.TradeDate.HasValue)
                dailyRecord.TradeDate = recordImportOperationInfo.TradeDate.Value;
        }

        /// <summary>
        /// 设置交易记录的交易类型
        /// </summary>
        /// <param name="dailyRecord"></param>
        /// <param name="tradeType"></param>
        public static void SetTradeType(this DailyRecord dailyRecord, string tradeType)
        {
            if (dailyRecord == null)
                throw new ArgumentNullException(nameof(dailyRecord));

            switch (tradeType)
            {
                case "短差":
                case "日内":
                    dailyRecord.TradeType = (int)EnumLibrary.TradeType.Day;
                    break;

                case "波段":
                    dailyRecord.TradeType = (int)EnumLibrary.TradeType.Band;
                    break;

                case "目标":
                    dailyRecord.TradeType = (int)EnumLibrary.TradeType.Target;
                    break;
            }
        }

        /// <summary>
        /// 设置交易记录的实际受益人
        /// </summary>
        /// <param name="dailyRecord"></param>
        /// <param name="bandPrincipal"></param>
        /// <param name="targetPrincipal"></param>
        public static void SetBeneficiary(this DailyRecord dailyRecord, string bandPrincipal, string targetPrincipal)
        {
            if (dailyRecord == null)
                throw new ArgumentNullException(nameof(dailyRecord));

            switch (dailyRecord.TradeType)
            {
                case (int)EnumLibrary.TradeType.Day:
                    dailyRecord.Beneficiary = dailyRecord.OperatorCode;
                    break;

                case (int)EnumLibrary.TradeType.Band:
                    dailyRecord.Beneficiary = bandPrincipal;
                    break;

                case (int)EnumLibrary.TradeType.Target:
                    dailyRecord.Beneficiary = targetPrincipal;
                    break;
            }
        }
    }
}