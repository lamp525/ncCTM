using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using CTM.Core;
using CTM.Core.Data;
using CTM.Core.Domain.Account;
using CTM.Core.Domain.Stock;
using CTM.Core.Domain.TradeRecord;
using CTM.Core.Domain.User;
using CTM.Core.Util;
using CTM.Services.Account;
using CTM.Services.Stock;

namespace CTM.Services.TradeRecord
{
    public partial class DailyRecordService : IDailyRecordService
    {
        #region Fields

        private readonly IRepository<DailyRecord> _dailyRepository;
        private readonly IRepository<UserInfo> _userRepository;
        private readonly IRepository<AccountInfo> _accountRepository;

        private readonly IDataImportCommonService _dataImportService;
        private readonly IStockService _stockService;
        private readonly IAccountService _accountService;

        #endregion Fields

        #region Constants

        private readonly List<string> _buyTexts = new List<string> { "买入", "证券买入", "融券回购", "普通买入", "担保物买入", "融资买入", "融资借入" };
        private readonly List<string> _sellTexts = new List<string> { "卖出", "证券卖出", "融券购回", "普通卖出", "担保物卖出", "融资卖出", "卖券还款" };

        #endregion Constants

        #region Constructors

        public DailyRecordService(
            IRepository<DailyRecord> dailyRepository,
            IRepository<UserInfo> userRepository,
            IRepository<AccountInfo> accountRepository,
            IDataImportCommonService DICService,
            IStockService stockService,
            IAccountService accountService
            )
        {
            this._dailyRepository = dailyRepository;
            this._userRepository = userRepository;
            this._accountRepository = accountRepository;
            this._dataImportService = DICService;
            this._stockService = stockService;
            this._accountService = accountService;
        }

        #endregion Constructors

        #region Utilities

        /// <summary>
        /// 校验股票信息
        /// </summary>
        /// <param name="stockInfo"></param>
        /// <param name="stockCode"></param>
        /// <param name="stockName"></param>
        /// <returns></returns>
        private void VerifyStockInfo(StockInfo stockInfo, string stockCode, string stockName)
        {
            if (stockInfo == null)
                throw new Exception($"系统不存在【代码：{stockCode}】【名称：{stockName}】的股票信息！");
        }

        /// <summary>
        /// 导入数据验证
        /// </summary>
        /// <param name="importDataTable"></param>
        /// <param name="importOperation"></param>
        private void ImportRecordsCheck(DataTable importDataTable, RecordImportOperationEntity importOperation)
        {
            //判断短差部的日内交易股票是否设置了融券信息
            //var operatorInfo = this.luOperator.GetSelectedDataRow() as UserInfo;
            //if (operatorInfo.DepartmentId == 2)
            //{
            //    var today = _commonService.GetCurrentServerTime().Date;
            //    var loanInfos = _securitiesLoanService.GetSecuritiesLoanInfos(loanDate: today, operatorCode: operatorInfo.Code);

            //    var dayRecords = _importDataTable.AsEnumerable().Where(x => x.Field<string>("交易类别").Trim() == "日内");
            //}

            //判断是否需要设置波段收益人
            var bandRecordCount = importDataTable.AsEnumerable().Count(x => x.Field<string>("交易类别").Trim() == "波段");
            if (bandRecordCount > 0 && string.IsNullOrEmpty(importOperation.BandPrincipal))
            {
                throw new Exception("请设置波段交易记录对应的波段受益人！");
            }

            //判断是否需要设置目标收益人
            var targetRecordCount = importDataTable.AsEnumerable().Count(x => x.Field<string>("交易类别").Trim() == "目标");
            if (targetRecordCount > 0 && string.IsNullOrEmpty(importOperation.TargetPrincipal))
            {
                throw new Exception("请设置目标交易记录对应的目标受益人！");
            }
        }

        #region 数据导入

        #region 当日委托数据导入

        #region 当日委托--券商选择

        /// <summary>
        /// 当日委托--券商选择
        /// </summary>
        /// <param name="securityAccount"></param>
        /// <param name="importDataTable"></param>
        /// <param name="importOperation"></param>
        /// <returns></returns>
        private IList<DailyRecord> EntrustImport(EnumLibrary.SecurityAccount securityAccount, DataTable importDataTable, RecordImportOperationEntity importOperation)
        {
            IList<DailyRecord> result = new List<DailyRecord>();

            switch (securityAccount)
            {
                //财通信用
                case EnumLibrary.SecurityAccount.CaiTong_C:
                    result = EntrustImportCaiTong_C(importOperation, importDataTable);
                    break;

                //财通普通
                case EnumLibrary.SecurityAccount.CaiTong_N:
                    result = EntrustImportCaiTong_N(importOperation, importDataTable);
                    break;

                //方正普通
                case EnumLibrary.SecurityAccount.Founder_N:
                    result = EntrustImportFounder_N(importOperation, importDataTable);
                    break;

                // 银河普通
                case EnumLibrary.SecurityAccount.Galaxy_N:
                    result = EntrustImportGalaxy_N(importOperation, importDataTable);
                    break;

                //国泰普通
                case EnumLibrary.SecurityAccount.GuoTai_N:
                    result = EntrustImportGuoTai_N(importOperation, importDataTable);
                    break;

                //国泰信用
                case EnumLibrary.SecurityAccount.GuoTai_C:
                    result = EntrustImportGuoTai_C(importOperation, importDataTable);
                    break;

                //华泰普通
                case EnumLibrary.SecurityAccount.HuaTai_N:
                    result = EntrustImportHuaTai_N(importOperation, importDataTable);
                    break;

                //华泰信用
                case EnumLibrary.SecurityAccount.HuaTai_C:
                    result = EntrustImportHuaTai_C(importOperation, importDataTable);
                    break;

                //申万普通
                case EnumLibrary.SecurityAccount.ShenWan_N:
                    result = EntrustImportShenWan_N(importOperation, importDataTable);
                    break;

                //浙商普通
                case EnumLibrary.SecurityAccount.ZheShang_N:
                    result = EntrustImportZheShang_N(importOperation, importDataTable);
                    break;

                //中信普通
                case EnumLibrary.SecurityAccount.CITIC_N:
                    result = EntrustImportCITIC_N(importOperation, importDataTable);
                    break;

                //中信信用
                case EnumLibrary.SecurityAccount.CITIC_C:
                    result = EntrustImportCITIC_C(importOperation, importDataTable);
                    break;

                ////中银信用
                //case EnumLibrary.SecurityAccount.BOCI_C:
                //    result = EntrustImportBOCI_C(importOperation, importDataTable);
                //    break;

                //中银普通
                case EnumLibrary.SecurityAccount.BOCI_N:
                    result = EntrustImportBOCI_N(importOperation, importDataTable);
                    break;
            }
            return result;
        }

        #endregion 当日委托--券商选择

        #region 当日委托--财通证券（信用）

        /// <summary>
        /// 当日委托--财通证券（信用）
        /// </summary>
        /// <param name="importOperation"></param>
        /// <param name="importDataTable"></param>
        /// <returns></returns>
        private IList<DailyRecord> EntrustImportCaiTong_C(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            #region DataFormatCheck

            var TemplateColumnNames = new List<string> { "委托时间", "证券代码", "证券名称", "操作", "备注", "委托价格", "委托数量", "成交均价", "成交数量", "股东帐户", "合同编号", "交易市场", "订单类型", "交易类别" };

            this._dataImportService.DataFormatCheck(TemplateColumnNames, importDataTable);

            #endregion DataFormatCheck

            #region DataProcess

            var tradeRecords = new List<DailyRecord>();

            foreach (DataRow row in importDataTable.Rows)
            {
                //忽略成交数量为0的数据
                if (string.IsNullOrEmpty(row["成交数量"].ToString().Trim()) || int.Parse(row["成交数量"].ToString().Trim()) == 0) continue;

                //成交数量
                var dealVolume = int.Parse(row["成交数量"].ToString().Trim());

                var tradeRecord = new DailyRecord();

                tradeRecord.SetTradeRecordCommonFields(importOperation);

                var stockCode = CommonHelper.StockCodeZerofill(row["证券代码"].ToString().Trim());

                var stockName = row["证券名称"].ToString().Trim();

                var stockInfo = _stockService.GetStockInfoByCode(stockCode);

                VerifyStockInfo(stockInfo, stockCode, stockName);

                tradeRecord.StockCode = stockInfo.FullCode;

                tradeRecord.StockName = stockName;

                var stockPoolInfo = _stockService.GetStockPoolInfoByStockId(stockInfo.Id);

                var tradeType = row["交易类别"].ToString().Trim();
                tradeRecord.SetTradeType(tradeType);

                tradeRecord.SetBeneficiary(importOperation.BandPrincipal, importOperation.TargetPrincipal);

                tradeRecord.TradeTime = row["委托时间"].ToString().Trim();

                tradeRecord.DealFlag = row["操作"].ToString().Trim().IndexOf("买入") > -1 ? true : false;

                tradeRecord.DealNo = string.Empty;

                tradeRecord.DealPrice = decimal.Parse(row["成交均价"].ToString().Trim());

                tradeRecord.DealAmount = dealVolume * tradeRecord.DealPrice;

                tradeRecord.StockHolderCode = row["股东帐户"].ToString().Trim();

                tradeRecord.ContractNo = row["合同编号"].ToString().Trim();

                var accountInfo = _accountService.GetAccountInfoById(tradeRecord.AccountId);

                tradeRecord.Commission = tradeRecord.DealAmount * accountInfo.CommissionRate;

                tradeRecord.Incidentals = 0;

                //买入
                if (tradeRecord.DealFlag)
                {
                    tradeRecord.DealVolume = CommonHelper.ConvertToPositive(int.Parse(row["成交数量"].ToString().Trim()));
                    tradeRecord.StampDuty = 0;
                    tradeRecord.ActualAmount = CommonHelper.ConvertToNegtive(tradeRecord.DealAmount + tradeRecord.Commission + tradeRecord.Incidentals);
                }
                //卖出
                else
                {
                    tradeRecord.DealVolume = CommonHelper.ConvertToNegtive(int.Parse(row["成交数量"].ToString().Trim()));
                    tradeRecord.StampDuty = tradeRecord.DealAmount * accountInfo.StampDutyRate;
                    tradeRecord.ActualAmount = CommonHelper.ConvertToPositive(tradeRecord.DealAmount - tradeRecord.StampDuty - tradeRecord.Commission - tradeRecord.Incidentals);
                }

                tradeRecord.Remarks = row["操作"].ToString().Trim();

                tradeRecords.Add(tradeRecord);
            }

            #endregion DataProcess

            return tradeRecords;
        }

        #endregion 当日委托--财通证券（信用）

        #region 当日委托--财通证券（普通）

