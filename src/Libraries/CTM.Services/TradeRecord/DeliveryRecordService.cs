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
using CTM.Services.Stock;

namespace CTM.Services.TradeRecord
{
    public partial class DeliveryRecordService : IDeliveryRecordService
    {
        #region Fields

        private readonly IRepository<DeliveryRecord> _deliveryRepository;
        private readonly IRepository<UserInfo> _userRepository;
        private readonly IRepository<AccountInfo> _accountRepository;

        private readonly IDataImportCommonService _dataImportService;
        private readonly IStockService _stockService;

        #endregion Fields

        #region Constructors

        public DeliveryRecordService
            (
            IRepository<DeliveryRecord> deliveryRepository,
            IRepository<UserInfo> userRepository,
            IRepository<AccountInfo> accountRepository,
            IDataImportCommonService DICService,
            IStockService stockService
            )
        {
            this._deliveryRepository = deliveryRepository;
            this._userRepository = userRepository;
            this._accountRepository = accountRepository;
            this._dataImportService = DICService;
            this._stockService = stockService;
        }

        #endregion Constructors

        #region Utilities

        #region Delivery Data Import

        #region 交割单--财通证券（信用）

        /// <summary>
        /// 交割单--财通证券（信用）
        /// </summary>
        /// <param name="importOperation"></param>
        /// <param name="importDataTable"></param>
        /// <returns></returns>
        private IList<DeliveryRecord> DeliveryImportCaiTong_C(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            #region DataFormatCheck

            var TemplateColumnNames = new List<string> { "证券代码", "证券名称", "操作", "成交均价", "成交数量", "成交金额", "印花税", "过户费", "发生金额", "合同编号", "股东帐户", "委托日期", "可用余额", "其他杂费", "佣金", "可用金额" };

            this._dataImportService.DataFormatCheck(TemplateColumnNames, importDataTable);

            #endregion DataFormatCheck

            #region DataProcess

            var tradeRecords = new List<DeliveryRecord>();

            foreach (DataRow row in importDataTable.Rows)
            {
                //过滤交易记录
                if (int.Parse(row["成交数量"].ToString().Trim()) == 0 && decimal.Parse(row["发生金额"].ToString().Trim()) == 0) continue;

                var tradeRecord = new DeliveryRecord();

                var stockCode = CommonHelper.StockCodeZerofill(row["证券代码"].ToString().Trim());

                var stockName = row["证券名称"].ToString().Trim();

                var stockInfo = _stockService.GetStockInfoByCode(stockCode);

                if (stockInfo == null) continue;

                tradeRecord.StockCode = stockInfo.FullCode;

                tradeRecord.StockName = stockName;

                tradeRecord.SetTradeRecordCommonFields(importOperation);

                tradeRecord.TradeDate = CommonHelper.StringToDateTime(row["委托日期"].ToString().Trim());

                tradeRecord.TradeTime = string.Empty;

                tradeRecord.DealNo = string.Empty;

                tradeRecord.DealPrice = decimal.Parse(row["成交均价"].ToString().Trim());

                tradeRecord.DealAmount = decimal.Parse(row["成交金额"].ToString().Trim());

                //买入
                if (int.Parse(row["成交数量"].ToString().Trim()) > 0 || decimal.Parse(row["发生金额"].ToString().Trim()) < 0)
                    tradeRecord.DealFlag = true;
                //卖出
                else
                    tradeRecord.DealFlag = false;

                tradeRecord.DealVolume = int.Parse(row["成交数量"].ToString().Trim());

                tradeRecord.ActualAmount = decimal.Parse(row["发生金额"].ToString().Trim());

                tradeRecord.StockHolderCode = row["股东帐户"].ToString().Trim();

                tradeRecord.ContractNo = row["合同编号"].ToString().Trim();

                tradeRecord.Commission = decimal.Parse(row["佣金"].ToString().Trim());

                tradeRecord.StampDuty = decimal.Parse(row["印花税"].ToString().Trim());

                tradeRecord.Incidentals = decimal.Parse(row["过户费"].ToString().Trim()) + decimal.Parse(row["其他杂费"].ToString().Trim());

                tradeRecord.Remarks = row["操作"].ToString().Trim();

                tradeRecords.Add(tradeRecord);
            }

            #endregion DataProcess

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
            #region DataFormatCheck

            var TemplateColumnNames = new List<string> { "成交日期", "证券代码", "证券名称", "操作", "成交均价", "成交数量", "成交金额", "手续费", "印花税", "其他杂费", "发生金额", "本次金额", "合同编号", "成交时间", "股东帐户", "备注", "成交编号", "交易市场" };

            this._dataImportService.DataFormatCheck(TemplateColumnNames, importDataTable);

            #endregion DataFormatCheck

            #region DataProcess

            var tradeRecords = new List<DeliveryRecord>();

            foreach (DataRow row in importDataTable.Rows)
            {
                //过滤交易记录
                if (int.Parse(row["成交数量"].ToString().Trim()) == 0 && decimal.Parse(row["发生金额"].ToString().Trim()) == 0) continue;

                var tradeRecord = new DeliveryRecord();

                var stockCode = CommonHelper.StockCodeZerofill(row["证券代码"].ToString().Trim());

                var stockName = row["证券名称"].ToString().Trim();

                var stockInfo = _stockService.GetStockInfoByCode(stockCode);

                if (stockInfo == null) continue;

                tradeRecord.StockCode = stockInfo.FullCode;

                tradeRecord.StockName = stockName;

                tradeRecord.SetTradeRecordCommonFields(importOperation);

                tradeRecord.TradeDate = CommonHelper.StringToDateTime(row["成交日期"].ToString().Trim());

                tradeRecord.TradeTime = row["成交时间"].ToString().Trim();

                tradeRecord.DealNo = row["成交编号"].ToString().Trim();

                tradeRecord.DealPrice = decimal.Parse(row["成交均价"].ToString().Trim());

                tradeRecord.DealAmount = decimal.Parse(row["成交金额"].ToString().Trim());

                //买入
                if (int.Parse(row["成交数量"].ToString().Trim()) > 0 || decimal.Parse(row["发生金额"].ToString().Trim()) < 0)
                    tradeRecord.DealFlag = true;
                //卖出
                else
                    tradeRecord.DealFlag = false;

                tradeRecord.DealVolume = int.Parse(row["成交数量"].ToString().Trim());

                tradeRecord.ActualAmount = decimal.Parse(row["发生金额"].ToString().Trim());

                tradeRecord.StockHolderCode = row["股东帐户"].ToString().Trim();

                tradeRecord.ContractNo = row["合同编号"].ToString().Trim();

                tradeRecord.Commission = decimal.Parse(row["手续费"].ToString().Trim());

                tradeRecord.StampDuty = decimal.Parse(row["印花税"].ToString().Trim());

                tradeRecord.Incidentals = decimal.Parse(row["其他杂费"].ToString().Trim());

                tradeRecord.Remarks = row["操作"].ToString().Trim();

                tradeRecords.Add(tradeRecord);
            }

            #endregion DataProcess

            return tradeRecords;
        }

