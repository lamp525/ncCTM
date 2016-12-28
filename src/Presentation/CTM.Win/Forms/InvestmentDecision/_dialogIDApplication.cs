using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using CTM.Core;
using CTM.Core.Domain.InvestmentDecision;
using CTM.Core.Infrastructure;
using CTM.Core.Util;
using CTM.Data;
using CTM.Services.Common;
using CTM.Services.InvestmentDecision;
using CTM.Services.Stock;
using CTM.Services.User;
using CTM.Win.Extensions;
using CTM.Win.Models;
using CTM.Win.Util;

namespace CTM.Win.Forms.InvestmentDecision
{
    public partial class _dialogIDApplication : BaseForm
    {
        #region Fields

        private readonly ICommonService _commonService;
        private readonly IInvestmentDecisionService _IDService;
        private readonly IStockService _stockService;
        private readonly IUserService _userService;

        private bool _initialFlag = true;
        private bool _submitSucceedFlag = false;
        private bool _profitLossBoundSettingFlag = true;

        private _embedAppointedStockApplication _embedASA = null;
        private _embedIDOperationDetail _embedIDOD = null;
        private _embedIDOperationVote _embedIDOV = null;
        private _embedIDOperationAccuracy _embedIDOA = null;
        private _embedIDOperationExecute _embedIDOE = null;

        #endregion Fields

        #region Properties

        public string ApplyNo { get; set; }

        public EnumLibrary.IDOperationApplyType ApplyType { get; set; }

        public string OperateNo { get; set; }

        public int OperateStep { get; set; }

        #endregion Properties

        #region Enums

        public enum PageMode
        {
            /// <summary>
            /// 新建投资决策申请
            /// </summary>
            NewApplication,

            /// <summary>
            /// 新建投资决策操作
            /// </summary>
            NewOperation,

            /// <summary>
            /// 查看详情
            /// </summary>
            ViewDetail,

            /// <summary>
            /// 决策投票
            /// </summary>
            OperationVote,

            /// <summary>
            /// 准确度判定
            /// </summary>
            AccuracyDetermination,

            /// <summary>
            /// 执行确认
            /// </summary>
            ExecutionConfirm
        }

        #endregion Enums

        #region Properties

        public PageMode CurrentPageMode { get; set; }

        #endregion Properties

        #region Delegates

        public delegate void RefreshParentForm();

        public event RefreshParentForm RefreshEvent;

        #endregion Delegates

        #region Constructors

        public _dialogIDApplication(
            ICommonService commonService,
            IInvestmentDecisionService IDService,
            IStockService stockService,
            IUserService userService)
        {
            InitializeComponent();

            this._commonService = commonService;
            this._IDService = IDService;
            this._stockService = stockService;
            this._userService = userService;
        }

        #endregion Constructors

        #region Utilities

