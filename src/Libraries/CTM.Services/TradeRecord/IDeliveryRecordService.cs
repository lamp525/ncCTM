using System;
using System.Collections.Generic;
using System.Data;
using CTM.Core;
using CTM.Core.Domain.TradeRecord;

namespace CTM.Services.TradeRecord
{
    public partial interface IDeliveryRecordService : IBaseService
    {
        bool DataImportProcess(EnumLibrary.SecurityAccount securityAccount, DataTable source, RecordImportOperationEntity operationInfo, out IList<DataRow> skippedRecords);

        void BatchInsertDeliveryRecords(IList<DeliveryRecord> deliveryRecords);

        void InsertDeliveryRecords(IList<DeliveryRecord> deliveryRecords);

        void DeleteDeliveryRecords(int[] ids);

        IList<DeliveryRecord> GetDeliveryRecordsDetail(string stockCode, int accountId, bool? dealFlag, DateTime? tradeDateFrom, DateTime? tradeDateTo, string importUserCode, DateTime? importDateFrom, DateTime? importDateTo);

        IList<DeliveryRecord> GetDeliveryRecords(int[] accountIds, DateTime? tradeDateFrom, DateTime? tradeDateTo);

        IList<int> GetTradingAccountIds();

        void CopyToDailyRecord(IList<int> deliveryRecordIds, string importUserCode, int accountId, string beneficiary, int tradeType);
    }
}