        #endregion 交割单--财通证券（普通）

        #region 交割单--方正证券（普通）

        /// <summary>
        ///  交割单--方正证券（普通）
        /// </summary>
        /// <param name="importOperation"></param>
        /// <param name="importDataTable"></param>
        /// <returns></returns>

        private IList<DeliveryRecord> DeliveryImportFounder_N(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            #region DataFormatCheck

            var TemplateColumnNames = new List<string> { "成交日期", "成交时间", "证券代码", "证券名称", "操作", "成交数量", "成交编号", "成交均价", "成交金额", "余额", "发生金额", "印花税", "其他杂费", "本次金额", "合同编号", "股东帐户", "佣金", "过户费", "交易市场" };

            this._dataImportService.DataFormatCheck(TemplateColumnNames, importDataTable);

            #endregion DataFormatCheck

            #region DataProcess

            var tradeRecords = new List<DeliveryRecord>();

            foreach (DataRow row in importDataTable.Rows)
            {
                //过滤交易记录
                if (int.Parse(row["成交数量"].ToString().Trim()) == 0 && decimal.Parse(row["发生金额"].ToString().Trim()) == 0) continue;

                var tradeRecord = new DeliveryRecord();

                var stockCode = CommonHelper.StockCodeZerofill(row["证券代码"].ToString().Trim());

                var stockName = row["证券名称"].ToString().Trim();

                var stockInfo = _stockService.GetStockInfoByCode(stockCode);

                if (stockInfo == null) continue;

                tradeRecord.StockCode = stockInfo.FullCode;

                tradeRecord.StockName = stockName;

                tradeRecord.SetTradeRecordCommonFields(importOperation);

                tradeRecord.TradeDate = CommonHelper.StringToDateTime(row["成交日期"].ToString().Trim());

                tradeRecord.TradeTime = row["成交时间"].ToString().Trim();

                tradeRecord.DealNo = row["成交编号"].ToString().Trim();

                tradeRecord.DealPrice = decimal.Parse(row["成交均价"].ToString().Trim());

                tradeRecord.DealAmount = decimal.Parse(row["成交金额"].ToString().Trim());

                //买入
                if (int.Parse(row["成交数量"].ToString().Trim()) > 0 || decimal.Parse(row["发生金额"].ToString().Trim()) < 0)
                    tradeRecord.DealFlag = true;
                //卖出
                else
                    tradeRecord.DealFlag = false;

                tradeRecord.DealVolume = int.Parse(row["成交数量"].ToString().Trim());

                tradeRecord.ActualAmount = decimal.Parse(row["发生金额"].ToString().Trim());

                tradeRecord.StockHolderCode = row["股东帐户"].ToString().Trim();

                tradeRecord.ContractNo = row["合同编号"].ToString().Trim();

                tradeRecord.Commission = decimal.Parse(row["佣金"].ToString().Trim());

                tradeRecord.StampDuty = decimal.Parse(row["印花税"].ToString().Trim());

                tradeRecord.Incidentals = decimal.Parse(row["过户费"].ToString().Trim()) + decimal.Parse(row["其他杂费"].ToString().Trim());

                tradeRecord.Remarks = row["操作"].ToString().Trim();

                tradeRecords.Add(tradeRecord);
            }

            #endregion DataProcess

            return tradeRecords;
        }

        #endregion 交割单--方正证券（普通）

        #region 交割单--国金证券（普通）

        /// <summary>
        /// 交割单--国金证券（普通）
        /// </summary>
        /// <param name="importOperation"></param>
        /// <param name="importDataTable"></param>
        /// <returns></returns>
        private IList<DeliveryRecord> DeliveryImportSinoLink_N(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            #region DataFormatCheck

            var TemplateColumnNames = new List<string> { "成交日期", "证券代码", "证券名称", "买卖标志", "成交价格", "成交数量", "成交金额", "发生金额", "佣金", "印花税", "过户费", "成交编号", "股东代码", "备注" };

            this._dataImportService.DataFormatCheck(TemplateColumnNames, importDataTable);

            #endregion DataFormatCheck

            #region DataProcess

            var tradeRecords = new List<DeliveryRecord>();

            foreach (DataRow row in importDataTable.Rows)
            {
                //过滤交易记录
                if (int.Parse(row["成交数量"].ToString().Trim()) == 0 && decimal.Parse(row["发生金额"].ToString().Trim()) == 0) continue;

                var tradeRecord = new DeliveryRecord();

                var stockCode = CommonHelper.StockCodeZerofill(row["证券代码"].ToString().Trim());

                var stockName = row["证券名称"].ToString().Trim();

                var stockInfo = _stockService.GetStockInfoByCode(stockCode);

                if (stockInfo == null) continue;

                tradeRecord.StockCode = stockInfo.FullCode;

                tradeRecord.StockName = stockName;

                tradeRecord.SetTradeRecordCommonFields(importOperation);

                tradeRecord.TradeDate = CommonHelper.StringToDateTime(row["成交日期"].ToString().Trim());

                tradeRecord.TradeTime = string.Empty;

                tradeRecord.DealNo = row["成交编号"].ToString().Trim();

                tradeRecord.DealPrice = decimal.Parse(row["成交价格"].ToString().Trim());

                tradeRecord.DealAmount = decimal.Parse(row["成交金额"].ToString().Trim());

                //买入
                if (int.Parse(row["成交数量"].ToString().Trim()) > 0 || decimal.Parse(row["发生金额"].ToString().Trim()) < 0)
                    tradeRecord.DealFlag = true;
                //卖出
                else
                    tradeRecord.DealFlag = false;

                tradeRecord.DealVolume = int.Parse(row["成交数量"].ToString().Trim());

                tradeRecord.ActualAmount = decimal.Parse(row["发生金额"].ToString().Trim());

                tradeRecord.StockHolderCode = row["股东代码"].ToString().Trim();

                tradeRecord.ContractNo = row["成交编号"].ToString().Trim();

                tradeRecord.Commission = decimal.Parse(row["佣金"].ToString().Trim());

                tradeRecord.StampDuty = decimal.Parse(row["印花税"].ToString().Trim());

                tradeRecord.Incidentals = decimal.Parse(row["过户费"].ToString().Trim());

                tradeRecord.Remarks = row["买卖标志"].ToString().Trim();

                tradeRecords.Add(tradeRecord);
            }

            #endregion DataProcess

            return tradeRecords;
        }

