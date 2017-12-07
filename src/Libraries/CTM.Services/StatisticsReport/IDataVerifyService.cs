using System;
using System.Collections.Generic;

namespace CTM.Services.StatisticsReport
{
    public partial interface IDataVerifyService : IBaseService
    {
        IList<DataVerifyEntity> sp_GetDeliveryAndEntrustDiffData(int displayType, IList<int> accountIds, DateTime dateFrom, DateTime dateTo);
    }
}