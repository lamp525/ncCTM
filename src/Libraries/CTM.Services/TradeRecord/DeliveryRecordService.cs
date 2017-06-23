using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using CTM.Core;
using CTM.Core.Data;
using CTM.Core.Domain.Account;
using CTM.Core.Domain.TradeRecord;
using CTM.Core.Domain.User;
using CTM.Core.Util;
using CTM.Services.Common;
using CTM.Services.Stock;

namespace CTM.Services.TradeRecord
{
    public partial class DeliveryRecordService : IDeliveryRecordService
    {
        #region Fields

        private readonly IRepository<DailyRecord> _dailyRepository;
        private readonly IRepository<DeliveryRecord> _deliveryRepository;
        private readonly IRepository<UserInfo> _userRepository;
        private readonly IRepository<AccountInfo> _accountRepository;

        private readonly IDataImportCommonService _dataImportService;
        private readonly IStockService _stockService;
        private readonly ICommonService _commonService;

        private IList<DataRow> _skippedRecords = null;

        #endregion Fields

        #region Constructors

        public DeliveryRecordService(
            IRepository<DailyRecord> dailyRepository,
            IRepository<DeliveryRecord> deliveryRepository,
            IRepository<UserInfo> userRepository,
            IRepository<AccountInfo> accountRepository,
            IDataImportCommonService DICService,
            IStockService stockService,
            ICommonService commonService)
        {
            this._dailyRepository = dailyRepository;
            this._deliveryRepository = deliveryRepository;
            this._userRepository = userRepository;
            this._accountRepository = accountRepository;
            this._dataImportService = DICService;
            this._stockService = stockService;
            this._commonService = commonService;
        }

        #endregion Constructors

        #region Utilities

        #region Delivery Data Import

        /// <summary>
        /// 从导入数据DataTable从获取交易数据
        /// </summary>
        /// <param name="importOperation"></param>
        /// <param name="importDataTable"></param>
        /// <param name="columnList"></param>
        /// <returns></returns>
        private IList<DeliveryRecord> ObtainTradeDataFromImportDataTable(RecordImportOperationEntity importOperation, DataTable importDataTable, Dictionary<string, string> columnList)
        {
            var tradeRecords = new List<DeliveryRecord>();

            DeliveryRecord record = null;

            List<DataRow> validRecords = new List<DataRow>();

            ///过滤记录
            validRecords = importDataTable.AsEnumerable().Where(x => CommonHelper.StringToDecimal(x.Field<string>(columnList[nameof(record.ActualAmount)]).Trim()) != 0).ToList();

            _skippedRecords = importDataTable.AsEnumerable().Where(x => !validRecords.Contains(x)).ToList();

            foreach (DataRow row in validRecords)
            {
                record = new DeliveryRecord();

                //买卖标志
                record.DealFlag = decimal.Parse(row[columnList[nameof(record.ActualAmount)]].ToString().Trim()) > 0 ? false : true;

                var stockCode = string.IsNullOrEmpty(columnList[nameof(record.StockCode)]) ? string.Empty : CommonHelper.StockCodeZerofill(row[columnList[nameof(record.StockCode)]].ToString().Trim());
                var stockName = row[columnList[nameof(record.StockName)]].ToString().Trim();
                var stockInfo = string.IsNullOrEmpty(stockCode) ? _stockService.GetStockInfoByName(stockName) : _stockService.GetStockInfoByCode(stockCode);

                if (stockInfo == null)
                {
                    _skippedRecords.Add(row);
                    continue;
                }

                //共通字段
                record.SetTradeRecordCommonFields(importOperation);

                //证券代码
                record.StockCode = stockInfo.FullCode;
                //证券名称
                record.StockName = stockInfo.Name;

                //交易日期
                if (!string.IsNullOrEmpty(columnList[nameof(record.TradeDate)]))
                    record.TradeDate = CommonHelper.StringToDateTime(row[columnList[nameof(record.TradeDate)]].ToString().Trim());
                //交易时间
                if (!string.IsNullOrEmpty(columnList[nameof(record.TradeTime)]))
                    record.TradeTime = row[columnList[nameof(record.TradeTime)]].ToString().Trim();

                //成交价格
                record.DealPrice = CommonHelper.StringToDecimal(row[columnList[nameof(record.DealPrice)]].ToString().Trim());
                //成交数量
                var dealVolume = CommonHelper.StringToDecimal(row[columnList[nameof(record.DealVolume)]].ToString().Trim());
                record.DealVolume = record.DealFlag ? CommonHelper.ConvertToPositive(dealVolume) : CommonHelper.ConvertToNegtive(dealVolume);
                //成交金额
                if (string.IsNullOrEmpty(columnList[nameof(record.DealAmount)]))
                    record.DealAmount = record.DealPrice * Math.Abs(record.DealVolume);
                else
                    record.DealAmount = CommonHelper.StringToDecimal(row[columnList[nameof(record.DealAmount)]].ToString().Trim());
                //发生金额
                record.ActualAmount = CommonHelper.StringToDecimal(row[columnList[nameof(record.ActualAmount)]].ToString().Trim());
                //佣金
                if (!string.IsNullOrEmpty(columnList[nameof(record.Commission)]))
                    record.Commission = CommonHelper.StringToDecimal(row[columnList[nameof(record.Commission)]].ToString().Trim());
                //印花税
                if (!string.IsNullOrEmpty(columnList[nameof(record.StampDuty)]))
                    record.StampDuty = CommonHelper.StringToDecimal(row[columnList[nameof(record.StampDuty)]].ToString().Trim());
                //杂费
                if (!string.IsNullOrEmpty(columnList[nameof(record.Incidentals)]))
                    record.Incidentals = CommonHelper.StringToDecimal(row[columnList[nameof(record.Incidentals)]].ToString().Trim());
                if (!string.IsNullOrEmpty(columnList["OtherFee1"]))
                    record.Incidentals += CommonHelper.StringToDecimal(row[columnList["OtherFee1"]].ToString().Trim());
                if (!string.IsNullOrEmpty(columnList["OtherFee2"]))
                    record.Incidentals += CommonHelper.StringToDecimal(row[columnList["OtherFee2"]].ToString().Trim());
                if (!string.IsNullOrEmpty(columnList["OtherFee3"]))
                    record.Incidentals += CommonHelper.StringToDecimal(row[columnList["OtherFee3"]].ToString().Trim());

                //成交编号
                record.DealNo = row[columnList[nameof(record.DealNo)]].ToString().Trim();
                //合同编号
                record.ContractNo = row[columnList[nameof(record.ContractNo)]].ToString().Trim();
                //股东代码\股东帐户
                if (!string.IsNullOrEmpty(columnList[nameof(record.StockHolderCode)]))
                    record.StockHolderCode = row[columnList[nameof(record.StockHolderCode)]].ToString().Trim();
                //备注
                record.Remarks = row[columnList[nameof(record.Remarks)]].ToString().Trim();

                tradeRecords.Add(record);
            }

            return tradeRecords;
        }

