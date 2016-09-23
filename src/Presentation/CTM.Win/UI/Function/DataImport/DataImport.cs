using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using CTM.Core;
using CTM.Core.Domain.Stock;
using CTM.Core.Domain.TradeRecord;
using CTM.Core.Util;
using CTM.Services.TradeRecord;
using CTM.Win.Extensions;
using CTM.Win.Models;
using CTM.Win.Util;

namespace CTM.Win.UI.Function.DataImport
{
    partial class FrmTradeDataImportWizard
    {
        #region Constants

        private readonly List<string> _buyTexts = new List<string> { "买入", "证券买入", "融券回购", "普通买入", "担保物买入", "融资买入", "融资借入" };

        private readonly List<string> _sellTexts = new List<string> { "卖出", "证券卖出", "融券购回", "普通卖出", "担保物卖出", "融资卖出", "卖券还款" };

        #endregion Constants

        #region 证券账户导入功能判断

        /// <summary>
        /// 判断选中的证券公司和账户属性是否支持数据导入处理
        /// </summary>
        /// <param name="securityCompanyName">证券公司名称</param>
        /// <param name="accountAttributeName">账号属性名称</param>
        /// <returns></returns>
        private bool CheckSeletedObjectImportFunciton(string securityCompanyName, string accountAttributeName)
        {
            if (securityCompanyName == "中银国际" && accountAttributeName == "信用")
            {
                this._securityAccount = SecurityAccount.BOCI_C;
                return true;
            }

            if (securityCompanyName == "中银国际" && accountAttributeName == "普通")
            {
                this._securityAccount = SecurityAccount.BOCI_N;
                return true;
            }

            if (securityCompanyName == "浙商证券" && accountAttributeName == "信用")
            {
                //this._securityAccount = SecurityAccount.ZheShang_C;
                //return true;
            }

            if (securityCompanyName == "浙商证券" && accountAttributeName == "普通")
            {
                this._securityAccount = SecurityAccount.ZheShang_N;
                return true;
            }

            if (securityCompanyName == "中信证券" && accountAttributeName == "信用")
            {
                this._securityAccount = SecurityAccount.CITIC_C;
                return true;
            }

            if (securityCompanyName == "中信证券" && accountAttributeName == "普通")
            {
                this._securityAccount = SecurityAccount.CITIC_N;
                return true;
            }
            if (securityCompanyName == "方正证券" && accountAttributeName == "普通")
            {
                this._securityAccount = SecurityAccount.Founder_N;
                return true;
            }
            if (securityCompanyName == "银河证券" && accountAttributeName == "普通")
            {
                this._securityAccount = SecurityAccount.Galaxy_N;
                return true;
            }
            if (securityCompanyName == "国金证券" && accountAttributeName == "普通")
            {
                this._securityAccount = SecurityAccount.SinoLink_N;
                return true;
            }
            if (securityCompanyName == "国泰君安" && accountAttributeName == "普通")
            {
                this._securityAccount = SecurityAccount.GuoTai_N;
                return true;
            }
            if (securityCompanyName == "国泰君安" && accountAttributeName == "信用")
            {
                this._securityAccount = SecurityAccount.GuoTai_C;
                return true;
            }
            if (securityCompanyName == "华泰证券" && accountAttributeName == "普通")
            {
                this._securityAccount = SecurityAccount.HuaTai_N;
                return true;
            }
            if (securityCompanyName == "华泰证券" && accountAttributeName == "信用")
            {
                this._securityAccount = SecurityAccount.HuaTai_C;
                return true;
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
                this._securityAccount = SecurityAccount.ShenWan_N;
                return true;
            }
            if (securityCompanyName == "财通证券" && accountAttributeName == "信用")
            {
                this._securityAccount = SecurityAccount.CaiTong_C;
                return true;
            }
            if (securityCompanyName == "财通证券" && accountAttributeName == "普通")
            {
                this._securityAccount = SecurityAccount.CaiTong_N;
                return true;
            }
            if (securityCompanyName == "招商证券" && accountAttributeName == "普通")
            {
                this._securityAccount = SecurityAccount.ZhaoShang_N;
                return true;
            }

            return false;
        }

        #endregion 证券账户导入功能判断

        #region 数据验证

        /// <summary>
        /// 校验股票信息
        /// </summary>
        /// <param name="stockInfo"></param>
        /// <param name="stockCode"></param>
        /// <param name="stockName"></param>
        /// <returns></returns>
        private bool VerifySotckInfo(StockInfo stockInfo, string stockCode, string stockName)
        {
            if (stockInfo == null)
            {
                DXMessage.ShowWarning(string.Format("系统不存在【代码：{0}】【名称：{1}】的股票信息！", stockCode, stockName));
                return false;
            }

            return true;
        }

        /// <summary>
        /// 检查股票是否在股票池中
        /// </summary>
        /// <param name="stockPoolInfo"></param>
        /// <param name="stockCode"></param>
        /// <param name="stockName"></param>
        /// <param name="tradeType"></param>
        /// <returns></returns>
        private bool IsExistedInPool(StockPoolInfo stockPoolInfo, string stockCode, string stockName, int tradeType)
        {
            //if (stockName == "GC001" || tradeType == (int)EnumFactory.TradeType.Day) return true;

            //if (stockPoolInfo == null)
            //    stockPoolInfo = new StockPoolInfo();

            //if (string.IsNullOrEmpty(stockPoolInfo.BandPrincipal) && tradeType == (int)EnumFactory.TradeType.Band)
            //{
            //    if (DXMessage.ShowYesNoAndTips(string.Format("股票【{0}】【{1}】未设置波段负责人！是否继续导入？", stockName, stockCode)) == DialogResult.No)
            //        return false;
            //}
            //else if (string.IsNullOrEmpty(stockPoolInfo.TargetPrincipal) && tradeType == (int)EnumFactory.TradeType.Target)
            //{
            //    if (DXMessage.ShowYesNoAndTips(string.Format("股票【{0}】【{1}】未设置目标负责人！是否继续导入？", stockName, stockCode)) == DialogResult.No)
            //        return false;
            //}

            return true;
        }

        /// <summary>
        ///导入数据格式检查
        /// </summary>
        /// <param name="TemplateColumnNames"></param>
        /// <param name="importDataTable"></param>
        /// <returns></returns>
        private bool DataFormatCheck(List<string> TemplateColumnNames, DataTable importDataTable)
        {
            var DataColumnNames = new List<string>();

            foreach (DataColumn column in importDataTable.Columns)
            {
                if (TemplateColumnNames.Contains(column.ColumnName))
                    DataColumnNames.Add(column.ColumnName);
                else
                {
                    DXMessage.ShowTips(string.Format("交易数据Excel文件中列【{0}】的名称不正确。", column.ColumnName));
                    return false;
                }
            }

            foreach (var name in TemplateColumnNames)
            {
                if (!DataColumnNames.Contains(name))
                {
                    DXMessage.ShowTips(string.Format("交易数据Excel文件中缺少列【{0}】。", name));
                    return false;
                }
            }

            return true;
        }

        #endregion 数据验证

        #region 数据导入

        /// <summary>
        /// 导入数据校验
        /// </summary>
        /// <returns></returns>
        private bool ImportRecordsCheck()
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
            var bandRecordCount = _importDataTable.AsEnumerable().Count(x => x.Field<string>("交易类别").Trim() == "波段");
            if (bandRecordCount > 0 && string.IsNullOrEmpty(this.luBandPrincipal.SelectedValue()))
            {
                DXMessage.ShowTips("请设置波段交易记录对应的波段受益人！");
                this.luBandPrincipal.Focus();
                return false;
            }

            //判断是否需要设置目标收益人
            var targetRecordCount = _importDataTable.AsEnumerable().Count(x => x.Field<string>("交易类别").Trim() == "目标");
            if (targetRecordCount > 0 && string.IsNullOrEmpty(this.luBandPrincipal.SelectedValue()))
            {
                DXMessage.ShowTips("请设置目标交易记录对应的目标受益人！");
                this.luTargetPrincipal.Focus();
                return false;
            }

            return true;
        }

        /// <summary>
        /// 交易数据导入处理
        /// </summary>
        /// <returns></returns>
        private bool DataImportProcess()
        {
            bool result = false;

            try
            {
                if (this._importDataTable == null || !ImportRecordsCheck()) return false;

                var importOperation = new RecordImportOperationEntity
                {
                    AccountId = int.Parse(txtAccountName.Tag.ToString()),
                    AccountName = txtAccountName.Text.Trim(),
                    OperatorCode = this.luOperator.SelectedValue(),
                    ImportTime = _commonService.GetCurrentServerTime(),
                    ImportUserCode = LoginInfo.CurrentUser.UserCode,
                    DataType = this.checkDaily.Checked ? EnumLibrary.DataType.Daily : this.chkEntrust.Checked ? EnumLibrary.DataType.Entrust : EnumLibrary.DataType.Delivery,
                };

                switch (importOperation.DataType)
                {
                    case EnumLibrary.DataType.Entrust:
                        importOperation.TradeDate = importOperation.ImportTime.Date;
                        result = EntrustImport(importOperation, this._importDataTable);
                        break;

                    case EnumLibrary.DataType.Daily:
                        importOperation.TradeDate = importOperation.ImportTime.Date;
                        result = DailyImport(importOperation, this._importDataTable);
                        break;

                    case EnumLibrary.DataType.Delivery:
                        importOperation.TradeDate = null;
                        result = DeliveryImport(importOperation, this._importDataTable);
                        break;
                }
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }

            return result;
        }

        #region 当日委托数据导入

        #region 当日委托--券商选择