        private void FormInit()
        {
            var now = _commonService.GetCurrentServerTime();

            //申请日期
            this.deApply.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;

            //股票
            var stocks = _stockService.GetAllStocks(showDeleted: true)
                .Select(x => new StockInfoModel
                {
                    Id = x.Id,
                    Code = x.Code,
                    FullCode = x.FullCode,
                    Name = x.Name,
                    DisplayMember = x.FullCode + " - " + x.Name,
                }
           ).OrderBy(x => x.FullCode).ToList();
            this.luStock.Initialize(stocks, "FullCode", "DisplayMember", enableSearch: true);

            //操作类型
            var operateTypes = new List<ComboBoxItemModel>
            {
                new ComboBoxItemModel
                {
                    Text ="目标",
                    Value ="1",
                },
                new ComboBoxItemModel
                {
                    Text ="波段",
                    Value ="2",
                },
                       new ComboBoxItemModel
                {
                    Text ="隔日短差",
                    Value ="3",
                },
            };

            this.txtOperateUser.Text = LoginInfo.CurrentUser.UserName;
            this.cbOperateType.Initialize(operateTypes);

            //操作日期
            this.deOperate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;

            switch (ApplyType)
            {
                case EnumLibrary.IDOperationApplyType.Buy:
                    this.chkBuy.Checked = true;
                    this.chkSell.Enabled = false;
                    break;

                case EnumLibrary.IDOperationApplyType.Sell:
                    this.chkSell.Checked = true;
                    this.chkBuy.Enabled = false;
                    break;

                case EnumLibrary.IDOperationApplyType.Both:
                    break;

                default:
                    break;
            }

            this.txtPrice.SetNumericMask(2);
            this.txtPrice.Text = string.Empty;
            this.spinPriceBound.SetProperties();
            this.txtVolume.SetNumberMask();
            this.txtVolume.Text = string.Empty;
            this.txtProfitPrice.SetNumericMask(2);
            this.txtProfitPrice.Text = string.Empty;
            this.spinProfitBound.SetProperties();
            this.txtLossPrice.SetNumericMask(2);
            this.txtLossPrice.Text = string.Empty;
            this.spinLossBound.SetProperties();

            //理由类别
            var categories = _IDService.GetIDReasonCategories();

            this.treeListLookUpEdit1.Properties.DisplayMember = "FullName";
            this.treeListLookUpEdit1.Properties.ValueMember = "Id";
            this.treeListLookUpEdit1TreeList.Initialize(categories, "Id", "ParentId", editable: false, autoWidth: true, showColumns: false, showVertLines: false, showHorzLines: false, multiSelect: true);
            categories = null;

            if (string.IsNullOrEmpty(ApplyNo))
            {
                //申请单号
                this.txtApplyNo.Text = _IDService.GenerateIDApplicationApplyNo();

                //申请人员
                this.txtApplyUser.Text = LoginInfo.CurrentUser.UserName;

                //申请日期
                this.deApply.EditValue = now.Date;

                //操作类型
                this.cbOperateType.DefaultSelected("1");
            }
            else
            {
                this.lcgImport.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

                SetApplicationInfo(ApplyNo);
            }

            if (string.IsNullOrEmpty(OperateNo))
            {
                //操作编号
                this.txtOperateNo.Text = _IDService.GenerateIDOperationNo();

                //操作人员
                this.txtOperateUser.Text = LoginInfo.CurrentUser.UserName;

                //操作日期
                this.deOperate.EditValue = now.Date;

                if (OperateStep == 1 || (this.txtApplyNo.Tag != null && bool.Parse(this.txtApplyNo.Tag.ToString()) == this.chkBuy.Checked))
                    _profitLossBoundSettingFlag = true;
                else
                    _profitLossBoundSettingFlag = false;

                if (!_profitLossBoundSettingFlag)
                {
                    this.txtProfitPrice.ReadOnly = true;
                    this.spinProfitBound.ReadOnly = true;
                    this.txtLossPrice.ReadOnly = true;
                    this.spinLossBound.ReadOnly = true;
                }
            }
            else
            {
                this.lcgApply.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

                SetOperationInfo(OperateNo);
            }
        }

        private void SetLimitBound()
        {
            throw new NotImplementedException();
        }