        #region 交割单--中银国际（信用）

        /// <summary>
        /// 交割单--中银国际（信用）
        /// </summary>
        /// <param name="importOperation"></param>
        /// <param name="importDataTable"></param>
        /// <returns></returns>
        private IList<DeliveryRecord> DeliveryImportBOCI_C(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            Dictionary<string, string> columnList = new Dictionary<string, string>();
            DeliveryRecord record = null;

            columnList.Add(nameof(record.TradeDate), "发生日期");
            columnList.Add(nameof(record.TradeTime), "成交时间");
            columnList.Add(nameof(record.StockCode), "证券代码");
            columnList.Add(nameof(record.StockName), "证券名称");
            columnList.Add(nameof(record.DealFlag), "买卖标志");
            columnList.Add(nameof(record.DealPrice), "成交价格");
            columnList.Add(nameof(record.DealVolume), "成交数量");
            columnList.Add(nameof(record.DealAmount), "成交金额");
            columnList.Add(nameof(record.ActualAmount), "发生金额");
            columnList.Add(nameof(record.Commission), "佣金");
            columnList.Add(nameof(record.StampDuty), "印花税");
            columnList.Add(nameof(record.Incidentals), "过户费");
            columnList.Add("OtherFee1", "交易征费");
            columnList.Add("OtherFee2", "交易规费");
            columnList.Add("OtherFee3", null);
            columnList.Add(nameof(record.StockHolderCode), "股东代码");
            columnList.Add(nameof(record.DealNo), "成交编号");
            columnList.Add(nameof(record.ContractNo), "成交编号");
            columnList.Add(nameof(record.Remarks), "备注");

            List<string> templateColumnNames = columnList.Values.Where(x => !string.IsNullOrEmpty(x)).ToList();
            this._dataImportService.DataFormatCheck(templateColumnNames, importDataTable);

            var tradeRecords = ObtainTradeDataFromImportDataTable(importOperation, importDataTable, columnList);

            return tradeRecords;
        }

        #endregion 交割单--中银国际（信用）

        #region 交割单--中银国际（普通）

        /// <summary>
        /// 交割单--中银国际（普通）
        /// </summary>
        /// <param name="importOperation"></param>
        /// <param name="importDataTable"></param>
        /// <returns></returns>
        private IList<DeliveryRecord> DeliveryImportBOCI_N(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            Dictionary<string, string> columnList = new Dictionary<string, string>();
            DeliveryRecord record = null;

            columnList.Add(nameof(record.TradeDate), "发生日期");
            columnList.Add(nameof(record.TradeTime), "成交时间");
            columnList.Add(nameof(record.StockCode), "证券代码");
            columnList.Add(nameof(record.StockName), "证券名称");
            columnList.Add(nameof(record.DealFlag), "买卖标志");
            columnList.Add(nameof(record.DealPrice), "成交价格");
            columnList.Add(nameof(record.DealVolume), "成交数量");
            columnList.Add(nameof(record.DealAmount), "成交金额");
            columnList.Add(nameof(record.ActualAmount), "发生金额");
            columnList.Add(nameof(record.Commission), "佣金");
            columnList.Add(nameof(record.StampDuty), "印花税");
            columnList.Add(nameof(record.Incidentals), "过户费");
            columnList.Add("OtherFee1", "交易征费");
            columnList.Add("OtherFee2", "交易规费");
            columnList.Add("OtherFee3", null);
            columnList.Add(nameof(record.StockHolderCode), "股东代码");
            columnList.Add(nameof(record.DealNo), "成交编号");
            columnList.Add(nameof(record.ContractNo), "成交编号");
            columnList.Add(nameof(record.Remarks), "备注");

            List<string> templateColumnNames = columnList.Values.Where(x => !string.IsNullOrEmpty(x)).ToList();
            this._dataImportService.DataFormatCheck(templateColumnNames, importDataTable);

            var tradeRecords = ObtainTradeDataFromImportDataTable(importOperation, importDataTable, columnList);

            return tradeRecords;
        }

        #endregion 交割单--中银国际（普通）

        #region 交割单--财通证券（信用）

        /// <summary>
        /// 交割单--财通证券（信用）
        /// </summary>
        /// <param name="importOperation"></param>
        /// <param name="importDataTable"></param>
        /// <returns></returns>
        private IList<DeliveryRecord> DeliveryImportCaiTong_C(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            Dictionary<string, string> columnList = new Dictionary<string, string>();
            DeliveryRecord record = null;

            columnList.Add(nameof(record.TradeDate), "委托日期");
            columnList.Add(nameof(record.TradeTime), null);
            columnList.Add(nameof(record.StockCode), "证券代码");
            columnList.Add(nameof(record.StockName), "证券名称");
            columnList.Add(nameof(record.DealFlag), "操作");
            columnList.Add(nameof(record.DealPrice), "成交均价");
            columnList.Add(nameof(record.DealVolume), "成交数量");
            columnList.Add(nameof(record.DealAmount), "成交金额");
            columnList.Add(nameof(record.ActualAmount), "发生金额");
            columnList.Add(nameof(record.Commission), "佣金");
            columnList.Add(nameof(record.StampDuty), "印花税");
            columnList.Add(nameof(record.Incidentals), "过户费");
            columnList.Add("OtherFee1", "其他杂费");
            columnList.Add("OtherFee2", null);
            columnList.Add("OtherFee3", null);
            columnList.Add(nameof(record.StockHolderCode), "股东帐户");
            columnList.Add(nameof(record.DealNo), "合同编号");
            columnList.Add(nameof(record.ContractNo), "合同编号");
            columnList.Add(nameof(record.Remarks), "操作");

            List<string> templateColumnNames = columnList.Values.Where(x => !string.IsNullOrEmpty(x)).ToList();
            this._dataImportService.DataFormatCheck(templateColumnNames, importDataTable);

            var tradeRecords = ObtainTradeDataFromImportDataTable(importOperation, importDataTable, columnList);

            return tradeRecords;
        }

        #endregion 交割单--财通证券（信用）

        #region 交割单--财通证券（普通）

