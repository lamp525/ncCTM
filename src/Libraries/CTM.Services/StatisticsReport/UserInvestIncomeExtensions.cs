using System;
using System.Collections.Generic;
using System.Linq;
using CTM.Core;
using CTM.Core.Util;

namespace CTM.Services.StatisticsReport
{
    public static class UserInvestIncomeExtensions
    {
        /// <summary>
        /// 查询区间的平均融资融券额计算
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static decimal CalculatePeriodAverageMarginAmount(this IList<UserInvestIncomeEntity> source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            //有实际融资融券的收益信息
            var hasActualMarginAmountInvestIncomes = source.Where(x => x.ActualMarginAmount > 0);
            //日均融资融券额
            var periodAverageMarginAmount = !hasActualMarginAmountInvestIncomes.Any() ? 0 : hasActualMarginAmountInvestIncomes.Average(x => x.ActualMarginAmount);

            return periodAverageMarginAmount;
        }

        /// <summary>
        /// 查询区间的平均日收益率计算
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static decimal CalculatePeriodAverageDayIncomeRate(this IList<UserInvestIncomeEntity> source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            decimal averageDayIncomeRate = 0;

            //有实际融资融券的收益信息
            var hasActualMarginAmountInvestIncomes = source.Where(x => x.ActualMarginAmount > 0);

            if (hasActualMarginAmountInvestIncomes.Any())
            {
                //日均融资融券额
                var periodAverageMarginAmount = hasActualMarginAmountInvestIncomes.Average(x => x.ActualMarginAmount);
                averageDayIncomeRate = CommonHelper.CalculateRate(source.Sum(x => x.CurrentActualProfit), periodAverageMarginAmount * hasActualMarginAmountInvestIncomes.Count());
            }

            return averageDayIncomeRate;
        }
    }
}