        /// <summary>
        /// 当日委托--财通证券（普通）
        /// </summary>
        /// <param name="importOperation"></param>
        /// <param name="importDataTable"></param>
        /// <returns></returns>
        private IList<DailyRecord> EntrustImportCaiTong_N(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            #region DataFormatCheck

            var TemplateColumnNames = new List<string> { "委托时间", "申报编号", "证券代码", "证券名称", "操作", "备注", "委托价格", "委托数量", "成交均价", "成交数量", "撤单数量", "股东帐户", "合同编号", "交易市场", "交易类别" };

            this._dataImportService.DataFormatCheck(TemplateColumnNames, importDataTable);

            #endregion DataFormatCheck

            #region DataProcess

            var tradeRecords = new List<DailyRecord>();

            foreach (DataRow row in importDataTable.Rows)
            {
                //忽略成交数量为0的数据
                if (string.IsNullOrEmpty(row["成交数量"].ToString().Trim()) || int.Parse(row["成交数量"].ToString().Trim()) == 0) continue;

                //成交数量
                var dealVolume = int.Parse(row["成交数量"].ToString().Trim());

                var tradeRecord = new DailyRecord();

                tradeRecord.SetTradeRecordCommonFields(importOperation);

                var stockCode = CommonHelper.StockCodeZerofill(row["证券代码"].ToString().Trim());

                var stockName = row["证券名称"].ToString().Trim();

                var stockInfo = _stockService.GetStockInfoByCode(stockCode);

                VerifyStockInfo(stockInfo, stockCode, stockName);

                tradeRecord.StockCode = stockInfo.FullCode;

                tradeRecord.StockName = stockName;

                var stockPoolInfo = _stockService.GetStockPoolInfoByStockId(stockInfo.Id);

                var tradeType = row["交易类别"].ToString().Trim();
                tradeRecord.SetTradeType(tradeType);

                tradeRecord.SetBeneficiary(importOperation.BandPrincipal, importOperation.TargetPrincipal);

                tradeRecord.TradeTime = row["委托时间"].ToString().Trim();

                tradeRecord.DealFlag = row["操作"].ToString().Trim().IndexOf("买入") > -1 ? true : false;

                tradeRecord.DealNo = string.Empty;

                tradeRecord.DealPrice = decimal.Parse(row["成交均价"].ToString().Trim());

                tradeRecord.DealAmount = dealVolume * tradeRecord.DealPrice;

                tradeRecord.StockHolderCode = row["股东帐户"].ToString().Trim();

                tradeRecord.ContractNo = row["合同编号"].ToString().Trim();

                var accountInfo = _accountService.GetAccountInfoById(tradeRecord.AccountId);

                tradeRecord.Commission = tradeRecord.DealAmount * accountInfo.CommissionRate;

                tradeRecord.Incidentals = 0;

                //买入
                if (tradeRecord.DealFlag)
                {
                    tradeRecord.DealVolume = CommonHelper.ConvertToPositive(int.Parse(row["成交数量"].ToString().Trim()));
                    tradeRecord.StampDuty = 0;
                    tradeRecord.ActualAmount = CommonHelper.ConvertToNegtive(tradeRecord.DealAmount + tradeRecord.Commission + tradeRecord.Incidentals);
                }
                //卖出
                else
                {
                    tradeRecord.DealVolume = CommonHelper.ConvertToNegtive(int.Parse(row["成交数量"].ToString().Trim()));
                    tradeRecord.StampDuty = tradeRecord.DealAmount * accountInfo.StampDutyRate;
                    tradeRecord.ActualAmount = CommonHelper.ConvertToPositive(tradeRecord.DealAmount - tradeRecord.StampDuty - tradeRecord.Commission - tradeRecord.Incidentals);
                }

                tradeRecord.Remarks = row["操作"].ToString().Trim();

                tradeRecords.Add(tradeRecord);
            }

            #endregion DataProcess

            return tradeRecords;
        }

        #endregion 当日委托--财通证券（普通）

        #region 当日委托--方正证券（普通）

        /// <summary>
        /// 当日委托--方正证券（普通）
        /// </summary>
        /// <param name="importOperation"></param>
        /// <param name="importDataTable"></param>
        /// <returns></returns>
        private IList<DailyRecord> EntrustImportFounder_N(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            #region DataFormatCheck

            var TemplateColumnNames = new List<string> { "委托时间", "证券代码", "证券名称", "操作", "备注", "委托数量", "成交数量", "撤消数量", "委托价格", "成交均价", "合同编号", "申报编号", "交易类别" };

            this._dataImportService.DataFormatCheck(TemplateColumnNames, importDataTable);

            #endregion DataFormatCheck

            #region DataProcess

            var tradeRecords = new List<DailyRecord>();

            foreach (DataRow row in importDataTable.Rows)
            {
                //忽略成交数量为0的数据
                if (string.IsNullOrEmpty(row["成交数量"].ToString().Trim()) || int.Parse(row["成交数量"].ToString().Trim()) == 0) continue;

                //成交数量
                var dealVolume = int.Parse(row["成交数量"].ToString().Trim());

                var tradeRecord = new DailyRecord();

                tradeRecord.SetTradeRecordCommonFields(importOperation);

                var stockCode = CommonHelper.StockCodeZerofill(row["证券代码"].ToString().Trim());

                var stockName = row["证券名称"].ToString().Trim();

                var stockInfo = _stockService.GetStockInfoByCode(stockCode);

                VerifyStockInfo(stockInfo, stockCode, stockName);

                tradeRecord.StockCode = stockInfo.FullCode;

                tradeRecord.StockName = stockName;

                var stockPoolInfo = _stockService.GetStockPoolInfoByStockId(stockInfo.Id);

                var tradeType = row["交易类别"].ToString().Trim();
                tradeRecord.SetTradeType(tradeType);

                tradeRecord.SetBeneficiary(importOperation.BandPrincipal, importOperation.TargetPrincipal);

                tradeRecord.TradeTime = row["委托时间"].ToString().Trim();

                tradeRecord.DealFlag = row["操作"].ToString().Trim().IndexOf("买入") > -1 ? true : false;

                tradeRecord.DealNo = string.Empty;

                tradeRecord.DealPrice = decimal.Parse(row["成交均价"].ToString().Trim());

                tradeRecord.DealAmount = dealVolume * tradeRecord.DealPrice;

                tradeRecord.StockHolderCode = string.Empty;

                tradeRecord.ContractNo = row["合同编号"].ToString().Trim();

                var accountInfo = _accountService.GetAccountInfoById(tradeRecord.AccountId);

                tradeRecord.Commission = tradeRecord.DealAmount * accountInfo.CommissionRate;

                tradeRecord.Incidentals = 0;

                //买入
                if (tradeRecord.DealFlag)
                {
                    tradeRecord.DealVolume = CommonHelper.ConvertToPositive(int.Parse(row["成交数量"].ToString().Trim()));
                    tradeRecord.StampDuty = 0;
                    tradeRecord.ActualAmount = CommonHelper.ConvertToNegtive(tradeRecord.DealAmount + tradeRecord.Commission + tradeRecord.Incidentals);
                }
                //卖出
                else
                {
                    tradeRecord.DealVolume = CommonHelper.ConvertToNegtive(int.Parse(row["成交数量"].ToString().Trim()));
                    tradeRecord.StampDuty = tradeRecord.DealAmount * accountInfo.StampDutyRate;
                    tradeRecord.ActualAmount = CommonHelper.ConvertToPositive(tradeRecord.DealAmount - tradeRecord.StampDuty - tradeRecord.Commission - tradeRecord.Incidentals);
                }

                tradeRecord.Remarks = row["操作"].ToString().Trim();

                tradeRecords.Add(tradeRecord);
            }

            #endregion DataProcess

            return tradeRecords;
        }

        #endregion 当日委托--方正证券（普通）

        #region 当日委托--方正证券（普通）

        /// <summary>
        /// 当日委托--银河证券（普通）
        /// </summary>
        /// <param name="importOperation"></param>
        /// <param name="importDataTable"></param>
        /// <returns></returns>
        private IList<DailyRecord> EntrustImportGalaxy_N(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            #region DataFormatCheck

            var TemplateColumnNames = new List<string> { "委托时间", "证券代码", "证券名称", "操作", "备注", "委托数量", "成交数量", "成交金额", "委托价格", "成交均价", "合同编号", "操作日期", "交易类别" };

            this._dataImportService.DataFormatCheck(TemplateColumnNames, importDataTable);

            #endregion DataFormatCheck

            #region DataProcess

            var tradeRecords = new List<DailyRecord>();

            foreach (DataRow row in importDataTable.Rows)
            {
                //忽略成交数量为0的数据
                if (string.IsNullOrEmpty(row["成交数量"].ToString().Trim()) || int.Parse(row["成交数量"].ToString().Trim()) == 0) continue;

                //成交数量
                var dealVolume = int.Parse(row["成交数量"].ToString().Trim());

                var tradeRecord = new DailyRecord();

                tradeRecord.SetTradeRecordCommonFields(importOperation);

                var stockCode = CommonHelper.StockCodeZerofill(row["证券代码"].ToString().Trim());

                var stockName = row["证券名称"].ToString().Trim();

                var stockInfo = _stockService.GetStockInfoByCode(stockCode);

                VerifyStockInfo(stockInfo, stockCode, stockName);

                tradeRecord.StockCode = stockInfo.FullCode;

                tradeRecord.StockName = stockName;

                var stockPoolInfo = _stockService.GetStockPoolInfoByStockId(stockInfo.Id);

                var tradeType = row["交易类别"].ToString().Trim();
                tradeRecord.SetTradeType(tradeType);

                tradeRecord.SetBeneficiary(importOperation.BandPrincipal, importOperation.TargetPrincipal);

                tradeRecord.TradeTime = row["委托时间"].ToString().Trim();

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

                tradeRecord.StockHolderCode = string.Empty;

                tradeRecord.ContractNo = row["合同编号"].ToString().Trim();

                var accountInfo = _accountService.GetAccountInfoById(tradeRecord.AccountId);

                tradeRecord.Commission = tradeRecord.DealAmount * accountInfo.CommissionRate;

                tradeRecord.Incidentals = 0;

                //买入
                if (tradeRecord.DealFlag)
                {
                    tradeRecord.DealVolume = CommonHelper.ConvertToPositive(int.Parse(row["成交数量"].ToString().Trim()));
                    tradeRecord.StampDuty = 0;
                    tradeRecord.ActualAmount = CommonHelper.ConvertToNegtive(tradeRecord.DealAmount + tradeRecord.Commission + tradeRecord.Incidentals);
                }
                //卖出
                else
                {
                    tradeRecord.DealVolume = CommonHelper.ConvertToNegtive(int.Parse(row["成交数量"].ToString().Trim()));
                    tradeRecord.StampDuty = tradeRecord.DealAmount * accountInfo.StampDutyRate;
                    tradeRecord.ActualAmount = CommonHelper.ConvertToPositive(tradeRecord.DealAmount - tradeRecord.StampDuty - tradeRecord.Commission - tradeRecord.Incidentals);
                }

                tradeRecord.Remarks = row["操作"].ToString().Trim();

                tradeRecords.Add(tradeRecord);
            }

            #endregion DataProcess

            return tradeRecords;
        }

        #endregion 当日委托--方正证券（普通）

        #region 当日委托--国泰证券（普通）

        /// <summary>
        /// 当日委托--国泰证券（普通）
        /// </summary>
        /// <param name="importOperation"></param>
        /// <param name="importDataTable"></param>
        /// <returns></returns>
        private IList<DailyRecord> EntrustImportGuoTai_N(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            #region DataFormatCheck

            var TemplateColumnNames = new List<string> { "委托时间", "证券代码", "证券名称", "操作", "备注", "委托数量", "成交数量", "成交金额", "委托价格", "成交均价", "合同编号", "交易类别" };

            this._dataImportService.DataFormatCheck(TemplateColumnNames, importDataTable);

            #endregion DataFormatCheck

            #region DataProcess

            var tradeRecords = new List<DailyRecord>();

            foreach (DataRow row in importDataTable.Rows)
            {
                //忽略成交数量为0的数据
                if (string.IsNullOrEmpty(row["成交数量"].ToString().Trim()) || int.Parse(row["成交数量"].ToString().Trim()) == 0) continue;

                //成交数量
                var dealVolume = int.Parse(row["成交数量"].ToString().Trim());

                var tradeRecord = new DailyRecord();

                tradeRecord.SetTradeRecordCommonFields(importOperation);

                var stockCode = CommonHelper.StockCodeZerofill(row["证券代码"].ToString().Trim());

                var stockName = row["证券名称"].ToString().Trim();

                var stockInfo = _stockService.GetStockInfoByCode(stockCode);

                VerifyStockInfo(stockInfo, stockCode, stockName);

                tradeRecord.StockCode = stockInfo.FullCode;

                tradeRecord.StockName = stockName;

                var stockPoolInfo = _stockService.GetStockPoolInfoByStockId(stockInfo.Id);

                var tradeType = row["交易类别"].ToString().Trim();
                tradeRecord.SetTradeType(tradeType);

                tradeRecord.SetBeneficiary(importOperation.BandPrincipal, importOperation.TargetPrincipal);

                tradeRecord.TradeTime = row["委托时间"].ToString().Trim();

                tradeRecord.DealFlag = row["操作"].ToString().Trim().IndexOf("买入") > -1 ? true : false;

                tradeRecord.DealNo = string.Empty;

                tradeRecord.DealPrice = decimal.Parse(row["成交均价"].ToString().Trim());

                tradeRecord.DealAmount = dealVolume * tradeRecord.DealPrice;

                tradeRecord.StockHolderCode = string.Empty;

                tradeRecord.ContractNo = row["合同编号"].ToString().Trim();

                var accountInfo = _accountService.GetAccountInfoById(tradeRecord.AccountId);

                tradeRecord.Commission = tradeRecord.DealAmount * accountInfo.CommissionRate;

                tradeRecord.Incidentals = 0;

                //买入
                if (tradeRecord.DealFlag)
                {
                    tradeRecord.DealVolume = CommonHelper.ConvertToPositive(int.Parse(row["成交数量"].ToString().Trim()));
                    tradeRecord.StampDuty = 0;
                    tradeRecord.ActualAmount = CommonHelper.ConvertToNegtive(tradeRecord.DealAmount + tradeRecord.Commission + tradeRecord.Incidentals);
                }
                //卖出
                else
                {
                    tradeRecord.DealVolume = CommonHelper.ConvertToNegtive(int.Parse(row["成交数量"].ToString().Trim()));
                    tradeRecord.StampDuty = tradeRecord.DealAmount * accountInfo.StampDutyRate;
                    tradeRecord.ActualAmount = CommonHelper.ConvertToPositive(tradeRecord.DealAmount - tradeRecord.StampDuty - tradeRecord.Commission - tradeRecord.Incidentals);
                }

                tradeRecord.Remarks = row["操作"].ToString().Trim();

                tradeRecords.Add(tradeRecord);
            }

            #endregion DataProcess

            return tradeRecords;
        }