        #endregion 交割单--国金证券（普通）

        #region 交割单--国泰证券（信用）

        /// <summary>
        /// 交割单--国泰证券（信用）
        /// </summary>
        /// <param name="importOperation"></param>
        /// <param name="importDataTable"></param>
        /// <returns></returns>
        private IList<DeliveryRecord> DeliveryImportGuoTai_C(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            #region DataFormatCheck

            var TemplateColumnNames = new List<string> { "成交日期", "业务名称", "证券代码", "证券名称", "成交价格", "成交数量", "剩余数量", "成交金额", "清算金额", "剩余金额", "净佣金", "规费", "印花税", "过户费", "结算费", "附加费", "成交编号", "股东代码" };

            this._dataImportService.DataFormatCheck(TemplateColumnNames, importDataTable);

            #endregion DataFormatCheck

            #region DataProcess

            var tradeRecords = new List<DeliveryRecord>();

            foreach (DataRow row in importDataTable.Rows)
            {
                //过滤交易记录
                if (int.Parse(row["成交数量"].ToString().Trim()) == 0 && decimal.Parse(row["清算金额"].ToString().Trim()) == 0) continue;

                var tradeRecord = new DeliveryRecord();

                var stockCode = CommonHelper.StockCodeZerofill(row["证券代码"].ToString().Trim());

                var stockName = row["证券名称"].ToString().Trim();

                var stockInfo = _stockService.GetStockInfoByCode(stockCode);

                if (stockInfo == null) continue;

                tradeRecord.StockCode = stockInfo.FullCode;

                tradeRecord.StockName = stockName;

                tradeRecord.SetTradeRecordCommonFields(importOperation);

                tradeRecord.TradeDate = CommonHelper.StringToDateTime(row["成交日期"].ToString().Trim());

                tradeRecord.TradeTime = string.Empty;

                tradeRecord.DealNo = row["成交编号"].ToString().Trim();

                tradeRecord.DealPrice = decimal.Parse(row["成交价格"].ToString().Trim());

                tradeRecord.DealAmount = decimal.Parse(row["成交金额"].ToString().Trim());

                //买入
                if (int.Parse(row["成交数量"].ToString().Trim()) > 0 || decimal.Parse(row["清算金额"].ToString().Trim()) < 0)
                    tradeRecord.DealFlag = true;
                //卖出
                else
                    tradeRecord.DealFlag = false;

                tradeRecord.DealVolume = int.Parse(row["成交数量"].ToString().Trim());

                tradeRecord.ActualAmount = decimal.Parse(row["清算金额"].ToString().Trim());

                tradeRecord.StockHolderCode = row["股东代码"].ToString().Trim();

                tradeRecord.ContractNo = row["成交编号"].ToString().Trim();

                tradeRecord.Commission = decimal.Parse(row["净佣金"].ToString().Trim());

                tradeRecord.StampDuty = decimal.Parse(row["印花税"].ToString().Trim());

                tradeRecord.Incidentals = decimal.Parse(row["规费"].ToString().Trim()) + decimal.Parse(row["过户费"].ToString().Trim()) + decimal.Parse(row["结算费"].ToString().Trim()) + decimal.Parse(row["附加费"].ToString().Trim());

                tradeRecord.Remarks = row["业务名称"].ToString().Trim();

                tradeRecords.Add(tradeRecord);
            }

            #endregion DataProcess

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
            #region DataFormatCheck

            var TemplateColumnNames = new List<string> { "成交日期", "证券代码", "证券名称", "操作", "成交数量", "成交均价", "成交金额", "股票余额", "发生金额", "手续费", "印花税", "其他杂费", "资金余额", "合同编号", "市场名称", "股东帐户" };

            this._dataImportService.DataFormatCheck(TemplateColumnNames, importDataTable);

            #endregion DataFormatCheck

            #region DataProcess

            var tradeRecords = new List<DeliveryRecord>();

            foreach (DataRow row in importDataTable.Rows)
            {
                //过滤交易记录
                if (int.Parse(row["成交数量"].ToString().Trim()) == 0 && decimal.Parse(row["发生金额"].ToString().Trim()) == 0) continue;

                var tradeRecord = new DeliveryRecord();

                var stockCode = CommonHelper.StockCodeZerofill(row["证券代码"].ToString().Trim());

                var stockName = row["证券名称"].ToString().Trim();

                var stockInfo = _stockService.GetStockInfoByCode(stockCode);

                if (stockInfo == null) continue;

                tradeRecord.StockCode = stockInfo.FullCode;

                tradeRecord.StockName = stockName;

                tradeRecord.SetTradeRecordCommonFields(importOperation);

                tradeRecord.TradeDate = CommonHelper.StringToDateTime(row["成交日期"].ToString().Trim());

                tradeRecord.TradeTime = string.Empty;

                tradeRecord.DealNo = string.Empty;

                tradeRecord.DealPrice = decimal.Parse(row["成交均价"].ToString().Trim());

                tradeRecord.DealAmount = decimal.Parse(row["成交金额"].ToString().Trim());

                //买入
                if (int.Parse(row["成交数量"].ToString().Trim()) > 0 || decimal.Parse(row["发生金额"].ToString().Trim()) < 0)
                    tradeRecord.DealFlag = true;
                //卖出
                else
                    tradeRecord.DealFlag = false;

                tradeRecord.DealVolume = int.Parse(row["成交数量"].ToString().Trim());

                tradeRecord.ActualAmount = decimal.Parse(row["发生金额"].ToString().Trim());

                tradeRecord.StockHolderCode = row["股东帐户"].ToString().Trim();

                tradeRecord.ContractNo = row["合同编号"].ToString().Trim();

                tradeRecord.Commission = decimal.Parse(row["手续费"].ToString().Trim());

                tradeRecord.StampDuty = decimal.Parse(row["印花税"].ToString().Trim());

                tradeRecord.Incidentals = decimal.Parse(row["其他杂费"].ToString().Trim());

                tradeRecord.Remarks = row["操作"].ToString().Trim();

                tradeRecords.Add(tradeRecord);
            }

            #endregion DataProcess

            return tradeRecords;
        }

