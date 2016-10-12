using System;
using System.Collections.Generic;

namespace CTM.Services.StatisticsReport
{
    public partial interface IDataVerifyService : IBaseService
    {
        IList<DataVerifyEntity> GetDiffBetweenDeliveryAndDailyData(int accountId, DateTime dateFrom, DateTime dateTo);
    }
}