        private void SetOperationInfo(string operateNo)
        {
            this.txtOperateNo.ReadOnly = true;
            this.txtOperateUser.ReadOnly = true;
            this.deOperate.ReadOnly = true;
            this.chkBuy.ReadOnly = true;
            this.chkSell.ReadOnly = true;
            this.txtPrice.ReadOnly = true;
            this.spinPriceBound.ReadOnly = true;
            this.txtVolume.ReadOnly = true;
            this.txtAmount.ReadOnly = true;
            this.txtProfitPrice.ReadOnly = true;
            this.spinProfitBound.ReadOnly = true;
            this.txtLossPrice.ReadOnly = true;
            this.spinLossBound.ReadOnly = true;
            this.treeListLookUpEdit1.ReadOnly = true;
            this.memoReason.ReadOnly = true;

            var connString = System.Configuration.ConfigurationManager.ConnectionStrings["CTMContext"].ToString();
            var commandText = $@" SELECT * FROM [dbo].[v_IDOperation] WHERE OperateNo = '{operateNo}' ";
            var ds = SqlHelper.ExecuteDataset(connString, CommandType.Text, commandText);

            var operationInfo = ds?.Tables?[0].Rows?[0];

            if (operationInfo == null) return;

            this.txtOperateNo.Text = operationInfo["OperateNo"].ToString();
            this.txtOperateUser.Text = operationInfo["OperateUserName"].ToString();
            this.deOperate.EditValue = operationInfo["OperateDate"];
            this.chkBuy.Checked = bool.Parse(operationInfo["DealFlag"].ToString()) ? true : false;
            this.txtPrice.Text = operationInfo["DealPrice"].ToString();
            this.spinPriceBound.EditValue = CommonHelper.SetDecimalDigits(decimal.Parse(operationInfo["PriceBound"].ToString()) * (int)EnumLibrary.NumericUnit.Hundred, 0);
            this.txtAmount.Text = (decimal.Parse(operationInfo["DealAmount"].ToString()) / (int)EnumLibrary.NumericUnit.TenThousand).ToString();
            this.txtVolume.Text = operationInfo["DealVolume"].ToString();
            this.txtProfitPrice.Text = operationInfo["StopProfitPrice"].ToString();
            this.spinProfitBound.EditValue = CommonHelper.SetDecimalDigits(decimal.Parse(operationInfo["StopProfitBound"].ToString()) * (int)EnumLibrary.NumericUnit.Hundred, 0);
            this.txtLossPrice.Text = operationInfo["StopLossPrice"].ToString();
            this.spinLossBound.EditValue = CommonHelper.SetDecimalDigits(decimal.Parse(operationInfo["StopLossBound"].ToString()) * (int)EnumLibrary.NumericUnit.Hundred, 0);
            this.treeListLookUpEdit1.EditValue = operationInfo["ReasonCategoryId"];
            this.memoReason.Text = operationInfo["ReasonContent"].ToString();
        }

        private void SetApplicationInfo(object applyNo)
        {
            this.txtApplyNo.ReadOnly = true;
            this.txtPlanNo.ReadOnly = true;
            this.txtApplyUser.ReadOnly = true;
            this.deApply.ReadOnly = true;
            this.luStock.ReadOnly = true;
            this.cbOperateType.ReadOnly = true;

            var connString = System.Configuration.ConfigurationManager.ConnectionStrings["CTMContext"].ToString();
            var commandText = $@" SELECT * FROM [dbo].[v_IDApplication] WHERE ApplyNo = '{applyNo}' ";

            var ds = SqlHelper.ExecuteDataset(connString, CommandType.Text, commandText);

            var applicationInfo = ds?.Tables?[0].Rows?[0];

            if (applicationInfo == null) return;

            this.txtApplyNo.Text = applicationInfo["ApplyNo"].ToString();
            this.txtApplyNo.Tag = applicationInfo["InitialDealFlag"].ToString();
            this.txtPlanNo.Text = applicationInfo["TradePlanNo"].ToString();
            this.txtApplyUser.Text = applicationInfo["ApplyUserName"].ToString();
            this.deApply.EditValue = applicationInfo["ApplyDate"];
            this.luStock.EditValue = applicationInfo["StockCode"];
            this.cbOperateType.DefaultSelected(applicationInfo["TradeType"].ToString());
        }

