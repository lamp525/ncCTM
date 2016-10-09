using System;
using System.Collections.Generic;
using CTM.Core.Domain.MarginTrading;

namespace CTM.Services.MarginTrading
{
    public partial interface IMarginTradingService : IBaseService
    {
        void InsertMarginTradingInfo(MarginTradingInfo entity);

        void DeleteMarginTradingInfo(IList<int> ids);

        IList<MarginTradingInfo> GetUserAllMarginTradingInfo(string[] investorCodes = null, int tradeType = 0, DateTime? dateFrom = null, DateTime? dateTo = null);

        IList<MarginTradingInfo> GetUserOutMarginTradingInfo(string[] investorCodes = null, int tradeType = 0, DateTime? dateFrom = null, DateTime? dateTo = null);

        IList<MarginTradingInfo> GetUserInMarginTradingInfo(string[] investorCodes = null, int tradeType = 0, DateTime? dateFrom = null, DateTime? dateTo = null);

        IList<MarginTradingEntity> GetUserAllMarginTradingDetails(string[] investorCodes = null, int tradeType = 0, DateTime? dateFrom = null, DateTime? dateTo = null);

        IList<MarginTradingEntity> GetUserOutMarginTradingDetails(string[] investorCodes = null, int tradeType = 0, DateTime? dateFrom = null, DateTime? dateTo = null);

        IList<MarginTradingEntity> GetUserInMarginTradingDetails(string[] investorCodes = null, int tradeType = 0, DateTime? dateFrom = null, DateTime? dateTo = null);
    }
}