        #endregion 当日委托--国泰证券（普通）

        #region 当日委托--国泰证券（信用）

        /// <summary>
        /// 当日委托--国泰证券（信用）
        /// </summary>
        /// <param name="importOperation"></param>
        /// <param name="importDataTable"></param>
        /// <returns></returns>
        private IList<DailyRecord> EntrustImportGuoTai_C(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            #region DataFormatCheck

            var TemplateColumnNames = new List<string> { "委托日期", "委托时间", "证券代码", "证券名称", "买卖标志", "委托价格", "委托数量", "委托编号", "成交数量", "撤单数量", "状态说明", "撤单标志", "股东代码", "操作日期", "交易类别" };

            this._dataImportService.DataFormatCheck(TemplateColumnNames, importDataTable);

            #endregion DataFormatCheck

            #region DataProcess

            var tradeRecords = new List<DailyRecord>();

            foreach (DataRow row in importDataTable.Rows)
            {
                //忽略成交数量为0的数据
                if (string.IsNullOrEmpty(row["成交数量"].ToString().Trim()) || int.Parse(row["成交数量"].ToString().Trim()) == 0) continue;

                //成交数量
                var dealVolume = int.Parse(row["成交数量"].ToString().Trim());

                var tradeRecord = new DailyRecord();

                tradeRecord.SetTradeRecordCommonFields(importOperation);

                var stockCode = CommonHelper.StockCodeZerofill(row["证券代码"].ToString().Trim());

                var stockName = row["证券名称"].ToString().Trim();

                var stockInfo = _stockService.GetStockInfoByCode(stockCode);

                VerifyStockInfo(stockInfo, stockCode, stockName);

                tradeRecord.StockCode = stockInfo.FullCode;

                tradeRecord.StockName = stockName;

                var stockPoolInfo = _stockService.GetStockPoolInfoByStockId(stockInfo.Id);

                var tradeType = row["交易类别"].ToString().Trim();
                tradeRecord.SetTradeType(tradeType);

                tradeRecord.SetBeneficiary(importOperation.BandPrincipal, importOperation.TargetPrincipal);

                tradeRecord.TradeTime = row["委托时间"].ToString().Trim();

                if (_buyTexts.Contains(row["买卖标志"].ToString().Trim()))
                    tradeRecord.DealFlag = true;
                else if (_sellTexts.Contains(row["买卖标志"].ToString().Trim()))
                    tradeRecord.DealFlag = false;
                else
                {
                    //跳过操作类型不明的数据
                    continue;
                }

                tradeRecord.DealNo = row["委托编号"].ToString().Trim();

                tradeRecord.DealPrice = decimal.Parse(row["委托价格"].ToString().Trim());

                tradeRecord.DealAmount = dealVolume * tradeRecord.DealPrice;

                tradeRecord.StockHolderCode = row["股东代码"].ToString().Trim();

                tradeRecord.ContractNo = row["委托编号"].ToString().Trim();

                var accountInfo = _accountService.GetAccountInfoById(tradeRecord.AccountId);

                tradeRecord.Commission = tradeRecord.DealAmount * accountInfo.CommissionRate;

                tradeRecord.Incidentals = 0;

                //买入
                if (tradeRecord.DealFlag)
                {
                    tradeRecord.DealVolume = CommonHelper.ConvertToPositive(int.Parse(row["成交数量"].ToString().Trim()));
                    tradeRecord.StampDuty = 0;
                    tradeRecord.ActualAmount = CommonHelper.ConvertToNegtive(tradeRecord.DealAmount + tradeRecord.Commission + tradeRecord.Incidentals);
                }
                //卖出
                else
                {
                    tradeRecord.DealVolume = CommonHelper.ConvertToNegtive(int.Parse(row["成交数量"].ToString().Trim()));
                    tradeRecord.StampDuty = tradeRecord.DealAmount * accountInfo.StampDutyRate;
                    tradeRecord.ActualAmount = CommonHelper.ConvertToPositive(tradeRecord.DealAmount - tradeRecord.StampDuty - tradeRecord.Commission - tradeRecord.Incidentals);
                }

                tradeRecord.Remarks = row["买卖标志"].ToString().Trim();

                tradeRecords.Add(tradeRecord);
            }

            #endregion DataProcess

            return tradeRecords;
        }

        #endregion 当日委托--国泰证券（信用）

        #region 当日委托--华泰证券（信用）

        /// <summary>
        /// 当日委托--华泰证券（信用）
        /// </summary>
        /// <param name="importOperation"></param>
        /// <param name="importDataTable"></param>
        /// <returns></returns>
        private IList<DailyRecord> EntrustImportHuaTai_C(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            #region DataFormatCheck

            var TemplateColumnNames = new List<string> { "委托日期", "证券代码", "证券名称", "操作", "委托价格", "委托数量", "委托时间", "成交数量", "成交均价", "合同编号", "交易市场", "股东帐户", "备注", "交易类别" };

            this._dataImportService.DataFormatCheck(TemplateColumnNames, importDataTable);

            #endregion DataFormatCheck

            #region DataProcess

            var tradeRecords = new List<DailyRecord>();

            foreach (DataRow row in importDataTable.Rows)
            {
                //忽略成交数量为0的数据
                if (string.IsNullOrEmpty(row["成交数量"].ToString().Trim()) || int.Parse(row["成交数量"].ToString().Trim()) == 0) continue;

                //成交数量
                var dealVolume = int.Parse(row["成交数量"].ToString().Trim());

                var tradeRecord = new DailyRecord();

                tradeRecord.SetTradeRecordCommonFields(importOperation);

                var stockCode = CommonHelper.StockCodeZerofill(row["证券代码"].ToString().Trim());

                var stockName = row["证券名称"].ToString().Trim();

                var stockInfo = _stockService.GetStockInfoByCode(stockCode);

                VerifyStockInfo(stockInfo, stockCode, stockName);

                tradeRecord.StockCode = stockInfo.FullCode;

                tradeRecord.StockName = stockName;

                var stockPoolInfo = _stockService.GetStockPoolInfoByStockId(stockInfo.Id);

                var tradeType = row["交易类别"].ToString().Trim();
                tradeRecord.SetTradeType(tradeType);

                tradeRecord.SetBeneficiary(importOperation.BandPrincipal, importOperation.TargetPrincipal);

                tradeRecord.TradeTime = row["委托时间"].ToString().Trim();

                tradeRecord.DealFlag = row["操作"].ToString().Trim().IndexOf("买入") > -1 ? true : false;

                tradeRecord.DealNo = string.Empty;

                tradeRecord.DealPrice = decimal.Parse(row["成交均价"].ToString().Trim());

                tradeRecord.DealAmount = dealVolume * tradeRecord.DealPrice;

                tradeRecord.StockHolderCode = row["股东帐户"].ToString().Trim();

                tradeRecord.ContractNo = row["合同编号"].ToString().Trim();

                var accountInfo = _accountService.GetAccountInfoById(tradeRecord.AccountId);

                tradeRecord.Commission = tradeRecord.DealAmount * accountInfo.CommissionRate;

                tradeRecord.Incidentals = 0;

                //买入
                if (tradeRecord.DealFlag)
                {
                    tradeRecord.DealVolume = CommonHelper.ConvertToPositive(int.Parse(row["成交数量"].ToString().Trim()));
                    tradeRecord.StampDuty = 0;
                    tradeRecord.ActualAmount = CommonHelper.ConvertToNegtive(tradeRecord.DealAmount + tradeRecord.Commission + tradeRecord.Incidentals);
                }
                //卖出
                else
                {
                    tradeRecord.DealVolume = CommonHelper.ConvertToNegtive(int.Parse(row["成交数量"].ToString().Trim()));
                    tradeRecord.StampDuty = tradeRecord.DealAmount * accountInfo.StampDutyRate;
                    tradeRecord.ActualAmount = CommonHelper.ConvertToPositive(tradeRecord.DealAmount - tradeRecord.StampDuty - tradeRecord.Commission - tradeRecord.Incidentals);
                }

                tradeRecord.Remarks = row["操作"].ToString().Trim();

                tradeRecords.Add(tradeRecord);
            }

            #endregion DataProcess

            return tradeRecords;
        }

        #endregion 当日委托--华泰证券（信用）

        #region 当日委托--华泰证券（普通）

        /// <summary>
        /// 当日委托--华泰证券（普通）
        /// </summary>
        /// <param name="importOperation"></param>
        /// <param name="importDataTable"></param>
        /// <returns></returns>
        private IList<DailyRecord> EntrustImportHuaTai_N(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            #region DataFormatCheck

            var TemplateColumnNames = new List<string> { "委托时间", "证券代码", "证券名称", "操作", "备注", "委托数量", "成交数量", "委托价格", "成交均价", "合同编号", "交易类别" };

            this._dataImportService.DataFormatCheck(TemplateColumnNames, importDataTable);

            #endregion DataFormatCheck

            #region DataProcess

            var tradeRecords = new List<DailyRecord>();

            foreach (DataRow row in importDataTable.Rows)
            {
                //忽略成交数量为0的数据
                if (string.IsNullOrEmpty(row["成交数量"].ToString().Trim()) || int.Parse(row["成交数量"].ToString().Trim()) == 0) continue;

                //成交数量
                var dealVolume = int.Parse(row["成交数量"].ToString().Trim());

                var tradeRecord = new DailyRecord();

                tradeRecord.SetTradeRecordCommonFields(importOperation);

                var stockCode = CommonHelper.StockCodeZerofill(row["证券代码"].ToString().Trim());

                var stockName = row["证券名称"].ToString().Trim();

                var stockInfo = _stockService.GetStockInfoByCode(stockCode);

                VerifyStockInfo(stockInfo, stockCode, stockName);

                tradeRecord.StockCode = stockInfo.FullCode;

                tradeRecord.StockName = stockName;

                var stockPoolInfo = _stockService.GetStockPoolInfoByStockId(stockInfo.Id);

                var tradeType = row["交易类别"].ToString().Trim();
                tradeRecord.SetTradeType(tradeType);

                tradeRecord.SetBeneficiary(importOperation.BandPrincipal, importOperation.TargetPrincipal);

                tradeRecord.TradeTime = row["委托时间"].ToString().Trim();

                tradeRecord.DealFlag = row["操作"].ToString().Trim().IndexOf("买入") > -1 ? true : false;

                tradeRecord.DealNo = string.Empty;

                tradeRecord.DealPrice = decimal.Parse(row["成交均价"].ToString().Trim());

                tradeRecord.DealAmount = dealVolume * tradeRecord.DealPrice;

                tradeRecord.StockHolderCode = string.Empty;

                tradeRecord.ContractNo = row["合同编号"].ToString().Trim();

                var accountInfo = _accountService.GetAccountInfoById(tradeRecord.AccountId);

                tradeRecord.Commission = tradeRecord.DealAmount * accountInfo.CommissionRate;

                tradeRecord.Incidentals = 0;

                //买入
                if (tradeRecord.DealFlag)
                {
                    tradeRecord.DealVolume = CommonHelper.ConvertToPositive(int.Parse(row["成交数量"].ToString().Trim()));
                    tradeRecord.StampDuty = 0;
                    tradeRecord.ActualAmount = CommonHelper.ConvertToNegtive(tradeRecord.DealAmount + tradeRecord.Commission + tradeRecord.Incidentals);
                }
                //卖出
                else
                {
                    tradeRecord.DealVolume = CommonHelper.ConvertToNegtive(int.Parse(row["成交数量"].ToString().Trim()));
                    tradeRecord.StampDuty = tradeRecord.DealAmount * accountInfo.StampDutyRate;
                    tradeRecord.ActualAmount = CommonHelper.ConvertToPositive(tradeRecord.DealAmount - tradeRecord.StampDuty - tradeRecord.Commission - tradeRecord.Incidentals);
                }

                tradeRecord.Remarks = row["操作"].ToString().Trim();

                tradeRecords.Add(tradeRecord);
            }

            #endregion DataProcess

            return tradeRecords;
        }

