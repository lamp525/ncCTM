using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using CTM.Core.Domain.Stock;
using CTM.Core.Util;

namespace CTM.Services.TradeRecord
{
    public partial class DataImportCommonService : IDataImportCommonService
    {
        #region Utilities

        private void FormatImportData(DataTable source)
        {
            var timeColumns = new List<DataColumn>();
            foreach (DataColumn column in source.Columns)
            {
                if (column.ColumnName.IndexOf("时间") > -1)
                    timeColumns.Add(column);
            }

            foreach (DataRow row in source.Rows)
            {
                foreach (DataColumn colTime in timeColumns)
                {
                    row[colTime] = CommonHelper.FormatNumberString(row[colTime].ToString().Trim());
                }
            }
        }

        #endregion Utilities

        #region Methods

        /// <summary>
        /// 从Excel文件中读取导入数据
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public virtual DataTable GetImportDataFromExcel(string filePath)
        {
            if (filePath == null)
                throw new ArgumentNullException(nameof(filePath));

            var data = NPOIHelper.ImportFirstSheetToDataTable(filePath);

            FormatImportData(data);

            return data;
        }

        /// <summary>
        ///导入数据格式检查
        /// </summary>
        /// <param name="TemplateColumnNames"></param>
        /// <param name="importDataTable"></param>
        /// <returns></returns>
        public virtual void DataFormatCheck(List<string> TemplateColumnNames, DataTable importDataTable)
        {
            var DataColumnNames = new List<string>();

            foreach (DataColumn column in importDataTable.Columns)
            {
                if (TemplateColumnNames.Contains(column.ColumnName))
                    DataColumnNames.Add(column.ColumnName);
                else
                {
                    throw new Exception($"交易数据Excel文件中列【{column.ColumnName}】的名称不正确。");
                }
            }

            foreach (var name in TemplateColumnNames)
            {
                if (!DataColumnNames.Contains(name))
                {
                    throw new Exception($"交易数据Excel文件中缺少列【{name}】。");
                }
            }
        }

        /// <summary>
        /// 校验股票信息
        /// </summary>
        /// <param name="stockInfo"></param>
        /// <param name="stockCode"></param>
        /// <param name="stockName"></param>
        /// <returns></returns>
        public virtual void VerifyStockInfo(StockInfo stockInfo, string stockCode, string stockName)
        {
            if (stockInfo == null)
                throw new Exception($"系统不存在【代码：{stockCode}】【名称：{stockName}】的股票信息！");
        }

        #endregion Methods
    }
}