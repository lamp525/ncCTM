using System;
using System.Collections.Generic;
using System.Linq;
using CTM.Core.Domain.TradeRecord;

namespace CTM.Services.TradeRecord
{
    public static class DeliveryRecordExtensions
    {
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