        /// <summary>
        /// 交割单--财通证券（普通）
        /// </summary>
        /// <param name="importOperation"></param>
        /// <param name="importDataTable"></param>
        /// <returns></returns>
        private IList<DeliveryRecord> DeliveryImportCaiTong_N(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            Dictionary<string, string> columnList = new Dictionary<string, string>();
            DeliveryRecord record = null;

            columnList.Add(nameof(record.TradeDate), "成交日期");
            columnList.Add(nameof(record.TradeTime), "成交时间");
            columnList.Add(nameof(record.StockCode), "证券代码");
            columnList.Add(nameof(record.StockName), "证券名称");
            columnList.Add(nameof(record.DealFlag), "操作");
            columnList.Add(nameof(record.DealPrice), "成交均价");
            columnList.Add(nameof(record.DealVolume), "成交数量");
            columnList.Add(nameof(record.DealAmount), "成交金额");
            columnList.Add(nameof(record.ActualAmount), "发生金额");
            columnList.Add(nameof(record.Commission), "手续费");
            columnList.Add(nameof(record.StampDuty), "印花税");
            columnList.Add(nameof(record.Incidentals), "其他杂费");
            columnList.Add("OtherFee1", null);
            columnList.Add("OtherFee2", null);
            columnList.Add("OtherFee3", null);
            columnList.Add(nameof(record.StockHolderCode), "股东帐户");
            columnList.Add(nameof(record.DealNo), "成交编号");
            columnList.Add(nameof(record.ContractNo), "合同编号");
            columnList.Add(nameof(record.Remarks), "备注");

            List<string> templateColumnNames = columnList.Values.Where(x => !string.IsNullOrEmpty(x)).ToList();
            this._dataImportService.DataFormatCheck(templateColumnNames, importDataTable);

            var tradeRecords = ObtainTradeDataFromImportDataTable(importOperation, importDataTable, columnList);

            return tradeRecords;
        }

        #endregion 交割单--财通证券（普通）

        #region 交割单--中信证券（信用）

        /// <summary>
        /// 交割单--中信证券（信用）
        /// </summary>
        /// <param name="importOperation"></param>
        /// <param name="importDataTable"></param>
        /// <returns></returns>
        private IList<DeliveryRecord> DeliveryImportCITIC_C(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            Dictionary<string, string> columnList = new Dictionary<string, string>();
            DeliveryRecord record = null;

            columnList.Add(nameof(record.TradeDate), "发生日期");
            columnList.Add(nameof(record.TradeTime), "成交时间");
            columnList.Add(nameof(record.StockCode), null);
            columnList.Add(nameof(record.StockName), "证券名称");
            columnList.Add(nameof(record.DealFlag), "备注");
            columnList.Add(nameof(record.DealPrice), "成交价格");
            columnList.Add(nameof(record.DealVolume), "成交数量");
            columnList.Add(nameof(record.DealAmount), "成交金额");
            columnList.Add(nameof(record.ActualAmount), "清算金额");
            columnList.Add(nameof(record.Commission), "手续费");
            columnList.Add(nameof(record.StampDuty), "印花税");
            columnList.Add(nameof(record.Incidentals), "过户费");
            columnList.Add("OtherFee1", "交易所清算费");
            columnList.Add("OtherFee2", null);
            columnList.Add("OtherFee3", null);
            columnList.Add(nameof(record.StockHolderCode), "股东代码");
            columnList.Add(nameof(record.DealNo), "委托编号");
            columnList.Add(nameof(record.ContractNo), "委托编号");
            columnList.Add(nameof(record.Remarks), "备注");

            List<string> templateColumnNames = columnList.Values.Where(x => !string.IsNullOrEmpty(x)).ToList();
            this._dataImportService.DataFormatCheck(templateColumnNames, importDataTable);

            var tradeRecords = ObtainTradeDataFromImportDataTable(importOperation, importDataTable, columnList);

            return tradeRecords;
        }

        #endregion 交割单--中信证券（信用）

        #region 交割单--中信证券（普通）

        /// <summary>
        /// 交割单--中信证券（普通）
        /// </summary>
        /// <param name="importOperation"></param>
        /// <param name="importDataTable"></param>
        /// <returns></returns>
        private IList<DeliveryRecord> DeliveryImportCITIC_N(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            Dictionary<string, string> columnList = new Dictionary<string, string>();
            DeliveryRecord record = null;

            columnList.Add(nameof(record.TradeDate), "发生日期");
            columnList.Add(nameof(record.TradeTime), "成交时间");
            columnList.Add(nameof(record.StockCode), "证券代码");
            columnList.Add(nameof(record.StockName), "证券名称");
            columnList.Add(nameof(record.DealFlag), "业务名称");
            columnList.Add(nameof(record.DealPrice), "成交价格");
            columnList.Add(nameof(record.DealVolume), "成交数量");
            columnList.Add(nameof(record.DealAmount), "成交金额");
            columnList.Add(nameof(record.ActualAmount), "清算金额");
            columnList.Add(nameof(record.Commission), "手续费");
            columnList.Add(nameof(record.StampDuty), "印花税");
            columnList.Add(nameof(record.Incidentals), "过户费");
            columnList.Add("OtherFee1", "交易所清算费");
            columnList.Add("OtherFee2", "附加费");
            columnList.Add("OtherFee3", null);
            columnList.Add(nameof(record.StockHolderCode), "股东代码");
            columnList.Add(nameof(record.DealNo), "委托编号");
            columnList.Add(nameof(record.ContractNo), "委托编号");
            columnList.Add(nameof(record.Remarks), "业务名称");

            List<string> templateColumnNames = columnList.Values.Where(x => !string.IsNullOrEmpty(x)).ToList();
            this._dataImportService.DataFormatCheck(templateColumnNames, importDataTable);

            var tradeRecords = ObtainTradeDataFromImportDataTable(importOperation, importDataTable, columnList);

            return tradeRecords;
        }

        #endregion 交割单--中信证券（普通）

        #region 交割单--安信证券（普通）

        /// <summary>
        /// 交割单--安信证券（普通）
        /// </summary>
        /// <param name="importOperation"></param>
        /// <param name="importDataTable"></param>
        /// <returns></returns>
        private IList<DeliveryRecord> DeliveryImportESSENCE_N(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            Dictionary<string, string> columnList = new Dictionary<string, string>();
            DeliveryRecord record = null;

            columnList.Add(nameof(record.TradeDate), "发生日期");
            columnList.Add(nameof(record.TradeTime), "成交时间");
            columnList.Add(nameof(record.StockCode), "证券代码");
            columnList.Add(nameof(record.StockName), "证券名称");
            columnList.Add(nameof(record.DealFlag), "业务名称");
            columnList.Add(nameof(record.DealPrice), "成交价格");
            columnList.Add(nameof(record.DealVolume), "成交数量");
            columnList.Add(nameof(record.DealAmount), "成交金额");
            columnList.Add(nameof(record.ActualAmount), "清算金额");
            columnList.Add(nameof(record.Commission), "手续费");
            columnList.Add(nameof(record.StampDuty), "印花税");
            columnList.Add(nameof(record.Incidentals), "清算费");
            columnList.Add("OtherFee1", null);
            columnList.Add("OtherFee2", null);
            columnList.Add("OtherFee3", null);
            columnList.Add(nameof(record.StockHolderCode), "股东代码");
            columnList.Add(nameof(record.DealNo), "委托编号");
            columnList.Add(nameof(record.ContractNo), "委托编号");
            columnList.Add(nameof(record.Remarks), "业务名称");

            List<string> templateColumnNames = columnList.Values.Where(x => !string.IsNullOrEmpty(x)).ToList();
            this._dataImportService.DataFormatCheck(templateColumnNames, importDataTable);

            var tradeRecords = ObtainTradeDataFromImportDataTable(importOperation, importDataTable, columnList);

            return tradeRecords;
        }

