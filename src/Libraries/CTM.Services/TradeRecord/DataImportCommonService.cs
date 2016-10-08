using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using CTM.Core;
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
                    row[colTime] = CommonHelper.FormatNumberString(row[colTime].ToString().Trim());
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
                    throw new Exception($"交易数据Excel文件中列【{column.ColumnName}】的名称不正确。");
            }

            foreach (var name in TemplateColumnNames)
            {
                if (!DataColumnNames.Contains(name))
                    throw new Exception($"交易数据Excel文件中缺少列【{name}】。");
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

        /// <summary>
        /// 判断选中的证券公司和账户属性是否支持数据导入处理
        /// </summary>
        /// <param name="securityCompanyName"></param>
        /// <param name="accountAttributeName"></param>
        /// <returns></returns>
        public virtual EnumLibrary.SecurityAccount GetSelectedSecurityCompanyEnum(string securityCompanyName, string accountAttributeName)
        {
            var securityAccount = EnumLibrary.SecurityAccount.Unknown;

            if (securityCompanyName == "中银国际" && accountAttributeName == "信用")
            {
                securityAccount = EnumLibrary.SecurityAccount.BOCI_C;
            }

            if (securityCompanyName == "中银国际" && accountAttributeName == "普通")
            {
                securityAccount = EnumLibrary.SecurityAccount.BOCI_N;
            }

            if (securityCompanyName == "浙商证券" && accountAttributeName == "信用")
            {
                //securityAccount = EnumLibrary.SecurityAccount.ZheShang_C;
            }

            if (securityCompanyName == "浙商证券" && accountAttributeName == "普通")
            {
                securityAccount = EnumLibrary.SecurityAccount.ZheShang_N;
            }

            if (securityCompanyName == "中信证券" && accountAttributeName == "信用")
            {
                securityAccount = EnumLibrary.SecurityAccount.CITIC_C;
            }

            if (securityCompanyName == "中信证券" && accountAttributeName == "普通")
            {
                securityAccount = EnumLibrary.SecurityAccount.CITIC_N;
            }

            if (securityCompanyName == "方正证券" && accountAttributeName == "普通")
            {
                securityAccount = EnumLibrary.SecurityAccount.Founder_N;
            }

            if (securityCompanyName == "银河证券" && accountAttributeName == "普通")
            {
                securityAccount = EnumLibrary.SecurityAccount.Galaxy_N;
            }

            if (securityCompanyName == "国金证券" && accountAttributeName == "普通")
            {
                securityAccount = EnumLibrary.SecurityAccount.SinoLink_N;
            }

            if (securityCompanyName == "国泰君安" && accountAttributeName == "普通")
            {
                securityAccount = EnumLibrary.SecurityAccount.GuoTai_N;
            }

            if (securityCompanyName == "国泰君安" && accountAttributeName == "信用")
            {
                securityAccount = EnumLibrary.SecurityAccount.GuoTai_C;
            }

            if (securityCompanyName == "华泰证券" && accountAttributeName == "普通")
            {
                securityAccount = EnumLibrary.SecurityAccount.HuaTai_N;
            }

            if (securityCompanyName == "华泰证券" && accountAttributeName == "信用")
            {
                securityAccount = EnumLibrary.SecurityAccount.HuaTai_C;
            }

            if (securityCompanyName == "安信证券" && accountAttributeName == "普通")
            {
                // return true;
            }
            if (securityCompanyName == "海通证券" && accountAttributeName == "普通")
            {
                // return true;
            }

            if (securityCompanyName == "申万宏源" && accountAttributeName == "普通")
            {
                securityAccount = EnumLibrary.SecurityAccount.ShenWan_N;
            }

            if (securityCompanyName == "财通证券" && accountAttributeName == "信用")
            {
                securityAccount = EnumLibrary.SecurityAccount.CaiTong_C;
            }

            if (securityCompanyName == "财通证券" && accountAttributeName == "普通")
            {
                securityAccount = EnumLibrary.SecurityAccount.CaiTong_N;
            }

            if (securityCompanyName == "招商证券" && accountAttributeName == "普通")
            {
                securityAccount = EnumLibrary.SecurityAccount.ZhaoShang_N;
            }

            return securityAccount;
        }

        #endregion Methods
    }
}