        private bool SubmitProcess()
        {
            InvestmentDecisionApplication application = null;
            InvestmentDecisionOperation operation = null;

            var now = _commonService.GetCurrentServerTime();

            if (string.IsNullOrEmpty(ApplyNo))
            {
                if (string.IsNullOrEmpty(this.luStock.SelectedValue()))
                {
                    DXMessage.ShowTips("请选择股票信息！");
                    this.luStock.Focus();
                    return false;
                }

                var applyDate = CommonHelper.StringToDateTime(this.deApply.EditValue.ToString());
                var tradeType = int.Parse(this.cbOperateType.SelectedValue());
                var stock = this.luStock.GetSelectedDataRow() as StockInfoModel;

                application = new InvestmentDecisionApplication
                {
                    ApplyNo = string.Empty,
                    ApplyDate = applyDate,
                    ApplyUser = LoginInfo.CurrentUser.UserCode,
                    BuyAmount = 0,
                    BuyVolume = 0,
                    CreateTime = now,
                    DepartmentId = LoginInfo.CurrentUser.DepartmentId,
                    InitialDealFlag = this.chkBuy.Checked,
                    IsDeleted = false,
                    PositionVolume = 0,
                    Profit = 0,
                    SellAmount = 0,
                    SellVolume = 0,
                    Status = (int)EnumLibrary.IDApplicationStatus.Proceed,
                    StockCode = stock.FullCode,
                    StockName = stock.Name,
                    TradePlanNo = txtPlanNo.Text.Trim(),
                    TradeType = tradeType,
                    UpdateTime = now,
                };
            }

            if (string.IsNullOrEmpty(OperateNo))
            {
                if (this.txtPrice.Text.Trim().Length == 0)
                {
                    DXMessage.ShowTips("请输入单价！");
                    this.txtPrice.Focus();
                    return false;
                }

                if (decimal.Parse(this.txtPrice.Text.Trim()) <= 0)
                {
                    DXMessage.ShowTips("单价应该大于0！");
                    this.txtPrice.Focus();
                    return false;
                }

                if (this.txtVolume.Text.Trim().Length == 0)
                {
                    DXMessage.ShowTips("请输入数量！");
                    this.txtVolume.Focus();
                    return false;
                }

                if (decimal.Parse(this.txtVolume.Text.Trim()) <= 0)
                {
                    DXMessage.ShowTips("数量应该大于0！");
                    this.txtVolume.Focus();
                    return false;
                }

                if (_profitLossBoundSettingFlag)
                {
                    if (this.txtProfitPrice.Text.Trim().Length == 0)
                    {
                        DXMessage.ShowTips("请输入止盈价格！");
                        this.txtProfitPrice.Focus();
                        return false;
                    }

                    if (decimal.Parse(this.txtProfitPrice.Text.Trim()) <= 0)
                    {
                        DXMessage.ShowTips("止盈价格应该大于0！");
                        this.txtProfitPrice.Focus();
                        return false;
                    }

                    if (this.txtLossPrice.Text.Trim().Length == 0)
                    {
                        DXMessage.ShowTips("请输入止损价格！");
                        this.txtLossPrice.Focus();
                        return false;
                    }

                    if (decimal.Parse(this.txtLossPrice.Text.Trim()) <= 0)
                    {
                        DXMessage.ShowTips("止损价格应该大于0！");
                        this.txtLossPrice.Focus();
                        return false;
                    }
                }

                if (string.IsNullOrEmpty(this.treeListLookUpEdit1.SelectedValue()))
                {
                    DXMessage.ShowTips("请选择理由分类！");
                    this.treeListLookUpEdit1.Focus();
                    return false;
                }

                if (string.IsNullOrEmpty(this.memoReason.Text.Trim()))
                {
                    DXMessage.ShowTips("请输入理由内容！");
                    this.memoReason.Focus();
                    return false;
                }

                var price = decimal.Parse(this.txtPrice.Text.Trim());
                var priceBound = decimal.Parse(this.spinPriceBound.EditValue.ToString()) / (int)EnumLibrary.NumericUnit.Hundred;
                var volume = decimal.Parse(this.txtVolume.Text.Trim());
                var amount = Math.Abs(decimal.Parse(this.txtAmount.Text.Trim()) * (int)EnumLibrary.NumericUnit.TenThousand);
                var operateDate = CommonHelper.StringToDateTime(this.deOperate.EditValue.ToString());
                var stock = this.luStock.GetSelectedDataRow() as StockInfoModel;
                var stopProfitPrice = decimal.Parse(this.txtProfitPrice.Text.Trim());
                var stopProfitBound = decimal.Parse(this.spinProfitBound.EditValue.ToString()) / (int)EnumLibrary.NumericUnit.Hundred; ;
                var stopLossPrice = decimal.Parse(this.txtLossPrice.Text.Trim());
                var stopLossBound = decimal.Parse(this.spinLossBound.EditValue.ToString()) / (int)EnumLibrary.NumericUnit.Hundred; ;

                operation = new InvestmentDecisionOperation
                {
                    AccuracyPoint = 0,
                    AccuracyStatus = (int)EnumLibrary.IDOperationAccuracyStatus.None,
                    ApplyNo = ApplyNo,
                    CreateTime = now,
                    DealAmount = amount,
                    DealFlag = chkBuy.Checked,
                    DealPrice = price,
                    DealVolume = volume,
                    ExecuteFlag = (int)EnumLibrary.IDOperationExecuteStatus.None,
                    InitialFlag = _initialFlag,
                    IsDeleted = false,
                    IsStopped = false,
                    Step = OperateStep,
                    OperateDate = operateDate,
                    OperateUser = LoginInfo.CurrentUser.UserCode,
                    OperateNo = string.Empty,
                    PriceBound = priceBound,
                    ReasonCategoryId = int.Parse(this.treeListLookUpEdit1.SelectedValue()),
                    ReasonContent = memoReason.Text.Trim(),
                    StockCode = stock.FullCode,
                    StockName = stock.Name,
                    StopLossBound = stopLossBound,
                    StopLossPrice = stopLossPrice,
                    StopProfitBound = stopProfitBound,
                    StopProfitPrice = stopProfitPrice,
                    TradeRecordRelateFlag = false,
                    UpdateTime = now,
                    VotePoint = 0,
                    VoteStatus = (int)EnumLibrary.IDOperationVoteStatus.None,
                };
            }

            _IDService.IDApplicationApplyProcess(application, operation);

            return true;
        }