        #endregion 交割单--安信证券（普通）

        #region 交割单--方正证券（普通）

        /// <summary>
        ///  交割单--方正证券（普通）
        /// </summary>
        /// <param name="importOperation"></param>
        /// <param name="importDataTable"></param>
        /// <returns></returns>

        private IList<DeliveryRecord> DeliveryImportFounder_N(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            Dictionary<string, string> columnList = new Dictionary<string, string>();
            DeliveryRecord record = null;

            columnList.Add(nameof(record.TradeDate), "成交日期");
            columnList.Add(nameof(record.TradeTime), "成交时间");
            columnList.Add(nameof(record.StockCode), "证券代码");
            columnList.Add(nameof(record.StockName), "证券名称");
            columnList.Add(nameof(record.DealFlag), "操作");
            columnList.Add(nameof(record.DealPrice), "成交均价");
            columnList.Add(nameof(record.DealVolume), "成交数量");
            columnList.Add(nameof(record.DealAmount), "成交金额");
            columnList.Add(nameof(record.ActualAmount), "发生金额");
            columnList.Add(nameof(record.Commission), "佣金");
            columnList.Add(nameof(record.StampDuty), "印花税");
            columnList.Add(nameof(record.Incidentals), "过户费");
            columnList.Add("OtherFee1", "手续费");
            columnList.Add("OtherFee2", "其他杂费");
            columnList.Add("OtherFee3", null);
            columnList.Add(nameof(record.StockHolderCode), "股东帐户");
            columnList.Add(nameof(record.DealNo), "成交编号");
            columnList.Add(nameof(record.ContractNo), "合同编号");
            columnList.Add(nameof(record.Remarks), "操作");

            List<string> templateColumnNames = columnList.Values.Where(x => !string.IsNullOrEmpty(x)).ToList();
            this._dataImportService.DataFormatCheck(templateColumnNames, importDataTable);

            var tradeRecords = ObtainTradeDataFromImportDataTable(importOperation, importDataTable, columnList);

            return tradeRecords;
        }

        #endregion 交割单--方正证券（普通）

        #region 交割单--银河证券（普通）

        /// <summary>
        /// 交割单--银河证券（普通）
        /// </summary>
        /// <param name="importOperation"></param>
        /// <param name="importDataTable"></param>
        /// <returns></returns>
        private IList<DeliveryRecord> DeliveryImportGalaxy_N(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            Dictionary<string, string> columnList = new Dictionary<string, string>();
            DeliveryRecord record = null;

            columnList.Add(nameof(record.TradeDate), "交收日期");
            columnList.Add(nameof(record.TradeTime), null);
            columnList.Add(nameof(record.StockCode), "证券代码");
            columnList.Add(nameof(record.StockName), "证券名称");
            columnList.Add(nameof(record.DealFlag), "操作");
            columnList.Add(nameof(record.DealPrice), "成交均价");
            columnList.Add(nameof(record.DealVolume), "成交数量");
            columnList.Add(nameof(record.DealAmount), "成交金额");
            columnList.Add(nameof(record.ActualAmount), "发生金额");
            columnList.Add(nameof(record.Commission), "净佣金");
            columnList.Add(nameof(record.StampDuty), "印花税");
            columnList.Add(nameof(record.Incidentals), "手续费");
            columnList.Add("OtherFee1", "过户费");
            columnList.Add("OtherFee2", "结算费");
            columnList.Add("OtherFee3", "其他杂费");
            columnList.Add(nameof(record.StockHolderCode), "股东帐户");
            columnList.Add(nameof(record.DealNo), "合同编号");
            columnList.Add(nameof(record.ContractNo), "合同编号");
            columnList.Add(nameof(record.Remarks), "操作");

            List<string> templateColumnNames = columnList.Values.Where(x => !string.IsNullOrEmpty(x)).ToList();
            this._dataImportService.DataFormatCheck(templateColumnNames, importDataTable);

            var tradeRecords = ObtainTradeDataFromImportDataTable(importOperation, importDataTable, columnList);

            return tradeRecords;
        }

        #endregion 交割单--银河证券（普通）

        #region 交割单--国泰证券（信用）

        /// <summary>
        /// 交割单--国泰证券（信用）
        /// </summary>
        /// <param name="importOperation"></param>
        /// <param name="importDataTable"></param>
        /// <returns></returns>
        private IList<DeliveryRecord> DeliveryImportGuoTai_C(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            Dictionary<string, string> columnList = new Dictionary<string, string>();
            DeliveryRecord record = null;

            columnList.Add(nameof(record.TradeDate), "成交日期");
            columnList.Add(nameof(record.TradeTime), null);
            columnList.Add(nameof(record.StockCode), "证券代码");
            columnList.Add(nameof(record.StockName), "证券名称");
            columnList.Add(nameof(record.DealFlag), "业务名称");
            columnList.Add(nameof(record.DealPrice), "成交价格");
            columnList.Add(nameof(record.DealVolume), "成交数量");
            columnList.Add(nameof(record.DealAmount), "成交金额");
            columnList.Add(nameof(record.ActualAmount), "清算金额");
            columnList.Add(nameof(record.Commission), "净佣金");
            columnList.Add(nameof(record.StampDuty), "印花税");
            columnList.Add(nameof(record.Incidentals), "规费");
            columnList.Add("OtherFee1", "附加费");
            columnList.Add("OtherFee2", "结算费");
            columnList.Add("OtherFee3", "过户费");
            columnList.Add(nameof(record.StockHolderCode), "股东代码");
            columnList.Add(nameof(record.DealNo), "成交编号");
            columnList.Add(nameof(record.ContractNo), "成交编号");
            columnList.Add(nameof(record.Remarks), "业务名称");

            List<string> templateColumnNames = columnList.Values.Where(x => !string.IsNullOrEmpty(x)).ToList();
            this._dataImportService.DataFormatCheck(templateColumnNames, importDataTable);

            var tradeRecords = ObtainTradeDataFromImportDataTable(importOperation, importDataTable, columnList);

            return tradeRecords;
        }

        #endregion 交割单--国泰证券（信用）

        #region 交割单--国泰证券（普通）

