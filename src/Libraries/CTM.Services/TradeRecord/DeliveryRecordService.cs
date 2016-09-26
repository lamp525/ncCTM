using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using CTM.Core;
using CTM.Core.Data;
using CTM.Core.Domain.TradeRecord;
using CTM.Core.Util;
using CTM.Services.Stock;

namespace CTM.Services.TradeRecord
{
    public partial class DeliveryRecordService : IDeliveryRecordService
    {
        #region Fields

        private readonly IRepository<DeliveryRecord> _deliveryRepository;

        private readonly IDataImportCommonService _dataImportService;
        private readonly IStockService _stockService;

        #endregion Fields

        #region Constants

        private readonly List<string> _buyTexts = new List<string> { "买入", "证券买入", "融券回购", "普通买入", "担保物买入", "融资买入", "融资借入" };
        private readonly List<string> _sellTexts = new List<string> { "卖出", "证券卖出", "融券购回", "普通卖出", "担保物卖出", "融资卖出", "卖券还款" };

        #endregion Constants

        #region Constructors

        public DeliveryRecordService
            (
            IRepository<DeliveryRecord> deliveryRepository,
            IDataImportCommonService DICService,
            IStockService stockService
            )
        {
            this._deliveryRepository = deliveryRepository;
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
        private bool DeliveryImportCaiTong_C(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            #region DataFormatCheck

            var TemplateColumnNames = new List<string> { "证券代码", "证券名称", "操作", "成交均价", "成交数量", "成交金额", "印花税", "过户费", "发生金额", "合同编号", "股东帐户", "委托日期", "可用余额", "其他杂费", "佣金", "可用金额" };

            this._dataImportService.DataFormatCheck(TemplateColumnNames, importDataTable);

            #endregion DataFormatCheck

            #region DataProcess

            var tradeRecords = new List<DeliveryRecord>();

            foreach (DataRow row in importDataTable.Rows)
            {
                //过滤无实际成交额或成交量的交易记录
                if (int.Parse(row["成交数量"].ToString().Trim()) == 0 || decimal.Parse(row["成交金额"].ToString().Trim()) == 0) continue;

                var tradeRecord = new DeliveryRecord();

                tradeRecord.SetTradeRecordCommonFields(importOperation);

                var stockCode = CommonHelper.StockCodeZerofill(row["证券代码"].ToString().Trim());

                var stockName = row["证券名称"].ToString().Trim();

                var stockInfo = _stockService.GetStockInfoByCode(stockCode);

                tradeRecord.StockCode = stockInfo.FullCode;

                tradeRecord.StockName = stockName;

                tradeRecord.TradeDate = CommonHelper.StringToDateTime(row["委托日期"].ToString().Trim());

                tradeRecord.TradeTime = string.Empty;

                if (_buyTexts.Contains(row["操作"].ToString().Trim()))
                    tradeRecord.DealFlag = true;
                else if (_sellTexts.Contains(row["操作"].ToString().Trim()))
                    tradeRecord.DealFlag = false;
                else
                {
                    //跳过操作类型不明的数据
                    continue;
                }

                tradeRecord.DealNo = string.Empty;

                tradeRecord.DealPrice = decimal.Parse(row["成交均价"].ToString().Trim());

                tradeRecord.DealAmount = decimal.Parse(row["成交金额"].ToString().Trim());

                //买入
                if (tradeRecord.DealFlag)
                {
                    tradeRecord.DealVolume = CommonHelper.ConvertToPositive(int.Parse(row["成交数量"].ToString().Trim()));

                    tradeRecord.ActualAmount = CommonHelper.ConvertToNegtive(decimal.Parse(row["发生金额"].ToString().Trim()));
                }
                //卖出
                else
                {
                    tradeRecord.DealVolume = CommonHelper.ConvertToNegtive(int.Parse(row["成交数量"].ToString().Trim()));

                    tradeRecord.ActualAmount = CommonHelper.ConvertToPositive(decimal.Parse(row["发生金额"].ToString().Trim()));
                }

                tradeRecord.StockHolderCode = row["股东帐户"].ToString().Trim();

                tradeRecord.ContractNo = row["合同编号"].ToString().Trim();

                tradeRecord.Commission = decimal.Parse(row["佣金"].ToString().Trim());

                tradeRecord.StampDuty = decimal.Parse(row["印花税"].ToString().Trim());

                tradeRecord.Incidentals = decimal.Parse(row["过户费"].ToString().Trim()) + decimal.Parse(row["其他杂费"].ToString().Trim());

                tradeRecord.Remarks = string.Empty;

                tradeRecords.Add(tradeRecord);
            }

            InsertDeliveryRecords(tradeRecords);

            #endregion DataProcess

            return true;
        }

        #endregion 交割单--财通证券（信用）

        #region 交割单--财通证券（普通）

        /// <summary>
        /// 交割单--财通证券（普通）
        /// </summary>
        /// <param name="importOperation"></param>
        /// <param name="importDataTable"></param>
        /// <returns></returns>
        private bool DeliveryImportCaiTong_N(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            #region DataFormatCheck

            var TemplateColumnNames = new List<string> { "成交日期", "证券代码", "证券名称", "操作", "成交均价", "成交数量", "成交金额", "手续费", "印花税", "其他杂费", "发生金额", "本次金额", "合同编号", "成交时间", "股东帐户", "备注", "成交编号", "交易市场", "交易类别" };

            this._dataImportService.DataFormatCheck(TemplateColumnNames, importDataTable);

            #endregion DataFormatCheck

            #region DataProcess

            var tradeRecords = new List<DeliveryRecord>();

            foreach (DataRow row in importDataTable.Rows)
            {
                //过滤无实际成交额或成交量的交易记录
                if (int.Parse(row["成交数量"].ToString().Trim()) == 0 || decimal.Parse(row["成交金额"].ToString().Trim()) == 0) continue;

                var tradeRecord = new DeliveryRecord();

                tradeRecord.SetTradeRecordCommonFields(importOperation);

                var stockCode = CommonHelper.StockCodeZerofill(row["证券代码"].ToString().Trim());

                var stockName = row["证券名称"].ToString().Trim();

                var stockInfo = _stockService.GetStockInfoByCode(stockCode);

                tradeRecord.StockCode = stockInfo.FullCode;

                tradeRecord.StockName = stockName;

                tradeRecord.TradeDate = CommonHelper.StringToDateTime(row["成交日期"].ToString().Trim());

                tradeRecord.TradeTime = row["成交时间"].ToString().Trim();

                if (_buyTexts.Contains(row["操作"].ToString().Trim()))
                    tradeRecord.DealFlag = true;
                else if (_sellTexts.Contains(row["操作"].ToString().Trim()))
                    tradeRecord.DealFlag = false;
                else
                {
                    //跳过操作类型不明的数据
                    continue;
                }

                tradeRecord.DealNo = row["成交编号"].ToString().Trim();

                tradeRecord.DealPrice = decimal.Parse(row["成交均价"].ToString().Trim());

                tradeRecord.DealAmount = decimal.Parse(row["成交金额"].ToString().Trim());

                //买入
                if (tradeRecord.DealFlag)
                {
                    tradeRecord.DealVolume = CommonHelper.ConvertToPositive(int.Parse(row["成交数量"].ToString().Trim()));

                    tradeRecord.ActualAmount = CommonHelper.ConvertToNegtive(decimal.Parse(row["发生金额"].ToString().Trim()));
                }
                //卖出
                else
                {
                    tradeRecord.DealVolume = CommonHelper.ConvertToNegtive(int.Parse(row["成交数量"].ToString().Trim()));

                    tradeRecord.ActualAmount = CommonHelper.ConvertToPositive(decimal.Parse(row["发生金额"].ToString().Trim()));
                }

                tradeRecord.StockHolderCode = row["股东帐户"].ToString().Trim();

                tradeRecord.ContractNo = row["合同编号"].ToString().Trim();

                tradeRecord.Commission = decimal.Parse(row["手续费"].ToString().Trim());

                tradeRecord.StampDuty = decimal.Parse(row["印花税"].ToString().Trim());

                tradeRecord.Incidentals = decimal.Parse(row["其他杂费"].ToString().Trim());

                tradeRecord.Remarks = row["备注"].ToString().Trim();

                tradeRecords.Add(tradeRecord);
            }

            InsertDeliveryRecords(tradeRecords);

            #endregion DataProcess

            return true;
        }

        #endregion 交割单--财通证券（普通）

        #region 交割单--方正证券（普通）

        /// <summary>
        ///  交割单--方正证券（普通）
        /// </summary>
        /// <param name="importOperation"></param>
        /// <param name="importDataTable"></param>
        /// <returns></returns>

        private bool DeliveryImportFounder_N(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            #region DataFormatCheck

            var TemplateColumnNames = new List<string> { "成交日期", "成交时间", "证券代码", "证券名称", "操作", "成交数量", "成交编号", "成交均价", "成交金额", "余额", "发生金额", "印花税", "其他杂费", "本次金额", "合同编号", "股东帐户", "佣金", "过户费", "交易市场", "交易类别" };

            this._dataImportService.DataFormatCheck(TemplateColumnNames, importDataTable);

            #endregion DataFormatCheck

            #region DataProcess

            var tradeRecords = new List<DeliveryRecord>();

            foreach (DataRow row in importDataTable.Rows)
            {
                //过滤无实际成交额或成交量的交易记录
                if (int.Parse(row["成交数量"].ToString().Trim()) == 0 || decimal.Parse(row["成交金额"].ToString().Trim()) == 0) continue;

                var tradeRecord = new DeliveryRecord();

                tradeRecord.SetTradeRecordCommonFields(importOperation);

                var stockCode = CommonHelper.StockCodeZerofill(row["证券代码"].ToString().Trim());

                var stockName = row["证券名称"].ToString().Trim();

                var stockInfo = _stockService.GetStockInfoByCode(stockCode);

                tradeRecord.StockCode = stockInfo.FullCode;

                tradeRecord.StockName = stockName;

                tradeRecord.TradeDate = CommonHelper.StringToDateTime(row["成交日期"].ToString().Trim());

                tradeRecord.TradeTime = row["成交时间"].ToString().Trim();

                if (_buyTexts.Contains(row["操作"].ToString().Trim()))
                    tradeRecord.DealFlag = true;
                else if (_sellTexts.Contains(row["操作"].ToString().Trim()))
                    tradeRecord.DealFlag = false;
                else
                {
                    //跳过操作类型不明的数据
                    continue;
                }

                tradeRecord.DealNo = row["成交编号"].ToString().Trim();

                tradeRecord.DealPrice = decimal.Parse(row["成交均价"].ToString().Trim());

                tradeRecord.DealAmount = decimal.Parse(row["成交金额"].ToString().Trim());

                //买入
                if (tradeRecord.DealFlag)
                {
                    tradeRecord.DealVolume = CommonHelper.ConvertToPositive(int.Parse(row["成交数量"].ToString().Trim()));

                    tradeRecord.ActualAmount = CommonHelper.ConvertToNegtive(decimal.Parse(row["发生金额"].ToString().Trim()));
                }
                //卖出
                else
                {
                    tradeRecord.DealVolume = CommonHelper.ConvertToNegtive(int.Parse(row["成交数量"].ToString().Trim()));

                    tradeRecord.ActualAmount = CommonHelper.ConvertToPositive(decimal.Parse(row["发生金额"].ToString().Trim()));
                }

                tradeRecord.StockHolderCode = row["股东帐户"].ToString().Trim();

                tradeRecord.ContractNo = row["合同编号"].ToString().Trim();

                tradeRecord.Commission = decimal.Parse(row["佣金"].ToString().Trim());

                tradeRecord.StampDuty = decimal.Parse(row["印花税"].ToString().Trim());

                tradeRecord.Incidentals = decimal.Parse(row["过户费"].ToString().Trim()) + decimal.Parse(row["其他杂费"].ToString().Trim());

                tradeRecord.Remarks = row["操作"].ToString().Trim();

                tradeRecords.Add(tradeRecord);
            }

            InsertDeliveryRecords(tradeRecords);

            #endregion DataProcess

            return true;
        }

        #endregion 交割单--方正证券（普通）

        #region 交割单--国金证券（普通）

        /// <summary>
        /// 交割单--国金证券（普通）
        /// </summary>
        /// <param name="importOperation"></param>
        /// <param name="importDataTable"></param>
        /// <returns></returns>
        private bool DeliveryImportSinoLink_N(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            #region DataFormatCheck

            var TemplateColumnNames = new List<string> { "成交日期", "证券代码", "证券名称", "买卖标志", "成交价格", "成交数量", "成交金额", "发生金额", "佣金", "印花税", "过户费", "成交编号", "股东代码", "备注", "交易类别" };

            this._dataImportService.DataFormatCheck(TemplateColumnNames, importDataTable);

            #endregion DataFormatCheck

            #region DataProcess

            var tradeRecords = new List<DeliveryRecord>();

            foreach (DataRow row in importDataTable.Rows)
            {
                //过滤无实际成交额或成交量的交易记录
                if (int.Parse(row["成交数量"].ToString().Trim()) == 0 || decimal.Parse(row["成交金额"].ToString().Trim()) == 0) continue;

                var tradeRecord = new DeliveryRecord();

                tradeRecord.SetTradeRecordCommonFields(importOperation);

                var stockCode = CommonHelper.StockCodeZerofill(row["证券代码"].ToString().Trim());

                var stockName = row["证券名称"].ToString().Trim();

                var stockInfo = _stockService.GetStockInfoByCode(stockCode);

                tradeRecord.StockCode = stockInfo.FullCode;

                tradeRecord.StockName = stockName;

                tradeRecord.TradeDate = CommonHelper.StringToDateTime(row["成交日期"].ToString().Trim());

                tradeRecord.TradeTime = string.Empty;

                if (_buyTexts.Contains(row["买卖标志"].ToString().Trim()))
                    tradeRecord.DealFlag = true;
                else if (_sellTexts.Contains(row["买卖标志"].ToString().Trim()))
                    tradeRecord.DealFlag = false;
                else
                {
                    //跳过操作类型不明的数据
                    continue;
                }

                tradeRecord.DealNo = row["成交编号"].ToString().Trim();

                tradeRecord.DealPrice = decimal.Parse(row["成交价格"].ToString().Trim());

                tradeRecord.DealAmount = decimal.Parse(row["成交金额"].ToString().Trim());

                //买入
                if (tradeRecord.DealFlag)
                {
                    tradeRecord.DealVolume = CommonHelper.ConvertToPositive(int.Parse(row["成交数量"].ToString().Trim()));

                    tradeRecord.ActualAmount = CommonHelper.ConvertToNegtive(decimal.Parse(row["发生金额"].ToString().Trim()));
                }
                //卖出
                else
                {
                    tradeRecord.DealVolume = CommonHelper.ConvertToNegtive(int.Parse(row["成交数量"].ToString().Trim()));

                    tradeRecord.ActualAmount = CommonHelper.ConvertToPositive(decimal.Parse(row["发生金额"].ToString().Trim()));
                }

                tradeRecord.StockHolderCode = row["股东代码"].ToString().Trim();

                tradeRecord.ContractNo = string.Empty;

                tradeRecord.Commission = decimal.Parse(row["佣金"].ToString().Trim());

                tradeRecord.StampDuty = decimal.Parse(row["印花税"].ToString().Trim());

                tradeRecord.Incidentals = decimal.Parse(row["过户费"].ToString().Trim());

                tradeRecord.Remarks = row["备注"].ToString().Trim();

                tradeRecords.Add(tradeRecord);
            }

            InsertDeliveryRecords(tradeRecords);

            #endregion DataProcess

            return true;
        }

        #endregion 交割单--国金证券（普通）

        #region 交割单--国泰证券（信用）

        /// <summary>
        /// 交割单--国泰证券（信用）
        /// </summary>
        /// <param name="importOperation"></param>
        /// <param name="importDataTable"></param>
        /// <returns></returns>
        private bool DeliveryImportGuoTai_C(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            #region DataFormatCheck

            var TemplateColumnNames = new List<string> { "成交日期", "业务名称", "证券代码", "证券名称", "成交价格", "成交数量", "剩余数量", "成交金额", "清算金额", "剩余金额", "净佣金", "规费", "印花税", "过户费", "结算费", "附加费", "成交编号", "股东代码", "交易类别" };

            this._dataImportService.DataFormatCheck(TemplateColumnNames, importDataTable);

            #endregion DataFormatCheck

            #region DataProcess

            var tradeRecords = new List<DeliveryRecord>();

            foreach (DataRow row in importDataTable.Rows)
            {
                //过滤无实际成交额或成交量的交易记录
                if (int.Parse(row["成交数量"].ToString().Trim()) == 0 || decimal.Parse(row["成交金额"].ToString().Trim()) == 0) continue;

                var tradeRecord = new DeliveryRecord();

                tradeRecord.SetTradeRecordCommonFields(importOperation);

                var stockCode = CommonHelper.StockCodeZerofill(row["证券代码"].ToString().Trim());

                var stockName = row["证券名称"].ToString().Trim();

                var stockInfo = _stockService.GetStockInfoByCode(stockCode);

                tradeRecord.StockCode = stockInfo.FullCode;

                tradeRecord.StockName = stockName;

                tradeRecord.TradeDate = CommonHelper.StringToDateTime(row["成交日期"].ToString().Trim());

                tradeRecord.TradeTime = string.Empty;

                if (_buyTexts.Contains(row["业务名称"].ToString().Trim()))
                    tradeRecord.DealFlag = true;
                else if (_sellTexts.Contains(row["业务名称"].ToString().Trim()))
                    tradeRecord.DealFlag = false;
                else
                {
                    //跳过操作类型不明的数据
                    continue;
                }

                tradeRecord.DealNo = row["成交编号"].ToString().Trim();

                tradeRecord.DealPrice = decimal.Parse(row["成交价格"].ToString().Trim());

                tradeRecord.DealAmount = decimal.Parse(row["成交金额"].ToString().Trim());

                //买入
                if (tradeRecord.DealFlag)
                {
                    tradeRecord.DealVolume = CommonHelper.ConvertToPositive(int.Parse(row["成交数量"].ToString().Trim()));

                    tradeRecord.ActualAmount = CommonHelper.ConvertToNegtive(decimal.Parse(row["清算金额"].ToString().Trim()));
                }
                //卖出
                else
                {
                    tradeRecord.DealVolume = CommonHelper.ConvertToNegtive(int.Parse(row["成交数量"].ToString().Trim()));

                    tradeRecord.ActualAmount = CommonHelper.ConvertToPositive(decimal.Parse(row["清算金额"].ToString().Trim()));
                }

                tradeRecord.StockHolderCode = row["股东代码"].ToString().Trim();

                tradeRecord.ContractNo = string.Empty;

                tradeRecord.Commission = decimal.Parse(row["净佣金"].ToString().Trim());

                tradeRecord.StampDuty = decimal.Parse(row["印花税"].ToString().Trim());

                tradeRecord.Incidentals = decimal.Parse(row["规费"].ToString().Trim()) + decimal.Parse(row["过户费"].ToString().Trim()) + decimal.Parse(row["结算费"].ToString().Trim()) + decimal.Parse(row["附加费"].ToString().Trim());

                tradeRecord.Remarks = row["业务名称"].ToString().Trim();

                tradeRecords.Add(tradeRecord);
            }

            InsertDeliveryRecords(tradeRecords);

            #endregion DataProcess

            return true;
        }

        #endregion 交割单--国泰证券（信用）

        #region 交割单--国泰证券（普通）

        /// <summary>
        /// 交割单--国泰证券（普通）
        /// </summary>
        /// <param name="importOperation"></param>
        /// <param name="importDataTable"></param>
        /// <returns></returns>
        private bool DeliveryImportGuoTai_N(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            #region DataFormatCheck

            var TemplateColumnNames = new List<string> { "成交日期", "证券代码", "证券名称", "操作", "成交数量", "成交均价", "成交金额", "股票余额", "发生金额", "手续费", "印花税", "其他杂费", "资金余额", "合同编号", "市场名称", "股东帐户", "交易类别" };

            this._dataImportService.DataFormatCheck(TemplateColumnNames, importDataTable);

            #endregion DataFormatCheck

            #region DataProcess

            var tradeRecords = new List<DeliveryRecord>();

            foreach (DataRow row in importDataTable.Rows)
            {
                //过滤无实际成交额或成交量的交易记录
                if (int.Parse(row["成交数量"].ToString().Trim()) == 0 || decimal.Parse(row["成交金额"].ToString().Trim()) == 0) continue;

                var tradeRecord = new DeliveryRecord();

                tradeRecord.SetTradeRecordCommonFields(importOperation);

                var stockCode = CommonHelper.StockCodeZerofill(row["证券代码"].ToString().Trim());

                var stockName = row["证券名称"].ToString().Trim();

                var stockInfo = _stockService.GetStockInfoByCode(stockCode);

                tradeRecord.StockCode = stockInfo.FullCode;

                tradeRecord.StockName = stockName;

                tradeRecord.TradeDate = CommonHelper.StringToDateTime(row["成交日期"].ToString().Trim());

                tradeRecord.TradeTime = string.Empty;

                if (_buyTexts.Contains(row["操作"].ToString().Trim()))
                    tradeRecord.DealFlag = true;
                else if (_sellTexts.Contains(row["操作"].ToString().Trim()))
                    tradeRecord.DealFlag = false;
                else
                {
                    //跳过操作类型不明的数据
                    continue;
                }

                tradeRecord.DealNo = string.Empty;

                tradeRecord.DealPrice = decimal.Parse(row["成交均价"].ToString().Trim());

                tradeRecord.DealAmount = decimal.Parse(row["成交金额"].ToString().Trim());

                //买入
                if (tradeRecord.DealFlag)
                {
                    tradeRecord.DealVolume = CommonHelper.ConvertToPositive(int.Parse(row["成交数量"].ToString().Trim()));

                    tradeRecord.ActualAmount = CommonHelper.ConvertToNegtive(decimal.Parse(row["发生金额"].ToString().Trim()));
                }
                //卖出
                else
                {
                    tradeRecord.DealVolume = CommonHelper.ConvertToNegtive(int.Parse(row["成交数量"].ToString().Trim()));

                    tradeRecord.ActualAmount = CommonHelper.ConvertToPositive(decimal.Parse(row["发生金额"].ToString().Trim()));
                }

                tradeRecord.StockHolderCode = row["股东帐户"].ToString().Trim();

                tradeRecord.ContractNo = row["合同编号"].ToString().Trim();

                tradeRecord.Commission = decimal.Parse(row["手续费"].ToString().Trim());

                tradeRecord.StampDuty = decimal.Parse(row["印花税"].ToString().Trim());

                tradeRecord.Incidentals = decimal.Parse(row["其他杂费"].ToString().Trim());

                tradeRecord.Remarks = row["操作"].ToString().Trim();

                tradeRecords.Add(tradeRecord);
            }

            InsertDeliveryRecords(tradeRecords);

            #endregion DataProcess

            return true;
        }

        #endregion 交割单--国泰证券（普通）

        #region 交割单--华泰证券（普通）

        /// <summary>
        /// 交割单--华泰证券（普通）
        /// </summary>
        /// <param name="importOperation"></param>
        /// <param name="importDataTable"></param>
        /// <returns></returns>
        private bool DeliveryImportHuaTai_N(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            #region DataFormatCheck

            var TemplateColumnNames = new List<string> { "成交日期", "摘要", "证券名称", "合同编号", "成交数量", "成交均价", "成交金额", "手续费", "印花税", "其他杂费", "发生金额", "股东帐户", "备注", "操作", "证券代码", "结算汇率", "交易类别" };

            this._dataImportService.DataFormatCheck(TemplateColumnNames, importDataTable);

            #endregion DataFormatCheck

            #region DataProcess

            var tradeRecords = new List<DeliveryRecord>();

            foreach (DataRow row in importDataTable.Rows)
            {
                //过滤无实际成交额或成交量的交易记录
                if (int.Parse(row["成交数量"].ToString().Trim()) == 0 || decimal.Parse(row["成交金额"].ToString().Trim()) == 0) continue;

                var tradeRecord = new DeliveryRecord();

                tradeRecord.SetTradeRecordCommonFields(importOperation);

                var stockCode = CommonHelper.StockCodeZerofill(row["证券代码"].ToString().Trim());

                var stockName = row["证券名称"].ToString().Trim();

                var stockInfo = _stockService.GetStockInfoByCode(stockCode);

                tradeRecord.StockCode = stockInfo.FullCode;

                tradeRecord.StockName = stockName;

                tradeRecord.TradeDate = CommonHelper.StringToDateTime(row["成交日期"].ToString().Trim());

                tradeRecord.TradeTime = string.Empty;

                if (_buyTexts.Contains(row["操作"].ToString().Trim()))
                    tradeRecord.DealFlag = true;
                else if (_sellTexts.Contains(row["操作"].ToString().Trim()))
                    tradeRecord.DealFlag = false;
                else
                {
                    //跳过操作类型不明的数据
                    continue;
                }

                tradeRecord.DealNo = string.Empty;

                tradeRecord.DealPrice = decimal.Parse(row["成交均价"].ToString().Trim());

                tradeRecord.DealAmount = decimal.Parse(row["成交金额"].ToString().Trim());

                //买入
                if (tradeRecord.DealFlag)
                {
                    tradeRecord.DealVolume = CommonHelper.ConvertToPositive(int.Parse(row["成交数量"].ToString().Trim()));

                    tradeRecord.ActualAmount = CommonHelper.ConvertToNegtive(decimal.Parse(row["发生金额"].ToString().Trim()));
                }
                //卖出
                else
                {
                    tradeRecord.DealVolume = CommonHelper.ConvertToNegtive(int.Parse(row["成交数量"].ToString().Trim()));

                    tradeRecord.ActualAmount = CommonHelper.ConvertToPositive(decimal.Parse(row["发生金额"].ToString().Trim()));
                }

                tradeRecord.StockHolderCode = row["股东帐户"].ToString().Trim();

                tradeRecord.ContractNo = row["合同编号"].ToString().Trim();

                tradeRecord.Commission = decimal.Parse(row["手续费"].ToString().Trim());

                tradeRecord.StampDuty = decimal.Parse(row["印花税"].ToString().Trim());

                tradeRecord.Incidentals = decimal.Parse(row["其他杂费"].ToString().Trim());

                tradeRecord.Remarks = row["摘要"].ToString().Trim();

                tradeRecords.Add(tradeRecord);
            }

            InsertDeliveryRecords(tradeRecords);

            #endregion DataProcess

            return true;
        }

        #endregion 交割单--华泰证券（普通）

        #region 交割单--华泰证券（信用）

        /// <summary>
        /// 交割单--华泰证券（信用）
        /// </summary>
        /// <param name="importOperation"></param>
        /// <param name="importDataTable"></param>
        /// <returns></returns>
        private bool DeliveryImportHuaTai_C(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            #region DataFormatCheck

            var TemplateColumnNames = new List<string> { "成交日期", "摘要", "证券名称", "合同编号", "成交数量", "成交均价", "成交金额", "手续费", "印花税", "其他杂费", "发生金额", "股东帐户", "备注", "本次资金余额", "本次股票余额", "交易类别" };

            this._dataImportService.DataFormatCheck(TemplateColumnNames, importDataTable);

            #endregion DataFormatCheck

            #region DataProcess

            var tradeRecords = new List<DeliveryRecord>();

            foreach (DataRow row in importDataTable.Rows)
            {
                //过滤无实际成交额或成交量的交易记录
                if (int.Parse(row["成交数量"].ToString().Trim()) == 0 || decimal.Parse(row["成交金额"].ToString().Trim()) == 0) continue;

                var tradeRecord = new DeliveryRecord();

                tradeRecord.SetTradeRecordCommonFields(importOperation);

                var stockCode = string.Empty;

                var stockName = row["证券名称"].ToString().Trim();

                tradeRecord.TradeDate = CommonHelper.StringToDateTime(row["成交日期"].ToString().Trim());

                tradeRecord.TradeTime = string.Empty;

                if (_buyTexts.Contains(row["摘要"].ToString().Trim()))
                    tradeRecord.DealFlag = true;
                else if (_sellTexts.Contains(row["摘要"].ToString().Trim()))
                    tradeRecord.DealFlag = false;
                else
                {
                    //跳过操作类型不明的数据
                    continue;
                }

                tradeRecord.DealNo = string.Empty;

                tradeRecord.DealPrice = decimal.Parse(row["成交均价"].ToString().Trim());

                tradeRecord.DealAmount = decimal.Parse(row["成交金额"].ToString().Trim());

                //买入
                if (tradeRecord.DealFlag)
                {
                    tradeRecord.DealVolume = CommonHelper.ConvertToPositive(int.Parse(row["成交数量"].ToString().Trim()));

                    tradeRecord.ActualAmount = CommonHelper.ConvertToNegtive(decimal.Parse(row["发生金额"].ToString().Trim()));
                }
                //卖出
                else
                {
                    tradeRecord.DealVolume = CommonHelper.ConvertToNegtive(int.Parse(row["成交数量"].ToString().Trim()));

                    tradeRecord.ActualAmount = CommonHelper.ConvertToPositive(decimal.Parse(row["发生金额"].ToString().Trim()));
                }

                tradeRecord.StockHolderCode = row["股东帐户"].ToString().Trim();

                tradeRecord.ContractNo = row["合同编号"].ToString().Trim();

                tradeRecord.Commission = decimal.Parse(row["手续费"].ToString().Trim());

                tradeRecord.StampDuty = decimal.Parse(row["印花税"].ToString().Trim());

                tradeRecord.Incidentals = decimal.Parse(row["其他杂费"].ToString().Trim());

                tradeRecord.Remarks = row["备注"].ToString().Trim();

                tradeRecords.Add(tradeRecord);
            }

            InsertDeliveryRecords(tradeRecords);

            #endregion DataProcess

            return true;
        }

        #endregion 交割单--华泰证券（信用）

        #region 交割单--申万证券（普通）

        /// <summary>
        /// 交割单--申万证券（普通）
        /// </summary>
        /// <param name="importOperation"></param>
        /// <param name="importDataTable"></param>
        /// <returns></returns>
        private bool DeliveryImportShenWan_N(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            #region DataFormatCheck

            var TemplateColumnNames = new List<string> { "成交日期", "证券代码", "证券名称", "操作", "成交数量", "成交编号", "成交均价", "成交金额", "余额", "发生金额", "手续费", "印花税", "其他杂费", "本次金额", "合同编号", "股东帐户", "交易市场", "交易类别" };

            this._dataImportService.DataFormatCheck(TemplateColumnNames, importDataTable);

            #endregion DataFormatCheck

            #region DataProcess

            var tradeRecords = new List<DeliveryRecord>();

            foreach (DataRow row in importDataTable.Rows)
            {
                //过滤无实际成交额或成交量的交易记录
                if (int.Parse(row["成交数量"].ToString().Trim()) == 0 || decimal.Parse(row["成交金额"].ToString().Trim()) == 0) continue;

                var tradeRecord = new DeliveryRecord();

                tradeRecord.SetTradeRecordCommonFields(importOperation);

                var stockCode = CommonHelper.StockCodeZerofill(row["证券代码"].ToString().Trim());

                var stockName = row["证券名称"].ToString().Trim();

                var stockInfo = _stockService.GetStockInfoByCode(stockCode);

                tradeRecord.StockCode = stockInfo.FullCode;

                tradeRecord.StockName = stockName;

                tradeRecord.TradeDate = CommonHelper.StringToDateTime(row["成交日期"].ToString().Trim());

                tradeRecord.TradeTime = string.Empty;

                if (_buyTexts.Contains(row["操作"].ToString().Trim()))
                    tradeRecord.DealFlag = true;
                else if (_sellTexts.Contains(row["操作"].ToString().Trim()))
                    tradeRecord.DealFlag = false;
                else
                {
                    //跳过操作类型不明的数据
                    continue;
                }

                tradeRecord.DealNo = row["成交编号"].ToString().Trim();

                tradeRecord.DealPrice = decimal.Parse(row["成交均价"].ToString().Trim());

                tradeRecord.DealAmount = decimal.Parse(row["成交金额"].ToString().Trim());

                //买入
                if (tradeRecord.DealFlag)
                {
                    tradeRecord.DealVolume = CommonHelper.ConvertToPositive(int.Parse(row["成交数量"].ToString().Trim()));

                    tradeRecord.ActualAmount = CommonHelper.ConvertToNegtive(decimal.Parse(row["发生金额"].ToString().Trim()));
                }
                //卖出
                else
                {
                    tradeRecord.DealVolume = CommonHelper.ConvertToNegtive(int.Parse(row["成交数量"].ToString().Trim()));

                    tradeRecord.ActualAmount = CommonHelper.ConvertToPositive(decimal.Parse(row["发生金额"].ToString().Trim()));
                }

                tradeRecord.StockHolderCode = row["股东帐户"].ToString().Trim();

                tradeRecord.ContractNo = row["合同编号"].ToString().Trim();

                tradeRecord.Commission = decimal.Parse(row["手续费"].ToString().Trim());

                tradeRecord.StampDuty = decimal.Parse(row["印花税"].ToString().Trim());

                tradeRecord.Incidentals = decimal.Parse(row["其他杂费"].ToString().Trim());

                tradeRecord.Remarks = string.Empty;

                tradeRecords.Add(tradeRecord);
            }

            InsertDeliveryRecords(tradeRecords);

            #endregion DataProcess

            return true;
        }

        #endregion 交割单--申万证券（普通）

        #region 交割单--银河证券（普通）

        /// <summary>
        /// 交割单--银河证券（普通）
        /// </summary>
        /// <param name="importOperation"></param>
        /// <param name="importDataTable"></param>
        /// <returns></returns>
        private bool DeliveryImportGalaxy_N(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            #region DataFormatCheck

            var TemplateColumnNames = new List<string> { "证券代码", "证券名称", "操作", "成交数量", "成交均价", "成交金额", "股票余额", "发生金额", "手续费", "印花税", "其他杂费", "资金余额", "合同编号", "股东帐户", "交收日期", "净佣金", "过户费", "结算费", "币种", "交易类别" };

            this._dataImportService.DataFormatCheck(TemplateColumnNames, importDataTable);

            #endregion DataFormatCheck

            #region DataProcess

            var tradeRecords = new List<DeliveryRecord>();

            foreach (DataRow row in importDataTable.Rows)
            {
                //过滤无实际成交额或成交量的交易记录
                if (int.Parse(row["成交数量"].ToString().Trim()) == 0 || decimal.Parse(row["成交金额"].ToString().Trim()) == 0) continue;

                var tradeRecord = new DeliveryRecord();

                tradeRecord.SetTradeRecordCommonFields(importOperation);

                var stockCode = CommonHelper.StockCodeZerofill(row["证券代码"].ToString().Trim());

                var stockName = row["证券名称"].ToString().Trim();

                var stockInfo = _stockService.GetStockInfoByCode(stockCode);

                tradeRecord.StockCode = stockInfo.FullCode;

                tradeRecord.StockName = stockName;

                tradeRecord.TradeDate = CommonHelper.StringToDateTime(row["交收日期"].ToString().Trim());

                tradeRecord.TradeTime = string.Empty;

                if (_buyTexts.Contains(row["操作"].ToString().Trim()))
                    tradeRecord.DealFlag = true;
                else if (_sellTexts.Contains(row["操作"].ToString().Trim()))
                    tradeRecord.DealFlag = false;
                else
                {
                    //跳过操作类型不明的数据
                    continue;
                }

                tradeRecord.DealNo = string.Empty;

                tradeRecord.DealPrice = decimal.Parse(row["成交均价"].ToString().Trim());

                tradeRecord.DealAmount = decimal.Parse(row["成交金额"].ToString().Trim());

                //买入
                if (tradeRecord.DealFlag)
                {
                    tradeRecord.DealVolume = CommonHelper.ConvertToPositive(int.Parse(row["成交数量"].ToString().Trim()));

                    tradeRecord.ActualAmount = CommonHelper.ConvertToNegtive(decimal.Parse(row["发生金额"].ToString().Trim()));
                }
                //卖出
                else
                {
                    tradeRecord.DealVolume = CommonHelper.ConvertToNegtive(int.Parse(row["成交数量"].ToString().Trim()));

                    tradeRecord.ActualAmount = CommonHelper.ConvertToPositive(decimal.Parse(row["发生金额"].ToString().Trim()));
                }

                tradeRecord.StockHolderCode = row["股东帐户"].ToString().Trim();

                tradeRecord.ContractNo = row["合同编号"].ToString().Trim();

                tradeRecord.Commission = decimal.Parse(row["净佣金"].ToString().Trim());

                tradeRecord.StampDuty = decimal.Parse(row["印花税"].ToString().Trim());

                tradeRecord.Incidentals = decimal.Parse(row["手续费"].ToString().Trim()) + decimal.Parse(row["其他杂费"].ToString().Trim()) + decimal.Parse(row["过户费"].ToString().Trim()) + decimal.Parse(row["结算费"].ToString().Trim());

                tradeRecord.Remarks = string.Empty;

                tradeRecords.Add(tradeRecord);
            }

            InsertDeliveryRecords(tradeRecords);

            #endregion DataProcess

            return true;
        }

        #endregion 交割单--银河证券（普通）

        #region 交割单--招商证券（普通）

        /// <summary>
        /// 交割单--招商证券（普通）
        /// </summary>
        /// <param name="importOperation"></param>
        /// <param name="importDataTable"></param>
        /// <returns></returns>
        private bool DeliveryImportZhaoShang_N(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            #region DataFormatCheck

            var TemplateColumnNames = new List<string> { "币种", "证券名称", "成交日期", "成交价格", "成交数量", "发生金额", "资金余额", "合同编号", "业务名称", "手续费", "印花税", "过户费", "结算费", "证券代码", "股东代码", "交易类别" };

            this._dataImportService.DataFormatCheck(TemplateColumnNames, importDataTable);

            #endregion DataFormatCheck

            #region DataProcess

            var tradeRecords = new List<DeliveryRecord>();

            var validRecords = importDataTable.AsEnumerable()
                .Where(x =>
                CommonHelper.IsInt(x.Field<string>("合同编号").Trim()) &&
                Convert.ToDecimal(x.Field<string>("成交价格").Trim()) != 0 &&
                Convert.ToDecimal(x.Field<string>("成交数量").Trim()) != 0
                ).ToList();

            foreach (DataRow row in validRecords)
            {
                var tradeRecord = new DeliveryRecord();
                tradeRecord.SetTradeRecordCommonFields(importOperation);

                var stockCode = CommonHelper.StockCodeZerofill(row["证券代码"].ToString().Trim());

                var stockName = row["证券名称"].ToString().Trim();

                var stockInfo = _stockService.GetStockInfoByCode(stockCode);

                tradeRecord.StockCode = stockInfo.FullCode;

                tradeRecord.StockName = stockName;

                tradeRecord.TradeDate = CommonHelper.StringToDateTime(row["成交日期"].ToString().Trim().Substring(0, 8));

                tradeRecord.TradeTime = string.Empty;

                if (_buyTexts.Contains(row["业务名称"].ToString().Trim().Substring(0, 4)))
                    tradeRecord.DealFlag = true;
                else if (_sellTexts.Contains(row["业务名称"].ToString().Trim().Substring(0, 4)))
                    tradeRecord.DealFlag = false;
                else
                {
                    //跳过操作类型不明的数据
                    continue;
                }

                tradeRecord.DealNo = string.Empty;

                //买入
                if (tradeRecord.DealFlag)
                {
                    tradeRecord.DealVolume = (int)CommonHelper.ConvertToPositive(decimal.Parse(row["成交数量"].ToString().Trim()));

                    tradeRecord.ActualAmount = CommonHelper.ConvertToNegtive(decimal.Parse(row["发生金额"].ToString().Trim()));
                }
                //卖出
                else
                {
                    tradeRecord.DealVolume = (int)CommonHelper.ConvertToNegtive(decimal.Parse(row["成交数量"].ToString().Trim()));

                    tradeRecord.ActualAmount = CommonHelper.ConvertToPositive(decimal.Parse(row["发生金额"].ToString().Trim()));
                }

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

            InsertDeliveryRecords(tradeRecords);

            #endregion DataProcess

            return true;
        }

        #endregion 交割单--招商证券（普通）

        #region 交割单--浙商证券（普通）

        /// <summary>
        /// 交割单--浙商证券（普通）
        /// </summary>
        /// <param name="importOperation"></param>
        /// <param name="importDataTable"></param>
        /// <returns></returns>
        private bool DeliveryImportZheShang_N(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            #region DataFormatCheck

            var TemplateColumnNames = new List<string> { "成交日期", "证券代码", "证券名称", "成交价格", "发生数量", "成交数量", "成交金额", "发生金额", "股票余额", "佣金", "印花税", "过户费", "成交编号", "合同编号", "操作", "股东帐户", "交易市场", "备注", "交易类别" };

            this._dataImportService.DataFormatCheck(TemplateColumnNames, importDataTable);

            #endregion DataFormatCheck

            #region DataProcess

            var tradeRecords = new List<DeliveryRecord>();

            foreach (DataRow row in importDataTable.Rows)
            {
                //过滤无实际成交额或成交量的交易记录
                if (int.Parse(row["成交数量"].ToString().Trim()) == 0 || decimal.Parse(row["成交金额"].ToString().Trim()) == 0) continue;

                var tradeRecord = new DeliveryRecord();
                tradeRecord.SetTradeRecordCommonFields(importOperation);

                var stockCode = CommonHelper.StockCodeZerofill(row["证券代码"].ToString().Trim());

                var stockName = row["证券名称"].ToString().Trim();

                var stockInfo = _stockService.GetStockInfoByCode(stockCode);

                tradeRecord.StockCode = stockInfo.FullCode;

                tradeRecord.StockName = stockName;

                tradeRecord.TradeDate = CommonHelper.StringToDateTime(row["成交日期"].ToString().Trim());

                tradeRecord.TradeTime = string.Empty;

                if (_buyTexts.Contains(row["操作"].ToString().Trim()))
                    tradeRecord.DealFlag = true;
                else if (_sellTexts.Contains(row["操作"].ToString().Trim()))
                    tradeRecord.DealFlag = false;
                else
                {
                    //跳过操作类型不明的数据
                    continue;
                }

                tradeRecord.DealNo = row["成交编号"].ToString().Trim();

                tradeRecord.DealPrice = decimal.Parse(row["成交价格"].ToString().Trim());

                tradeRecord.DealAmount = decimal.Parse(row["成交金额"].ToString().Trim());

                //买入
                if (tradeRecord.DealFlag)
                {
                    tradeRecord.DealVolume = CommonHelper.ConvertToPositive(int.Parse(row["成交数量"].ToString().Trim()));

                    tradeRecord.ActualAmount = CommonHelper.ConvertToNegtive(decimal.Parse(row["发生金额"].ToString().Trim()));
                }
                //卖出
                else
                {
                    tradeRecord.DealVolume = CommonHelper.ConvertToNegtive(int.Parse(row["成交数量"].ToString().Trim()));

                    tradeRecord.ActualAmount = CommonHelper.ConvertToPositive(decimal.Parse(row["发生金额"].ToString().Trim()));
                }

                tradeRecord.StockHolderCode = row["股东帐户"].ToString().Trim();

                tradeRecord.ContractNo = row["合同编号"].ToString().Trim();

                tradeRecord.Commission = decimal.Parse(row["佣金"].ToString().Trim());

                tradeRecord.StampDuty = decimal.Parse(row["印花税"].ToString().Trim());

                tradeRecord.Incidentals = decimal.Parse(row["过户费"].ToString().Trim());

                tradeRecord.Remarks = row["备注"].ToString().Trim();

                tradeRecords.Add(tradeRecord);
            }

            InsertDeliveryRecords(tradeRecords);

            #endregion DataProcess

            return true;
        }

        #endregion 交割单--浙商证券（普通）

        #region 交割单--中信国际（信用）

        /// <summary>
        /// 交割单--中信国际（信用）
        /// </summary>
        /// <param name="importOperation"></param>
        /// <param name="importDataTable"></param>
        /// <returns></returns>
        private bool DeliveryImportCITIC_C(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            #region DataFormatCheck

            var TemplateColumnNames = new List<string> { "发生日期", "证券名称", "委托编号", "成交数量", "成交价格", "成交金额", "手续费", "印花税", "清算金额", "资金本次余额", "股东代码", "备注", "过户费", "交易所清算费", "成交时间", "资金帐号", "币种", "费用备注", "交易类别" };

            this._dataImportService.DataFormatCheck(TemplateColumnNames, importDataTable);

            #endregion DataFormatCheck

            #region DataProcess

            var tradeRecords = new List<DeliveryRecord>();

            foreach (DataRow row in importDataTable.Rows)
            {
                //过滤无实际成交额或成交量的交易记录
                if (int.Parse(row["成交数量"].ToString().Trim()) == 0 || decimal.Parse(row["成交金额"].ToString().Trim()) == 0) continue;

                var tradeRecord = new DeliveryRecord();

                tradeRecord.SetTradeRecordCommonFields(importOperation);

                var stockName = row["证券名称"].ToString().Trim();

                var stockInfo = _stockService.GetStockInfoByName(stockName);

                var stockCode = string.Empty;

                tradeRecord.StockCode = stockInfo.FullCode;

                tradeRecord.StockName = stockName;

                tradeRecord.TradeDate = CommonHelper.StringToDateTime(row["发生日期"].ToString().Trim());

                tradeRecord.TradeTime = row["成交时间"].ToString().Trim();

                if (_buyTexts.Contains(row["备注"].ToString().Trim()))
                    tradeRecord.DealFlag = true;
                else if (_sellTexts.Contains(row["备注"].ToString().Trim()))
                    tradeRecord.DealFlag = false;
                else
                {
                    //跳过操作类型不明的数据
                    continue;
                }

                tradeRecord.DealNo = string.Empty;

                tradeRecord.DealPrice = decimal.Parse(row["成交价格"].ToString().Trim());

                tradeRecord.DealAmount = decimal.Parse(row["成交金额"].ToString().Trim());

                //买入
                if (tradeRecord.DealFlag)
                {
                    tradeRecord.DealVolume = CommonHelper.ConvertToPositive(int.Parse(row["成交数量"].ToString().Trim()));

                    tradeRecord.ActualAmount = CommonHelper.ConvertToNegtive(decimal.Parse(row["清算金额"].ToString().Trim()));
                }
                //卖出
                else
                {
                    tradeRecord.DealVolume = CommonHelper.ConvertToNegtive(int.Parse(row["成交数量"].ToString().Trim()));

                    tradeRecord.ActualAmount = CommonHelper.ConvertToPositive(decimal.Parse(row["清算金额"].ToString().Trim()));
                }

                tradeRecord.StockHolderCode = row["股东代码"].ToString().Trim();

                tradeRecord.ContractNo = row["委托编号"].ToString().Trim();

                tradeRecord.Commission = decimal.Parse(row["手续费"].ToString().Trim());

                tradeRecord.StampDuty = decimal.Parse(row["印花税"].ToString().Trim());

                tradeRecord.Incidentals = decimal.Parse(row["过户费"].ToString().Trim()) + decimal.Parse(row["交易所清算费"].ToString().Trim());

                tradeRecord.Remarks = row["费用备注"].ToString().Trim();

                tradeRecords.Add(tradeRecord);
            }

            InsertDeliveryRecords(tradeRecords);

            #endregion DataProcess

            return true;
        }

        #endregion 交割单--中信国际（信用）

        #region 交割单--中信证券（普通）

        /// <summary>
        /// 交割单--中信证券（普通）
        /// </summary>
        /// <param name="importOperation"></param>
        /// <param name="importDataTable"></param>
        /// <returns></returns>
        private bool DeliveryImportCITIC_N(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            #region DataFormatCheck

            var TemplateColumnNames = new List<string> { "发生日期", "成交时间", "证券代码", "证券名称", "业务名称", "成交数量", "成交价格", "成交金额", "余额", "清算金额", "手续费", "印花税", "附加费", "资金本次余额", "委托编号", "股东代码", "过户费", "交易所清算费", "资金帐号", "币种", "费用备注", "交易类别" };

            this._dataImportService.DataFormatCheck(TemplateColumnNames, importDataTable);

            #endregion DataFormatCheck

            #region DataProcess

            var tradeRecords = new List<DeliveryRecord>();

            foreach (DataRow row in importDataTable.Rows)
            {
                //过滤无实际成交额或成交量的交易记录
                if (int.Parse(row["成交数量"].ToString().Trim()) == 0 || decimal.Parse(row["成交金额"].ToString().Trim()) == 0) continue;

                var tradeRecord = new DeliveryRecord();

                tradeRecord.SetTradeRecordCommonFields(importOperation);

                var stockCode = CommonHelper.StockCodeZerofill(row["证券代码"].ToString().Trim());

                var stockName = row["证券名称"].ToString().Trim();

                var stockInfo = _stockService.GetStockInfoByCode(stockCode);

                tradeRecord.StockCode = stockInfo.FullCode;

                tradeRecord.StockName = stockName;

                tradeRecord.TradeDate = CommonHelper.StringToDateTime(row["发生日期"].ToString().Trim());

                tradeRecord.TradeTime = row["成交时间"].ToString().Trim();

                if (_buyTexts.Contains(row["业务名称"].ToString().Trim()))
                    tradeRecord.DealFlag = true;
                else if (_sellTexts.Contains(row["业务名称"].ToString().Trim()))
                    tradeRecord.DealFlag = false;
                else
                {
                    //跳过操作类型不明的数据
                    continue;
                }

                tradeRecord.DealNo = string.Empty;

                tradeRecord.DealPrice = decimal.Parse(row["成交价格"].ToString().Trim());

                tradeRecord.DealAmount = decimal.Parse(row["成交金额"].ToString().Trim());

                //买入
                if (tradeRecord.DealFlag)
                {
                    tradeRecord.DealVolume = CommonHelper.ConvertToPositive(int.Parse(row["成交数量"].ToString().Trim()));

                    tradeRecord.ActualAmount = CommonHelper.ConvertToNegtive(decimal.Parse(row["清算金额"].ToString().Trim()));
                }
                //卖出
                else
                {
                    tradeRecord.DealVolume = CommonHelper.ConvertToNegtive(int.Parse(row["成交数量"].ToString().Trim()));

                    tradeRecord.ActualAmount = CommonHelper.ConvertToPositive(decimal.Parse(row["清算金额"].ToString().Trim()));
                }

                tradeRecord.StockHolderCode = row["股东代码"].ToString().Trim();

                tradeRecord.ContractNo = row["委托编号"].ToString().Trim();

                tradeRecord.Commission = decimal.Parse(row["手续费"].ToString().Trim());

                tradeRecord.StampDuty = decimal.Parse(row["印花税"].ToString().Trim());

                tradeRecord.Incidentals = decimal.Parse(row["过户费"].ToString().Trim()) + decimal.Parse(row["交易所清算费"].ToString().Trim()) + decimal.Parse(row["附加费"].ToString().Trim());

                tradeRecord.Remarks = row["费用备注"].ToString().Trim();

                tradeRecords.Add(tradeRecord);
            }

            InsertDeliveryRecords(tradeRecords);

            #endregion DataProcess

            return true;
        }

        #endregion 交割单--中信证券（普通）

        #region 交割单--中银国际（信用）

        /// <summary>
        /// 交割单--中银国际（信用）
        /// </summary>
        /// <param name="importOperation"></param>
        /// <param name="importDataTable"></param>
        /// <returns></returns>
        private bool DeliveryImportBOCI_C(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            #region DataFormatCheck

            var TemplateColumnNames = new List<string> { "发生日期", "成交时间", "证券代码", "证券名称", "买卖标志", "成交价格", "成交数量", "成交金额", "发生金额", "剩余金额", "申报序号", "成交编号", "委托编号", "股东代码", "席位代码", "证券数量", "佣金", "印花税", "过户费", "交易征费", "交易规费", "备注", "交易类别" };

            this._dataImportService.DataFormatCheck(TemplateColumnNames, importDataTable);

            #endregion DataFormatCheck

            #region DataProcess

            var tradeRecords = new List<DeliveryRecord>();

            foreach (DataRow row in importDataTable.Rows)
            {
                //过滤无实际成交额或成交量的交易记录
                if (int.Parse(row["成交数量"].ToString().Trim()) == 0 || decimal.Parse(row["成交金额"].ToString().Trim()) == 0) continue;

                var tradeRecord = new DeliveryRecord();

                tradeRecord.SetTradeRecordCommonFields(importOperation);

                var stockCode = CommonHelper.StockCodeZerofill(row["证券代码"].ToString().Trim());

                var stockName = row["证券名称"].ToString().Trim();

                var stockInfo = _stockService.GetStockInfoByCode(stockCode);

                tradeRecord.StockCode = stockInfo.FullCode;

                tradeRecord.StockName = stockName;

                tradeRecord.TradeDate = CommonHelper.StringToDateTime(row["发生日期"].ToString().Trim());

                tradeRecord.TradeTime = row["成交时间"].ToString().Trim();

                if (_buyTexts.Contains(row["买卖标志"].ToString().Trim()))
                    tradeRecord.DealFlag = true;
                else if (_sellTexts.Contains(row["买卖标志"].ToString().Trim()))
                    tradeRecord.DealFlag = false;
                else
                {
                    //跳过操作类型不明的数据
                    continue;
                }

                tradeRecord.DealNo = row["成交编号"].ToString().Trim();

                tradeRecord.DealPrice = decimal.Parse(row["成交价格"].ToString().Trim());

                tradeRecord.DealAmount = decimal.Parse(row["成交金额"].ToString().Trim());

                //买入
                if (tradeRecord.DealFlag)
                {
                    tradeRecord.DealVolume = CommonHelper.ConvertToPositive(int.Parse(row["成交数量"].ToString().Trim()));

                    tradeRecord.ActualAmount = CommonHelper.ConvertToNegtive(decimal.Parse(row["发生金额"].ToString().Trim()));
                }
                //卖出
                else
                {
                    tradeRecord.DealVolume = CommonHelper.ConvertToNegtive(int.Parse(row["成交数量"].ToString().Trim()));

                    tradeRecord.ActualAmount = CommonHelper.ConvertToPositive(decimal.Parse(row["发生金额"].ToString().Trim()));
                }

                tradeRecord.StockHolderCode = row["股东代码"].ToString().Trim();

                tradeRecord.ContractNo = row["委托编号"].ToString().Trim();

                tradeRecord.Commission = decimal.Parse(row["佣金"].ToString().Trim());

                tradeRecord.StampDuty = decimal.Parse(row["印花税"].ToString().Trim());

                tradeRecord.Incidentals = decimal.Parse(row["过户费"].ToString().Trim()) + decimal.Parse(row["交易征费"].ToString().Trim()) + decimal.Parse(row["交易规费"].ToString().Trim());

                tradeRecord.Remarks = row["备注"].ToString().Trim();

                tradeRecords.Add(tradeRecord);
            }

            InsertDeliveryRecords(tradeRecords);

            #endregion DataProcess

            return true;
        }

        #endregion 交割单--中银国际（信用）

        #region 交割单--中银国际（普通）

        /// <summary>
        /// 交割单--中银国际（普通）
        /// </summary>
        /// <param name="importOperation"></param>
        /// <param name="importDataTable"></param>
        /// <returns></returns>
        private bool DeliveryImportBOCI_N(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            #region DataFormatCheck

            var TemplateColumnNames = new List<string> { "发生日期", "成交时间", "证券代码", "证券名称", "买卖标志", "成交价格", "成交数量", "成交金额", "发生金额", "剩余金额", "申报序号", "成交编号", "委托编号", "股东代码", "席位代码", "证券数量", "佣金", "印花税", "过户费", "交易征费", "交易规费", "备注", "交易类别" };

            this._dataImportService.DataFormatCheck(TemplateColumnNames, importDataTable);

            #endregion DataFormatCheck

            #region DataProcess

            var tradeRecords = new List<DeliveryRecord>();

            foreach (DataRow row in importDataTable.Rows)
            {
                //过滤无实际成交额或成交量的交易记录
                if (int.Parse(row["成交数量"].ToString().Trim()) == 0 || decimal.Parse(row["成交金额"].ToString().Trim()) == 0) continue;

                var tradeRecord = new DeliveryRecord();

                tradeRecord.SetTradeRecordCommonFields(importOperation);

                var stockCode = CommonHelper.StockCodeZerofill(row["证券代码"].ToString().Trim());

                var stockName = row["证券名称"].ToString().Trim();

                var stockInfo = _stockService.GetStockInfoByCode(stockCode);

                tradeRecord.StockCode = stockInfo.FullCode;

                tradeRecord.StockName = stockName;

                tradeRecord.TradeDate = CommonHelper.StringToDateTime(row["发生日期"].ToString().Trim());

                tradeRecord.TradeTime = row["成交时间"].ToString().Trim();

                if (_buyTexts.Contains(row["买卖标志"].ToString().Trim()))
                    tradeRecord.DealFlag = true;
                else if (_sellTexts.Contains(row["买卖标志"].ToString().Trim()))
                    tradeRecord.DealFlag = false;
                else
                {
                    //跳过操作类型不明的数据
                    continue;
                }

                tradeRecord.DealNo = row["成交编号"].ToString().Trim();

                tradeRecord.DealPrice = decimal.Parse(row["成交价格"].ToString().Trim());

                tradeRecord.DealAmount = decimal.Parse(row["成交金额"].ToString().Trim());

                //买入
                if (tradeRecord.DealFlag)
                {
                    tradeRecord.DealVolume = CommonHelper.ConvertToPositive(int.Parse(row["成交数量"].ToString().Trim()));

                    tradeRecord.ActualAmount = CommonHelper.ConvertToNegtive(decimal.Parse(row["发生金额"].ToString().Trim()));
                }
                //卖出
                else
                {
                    tradeRecord.DealVolume = CommonHelper.ConvertToNegtive(int.Parse(row["成交数量"].ToString().Trim()));

                    tradeRecord.ActualAmount = CommonHelper.ConvertToPositive(decimal.Parse(row["发生金额"].ToString().Trim()));
                }

                tradeRecord.StockHolderCode = row["股东代码"].ToString().Trim();

                tradeRecord.ContractNo = row["委托编号"].ToString().Trim();

                tradeRecord.Commission = decimal.Parse(row["佣金"].ToString().Trim());

                tradeRecord.StampDuty = decimal.Parse(row["印花税"].ToString().Trim());

                tradeRecord.Incidentals = decimal.Parse(row["过户费"].ToString().Trim()) + decimal.Parse(row["交易征费"].ToString().Trim()) + decimal.Parse(row["交易规费"].ToString().Trim());

                tradeRecord.Remarks = row["备注"].ToString().Trim();

                tradeRecords.Add(tradeRecord);
            }

            InsertDeliveryRecords(tradeRecords);

            #endregion DataProcess

            return true;
        }

        #endregion 交割单--中银国际（普通）

        #endregion Delivery Data Import

        #endregion Utilities

        #region Methods

        public void BatchInsertDeliveryRecords(IList<DeliveryRecord> deliveryRecords)
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

        public virtual bool DataImportProcess(EnumLibrary.SecurityAccount securityAccount, DataTable importDataTable, RecordImportOperationEntity importOperation)
        {
            bool result = false;
            switch (securityAccount)
            {
                //财通信用
                case EnumLibrary.SecurityAccount.CaiTong_C:
                    result = DeliveryImportCaiTong_C(importOperation, importDataTable);
                    break;

                //财通普通
                case EnumLibrary.SecurityAccount.CaiTong_N:
                    result = DeliveryImportCaiTong_N(importOperation, importDataTable);
                    break;

                //方正普通
                case EnumLibrary.SecurityAccount.Founder_N:
                    result = DeliveryImportFounder_N(importOperation, importDataTable);
                    break;

                //国金普通
                case EnumLibrary.SecurityAccount.SinoLink_N:
                    result = DeliveryImportSinoLink_N(importOperation, importDataTable);
                    break;

                //国泰信用
                case EnumLibrary.SecurityAccount.GuoTai_C:
                    result = DeliveryImportGuoTai_C(importOperation, importDataTable);
                    break;

                //国泰普通
                case EnumLibrary.SecurityAccount.GuoTai_N:
                    result = DeliveryImportGuoTai_N(importOperation, importDataTable);
                    break;

                //华泰信用
                case EnumLibrary.SecurityAccount.HuaTai_C:
                    result = DeliveryImportHuaTai_C(importOperation, importDataTable);
                    break;

                //华泰普通
                case EnumLibrary.SecurityAccount.HuaTai_N:
                    result = DeliveryImportHuaTai_N(importOperation, importDataTable);
                    break;

                //申万普通
                case EnumLibrary.SecurityAccount.ShenWan_N:
                    result = DeliveryImportShenWan_N(importOperation, importDataTable);
                    break;

                //银河普通
                case EnumLibrary.SecurityAccount.Galaxy_N:
                    result = DeliveryImportGalaxy_N(importOperation, importDataTable);
                    break;

                //招商普通
                case EnumLibrary.SecurityAccount.ZhaoShang_N:
                    result = DeliveryImportZhaoShang_N(importOperation, importDataTable);
                    break;

                //浙商普通
                case EnumLibrary.SecurityAccount.ZheShang_N:
                    result = DeliveryImportZheShang_N(importOperation, importDataTable);
                    break;

                //中信信用
                case EnumLibrary.SecurityAccount.CITIC_C:
                    result = DeliveryImportCITIC_C(importOperation, importDataTable);
                    break;

                //中信普通
                case EnumLibrary.SecurityAccount.CITIC_N:
                    result = DeliveryImportCITIC_N(importOperation, importDataTable);
                    break;

                //中银信用
                case EnumLibrary.SecurityAccount.BOCI_C:
                    result = DeliveryImportBOCI_C(importOperation, importDataTable);
                    break;

                //中银普通
                case EnumLibrary.SecurityAccount.BOCI_N:
                    result = DeliveryImportBOCI_N(importOperation, importDataTable);
                    break;
            }
            return result;
        }

        #endregion Methods
    }
}