        #endregion 当日委托--华泰证券（普通）

        #region 当日委托--申万证券（普通）

        /// <summary>
        /// 当日委托--申万证券（普通）
        /// </summary>
        /// <param name="importOperation"></param>
        /// <param name="importDataTable"></param>
        /// <returns></returns>
        private IList<DailyRecord> EntrustImportShenWan_N(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            #region DataFormatCheck

            var TemplateColumnNames = new List<string> { "委托时间", "证券代码", "证券名称", "操作", "委托状态", "委托数量", "成交数量", "撤单数量", "委托价格", "成交均价", "合同编号", "申报编号", "业务名称", "委托属性", "委托金额", "成交金额", "已撤数量", "交易类别" };

            this._dataImportService.DataFormatCheck(TemplateColumnNames, importDataTable);

            #endregion DataFormatCheck

            #region DataProcess

            var tradeRecords = new List<DailyRecord>();

            foreach (DataRow row in importDataTable.Rows)
            {
                //忽略成交数量为0的数据
                if (string.IsNullOrEmpty(row["成交数量"].ToString().Trim()) || int.Parse(row["成交数量"].ToString().Trim()) == 0) continue;

                //成交数量
                var dealVolume = int.Parse(row["成交数量"].ToString().Trim());

                var tradeRecord = new DailyRecord();

                tradeRecord.SetTradeRecordCommonFields(importOperation);

                var stockCode = CommonHelper.StockCodeZerofill(row["证券代码"].ToString().Trim());

                var stockName = row["证券名称"].ToString().Trim();

                var stockInfo = _stockService.GetStockInfoByCode(stockCode);

                VerifyStockInfo(stockInfo, stockCode, stockName);

                tradeRecord.StockCode = stockInfo.FullCode;

                tradeRecord.StockName = stockName;

                var stockPoolInfo = _stockService.GetStockPoolInfoByStockId(stockInfo.Id);

                var tradeType = row["交易类别"].ToString().Trim();
                tradeRecord.SetTradeType(tradeType);

                tradeRecord.SetBeneficiary(importOperation.BandPrincipal, importOperation.TargetPrincipal);

                tradeRecord.TradeTime = row["委托时间"].ToString().Trim();

                tradeRecord.DealFlag = row["操作"].ToString().Trim().IndexOf("买入") > -1 ? true : false;

                tradeRecord.DealNo = string.Empty;

                tradeRecord.DealPrice = decimal.Parse(row["成交均价"].ToString().Trim());

                tradeRecord.DealAmount = dealVolume * tradeRecord.DealPrice;

                tradeRecord.StockHolderCode = string.Empty;

                tradeRecord.ContractNo = row["合同编号"].ToString().Trim();

                var accountInfo = _accountService.GetAccountInfoById(tradeRecord.AccountId);

                tradeRecord.Commission = tradeRecord.DealAmount * accountInfo.CommissionRate;

                tradeRecord.Incidentals = 0;

                //买入
                if (tradeRecord.DealFlag)
                {
                    tradeRecord.DealVolume = CommonHelper.ConvertToPositive(int.Parse(row["成交数量"].ToString().Trim()));
                    tradeRecord.StampDuty = 0;
                    tradeRecord.ActualAmount = CommonHelper.ConvertToNegtive(tradeRecord.DealAmount + tradeRecord.Commission + tradeRecord.Incidentals);
                }
                //卖出
                else
                {
                    tradeRecord.DealVolume = CommonHelper.ConvertToNegtive(int.Parse(row["成交数量"].ToString().Trim()));
                    tradeRecord.StampDuty = tradeRecord.DealAmount * accountInfo.StampDutyRate;
                    tradeRecord.ActualAmount = CommonHelper.ConvertToPositive(tradeRecord.DealAmount - tradeRecord.StampDuty - tradeRecord.Commission - tradeRecord.Incidentals);
                }

                tradeRecord.Remarks = row["操作"].ToString().Trim();

                tradeRecords.Add(tradeRecord);
            }

            #endregion DataProcess

            return tradeRecords;
        }

        #endregion 当日委托--申万证券（普通）

        #region 当日委托--浙商证券（普通）

        /// <summary>
        /// 当日委托--浙商证券（普通）
        /// </summary>
        /// <param name="importOperation"></param>
        /// <param name="importDataTable"></param>
        /// <returns></returns>
        private IList<DailyRecord> EntrustImportZheShang_N(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            #region DataFormatCheck

            var TemplateColumnNames = new List<string> { "委托时间", "证券代码", "证券名称", "委托数量", "成交数量", "操作", "委托价格", "成交均价", "合同编号", "股东帐户", "交易市场", "申报编号", "备注", "废单原因", "交易类别" };

            this._dataImportService.DataFormatCheck(TemplateColumnNames, importDataTable);

            #endregion DataFormatCheck

            #region DataProcess

            var tradeRecords = new List<DailyRecord>();

            foreach (DataRow row in importDataTable.Rows)
            {
                //忽略成交数量为0的数据
                if (string.IsNullOrEmpty(row["成交数量"].ToString().Trim()) || int.Parse(row["成交数量"].ToString().Trim()) == 0) continue;

                //成交数量
                var dealVolume = int.Parse(row["成交数量"].ToString().Trim());

                var tradeRecord = new DailyRecord();

                tradeRecord.SetTradeRecordCommonFields(importOperation);

                var stockCode = CommonHelper.StockCodeZerofill(row["证券代码"].ToString().Trim());

                var stockName = row["证券名称"].ToString().Trim();

                var stockInfo = _stockService.GetStockInfoByCode(stockCode);

                VerifyStockInfo(stockInfo, stockCode, stockName);

                tradeRecord.StockCode = stockInfo.FullCode;

                tradeRecord.StockName = stockName;

                var stockPoolInfo = _stockService.GetStockPoolInfoByStockId(stockInfo.Id);

                var tradeType = row["交易类别"].ToString().Trim();
                tradeRecord.SetTradeType(tradeType);

                tradeRecord.SetBeneficiary(importOperation.BandPrincipal, importOperation.TargetPrincipal);

                tradeRecord.TradeTime = row["委托时间"].ToString().Trim();

                tradeRecord.DealFlag = row["操作"].ToString().Trim().IndexOf("买入") > -1 ? true : false;

                tradeRecord.DealNo = string.Empty;

                tradeRecord.DealPrice = decimal.Parse(row["成交均价"].ToString().Trim());

                tradeRecord.DealAmount = dealVolume * tradeRecord.DealPrice;

                tradeRecord.StockHolderCode = string.Empty;

                tradeRecord.ContractNo = row["合同编号"].ToString().Trim();

                var accountInfo = _accountService.GetAccountInfoById(tradeRecord.AccountId);

                tradeRecord.Commission = tradeRecord.DealAmount * accountInfo.CommissionRate;

                tradeRecord.Incidentals = 0;

                //买入
                if (tradeRecord.DealFlag)
                {
                    tradeRecord.DealVolume = CommonHelper.ConvertToPositive(int.Parse(row["成交数量"].ToString().Trim()));
                    tradeRecord.StampDuty = 0;
                    tradeRecord.ActualAmount = CommonHelper.ConvertToNegtive(tradeRecord.DealAmount + tradeRecord.Commission + tradeRecord.Incidentals);
                }
                //卖出
                else
                {
                    tradeRecord.DealVolume = CommonHelper.ConvertToNegtive(int.Parse(row["成交数量"].ToString().Trim()));
                    tradeRecord.StampDuty = tradeRecord.DealAmount * accountInfo.StampDutyRate;
                    tradeRecord.ActualAmount = CommonHelper.ConvertToPositive(tradeRecord.DealAmount - tradeRecord.StampDuty - tradeRecord.Commission - tradeRecord.Incidentals);
                }

                tradeRecord.Remarks = row["操作"].ToString().Trim();

                tradeRecords.Add(tradeRecord);
            }

            #endregion DataProcess

            return tradeRecords;
        }

        #endregion 当日委托--浙商证券（普通）

        #region 当日委托--中信证券（信用）

        /// <summary>
        /// 当日委托--中信证券（信用）
        /// </summary>
        /// <param name="importOperation"></param>
        /// <param name="importDataTable"></param>
        /// <returns></returns>
        private IList<DailyRecord> EntrustImportCITIC_C(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            #region DataFormatCheck

            var TemplateColumnNames = new List<string> { "证券代码", "证券名称", "买卖", "委托价格", "委托数量", "委托时间", "成交数量", "成交价格", "交易市场", "股东代码", "委托状态", "申请编号", "委托类型", "成交金额", "已撤数量", "业务名称", "资金帐号", "委托编号", "返回信息", "交易类别" };

            this._dataImportService.DataFormatCheck(TemplateColumnNames, importDataTable);

            #endregion DataFormatCheck

            #region DataProcess

            var tradeRecords = new List<DailyRecord>();

            foreach (DataRow row in importDataTable.Rows)
            {
                //忽略成交数量为0的数据
                if (string.IsNullOrEmpty(row["成交数量"].ToString().Trim()) || int.Parse(row["成交数量"].ToString().Trim()) == 0) continue;

                //成交数量
                var dealVolume = int.Parse(row["成交数量"].ToString().Trim());

                var tradeRecord = new DailyRecord();

                tradeRecord.SetTradeRecordCommonFields(importOperation);

                var stockCode = CommonHelper.StockCodeZerofill(row["证券代码"].ToString().Trim());

                var stockName = row["证券名称"].ToString().Trim();

                var stockInfo = _stockService.GetStockInfoByCode(stockCode);

                VerifyStockInfo(stockInfo, stockCode, stockName);

                tradeRecord.StockCode = stockInfo.FullCode;

                tradeRecord.StockName = stockName;

                var stockPoolInfo = _stockService.GetStockPoolInfoByStockId(stockInfo.Id);

                var tradeType = row["交易类别"].ToString().Trim();
                tradeRecord.SetTradeType(tradeType);

                tradeRecord.SetBeneficiary(importOperation.BandPrincipal, importOperation.TargetPrincipal);

                tradeRecord.TradeTime = row["委托时间"].ToString().Trim();

                tradeRecord.DealFlag = row["买卖"].ToString().Trim().IndexOf("买入") > -1 ? true : false;

                tradeRecord.DealNo = row["委托编号"].ToString().Trim();

                tradeRecord.DealPrice = decimal.Parse(row["成交价格"].ToString().Trim());

                tradeRecord.DealAmount = decimal.Parse(row["成交金额"].ToString().Trim());

                tradeRecord.StockHolderCode = row["股东代码"].ToString().Trim();

                tradeRecord.ContractNo = row["委托编号"].ToString().Trim();

                var accountInfo = _accountService.GetAccountInfoById(tradeRecord.AccountId);

                tradeRecord.Commission = tradeRecord.DealAmount * accountInfo.CommissionRate;

                tradeRecord.Incidentals = 0;

                //买入
                if (tradeRecord.DealFlag)
                {
                    tradeRecord.DealVolume = CommonHelper.ConvertToPositive(int.Parse(row["成交数量"].ToString().Trim()));
                    tradeRecord.StampDuty = 0;
                    tradeRecord.ActualAmount = CommonHelper.ConvertToNegtive(tradeRecord.DealAmount + tradeRecord.Commission + tradeRecord.Incidentals);
                }
                //卖出
                else
                {
                    tradeRecord.DealVolume = CommonHelper.ConvertToNegtive(int.Parse(row["成交数量"].ToString().Trim()));
                    tradeRecord.StampDuty = tradeRecord.DealAmount * accountInfo.StampDutyRate;
                    tradeRecord.ActualAmount = CommonHelper.ConvertToPositive(tradeRecord.DealAmount - tradeRecord.StampDuty - tradeRecord.Commission - tradeRecord.Incidentals);
                }

                tradeRecord.Remarks = row["买卖"].ToString().Trim();

                tradeRecords.Add(tradeRecord);
            }

            #endregion DataProcess

            return tradeRecords;
        }