        /// <summary>
        /// 当日委托--券商选择
        /// </summary>
        /// <param name="importOperation"></param>
        /// <param name="importDataTable"></param>
        /// <returns></returns>
        private bool EntrustImport(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            bool result = false;
            switch (this._securityAccount)
            {
                //财通信用
                case SecurityAccount.CaiTong_C:
                    result = EntrustImportCaiTong_C(importOperation, importDataTable);
                    break;

                //财通普通
                case SecurityAccount.CaiTong_N:
                    result = EntrustImportCaiTong_N(importOperation, importDataTable);
                    break;

                //方正普通
                case SecurityAccount.Founder_N:
                    result = EntrustImportFounder_N(importOperation, importDataTable);
                    break;

                // 银河普通
                case SecurityAccount.Galaxy_N:
                    result = EntrustImportGalaxy_N(importOperation, importDataTable);
                    break;

                //国泰普通
                case SecurityAccount.GuoTai_N:
                    result = EntrustImportGuoTai_N(importOperation, importDataTable);
                    break;

                //国泰信用
                case SecurityAccount.GuoTai_C:
                    result = EntrustImportGuoTai_C(importOperation, importDataTable);
                    break;

                //华泰普通
                case SecurityAccount.HuaTai_N:
                    result = EntrustImportHuaTai_N(importOperation, importDataTable);
                    break;

                //华泰信用
                case SecurityAccount.HuaTai_C:
                    result = EntrustImportHuaTai_C(importOperation, importDataTable);
                    break;

                //申万普通
                case SecurityAccount.ShenWan_N:
                    result = EntrustImportShenWan_N(importOperation, importDataTable);
                    break;

                //浙商普通
                case SecurityAccount.ZheShang_N:
                    result = EntrustImportZheShang_N(importOperation, importDataTable);
                    break;

                //中信普通
                case SecurityAccount.CITIC_N:
                    result = EntrustImportCITIC_N(importOperation, importDataTable);
                    break;

                //中信信用
                case SecurityAccount.CITIC_C:
                    result = EntrustImportCITIC_C(importOperation, importDataTable);
                    break;

                ////中银信用
                //case SecurityAccount.BOCI_C:
                //    result = EntrustImportBOCI_C(importOperation, importDataTable);
                //    break;

                //中银普通
                case SecurityAccount.BOCI_N:
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
        private bool EntrustImportCaiTong_C(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            #region DataFormatCheck

            var TemplateColumnNames = new List<string> { "委托时间", "证券代码", "证券名称", "操作", "备注", "委托价格", "委托数量", "成交均价", "成交数量", "股东帐户", "合同编号", "交易市场", "订单类型", "交易类别" };

            if (!DataFormatCheck(TemplateColumnNames, importDataTable)) return false;

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

                if (!VerifySotckInfo(stockInfo, stockCode, stockName)) return false;

                tradeRecord.StockCode = stockInfo.FullCode;

                tradeRecord.StockName = stockName;

                var stockPoolInfo = _stockService.GetStockPoolInfoByStockId(stockInfo.Id);

                var tradeType = row["交易类别"].ToString().Trim();
                tradeRecord.SetTradeType(tradeType);

                if (!IsExistedInPool(stockPoolInfo, tradeRecord.StockCode, tradeRecord.StockName, tradeRecord.TradeType)) continue;

                // tradeRecord.SetBeneficiary(stockPoolInfo);
                tradeRecord.SetBeneficiary(this.luBandPrincipal.SelectedValue(), this.luTargetPrincipal.SelectedValue());

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

                tradeRecord.Remarks = row["备注"].ToString().Trim();

                tradeRecords.Add(tradeRecord);
            }

            _tradeRecordService.InsertDailyRecords(tradeRecords);

            #endregion DataProcess

            return true;
        }

        #endregion 当日委托--财通证券（信用）

        #region 当日委托--财通证券（普通）

        /// <summary>
        /// 当日委托--财通证券（普通）
        /// </summary>
        /// <param name="importOperation"></param>
        /// <param name="importDataTable"></param>
        /// <returns></returns>
        private bool EntrustImportCaiTong_N(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            #region DataFormatCheck

            var TemplateColumnNames = new List<string> { "委托时间", "申报编号", "证券代码", "证券名称", "操作", "备注", "委托价格", "委托数量", "成交均价", "成交数量", "撤单数量", "股东帐户", "合同编号", "交易市场", "交易类别" };

            if (!DataFormatCheck(TemplateColumnNames, importDataTable)) return false;

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

                if (!VerifySotckInfo(stockInfo, stockCode, stockName)) return false;

                tradeRecord.StockCode = stockInfo.FullCode;

                tradeRecord.StockName = stockName;

                var stockPoolInfo = _stockService.GetStockPoolInfoByStockId(stockInfo.Id);

                var tradeType = row["交易类别"].ToString().Trim();
                tradeRecord.SetTradeType(tradeType);

                if (!IsExistedInPool(stockPoolInfo, tradeRecord.StockCode, tradeRecord.StockName, tradeRecord.TradeType)) continue;

                // tradeRecord.SetBeneficiary(stockPoolInfo);
                tradeRecord.SetBeneficiary(this.luBandPrincipal.SelectedValue(), this.luTargetPrincipal.SelectedValue());

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

                tradeRecord.Remarks = row["备注"].ToString().Trim();

                tradeRecords.Add(tradeRecord);
            }

            _tradeRecordService.InsertDailyRecords(tradeRecords);

            #endregion DataProcess

            return true;
        }

        #endregion 当日委托--财通证券（普通）

        #region 当日委托--方正证券（普通）

        /// <summary>
        /// 当日委托--方正证券（普通）
        /// </summary>
        /// <param name="importOperation"></param>
        /// <param name="importDataTable"></param>
        /// <returns></returns>
        private bool EntrustImportFounder_N(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            #region DataFormatCheck

            var TemplateColumnNames = new List<string> { "委托时间", "证券代码", "证券名称", "操作", "备注", "委托数量", "成交数量", "撤消数量", "委托价格", "成交均价", "合同编号", "申报编号", "交易类别" };

            if (!DataFormatCheck(TemplateColumnNames, importDataTable)) return false;

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

                if (!VerifySotckInfo(stockInfo, stockCode, stockName)) return false;

                tradeRecord.StockCode = stockInfo.FullCode;

                tradeRecord.StockName = stockName;

                var stockPoolInfo = _stockService.GetStockPoolInfoByStockId(stockInfo.Id);

                var tradeType = row["交易类别"].ToString().Trim();
                tradeRecord.SetTradeType(tradeType);

                if (!IsExistedInPool(stockPoolInfo, tradeRecord.StockCode, tradeRecord.StockName, tradeRecord.TradeType)) continue;

                // tradeRecord.SetBeneficiary(stockPoolInfo);
                tradeRecord.SetBeneficiary(this.luBandPrincipal.SelectedValue(), this.luTargetPrincipal.SelectedValue());

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

                tradeRecord.Remarks = row["备注"].ToString().Trim();

                tradeRecords.Add(tradeRecord);
            }

            _tradeRecordService.InsertDailyRecords(tradeRecords);

            #endregion DataProcess