        /// <summary>
        /// 交割单--国泰证券（普通）
        /// </summary>
        /// <param name="importOperation"></param>
        /// <param name="importDataTable"></param>
        /// <returns></returns>
        private IList<DeliveryRecord> DeliveryImportGuoTai_N(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            Dictionary<string, string> columnList = new Dictionary<string, string>();
            DeliveryRecord record = null;

            columnList.Add(nameof(record.TradeDate), "成交日期");
            columnList.Add(nameof(record.TradeTime), null);
            columnList.Add(nameof(record.StockCode), "证券代码");
            columnList.Add(nameof(record.StockName), "证券名称");
            columnList.Add(nameof(record.DealFlag), "业务名称");
            columnList.Add(nameof(record.DealPrice), "成交价格");
            columnList.Add(nameof(record.DealVolume), "成交数量");
            columnList.Add(nameof(record.DealAmount), "成交金额");
            columnList.Add(nameof(record.ActualAmount), "清算金额");
            columnList.Add(nameof(record.Commission), "净佣金");
            columnList.Add(nameof(record.StampDuty), "印花税");
            columnList.Add(nameof(record.Incidentals), "规费");
            columnList.Add("OtherFee1", "过户费");
            columnList.Add("OtherFee2", "附加费");
            columnList.Add("OtherFee3", "结算费");
            columnList.Add(nameof(record.StockHolderCode), "股东代码");
            columnList.Add(nameof(record.DealNo), "成交编号");
            columnList.Add(nameof(record.ContractNo), "成交编号");
            columnList.Add(nameof(record.Remarks), "业务名称");

            List<string> templateColumnNames = columnList.Values.Where(x => !string.IsNullOrEmpty(x)).ToList();
            this._dataImportService.DataFormatCheck(templateColumnNames, importDataTable);

            var tradeRecords = ObtainTradeDataFromImportDataTable(importOperation, importDataTable, columnList);

            return tradeRecords;
        }

        #endregion 交割单--国泰证券（普通）

        #region 交割单--海通证券（普通）

        /// <summary>
        /// 交割单--海通证券（普通）
        /// </summary>
        /// <param name="importOperation"></param>
        /// <param name="importDataTable"></param>
        /// <returns></returns>
        private IList<DeliveryRecord> DeliveryImportHaiTong_N(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            Dictionary<string, string> columnList = new Dictionary<string, string>();
            DeliveryRecord record = null;

            columnList.Add(nameof(record.TradeDate), "成交日期");
            columnList.Add(nameof(record.TradeTime), null);
            columnList.Add(nameof(record.StockCode), "证券代码");
            columnList.Add(nameof(record.StockName), "证券名称");
            columnList.Add(nameof(record.DealFlag), "操作");
            columnList.Add(nameof(record.DealPrice), "成交均价");
            columnList.Add(nameof(record.DealVolume), "成交数量");
            columnList.Add(nameof(record.DealAmount), "成交金额");
            columnList.Add(nameof(record.ActualAmount), "发生金额");
            columnList.Add(nameof(record.Commission), "手续费");
            columnList.Add(nameof(record.StampDuty), "印花税");
            columnList.Add(nameof(record.Incidentals), "其他杂费");
            columnList.Add("OtherFee1", "过户费");
            columnList.Add("OtherFee2", null);
            columnList.Add("OtherFee3", null);
            columnList.Add(nameof(record.StockHolderCode), "股东帐户");
            columnList.Add(nameof(record.DealNo), "合同编号");
            columnList.Add(nameof(record.ContractNo), "合同编号");
            columnList.Add(nameof(record.Remarks), "操作");

            List<string> templateColumnNames = columnList.Values.Where(x => !string.IsNullOrEmpty(x)).ToList();
            this._dataImportService.DataFormatCheck(templateColumnNames, importDataTable);

            var tradeRecords = ObtainTradeDataFromImportDataTable(importOperation, importDataTable, columnList);

            return tradeRecords;
        }

        #endregion 交割单--海通证券（普通）

        #region 交割单--华泰证券（信用）

        /// <summary>
        /// 交割单--华泰证券（信用）
        /// </summary>
        /// <param name="importOperation"></param>
        /// <param name="importDataTable"></param>
        /// <returns></returns>
        private IList<DeliveryRecord> DeliveryImportHuaTai_C(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            Dictionary<string, string> columnList = new Dictionary<string, string>();
            DeliveryRecord record = null;

            columnList.Add(nameof(record.TradeDate), "成交日期");
            columnList.Add(nameof(record.TradeTime), null);
            columnList.Add(nameof(record.StockCode), null);
            columnList.Add(nameof(record.StockName), "证券名称");
            columnList.Add(nameof(record.DealFlag), "摘要");
            columnList.Add(nameof(record.DealPrice), "成交均价");
            columnList.Add(nameof(record.DealVolume), "成交数量");
            columnList.Add(nameof(record.DealAmount), "成交金额");
            columnList.Add(nameof(record.ActualAmount), "发生金额");
            columnList.Add(nameof(record.Commission), "手续费");
            columnList.Add(nameof(record.StampDuty), "印花税");
            columnList.Add(nameof(record.Incidentals), "其他杂费");
            columnList.Add("OtherFee1", null);
            columnList.Add("OtherFee2", null);
            columnList.Add("OtherFee3", null);
            columnList.Add(nameof(record.StockHolderCode), "股东帐户");
            columnList.Add(nameof(record.DealNo), "合同编号");
            columnList.Add(nameof(record.ContractNo), "合同编号");
            columnList.Add(nameof(record.Remarks), "摘要");

            List<string> templateColumnNames = columnList.Values.Where(x => !string.IsNullOrEmpty(x)).ToList();
            this._dataImportService.DataFormatCheck(templateColumnNames, importDataTable);

            var tradeRecords = ObtainTradeDataFromImportDataTable(importOperation, importDataTable, columnList);

            return tradeRecords;
        }

        #endregion 交割单--华泰证券（信用）

        #region 交割单--华泰证券（普通）

        /// <summary>
        /// 交割单--华泰证券（普通）
        /// </summary>
        /// <param name="importOperation"></param>
        /// <param name="importDataTable"></param>
        /// <returns></returns>
        private IList<DeliveryRecord> DeliveryImportHuaTai_N(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            Dictionary<string, string> columnList = new Dictionary<string, string>();
            DeliveryRecord record = null;

            columnList.Add(nameof(record.TradeDate), "成交日期");
            columnList.Add(nameof(record.TradeTime), null);
            columnList.Add(nameof(record.StockCode), "证券代码");
            columnList.Add(nameof(record.StockName), "证券名称");
            columnList.Add(nameof(record.DealFlag), "操作");
            columnList.Add(nameof(record.DealPrice), "成交均价");
            columnList.Add(nameof(record.DealVolume), "成交数量");
            columnList.Add(nameof(record.DealAmount), "成交金额");
            columnList.Add(nameof(record.ActualAmount), "发生金额");
            columnList.Add(nameof(record.Commission), "手续费");
            columnList.Add(nameof(record.StampDuty), "印花税");
            columnList.Add(nameof(record.Incidentals), "其他杂费");
            columnList.Add("OtherFee1", null);
            columnList.Add("OtherFee2", null);
            columnList.Add("OtherFee3", null);
            columnList.Add(nameof(record.StockHolderCode), "股东帐户");
            columnList.Add(nameof(record.DealNo), "合同编号");
            columnList.Add(nameof(record.ContractNo), "合同编号");
            columnList.Add(nameof(record.Remarks), "摘要");

            List<string> templateColumnNames = columnList.Values.Where(x => !string.IsNullOrEmpty(x)).ToList();
            this._dataImportService.DataFormatCheck(templateColumnNames, importDataTable);

            var tradeRecords = ObtainTradeDataFromImportDataTable(importOperation, importDataTable, columnList);

            return tradeRecords;
        }