        #endregion 当日委托--中信证券（信用）

        #region 当日委托--中信证券（普通）

        /// <summary>
        /// 当日委托--中信证券（普通）
        /// </summary>
        /// <param name="importOperation"></param>
        /// <param name="importDataTable"></param>
        /// <returns></returns>
        private IList<DailyRecord> EntrustImportCITIC_N(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            #region DataFormatCheck

            var TemplateColumnNames = new List<string> { "委托时间", "证券代码", "证券名称", "买卖", "委托状态", "委托数量", "成交数量", "已撤数量", "委托价格", "成交价格", "合同编号", "申请编号", "委托类型", "资金帐号", "委托编号", "委托类别", "返回信息", "交易类别" };

            this._dataImportService.DataFormatCheck(TemplateColumnNames, importDataTable);

            #endregion DataFormatCheck

            #region DataProcess

            var tradeRecords = new List<DailyRecord>();

            foreach (DataRow row in importDataTable.Rows)
            {
                //忽略成交数量为0的数据
                if (string.IsNullOrEmpty(row["成交数量"].ToString().Trim()) || int.Parse(row["成交数量"].ToString().Trim()) == 0) continue;

                //成交数量
                var dealVolume = int.Parse(row["成交数量"].ToString().Trim());

                var tradeRecord = new DailyRecord();

                tradeRecord.SetTradeRecordCommonFields(importOperation);

                var stockCode = CommonHelper.StockCodeZerofill(row["证券代码"].ToString().Trim());

                var stockName = row["证券名称"].ToString().Trim();

                var stockInfo = _stockService.GetStockInfoByCode(stockCode);

                VerifyStockInfo(stockInfo, stockCode, stockName);

                tradeRecord.StockCode = stockInfo.FullCode;

                tradeRecord.StockName = stockName;

                var stockPoolInfo = _stockService.GetStockPoolInfoByStockId(stockInfo.Id);

                var tradeType = row["交易类别"].ToString().Trim();
                tradeRecord.SetTradeType(tradeType);

                tradeRecord.SetBeneficiary(importOperation.BandPrincipal, importOperation.TargetPrincipal);

                tradeRecord.TradeTime = row["委托时间"].ToString().Trim();

                tradeRecord.DealFlag = row["买卖"].ToString().Trim().IndexOf("买入") > -1 ? true : false;

                tradeRecord.DealNo = string.Empty;

                tradeRecord.DealPrice = decimal.Parse(row["成交价格"].ToString().Trim());

                tradeRecord.DealAmount = dealVolume * tradeRecord.DealPrice;

                tradeRecord.StockHolderCode = string.Empty;

                tradeRecord.ContractNo = row["合同编号"].ToString().Trim();

                var accountInfo = _accountService.GetAccountInfoById(tradeRecord.AccountId);

                tradeRecord.Commission = tradeRecord.DealAmount * accountInfo.CommissionRate;

                tradeRecord.Incidentals = 0;

                //买入
                if (tradeRecord.DealFlag)
                {
                    tradeRecord.DealVolume = CommonHelper.ConvertToPositive(int.Parse(row["成交数量"].ToString().Trim()));
                    tradeRecord.StampDuty = 0;
                    tradeRecord.ActualAmount = CommonHelper.ConvertToNegtive(tradeRecord.DealAmount + tradeRecord.Commission + tradeRecord.Incidentals);
                }
                //卖出
                else
                {
                    tradeRecord.DealVolume = CommonHelper.ConvertToNegtive(int.Parse(row["成交数量"].ToString().Trim()));
                    tradeRecord.StampDuty = tradeRecord.DealAmount * accountInfo.StampDutyRate;
                    tradeRecord.ActualAmount = CommonHelper.ConvertToPositive(tradeRecord.DealAmount - tradeRecord.StampDuty - tradeRecord.Commission - tradeRecord.Incidentals);
                }

                tradeRecord.Remarks = row["买卖"].ToString().Trim();

                tradeRecords.Add(tradeRecord);
            }

            #endregion DataProcess

            return tradeRecords;
        }

        #endregion 当日委托--中信证券（普通）

        #region 当日委托--中银证券（普通）

        /// <summary>
        /// 当日委托--中银证券（普通）
        /// </summary>
        /// <param name="importOperation"></param>
        /// <param name="importDataTable"></param>
        /// <returns></returns>
        private IList<DailyRecord> EntrustImportBOCI_N(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            #region DataFormatCheck

            var TemplateColumnNames = new List<string> { "委托时间", "证券代码", "证券名称", "操作", "备注", "委托数量", "成交数量", "撤单数量", "委托价格", "成交均价", "合同编号", "申报编号", "交易类别" };

            this._dataImportService.DataFormatCheck(TemplateColumnNames, importDataTable);

            #endregion DataFormatCheck

            #region DataProcess

            var tradeRecords = new List<DailyRecord>();

            foreach (DataRow row in importDataTable.Rows)
            {
                //忽略成交数量为0的数据
                if (string.IsNullOrEmpty(row["成交数量"].ToString().Trim()) || int.Parse(row["成交数量"].ToString().Trim()) == 0) continue;

                //成交数量
                var dealVolume = int.Parse(row["成交数量"].ToString().Trim());

                var tradeRecord = new DailyRecord();

                tradeRecord.SetTradeRecordCommonFields(importOperation);

                var stockCode = CommonHelper.StockCodeZerofill(row["证券代码"].ToString().Trim());

                var stockName = row["证券名称"].ToString().Trim();

                var stockInfo = _stockService.GetStockInfoByCode(stockCode);

                VerifyStockInfo(stockInfo, stockCode, stockName);

                tradeRecord.StockCode = stockInfo.FullCode;

                tradeRecord.StockName = stockName;

                var stockPoolInfo = _stockService.GetStockPoolInfoByStockId(stockInfo.Id);

                var tradeType = row["交易类别"].ToString().Trim();
                tradeRecord.SetTradeType(tradeType);

                tradeRecord.SetBeneficiary(importOperation.BandPrincipal, importOperation.TargetPrincipal);

                tradeRecord.TradeTime = row["委托时间"].ToString().Trim();

                tradeRecord.DealFlag = row["操作"].ToString().Trim().IndexOf("买入") > -1 ? true : false;

                tradeRecord.DealNo = row["申报编号"].ToString().Trim();

                tradeRecord.DealPrice = decimal.Parse(row["成交均价"].ToString().Trim());

                tradeRecord.DealAmount = dealVolume * tradeRecord.DealPrice;

                tradeRecord.StockHolderCode = string.Empty;

                tradeRecord.ContractNo = row["合同编号"].ToString().Trim();

                var accountInfo = _accountService.GetAccountInfoById(tradeRecord.AccountId);

                tradeRecord.Commission = tradeRecord.DealAmount * accountInfo.CommissionRate;

                tradeRecord.Incidentals = 0;

                //买入
                if (tradeRecord.DealFlag)
                {
                    tradeRecord.DealVolume = CommonHelper.ConvertToPositive(int.Parse(row["成交数量"].ToString().Trim()));
                    tradeRecord.StampDuty = 0;
                    tradeRecord.ActualAmount = CommonHelper.ConvertToNegtive(tradeRecord.DealAmount + tradeRecord.Commission + tradeRecord.Incidentals);
                }
                //卖出
                else
                {
                    tradeRecord.DealVolume = CommonHelper.ConvertToNegtive(int.Parse(row["成交数量"].ToString().Trim()));
                    tradeRecord.StampDuty = tradeRecord.DealAmount * accountInfo.StampDutyRate;
                    tradeRecord.ActualAmount = CommonHelper.ConvertToPositive(tradeRecord.DealAmount - tradeRecord.StampDuty - tradeRecord.Commission - tradeRecord.Incidentals);
                }

                tradeRecord.Remarks = row["操作"].ToString().Trim();

                tradeRecords.Add(tradeRecord);
            }

            #endregion DataProcess

            return tradeRecords;
        }

        #endregion 当日委托--中银证券（普通）

        #endregion 当日委托数据导入

        #region 交割单数据导入

        #region 交割单--券商选择

