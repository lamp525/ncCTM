using System;
using System.Collections.Generic;
using System.Data;
using CTM.Core;
using CTM.Core.Domain.TradeRecord;

namespace CTM.Services.TradeRecord
{
    public partial interface IDailyRecordService : IBaseService
    {
        bool DataImportProcess(EnumLibrary.SecurityAccount securityAccount, DataTable source, RecordImportOperationEntity operationInfo);

        void BatchInsertDailyRecords(IList<DailyRecord> dailyRecords);

        void InsertDailyRecords(IList<DailyRecord> dailyRecords);

        void InsertDailyRecord(DailyRecord dailyRecord);

        void UpdateDailyRecords(IList<DailyRecord> dailyRecords);

        void UpdateDailyRecord(DailyRecord dailyRecord);

        void DeleteDailyRecords(int[] ids);

        void DeleteDailyRecords(IList<DailyRecord> dailyRecords);

        DailyRecord GetDailyRecordById(int id);

        IList<DailyRecord> GetDailyRecordsByIds(int[] recordIds);

        IList<DailyRecord> GetDailyRecordsDetail(
            string stockCode = null,
            int accountId = 0,
            int dataType = 0,
            int tradeType = 0,
            string beneficiary = null,
            string operatorCode = null,
            bool? dealFlag = null,
            DateTime? tradeDateFrom = null,
            DateTime? tradeDateTo = null
            );

        IList<DailyRecord> GetDailyRecordsDetailBySearchCondition(
            bool IsAdmin = true,
            string stockCode = null,
            int accountId = 0,
            int dataType = 0,
            int tradeType = 0,
            string beneficiary = null,
            string operatorCode = null,
            bool? dealFlag = null,
            DateTime? tradeDateFrom = null,
            DateTime? tradeDateTo = null,
            string importUserCode = null,
            DateTime? importDateFrom = null,
            DateTime? importDateTo = null
            );

        IList<DailyRecord> GetDailyRecordsBySearchCondition(
            string stockCode = null,
            int accountId = 0,
            int tradeType = 0,
            string beneficiary = null,
            string operatorCode = null,
            bool? dealFlag = null,
            DateTime? tradeDateFrom = null,
            DateTime? tradeDateTo = null,
            string importUserCode = null,
            DateTime? importDateFrom = null,
            DateTime? importDateTo = null
            );

        IList<DailyRecord> GetDailyRecords(
            int[] accountIds = null,
            int tradeType = 0,
            string[] beneficiaries = null,
            DateTime? tradeDateFrom = null,
            DateTime? tradeDateTo = null
            );

        IList<int> GetTradingAccountIds();
    }
}