        #endregion 交割单--国泰证券（普通）

        #region 交割单--华泰证券（普通）

        /// <summary>
        /// 交割单--华泰证券（普通）
        /// </summary>
        /// <param name="importOperation"></param>
        /// <param name="importDataTable"></param>
        /// <returns></returns>
        private IList<DeliveryRecord> DeliveryImportHuaTai_N(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            #region DataFormatCheck

            var TemplateColumnNames = new List<string> { "成交日期", "摘要", "证券名称", "合同编号", "成交数量", "成交均价", "成交金额", "手续费", "印花税", "其他杂费", "发生金额", "股东帐户", "备注", "操作", "证券代码", "结算汇率" };

            this._dataImportService.DataFormatCheck(TemplateColumnNames, importDataTable);

            #endregion DataFormatCheck

            #region DataProcess

            var tradeRecords = new List<DeliveryRecord>();

            foreach (DataRow row in importDataTable.Rows)
            {
                //过滤交易记录
                if (int.Parse(row["成交数量"].ToString().Trim()) == 0 && decimal.Parse(row["发生金额"].ToString().Trim()) == 0) continue;

                var tradeRecord = new DeliveryRecord();

                var stockCode = CommonHelper.StockCodeZerofill(row["证券代码"].ToString().Trim());

                var stockName = row["证券名称"].ToString().Trim();

                var stockInfo = _stockService.GetStockInfoByCode(stockCode);

                if (stockInfo == null) continue;

                tradeRecord.StockCode = stockInfo.FullCode;

                tradeRecord.StockName = stockName;

                tradeRecord.SetTradeRecordCommonFields(importOperation);

                tradeRecord.TradeDate = CommonHelper.StringToDateTime(row["成交日期"].ToString().Trim());

                tradeRecord.TradeTime = string.Empty;

                tradeRecord.DealNo = string.Empty;

                tradeRecord.DealPrice = decimal.Parse(row["成交均价"].ToString().Trim());

                tradeRecord.DealAmount = decimal.Parse(row["成交金额"].ToString().Trim());

                //买入
                if (int.Parse(row["成交数量"].ToString().Trim()) > 0 || decimal.Parse(row["发生金额"].ToString().Trim()) < 0)
                    tradeRecord.DealFlag = true;
                //卖出
                else
                    tradeRecord.DealFlag = false;

                tradeRecord.DealVolume = int.Parse(row["成交数量"].ToString().Trim());

                tradeRecord.ActualAmount = decimal.Parse(row["发生金额"].ToString().Trim());

                tradeRecord.StockHolderCode = row["股东帐户"].ToString().Trim();

                tradeRecord.ContractNo = row["合同编号"].ToString().Trim();

                tradeRecord.Commission = decimal.Parse(row["手续费"].ToString().Trim());

                tradeRecord.StampDuty = decimal.Parse(row["印花税"].ToString().Trim());

                tradeRecord.Incidentals = decimal.Parse(row["其他杂费"].ToString().Trim());

                tradeRecord.Remarks = row["操作"].ToString().Trim();

                tradeRecords.Add(tradeRecord);
            }

            #endregion DataProcess

            return tradeRecords;
        }

        #endregion 交割单--华泰证券（普通）

        #region 交割单--华泰证券（信用）

        /// <summary>
        /// 交割单--华泰证券（信用）
        /// </summary>
        /// <param name="importOperation"></param>
        /// <param name="importDataTable"></param>
        /// <returns></returns>
        private IList<DeliveryRecord> DeliveryImportHuaTai_C(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            #region DataFormatCheck

            var TemplateColumnNames = new List<string> { "成交日期", "摘要", "证券名称", "合同编号", "成交数量", "成交均价", "成交金额", "手续费", "印花税", "其他杂费", "发生金额", "股东帐户", "备注", "本次资金余额", "本次股票余额" };

            this._dataImportService.DataFormatCheck(TemplateColumnNames, importDataTable);

            #endregion DataFormatCheck

            #region DataProcess

            var tradeRecords = new List<DeliveryRecord>();

            foreach (DataRow row in importDataTable.Rows)
            {
                //过滤交易记录
                if (int.Parse(row["成交数量"].ToString().Trim()) == 0 && decimal.Parse(row["发生金额"].ToString().Trim()) == 0) continue;

                var tradeRecord = new DeliveryRecord();

                tradeRecord.SetTradeRecordCommonFields(importOperation);

                var stockCode = string.Empty;

                var stockName = row["证券名称"].ToString().Trim();

                tradeRecord.TradeDate = CommonHelper.StringToDateTime(row["成交日期"].ToString().Trim());

                tradeRecord.TradeTime = string.Empty;

                tradeRecord.DealNo = string.Empty;

                tradeRecord.DealPrice = decimal.Parse(row["成交均价"].ToString().Trim());

                tradeRecord.DealAmount = decimal.Parse(row["成交金额"].ToString().Trim());

                //买入
                if (int.Parse(row["成交数量"].ToString().Trim()) > 0 || decimal.Parse(row["发生金额"].ToString().Trim()) < 0)
                    tradeRecord.DealFlag = true;
                //卖出
                else
                    tradeRecord.DealFlag = false;

                tradeRecord.DealVolume = int.Parse(row["成交数量"].ToString().Trim());

                tradeRecord.ActualAmount = decimal.Parse(row["发生金额"].ToString().Trim());

                tradeRecord.StockHolderCode = row["股东帐户"].ToString().Trim();

                tradeRecord.ContractNo = row["合同编号"].ToString().Trim();

                tradeRecord.Commission = decimal.Parse(row["手续费"].ToString().Trim());

                tradeRecord.StampDuty = decimal.Parse(row["印花税"].ToString().Trim());

                tradeRecord.Incidentals = decimal.Parse(row["其他杂费"].ToString().Trim());

                tradeRecord.Remarks = row["摘要"].ToString().Trim();

                tradeRecords.Add(tradeRecord);
            }

            #endregion DataProcess

            return tradeRecords;
        }

        #endregion 交割单--华泰证券（信用）

        #region 交割单--申万证券（普通）

