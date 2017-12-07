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

        private IList<DataRow> _skippedRecords = null;

        private readonly List<string> _buyTexts = new List<string> { "融券回购", "融资借入", "红股入帐", "新股入帐", "红股入账", "新股入账" };
        private readonly List<string> _sellTexts = new List<string> { "融券购回", "卖券还款" };

        #endregion Fields

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

        #region 数据校验

        /// <summary>
        /// 导入数据验证
        /// </summary>
        /// <param name="importDataTable"></param>
        /// <param name="importOperation"></param>
        private void ImportRecordsCheck(DataTable importDataTable, RecordImportOperationEntity importOperation)
        {
            //验证交易类别列的值
            string[] tradeTypeNames = new string[] { "短差", "日内", "波段", "目标" };
            foreach (DataRow row in importDataTable.Rows)
            {
                if (!tradeTypeNames.Contains(row["交易类别"].ToString().Trim()))
                {
                    throw new Exception(@"交易类别列中的值设置有误！只能为【短差、日内/波段/目标】之一。");
                }
            }

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

        #endregion 数据校验

        #region 数据导入

        /// <summary>
        /// 从导入数据DataTable从获取交易数据
        /// </summary>
        /// <param name="isDelivery">
        /// True：交割单
        /// Flase：当日委托
        /// </param>
        /// <param name="importOperation"></param>
        /// <param name="importDataTable"></param>
        /// <param name="columnList"></param>
        /// <returns></returns>
        private IList<DailyRecord> ObtainTradeDataFromImportDataTable(bool isDelivery, RecordImportOperationEntity importOperation, DataTable importDataTable, Dictionary<string, string> columnList)
        {
            var tradeRecords = new List<DailyRecord>();

            //当前账户信息
            AccountInfo currentAccount = isDelivery ? null : _accountService.GetAccountInfoById(importOperation.AccountId);

            DailyRecord record = null;

            List<DataRow> validRecords = new List<DataRow>();

            //过滤记录
            if (isDelivery)
            {
                //validRecords = importDataTable.AsEnumerable().Where(x => CommonHelper.IsNumberAndAlphabet(x.Field<string>(columnList[nameof(record.ContractNo)]).Trim())
                //                                                                                             && CommonHelper.StringToDecimal(x.Field<string>(columnList[nameof(record.DealVolume)]).Trim()) != 0
                //                                                                                             && CommonHelper.StringToDecimal(x.Field<string>(columnList[nameof(record.ActualAmount)]).Trim()) != 0)
                //                                                                               .ToList();
                validRecords = importDataTable.AsEnumerable().Where(x => CommonHelper.StringToDecimal(x.Field<string>(columnList[nameof(record.DealVolume)]).Trim()) != 0
                                                                                                            && CommonHelper.StringToDecimal(x.Field<string>(columnList[nameof(record.ActualAmount)]).Trim()) != 0)
                                                                                               .ToList();
            }
            else
            {
                //validRecords = importDataTable.AsEnumerable().Where(x => CommonHelper.StringToDecimal(x.Field<string>(columnList[nameof(record.DealPrice)]).Trim()) != 0
                //                                                                                             && CommonHelper.StringToDecimal(x.Field<string>(columnList[nameof(record.DealVolume)]).Trim()) != 0)
                //                                                                              .ToList();

                validRecords = importDataTable.AsEnumerable().ToList();
            }

            _skippedRecords = importDataTable.AsEnumerable().Where(x => !validRecords.Contains(x)).ToList();

            foreach (DataRow row in validRecords)
            {
                record = new DailyRecord();

                //买卖标志
                if (!string.IsNullOrEmpty(columnList[nameof(record.DealFlag)]))
                {
                    if (_buyTexts.Contains(row[columnList[nameof(record.DealFlag)]].ToString().Trim()) || row[columnList[nameof(record.DealFlag)]].ToString().Trim().IndexOf("买") > -1)
                        record.DealFlag = true;
                    else if (_sellTexts.Contains(row[columnList[nameof(record.DealFlag)]].ToString().Trim()) || row[columnList[nameof(record.DealFlag)]].ToString().Trim().IndexOf("卖") > -1)
                        record.DealFlag = false;
                    else
                    {
                        _skippedRecords.Add(row);
                        continue;
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(columnList[nameof(record.ActualAmount)]))
                    {
                        if (CommonHelper.StringToDecimal(row[columnList[nameof(record.ActualAmount)]].ToString().Trim()) > 0)
                            record.DealFlag = false;
                        else
                            record.DealFlag = true;
                    }

                }

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
                //交易类别
                record.SetTradeType(row[columnList[nameof(record.TradeType)]].ToString().Trim());
                //受益人
                record.SetBeneficiary(importOperation.BandPrincipal, importOperation.TargetPrincipal);

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
                if (record.DealPrice > 0)
                {            
                    var dealVolume = CommonHelper.StringToDecimal(row[columnList[nameof(record.DealVolume)]].ToString().Trim());
                    record.DealVolume = record.DealFlag ? CommonHelper.ConvertToPositive(dealVolume) : CommonHelper.ConvertToNegtive(dealVolume);
                }
                //成交金额
                if (string.IsNullOrEmpty(columnList[nameof(record.DealAmount)]))
                    record.DealAmount = record.DealPrice * Math.Abs(record.DealVolume);
                else
                    record.DealAmount = CommonHelper.StringToDecimal(row[columnList[nameof(record.DealAmount)]].ToString().Trim());

                ///交割单
                if (isDelivery)
                {
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
                }
                ///当日委托
                else
                {
                    //委托数量
                    record.EntrustVolume = CommonHelper.StringToDecimal(row[columnList[nameof(record.EntrustVolume)]].ToString().Trim());
                    //委托价格
                    record.EntrustPrice = CommonHelper.StringToDecimal(row[columnList[nameof(record.EntrustPrice)]].ToString().Trim());
                    //委托金额
                    if (string.IsNullOrEmpty(columnList[nameof(record.EntrustAmount)]))
                        record.EntrustAmount = record.EntrustVolume * record.EntrustPrice;
                    else
                        record.EntrustAmount = CommonHelper.StringToDecimal(row[columnList[nameof(record.EntrustAmount)]].ToString().Trim());

                    //佣金
                    record.Commission = record.DealAmount * currentAccount.CommissionRate;
                    //印花税
                    record.StampDuty = record.DealFlag ? 0 : record.DealAmount * currentAccount.StampDutyRate;
                    //杂费
                    record.Incidentals = 0;
                    //发生金额
                    record.ActualAmount = (record.DealFlag ? CommonHelper.ConvertToNegtive(record.DealAmount) : CommonHelper.ConvertToPositive(record.DealAmount)) - record.Commission - record.StampDuty - record.Incidentals;
                }

                //成交编号
                if (!string.IsNullOrEmpty(columnList[nameof(record.DealNo)]))
                    record.DealNo = row[columnList[nameof(record.DealNo)]].ToString().Trim();
                //合同编号
                if (!string.IsNullOrEmpty(columnList[nameof(record.ContractNo)]))
                    record.ContractNo = row[columnList[nameof(record.ContractNo)]].ToString().Trim();
                //股东代码
                if (!string.IsNullOrEmpty(columnList[nameof(record.StockHolderCode)]))
                    record.StockHolderCode = row[columnList[nameof(record.StockHolderCode)]].ToString().Trim();
                //备注
                if (!string.IsNullOrEmpty(columnList[nameof(record.Remarks)]))
                    record.Remarks = row[columnList[nameof(record.Remarks)]].ToString().Trim();

                tradeRecords.Add(record);
            }

            return tradeRecords;
        }

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
                //中银信用
                case EnumLibrary.SecurityAccount.BOCI_C:
                    result = EntrustImportBOCI_C(importOperation, importDataTable);
                    break;

                //中银普通
                case EnumLibrary.SecurityAccount.BOCI_N:
                    result = EntrustImportBOCI_N(importOperation, importDataTable);
                    break;

                //财通信用
                case EnumLibrary.SecurityAccount.CaiTong_C:
                    result = EntrustImportCaiTong_C(importOperation, importDataTable);
                    break;

                //财通普通
                case EnumLibrary.SecurityAccount.CaiTong_N:
                    result = EntrustImportCaiTong_N(importOperation, importDataTable);
                    break;

                //中信信用
                case EnumLibrary.SecurityAccount.CITIC_C:
                    result = EntrustImportCITIC_C(importOperation, importDataTable);
                    break;

                //中信普通
                case EnumLibrary.SecurityAccount.CITIC_N:
                    result = EntrustImportCITIC_N(importOperation, importDataTable);
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

                //海通普通
                case EnumLibrary.SecurityAccount.HaiTong_N:
                    result = EntrustImportHaiTong_N(importOperation, importDataTable);
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

                //国金普通
                case EnumLibrary.SecurityAccount.SinoLink_N:
                    result = EntrustImportSinoLink_N(importOperation, importDataTable);
                    break;

                //招商普通
                case EnumLibrary.SecurityAccount.ZhaoShang_N:
                    result = EntrustImportZhaoShang_N(importOperation, importDataTable);
                    break;

                //浙商信用
                case EnumLibrary.SecurityAccount.ZheShang_C:
                    result = EntrustImportZheShang_C(importOperation, importDataTable);
                    break;

                //浙商普通
                case EnumLibrary.SecurityAccount.ZheShang_N:
                    result = EntrustImportZheShang_N(importOperation, importDataTable);
                    break;

                default:
                    throw new Exception($@"券商【{CTMHelper.GetSecurityAccountName((int)securityAccount)}】的委托数据导入功能未实现，请联系管理员！");
            }
            return result;
        }

        #endregion 当日委托--券商选择
        #region 当日委托--中银证券（信用）

        /// <summary>
        /// 当日委托--中银证券（普通）
        /// </summary>
        /// <param name="importOperation"></param>
        /// <param name="importDataTable"></param>
        /// <returns></returns>
        private IList<DailyRecord> EntrustImportBOCI_C(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            Dictionary<string, string> columnList = new Dictionary<string, string>();
            DailyRecord record = null;

            columnList.Add(nameof(record.TradeDate), null);
            columnList.Add(nameof(record.TradeTime), "委托时间");
            columnList.Add(nameof(record.StockCode), "证券代码");
            columnList.Add(nameof(record.StockName), "证券名称");
            columnList.Add(nameof(record.DealFlag), "买卖方向");
            columnList.Add(nameof(record.EntrustVolume), "委托数量");
            columnList.Add(nameof(record.EntrustPrice), "委托价格");
            columnList.Add(nameof(record.EntrustAmount), null);
            columnList.Add(nameof(record.DealPrice), "成交均价");
            columnList.Add(nameof(record.DealVolume), "成交数量");
            columnList.Add(nameof(record.DealAmount), null);
            columnList.Add(nameof(record.StockHolderCode), "股东帐户");
            columnList.Add(nameof(record.DealNo), "合同编号");
            columnList.Add(nameof(record.ContractNo), "合同编号");
            columnList.Add(nameof(record.Remarks), "买卖方向");
            columnList.Add(nameof(record.TradeType), "交易类别");

            List<string> templateColumnNames = columnList.Values.Where(x => !string.IsNullOrEmpty(x)).ToList();
            this._dataImportService.DataFormatCheck(templateColumnNames, importDataTable);

            var tradeRecords = ObtainTradeDataFromImportDataTable(false, importOperation, importDataTable, columnList);

            return tradeRecords;
        }

        #endregion 当日委托--中银证券（信用）

        #region 当日委托--中银证券（普通）

        /// <summary>
        /// 当日委托--中银证券（普通）
        /// </summary>
        /// <param name="importOperation"></param>
        /// <param name="importDataTable"></param>
        /// <returns></returns>
        private IList<DailyRecord> EntrustImportBOCI_N(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            Dictionary<string, string> columnList = new Dictionary<string, string>();
            DailyRecord record = null;

            columnList.Add(nameof(record.TradeDate), null);
            columnList.Add(nameof(record.TradeTime), "委托时间");
            columnList.Add(nameof(record.StockCode), "证券代码");
            columnList.Add(nameof(record.StockName), "证券名称");
            columnList.Add(nameof(record.DealFlag), "操作");
            columnList.Add(nameof(record.EntrustVolume), "委托数量");
            columnList.Add(nameof(record.EntrustPrice), "委托价格");
            columnList.Add(nameof(record.EntrustAmount), null);
            columnList.Add(nameof(record.DealPrice), "成交均价");
            columnList.Add(nameof(record.DealVolume), "成交数量");
            columnList.Add(nameof(record.DealAmount), null);
            columnList.Add(nameof(record.StockHolderCode), null);
            columnList.Add(nameof(record.DealNo), "合同编号");
            columnList.Add(nameof(record.ContractNo), "合同编号");
            columnList.Add(nameof(record.Remarks), "操作");
            columnList.Add(nameof(record.TradeType), "交易类别");

            List<string> templateColumnNames = columnList.Values.Where(x => !string.IsNullOrEmpty(x)).ToList();
            this._dataImportService.DataFormatCheck(templateColumnNames, importDataTable);

            var tradeRecords = ObtainTradeDataFromImportDataTable(false, importOperation, importDataTable, columnList);

            return tradeRecords;
        }

        #endregion 当日委托--中银证券（普通）

        #region 当日委托--财通证券（信用）

        /// <summary>
        /// 当日委托--财通证券（信用）
        /// </summary>
        /// <param name="importOperation"></param>
        /// <param name="importDataTable"></param>
        /// <returns></returns>
        private IList<DailyRecord> EntrustImportCaiTong_C(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            Dictionary<string, string> columnList = new Dictionary<string, string>();
            DailyRecord record = null;

            columnList.Add(nameof(record.TradeDate), null);
            columnList.Add(nameof(record.TradeTime), "委托时间");
            columnList.Add(nameof(record.StockCode), "证券代码");
            columnList.Add(nameof(record.StockName), "证券名称");
            columnList.Add(nameof(record.DealFlag), "操作");
            columnList.Add(nameof(record.EntrustVolume), "委托数量");
            columnList.Add(nameof(record.EntrustPrice), "委托价格");
            columnList.Add(nameof(record.EntrustAmount), null);
            columnList.Add(nameof(record.DealPrice), "成交均价");
            columnList.Add(nameof(record.DealVolume), "成交数量");
            columnList.Add(nameof(record.DealAmount), null);
            columnList.Add(nameof(record.StockHolderCode), "股东帐户");
            columnList.Add(nameof(record.DealNo), "合同编号");
            columnList.Add(nameof(record.ContractNo), "合同编号");
            columnList.Add(nameof(record.Remarks), "操作");
            columnList.Add(nameof(record.TradeType), "交易类别");

            List<string> templateColumnNames = columnList.Values.Where(x => !string.IsNullOrEmpty(x)).ToList();
            this._dataImportService.DataFormatCheck(templateColumnNames, importDataTable);

            var tradeRecords = ObtainTradeDataFromImportDataTable(false, importOperation, importDataTable, columnList);

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
            Dictionary<string, string> columnList = new Dictionary<string, string>();
            DailyRecord record = null;

            columnList.Add(nameof(record.TradeDate), null);
            columnList.Add(nameof(record.TradeTime), "委托时间");
            columnList.Add(nameof(record.StockCode), "证券代码");
            columnList.Add(nameof(record.StockName), "证券名称");
            columnList.Add(nameof(record.DealFlag), "操作");
            columnList.Add(nameof(record.EntrustVolume), "委托数量");
            columnList.Add(nameof(record.EntrustPrice), "委托价格");
            columnList.Add(nameof(record.EntrustAmount), null);
            columnList.Add(nameof(record.DealPrice), "成交均价");
            columnList.Add(nameof(record.DealVolume), "成交数量");
            columnList.Add(nameof(record.DealAmount), null);
            columnList.Add(nameof(record.StockHolderCode), "股东帐户");
            columnList.Add(nameof(record.DealNo), "合同编号");
            columnList.Add(nameof(record.ContractNo), "合同编号");
            columnList.Add(nameof(record.Remarks), "操作");
            columnList.Add(nameof(record.TradeType), "交易类别");

            List<string> templateColumnNames = columnList.Values.Where(x => !string.IsNullOrEmpty(x)).ToList();
            this._dataImportService.DataFormatCheck(templateColumnNames, importDataTable);

            var tradeRecords = ObtainTradeDataFromImportDataTable(false, importOperation, importDataTable, columnList);

            return tradeRecords;
        }

        #endregion 当日委托--财通证券（普通）

        #region 当日委托--中信证券（信用）

        /// <summary>
        /// 当日委托--中信证券（信用）
        /// </summary>
        /// <param name="importOperation"></param>
        /// <param name="importDataTable"></param>
        /// <returns></returns>
        private IList<DailyRecord> EntrustImportCITIC_C(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            Dictionary<string, string> columnList = new Dictionary<string, string>();
            DailyRecord record = null;

            columnList.Add(nameof(record.TradeDate), null);
            columnList.Add(nameof(record.TradeTime), "委托时间");
            columnList.Add(nameof(record.StockCode), "证券代码");
            columnList.Add(nameof(record.StockName), "证券名称");
            columnList.Add(nameof(record.DealFlag), "买卖");
            columnList.Add(nameof(record.EntrustVolume), "委托数量");
            columnList.Add(nameof(record.EntrustPrice), "委托价格");
            columnList.Add(nameof(record.EntrustAmount), null);
            columnList.Add(nameof(record.DealPrice), "成交价格");
            columnList.Add(nameof(record.DealVolume), "成交数量");
            columnList.Add(nameof(record.DealAmount), null);
            columnList.Add(nameof(record.StockHolderCode), "股东代码");
            columnList.Add(nameof(record.DealNo), "委托编号");
            columnList.Add(nameof(record.ContractNo), "委托编号");
            columnList.Add(nameof(record.Remarks), "买卖");
            columnList.Add(nameof(record.TradeType), "交易类别");

            List<string> templateColumnNames = columnList.Values.Where(x => !string.IsNullOrEmpty(x)).ToList();
            this._dataImportService.DataFormatCheck(templateColumnNames, importDataTable);

            var tradeRecords = ObtainTradeDataFromImportDataTable(false, importOperation, importDataTable, columnList);

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
            Dictionary<string, string> columnList = new Dictionary<string, string>();
            DailyRecord record = null;

            columnList.Add(nameof(record.TradeDate), null);
            columnList.Add(nameof(record.TradeTime), "委托时间");
            columnList.Add(nameof(record.StockCode), "证券代码");
            columnList.Add(nameof(record.StockName), "证券名称");
            columnList.Add(nameof(record.DealFlag), "买卖");
            columnList.Add(nameof(record.EntrustVolume), "委托数量");
            columnList.Add(nameof(record.EntrustPrice), "委托价格");
            columnList.Add(nameof(record.EntrustAmount), null);
            columnList.Add(nameof(record.DealPrice), "成交价格");
            columnList.Add(nameof(record.DealVolume), "成交数量");
            columnList.Add(nameof(record.DealAmount), null);
            columnList.Add(nameof(record.StockHolderCode), null);
            columnList.Add(nameof(record.DealNo), null);
            columnList.Add(nameof(record.ContractNo), null);
            columnList.Add(nameof(record.Remarks), "买卖");
            columnList.Add(nameof(record.TradeType), "交易类别");

            List<string> templateColumnNames = columnList.Values.Where(x => !string.IsNullOrEmpty(x)).ToList();
            this._dataImportService.DataFormatCheck(templateColumnNames, importDataTable);

            var tradeRecords = ObtainTradeDataFromImportDataTable(false, importOperation, importDataTable, columnList);

            return tradeRecords;
        }

        #endregion 当日委托--中信证券（普通）

        #region 当日委托--方正证券（普通）

        /// <summary>
        /// 当日委托--方正证券（普通）
        /// </summary>
        /// <param name="importOperation"></param>
        /// <param name="importDataTable"></param>
        /// <returns></returns>
        private IList<DailyRecord> EntrustImportFounder_N(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            Dictionary<string, string> columnList = new Dictionary<string, string>();
            DailyRecord record = null;

            columnList.Add(nameof(record.TradeDate), null);
            columnList.Add(nameof(record.TradeTime), "委托时间");
            columnList.Add(nameof(record.StockCode), "证券代码");
            columnList.Add(nameof(record.StockName), "证券名称");
            columnList.Add(nameof(record.DealFlag), "操作");
            columnList.Add(nameof(record.EntrustVolume), "委托数量");
            columnList.Add(nameof(record.EntrustPrice), "委托价格");
            columnList.Add(nameof(record.EntrustAmount), null);
            columnList.Add(nameof(record.DealPrice), "成交均价");
            columnList.Add(nameof(record.DealVolume), "成交数量");
            columnList.Add(nameof(record.DealAmount), null);
            columnList.Add(nameof(record.StockHolderCode), null);
            columnList.Add(nameof(record.DealNo), "合同编号");
            columnList.Add(nameof(record.ContractNo), "合同编号");
            columnList.Add(nameof(record.Remarks), "备注");
            columnList.Add(nameof(record.TradeType), "交易类别");

            List<string> templateColumnNames = columnList.Values.Where(x => !string.IsNullOrEmpty(x)).ToList();
            this._dataImportService.DataFormatCheck(templateColumnNames, importDataTable);

            var tradeRecords = ObtainTradeDataFromImportDataTable(false, importOperation, importDataTable, columnList);

            return tradeRecords;
        }

        #endregion 当日委托--方正证券（普通）

        #region 当日委托--银河证券（普通）

        /// <summary>
        /// 当日委托--银河证券（普通）
        /// </summary>
        /// <param name="importOperation"></param>
        /// <param name="importDataTable"></param>
        /// <returns></returns>
        private IList<DailyRecord> EntrustImportGalaxy_N(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            Dictionary<string, string> columnList = new Dictionary<string, string>();
            DailyRecord record = null;

            columnList.Add(nameof(record.TradeDate), "操作日期");
            columnList.Add(nameof(record.TradeTime), "委托时间");
            columnList.Add(nameof(record.StockCode), "证券代码");
            columnList.Add(nameof(record.StockName), "证券名称");
            columnList.Add(nameof(record.DealFlag), "操作");
            columnList.Add(nameof(record.EntrustVolume), "委托数量");
            columnList.Add(nameof(record.EntrustPrice), "委托价格");
            columnList.Add(nameof(record.EntrustAmount), null);
            columnList.Add(nameof(record.DealPrice), "成交均价");
            columnList.Add(nameof(record.DealVolume), "成交数量");
            columnList.Add(nameof(record.DealAmount), "成交金额");
            columnList.Add(nameof(record.StockHolderCode), null);
            columnList.Add(nameof(record.DealNo), "合同编号");
            columnList.Add(nameof(record.ContractNo), "合同编号");
            columnList.Add(nameof(record.Remarks), "操作");
            columnList.Add(nameof(record.TradeType), "交易类别");

            List<string> templateColumnNames = columnList.Values.Where(x => !string.IsNullOrEmpty(x)).ToList();
            this._dataImportService.DataFormatCheck(templateColumnNames, importDataTable);

            var tradeRecords = ObtainTradeDataFromImportDataTable(false, importOperation, importDataTable, columnList);

            return tradeRecords;
        }

        #endregion 当日委托--银河证券（普通）

        #region 当日委托--国泰证券（信用）

        /// <summary>
        /// 当日委托--国泰证券（信用）
        /// </summary>
        /// <param name="importOperation"></param>
        /// <param name="importDataTable"></param>
        /// <returns></returns>
        private IList<DailyRecord> EntrustImportGuoTai_C(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            Dictionary<string, string> columnList = new Dictionary<string, string>();
            DailyRecord record = null;

            columnList.Add(nameof(record.TradeDate), "成交日期");
            columnList.Add(nameof(record.TradeTime), "成交时间");
            columnList.Add(nameof(record.StockCode), "证券代码");
            columnList.Add(nameof(record.StockName), "证券名称");
            columnList.Add(nameof(record.DealFlag), "买卖标志");
            columnList.Add(nameof(record.EntrustVolume), "委托数量");
            columnList.Add(nameof(record.EntrustPrice), "委托价格");
            columnList.Add(nameof(record.EntrustAmount), null);
            columnList.Add(nameof(record.DealPrice), "成交价格");
            columnList.Add(nameof(record.DealVolume), "成交数量");
            columnList.Add(nameof(record.DealAmount),"成交金额");
            columnList.Add(nameof(record.StockHolderCode), "股东代码");
            columnList.Add(nameof(record.DealNo), "成交编号");
            columnList.Add(nameof(record.ContractNo), "成交编号");
            columnList.Add(nameof(record.Remarks), "买卖标志");
            columnList.Add(nameof(record.TradeType), "交易类别");

            List<string> templateColumnNames = columnList.Values.Where(x => !string.IsNullOrEmpty(x)).ToList();
            this._dataImportService.DataFormatCheck(templateColumnNames, importDataTable);

            var tradeRecords = ObtainTradeDataFromImportDataTable(false, importOperation, importDataTable, columnList);

            return tradeRecords;
        }

        #endregion 当日委托--国泰证券（信用）

        #region 当日委托--国泰证券（普通）

        /// <summary>
        /// 当日委托--国泰证券（普通）
        /// </summary>
        /// <param name="importOperation"></param>
        /// <param name="importDataTable"></param>
        /// <returns></returns>
        private IList<DailyRecord> EntrustImportGuoTai_N(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            Dictionary<string, string> columnList = new Dictionary<string, string>();
            DailyRecord record = null;

            columnList.Add(nameof(record.TradeDate), "成交日期");
            columnList.Add(nameof(record.TradeTime), "成交时间");
            columnList.Add(nameof(record.StockCode), "证券代码");
            columnList.Add(nameof(record.StockName), "证券名称");
            columnList.Add(nameof(record.DealFlag), "买卖标志");
            columnList.Add(nameof(record.EntrustVolume), "委托数量");
            columnList.Add(nameof(record.EntrustPrice), "委托价格");
            columnList.Add(nameof(record.EntrustAmount), null);
            columnList.Add(nameof(record.DealPrice), "成交价格");
            columnList.Add(nameof(record.DealVolume), "成交数量");
            columnList.Add(nameof(record.DealAmount), "成交金额");
            columnList.Add(nameof(record.StockHolderCode), "股东代码");
            columnList.Add(nameof(record.DealNo), "委托编号");
            columnList.Add(nameof(record.ContractNo), "委托编号");
            columnList.Add(nameof(record.Remarks), "买卖标志");
            columnList.Add(nameof(record.TradeType), "交易类别");

            List<string> templateColumnNames = columnList.Values.Where(x => !string.IsNullOrEmpty(x)).ToList();
            this._dataImportService.DataFormatCheck(templateColumnNames, importDataTable);

            var tradeRecords = ObtainTradeDataFromImportDataTable(false, importOperation, importDataTable, columnList);

            return tradeRecords;
        }

        #endregion 当日委托--国泰证券（普通）

        #region 当日委托--海通证券（普通）

        /// <summary>
        /// 当日委托--海通证券（普通）
        /// </summary>
        /// <param name="importOperation"></param>
        /// <param name="importDataTable"></param>
        /// <returns></returns>
        private IList<DailyRecord> EntrustImportHaiTong_N(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            Dictionary<string, string> columnList = new Dictionary<string, string>();
            DailyRecord record = null;

            columnList.Add(nameof(record.TradeDate), null);
            columnList.Add(nameof(record.TradeTime), "申报时间");
            columnList.Add(nameof(record.StockCode), "证券代码");
            columnList.Add(nameof(record.StockName), "证券名称");
            columnList.Add(nameof(record.DealFlag), "操作");
            columnList.Add(nameof(record.EntrustVolume), "委托数量");
            columnList.Add(nameof(record.EntrustPrice), "委托价格");
            columnList.Add(nameof(record.EntrustAmount), null);
            columnList.Add(nameof(record.DealPrice), "成交均价");
            columnList.Add(nameof(record.DealVolume), "成交数量");
            columnList.Add(nameof(record.DealAmount), null);
            columnList.Add(nameof(record.StockHolderCode), null);
            columnList.Add(nameof(record.DealNo), "合同编号");
            columnList.Add(nameof(record.ContractNo), "合同编号");
            columnList.Add(nameof(record.Remarks), "操作");
            columnList.Add(nameof(record.TradeType), "交易类别");

            List<string> templateColumnNames = columnList.Values.Where(x => !string.IsNullOrEmpty(x)).ToList();
            this._dataImportService.DataFormatCheck(templateColumnNames, importDataTable);

            var tradeRecords = ObtainTradeDataFromImportDataTable(false, importOperation, importDataTable, columnList);

            return tradeRecords;
        }

        #endregion 当日委托--海通证券（普通）

        #region 当日委托--华泰证券（信用）

        /// <summary>
        /// 当日委托--华泰证券（信用）
        /// </summary>
        /// <param name="importOperation"></param>
        /// <param name="importDataTable"></param>
        /// <returns></returns>
        private IList<DailyRecord> EntrustImportHuaTai_C(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            Dictionary<string, string> columnList = new Dictionary<string, string>();
            DailyRecord record = null;

            columnList.Add(nameof(record.TradeDate), "委托日期");
            columnList.Add(nameof(record.TradeTime), "委托时间");
            columnList.Add(nameof(record.StockCode), "证券代码");
            columnList.Add(nameof(record.StockName), "证券名称");
            columnList.Add(nameof(record.DealFlag), "操作");
            columnList.Add(nameof(record.EntrustVolume), "委托数量");
            columnList.Add(nameof(record.EntrustPrice), "委托价格");
            columnList.Add(nameof(record.EntrustAmount), null);
            columnList.Add(nameof(record.DealPrice), "成交均价");
            columnList.Add(nameof(record.DealVolume), "成交数量");
            columnList.Add(nameof(record.DealAmount), null);
            columnList.Add(nameof(record.StockHolderCode), "股东帐户");
            columnList.Add(nameof(record.DealNo), "合同编号");
            columnList.Add(nameof(record.ContractNo), "合同编号");
            columnList.Add(nameof(record.Remarks), "操作");
            columnList.Add(nameof(record.TradeType), "交易类别");

            List<string> templateColumnNames = columnList.Values.Where(x => !string.IsNullOrEmpty(x)).ToList();
            this._dataImportService.DataFormatCheck(templateColumnNames, importDataTable);

            var tradeRecords = ObtainTradeDataFromImportDataTable(false, importOperation, importDataTable, columnList);

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
            Dictionary<string, string> columnList = new Dictionary<string, string>();
            DailyRecord record = null;

            columnList.Add(nameof(record.TradeDate), "委托日期");
            columnList.Add(nameof(record.TradeTime), "委托时间");
            columnList.Add(nameof(record.StockCode), "证券代码");
            columnList.Add(nameof(record.StockName), "证券名称");
            columnList.Add(nameof(record.DealFlag), "操作");
            columnList.Add(nameof(record.EntrustVolume), "委托数量");
            columnList.Add(nameof(record.EntrustPrice), "委托价格");
            columnList.Add(nameof(record.EntrustAmount), null);
            columnList.Add(nameof(record.DealPrice), "成交均价");
            columnList.Add(nameof(record.DealVolume), "成交数量");
            columnList.Add(nameof(record.DealAmount), null);
            columnList.Add(nameof(record.StockHolderCode), "股东帐户");
            columnList.Add(nameof(record.DealNo), "合同编号");
            columnList.Add(nameof(record.ContractNo), "合同编号");
            columnList.Add(nameof(record.Remarks), "操作");
            columnList.Add(nameof(record.TradeType), "交易类别");

            List<string> templateColumnNames = columnList.Values.Where(x => !string.IsNullOrEmpty(x)).ToList();
            this._dataImportService.DataFormatCheck(templateColumnNames, importDataTable);

            var tradeRecords = ObtainTradeDataFromImportDataTable(false, importOperation, importDataTable, columnList);

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
            Dictionary<string, string> columnList = new Dictionary<string, string>();
            DailyRecord record = null;

            columnList.Add(nameof(record.TradeDate), null);
            columnList.Add(nameof(record.TradeTime), "委托时间");
            columnList.Add(nameof(record.StockCode), "证券代码");
            columnList.Add(nameof(record.StockName), "证券名称");
            columnList.Add(nameof(record.DealFlag), "操作");
            columnList.Add(nameof(record.EntrustVolume), "委托数量");
            columnList.Add(nameof(record.EntrustPrice), "委托价格");
            columnList.Add(nameof(record.EntrustAmount), null);
            columnList.Add(nameof(record.DealPrice), "成交均价");
            columnList.Add(nameof(record.DealVolume), "成交数量");
            columnList.Add(nameof(record.DealAmount), "成交金额");
            columnList.Add(nameof(record.StockHolderCode), null);
            columnList.Add(nameof(record.DealNo), "合同编号");
            columnList.Add(nameof(record.ContractNo), "合同编号");
            columnList.Add(nameof(record.Remarks), "操作");
            columnList.Add(nameof(record.TradeType), "交易类别");

            List<string> templateColumnNames = columnList.Values.Where(x => !string.IsNullOrEmpty(x)).ToList();
            this._dataImportService.DataFormatCheck(templateColumnNames, importDataTable);

            var tradeRecords = ObtainTradeDataFromImportDataTable(false, importOperation, importDataTable, columnList);

            return tradeRecords;
        }

        #endregion 当日委托--申万证券（普通）

        #region 当日委托--国金证券（普通）

        /// <summary>
        /// 当日委托--国金证券（普通）
        /// </summary>
        /// <param name="importOperation"></param>
        /// <param name="importDataTable"></param>
        /// <returns></returns>
        private IList<DailyRecord> EntrustImportSinoLink_N(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            Dictionary<string, string> columnList = new Dictionary<string, string>();
            DailyRecord record = null;

            columnList.Add(nameof(record.TradeDate), "委托日期");
            columnList.Add(nameof(record.TradeTime), "委托时间");
            columnList.Add(nameof(record.StockCode), "证券代码");
            columnList.Add(nameof(record.StockName), "证券名称");
            columnList.Add(nameof(record.DealFlag), "操作");
            columnList.Add(nameof(record.EntrustVolume), "委托数量");
            columnList.Add(nameof(record.EntrustPrice), "委托价格");
            columnList.Add(nameof(record.EntrustAmount), null);
            columnList.Add(nameof(record.DealPrice), "成交均价");
            columnList.Add(nameof(record.DealVolume), "成交数量");
            columnList.Add(nameof(record.DealAmount), null);
            columnList.Add(nameof(record.StockHolderCode), null);
            columnList.Add(nameof(record.DealNo), "合同编号");
            columnList.Add(nameof(record.ContractNo), "合同编号");
            columnList.Add(nameof(record.Remarks), "操作");
            columnList.Add(nameof(record.TradeType), "交易类别");

            List<string> templateColumnNames = columnList.Values.Where(x => !string.IsNullOrEmpty(x)).ToList();
            this._dataImportService.DataFormatCheck(templateColumnNames, importDataTable);

            var tradeRecords = ObtainTradeDataFromImportDataTable(false, importOperation, importDataTable, columnList);

            return tradeRecords;
        }

        #endregion 当日委托--国金证券（普通）

        #region 当日委托--招商证券（普通）

        /// <summary>
        /// 当日委托--招商证券（普通）
        /// </summary>
        /// <param name="importOperation"></param>
        /// <param name="importDataTable"></param>
        /// <returns></returns>
        private IList<DailyRecord> EntrustImportZhaoShang_N(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            Dictionary<string, string> columnList = new Dictionary<string, string>();
            DailyRecord record = null;

            columnList.Add(nameof(record.TradeDate), null);
            columnList.Add(nameof(record.TradeTime), "委托时间");
            columnList.Add(nameof(record.StockCode), "证券代码");
            columnList.Add(nameof(record.StockName), "证券名称");
            columnList.Add(nameof(record.DealFlag), "买卖标志");
            columnList.Add(nameof(record.EntrustVolume), "委托数量");
            columnList.Add(nameof(record.EntrustPrice), "委托价格");
            columnList.Add(nameof(record.EntrustAmount), null);
            columnList.Add(nameof(record.DealPrice), "成交价格");
            columnList.Add(nameof(record.DealVolume), "成交数量");
            columnList.Add(nameof(record.DealAmount), null);
            columnList.Add(nameof(record.StockHolderCode), "股东代码");
            columnList.Add(nameof(record.DealNo), "委托编号");
            columnList.Add(nameof(record.ContractNo), "委托编号");
            columnList.Add(nameof(record.Remarks), "买卖标志");
            columnList.Add(nameof(record.TradeType), "交易类别");

            List<string> templateColumnNames = columnList.Values.Where(x => !string.IsNullOrEmpty(x)).ToList();
            this._dataImportService.DataFormatCheck(templateColumnNames, importDataTable);

            var tradeRecords = ObtainTradeDataFromImportDataTable(false, importOperation, importDataTable, columnList);

            return tradeRecords;
        }

        #endregion 当日委托--招商证券（普通）

        #region 当日委托--浙商证券（信用）

        /// <summary>
        /// 当日委托--浙商证券（信用）
        /// </summary>
        /// <param name="importOperation"></param>
        /// <param name="importDataTable"></param>
        /// <returns></returns>
        private IList<DailyRecord> EntrustImportZheShang_C(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            Dictionary<string, string> columnList = new Dictionary<string, string>();
            DailyRecord record = null;

            columnList.Add(nameof(record.TradeDate), null);
            columnList.Add(nameof(record.TradeTime), "委托时间");
            columnList.Add(nameof(record.StockCode), "证券代码");
            columnList.Add(nameof(record.StockName), "证券名称");
            columnList.Add(nameof(record.DealFlag), "操作");
            columnList.Add(nameof(record.EntrustVolume), "委托数量");
            columnList.Add(nameof(record.EntrustPrice), "委托价格");
            columnList.Add(nameof(record.EntrustAmount), null);
            columnList.Add(nameof(record.DealPrice), "成交均价");
            columnList.Add(nameof(record.DealVolume), "成交数量");
            columnList.Add(nameof(record.DealAmount), null);
            columnList.Add(nameof(record.StockHolderCode), "股东帐户");
            columnList.Add(nameof(record.DealNo), "合同编号");
            columnList.Add(nameof(record.ContractNo), "合同编号");
            columnList.Add(nameof(record.Remarks), "操作");
            columnList.Add(nameof(record.TradeType), "交易类别");

            List<string> templateColumnNames = columnList.Values.Where(x => !string.IsNullOrEmpty(x)).ToList();
            this._dataImportService.DataFormatCheck(templateColumnNames, importDataTable);

            var tradeRecords = ObtainTradeDataFromImportDataTable(false, importOperation, importDataTable, columnList);

            return tradeRecords;
        }

        #endregion 当日委托--浙商证券（信用）

        #region 当日委托--浙商证券（普通）

        /// <summary>
        /// 当日委托--浙商证券（普通）
        /// </summary>
        /// <param name="importOperation"></param>
        /// <param name="importDataTable"></param>
        /// <returns></returns>
        private IList<DailyRecord> EntrustImportZheShang_N(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            Dictionary<string, string> columnList = new Dictionary<string, string>();
            DailyRecord record = null;

            columnList.Add(nameof(record.TradeDate), null);
            columnList.Add(nameof(record.TradeTime), "委托时间");
            columnList.Add(nameof(record.StockCode), "证券代码");
            columnList.Add(nameof(record.StockName), "证券名称");
            columnList.Add(nameof(record.DealFlag), "操作");
            columnList.Add(nameof(record.EntrustVolume), "委托数量");
            columnList.Add(nameof(record.EntrustPrice), "委托价格");
            columnList.Add(nameof(record.EntrustAmount), null);
            columnList.Add(nameof(record.DealPrice), "成交均价");
            columnList.Add(nameof(record.DealVolume), "成交数量");
            columnList.Add(nameof(record.DealAmount), null);
            columnList.Add(nameof(record.StockHolderCode), "股东帐户");
            columnList.Add(nameof(record.DealNo), "合同编号");
            columnList.Add(nameof(record.ContractNo), "合同编号");
            columnList.Add(nameof(record.Remarks), "操作");
            columnList.Add(nameof(record.TradeType), "交易类别");

            List<string> templateColumnNames = columnList.Values.Where(x => !string.IsNullOrEmpty(x)).ToList();
            this._dataImportService.DataFormatCheck(templateColumnNames, importDataTable);

            var tradeRecords = ObtainTradeDataFromImportDataTable(false, importOperation, importDataTable, columnList);

            return tradeRecords;
        }

        #endregion 当日委托--浙商证券（普通）

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

                //安信普通
                case EnumLibrary.SecurityAccount.ESSENCE_N:
                    result = DeliveryImportEssence_N(importOperation, importDataTable);
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

                //海通普通
                case EnumLibrary.SecurityAccount.HaiTong_N:
                    result = DeliveryImportHaiTong_N(importOperation, importDataTable);
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

                //浙商信用
                case EnumLibrary.SecurityAccount.ZheShang_C:
                    result = DeliveryImportZheShang_C(importOperation, importDataTable);
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

                default:
                    throw new Exception($@"券商【{CTMHelper.GetSecurityAccountName((int)securityAccount)}】的交割单导入功能未实现，请联系管理员！");
            }
            return result;
        }

        #endregion 交割单--券商选择

        #region 交割单--中银国际（信用）

        /// <summary>
        /// 交割单--中银国际（信用）
        /// </summary>
        /// <param name="importOperation"></param>
        /// <param name="importDataTable"></param>
        /// <returns></returns>
        private IList<DailyRecord> DeliveryImportBOCI_C(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            Dictionary<string, string> columnList = new Dictionary<string, string>();
            DailyRecord record = null;

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
            columnList.Add(nameof(record.TradeType), "交易类别");

            List<string> templateColumnNames = columnList.Values.Where(x => !string.IsNullOrEmpty(x)).ToList();
            this._dataImportService.DataFormatCheck(templateColumnNames, importDataTable);

            var tradeRecords = ObtainTradeDataFromImportDataTable(true, importOperation, importDataTable, columnList);

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
            Dictionary<string, string> columnList = new Dictionary<string, string>();
            DailyRecord record = null;

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
            columnList.Add(nameof(record.TradeType), "交易类别");

            List<string> templateColumnNames = columnList.Values.Where(x => !string.IsNullOrEmpty(x)).ToList();
            this._dataImportService.DataFormatCheck(templateColumnNames, importDataTable);

            var tradeRecords = ObtainTradeDataFromImportDataTable(true, importOperation, importDataTable, columnList);

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
        private IList<DailyRecord> DeliveryImportCaiTong_C(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            Dictionary<string, string> columnList = new Dictionary<string, string>();
            DailyRecord record = null;

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
            columnList.Add(nameof(record.TradeType), "交易类别");

            List<string> templateColumnNames = columnList.Values.Where(x => !string.IsNullOrEmpty(x)).ToList();
            this._dataImportService.DataFormatCheck(templateColumnNames, importDataTable);

            var tradeRecords = ObtainTradeDataFromImportDataTable(true, importOperation, importDataTable, columnList);

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
            Dictionary<string, string> columnList = new Dictionary<string, string>();
            DailyRecord record = null;

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
            columnList.Add(nameof(record.TradeType), "交易类别");

            List<string> templateColumnNames = columnList.Values.Where(x => !string.IsNullOrEmpty(x)).ToList();
            this._dataImportService.DataFormatCheck(templateColumnNames, importDataTable);

            var tradeRecords = ObtainTradeDataFromImportDataTable(true, importOperation, importDataTable, columnList);

            return tradeRecords;
        }

        #endregion 交割单--财通证券（普通）

        #region 交割单--中信证券（信用）

        /// <summary>
        /// 交割单--中信国际（信用）
        /// </summary>
        /// <param name="importOperation"></param>
        /// <param name="importDataTable"></param>
        /// <returns></returns>
        private IList<DailyRecord> DeliveryImportCITIC_C(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            Dictionary<string, string> columnList = new Dictionary<string, string>();
            DailyRecord record = null;

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
            columnList.Add(nameof(record.TradeType), "交易类别");

            List<string> templateColumnNames = columnList.Values.Where(x => !string.IsNullOrEmpty(x)).ToList();
            this._dataImportService.DataFormatCheck(templateColumnNames, importDataTable);

            var tradeRecords = ObtainTradeDataFromImportDataTable(true, importOperation, importDataTable, columnList);

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
        private IList<DailyRecord> DeliveryImportCITIC_N(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            Dictionary<string, string> columnList = new Dictionary<string, string>();
            DailyRecord record = null;

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
            columnList.Add(nameof(record.TradeType), "交易类别");

            List<string> templateColumnNames = columnList.Values.Where(x => !string.IsNullOrEmpty(x)).ToList();
            this._dataImportService.DataFormatCheck(templateColumnNames, importDataTable);

            var tradeRecords = ObtainTradeDataFromImportDataTable(true, importOperation, importDataTable, columnList);

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
        private IList<DailyRecord> DeliveryImportEssence_N(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            Dictionary<string, string> columnList = new Dictionary<string, string>();
            DailyRecord record = null;

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
            columnList.Add(nameof(record.Incidentals), "沪市过户费");
            columnList.Add("OtherFee1", "清算费");
            columnList.Add("OtherFee2", null);
            columnList.Add("OtherFee3", null);
            columnList.Add(nameof(record.StockHolderCode), "股东代码");
            columnList.Add(nameof(record.DealNo), "委托编号");
            columnList.Add(nameof(record.ContractNo), "委托编号");
            columnList.Add(nameof(record.Remarks), "业务名称");
            columnList.Add(nameof(record.TradeType), "交易类别");

            List<string> templateColumnNames = columnList.Values.Where(x => !string.IsNullOrEmpty(x)).ToList();
            this._dataImportService.DataFormatCheck(templateColumnNames, importDataTable);

            var tradeRecords = ObtainTradeDataFromImportDataTable(true, importOperation, importDataTable, columnList);

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

        private IList<DailyRecord> DeliveryImportFounder_N(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            Dictionary<string, string> columnList = new Dictionary<string, string>();
            DailyRecord record = null;

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
            columnList.Add(nameof(record.TradeType), "交易类别");

            List<string> templateColumnNames = columnList.Values.Where(x => !string.IsNullOrEmpty(x)).ToList();
            this._dataImportService.DataFormatCheck(templateColumnNames, importDataTable);

            var tradeRecords = ObtainTradeDataFromImportDataTable(true, importOperation, importDataTable, columnList);

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
        private IList<DailyRecord> DeliveryImportGalaxy_N(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            Dictionary<string, string> columnList = new Dictionary<string, string>();
            DailyRecord record = null;

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
            columnList.Add(nameof(record.TradeType), "交易类别");

            List<string> templateColumnNames = columnList.Values.Where(x => !string.IsNullOrEmpty(x)).ToList();
            this._dataImportService.DataFormatCheck(templateColumnNames, importDataTable);

            var tradeRecords = ObtainTradeDataFromImportDataTable(true, importOperation, importDataTable, columnList);

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
        private IList<DailyRecord> DeliveryImportGuoTai_C(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            Dictionary<string, string> columnList = new Dictionary<string, string>();
            DailyRecord record = null;

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
            columnList.Add(nameof(record.TradeType), "交易类别");

            List<string> templateColumnNames = columnList.Values.Where(x => !string.IsNullOrEmpty(x)).ToList();
            this._dataImportService.DataFormatCheck(templateColumnNames, importDataTable);

            var tradeRecords = ObtainTradeDataFromImportDataTable(true, importOperation, importDataTable, columnList);

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
            Dictionary<string, string> columnList = new Dictionary<string, string>();
            DailyRecord record = null;

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
            columnList.Add(nameof(record.Remarks), "操作");
            columnList.Add(nameof(record.TradeType), "交易类别");

            List<string> templateColumnNames = columnList.Values.Where(x => !string.IsNullOrEmpty(x)).ToList();
            this._dataImportService.DataFormatCheck(templateColumnNames, importDataTable);

            var tradeRecords = ObtainTradeDataFromImportDataTable(true, importOperation, importDataTable, columnList);

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
        private IList<DailyRecord> DeliveryImportHaiTong_N(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            Dictionary<string, string> columnList = new Dictionary<string, string>();
            DailyRecord record = null;

            columnList.Add(nameof(record.TradeDate), "成交日期");
            columnList.Add(nameof(record.TradeTime), null);
            columnList.Add(nameof(record.StockCode), "证券代码");
            columnList.Add(nameof(record.StockName), "证券名称");
            columnList.Add(nameof(record.DealFlag), "操作");
            columnList.Add(nameof(record.DealPrice), "成交均价");
            columnList.Add(nameof(record.DealVolume), "成交数量");
            columnList.Add(nameof(record.DealAmount), "成交金额");
            columnList.Add(nameof(record.ActualAmount), "发生金额");
            columnList.Add(nameof(record.Commission), null);
            columnList.Add(nameof(record.StampDuty), null);
            columnList.Add(nameof(record.Incidentals), null);
            columnList.Add("OtherFee1", null);
            columnList.Add("OtherFee2", null);
            columnList.Add("OtherFee3", null);
            columnList.Add(nameof(record.StockHolderCode), "股东帐户");
            columnList.Add(nameof(record.DealNo), "成交编号");
            columnList.Add(nameof(record.ContractNo), "成交编号");
            columnList.Add(nameof(record.Remarks), "操作");
            columnList.Add(nameof(record.TradeType), "交易类别");

            List<string> templateColumnNames = columnList.Values.Where(x => !string.IsNullOrEmpty(x)).ToList();
            this._dataImportService.DataFormatCheck(templateColumnNames, importDataTable);

            var tradeRecords = ObtainTradeDataFromImportDataTable(true, importOperation, importDataTable, columnList);

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
        private IList<DailyRecord> DeliveryImportHuaTai_C(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            Dictionary<string, string> columnList = new Dictionary<string, string>();
            DailyRecord record = null;

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
            columnList.Add(nameof(record.TradeType), "交易类别");

            List<string> templateColumnNames = columnList.Values.Where(x => !string.IsNullOrEmpty(x)).ToList();
            this._dataImportService.DataFormatCheck(templateColumnNames, importDataTable);

            var tradeRecords = ObtainTradeDataFromImportDataTable(true, importOperation, importDataTable, columnList);

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
        private IList<DailyRecord> DeliveryImportHuaTai_N(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            Dictionary<string, string> columnList = new Dictionary<string, string>();
            DailyRecord record = null;

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
            columnList.Add(nameof(record.TradeType), "交易类别");

            List<string> templateColumnNames = columnList.Values.Where(x => !string.IsNullOrEmpty(x)).ToList();
            this._dataImportService.DataFormatCheck(templateColumnNames, importDataTable);

            var tradeRecords = ObtainTradeDataFromImportDataTable(true, importOperation, importDataTable, columnList);

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
        private IList<DailyRecord> DeliveryImportShenWan_N(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            Dictionary<string, string> columnList = new Dictionary<string, string>();
            DailyRecord record = null;

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
            columnList.Add(nameof(record.TradeType), "交易类别");

            List<string> templateColumnNames = columnList.Values.Where(x => !string.IsNullOrEmpty(x)).ToList();
            this._dataImportService.DataFormatCheck(templateColumnNames, importDataTable);

            var tradeRecords = ObtainTradeDataFromImportDataTable(true, importOperation, importDataTable, columnList);

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
        private IList<DailyRecord> DeliveryImportSinoLink_N(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            Dictionary<string, string> columnList = new Dictionary<string, string>();
            DailyRecord record = null;

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
            columnList.Add(nameof(record.Incidentals), "过户费");
            columnList.Add("OtherFee1", "其他费用");
            columnList.Add("OtherFee2", null);
            columnList.Add("OtherFee3", null);
            columnList.Add(nameof(record.StockHolderCode), "股东帐户");
            columnList.Add(nameof(record.DealNo), null);
            columnList.Add(nameof(record.ContractNo), null);
            columnList.Add(nameof(record.Remarks), "操作");
            columnList.Add(nameof(record.TradeType), "交易类别");

            List<string> templateColumnNames = columnList.Values.Where(x => !string.IsNullOrEmpty(x)).ToList();
            this._dataImportService.DataFormatCheck(templateColumnNames, importDataTable);

            var tradeRecords = ObtainTradeDataFromImportDataTable(true, importOperation, importDataTable, columnList);

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
        private IList<DailyRecord> DeliveryImportZhaoShang_N(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            Dictionary<string, string> columnList = new Dictionary<string, string>();
            DailyRecord record = null;

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
            columnList.Add(nameof(record.TradeType), "交易类别");

            List<string> templateColumnNames = columnList.Values.Where(x => !string.IsNullOrEmpty(x)).Distinct().ToList();
            this._dataImportService.DataFormatCheck(templateColumnNames, importDataTable);

            var tradeRecords = ObtainTradeDataFromImportDataTable(true, importOperation, importDataTable, columnList);

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
        private IList<DailyRecord> DeliveryImportZheShang_C(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            Dictionary<string, string> columnList = new Dictionary<string, string>();
            DailyRecord record = null;

            columnList.Add(nameof(record.TradeDate), "成交日期");
            columnList.Add(nameof(record.TradeTime), null);
            columnList.Add(nameof(record.StockCode), null);
            columnList.Add(nameof(record.StockName), "证券名称");
            columnList.Add(nameof(record.DealFlag), null);
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
            columnList.Add(nameof(record.Remarks), null);
            columnList.Add(nameof(record.TradeType), "交易类别");

            List<string> templateColumnNames = columnList.Values.Where(x => !string.IsNullOrEmpty(x)).ToList();
            this._dataImportService.DataFormatCheck(templateColumnNames, importDataTable);

            var tradeRecords = ObtainTradeDataFromImportDataTable(true, importOperation, importDataTable, columnList);

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
        private IList<DailyRecord> DeliveryImportZheShang_N(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            Dictionary<string, string> columnList = new Dictionary<string, string>();
            DailyRecord record = null;

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
            columnList.Add(nameof(record.TradeType), "交易类别");

            List<string> templateColumnNames = columnList.Values.Where(x => !string.IsNullOrEmpty(x)).ToList();
            this._dataImportService.DataFormatCheck(templateColumnNames, importDataTable);

            var tradeRecords = ObtainTradeDataFromImportDataTable(true, importOperation, importDataTable, columnList);

            return tradeRecords;
        }

        #endregion 交割单--浙商证券（普通）

        #endregion 交割单数据导入

        #endregion 数据导入

        #endregion Utilities

        #region Methods

        public virtual bool DataImportProcess(EnumLibrary.SecurityAccount securityAccount, DataTable importDataTable, RecordImportOperationEntity importOperation, out IList<DataRow> skippedRecords)
        {
            ImportRecordsCheck(importDataTable, importOperation);

            _skippedRecords = null;

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

            BatchInsertDailyRecords(records);

            skippedRecords = _skippedRecords;

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

            return query.Where (x=>x.ActualAmount !=0 || x.DealVolume !=0) .ToList();
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