        private void CalculateAmount()
        {
            if (!string.IsNullOrEmpty(this.txtVolume.Text.Trim()) && !string.IsNullOrEmpty(this.txtPrice.Text.Trim()))
            {
                decimal dealAmount = CommonHelper.SetDecimalDigits(decimal.Parse(this.txtVolume.Text.Trim()) * decimal.Parse(this.txtPrice.Text.Trim()) / (int)EnumLibrary.NumericUnit.TenThousand, 6);

                this.txtAmount.Text = dealAmount.ToString();
            }
        }

        private void CalculatePriceBound(DevExpress.XtraEditors.SpinEdit spinPriceBound, DevExpress.XtraEditors.TextEdit txtPrice, DevExpress.XtraEditors.LabelControl lblPriceBound)
        {
            if (!string.IsNullOrEmpty(spinPriceBound.EditValue.ToString()) && !string.IsNullOrEmpty(txtPrice.Text.Trim()))
            {
                decimal dealUpBound = CommonHelper.SetDecimalDigits((1 + decimal.Parse(spinPriceBound.EditValue.ToString()) / (int)EnumLibrary.NumericUnit.Hundred) * decimal.Parse(txtPrice.Text.Trim()), 2);
                decimal dealDownBound = CommonHelper.SetDecimalDigits((1 - decimal.Parse(spinPriceBound.EditValue.ToString()) / (int)EnumLibrary.NumericUnit.Hundred) * decimal.Parse(txtPrice.Text.Trim()), 2);
                lblPriceBound.Text = dealDownBound.ToString() + " ~ " + dealUpBound.ToString();
            }
        }

        private void DisplayTargetEmbedForm()
        {
            switch (CurrentPageMode)
            {
                case PageMode.NewApplication:
                case PageMode.NewOperation:
                    _embedASA = EngineContext.Current.Resolve<_embedAppointedStockApplication>();
                    _embedASA.FormBorderStyle = FormBorderStyle.None;
                    _embedASA.TopLevel = false;
                    _embedASA.Parent = this.splitContainerControl1.Panel2;
                    _embedASA.Dock = DockStyle.Fill;
                    this.splitContainerControl1.Panel2.Controls.Add(_embedASA);

                    _embedASA.Show();
                    break;

                case PageMode.ViewDetail:
                    _embedIDOD = EngineContext.Current.Resolve<_embedIDOperationDetail>();
                    _embedIDOD.OperateNo = OperateNo;
                    _embedIDOD.FormBorderStyle = FormBorderStyle.None;
                    _embedIDOD.TopLevel = false;
                    _embedIDOD.Parent = this.splitContainerControl1.Panel2;
                    _embedIDOD.Dock = DockStyle.Fill;
                    this.splitContainerControl1.Panel2.Controls.Add(_embedIDOD);

                    _embedIDOD.Show();
                    break;

                case PageMode.OperationVote:
                    _embedIDOV = EngineContext.Current.Resolve<_embedIDOperationVote>();
                    _embedIDOV.ApplyNo = ApplyNo;
                    _embedIDOV.OperateNo = OperateNo;
                    _embedIDOV.FormBorderStyle = FormBorderStyle.None;
                    _embedIDOV.TopLevel = false;
                    _embedIDOV.Parent = this.splitContainerControl1.Panel2;
                    _embedIDOV.Dock = DockStyle.Fill;
                    this.splitContainerControl1.Panel2.Controls.Add(_embedIDOV);

                    _embedIDOV.Show();
                    break;

                case PageMode.AccuracyDetermination:
                    _embedIDOA = EngineContext.Current.Resolve<_embedIDOperationAccuracy>();
                    _embedIDOA.ApplyNo = ApplyNo;
                    _embedIDOA.OperateNo = OperateNo;
                    _embedIDOA.FormBorderStyle = FormBorderStyle.None;
                    _embedIDOA.TopLevel = false;
                    _embedIDOA.Parent = this.splitContainerControl1.Panel2;
                    _embedIDOA.Dock = DockStyle.Fill;
                    this.splitContainerControl1.Panel2.Controls.Add(_embedIDOA);

                    _embedIDOA.Show();
                    break;

                case PageMode.ExecutionConfirm:
                    _embedIDOE = EngineContext.Current.Resolve<_embedIDOperationExecute>();
                    _embedIDOE.ApplyNo = ApplyNo;
                    _embedIDOE.OperateNo = OperateNo;
                    _embedIDOE.FormBorderStyle = FormBorderStyle.None;
                    _embedIDOE.TopLevel = false;
                    _embedIDOE.Parent = this.splitContainerControl1.Panel2;
                    _embedIDOE.Dock = DockStyle.Fill;
                    this.splitContainerControl1.Panel2.Controls.Add(_embedIDOE);

                    _embedIDOE.Show();
                    break;

                default:
                    break;
            }
        }