        /// <summary>
        /// 交割单--申万证券（普通）
        /// </summary>
        /// <param name="importOperation"></param>
        /// <param name="importDataTable"></param>
        /// <returns></returns>
        private IList<DeliveryRecord> DeliveryImportShenWan_N(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            #region DataFormatCheck

            var TemplateColumnNames = new List<string> { "成交日期", "证券代码", "证券名称", "操作", "成交数量", "成交编号", "成交均价", "成交金额", "余额", "发生金额", "手续费", "印花税", "其他杂费", "本次金额", "合同编号", "股东帐户", "交易市场" };

            this._dataImportService.DataFormatCheck(TemplateColumnNames, importDataTable);

            #endregion DataFormatCheck

            #region DataProcess

            var tradeRecords = new List<DeliveryRecord>();

            foreach (DataRow row in importDataTable.Rows)
            {
                //过滤交易记录
                if (int.Parse(row["成交数量"].ToString().Trim()) == 0 && decimal.Parse(row["发生金额"].ToString().Trim()) == 0) continue;

                var tradeRecord = new DeliveryRecord();

                var stockCode = CommonHelper.StockCodeZerofill(row["证券代码"].ToString().Trim());

                var stockName = row["证券名称"].ToString().Trim();

                var stockInfo = _stockService.GetStockInfoByCode(stockCode);

                if (stockInfo == null) continue;

                tradeRecord.StockCode = stockInfo.FullCode;

                tradeRecord.StockName = stockName;

                tradeRecord.SetTradeRecordCommonFields(importOperation);

                tradeRecord.TradeDate = CommonHelper.StringToDateTime(row["成交日期"].ToString().Trim());

                tradeRecord.TradeTime = string.Empty;

                tradeRecord.DealNo = row["成交编号"].ToString().Trim();

                tradeRecord.DealPrice = decimal.Parse(row["成交均价"].ToString().Trim());

                tradeRecord.DealAmount = decimal.Parse(row["成交金额"].ToString().Trim());

                //买入
                if (int.Parse(row["成交数量"].ToString().Trim()) > 0 || decimal.Parse(row["发生金额"].ToString().Trim()) < 0)
                    tradeRecord.DealFlag = true;
                //卖出
                else
                    tradeRecord.DealFlag = false;

                tradeRecord.DealVolume = int.Parse(row["成交数量"].ToString().Trim());

                tradeRecord.ActualAmount = decimal.Parse(row["发生金额"].ToString().Trim());

                tradeRecord.StockHolderCode = row["股东帐户"].ToString().Trim();

                tradeRecord.ContractNo = row["合同编号"].ToString().Trim();

                tradeRecord.Commission = decimal.Parse(row["手续费"].ToString().Trim());

                tradeRecord.StampDuty = decimal.Parse(row["印花税"].ToString().Trim());

                tradeRecord.Incidentals = decimal.Parse(row["其他杂费"].ToString().Trim());

                tradeRecord.Remarks = row["操作"].ToString().Trim();

                tradeRecords.Add(tradeRecord);
            }

            #endregion DataProcess

            return tradeRecords;
        }

        #endregion 交割单--申万证券（普通）

        #region 交割单--银河证券（普通）

        /// <summary>
        /// 交割单--银河证券（普通）
        /// </summary>
        /// <param name="importOperation"></param>
        /// <param name="importDataTable"></param>
        /// <returns></returns>
        private IList<DeliveryRecord> DeliveryImportGalaxy_N(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            #region DataFormatCheck

            var TemplateColumnNames = new List<string> { "证券代码", "证券名称", "操作", "成交数量", "成交均价", "成交金额", "股票余额", "发生金额", "手续费", "印花税", "其他杂费", "资金余额", "合同编号", "股东帐户", "交收日期", "净佣金", "过户费", "结算费", "币种" };

            this._dataImportService.DataFormatCheck(TemplateColumnNames, importDataTable);

            #endregion DataFormatCheck

            #region DataProcess

            var tradeRecords = new List<DeliveryRecord>();

            foreach (DataRow row in importDataTable.Rows)
            {
                //过滤交易记录
                if (int.Parse(row["成交数量"].ToString().Trim()) == 0 && decimal.Parse(row["发生金额"].ToString().Trim()) == 0) continue;

                var tradeRecord = new DeliveryRecord();

                var stockCode = CommonHelper.StockCodeZerofill(row["证券代码"].ToString().Trim());

                var stockName = row["证券名称"].ToString().Trim();

                var stockInfo = _stockService.GetStockInfoByCode(stockCode);

                if (stockInfo == null) continue;

                tradeRecord.StockCode = stockInfo.FullCode;

                tradeRecord.StockName = stockName;

                tradeRecord.SetTradeRecordCommonFields(importOperation);

                tradeRecord.TradeDate = CommonHelper.StringToDateTime(row["交收日期"].ToString().Trim());

                tradeRecord.TradeTime = string.Empty;

                tradeRecord.DealNo = string.Empty;

                tradeRecord.DealPrice = decimal.Parse(row["成交均价"].ToString().Trim());

                tradeRecord.DealAmount = decimal.Parse(row["成交金额"].ToString().Trim());

                //买入
                if (int.Parse(row["成交数量"].ToString().Trim()) > 0 || decimal.Parse(row["发生金额"].ToString().Trim()) < 0)
                    tradeRecord.DealFlag = true;
                //卖出
                else
                    tradeRecord.DealFlag = false;

                tradeRecord.DealVolume = int.Parse(row["成交数量"].ToString().Trim());

                tradeRecord.ActualAmount = decimal.Parse(row["发生金额"].ToString().Trim());

                tradeRecord.StockHolderCode = row["股东帐户"].ToString().Trim();

                tradeRecord.ContractNo = row["合同编号"].ToString().Trim();

                tradeRecord.Commission = decimal.Parse(row["净佣金"].ToString().Trim());

                tradeRecord.StampDuty = decimal.Parse(row["印花税"].ToString().Trim());

                tradeRecord.Incidentals = decimal.Parse(row["手续费"].ToString().Trim()) + decimal.Parse(row["其他杂费"].ToString().Trim()) + decimal.Parse(row["过户费"].ToString().Trim()) + decimal.Parse(row["结算费"].ToString().Trim());

                tradeRecord.Remarks = row["操作"].ToString().Trim();

                tradeRecords.Add(tradeRecord);
            }

            #endregion DataProcess

            return tradeRecords;
        }

        #endregion 交割单--银河证券（普通）

        #region 交割单--招商证券（普通）