        /// <summary>
        /// 交割单--券商选择
        /// </summary>
        /// <param name="securityAccount"></param>
        /// <param name="importDataTable"></param>
        /// <param name="importOperation"></param>
        /// <returns></returns>
        private IList<DailyRecord> DeliveryImport(EnumLibrary.SecurityAccount securityAccount, DataTable importDataTable, RecordImportOperationEntity importOperation)
        {
            IList<DailyRecord> result = new List<DailyRecord>();
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

        #endregion 交割单--券商选择

        #region 交割单--财通证券（信用）

        /// <summary>
        /// 交割单--财通证券（信用）
        /// </summary>
        /// <param name="importOperation"></param>
        /// <param name="importDataTable"></param>
        /// <returns></returns>
        private IList<DailyRecord> DeliveryImportCaiTong_C(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            #region DataFormatCheck

            var TemplateColumnNames = new List<string> { "证券代码", "证券名称", "操作", "成交均价", "成交数量", "成交金额", "印花税", "过户费", "发生金额", "合同编号", "股东帐户", "委托日期", "可用余额", "其他杂费", "佣金", "可用金额", "交易类别" };

            this._dataImportService.DataFormatCheck(TemplateColumnNames, importDataTable);

            #endregion DataFormatCheck

            #region DataProcess

            var tradeRecords = new List<DailyRecord>();

            foreach (DataRow row in importDataTable.Rows)
            {
                //过滤无实际成交额或成交量的交易记录
                if (int.Parse(row["成交数量"].ToString().Trim()) == 0 || decimal.Parse(row["成交金额"].ToString().Trim()) == 0) continue;

                var tradeRecord = new DailyRecord();

                tradeRecord.SetTradeRecordCommonFields(importOperation);

                var stockCode = CommonHelper.StockCodeZerofill(row["证券代码"].ToString().Trim());

                var stockName = row["证券名称"].ToString().Trim();

                var stockInfo = _stockService.GetStockInfoByCode(stockCode);

                VerifyStockInfo(stockInfo, stockCode, stockName);

                tradeRecord.StockCode = stockInfo.FullCode;

                tradeRecord.StockName = stockName;

                var stockPoolInfo = _stockService.GetStockPoolInfoByStockId(stockInfo.Id);

                var tradeType = row["交易类别"].ToString().Trim();
                tradeRecord.SetTradeType(tradeType);

                tradeRecord.SetBeneficiary(importOperation.BandPrincipal, importOperation.TargetPrincipal);

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
        private IList<DailyRecord> DeliveryImportCaiTong_N(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            #region DataFormatCheck

            var TemplateColumnNames = new List<string> { "成交日期", "证券代码", "证券名称", "操作", "成交均价", "成交数量", "成交金额", "手续费", "印花税", "其他杂费", "发生金额", "本次金额", "合同编号", "成交时间", "股东帐户", "备注", "成交编号", "交易市场", "交易类别" };

            this._dataImportService.DataFormatCheck(TemplateColumnNames, importDataTable);

            #endregion DataFormatCheck

            #region DataProcess

            var tradeRecords = new List<DailyRecord>();

            foreach (DataRow row in importDataTable.Rows)
            {
                //过滤无实际成交额或成交量的交易记录
                if (int.Parse(row["成交数量"].ToString().Trim()) == 0 || decimal.Parse(row["成交金额"].ToString().Trim()) == 0) continue;

                var tradeRecord = new DailyRecord();

                tradeRecord.SetTradeRecordCommonFields(importOperation);

                var stockCode = CommonHelper.StockCodeZerofill(row["证券代码"].ToString().Trim());

                var stockName = row["证券名称"].ToString().Trim();

                var stockInfo = _stockService.GetStockInfoByCode(stockCode);

                VerifyStockInfo(stockInfo, stockCode, stockName);

                tradeRecord.StockCode = stockInfo.FullCode;

                tradeRecord.StockName = stockName;

                var stockPoolInfo = _stockService.GetStockPoolInfoByStockId(stockInfo.Id);

                var tradeType = row["交易类别"].ToString().Trim();
                tradeRecord.SetTradeType(tradeType);

                tradeRecord.SetBeneficiary(importOperation.BandPrincipal, importOperation.TargetPrincipal);

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

        private IList<DailyRecord> DeliveryImportFounder_N(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            #region DataFormatCheck

            var TemplateColumnNames = new List<string> { "成交日期", "成交时间", "证券代码", "证券名称", "操作", "成交数量", "成交编号", "成交均价", "成交金额", "余额", "发生金额", "印花税", "其他杂费", "本次金额", "合同编号", "股东帐户", "佣金", "过户费", "交易市场", "交易类别" };

            this._dataImportService.DataFormatCheck(TemplateColumnNames, importDataTable);

            #endregion DataFormatCheck

            #region DataProcess

            var tradeRecords = new List<DailyRecord>();

            foreach (DataRow row in importDataTable.Rows)
            {
                //过滤无实际成交额或成交量的交易记录
                if (int.Parse(row["成交数量"].ToString().Trim()) == 0 || decimal.Parse(row["成交金额"].ToString().Trim()) == 0) continue;

                var tradeRecord = new DailyRecord();

                tradeRecord.SetTradeRecordCommonFields(importOperation);

                var stockCode = CommonHelper.StockCodeZerofill(row["证券代码"].ToString().Trim());

                var stockName = row["证券名称"].ToString().Trim();

                var stockInfo = _stockService.GetStockInfoByCode(stockCode);

                VerifyStockInfo(stockInfo, stockCode, stockName);

                tradeRecord.StockCode = stockInfo.FullCode;

                tradeRecord.StockName = stockName;

                var stockPoolInfo = _stockService.GetStockPoolInfoByStockId(stockInfo.Id);

                var tradeType = row["交易类别"].ToString().Trim();
                tradeRecord.SetTradeType(tradeType);

                tradeRecord.SetBeneficiary(importOperation.BandPrincipal, importOperation.TargetPrincipal);

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
        private IList<DailyRecord> DeliveryImportSinoLink_N(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            #region DataFormatCheck

            var TemplateColumnNames = new List<string> { "成交日期", "证券代码", "证券名称", "买卖标志", "成交价格", "成交数量", "成交金额", "发生金额", "佣金", "印花税", "过户费", "成交编号", "股东代码", "备注", "交易类别" };

            this._dataImportService.DataFormatCheck(TemplateColumnNames, importDataTable);

            #endregion DataFormatCheck

            #region DataProcess

            var tradeRecords = new List<DailyRecord>();

            foreach (DataRow row in importDataTable.Rows)
            {
                //过滤无实际成交额或成交量的交易记录
                if (int.Parse(row["成交数量"].ToString().Trim()) == 0 || decimal.Parse(row["成交金额"].ToString().Trim()) == 0) continue;

                var tradeRecord = new DailyRecord();

                tradeRecord.SetTradeRecordCommonFields(importOperation);

                var stockCode = CommonHelper.StockCodeZerofill(row["证券代码"].ToString().Trim());

                var stockName = row["证券名称"].ToString().Trim();

                var stockInfo = _stockService.GetStockInfoByCode(stockCode);

                VerifyStockInfo(stockInfo, stockCode, stockName);

                tradeRecord.StockCode = stockInfo.FullCode;

                tradeRecord.StockName = stockName;

                var stockPoolInfo = _stockService.GetStockPoolInfoByStockId(stockInfo.Id);

                var tradeType = row["交易类别"].ToString().Trim();
                tradeRecord.SetTradeType(tradeType);

                tradeRecord.SetBeneficiary(importOperation.BandPrincipal, importOperation.TargetPrincipal);

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
        private IList<DailyRecord> DeliveryImportGuoTai_C(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            #region DataFormatCheck

            var TemplateColumnNames = new List<string> { "成交日期", "业务名称", "证券代码", "证券名称", "成交价格", "成交数量", "剩余数量", "成交金额", "清算金额", "剩余金额", "净佣金", "规费", "印花税", "过户费", "结算费", "附加费", "成交编号", "股东代码", "交易类别" };

            this._dataImportService.DataFormatCheck(TemplateColumnNames, importDataTable);

            #endregion DataFormatCheck

            #region DataProcess

            var tradeRecords = new List<DailyRecord>();

            foreach (DataRow row in importDataTable.Rows)
            {
                //过滤无实际成交额或成交量的交易记录
                if (int.Parse(row["成交数量"].ToString().Trim()) == 0 || decimal.Parse(row["成交金额"].ToString().Trim()) == 0) continue;

                var tradeRecord = new DailyRecord();

                tradeRecord.SetTradeRecordCommonFields(importOperation);

                var stockCode = CommonHelper.StockCodeZerofill(row["证券代码"].ToString().Trim());

                var stockName = row["证券名称"].ToString().Trim();

                var stockInfo = _stockService.GetStockInfoByCode(stockCode);

                VerifyStockInfo(stockInfo, stockCode, stockName);

                tradeRecord.StockCode = stockInfo.FullCode;

                tradeRecord.StockName = stockName;

                var stockPoolInfo = _stockService.GetStockPoolInfoByStockId(stockInfo.Id);

                var tradeType = row["交易类别"].ToString().Trim();
                tradeRecord.SetTradeType(tradeType);

                tradeRecord.SetBeneficiary(importOperation.BandPrincipal, importOperation.TargetPrincipal);

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
        private IList<DailyRecord> DeliveryImportGuoTai_N(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            #region DataFormatCheck

            var TemplateColumnNames = new List<string> { "成交日期", "证券代码", "证券名称", "操作", "成交数量", "成交均价", "成交金额", "股票余额", "发生金额", "手续费", "印花税", "其他杂费", "资金余额", "合同编号", "市场名称", "股东帐户", "交易类别" };

            this._dataImportService.DataFormatCheck(TemplateColumnNames, importDataTable);

            #endregion DataFormatCheck

            #region DataProcess

            var tradeRecords = new List<DailyRecord>();

            foreach (DataRow row in importDataTable.Rows)
            {
                //过滤无实际成交额或成交量的交易记录
                if (int.Parse(row["成交数量"].ToString().Trim()) == 0 || decimal.Parse(row["成交金额"].ToString().Trim()) == 0) continue;

                var tradeRecord = new DailyRecord();

                tradeRecord.SetTradeRecordCommonFields(importOperation);

                var stockCode = CommonHelper.StockCodeZerofill(row["证券代码"].ToString().Trim());

                var stockName = row["证券名称"].ToString().Trim();

                var stockInfo = _stockService.GetStockInfoByCode(stockCode);

                VerifyStockInfo(stockInfo, stockCode, stockName);

                tradeRecord.StockCode = stockInfo.FullCode;

                tradeRecord.StockName = stockName;

                var stockPoolInfo = _stockService.GetStockPoolInfoByStockId(stockInfo.Id);

                var tradeType = row["交易类别"].ToString().Trim();
                tradeRecord.SetTradeType(tradeType);

                tradeRecord.SetBeneficiary(importOperation.BandPrincipal, importOperation.TargetPrincipal);

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
        private IList<DailyRecord> DeliveryImportHuaTai_N(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            #region DataFormatCheck

            var TemplateColumnNames = new List<string> { "成交日期", "摘要", "证券名称", "合同编号", "成交数量", "成交均价", "成交金额", "手续费", "印花税", "其他杂费", "发生金额", "股东帐户", "备注", "操作", "证券代码", "结算汇率", "交易类别" };

            this._dataImportService.DataFormatCheck(TemplateColumnNames, importDataTable);

            #endregion DataFormatCheck

            #region DataProcess

            var tradeRecords = new List<DailyRecord>();

            foreach (DataRow row in importDataTable.Rows)
            {
                //过滤无实际成交额或成交量的交易记录
                if (int.Parse(row["成交数量"].ToString().Trim()) == 0 || decimal.Parse(row["成交金额"].ToString().Trim()) == 0) continue;

                var tradeRecord = new DailyRecord();

                tradeRecord.SetTradeRecordCommonFields(importOperation);

                var stockCode = CommonHelper.StockCodeZerofill(row["证券代码"].ToString().Trim());

                var stockName = row["证券名称"].ToString().Trim();

                var stockInfo = _stockService.GetStockInfoByCode(stockCode);

                VerifyStockInfo(stockInfo, stockCode, stockName);

                tradeRecord.StockCode = stockInfo.FullCode;

                tradeRecord.StockName = stockName;

                var stockPoolInfo = _stockService.GetStockPoolInfoByStockId(stockInfo.Id);

                var tradeType = row["交易类别"].ToString().Trim();
                tradeRecord.SetTradeType(tradeType);

                tradeRecord.SetBeneficiary(importOperation.BandPrincipal, importOperation.TargetPrincipal);

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
        private IList<DailyRecord> DeliveryImportHuaTai_C(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            #region DataFormatCheck

            var TemplateColumnNames = new List<string> { "成交日期", "摘要", "证券名称", "合同编号", "成交数量", "成交均价", "成交金额", "手续费", "印花税", "其他杂费", "发生金额", "股东帐户", "备注", "本次资金余额", "本次股票余额", "交易类别" };

            this._dataImportService.DataFormatCheck(TemplateColumnNames, importDataTable);

            #endregion DataFormatCheck

            #region DataProcess

            var tradeRecords = new List<DailyRecord>();

            foreach (DataRow row in importDataTable.Rows)
            {
                //过滤无实际成交额或成交量的交易记录
                if (int.Parse(row["成交数量"].ToString().Trim()) == 0 || decimal.Parse(row["成交金额"].ToString().Trim()) == 0) continue;

                var tradeRecord = new DailyRecord();

                tradeRecord.SetTradeRecordCommonFields(importOperation);

                var stockCode = string.Empty;

                var stockName = row["证券名称"].ToString().Trim();

                var stockInfo = _stockService.GetStockInfoByName(stockName);

                VerifyStockInfo(stockInfo, stockCode, stockName);

                tradeRecord.StockCode = stockInfo.FullCode;

                tradeRecord.StockName = stockName;

                var stockPoolInfo = _stockService.GetStockPoolInfoByStockId(stockInfo.Id);

                var tradeType = row["交易类别"].ToString().Trim();
                tradeRecord.SetTradeType(tradeType);

                tradeRecord.SetBeneficiary(importOperation.BandPrincipal, importOperation.TargetPrincipal);

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
        private IList<DailyRecord> DeliveryImportShenWan_N(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            #region DataFormatCheck

            var TemplateColumnNames = new List<string> { "成交日期", "证券代码", "证券名称", "操作", "成交数量", "成交编号", "成交均价", "成交金额", "余额", "发生金额", "手续费", "印花税", "其他杂费", "本次金额", "合同编号", "股东帐户", "交易市场", "交易类别" };

            this._dataImportService.DataFormatCheck(TemplateColumnNames, importDataTable);

            #endregion DataFormatCheck

            #region DataProcess

            var tradeRecords = new List<DailyRecord>();

            foreach (DataRow row in importDataTable.Rows)
            {
                //过滤无实际成交额或成交量的交易记录
                if (int.Parse(row["成交数量"].ToString().Trim()) == 0 || decimal.Parse(row["成交金额"].ToString().Trim()) == 0) continue;

                var tradeRecord = new DailyRecord();

                tradeRecord.SetTradeRecordCommonFields(importOperation);

                var stockCode = CommonHelper.StockCodeZerofill(row["证券代码"].ToString().Trim());

                var stockName = row["证券名称"].ToString().Trim();

                var stockInfo = _stockService.GetStockInfoByCode(stockCode);

                VerifyStockInfo(stockInfo, stockCode, stockName);

                tradeRecord.StockCode = stockInfo.FullCode;

                tradeRecord.StockName = stockName;

                var stockPoolInfo = _stockService.GetStockPoolInfoByStockId(stockInfo.Id);

                var tradeType = row["交易类别"].ToString().Trim();
                tradeRecord.SetTradeType(tradeType);

                tradeRecord.SetBeneficiary(importOperation.BandPrincipal, importOperation.TargetPrincipal);

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
        private IList<DailyRecord> DeliveryImportGalaxy_N(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            #region DataFormatCheck

            var TemplateColumnNames = new List<string> { "证券代码", "证券名称", "操作", "成交数量", "成交均价", "成交金额", "股票余额", "发生金额", "手续费", "印花税", "其他杂费", "资金余额", "合同编号", "股东帐户", "交收日期", "净佣金", "过户费", "结算费", "币种", "交易类别" };

            this._dataImportService.DataFormatCheck(TemplateColumnNames, importDataTable);

            #endregion DataFormatCheck

            #region DataProcess

            var tradeRecords = new List<DailyRecord>();

            foreach (DataRow row in importDataTable.Rows)
            {
                //过滤无实际成交额或成交量的交易记录
                if (int.Parse(row["成交数量"].ToString().Trim()) == 0 || decimal.Parse(row["成交金额"].ToString().Trim()) == 0) continue;

                var tradeRecord = new DailyRecord();

                tradeRecord.SetTradeRecordCommonFields(importOperation);

                var stockCode = CommonHelper.StockCodeZerofill(row["证券代码"].ToString().Trim());

                var stockName = row["证券名称"].ToString().Trim();

                var stockInfo = _stockService.GetStockInfoByCode(stockCode);

                VerifyStockInfo(stockInfo, stockCode, stockName);

                tradeRecord.StockCode = stockInfo.FullCode;

                tradeRecord.StockName = stockName;

                var stockPoolInfo = _stockService.GetStockPoolInfoByStockId(stockInfo.Id);

                var tradeType = row["交易类别"].ToString().Trim();
                tradeRecord.SetTradeType(tradeType);

                tradeRecord.SetBeneficiary(importOperation.BandPrincipal, importOperation.TargetPrincipal);

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
        private IList<DailyRecord> DeliveryImportZhaoShang_N(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            #region DataFormatCheck

            var TemplateColumnNames = new List<string> { "币种", "证券名称", "成交日期", "成交价格", "成交数量", "发生金额", "资金余额", "合同编号", "业务名称", "手续费", "印花税", "过户费", "结算费", "证券代码", "股东代码", "交易类别" };

            this._dataImportService.DataFormatCheck(TemplateColumnNames, importDataTable);

            #endregion DataFormatCheck

            #region DataProcess

            var tradeRecords = new List<DailyRecord>();

            var validRecords = importDataTable.AsEnumerable()
                .Where(x =>
                CommonHelper.IsInt(x.Field<string>("合同编号").Trim()) &&
                Convert.ToDecimal(x.Field<string>("成交价格").Trim()) != 0 &&
                Convert.ToDecimal(x.Field<string>("成交数量").Trim()) != 0
                ).ToList();

            foreach (DataRow row in validRecords)
            {
                var tradeRecord = new DailyRecord();
                tradeRecord.SetTradeRecordCommonFields(importOperation);

                var stockCode = CommonHelper.StockCodeZerofill(row["证券代码"].ToString().Trim());

                var stockName = row["证券名称"].ToString().Trim();

                var stockInfo = _stockService.GetStockInfoByCode(stockCode);

                VerifyStockInfo(stockInfo, stockCode, stockName);

                tradeRecord.StockCode = stockInfo.FullCode;

                tradeRecord.StockName = stockName;

                var stockPoolInfo = _stockService.GetStockPoolInfoByStockId(stockInfo.Id);

                var tradeType = row["交易类别"].ToString().Trim();
                tradeRecord.SetTradeType(tradeType);

                tradeRecord.SetBeneficiary(importOperation.BandPrincipal, importOperation.TargetPrincipal);

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
        private IList<DailyRecord> DeliveryImportZheShang_N(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            #region DataFormatCheck

            var TemplateColumnNames = new List<string> { "成交日期", "证券代码", "证券名称", "成交价格", "发生数量", "成交数量", "成交金额", "发生金额", "股票余额", "佣金", "印花税", "过户费", "成交编号", "合同编号", "操作", "股东帐户", "交易市场", "备注", "交易类别" };

            this._dataImportService.DataFormatCheck(TemplateColumnNames, importDataTable);

            #endregion DataFormatCheck

            #region DataProcess

            var tradeRecords = new List<DailyRecord>();

            foreach (DataRow row in importDataTable.Rows)
            {
                //过滤无实际成交额或成交量的交易记录
                if (int.Parse(row["成交数量"].ToString().Trim()) == 0 || decimal.Parse(row["成交金额"].ToString().Trim()) == 0) continue;

                var tradeRecord = new DailyRecord();
                tradeRecord.SetTradeRecordCommonFields(importOperation);

                var stockCode = CommonHelper.StockCodeZerofill(row["证券代码"].ToString().Trim());

                var stockName = row["证券名称"].ToString().Trim();

                var stockInfo = _stockService.GetStockInfoByCode(stockCode);

                VerifyStockInfo(stockInfo, stockCode, stockName);

                tradeRecord.StockCode = stockInfo.FullCode;

                tradeRecord.StockName = stockName;

                var stockPoolInfo = _stockService.GetStockPoolInfoByStockId(stockInfo.Id);

                var tradeType = row["交易类别"].ToString().Trim();
                tradeRecord.SetTradeType(tradeType);

                tradeRecord.SetBeneficiary(importOperation.BandPrincipal, importOperation.TargetPrincipal);

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
        private IList<DailyRecord> DeliveryImportCITIC_C(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            #region DataFormatCheck

            var TemplateColumnNames = new List<string> { "发生日期", "证券名称", "委托编号", "成交数量", "成交价格", "成交金额", "手续费", "印花税", "清算金额", "资金本次余额", "股东代码", "备注", "过户费", "交易所清算费", "成交时间", "资金帐号", "币种", "费用备注", "交易类别" };

            this._dataImportService.DataFormatCheck(TemplateColumnNames, importDataTable);

            #endregion DataFormatCheck

            #region DataProcess

            var tradeRecords = new List<DailyRecord>();

            foreach (DataRow row in importDataTable.Rows)
            {
                //过滤无实际成交额或成交量的交易记录
                if (int.Parse(row["成交数量"].ToString().Trim()) == 0 || decimal.Parse(row["成交金额"].ToString().Trim()) == 0) continue;

                var tradeRecord = new DailyRecord();

                tradeRecord.SetTradeRecordCommonFields(importOperation);

                var stockName = row["证券名称"].ToString().Trim();

                var stockInfo = _stockService.GetStockInfoByName(stockName);

                var stockCode = string.Empty;

                VerifyStockInfo(stockInfo, stockCode, stockName);

                tradeRecord.StockCode = stockInfo.FullCode;

                tradeRecord.StockName = stockName;

                var stockPoolInfo = _stockService.GetStockPoolInfoByStockId(stockInfo.Id);

                var tradeType = row["交易类别"].ToString().Trim();
                tradeRecord.SetTradeType(tradeType);

                tradeRecord.SetBeneficiary(importOperation.BandPrincipal, importOperation.TargetPrincipal);

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
        private IList<DailyRecord> DeliveryImportCITIC_N(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            #region DataFormatCheck

            var TemplateColumnNames = new List<string> { "发生日期", "成交时间", "证券代码", "证券名称", "业务名称", "成交数量", "成交价格", "成交金额", "余额", "清算金额", "手续费", "印花税", "附加费", "资金本次余额", "委托编号", "股东代码", "过户费", "交易所清算费", "资金帐号", "币种", "费用备注", "交易类别" };

            this._dataImportService.DataFormatCheck(TemplateColumnNames, importDataTable);

            #endregion DataFormatCheck

            #region DataProcess

            var tradeRecords = new List<DailyRecord>();

            foreach (DataRow row in importDataTable.Rows)
            {
                //过滤无实际成交额或成交量的交易记录
                if (int.Parse(row["成交数量"].ToString().Trim()) == 0 || decimal.Parse(row["成交金额"].ToString().Trim()) == 0) continue;

                var tradeRecord = new DailyRecord();

                tradeRecord.SetTradeRecordCommonFields(importOperation);

                var stockCode = CommonHelper.StockCodeZerofill(row["证券代码"].ToString().Trim());

                var stockName = row["证券名称"].ToString().Trim();

                var stockInfo = _stockService.GetStockInfoByCode(stockCode);

                VerifyStockInfo(stockInfo, stockCode, stockName);

                tradeRecord.StockCode = stockInfo.FullCode;

                tradeRecord.StockName = stockName;

                var stockPoolInfo = _stockService.GetStockPoolInfoByStockId(stockInfo.Id);

                var tradeType = row["交易类别"].ToString().Trim();
                tradeRecord.SetTradeType(tradeType);

                tradeRecord.SetBeneficiary(importOperation.BandPrincipal, importOperation.TargetPrincipal);

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
        private IList<DailyRecord> DeliveryImportBOCI_C(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            #region DataFormatCheck

            var TemplateColumnNames = new List<string> { "发生日期", "成交时间", "证券代码", "证券名称", "买卖标志", "成交价格", "成交数量", "成交金额", "发生金额", "剩余金额", "申报序号", "成交编号", "委托编号", "股东代码", "席位代码", "证券数量", "佣金", "印花税", "过户费", "交易征费", "交易规费", "备注", "交易类别" };

            this._dataImportService.DataFormatCheck(TemplateColumnNames, importDataTable);

            #endregion DataFormatCheck

            #region DataProcess

            var tradeRecords = new List<DailyRecord>();

            foreach (DataRow row in importDataTable.Rows)
            {
                //过滤无实际成交额或成交量的交易记录
                if (int.Parse(row["成交数量"].ToString().Trim()) == 0 || decimal.Parse(row["成交金额"].ToString().Trim()) == 0) continue;

                var tradeRecord = new DailyRecord();

                tradeRecord.SetTradeRecordCommonFields(importOperation);

                var stockCode = CommonHelper.StockCodeZerofill(row["证券代码"].ToString().Trim());

                var stockName = row["证券名称"].ToString().Trim();

                var stockInfo = _stockService.GetStockInfoByCode(stockCode);

                VerifyStockInfo(stockInfo, stockCode, stockName);

                tradeRecord.StockCode = stockInfo.FullCode;

                tradeRecord.StockName = stockName;

                var stockPoolInfo = _stockService.GetStockPoolInfoByStockId(stockInfo.Id);

                var tradeType = row["交易类别"].ToString().Trim();
                tradeRecord.SetTradeType(tradeType);

                tradeRecord.SetBeneficiary(importOperation.BandPrincipal, importOperation.TargetPrincipal);

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
        private IList<DailyRecord> DeliveryImportBOCI_N(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            #region DataFormatCheck

            var TemplateColumnNames = new List<string> { "发生日期", "成交时间", "证券代码", "证券名称", "买卖标志", "成交价格", "成交数量", "成交金额", "发生金额", "剩余金额", "申报序号", "成交编号", "委托编号", "股东代码", "席位代码", "证券数量", "佣金", "印花税", "过户费", "交易征费", "交易规费", "备注", "交易类别" };

            this._dataImportService.DataFormatCheck(TemplateColumnNames, importDataTable);

            #endregion DataFormatCheck

            #region DataProcess

            var tradeRecords = new List<DailyRecord>();

            foreach (DataRow row in importDataTable.Rows)
            {
                //过滤无实际成交额或成交量的交易记录
                if (int.Parse(row["成交数量"].ToString().Trim()) == 0 || decimal.Parse(row["成交金额"].ToString().Trim()) == 0) continue;

                var tradeRecord = new DailyRecord();

                tradeRecord.SetTradeRecordCommonFields(importOperation);

                var stockCode = CommonHelper.StockCodeZerofill(row["证券代码"].ToString().Trim());

                var stockName = row["证券名称"].ToString().Trim();

                var stockInfo = _stockService.GetStockInfoByCode(stockCode);

                VerifyStockInfo(stockInfo, stockCode, stockName);

                tradeRecord.StockCode = stockInfo.FullCode;

                tradeRecord.StockName = stockName;

                var stockPoolInfo = _stockService.GetStockPoolInfoByStockId(stockInfo.Id);

                var tradeType = row["交易类别"].ToString().Trim();
                tradeRecord.SetTradeType(tradeType);

                tradeRecord.SetBeneficiary(importOperation.BandPrincipal, importOperation.TargetPrincipal);

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

                tradeRecord.Remarks = row["买卖标志"].ToString().Trim();

                tradeRecords.Add(tradeRecord);
            }

            #endregion DataProcess

            return tradeRecords;
        }

        #endregion 交割单--中银国际（普通）

        #endregion 交割单数据导入

        #endregion 数据导入

        #endregion Utilities

        #region Methods

        public virtual bool DataImportProcess(EnumLibrary.SecurityAccount securityAccount, DataTable importDataTable, RecordImportOperationEntity importOperation)
        {
            ImportRecordsCheck(importDataTable, importOperation);

            IList<DailyRecord> records = new List<DailyRecord>();

            switch (importOperation.DataType)
            {
                case EnumLibrary.DataType.Entrust:
                    importOperation.TradeDate = importOperation.ImportTime.Date;
                    records = EntrustImport(securityAccount, importDataTable, importOperation);
                    break;

                case EnumLibrary.DataType.Daily:
                    //importOperation.TradeDate = importOperation.ImportTime.Date;
                    //records = DailyImport(importOperation, importDataTable);
                    break;

                case EnumLibrary.DataType.Delivery:
                    importOperation.TradeDate = null;
                    records = DeliveryImport(securityAccount, importDataTable, importOperation);
                    break;
            }

            InsertDailyRecords(records);

            return true;
        }

        public virtual IList<DailyRecord> GetDailyRecords(
            int[] accountIds = null,
            int tradeType = 0,
            string[] beneficiaries = null,
            DateTime? tradeDateFrom = null,
            DateTime? tradeDateTo = null
            )
        {
            var query = _dailyRepository.TableNoTracking;

            if (accountIds != null)
                query = query.Where(x => accountIds.Contains(x.AccountId));
            if (tradeType > 0)
                query = query.Where(x => x.TradeType == tradeType);
            if (tradeDateFrom.HasValue)
                query = query.Where(x => x.TradeDate >= tradeDateFrom);
            if (tradeDateTo.HasValue)
                query = query.Where(x => x.TradeDate <= tradeDateTo);
            if (beneficiaries != null)
                query = query.Where(x => beneficiaries.Contains(x.Beneficiary));

            return query.ToList();
        }

        public virtual IList<DailyRecord> GetDailyRecordsBySearchCondition(
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
            )
        {
            var query = _dailyRepository.TableNoTracking;

            if (!string.IsNullOrEmpty(stockCode))
                query = query.Where(x => x.StockCode == stockCode);

            if (accountId > 0)
                query = query.Where(x => x.AccountId == accountId);

            if (tradeType > 0)
                query = query.Where(x => x.TradeType == tradeType);

            if (!string.IsNullOrEmpty(beneficiary))
                query = query.Where(x => x.Beneficiary == beneficiary);

            if (!string.IsNullOrEmpty(operatorCode))
                query = query.Where(x => x.OperatorCode == operatorCode);

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
            query = query.OrderByDescending(x => x.TradeDate);

            return query.ToList();
        }

        public virtual IList<DailyRecord> GetDailyRecordsDetail(
           string stockCode = null,
           int accountId = 0,
           int dataType = 0,
           int tradeType = 0,
           string beneficiary = null,
           string operatorCode = null,
           bool? dealFlag = null,
           DateTime? tradeDateFrom = null,
           DateTime? tradeDateTo = null
           )
        {
            var query = _dailyRepository.TableNoTracking;

            if (!string.IsNullOrEmpty(operatorCode) && !string.IsNullOrEmpty(beneficiary))
                query = query.Where(x => x.Beneficiary == beneficiary || x.OperatorCode == operatorCode);
            else
            {
                if (!string.IsNullOrEmpty(operatorCode))
                    query = query.Where(x => x.OperatorCode == operatorCode);
                if (!string.IsNullOrEmpty(beneficiary))
                    query = query.Where(x => x.Beneficiary == beneficiary);
            }

            if (!string.IsNullOrEmpty(stockCode))
                query = query.Where(x => x.StockCode == stockCode);

            if (accountId > 0)
                query = query.Where(x => x.AccountId == accountId);

            if (dataType > 0)
                query = query.Where(x => x.DataType == dataType);

            if (tradeType > 0)
                query = query.Where(x => x.TradeType == tradeType);

            if (dealFlag.HasValue)
                query = query.Where(x => x.DealFlag == dealFlag.Value);

            if (tradeDateFrom.HasValue)
                query = query.Where(x => x.TradeDate >= tradeDateFrom);

            if (tradeDateTo.HasValue)
                query = query.Where(x => x.TradeDate <= tradeDateTo);

            query = query.OrderByDescending(x => x.TradeDate);

            var infos = from d in query
                        join a in _accountRepository.Table
                        on d.AccountId equals a.Id into temp1
                        from account in temp1.DefaultIfEmpty()
                        join u1 in _userRepository.Table
                        on d.ImportUser equals u1.Code into temp2
                        from importUser in temp2.DefaultIfEmpty()
                        join u2 in _userRepository.Table
                        on d.OperatorCode equals u2.Code into temp3
                        from operateUser in temp3.DefaultIfEmpty()
                        join u3 in _userRepository.Table
                        on d.Beneficiary equals u3.Code into temp4
                        from beneficiaryUser in temp4.DefaultIfEmpty()
                        join u4 in _userRepository.Table
                        on d.UpdateUser equals u4.Code into temp5
                        from updateUser in temp5.DefaultIfEmpty()
                        select new { d, account, importUser, operateUser, beneficiaryUser, updateUser };

            var result = infos.ToList().Select(x => new DailyRecord
            {
                Id = x.d.Id,
                AccountId = x.d.AccountId,
                AccountName = x.account == null ? null : x.account.Name + " - " + x.account.SecurityCompanyName + " - " + x.account.AttributeName,
                ActualAmount = x.d.ActualAmount,
                AuditFlag = x.d.AuditFlag,
                AuditNo = x.d.AuditNo,
                AuditTime = x.d.AuditTime,
                Beneficiary = x.d.Beneficiary,
                BeneficiaryName = x.beneficiaryUser == null ? null : x.beneficiaryUser.Name,
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
                OperatorCode = x.d.OperatorCode,
                OperatorName = x.operateUser == null ? null : x.operateUser.Name,
                Remarks = x.d.Remarks,
                SplitNo = x.d.SplitNo,
                StampDuty = x.d.StampDuty,
                StockCode = x.d.StockCode,
                StockHolderCode = x.d.StockHolderCode,
                StockName = x.d.StockName,
                TradeDate = x.d.TradeDate,
                TradeTime = x.d.TradeTime,
                TradeType = x.d.TradeType,
                UpdateTime = x.d.UpdateTime,
                UpdateUser = x.d.UpdateUser,
                UpdateUserName = x.updateUser == null ? null : x.updateUser.Name,
            }
            )
            .ToList();

            return result;
        }

        public virtual IList<DailyRecord> GetDailyRecordsDetailBySearchCondition(
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
            )
        {
            var query = _dailyRepository.TableNoTracking;

            if (IsAdmin)
            {
                if (!string.IsNullOrEmpty(beneficiary))
                    query = query.Where(x => x.Beneficiary == beneficiary);

                if (!string.IsNullOrEmpty(operatorCode))
                    query = query.Where(x => x.OperatorCode == operatorCode);

                if (!string.IsNullOrEmpty(importUserCode))
                    query = query.Where(x => x.ImportUser == importUserCode);
            }
            else
                query = query.Where(x => x.Beneficiary == beneficiary || x.OperatorCode == operatorCode || x.ImportUser == importUserCode);

            if (!string.IsNullOrEmpty(stockCode))
                query = query.Where(x => x.StockCode == stockCode);

            if (accountId > 0)
                query = query.Where(x => x.AccountId == accountId);

            if (dataType > 0)
                query = query.Where(x => x.DataType == dataType);

            if (tradeType > 0)
                query = query.Where(x => x.TradeType == tradeType);

            if (dealFlag.HasValue)
                query = query.Where(x => x.DealFlag == dealFlag.Value);

            if (tradeDateFrom.HasValue)
                query = query.Where(x => x.TradeDate >= tradeDateFrom);

            if (tradeDateTo.HasValue)
                query = query.Where(x => x.TradeDate <= tradeDateTo);

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
                        join u2 in _userRepository.Table
                        on d.OperatorCode equals u2.Code into temp3
                        from operateUser in temp3.DefaultIfEmpty()
                        join u3 in _userRepository.Table
                        on d.Beneficiary equals u3.Code into temp4
                        from beneficiaryUser in temp4.DefaultIfEmpty()
                        join u4 in _userRepository.Table
                        on d.UpdateUser equals u4.Code into temp5
                        from updateUser in temp5.DefaultIfEmpty()
                        select new { d, account, importUser, operateUser, beneficiaryUser, updateUser };

            var result = infos.ToList().Select(x => new DailyRecord
            {
                Id = x.d.Id,
                AccountId = x.d.AccountId,
                AccountName = x.account == null ? null : x.account.Name + " - " + x.account.SecurityCompanyName + " - " + x.account.AttributeName,
                ActualAmount = x.d.ActualAmount,
                AuditFlag = x.d.AuditFlag,
                AuditNo = x.d.AuditNo,
                AuditTime = x.d.AuditTime,
                Beneficiary = x.d.Beneficiary,
                BeneficiaryName = x.beneficiaryUser == null ? null : x.beneficiaryUser.Name,
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
                OperatorCode = x.d.OperatorCode,
                OperatorName = x.operateUser == null ? null : x.operateUser.Name,
                Remarks = x.d.Remarks,
                SplitNo = x.d.SplitNo,
                StampDuty = x.d.StampDuty,
                StockCode = x.d.StockCode,
                StockHolderCode = x.d.StockHolderCode,
                StockName = x.d.StockName,
                TradeDate = x.d.TradeDate,
                TradeTime = x.d.TradeTime,
                TradeType = x.d.TradeType,
                UpdateTime = x.d.UpdateTime,
                UpdateUser = x.d.UpdateUser,
                UpdateUserName = x.updateUser == null ? null : x.updateUser.Name,
            }
            )
            .ToList();

            return result;
        }

        public virtual void BatchInsertDailyRecords(IList<DailyRecord> dailyRecords)
        {
            if (dailyRecords == null)
                throw new ArgumentNullException(nameof(dailyRecords));

            _dailyRepository.BatchInsert(dailyRecords, 1000);
        }

        public virtual void InsertDailyRecords(IList<DailyRecord> dailyRecords)
        {
            if (dailyRecords == null)
                throw new ArgumentNullException(nameof(dailyRecords));

            _dailyRepository.Insert(dailyRecords);
        }

        public virtual DailyRecord GetDailyRecordById(int id)
        {
            return _dailyRepository.GetById(id);
        }

        public virtual IList<DailyRecord> GetDailyRecordsByIds(int[] recordIds)
        {
            if (recordIds == null)
                throw new ArgumentNullException(nameof(recordIds));

            var query = _dailyRepository.Table;
            query = query.Where(x => recordIds.Contains(x.Id));

            return query.ToList();
        }

        public virtual void UpdateDailyRecords(IList<DailyRecord> dailyRecords)
        {
            if (dailyRecords == null)
                throw new ArgumentNullException(nameof(dailyRecords));

            _dailyRepository.Update(dailyRecords);
        }

        public virtual void DeleteDailyRecords(int[] ids)
        {
            if (ids == null)
                throw new ArgumentNullException(nameof(ids));

            var query = _dailyRepository.Table;
            query = query.Where(x => ids.Contains(x.Id));

            _dailyRepository.Delete(query.ToList());
        }

        public virtual void DeleteDailyRecords(IList<DailyRecord> dailyRecords)
        {
            if (dailyRecords == null)
                throw new ArgumentNullException(nameof(dailyRecords));

            _dailyRepository.Delete(dailyRecords);
        }

        public virtual void InsertDailyRecord(DailyRecord dailyRecord)
        {
            if (dailyRecord == null)
                throw new ArgumentNullException(nameof(dailyRecord));

            _dailyRepository.Insert(dailyRecord);
        }

        public virtual void UpdateDailyRecord(DailyRecord dailyRecord)
        {
            if (dailyRecord == null)
                throw new ArgumentNullException(nameof(dailyRecord));

            _dailyRepository.Update(dailyRecord);
        }

        public virtual IList<int> GetTradingAccountIds()
        {
            var query = _dailyRepository.Table.Select(x => x.AccountId).Distinct();

            return query.ToList();
        }

        #endregion Methods
    }
}