        #endregion Utilities

        #region Events

        private void _dialogTradeApplication_Load(object sender, EventArgs e)
        {
            try
            {
                DisplayTargetEmbedForm();
                FormInit();
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        private void luStock_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                var stock = this.luStock.GetSelectedDataRow() as StockInfoModel;

                if (_embedASA != null && stock != null)
                {
                    this._embedASA.BindStockApplication(stock.FullCode, stock.Name);
                }
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        private void chkBuy_CheckedChanged(object sender, EventArgs e)
        {
            this.chkSell.Checked = !this.chkBuy.Checked;


            SetLimitBound();
            
        }  

        private void chkSell_CheckedChanged(object sender, EventArgs e)
        {
            this.chkBuy.Checked = !this.chkSell.Checked;

            SetLimitBound();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnSubmit.Enabled = false;

                _submitSucceedFlag = SubmitProcess();

                if (_submitSucceedFlag)
                    this.Close();
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
            finally
            {
                this.btnSubmit.Enabled = true;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtPrice_EditValueChanged(object sender, EventArgs e)
        {
            CalculatePriceBound(this.spinPriceBound, this.txtPrice, this.lblPriceBound);
            CalculateAmount();
        }

        private void txtVolume_EditValueChanged(object sender, EventArgs e)
        {
            CalculateAmount();
        }

        private void spinPriceBound_EditValueChanged(object sender, EventArgs e)
        {
            CalculatePriceBound(this.spinPriceBound, this.txtPrice, this.lblPriceBound);
        }

        private void txtProfitPrice_EditValueChanged(object sender, EventArgs e)
        {
            CalculatePriceBound(this.spinProfitBound, this.txtProfitPrice, this.lblProfitBound);
        }

        private void spinProfitBound_EditValueChanged(object sender, EventArgs e)
        {
            CalculatePriceBound(this.spinProfitBound, this.txtProfitPrice, this.lblProfitBound);
        }

        private void txtLossPrice_EditValueChanged(object sender, EventArgs e)
        {
            CalculatePriceBound(this.spinLossBound, this.txtLossPrice, this.lblLossBound);
        }

        private void spinLossBound_EditValueChanged(object sender, EventArgs e)
        {
            CalculatePriceBound(this.spinLossBound, this.txtLossPrice, this.lblLossBound);
        }

        private void _dialogIDApplication_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_submitSucceedFlag
                || (this._embedIDOV != null && this._embedIDOV.VoteSucceedFlag)
                || (this._embedIDOA != null && this._embedIDOA.VoteSucceedFlag)
                || (this._embedIDOE != null && this._embedIDOE.ProcessSucceedFlag))
            {
                RefreshEvent?.Invoke();
            }
        }

        #endregion Events
    }
}