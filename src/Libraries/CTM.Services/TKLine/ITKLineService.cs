using System;
using System.Collections.Generic;
using CTM.Core.Domain.TKLine;

namespace CTM.Services.TKLine
{
    public partial interface ITKLineService : IBaseService
    {
        IList<TKLineToday> GetStockClosePrices(DateTime queryDate, IList<string> stockFullCodes = null);

        IList<TKLineToday> GetStockClosePrices(IList<DateTime> queryDates, IList<string> stockFullCodes = null);
    }
}