        #endregion 交割单--华泰证券（普通）

        #region 交割单--申万证券（普通）

        /// <summary>
        /// 交割单--申万证券（普通）
        /// </summary>
        /// <param name="importOperation"></param>
        /// <param name="importDataTable"></param>
        /// <returns></returns>
        private IList<DeliveryRecord> DeliveryImportShenWan_N(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            Dictionary<string, string> columnList = new Dictionary<string, string>();
            DeliveryRecord record = null;

            columnList.Add(nameof(record.TradeDate), "成交日期");
            columnList.Add(nameof(record.TradeTime), null);
            columnList.Add(nameof(record.StockCode), "证券代码");
            columnList.Add(nameof(record.StockName), "证券名称");
            columnList.Add(nameof(record.DealFlag), "操作");
            columnList.Add(nameof(record.DealPrice), "成交均价");
            columnList.Add(nameof(record.DealVolume), "成交数量");
            columnList.Add(nameof(record.DealAmount), "成交金额");
            columnList.Add(nameof(record.ActualAmount), "发生金额");
            columnList.Add(nameof(record.Commission), "手续费");
            columnList.Add(nameof(record.StampDuty), "印花税");
            columnList.Add(nameof(record.Incidentals), "其他杂费");
            columnList.Add("OtherFee1", null);
            columnList.Add("OtherFee2", null);
            columnList.Add("OtherFee3", null);
            columnList.Add(nameof(record.StockHolderCode), "股东帐户");
            columnList.Add(nameof(record.DealNo), "成交编号");
            columnList.Add(nameof(record.ContractNo), "合同编号");
            columnList.Add(nameof(record.Remarks), "操作");

            List<string> templateColumnNames = columnList.Values.Where(x => !string.IsNullOrEmpty(x)).ToList();
            this._dataImportService.DataFormatCheck(templateColumnNames, importDataTable);

            var tradeRecords = ObtainTradeDataFromImportDataTable(importOperation, importDataTable, columnList);

            return tradeRecords;
        }

        #endregion 交割单--申万证券（普通）

        #region 交割单--国金证券（普通）

        /// <summary>
        /// 交割单--国金证券（普通）
        /// </summary>
        /// <param name="importOperation"></param>
        /// <param name="importDataTable"></param>
        /// <returns></returns>
        private IList<DeliveryRecord> DeliveryImportSinoLink_N(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            Dictionary<string, string> columnList = new Dictionary<string, string>();
            DeliveryRecord record = null;

            columnList.Add(nameof(record.TradeDate), "成交日期");
            columnList.Add(nameof(record.TradeTime), null);
            columnList.Add(nameof(record.StockCode), "证券代码");
            columnList.Add(nameof(record.StockName), "证券名称");
            columnList.Add(nameof(record.DealFlag), "买卖标志");
            columnList.Add(nameof(record.DealPrice), "成交价格");
            columnList.Add(nameof(record.DealVolume), "成交数量");
            columnList.Add(nameof(record.DealAmount), "成交金额");
            columnList.Add(nameof(record.ActualAmount), "发生金额");
            columnList.Add(nameof(record.Commission), "佣金");
            columnList.Add(nameof(record.StampDuty), "印花税");
            columnList.Add(nameof(record.Incidentals), "过户费");
            columnList.Add("OtherFee1", null);
            columnList.Add("OtherFee2", null);
            columnList.Add("OtherFee3", null);
            columnList.Add(nameof(record.StockHolderCode), "股东代码");
            columnList.Add(nameof(record.DealNo), "成交编号");
            columnList.Add(nameof(record.ContractNo), "成交编号");
            columnList.Add(nameof(record.Remarks), "备注");

            List<string> templateColumnNames = columnList.Values.Where(x => !string.IsNullOrEmpty(x)).ToList();
            this._dataImportService.DataFormatCheck(templateColumnNames, importDataTable);

            var tradeRecords = ObtainTradeDataFromImportDataTable(importOperation, importDataTable, columnList);

            return tradeRecords;
        }

        #endregion 交割单--国金证券（普通）

        #region 交割单--招商证券（普通）

        /// <summary>
        /// 交割单--招商证券（普通）
        /// </summary>
        /// <param name="importOperation"></param>
        /// <param name="importDataTable"></param>
        /// <returns></returns>
        private IList<DeliveryRecord> DeliveryImportZhaoShang_N(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            Dictionary<string, string> columnList = new Dictionary<string, string>();
            DeliveryRecord record = null;

            columnList.Add(nameof(record.TradeDate), "成交日期");
            columnList.Add(nameof(record.TradeTime), null);
            columnList.Add(nameof(record.StockCode), "证券代码");
            columnList.Add(nameof(record.StockName), "证券名称");
            columnList.Add(nameof(record.DealFlag), "业务名称");
            columnList.Add(nameof(record.DealPrice), "成交价格");
            columnList.Add(nameof(record.DealVolume), "成交数量");
            columnList.Add(nameof(record.DealAmount), null);
            columnList.Add(nameof(record.ActualAmount), "发生金额");
            columnList.Add(nameof(record.Commission), "手续费");
            columnList.Add(nameof(record.StampDuty), "印花税");
            columnList.Add(nameof(record.Incidentals), "过户费");
            columnList.Add("OtherFee1", "结算费");
            columnList.Add("OtherFee2", null);
            columnList.Add("OtherFee3", null);
            columnList.Add(nameof(record.StockHolderCode), "股东代码");
            columnList.Add(nameof(record.DealNo), "合同编号");
            columnList.Add(nameof(record.ContractNo), "合同编号");
            columnList.Add(nameof(record.Remarks), "业务名称");

            List<string> templateColumnNames = columnList.Values.Where(x => !string.IsNullOrEmpty(x)).Distinct().ToList();
            this._dataImportService.DataFormatCheck(templateColumnNames, importDataTable);

            var tradeRecords = ObtainTradeDataFromImportDataTable(importOperation, importDataTable, columnList);

            return tradeRecords;
        }

        #endregion 交割单--招商证券（普通）

        #region 交割单--浙商证券（信用）

