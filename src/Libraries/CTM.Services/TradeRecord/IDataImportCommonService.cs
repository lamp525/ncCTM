using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using CTM.Core;
using CTM.Core.Domain.Stock;

namespace CTM.Services.TradeRecord
{
    public partial interface IDataImportCommonService : IBaseService
    {
        DataTable GetImportDataFromExcel(string filePath);

        void DataFormatCheck(List<string> TemplateColumnNames, DataTable importDataTable);

        void VerifyStockInfo(StockInfo stockInfo, string stockCode, string stockName);

        EnumLibrary.SecurityAccount GetSelectedSecurityCompanyEnum(string securityCompanyName, string accountAttributeName);
    }
}