        /// <summary>
        /// 交割单--招商证券（普通）
        /// </summary>
        /// <param name="importOperation"></param>
        /// <param name="importDataTable"></param>
        /// <returns></returns>
        private IList<DeliveryRecord> DeliveryImportZhaoShang_N(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            #region DataFormatCheck

            var TemplateColumnNames = new List<string> { "币种", "证券名称", "成交日期", "成交价格", "成交数量", "发生金额", "资金余额", "合同编号", "业务名称", "手续费", "印花税", "过户费", "结算费", "证券代码", "股东代码" };

            this._dataImportService.DataFormatCheck(TemplateColumnNames, importDataTable);

            #endregion DataFormatCheck

            #region DataProcess

            var tradeRecords = new List<DeliveryRecord>();

            foreach (DataRow row in importDataTable.Rows)
            {
                //过滤交易记录
                if (int.Parse(row["成交数量"].ToString().Trim()) == 0 && decimal.Parse(row["发生金额"].ToString().Trim()) == 0) continue;

                var tradeRecord = new DeliveryRecord();

                var stockCode = CommonHelper.StockCodeZerofill(row["证券代码"].ToString().Trim());

                var stockName = row["证券名称"].ToString().Trim();

                var stockInfo = _stockService.GetStockInfoByCode(stockCode);

                if (stockInfo == null) continue;

                tradeRecord.StockCode = stockInfo.FullCode;

                tradeRecord.StockName = stockName;

                tradeRecord.SetTradeRecordCommonFields(importOperation);

                tradeRecord.TradeDate = CommonHelper.StringToDateTime(row["成交日期"].ToString().Trim().Substring(0, 8));

                tradeRecord.TradeTime = string.Empty;

                tradeRecord.DealNo = string.Empty;

                //买入
                if (int.Parse(row["成交数量"].ToString().Trim()) > 0 || decimal.Parse(row["发生金额"].ToString().Trim()) < 0)
                    tradeRecord.DealFlag = true;
                //卖出
                else
                    tradeRecord.DealFlag = false;

                tradeRecord.DealVolume = int.Parse(row["成交数量"].ToString().Trim());

                tradeRecord.ActualAmount = decimal.Parse(row["发生金额"].ToString().Trim());

                tradeRecord.DealPrice = decimal.Parse(row["成交价格"].ToString().Trim());

                tradeRecord.DealAmount = tradeRecord.DealPrice * Math.Abs(tradeRecord.DealVolume);

                tradeRecord.StockHolderCode = row["股东代码"].ToString().Trim();

                tradeRecord.ContractNo = row["合同编号"].ToString().Trim();

                tradeRecord.Commission = decimal.Parse(row["手续费"].ToString().Trim());

                tradeRecord.StampDuty = decimal.Parse(row["印花税"].ToString().Trim());

                tradeRecord.Incidentals = decimal.Parse(row["过户费"].ToString().Trim()) + decimal.Parse(row["结算费"].ToString().Trim()); ;

                tradeRecord.Remarks = row["业务名称"].ToString().Trim();

                tradeRecords.Add(tradeRecord);
            }

            #endregion DataProcess

            return tradeRecords;
        }

        #endregion 交割单--招商证券（普通）

        #region 交割单--浙商证券（普通）

        /// <summary>
        /// 交割单--浙商证券（普通）
        /// </summary>
        /// <param name="importOperation"></param>
        /// <param name="importDataTable"></param>
        /// <returns></returns>
        private IList<DeliveryRecord> DeliveryImportZheShang_N(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            #region DataFormatCheck

            var TemplateColumnNames = new List<string> { "成交日期", "证券代码", "证券名称", "成交价格", "发生数量", "成交数量", "成交金额", "发生金额", "股票余额", "佣金", "印花税", "过户费", "成交编号", "合同编号", "操作", "股东帐户", "交易市场", "备注" };

            this._dataImportService.DataFormatCheck(TemplateColumnNames, importDataTable);

            #endregion DataFormatCheck

            #region DataProcess

            var tradeRecords = new List<DeliveryRecord>();

            foreach (DataRow row in importDataTable.Rows)
            {
                //过滤交易记录
                if (int.Parse(row["成交数量"].ToString().Trim()) == 0 && decimal.Parse(row["发生金额"].ToString().Trim()) == 0) continue;

                var tradeRecord = new DeliveryRecord();

                var stockCode = CommonHelper.StockCodeZerofill(row["证券代码"].ToString().Trim());

                var stockName = row["证券名称"].ToString().Trim();

                var stockInfo = _stockService.GetStockInfoByCode(stockCode);

                if (stockInfo == null) continue;

                tradeRecord.StockCode = stockInfo.FullCode;

                tradeRecord.StockName = stockName;

                tradeRecord.SetTradeRecordCommonFields(importOperation);

                tradeRecord.TradeDate = CommonHelper.StringToDateTime(row["成交日期"].ToString().Trim());

                tradeRecord.TradeTime = string.Empty;

                tradeRecord.DealNo = row["成交编号"].ToString().Trim();

                tradeRecord.DealPrice = decimal.Parse(row["成交价格"].ToString().Trim());

                tradeRecord.DealAmount = decimal.Parse(row["成交金额"].ToString().Trim());

                //买入
                if (int.Parse(row["成交数量"].ToString().Trim()) > 0 || decimal.Parse(row["发生金额"].ToString().Trim()) < 0)
                    tradeRecord.DealFlag = true;
                //卖出
                else
                    tradeRecord.DealFlag = false;

                tradeRecord.DealVolume = int.Parse(row["成交数量"].ToString().Trim());

                tradeRecord.ActualAmount = decimal.Parse(row["发生金额"].ToString().Trim());

                tradeRecord.StockHolderCode = row["股东帐户"].ToString().Trim();

                tradeRecord.ContractNo = row["合同编号"].ToString().Trim();

                tradeRecord.Commission = decimal.Parse(row["佣金"].ToString().Trim());

                tradeRecord.StampDuty = decimal.Parse(row["印花税"].ToString().Trim());

                tradeRecord.Incidentals = decimal.Parse(row["过户费"].ToString().Trim());

                tradeRecord.Remarks = row["操作"].ToString().Trim();

                tradeRecords.Add(tradeRecord);
            }

            #endregion DataProcess

            return tradeRecords;
        }

        #endregion 交割单--浙商证券（普通）

        #region 交割单--中信国际（信用）

