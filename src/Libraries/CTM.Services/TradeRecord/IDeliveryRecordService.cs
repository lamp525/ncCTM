using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using CTM.Core;
using CTM.Core.Domain.TradeRecord;

namespace CTM.Services.TradeRecord
{
    public partial interface IDeliveryRecordService : IBaseService
    {
        void BatchInsertDeliveryRecords(IList<DeliveryRecord> deliveryRecords);

        void InsertDeliveryRecords(IList<DeliveryRecord> deliveryRecords);

        EnumLibrary.SecurityAccount GetSelectedSecurityCompanyEnum(string securityCompanyName, string accountAttributeName);

        bool DataImportProcess(EnumLibrary.SecurityAccount securityAccount, DataTable source, RecordImportOperationEntity operationInfo);
    }
}