        /// <summary>
        /// 交割单--浙商证券（信用）
        /// </summary>
        /// <param name="importOperation"></param>
        /// <param name="importDataTable"></param>
        /// <returns></returns>
        private IList<DeliveryRecord> DeliveryImportZheShang_C(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            Dictionary<string, string> columnList = new Dictionary<string, string>();
            DeliveryRecord record = null;

            columnList.Add(nameof(record.TradeDate), "成交日期");
            columnList.Add(nameof(record.TradeTime), null);
            columnList.Add(nameof(record.StockCode), "证券代码");
            columnList.Add(nameof(record.StockName), "证券名称");
            columnList.Add(nameof(record.DealFlag), "操作");
            columnList.Add(nameof(record.DealPrice), "成交价格");
            columnList.Add(nameof(record.DealVolume), "成交数量");
            columnList.Add(nameof(record.DealAmount), "成交金额");
            columnList.Add(nameof(record.ActualAmount), "发生金额");
            columnList.Add(nameof(record.Commission), "佣金");
            columnList.Add(nameof(record.StampDuty), "印花税");
            columnList.Add(nameof(record.Incidentals), "过户费");
            columnList.Add("OtherFee1", null);
            columnList.Add("OtherFee2", null);
            columnList.Add("OtherFee3", null);
            columnList.Add(nameof(record.StockHolderCode), "股东帐户");
            columnList.Add(nameof(record.DealNo), "成交编号");
            columnList.Add(nameof(record.ContractNo), "合同编号");
            columnList.Add(nameof(record.Remarks), "操作");

            List<string> templateColumnNames = columnList.Values.Where(x => !string.IsNullOrEmpty(x)).ToList();
            this._dataImportService.DataFormatCheck(templateColumnNames, importDataTable);

            var tradeRecords = ObtainTradeDataFromImportDataTable(importOperation, importDataTable, columnList);

            return tradeRecords;
        }

        #endregion 交割单--浙商证券（信用）

        #region 交割单--浙商证券（普通）

        /// <summary>
        /// 交割单--浙商证券（普通）
        /// </summary>
        /// <param name="importOperation"></param>
        /// <param name="importDataTable"></param>
        /// <returns></returns>
        private IList<DeliveryRecord> DeliveryImportZheShang_N(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            Dictionary<string, string> columnList = new Dictionary<string, string>();
            DeliveryRecord record = null;

            columnList.Add(nameof(record.TradeDate), "成交日期");
            columnList.Add(nameof(record.TradeTime), null);
            columnList.Add(nameof(record.StockCode), "证券代码");
            columnList.Add(nameof(record.StockName), "证券名称");
            columnList.Add(nameof(record.DealFlag), "操作");
            columnList.Add(nameof(record.DealPrice), "成交价格");
            columnList.Add(nameof(record.DealVolume), "成交数量");
            columnList.Add(nameof(record.DealAmount), "成交金额");
            columnList.Add(nameof(record.ActualAmount), "发生金额");
            columnList.Add(nameof(record.Commission), "佣金");
            columnList.Add(nameof(record.StampDuty), "印花税");
            columnList.Add(nameof(record.Incidentals), "过户费");
            columnList.Add("OtherFee1", null);
            columnList.Add("OtherFee2", null);
            columnList.Add("OtherFee3", null);
            columnList.Add(nameof(record.StockHolderCode), "股东帐户");
            columnList.Add(nameof(record.DealNo), "成交编号");
            columnList.Add(nameof(record.ContractNo), "合同编号");
            columnList.Add(nameof(record.Remarks), "备注");

            List<string> templateColumnNames = columnList.Values.Where(x => !string.IsNullOrEmpty(x)).ToList();
            this._dataImportService.DataFormatCheck(templateColumnNames, importDataTable);

            var tradeRecords = ObtainTradeDataFromImportDataTable(importOperation, importDataTable, columnList);

            return tradeRecords;
        }

        #endregion 交割单--浙商证券（普通）

        #endregion Delivery Data Import

        #endregion Utilities

        #region Methods

        public virtual bool DataImportProcess(EnumLibrary.SecurityAccount securityAccount, DataTable importDataTable, RecordImportOperationEntity importOperation, out IList<DataRow> skippedRecords)
        {
            _skippedRecords = null;

            IList<DeliveryRecord> records = new List<DeliveryRecord>();

            switch (securityAccount)
            {
                //财通信用
                case EnumLibrary.SecurityAccount.CaiTong_C:
                    records = DeliveryImportCaiTong_C(importOperation, importDataTable);
                    break;

                //财通普通
                case EnumLibrary.SecurityAccount.CaiTong_N:
                    records = DeliveryImportCaiTong_N(importOperation, importDataTable);
                    break;

                //安信普通
                case EnumLibrary.SecurityAccount.ESSENCE_N:
                    records = DeliveryImportESSENCE_N(importOperation, importDataTable);
                    break;

                //方正普通
                case EnumLibrary.SecurityAccount.Founder_N:
                    records = DeliveryImportFounder_N(importOperation, importDataTable);
                    break;

                //国金普通
                case EnumLibrary.SecurityAccount.SinoLink_N:
                    records = DeliveryImportSinoLink_N(importOperation, importDataTable);
                    break;

                //国泰信用
                case EnumLibrary.SecurityAccount.GuoTai_C:
                    records = DeliveryImportGuoTai_C(importOperation, importDataTable);
                    break;

                //国泰普通
                case EnumLibrary.SecurityAccount.GuoTai_N:
                    records = DeliveryImportGuoTai_N(importOperation, importDataTable);
                    break;

                //海通普通
                case EnumLibrary.SecurityAccount.HaiTong_N:
                    records = DeliveryImportHaiTong_N(importOperation, importDataTable);
                    break;

                //华泰信用
                case EnumLibrary.SecurityAccount.HuaTai_C:
                    records = DeliveryImportHuaTai_C(importOperation, importDataTable);
                    break;

                //华泰普通
                case EnumLibrary.SecurityAccount.HuaTai_N:
                    records = DeliveryImportHuaTai_N(importOperation, importDataTable);
                    break;

                //申万普通
                case EnumLibrary.SecurityAccount.ShenWan_N:
                    records = DeliveryImportShenWan_N(importOperation, importDataTable);
                    break;

                //银河普通
                case EnumLibrary.SecurityAccount.Galaxy_N:
                    records = DeliveryImportGalaxy_N(importOperation, importDataTable);
                    break;

                //招商普通
                case EnumLibrary.SecurityAccount.ZhaoShang_N:
                    records = DeliveryImportZhaoShang_N(importOperation, importDataTable);
                    break;

                //浙商普通
                case EnumLibrary.SecurityAccount.ZheShang_N:
                    records = DeliveryImportZheShang_N(importOperation, importDataTable);
                    break;

                //浙商信用
                case EnumLibrary.SecurityAccount.ZheShang_C:
                    records = DeliveryImportZheShang_C(importOperation, importDataTable);
                    break;

                //中信信用
                case EnumLibrary.SecurityAccount.CITIC_C:
                    records = DeliveryImportCITIC_C(importOperation, importDataTable);
                    break;

                //中信普通
                case EnumLibrary.SecurityAccount.CITIC_N:
                    records = DeliveryImportCITIC_N(importOperation, importDataTable);
                    break;

                //中银信用
                case EnumLibrary.SecurityAccount.BOCI_C:
                    records = DeliveryImportBOCI_C(importOperation, importDataTable);
                    break;

                //中银普通
                case EnumLibrary.SecurityAccount.BOCI_N:
                    records = DeliveryImportBOCI_N(importOperation, importDataTable);
                    break;
            }

            BatchInsertDeliveryRecords(records);

            skippedRecords = _skippedRecords;

            return true;
        }