        /// <summary>
        /// 交割单--中信国际（信用）
        /// </summary>
        /// <param name="importOperation"></param>
        /// <param name="importDataTable"></param>
        /// <returns></returns>
        private IList<DeliveryRecord> DeliveryImportCITIC_C(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            #region DataFormatCheck

            var TemplateColumnNames = new List<string> { "发生日期", "证券名称", "委托编号", "成交数量", "成交价格", "成交金额", "手续费", "印花税", "清算金额", "资金本次余额", "股东代码", "备注", "过户费", "交易所清算费", "成交时间", "资金帐号", "币种", "费用备注" };

            this._dataImportService.DataFormatCheck(TemplateColumnNames, importDataTable);

            #endregion DataFormatCheck

            #region DataProcess

            var tradeRecords = new List<DeliveryRecord>();

            foreach (DataRow row in importDataTable.Rows)
            {
                //过滤交易记录
                if (int.Parse(row["成交数量"].ToString().Trim()) == 0 && decimal.Parse(row["清算金额"].ToString().Trim()) == 0) continue;

                var tradeRecord = new DeliveryRecord();

                var stockName = row["证券名称"].ToString().Trim();

                var stockInfo = _stockService.GetStockInfoByName(stockName);

                if (stockInfo == null) continue;

                tradeRecord.StockCode = stockInfo.FullCode;

                tradeRecord.StockName = stockName;

                tradeRecord.SetTradeRecordCommonFields(importOperation);

                tradeRecord.TradeDate = CommonHelper.StringToDateTime(row["发生日期"].ToString().Trim());

                tradeRecord.TradeTime = row["成交时间"].ToString().Trim();

                tradeRecord.DealNo = string.Empty;

                tradeRecord.DealPrice = decimal.Parse(row["成交价格"].ToString().Trim());

                tradeRecord.DealAmount = decimal.Parse(row["成交金额"].ToString().Trim());

                //买入
                if (int.Parse(row["成交数量"].ToString().Trim()) > 0 || decimal.Parse(row["清算金额"].ToString().Trim()) < 0)
                    tradeRecord.DealFlag = true;
                //卖出
                else
                    tradeRecord.DealFlag = false;

                tradeRecord.DealVolume = int.Parse(row["成交数量"].ToString().Trim());

                tradeRecord.ActualAmount = decimal.Parse(row["清算金额"].ToString().Trim());

                tradeRecord.StockHolderCode = row["股东代码"].ToString().Trim();

                tradeRecord.ContractNo = row["委托编号"].ToString().Trim();

                tradeRecord.Commission = decimal.Parse(row["手续费"].ToString().Trim());

                tradeRecord.StampDuty = decimal.Parse(row["印花税"].ToString().Trim());

                tradeRecord.Incidentals = decimal.Parse(row["过户费"].ToString().Trim()) + decimal.Parse(row["交易所清算费"].ToString().Trim());

                tradeRecord.Remarks = row["备注"].ToString().Trim();

                tradeRecords.Add(tradeRecord);
            }

            #endregion DataProcess

            return tradeRecords;
        }

        #endregion 交割单--中信国际（信用）

        #region 交割单--中信证券（普通）

        /// <summary>
        /// 交割单--中信证券（普通）
        /// </summary>
        /// <param name="importOperation"></param>
        /// <param name="importDataTable"></param>
        /// <returns></returns>
        private IList<DeliveryRecord> DeliveryImportCITIC_N(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            #region DataFormatCheck

            var TemplateColumnNames = new List<string> { "发生日期", "成交时间", "证券代码", "证券名称", "业务名称", "成交数量", "成交价格", "成交金额", "余额", "清算金额", "手续费", "印花税", "附加费", "资金本次余额", "委托编号", "股东代码", "过户费", "交易所清算费", "资金帐号", "币种", "费用备注" };

            this._dataImportService.DataFormatCheck(TemplateColumnNames, importDataTable);

            #endregion DataFormatCheck

            #region DataProcess

            var tradeRecords = new List<DeliveryRecord>();

            foreach (DataRow row in importDataTable.Rows)
            {
                //过滤交易记录
                if (int.Parse(row["成交数量"].ToString().Trim()) == 0 && decimal.Parse(row["清算金额"].ToString().Trim()) == 0) continue;

                var tradeRecord = new DeliveryRecord();

                var stockCode = CommonHelper.StockCodeZerofill(row["证券代码"].ToString().Trim());

                var stockName = row["证券名称"].ToString().Trim();

                var stockInfo = _stockService.GetStockInfoByCode(stockCode);

                if (stockInfo == null) continue;

                tradeRecord.StockCode = stockInfo.FullCode;

                tradeRecord.StockName = stockName;

                tradeRecord.SetTradeRecordCommonFields(importOperation);

                tradeRecord.TradeDate = CommonHelper.StringToDateTime(row["发生日期"].ToString().Trim());

                tradeRecord.TradeTime = row["成交时间"].ToString().Trim();

                tradeRecord.DealNo = string.Empty;

                tradeRecord.DealPrice = decimal.Parse(row["成交价格"].ToString().Trim());

                tradeRecord.DealAmount = decimal.Parse(row["成交金额"].ToString().Trim());

                //买入
                if (int.Parse(row["成交数量"].ToString().Trim()) > 0 || decimal.Parse(row["清算金额"].ToString().Trim()) < 0)
                    tradeRecord.DealFlag = true;
                //卖出
                else
                    tradeRecord.DealFlag = false;

                tradeRecord.DealVolume = int.Parse(row["成交数量"].ToString().Trim());

                tradeRecord.ActualAmount = decimal.Parse(row["清算金额"].ToString().Trim());

                tradeRecord.StockHolderCode = row["股东代码"].ToString().Trim();

                tradeRecord.ContractNo = row["委托编号"].ToString().Trim();

                tradeRecord.Commission = decimal.Parse(row["手续费"].ToString().Trim());

                tradeRecord.StampDuty = decimal.Parse(row["印花税"].ToString().Trim());

                tradeRecord.Incidentals = decimal.Parse(row["过户费"].ToString().Trim()) + decimal.Parse(row["交易所清算费"].ToString().Trim()) + decimal.Parse(row["附加费"].ToString().Trim());

                tradeRecord.Remarks = row["业务名称"].ToString().Trim();

                tradeRecords.Add(tradeRecord);
            }

            #endregion DataProcess

            return tradeRecords;
        }

        #endregion 交割单--中信证券（普通）

        #region 交割单--中银国际（信用）

