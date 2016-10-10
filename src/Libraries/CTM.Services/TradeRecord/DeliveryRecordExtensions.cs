using System;
using System.Collections.Generic;
using System.Linq;
using CTM.Core.Domain.TKLine;
using CTM.Core.Domain.TradeRecord;
using CTM.Services.StatisticsReport;

namespace CTM.Services.TradeRecord
{
    public static class DeliveryRecordExtensions
    {
        /// <summary>
        /// 取得交易记录的每日投资统计共通信息
        /// </summary>
        /// <param name="source"></param>
        /// <param name="stockClosePrices"></param>
        /// <returns></returns>
        public static InvestStatisticsCommonEntity GetInvestStatisticsCommonInfo(this IList<DeliveryRecord> source, IList<TKLineToday> stockClosePrices)
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
        /// 成交额计算
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static decimal CalculateDealAmount(this IList<DeliveryRecord> source)
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
        /// <param name="deliveryRecord"></param>
        /// <param name="recordImportOperationInfo"></param>
        public static void SetTradeRecordCommonFields(this DeliveryRecord deliveryRecord, RecordImportOperationEntity recordImportOperationInfo)
        {
            if (deliveryRecord == null)
                throw new ArgumentNullException(nameof(deliveryRecord));

            if (recordImportOperationInfo == null)
                throw new ArgumentNullException(nameof(recordImportOperationInfo));

            deliveryRecord.DataType = (int)recordImportOperationInfo.DataType;
            deliveryRecord.AccountId = recordImportOperationInfo.AccountId;
            deliveryRecord.ImportUser = recordImportOperationInfo.ImportUserCode;
            deliveryRecord.ImportTime = recordImportOperationInfo.ImportTime;
            deliveryRecord.UpdateUser = recordImportOperationInfo.ImportUserCode;
            deliveryRecord.UpdateTime = recordImportOperationInfo.ImportTime;
        }
    }
}