        public virtual void BatchInsertDeliveryRecords(IList<DeliveryRecord> deliveryRecords)
        {
            if (deliveryRecords == null)
                throw new ArgumentNullException(nameof(deliveryRecords));

            _deliveryRepository.BatchInsert(deliveryRecords, 1000);
        }

        public virtual void InsertDeliveryRecords(IList<DeliveryRecord> deliveryRecords)
        {
            if (deliveryRecords == null)
                throw new ArgumentNullException(nameof(deliveryRecords));

            _deliveryRepository.Insert(deliveryRecords);
        }

        public virtual void DeleteDeliveryRecords(int[] ids)
        {
            if (ids == null)
                throw new ArgumentNullException(nameof(ids));

            var query = _deliveryRepository.Table;
            query = query.Where(x => ids.Contains(x.Id));

            _deliveryRepository.Delete(query.ToList());
        }

        public virtual IList<DeliveryRecord> GetDeliveryRecordsDetail(string stockCode, int accountId, bool? dealFlag, DateTime? tradeDateFrom, DateTime? tradeDateTo, string importUserCode, DateTime? importDateFrom, DateTime? importDateTo)
        {
            var query = _deliveryRepository.TableNoTracking;

            if (!string.IsNullOrEmpty(stockCode))
                query = query.Where(x => x.StockCode == stockCode);

            if (accountId > 0)
                query = query.Where(x => x.AccountId == accountId);

            if (dealFlag.HasValue)
                query = query.Where(x => x.DealFlag == dealFlag.Value);

            if (tradeDateFrom.HasValue)
                query = query.Where(x => x.TradeDate >= tradeDateFrom);

            if (tradeDateTo.HasValue)
                query = query.Where(x => x.TradeDate <= tradeDateTo);

            if (!string.IsNullOrEmpty(importUserCode))
                query = query.Where(x => x.ImportUser == importUserCode);

            if (importDateFrom.HasValue)
                query = query.Where(x => x.ImportTime >= importDateFrom);

            if (importDateTo.HasValue)
            {
                var to = importDateTo.Value.AddDays(1);
                query = query.Where(x => x.ImportTime < to);
            }

            var infos = from d in query
                        join a in _accountRepository.Table
                        on d.AccountId equals a.Id into temp1
                        from account in temp1.DefaultIfEmpty()
                        join u1 in _userRepository.Table
                        on d.ImportUser equals u1.Code into temp2
                        from importUser in temp2.DefaultIfEmpty()
                        join u4 in _userRepository.Table
                        on d.UpdateUser equals u4.Code into temp3
                        from updateUser in temp3.DefaultIfEmpty()
                        select new { d, account, importUser, updateUser };

            var result = infos.ToList().Select(x => new DeliveryRecord
            {
                Id = x.d.Id,
                AccountId = x.d.AccountId,
                AccountName = x.account == null ? null : x.account.Name + " - " + x.account.SecurityCompanyName + " - " + x.account.AttributeName,
                ActualAmount = x.d.ActualAmount,
                Commission = x.d.Commission,
                ContractNo = x.d.ContractNo,
                DataType = x.d.DataType,
                DealAmount = x.d.DealAmount,
                DealFlag = x.d.DealFlag,
                DealNo = x.d.DealNo,
                DealPrice = x.d.DealPrice,
                DealVolume = x.d.DealVolume,
                ImportTime = x.d.ImportTime,
                ImportUser = x.importUser == null ? null : x.importUser.Code,
                ImportUserName = x.importUser.Name,
                Incidentals = x.d.Incidentals,
                Remarks = x.d.Remarks,
                StampDuty = x.d.StampDuty,
                StockCode = x.d.StockCode,
                StockHolderCode = x.d.StockHolderCode,
                StockName = x.d.StockName,
                TradeDate = x.d.TradeDate,
                TradeTime = x.d.TradeTime,
                UpdateTime = x.d.UpdateTime,
                UpdateUser = x.d.UpdateUser,
                UpdateUserName = x.updateUser == null ? null : x.updateUser.Name,
            }
            )
            .ToList();

            return result;
        }

        public virtual IList<DeliveryRecord> GetDeliveryRecords(int[] accountIds, DateTime? tradeDateFrom, DateTime? tradeDateTo)
        {
            var query = _deliveryRepository.TableNoTracking;

            if (accountIds != null)
                query = query.Where(x => accountIds.Contains(x.AccountId));
            if (tradeDateFrom.HasValue)
                query = query.Where(x => x.TradeDate >= tradeDateFrom);
            if (tradeDateTo.HasValue)
                query = query.Where(x => x.TradeDate <= tradeDateTo);

            return query.ToList();
        }

        public virtual IList<int> GetTradingAccountIds()
        {
            var query = _deliveryRepository.Table.Select(x => x.AccountId).Distinct();

            return query.ToList();
        }

        public virtual void CopyToDailyRecord(IList<int> deliveryRecordIds, string importUserCode, int accountId, string beneficiary, int tradeType)
        {
            var deliveryRecords = _deliveryRepository.Table.Where(x => deliveryRecordIds.Contains(x.Id));

            var now = _commonService.GetCurrentServerTime();


            IList<DailyRecord> dailyRecords = new List<DailyRecord>();

            foreach (var item in deliveryRecords)
            {
                var dailyRecord = new DailyRecord
                {
                    AccountCode = item.AccountCode,
                    AccountId = item.AccountId,
                    ActualAmount = item.ActualAmount,
                    Beneficiary = beneficiary,
                    Commission = item.Commission,
                    ContractNo = item.ContractNo,
                    DataType = item.DataType,
                    DealAmount = item.DealAmount,
                    DealFlag = item.DealFlag,
                    DealNo = item.DealNo,
                    DealPrice = item.DealPrice,
                    DealVolume = item.DealVolume,
                    ImportTime = now,
                    ImportUser = importUserCode,
                    Incidentals = item.Incidentals,
                    OperatorCode = beneficiary,
                    Remarks = "从财务交割单复制导入",
                    StampDuty = item.StampDuty,
                    StockCode = item.StockCode,
                    StockHolderCode = item.StockHolderCode,
                    StockName = item.StockName,
                    TradeDate = item.TradeDate,
                    TradeTime = item.TradeTime,
                    TradeType = tradeType,
                    UpdateTime = now,
                    UpdateUser = importUserCode,
                };

                dailyRecords.Add(dailyRecord);
            }

            if (dailyRecords.Any())
                _dailyRepository.Insert(dailyRecords);

        }

        #endregion Methods
    }
}