        /// <summary>
        /// 交割单--中银国际（信用）
        /// </summary>
        /// <param name="importOperation"></param>
        /// <param name="importDataTable"></param>
        /// <returns></returns>
        private IList<DeliveryRecord> DeliveryImportBOCI_C(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            #region DataFormatCheck

            var TemplateColumnNames = new List<string> { "发生日期", "成交时间", "证券代码", "证券名称", "买卖标志", "成交价格", "成交数量", "成交金额", "发生金额", "剩余金额", "申报序号", "成交编号", "委托编号", "股东代码", "席位代码", "证券数量", "佣金", "印花税", "过户费", "交易征费", "交易规费", "备注" };

            this._dataImportService.DataFormatCheck(TemplateColumnNames, importDataTable);

            #endregion DataFormatCheck

            #region DataProcess

            var tradeRecords = new List<DeliveryRecord>();

            foreach (DataRow row in importDataTable.Rows)
            {
                //过滤交易记录
                if (int.Parse(row["成交数量"].ToString().Trim()) == 0 && decimal.Parse(row["发生金额"].ToString().Trim()) == 0) continue;

                var tradeRecord = new DeliveryRecord();

                var stockCode = CommonHelper.StockCodeZerofill(row["证券代码"].ToString().Trim());

                var stockName = row["证券名称"].ToString().Trim();

                var stockInfo = _stockService.GetStockInfoByCode(stockCode);

                if (stockInfo == null) continue;

                tradeRecord.StockCode = stockInfo.FullCode;

                tradeRecord.StockName = stockName;

                tradeRecord.SetTradeRecordCommonFields(importOperation);

                tradeRecord.TradeDate = CommonHelper.StringToDateTime(row["发生日期"].ToString().Trim());

                tradeRecord.TradeTime = row["成交时间"].ToString().Trim();

                tradeRecord.DealNo = row["成交编号"].ToString().Trim();

                tradeRecord.DealPrice = decimal.Parse(row["成交价格"].ToString().Trim());

                tradeRecord.DealAmount = decimal.Parse(row["成交金额"].ToString().Trim());

                //买入
                if (int.Parse(row["成交数量"].ToString().Trim()) > 0 || decimal.Parse(row["发生金额"].ToString().Trim()) < 0)
                    tradeRecord.DealFlag = true;
                //卖出
                else
                    tradeRecord.DealFlag = false;

                tradeRecord.DealVolume = int.Parse(row["成交数量"].ToString().Trim());

                tradeRecord.ActualAmount = decimal.Parse(row["发生金额"].ToString().Trim());

                tradeRecord.StockHolderCode = row["股东代码"].ToString().Trim();

                tradeRecord.ContractNo = row["委托编号"].ToString().Trim();

                tradeRecord.Commission = decimal.Parse(row["佣金"].ToString().Trim());

                tradeRecord.StampDuty = decimal.Parse(row["印花税"].ToString().Trim());

                tradeRecord.Incidentals = decimal.Parse(row["过户费"].ToString().Trim()) + decimal.Parse(row["交易征费"].ToString().Trim()) + decimal.Parse(row["交易规费"].ToString().Trim());

                tradeRecord.Remarks = row["买卖标志"].ToString().Trim();

                tradeRecords.Add(tradeRecord);
            }

            #endregion DataProcess

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
            #region DataFormatCheck

            var TemplateColumnNames = new List<string> { "发生日期", "成交时间", "证券代码", "证券名称", "买卖标志", "成交价格", "成交数量", "成交金额", "发生金额", "剩余金额", "申报序号", "成交编号", "委托编号", "股东代码", "席位代码", "证券数量", "佣金", "印花税", "过户费", "交易征费", "交易规费", "备注" };

            this._dataImportService.DataFormatCheck(TemplateColumnNames, importDataTable);

            #endregion DataFormatCheck

            #region DataProcess

            var tradeRecords = new List<DeliveryRecord>();

            foreach (DataRow row in importDataTable.Rows)
            {
                //过滤交易记录
                if (int.Parse(row["成交数量"].ToString().Trim()) == 0 && decimal.Parse(row["发生金额"].ToString().Trim()) == 0) continue;

                var tradeRecord = new DeliveryRecord();

                var stockCode = CommonHelper.StockCodeZerofill(row["证券代码"].ToString().Trim());

                var stockName = row["证券名称"].ToString().Trim();

                var stockInfo = _stockService.GetStockInfoByCode(stockCode);

                if (stockInfo == null) continue;

                tradeRecord.StockCode = stockInfo.FullCode;

                tradeRecord.StockName = stockName;

                tradeRecord.SetTradeRecordCommonFields(importOperation);

                tradeRecord.TradeDate = CommonHelper.StringToDateTime(row["发生日期"].ToString().Trim());

                tradeRecord.TradeTime = row["成交时间"].ToString().Trim();

                tradeRecord.DealNo = row["成交编号"].ToString().Trim();

                tradeRecord.DealPrice = decimal.Parse(row["成交价格"].ToString().Trim());

                tradeRecord.DealAmount = decimal.Parse(row["成交金额"].ToString().Trim());

                //买入
                if (int.Parse(row["成交数量"].ToString().Trim()) > 0 || decimal.Parse(row["发生金额"].ToString().Trim()) < 0)
                    tradeRecord.DealFlag = true;
                //卖出
                else
                    tradeRecord.DealFlag = false;

                tradeRecord.DealVolume = int.Parse(row["成交数量"].ToString().Trim());

                tradeRecord.ActualAmount = decimal.Parse(row["发生金额"].ToString().Trim());

                tradeRecord.StockHolderCode = row["股东代码"].ToString().Trim();

                tradeRecord.ContractNo = row["委托编号"].ToString().Trim();

                tradeRecord.Commission = decimal.Parse(row["佣金"].ToString().Trim());

                tradeRecord.StampDuty = decimal.Parse(row["印花税"].ToString().Trim());

                tradeRecord.Incidentals = decimal.Parse(row["过户费"].ToString().Trim()) + decimal.Parse(row["交易征费"].ToString().Trim()) + decimal.Parse(row["交易规费"].ToString().Trim());

                tradeRecord.Remarks = row["买卖标志"].ToString().Trim();

                tradeRecords.Add(tradeRecord);
            }

            #endregion DataProcess

            return tradeRecords;
        }

        #endregion 交割单--中银国际（普通）

        #endregion Delivery Data Import

        #endregion Utilities

        #region Methods

        public virtual bool DataImportProcess(EnumLibrary.SecurityAccount securityAccount, DataTable importDataTable, RecordImportOperationEntity importOperation)
        {
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

        #endregion Methods
    }
}