            return true;
        }

        #endregion 当日委托--方正证券（普通）

        #region 当日委托--方正证券（普通）

        /// <summary>
        /// 当日委托--银河证券（普通）
        /// </summary>
        /// <param name="importOperation"></param>
        /// <param name="importDataTable"></param>
        /// <returns></returns>
        private bool EntrustImportGalaxy_N(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            #region DataFormatCheck

            var TemplateColumnNames = new List<string> { "委托时间", "证券代码", "证券名称", "操作", "备注", "委托数量", "成交数量", "成交金额", "委托价格", "成交均价", "合同编号", "操作日期", "交易类别" };

            if (!DataFormatCheck(TemplateColumnNames, importDataTable)) return false;

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

                if (!VerifySotckInfo(stockInfo, stockCode, stockName)) return false;

                tradeRecord.StockCode = stockInfo.FullCode;

                tradeRecord.StockName = stockName;

                var stockPoolInfo = _stockService.GetStockPoolInfoByStockId(stockInfo.Id);

                var tradeType = row["交易类别"].ToString().Trim();
                tradeRecord.SetTradeType(tradeType);

                if (!IsExistedInPool(stockPoolInfo, tradeRecord.StockCode, tradeRecord.StockName, tradeRecord.TradeType)) continue;

                // tradeRecord.SetBeneficiary(stockPoolInfo);
                tradeRecord.SetBeneficiary(this.luBandPrincipal.SelectedValue(), this.luTargetPrincipal.SelectedValue());

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

                tradeRecord.Remarks = row["备注"].ToString().Trim();

                tradeRecords.Add(tradeRecord);
            }

            _tradeRecordService.InsertDailyRecords(tradeRecords);

            #endregion DataProcess

            return true;
        }

        #endregion 当日委托--方正证券（普通）

        #region 当日委托--国泰证券（普通）

        /// <summary>
        /// 当日委托--国泰证券（普通）
        /// </summary>
        /// <param name="importOperation"></param>
        /// <param name="importDataTable"></param>
        /// <returns></returns>
        private bool EntrustImportGuoTai_N(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            #region DataFormatCheck

            var TemplateColumnNames = new List<string> { "委托时间", "证券代码", "证券名称", "操作", "备注", "委托数量", "成交数量", "成交金额", "委托价格", "成交均价", "合同编号", "交易类别" };

            if (!DataFormatCheck(TemplateColumnNames, importDataTable)) return false;

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

                if (!VerifySotckInfo(stockInfo, stockCode, stockName)) return false;

                tradeRecord.StockCode = stockInfo.FullCode;

                tradeRecord.StockName = stockName;

                var stockPoolInfo = _stockService.GetStockPoolInfoByStockId(stockInfo.Id);

                var tradeType = row["交易类别"].ToString().Trim();
                tradeRecord.SetTradeType(tradeType);

                if (!IsExistedInPool(stockPoolInfo, tradeRecord.StockCode, tradeRecord.StockName, tradeRecord.TradeType)) continue;

                // tradeRecord.SetBeneficiary(stockPoolInfo);
                tradeRecord.SetBeneficiary(this.luBandPrincipal.SelectedValue(), this.luTargetPrincipal.SelectedValue());

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

                tradeRecord.Remarks = row["备注"].ToString().Trim();

                tradeRecords.Add(tradeRecord);
            }

            _tradeRecordService.InsertDailyRecords(tradeRecords);

            #endregion DataProcess

            return true;
        }

        #endregion 当日委托--国泰证券（普通）

        #region 当日委托--国泰证券（信用）

        /// <summary>
        /// 当日委托--国泰证券（信用）
        /// </summary>
        /// <param name="importOperation"></param>
        /// <param name="importDataTable"></param>
        /// <returns></returns>
        private bool EntrustImportGuoTai_C(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            #region DataFormatCheck

            var TemplateColumnNames = new List<string> { "委托日期", "委托时间", "证券代码", "证券名称", "买卖标志", "委托价格", "委托数量", "委托编号", "成交数量", "撤单数量", "状态说明", "撤单标志", "股东代码", "操作日期", "交易类别" };

            if (!DataFormatCheck(TemplateColumnNames, importDataTable)) return false;

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

                if (!VerifySotckInfo(stockInfo, stockCode, stockName)) return false;

                tradeRecord.StockCode = stockInfo.FullCode;

                tradeRecord.StockName = stockName;

                var stockPoolInfo = _stockService.GetStockPoolInfoByStockId(stockInfo.Id);

                var tradeType = row["交易类别"].ToString().Trim();
                tradeRecord.SetTradeType(tradeType);

                if (!IsExistedInPool(stockPoolInfo, tradeRecord.StockCode, tradeRecord.StockName, tradeRecord.TradeType)) continue;

                // tradeRecord.SetBeneficiary(stockPoolInfo);
                tradeRecord.SetBeneficiary(this.luBandPrincipal.SelectedValue(), this.luTargetPrincipal.SelectedValue());

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

                tradeRecord.StockHolderCode = string.Empty;

                tradeRecord.ContractNo = string.Empty;

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

                tradeRecord.Remarks = string.Empty;

                tradeRecords.Add(tradeRecord);
            }

            _tradeRecordService.InsertDailyRecords(tradeRecords);

            #endregion DataProcess

            return true;
        }

        #endregion 当日委托--国泰证券（信用）

        #region 当日委托--华泰证券（信用）

        /// <summary>
        /// 当日委托--华泰证券（信用）
        /// </summary>
        /// <param name="importOperation"></param>
        /// <param name="importDataTable"></param>
        /// <returns></returns>
        private bool EntrustImportHuaTai_C(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            #region DataFormatCheck

            var TemplateColumnNames = new List<string> { "委托日期", "证券代码", "证券名称", "操作", "委托价格", "委托数量", "委托时间", "成交数量", "成交均价", "合同编号", "交易市场", "股东帐户", "备注", "交易类别" };

            if (!DataFormatCheck(TemplateColumnNames, importDataTable)) return false;

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

                if (!VerifySotckInfo(stockInfo, stockCode, stockName)) return false;

                tradeRecord.StockCode = stockInfo.FullCode;

                tradeRecord.StockName = stockName;

                var stockPoolInfo = _stockService.GetStockPoolInfoByStockId(stockInfo.Id);

                var tradeType = row["交易类别"].ToString().Trim();
                tradeRecord.SetTradeType(tradeType);

                if (!IsExistedInPool(stockPoolInfo, tradeRecord.StockCode, tradeRecord.StockName, tradeRecord.TradeType)) continue;

                // tradeRecord.SetBeneficiary(stockPoolInfo);
                tradeRecord.SetBeneficiary(this.luBandPrincipal.SelectedValue(), this.luTargetPrincipal.SelectedValue());

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

                tradeRecord.Remarks = row["备注"].ToString().Trim();

                tradeRecords.Add(tradeRecord);
            }

            _tradeRecordService.InsertDailyRecords(tradeRecords);

            #endregion DataProcess

            return true;
        }

        #endregion 当日委托--华泰证券（信用）

        #region 当日委托--华泰证券（普通）

        /// <summary>
        /// 当日委托--华泰证券（普通）
        /// </summary>
        /// <param name="importOperation"></param>
        /// <param name="importDataTable"></param>
        /// <returns></returns>
        private bool EntrustImportHuaTai_N(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            #region DataFormatCheck

            var TemplateColumnNames = new List<string> { "委托时间", "证券代码", "证券名称", "操作", "备注", "委托数量", "成交数量", "委托价格", "成交均价", "合同编号", "交易类别" };

            if (!DataFormatCheck(TemplateColumnNames, importDataTable)) return false;

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

                if (!VerifySotckInfo(stockInfo, stockCode, stockName)) return false;

                tradeRecord.StockCode = stockInfo.FullCode;

                tradeRecord.StockName = stockName;

                var stockPoolInfo = _stockService.GetStockPoolInfoByStockId(stockInfo.Id);

                var tradeType = row["交易类别"].ToString().Trim();
                tradeRecord.SetTradeType(tradeType);

                if (!IsExistedInPool(stockPoolInfo, tradeRecord.StockCode, tradeRecord.StockName, tradeRecord.TradeType)) continue;

                // tradeRecord.SetBeneficiary(stockPoolInfo);
                tradeRecord.SetBeneficiary(this.luBandPrincipal.SelectedValue(), this.luTargetPrincipal.SelectedValue());

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

                tradeRecord.Remarks = row["备注"].ToString().Trim();

                tradeRecords.Add(tradeRecord);
            }

            _tradeRecordService.InsertDailyRecords(tradeRecords);

            #endregion DataProcess

            return true;
        }

        #endregion 当日委托--华泰证券（普通）

        #region 当日委托--申万证券（普通）

        /// <summary>
        /// 当日委托--申万证券（普通）
        /// </summary>
        /// <param name="importOperation"></param>
        /// <param name="importDataTable"></param>
        /// <returns></returns>
        private bool EntrustImportShenWan_N(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            #region DataFormatCheck

            var TemplateColumnNames = new List<string> { "委托时间", "证券代码", "证券名称", "操作", "委托状态", "委托数量", "成交数量", "撤单数量", "委托价格", "成交均价", "合同编号", "申报编号", "业务名称", "委托属性", "委托金额", "成交金额", "已撤数量", "交易类别" };

            if (!DataFormatCheck(TemplateColumnNames, importDataTable)) return false;

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

                if (!VerifySotckInfo(stockInfo, stockCode, stockName)) return false;

                tradeRecord.StockCode = stockInfo.FullCode;

                tradeRecord.StockName = stockName;

                var stockPoolInfo = _stockService.GetStockPoolInfoByStockId(stockInfo.Id);

                var tradeType = row["交易类别"].ToString().Trim();
                tradeRecord.SetTradeType(tradeType);

                if (!IsExistedInPool(stockPoolInfo, tradeRecord.StockCode, tradeRecord.StockName, tradeRecord.TradeType)) continue;

                // tradeRecord.SetBeneficiary(stockPoolInfo);
                tradeRecord.SetBeneficiary(this.luBandPrincipal.SelectedValue(), this.luTargetPrincipal.SelectedValue());

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

                tradeRecord.Remarks = string.Empty;

                tradeRecords.Add(tradeRecord);
            }

            _tradeRecordService.InsertDailyRecords(tradeRecords);

            #endregion DataProcess

            return true;
        }

        #endregion 当日委托--申万证券（普通）

        #region 当日委托--浙商证券（普通）

        /// <summary>
        /// 当日委托--浙商证券（普通）
        /// </summary>
        /// <param name="importOperation"></param>
        /// <param name="importDataTable"></param>
        /// <returns></returns>
        private bool EntrustImportZheShang_N(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            #region DataFormatCheck

            var TemplateColumnNames = new List<string> { "委托时间", "证券代码", "证券名称", "委托数量", "成交数量", "操作", "委托价格", "成交均价", "合同编号", "股东帐户", "交易市场", "申报编号", "备注", "废单原因", "交易类别" };

            if (!DataFormatCheck(TemplateColumnNames, importDataTable)) return false;

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

                if (!VerifySotckInfo(stockInfo, stockCode, stockName)) return false;

                tradeRecord.StockCode = stockInfo.FullCode;

                tradeRecord.StockName = stockName;

                var stockPoolInfo = _stockService.GetStockPoolInfoByStockId(stockInfo.Id);

                var tradeType = row["交易类别"].ToString().Trim();
                tradeRecord.SetTradeType(tradeType);

                if (!IsExistedInPool(stockPoolInfo, tradeRecord.StockCode, tradeRecord.StockName, tradeRecord.TradeType)) continue;

                // tradeRecord.SetBeneficiary(stockPoolInfo);
                tradeRecord.SetBeneficiary(this.luBandPrincipal.SelectedValue(), this.luTargetPrincipal.SelectedValue());

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

                tradeRecord.Remarks = row["备注"].ToString().Trim();

                tradeRecords.Add(tradeRecord);
            }

            _tradeRecordService.InsertDailyRecords(tradeRecords);

            #endregion DataProcess

            return true;
        }

        #endregion 当日委托--浙商证券（普通）

        #region 当日委托--中信证券（信用）

        /// <summary>
        /// 当日委托--中信证券（信用）
        /// </summary>
        /// <param name="importOperation"></param>
        /// <param name="importDataTable"></param>
        /// <returns></returns>
        private bool EntrustImportCITIC_C(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            #region DataFormatCheck

            var TemplateColumnNames = new List<string> { "证券代码", "证券名称", "买卖", "委托价格", "委托数量", "委托时间", "成交数量", "成交价格", "交易市场", "股东代码", "委托状态", "申请编号", "委托类型", "成交金额", "已撤数量", "业务名称", "资金帐号", "委托编号", "返回信息", "交易类别" };

            if (!DataFormatCheck(TemplateColumnNames, importDataTable)) return false;

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

                if (!VerifySotckInfo(stockInfo, stockCode, stockName)) return false;

                tradeRecord.StockCode = stockInfo.FullCode;

                tradeRecord.StockName = stockName;

                var stockPoolInfo = _stockService.GetStockPoolInfoByStockId(stockInfo.Id);

                var tradeType = row["交易类别"].ToString().Trim();
                tradeRecord.SetTradeType(tradeType);

                if (!IsExistedInPool(stockPoolInfo, tradeRecord.StockCode, tradeRecord.StockName, tradeRecord.TradeType)) continue;

                // tradeRecord.SetBeneficiary(stockPoolInfo);
                tradeRecord.SetBeneficiary(this.luBandPrincipal.SelectedValue(), this.luTargetPrincipal.SelectedValue());

                tradeRecord.TradeTime = row["委托时间"].ToString().Trim();

                tradeRecord.DealFlag = row["买卖"].ToString().Trim().IndexOf("买入") > -1 ? true : false;

                tradeRecord.DealNo = row["委托编号"].ToString().Trim();

                tradeRecord.DealPrice = decimal.Parse(row["成交价格"].ToString().Trim());

                tradeRecord.DealAmount = decimal.Parse(row["成交金额"].ToString().Trim());

                tradeRecord.StockHolderCode = row["股东代码"].ToString().Trim();

                tradeRecord.ContractNo = string.Empty;

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

                tradeRecord.Remarks = row["返回信息"].ToString().Trim();

                tradeRecords.Add(tradeRecord);
            }

            _tradeRecordService.InsertDailyRecords(tradeRecords);

            #endregion DataProcess

            return true;
        }

        #endregion 当日委托--中信证券（信用）

        #region 当日委托--中信证券（普通）

        /// <summary>
        /// 当日委托--中信证券（普通）
        /// </summary>
        /// <param name="importOperation"></param>
        /// <param name="importDataTable"></param>
        /// <returns></returns>
        private bool EntrustImportCITIC_N(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            #region DataFormatCheck

            var TemplateColumnNames = new List<string> { "委托时间", "证券代码", "证券名称", "买卖", "委托状态", "委托数量", "成交数量", "已撤数量", "委托价格", "成交价格", "合同编号", "申请编号", "委托类型", "资金帐号", "委托编号", "委托类别", "返回信息", "交易类别" };

            if (!DataFormatCheck(TemplateColumnNames, importDataTable)) return false;

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

                if (!VerifySotckInfo(stockInfo, stockCode, stockName)) return false;

                tradeRecord.StockCode = stockInfo.FullCode;

                tradeRecord.StockName = stockName;

                var stockPoolInfo = _stockService.GetStockPoolInfoByStockId(stockInfo.Id);

                var tradeType = row["交易类别"].ToString().Trim();
                tradeRecord.SetTradeType(tradeType);

                if (!IsExistedInPool(stockPoolInfo, tradeRecord.StockCode, tradeRecord.StockName, tradeRecord.TradeType)) continue;

                // tradeRecord.SetBeneficiary(stockPoolInfo);
                tradeRecord.SetBeneficiary(this.luBandPrincipal.SelectedValue(), this.luTargetPrincipal.SelectedValue());

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

                tradeRecord.Remarks = row["返回信息"].ToString().Trim();

                tradeRecords.Add(tradeRecord);
            }

            _tradeRecordService.InsertDailyRecords(tradeRecords);

            #endregion DataProcess

            return true;
        }

        #endregion 当日委托--中信证券（普通）

        #region 当日委托--中银证券（普通）

        /// <summary>
        /// 当日委托--中银证券（普通）
        /// </summary>
        /// <param name="importOperation"></param>
        /// <param name="importDataTable"></param>
        /// <returns></returns>
        private bool EntrustImportBOCI_N(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            #region DataFormatCheck

            var TemplateColumnNames = new List<string> { "委托时间", "证券代码", "证券名称", "操作", "备注", "委托数量", "成交数量", "撤单数量", "委托价格", "成交均价", "合同编号", "申报编号", "交易类别" };

            if (!DataFormatCheck(TemplateColumnNames, importDataTable)) return false;

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

                if (!VerifySotckInfo(stockInfo, stockCode, stockName)) return false;

                tradeRecord.StockCode = stockInfo.FullCode;

                tradeRecord.StockName = stockName;

                var stockPoolInfo = _stockService.GetStockPoolInfoByStockId(stockInfo.Id);

                var tradeType = row["交易类别"].ToString().Trim();
                tradeRecord.SetTradeType(tradeType);

                if (!IsExistedInPool(stockPoolInfo, tradeRecord.StockCode, tradeRecord.StockName, tradeRecord.TradeType)) continue;

                // tradeRecord.SetBeneficiary(stockPoolInfo);
                tradeRecord.SetBeneficiary(this.luBandPrincipal.SelectedValue(), this.luTargetPrincipal.SelectedValue());

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

                tradeRecord.Remarks = row["备注"].ToString().Trim();

                tradeRecords.Add(tradeRecord);
            }

            _tradeRecordService.InsertDailyRecords(tradeRecords);

            #endregion DataProcess

            return true;
        }

        #endregion 当日委托--中银证券（普通）

        #endregion 当日委托数据导入

        #region 当日成交数据导入

        #region 当日成交--券商选择

        private bool DailyImport(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            bool result = false;
            switch (this._securityAccount)
            {
                //财通信用
                case SecurityAccount.CaiTong_C:
                    result = DailyImportCaiTong_C(importOperation, importDataTable);
                    break;

                //国金普通
                case SecurityAccount.SinoLink_N:
                    result = DailyImportSinoLink_N(importOperation, importDataTable);
                    break;

                //国泰普通
                case SecurityAccount.GuoTai_N:
                    result = DailyImportGuoTai_N(importOperation, importDataTable);
                    break;

                //华泰普通
                case SecurityAccount.HuaTai_N:
                    result = DailyImportHuaTai_N(importOperation, importDataTable);
                    break;

                //华泰信用
                case SecurityAccount.HuaTai_C:
                    result = DailyImportHuaTai_C(importOperation, importDataTable);
                    break;

                //申万普通
                case SecurityAccount.ShenWan_N:
                    result = DailyImportShenWan_N(importOperation, importDataTable);
                    break;

                //浙商普通
                case SecurityAccount.ZheShang_N:
                    result = DailyImportZheShang_N(importOperation, importDataTable);
                    break;

                //中信普通
                case SecurityAccount.CITIC_N:
                    result = DailyImportCITIC_N(importOperation, importDataTable);
                    break;

                //中银信用
                case SecurityAccount.BOCI_C:
                    result = DialyImportBOCI_C(importOperation, importDataTable);
                    break;
            }
            return result;
        }

        #endregion 当日成交--券商选择

        #region 当日成交--财通证券（信用）

        /// <summary>
        ///  当日成交--财通证券（信用）
        /// </summary>
        private bool DailyImportCaiTong_C(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            #region DataFormatCheck

            var TemplateColumnNames = new List<string> { "成交时间", "证券代码", "证券名称", "操作", "成交均价", "成交数量", "成交金额", "股东帐户", "合同编号", "交易市场", "成交编号", "交易类别" };

            if (!DataFormatCheck(TemplateColumnNames, importDataTable)) return false;

            #endregion DataFormatCheck

            #region DataProcess

            var tradeRecords = new List<DailyRecord>();

            foreach (DataRow row in importDataTable.Rows)
            {
                var tradeRecord = new DailyRecord();

                tradeRecord.SetTradeRecordCommonFields(importOperation);

                var stockCode = CommonHelper.StockCodeZerofill(row["证券代码"].ToString().Trim());

                var stockName = row["证券名称"].ToString().Trim();

                var stockInfo = _stockService.GetStockInfoByCode(stockCode);

                if (!VerifySotckInfo(stockInfo, stockCode, stockName)) return false;

                tradeRecord.StockCode = stockInfo.FullCode;

                tradeRecord.StockName = stockName;

                var stockPoolInfo = _stockService.GetStockPoolInfoByStockId(stockInfo.Id);

                var tradeType = row["交易类别"].ToString().Trim();
                tradeRecord.SetTradeType(tradeType);

                if (!IsExistedInPool(stockPoolInfo, tradeRecord.StockCode, tradeRecord.StockName, tradeRecord.TradeType)) continue;

                // tradeRecord.SetBeneficiary(stockPoolInfo);
                tradeRecord.SetBeneficiary(this.luBandPrincipal.SelectedValue(), this.luTargetPrincipal.SelectedValue());

                //tradeRecord.TradeDate = _commonService.GetCurrentServerTime().Date;

                tradeRecord.TradeTime = row["成交时间"].ToString().Trim();

                tradeRecord.DealFlag = row["操作"].ToString().Trim().IndexOf("买入") > -1 ? true : false;

                tradeRecord.DealNo = row["成交编号"].ToString().Trim();

                tradeRecord.DealPrice = decimal.Parse(row["成交均价"].ToString().Trim());

                tradeRecord.DealAmount = decimal.Parse(row["成交金额"].ToString().Trim());

                tradeRecord.StockHolderCode = row["股东代码"].ToString().Trim();

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

                tradeRecords.Add(tradeRecord);
            }

            _tradeRecordService.InsertDailyRecords(tradeRecords);

            #endregion DataProcess

            return true;
        }

        #endregion 当日成交--财通证券（信用）

        #region 当日成交--国金证券（普通）

        /// <summary>
        /// 当日成交--国金证券（普通）
        /// </summary>
        /// <param name="importOperation"></param>
        /// <param name="importDataTable"></param>
        /// <returns></returns>
        private bool DailyImportSinoLink_N(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            #region DataFormatCheck

            var TemplateColumnNames = new List<string> { "成交时间", "证券代码", "证券名称", "买卖标志", "状态说明", "成交价格", "成交数量", "成交金额", "成交编号", "委托编号", "股东代码", "委托类别", "交易类别" };

            if (!DataFormatCheck(TemplateColumnNames, importDataTable)) return false;

            #endregion DataFormatCheck

            #region DataProcess

            var tradeRecords = new List<DailyRecord>();

            foreach (DataRow row in importDataTable.Rows)
            {
                var tradeRecord = new DailyRecord();

                tradeRecord.SetTradeRecordCommonFields(importOperation);

                var stockCode = CommonHelper.StockCodeZerofill(row["证券代码"].ToString().Trim());

                var stockName = row["证券名称"].ToString().Trim();

                var stockInfo = _stockService.GetStockInfoByCode(stockCode);

                if (!VerifySotckInfo(stockInfo, stockCode, stockName)) return false;

                tradeRecord.StockCode = stockInfo.FullCode;

                tradeRecord.StockName = stockName;

                var stockPoolInfo = _stockService.GetStockPoolInfoByStockId(stockInfo.Id);

                var tradeType = row["交易类别"].ToString().Trim();
                tradeRecord.SetTradeType(tradeType);

                if (!IsExistedInPool(stockPoolInfo, tradeRecord.StockCode, tradeRecord.StockName, tradeRecord.TradeType)) continue;
                // tradeRecord.SetBeneficiary(stockPoolInfo);
                tradeRecord.SetBeneficiary(this.luBandPrincipal.SelectedValue(), this.luTargetPrincipal.SelectedValue());

                //tradeRecord.TradeDate = _commonService.GetCurrentServerTime().Date;

                tradeRecord.TradeTime = row["成交时间"].ToString().Trim();

                tradeRecord.DealFlag = row["买卖标志"].ToString().Trim().IndexOf("买入") > -1 ? true : false;

                tradeRecord.DealNo = row["成交编号"].ToString().Trim();

                tradeRecord.DealPrice = decimal.Parse(row["成交均价"].ToString().Trim());

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

                tradeRecords.Add(tradeRecord);
            }

            _tradeRecordService.InsertDailyRecords(tradeRecords);

            #endregion DataProcess

            return true;
        }

        #endregion 当日成交--国金证券（普通）

        #region 当日成交--国泰君安（普通）

        /// <summary>
        ///  当日成交--国泰君安（普通）
        /// </summary>
        /// <param name="importOperation"></param>
        /// <param name="importDataTable"></param>
        private bool DailyImportGuoTai_N(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            #region DataFormatCheck

            var TemplateColumnNames = new List<string> { "成交日期", "成交时间", "证券代码", "证券名称", "买卖标志", "委托价格", "委托数量", "委托编号", "成交价格", "成交数量", "成交金额", "成交编号", "股东代码", "状态说明", "交易类别" };

            if (!DataFormatCheck(TemplateColumnNames, importDataTable)) return false;

            #endregion DataFormatCheck

            #region DataProcess

            var tradeRecords = new List<DailyRecord>();

            foreach (DataRow row in importDataTable.Rows)
            {
                var tradeRecord = new DailyRecord();

                tradeRecord.SetTradeRecordCommonFields(importOperation);

                var stockCode = CommonHelper.StockCodeZerofill(row["证券代码"].ToString().Trim());

                var stockName = row["证券名称"].ToString().Trim();

                var stockInfo = _stockService.GetStockInfoByCode(stockCode);

                if (!VerifySotckInfo(stockInfo, stockCode, stockName)) return false;

                tradeRecord.StockCode = stockInfo.FullCode;

                tradeRecord.StockName = stockName;

                var stockPoolInfo = _stockService.GetStockPoolInfoByStockId(stockInfo.Id);

                var tradeType = row["交易类别"].ToString().Trim();
                tradeRecord.SetTradeType(tradeType);

                if (!IsExistedInPool(stockPoolInfo, tradeRecord.StockCode, tradeRecord.StockName, tradeRecord.TradeType)) continue;

                // tradeRecord.SetBeneficiary(stockPoolInfo);
                tradeRecord.SetBeneficiary(this.luBandPrincipal.SelectedValue(), this.luTargetPrincipal.SelectedValue());

                //tradeRecord.TradeDate = _commonService.GetCurrentServerTime().Date;

                tradeRecord.TradeTime = row["成交时间"].ToString().Trim();

                tradeRecord.DealFlag = row["买卖标志"].ToString().Trim().IndexOf("买入") > -1 ? true : false;

                tradeRecord.DealNo = row["成交编号"].ToString().Trim();

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

                tradeRecords.Add(tradeRecord);
            }

            _tradeRecordService.InsertDailyRecords(tradeRecords);

            #endregion DataProcess

            return true;
        }

        #endregion 当日成交--国泰君安（普通）

        #region 当日成交--华泰证券（普通）

        /// <summary>
        ///  当日成交--华泰证券（普通）
        /// </summary>
        /// <param name="importOperation"></param>
        /// <param name="importDataTable"></param>
        private bool DailyImportHuaTai_N(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            #region DataFormatCheck

            var TemplateColumnNames = new List<string> { "成交日期", "合同编号", "证券代码", "证券名称", "操作", "成交均价", "成交数量", "成交金额", "成交编号", "股东帐户", "交易市场", "交易类别" };

            if (!DataFormatCheck(TemplateColumnNames, importDataTable)) return false;

            #endregion DataFormatCheck

            #region DataProcess

            var tradeRecords = new List<DailyRecord>();

            foreach (DataRow row in importDataTable.Rows)
            {
                var tradeRecord = new DailyRecord();

                tradeRecord.SetTradeRecordCommonFields(importOperation);

                var stockCode = CommonHelper.StockCodeZerofill(row["证券代码"].ToString().Trim());

                var stockName = row["证券名称"].ToString().Trim();

                var stockInfo = _stockService.GetStockInfoByCode(stockCode);

                if (!VerifySotckInfo(stockInfo, stockCode, stockName)) return false;

                tradeRecord.StockCode = stockInfo.FullCode;

                tradeRecord.StockName = stockName;

                var stockPoolInfo = _stockService.GetStockPoolInfoByStockId(stockInfo.Id);

                var tradeType = row["交易类别"].ToString().Trim();
                tradeRecord.SetTradeType(tradeType);

                if (!IsExistedInPool(stockPoolInfo, tradeRecord.StockCode, tradeRecord.StockName, tradeRecord.TradeType)) continue;

                // tradeRecord.SetBeneficiary(stockPoolInfo);
                tradeRecord.SetBeneficiary(this.luBandPrincipal.SelectedValue(), this.luTargetPrincipal.SelectedValue());

                // tradeRecord.TradeDate = _commonService.GetCurrentServerTime().Date;

                tradeRecord.TradeTime = string.Empty;

                tradeRecord.DealFlag = row["操作"].ToString().Trim().IndexOf("买入") > -1 ? true : false;

                tradeRecord.DealNo = row["成交编号"].ToString().Trim();

                tradeRecord.DealPrice = decimal.Parse(row["成交均价"].ToString().Trim());

                tradeRecord.DealAmount = decimal.Parse(row["成交金额"].ToString().Trim());

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

                tradeRecords.Add(tradeRecord);
            }

            _tradeRecordService.InsertDailyRecords(tradeRecords);

            #endregion DataProcess

            return true;
        }

        #endregion 当日成交--华泰证券（普通）

        #region 当日成交--华泰证券（信用）

        /// <summary>
        ///  当日成交--华泰证券（信用）
        /// </summary>
        /// <param name="importOperation"></param>
        /// <param name="importDataTable"></param>
        private bool DailyImportHuaTai_C(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            #region DataFormatCheck

            var TemplateColumnNames = new List<string> { "成交日期", "合同编号", "证券代码", "证券名称", "操作", "成交均价", "成交数量", "成交金额", "成交编号", "股东帐户", "交易市场", "备注", "交易类别" };

            if (!DataFormatCheck(TemplateColumnNames, importDataTable)) return false;

            #endregion DataFormatCheck

            #region DataProcess

            var tradeRecords = new List<DailyRecord>();

            foreach (DataRow row in importDataTable.Rows)
            {
                var tradeRecord = new DailyRecord();

                tradeRecord.SetTradeRecordCommonFields(importOperation);

                var stockCode = CommonHelper.StockCodeZerofill(row["证券代码"].ToString().Trim());

                var stockName = row["证券名称"].ToString().Trim();

                var stockInfo = _stockService.GetStockInfoByCode(stockCode);

                if (!VerifySotckInfo(stockInfo, stockCode, stockName)) return false;

                tradeRecord.StockCode = stockInfo.FullCode;

                tradeRecord.StockName = stockName;

                var stockPoolInfo = _stockService.GetStockPoolInfoByStockId(stockInfo.Id);

                var tradeType = row["交易类别"].ToString().Trim();
                tradeRecord.SetTradeType(tradeType);

                if (!IsExistedInPool(stockPoolInfo, tradeRecord.StockCode, tradeRecord.StockName, tradeRecord.TradeType)) continue;

                // tradeRecord.SetBeneficiary(stockPoolInfo);
                tradeRecord.SetBeneficiary(this.luBandPrincipal.SelectedValue(), this.luTargetPrincipal.SelectedValue());

                // tradeRecord.TradeDate = _commonService.GetCurrentServerTime().Date;

                tradeRecord.TradeTime = string.Empty;

                tradeRecord.DealFlag = row["操作"].ToString().Trim().IndexOf("买入") > -1 ? true : false;

                tradeRecord.DealNo = row["成交编号"].ToString().Trim();

                tradeRecord.DealPrice = decimal.Parse(row["成交均价"].ToString().Trim());

                tradeRecord.DealAmount = decimal.Parse(row["成交金额"].ToString().Trim());

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

                tradeRecord.Remarks = row["备注"].ToString().Trim();
                tradeRecords.Add(tradeRecord);
            }

            _tradeRecordService.InsertDailyRecords(tradeRecords);

            #endregion DataProcess

            return true;
        }

        #endregion 当日成交--华泰证券（信用）

        #region 当日成交--申万证券（普通）

        /// <summary>
        ///  当日成交--申万证券（普通）
        /// </summary>
        /// <param name="importOperation"></param>
        /// <param name="importDataTable"></param>
        private bool DailyImportShenWan_N(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            #region DataFormatCheck

            var TemplateColumnNames = new List<string> { "委托时间", "委托编号", "证券代码", "证券名称", "买卖标志", "委托价格", "委托数量", "成交价格", "成交数量", "成交金额", "成交时间", "股东代码", "资金帐号", "客户代码", "股东姓名", "交易所名称", "交易类别" };

            if (!DataFormatCheck(TemplateColumnNames, importDataTable)) return false;

            #endregion DataFormatCheck

            #region DataProcess

            var tradeRecords = new List<DailyRecord>();

            foreach (DataRow row in importDataTable.Rows)
            {
                var tradeRecord = new DailyRecord();

                tradeRecord.SetTradeRecordCommonFields(importOperation);

                var stockCode = CommonHelper.StockCodeZerofill(row["证券代码"].ToString().Trim());

                var stockName = row["证券名称"].ToString().Trim();

                var stockInfo = _stockService.GetStockInfoByCode(stockCode);

                if (!VerifySotckInfo(stockInfo, stockCode, stockName)) return false;

                tradeRecord.StockCode = stockInfo.FullCode;

                tradeRecord.StockName = stockName;

                var stockPoolInfo = _stockService.GetStockPoolInfoByStockId(stockInfo.Id);

                var tradeType = row["交易类别"].ToString().Trim();
                tradeRecord.SetTradeType(tradeType);

                if (!IsExistedInPool(stockPoolInfo, tradeRecord.StockCode, tradeRecord.StockName, tradeRecord.TradeType)) continue;

                // tradeRecord.SetBeneficiary(stockPoolInfo);
                tradeRecord.SetBeneficiary(this.luBandPrincipal.SelectedValue(), this.luTargetPrincipal.SelectedValue());

                // tradeRecord.TradeDate = _commonService.GetCurrentServerTime().Date;

                tradeRecord.TradeTime = row["成交时间"].ToString().Trim();

                tradeRecord.DealFlag = row["买卖标志"].ToString().Trim().IndexOf("买入") > -1 ? true : false;

                tradeRecord.DealNo = string.Empty;

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

                tradeRecords.Add(tradeRecord);
            }

            _tradeRecordService.InsertDailyRecords(tradeRecords);

            #endregion DataProcess

            return true;
        }

        #endregion 当日成交--申万证券（普通）

        #region 当日成交--浙商证券（普通）

        /// <summary>
        ///  当日成交--浙商证券（普通）
        /// </summary>
        /// <param name="importOperation"></param>
        /// <param name="importDataTable"></param>
        private bool DailyImportZheShang_N(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            #region DataFormatCheck

            var TemplateColumnNames = new List<string> { "成交日期", "成交时间", "证券代码", "证券名称", "操作", "成交均价", "成交数量", "成交金额", "成交编号", "合同编号", "申报编号", "股东帐户", "交易市场", "交易类别" };

            if (!DataFormatCheck(TemplateColumnNames, importDataTable)) return false;

            #endregion DataFormatCheck

            #region DataProcess

            var tradeRecords = new List<DailyRecord>();

            foreach (DataRow row in importDataTable.Rows)
            {
                var tradeRecord = new DailyRecord();

                tradeRecord.SetTradeRecordCommonFields(importOperation);

                var stockCode = CommonHelper.StockCodeZerofill(row["证券代码"].ToString().Trim());

                var stockName = row["证券名称"].ToString().Trim();

                var stockInfo = _stockService.GetStockInfoByCode(stockCode);

                if (!VerifySotckInfo(stockInfo, stockCode, stockName)) return false;

                tradeRecord.StockCode = stockInfo.FullCode;

                tradeRecord.StockName = stockName;

                var stockPoolInfo = _stockService.GetStockPoolInfoByStockId(stockInfo.Id);

                var tradeType = row["交易类别"].ToString().Trim();
                tradeRecord.SetTradeType(tradeType);

                if (!IsExistedInPool(stockPoolInfo, tradeRecord.StockCode, tradeRecord.StockName, tradeRecord.TradeType)) continue;

                // tradeRecord.SetBeneficiary(stockPoolInfo);
                tradeRecord.SetBeneficiary(this.luBandPrincipal.SelectedValue(), this.luTargetPrincipal.SelectedValue());

                //   tradeRecord.TradeDate = _commonService.GetCurrentServerTime().Date;

                tradeRecord.TradeTime = row["成交时间"].ToString().Trim();

                tradeRecord.DealFlag = row["操作"].ToString().Trim().IndexOf("买入") > -1 ? true : false;

                tradeRecord.DealNo = row["成交编号"].ToString().Trim();

                tradeRecord.DealPrice = decimal.Parse(row["成交均价"].ToString().Trim());

                tradeRecord.DealAmount = decimal.Parse(row["成交金额"].ToString().Trim());

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

                tradeRecords.Add(tradeRecord);
            }

            _tradeRecordService.InsertDailyRecords(tradeRecords);

            #endregion DataProcess

            return true;
        }

        #endregion 当日成交--浙商证券（普通）

        #region 当日成交--中信证券（普通）

        /// <summary>
        ///  当日成交--中信证券（普通）
        /// </summary>
        /// <param name="importOperation"></param>
        /// <param name="importDataTable"></param>
        private bool DailyImportCITIC_N(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            #region DataFormatCheck

            var TemplateColumnNames = new List<string> { "成交时间", "申请编号", "证券代码", "证券名称", "买卖", "委托类型", "成交价格", "成交数量", "成交金额", "成交状态", "股东代码", "委托编号", "成交编号", "交易市场", "交易类别" };

            if (!DataFormatCheck(TemplateColumnNames, importDataTable)) return false;

            #endregion DataFormatCheck

            #region DataProcess

            var tradeRecords = new List<DailyRecord>();

            foreach (DataRow row in importDataTable.Rows)
            {
                var tradeRecord = new DailyRecord();

                tradeRecord.SetTradeRecordCommonFields(importOperation);

                var stockCode = CommonHelper.StockCodeZerofill(row["证券代码"].ToString().Trim());

                var stockName = row["证券名称"].ToString().Trim();

                var stockInfo = _stockService.GetStockInfoByCode(stockCode);

                if (!VerifySotckInfo(stockInfo, stockCode, stockName)) return false;

                tradeRecord.StockCode = stockInfo.FullCode;

                tradeRecord.StockName = stockName;

                var stockPoolInfo = _stockService.GetStockPoolInfoByStockId(stockInfo.Id);

                var tradeType = row["交易类别"].ToString().Trim();
                tradeRecord.SetTradeType(tradeType);

                if (!IsExistedInPool(stockPoolInfo, tradeRecord.StockCode, tradeRecord.StockName, tradeRecord.TradeType)) continue;

                // tradeRecord.SetBeneficiary(stockPoolInfo);
                tradeRecord.SetBeneficiary(this.luBandPrincipal.SelectedValue(), this.luTargetPrincipal.SelectedValue());

                //   tradeRecord.TradeDate = _commonService.GetCurrentServerTime().Date;

                tradeRecord.TradeTime = row["成交时间"].ToString().Trim();

                tradeRecord.DealFlag = row["买卖"].ToString().Trim().IndexOf("买入") > -1 ? true : false;

                tradeRecord.DealNo = row["成交编号"].ToString().Trim();

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

                tradeRecords.Add(tradeRecord);
            }

            _tradeRecordService.InsertDailyRecords(tradeRecords);

            #endregion DataProcess

            return true;
        }

        #endregion 当日成交--中信证券（普通）

        #region 当日成交--中银国际（信用）

        /// <summary>
        /// 当日成交--中银国际（信用）
        /// </summary>
        /// <param name="importOperation"></param>
        /// <param name="importDataTable"></param>
        private bool DialyImportBOCI_C(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            #region DataFormatCheck

            var TemplateColumnNames = new List<string> { "成交时间", "证券代码", "证券名称", "买卖标志", "成交价格", "成交数量", "成交金额", "成交编号", "委托编号", "股东代码", "成交类型", "交易类别" };

            if (!DataFormatCheck(TemplateColumnNames, importDataTable)) return false;

            #endregion DataFormatCheck

            #region DataProcess

            var tradeRecords = new List<DailyRecord>();

            foreach (DataRow row in importDataTable.Rows)
            {
                var tradeRecord = new DailyRecord();

                tradeRecord.SetTradeRecordCommonFields(importOperation);

                var stockCode = CommonHelper.StockCodeZerofill(row["证券代码"].ToString().Trim());

                var stockName = row["证券名称"].ToString().Trim();

                var stockInfo = _stockService.GetStockInfoByCode(stockCode);

                if (!VerifySotckInfo(stockInfo, stockCode, stockName)) return false;

                tradeRecord.StockCode = stockInfo.FullCode;

                tradeRecord.StockName = stockName;

                var stockPoolInfo = _stockService.GetStockPoolInfoByStockId(stockInfo.Id);

                var tradeType = row["交易类别"].ToString().Trim();
                tradeRecord.SetTradeType(tradeType);

                if (!IsExistedInPool(stockPoolInfo, tradeRecord.StockCode, tradeRecord.StockName, tradeRecord.TradeType)) continue;

                // tradeRecord.SetBeneficiary(stockPoolInfo);
                tradeRecord.SetBeneficiary(this.luBandPrincipal.SelectedValue(), this.luTargetPrincipal.SelectedValue());

                // tradeRecord.TradeDate = _commonService.GetCurrentServerTime().Date;

                tradeRecord.TradeTime = row["成交时间"].ToString().Trim();

                tradeRecord.DealFlag = row["买卖标志"].ToString().Trim().IndexOf("买入") > -1 ? true : false;

                tradeRecord.DealNo = row["成交编号"].ToString().Trim();

                tradeRecord.DealPrice = decimal.Parse(row["成交价格"].ToString().Trim());

                tradeRecord.DealAmount = decimal.Parse(row["成交金额"].ToString().Trim());

                tradeRecord.StockHolderCode = row["股东代码"].ToString().Trim();

                tradeRecord.ContractNo = row["委托编号"].ToString().Trim();

                //账号信息
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

                tradeRecords.Add(tradeRecord);
            }

            _tradeRecordService.InsertDailyRecords(tradeRecords);

            #endregion DataProcess

            return true;
        }

        #endregion 当日成交--中银国际（信用）

        #endregion 当日成交数据导入

        #region 交割单数据导入

        #region 交割单--券商选择

        /// <summary>
        /// 交割单--券商选择
        /// </summary>
        /// <param name="importOperation"></param>
        /// <param name="importDataTable"></param>
        /// <returns></returns>
        private bool DeliveryImport(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            bool result = false;
            switch (this._securityAccount)
            {
                //财通信用
                case SecurityAccount.CaiTong_C:
                    result = DeliveryImportCaiTong_C(importOperation, importDataTable);
                    break;

                //财通普通
                case SecurityAccount.CaiTong_N:
                    result = DeliveryImportCaiTong_N(importOperation, importDataTable);
                    break;

                //方正普通
                case SecurityAccount.Founder_N:
                    result = DeliveryImportFounder_N(importOperation, importDataTable);
                    break;

                //国金普通
                case SecurityAccount.SinoLink_N:
                    result = DeliveryImportSinoLink_N(importOperation, importDataTable);
                    break;

                //国泰信用
                case SecurityAccount.GuoTai_C:
                    result = DeliveryImportGuoTai_C(importOperation, importDataTable);
                    break;

                //国泰普通
                case SecurityAccount.GuoTai_N:
                    result = DeliveryImportGuoTai_N(importOperation, importDataTable);
                    break;

                //华泰信用
                case SecurityAccount.HuaTai_C:
                    result = DeliveryImportHuaTai_C(importOperation, importDataTable);
                    break;

                //华泰普通
                case SecurityAccount.HuaTai_N:
                    result = DeliveryImportHuaTai_N(importOperation, importDataTable);
                    break;

                //申万普通
                case SecurityAccount.ShenWan_N:
                    result = DeliveryImportShenWan_N(importOperation, importDataTable);
                    break;

                //银河普通
                case SecurityAccount.Galaxy_N:
                    result = DeliveryImportGalaxy_N(importOperation, importDataTable);
                    break;

                //招商普通
                case SecurityAccount.ZhaoShang_N:
                    result = DeliveryImportZhaoShang_N(importOperation, importDataTable);
                    break;

                //浙商普通
                case SecurityAccount.ZheShang_N:
                    result = DeliveryImportZheShang_N(importOperation, importDataTable);
                    break;

                //中信信用
                case SecurityAccount.CITIC_C:
                    result = DeliveryImportCITIC_C(importOperation, importDataTable);
                    break;

                //中信普通
                case SecurityAccount.CITIC_N:
                    result = DeliveryImportCITIC_N(importOperation, importDataTable);
                    break;

                //中银信用
                case SecurityAccount.BOCI_C:
                    result = DeliveryImportBOCI_C(importOperation, importDataTable);
                    break;

                //中银普通
                case SecurityAccount.BOCI_N:
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
        private bool DeliveryImportCaiTong_C(RecordImportOperationEntity importOperation, DataTable importDataTable)
        {
            #region DataFormatCheck

            var TemplateColumnNames = new List<string> { "证券代码", "证券名称", "操作", "成交均价", "成交数量", "成交金额", "印花税", "过户费", "发生金额", "合同编号", "股东帐户", "委托日期", "可用余额", "其他杂费", "佣金", "可用金额", "交易类别" };

            if (!DataFormatCheck(TemplateColumnNames, importDataTable)) return false;

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

                if (!VerifySotckInfo(stockInfo, stockCode, stockName)) return false;

                tradeRecord.StockCode = stockInfo.FullCode;

                tradeRecord.StockName = stockName;

                var stockPoolInfo = _stockService.GetStockPoolInfoByStockId(stockInfo.Id);

                var tradeType = row["交易类别"].ToString().Trim();
                tradeRecord.SetTradeType(tradeType);

                if (!IsExistedInPool(stockPoolInfo, tradeRecord.StockCode, tradeRecord.StockName, tradeRecord.TradeType)) continue;

                // tradeRecord.SetBeneficiary(stockPoolInfo);
                tradeRecord.SetBeneficiary(this.luBandPrincipal.SelectedValue(), this.luTargetPrincipal.SelectedValue());

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

            _tradeRecordService.InsertDailyRecords(tradeRecords);

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

            if (!DataFormatCheck(TemplateColumnNames, importDataTable)) return false;

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

                if (!VerifySotckInfo(stockInfo, stockCode, stockName)) return false;

                tradeRecord.StockCode = stockInfo.FullCode;

                tradeRecord.StockName = stockName;

                var stockPoolInfo = _stockService.GetStockPoolInfoByStockId(stockInfo.Id);

                var tradeType = row["交易类别"].ToString().Trim();
                tradeRecord.SetTradeType(tradeType);

                if (!IsExistedInPool(stockPoolInfo, tradeRecord.StockCode, tradeRecord.StockName, tradeRecord.TradeType)) continue;

                // tradeRecord.SetBeneficiary(stockPoolInfo);
                tradeRecord.SetBeneficiary(this.luBandPrincipal.SelectedValue(), this.luTargetPrincipal.SelectedValue());

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

            _tradeRecordService.InsertDailyRecords(tradeRecords);

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

            if (!DataFormatCheck(TemplateColumnNames, importDataTable)) return false;

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

                if (!VerifySotckInfo(stockInfo, stockCode, stockName)) return false;

                tradeRecord.StockCode = stockInfo.FullCode;

                tradeRecord.StockName = stockName;

                var stockPoolInfo = _stockService.GetStockPoolInfoByStockId(stockInfo.Id);

                var tradeType = row["交易类别"].ToString().Trim();
                tradeRecord.SetTradeType(tradeType);

                if (!IsExistedInPool(stockPoolInfo, tradeRecord.StockCode, tradeRecord.StockName, tradeRecord.TradeType)) continue;

                // tradeRecord.SetBeneficiary(stockPoolInfo);
                tradeRecord.SetBeneficiary(this.luBandPrincipal.SelectedValue(), this.luTargetPrincipal.SelectedValue());

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

            _tradeRecordService.InsertDailyRecords(tradeRecords);

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

            if (!DataFormatCheck(TemplateColumnNames, importDataTable)) return false;

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

                if (!VerifySotckInfo(stockInfo, stockCode, stockName)) return false;

                tradeRecord.StockCode = stockInfo.FullCode;

                tradeRecord.StockName = stockName;

                var stockPoolInfo = _stockService.GetStockPoolInfoByStockId(stockInfo.Id);

                var tradeType = row["交易类别"].ToString().Trim();
                tradeRecord.SetTradeType(tradeType);

                if (!IsExistedInPool(stockPoolInfo, tradeRecord.StockCode, tradeRecord.StockName, tradeRecord.TradeType)) continue;

                // tradeRecord.SetBeneficiary(stockPoolInfo);
                tradeRecord.SetBeneficiary(this.luBandPrincipal.SelectedValue(), this.luTargetPrincipal.SelectedValue());

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

            _tradeRecordService.InsertDailyRecords(tradeRecords);

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

            if (!DataFormatCheck(TemplateColumnNames, importDataTable)) return false;

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

                if (!VerifySotckInfo(stockInfo, stockCode, stockName)) return false;

                tradeRecord.StockCode = stockInfo.FullCode;

                tradeRecord.StockName = stockName;

                var stockPoolInfo = _stockService.GetStockPoolInfoByStockId(stockInfo.Id);

                var tradeType = row["交易类别"].ToString().Trim();
                tradeRecord.SetTradeType(tradeType);

                if (!IsExistedInPool(stockPoolInfo, tradeRecord.StockCode, tradeRecord.StockName, tradeRecord.TradeType)) continue;

                // tradeRecord.SetBeneficiary(stockPoolInfo);
                tradeRecord.SetBeneficiary(this.luBandPrincipal.SelectedValue(), this.luTargetPrincipal.SelectedValue());

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

            _tradeRecordService.InsertDailyRecords(tradeRecords);

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

            if (!DataFormatCheck(TemplateColumnNames, importDataTable)) return false;

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

                if (!VerifySotckInfo(stockInfo, stockCode, stockName)) return false;

                tradeRecord.StockCode = stockInfo.FullCode;

                tradeRecord.StockName = stockName;

                var stockPoolInfo = _stockService.GetStockPoolInfoByStockId(stockInfo.Id);

                var tradeType = row["交易类别"].ToString().Trim();
                tradeRecord.SetTradeType(tradeType);

                if (!IsExistedInPool(stockPoolInfo, tradeRecord.StockCode, tradeRecord.StockName, tradeRecord.TradeType)) continue;

                // tradeRecord.SetBeneficiary(stockPoolInfo);
                tradeRecord.SetBeneficiary(this.luBandPrincipal.SelectedValue(), this.luTargetPrincipal.SelectedValue());

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

            _tradeRecordService.InsertDailyRecords(tradeRecords);

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

            if (!DataFormatCheck(TemplateColumnNames, importDataTable)) return false;

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

                if (!VerifySotckInfo(stockInfo, stockCode, stockName)) return false;

                tradeRecord.StockCode = stockInfo.FullCode;

                tradeRecord.StockName = stockName;

                var stockPoolInfo = _stockService.GetStockPoolInfoByStockId(stockInfo.Id);

                var tradeType = row["交易类别"].ToString().Trim();
                tradeRecord.SetTradeType(tradeType);

                if (!IsExistedInPool(stockPoolInfo, tradeRecord.StockCode, tradeRecord.StockName, tradeRecord.TradeType)) continue;

                // tradeRecord.SetBeneficiary(stockPoolInfo);
                tradeRecord.SetBeneficiary(this.luBandPrincipal.SelectedValue(), this.luTargetPrincipal.SelectedValue());

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

            _tradeRecordService.InsertDailyRecords(tradeRecords);

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

            if (!DataFormatCheck(TemplateColumnNames, importDataTable)) return false;

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

                if (!VerifySotckInfo(stockInfo, stockCode, stockName)) return false;

                tradeRecord.StockCode = stockInfo.FullCode;

                tradeRecord.StockName = stockName;

                var stockPoolInfo = _stockService.GetStockPoolInfoByStockId(stockInfo.Id);

                var tradeType = row["交易类别"].ToString().Trim();
                tradeRecord.SetTradeType(tradeType);

                if (!IsExistedInPool(stockPoolInfo, tradeRecord.StockCode, tradeRecord.StockName, tradeRecord.TradeType)) continue;

                // tradeRecord.SetBeneficiary(stockPoolInfo);
                tradeRecord.SetBeneficiary(this.luBandPrincipal.SelectedValue(), this.luTargetPrincipal.SelectedValue());

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

            _tradeRecordService.InsertDailyRecords(tradeRecords);

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

            if (!DataFormatCheck(TemplateColumnNames, importDataTable)) return false;

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

                if (!VerifySotckInfo(stockInfo, stockCode, stockName)) return false;

                tradeRecord.StockCode = stockInfo.FullCode;

                tradeRecord.StockName = stockName;

                var stockPoolInfo = _stockService.GetStockPoolInfoByStockId(stockInfo.Id);

                var tradeType = row["交易类别"].ToString().Trim();
                tradeRecord.SetTradeType(tradeType);

                if (!IsExistedInPool(stockPoolInfo, tradeRecord.StockCode, tradeRecord.StockName, tradeRecord.TradeType)) continue;

                // tradeRecord.SetBeneficiary(stockPoolInfo);
                tradeRecord.SetBeneficiary(this.luBandPrincipal.SelectedValue(), this.luTargetPrincipal.SelectedValue());

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

            _tradeRecordService.InsertDailyRecords(tradeRecords);

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

            if (!DataFormatCheck(TemplateColumnNames, importDataTable)) return false;

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

                if (!VerifySotckInfo(stockInfo, stockCode, stockName)) return false;

                tradeRecord.StockCode = stockInfo.FullCode;

                tradeRecord.StockName = stockName;

                var stockPoolInfo = _stockService.GetStockPoolInfoByStockId(stockInfo.Id);

                var tradeType = row["交易类别"].ToString().Trim();
                tradeRecord.SetTradeType(tradeType);

                if (!IsExistedInPool(stockPoolInfo, tradeRecord.StockCode, tradeRecord.StockName, tradeRecord.TradeType)) continue;

                // tradeRecord.SetBeneficiary(stockPoolInfo);
                tradeRecord.SetBeneficiary(this.luBandPrincipal.SelectedValue(), this.luTargetPrincipal.SelectedValue());

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

            _tradeRecordService.InsertDailyRecords(tradeRecords);

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

            if (!DataFormatCheck(TemplateColumnNames, importDataTable)) return false;

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

                if (!VerifySotckInfo(stockInfo, stockCode, stockName)) return false;

                tradeRecord.StockCode = stockInfo.FullCode;

                tradeRecord.StockName = stockName;

                var stockPoolInfo = _stockService.GetStockPoolInfoByStockId(stockInfo.Id);

                var tradeType = row["交易类别"].ToString().Trim();
                tradeRecord.SetTradeType(tradeType);

                if (!IsExistedInPool(stockPoolInfo, tradeRecord.StockCode, tradeRecord.StockName, tradeRecord.TradeType)) continue;

                // tradeRecord.SetBeneficiary(stockPoolInfo);
                tradeRecord.SetBeneficiary(this.luBandPrincipal.SelectedValue(), this.luTargetPrincipal.SelectedValue());

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

            _tradeRecordService.InsertDailyRecords(tradeRecords);

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

            if (!DataFormatCheck(TemplateColumnNames, importDataTable)) return false;

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

                if (!VerifySotckInfo(stockInfo, stockCode, stockName)) return false;

                tradeRecord.StockCode = stockInfo.FullCode;

                tradeRecord.StockName = stockName;

                var stockPoolInfo = _stockService.GetStockPoolInfoByStockId(stockInfo.Id);

                var tradeType = row["交易类别"].ToString().Trim();
                tradeRecord.SetTradeType(tradeType);

                if (!IsExistedInPool(stockPoolInfo, tradeRecord.StockCode, tradeRecord.StockName, tradeRecord.TradeType)) continue;

                // tradeRecord.SetBeneficiary(stockPoolInfo);
                tradeRecord.SetBeneficiary(this.luBandPrincipal.SelectedValue(), this.luTargetPrincipal.SelectedValue());

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

            _tradeRecordService.InsertDailyRecords(tradeRecords);

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

            if (!DataFormatCheck(TemplateColumnNames, importDataTable)) return false;

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

                if (!VerifySotckInfo(stockInfo, stockCode, stockName)) return false;

                tradeRecord.StockCode = stockInfo.FullCode;

                tradeRecord.StockName = stockName;

                var stockPoolInfo = _stockService.GetStockPoolInfoByStockId(stockInfo.Id);

                var tradeType = row["交易类别"].ToString().Trim();
                tradeRecord.SetTradeType(tradeType);

                if (!IsExistedInPool(stockPoolInfo, tradeRecord.StockCode, tradeRecord.StockName, tradeRecord.TradeType)) continue;

                // tradeRecord.SetBeneficiary(stockPoolInfo);
                tradeRecord.SetBeneficiary(this.luBandPrincipal.SelectedValue(), this.luTargetPrincipal.SelectedValue());

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

            _tradeRecordService.InsertDailyRecords(tradeRecords);

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

            if (!DataFormatCheck(TemplateColumnNames, importDataTable)) return false;

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

                if (!VerifySotckInfo(stockInfo, stockCode, stockName)) return false;

                tradeRecord.StockCode = stockInfo.FullCode;

                tradeRecord.StockName = stockName;

                var stockPoolInfo = _stockService.GetStockPoolInfoByStockId(stockInfo.Id);

                var tradeType = row["交易类别"].ToString().Trim();
                tradeRecord.SetTradeType(tradeType);

                if (!IsExistedInPool(stockPoolInfo, tradeRecord.StockCode, tradeRecord.StockName, tradeRecord.TradeType)) continue;

                // tradeRecord.SetBeneficiary(stockPoolInfo);
                tradeRecord.SetBeneficiary(this.luBandPrincipal.SelectedValue(), this.luTargetPrincipal.SelectedValue());

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

            _tradeRecordService.InsertDailyRecords(tradeRecords);

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

            if (!DataFormatCheck(TemplateColumnNames, importDataTable)) return false;

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

                if (!VerifySotckInfo(stockInfo, stockCode, stockName)) return false;

                tradeRecord.StockCode = stockInfo.FullCode;

                tradeRecord.StockName = stockName;

                var stockPoolInfo = _stockService.GetStockPoolInfoByStockId(stockInfo.Id);

                var tradeType = row["交易类别"].ToString().Trim();
                tradeRecord.SetTradeType(tradeType);

                if (!IsExistedInPool(stockPoolInfo, tradeRecord.StockCode, tradeRecord.StockName, tradeRecord.TradeType)) continue;
                // tradeRecord.SetBeneficiary(stockPoolInfo);
                tradeRecord.SetBeneficiary(this.luBandPrincipal.SelectedValue(), this.luTargetPrincipal.SelectedValue());

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

            _tradeRecordService.InsertDailyRecords(tradeRecords);

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

            if (!DataFormatCheck(TemplateColumnNames, importDataTable)) return false;

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

                if (!VerifySotckInfo(stockInfo, stockCode, stockName)) return false;

                tradeRecord.StockCode = stockInfo.FullCode;

                tradeRecord.StockName = stockName;

                var stockPoolInfo = _stockService.GetStockPoolInfoByStockId(stockInfo.Id);

                var tradeType = row["交易类别"].ToString().Trim();
                tradeRecord.SetTradeType(tradeType);

                if (!IsExistedInPool(stockPoolInfo, tradeRecord.StockCode, tradeRecord.StockName, tradeRecord.TradeType)) continue;

                // tradeRecord.SetBeneficiary(stockPoolInfo);
                tradeRecord.SetBeneficiary(this.luBandPrincipal.SelectedValue(), this.luTargetPrincipal.SelectedValue());

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

            _tradeRecordService.InsertDailyRecords(tradeRecords);

            #endregion DataProcess

            return true;
        }

        #endregion 交割单--中银国际（普通）

        #endregion 交割单数据导入

        #endregion 数据导入
    }
}