using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using CTM.Core;
using CTM.Core.Domain.InvestmentDecision;
using CTM.Core.Domain.User;
using CTM.Core.Util;
using CTM.Services.Common;
using CTM.Services.InvestmentDecision;
using CTM.Services.Stock;
using CTM.Services.User;
using CTM.Win.Extensions;
using CTM.Win.Models;
using CTM.Win.Util;

namespace CTM.Win.Forms.InvestmentDecision
{
    public partial class _dialogTradeApplication : BaseForm
    {
        #region Fields

        private readonly ICommonService _commonService;
        private readonly IInvestmentDecisionService _IDService;
        private readonly IStockService _stockService;
        private readonly IUserService _userService;

        #endregion Fields

        #region Delegates

        public delegate void RefreshParentForm();

        public event RefreshParentForm RefreshEvent;

        #endregion Delegates

        #region Constructors

        public _dialogTradeApplication(
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
            //发起人
            var investors = _userService.GetAllOperators(true).OrderBy(x => x.Code).ToList();
            this.luInvestor.Initialize(investors, "Code", "Name", enableSearch: true);
            this.luInvestor.EditValue = LoginInfo.CurrentUser.UserCode;

            if (investors != null && investors.Any())
            {
                if (investors.Exists(x => x.Code == LoginInfo.CurrentUser.UserCode))
                    this.luInvestor.EditValue = LoginInfo.CurrentUser.UserCode;
                else
                    this.luInvestor.EditValue = string.Empty;
            }

            var now = _commonService.GetCurrentServerTime();

            //申请编号
            this.txtSerialNo.Text = string.Empty;

            //申请日期
            this.deApply.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.deApply.EditValue = now.Date;

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
            };

            this.cbOperateType.Initialize(operateTypes);
            this.cbOperateType.DefaultSelected("1");

            this.txtPrice.SetNumericMask(2);
            this.txtPrice.Text = string.Empty;
            this.txtPriceBound.MaxLength = 2;
            this.txtPriceBound.Text = "1";
            this.txtVolume.SetNumberMask();
            this.txtVolume.Text = string.Empty;
        }

        private bool SubmitProcess()
        {
            if (string.IsNullOrEmpty(this.luInvestor.SelectedValue()))
            {
                DXMessage.ShowTips("请选择投资发起人！");
                this.luInvestor.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(this.luStock.SelectedValue()))
            {
                DXMessage.ShowTips("请选择股票信息！");
                this.luStock.Focus();
                return false;
            }

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

            if (this.txtPriceBound.Text.Trim().Length == 0)
            {
                DXMessage.ShowTips("请输入单价上下限！");
                this.txtPriceBound.Focus();
                return false;
            }

            int priceBound;
            if (!int.TryParse(txtPriceBound.Text.Trim(), out priceBound))
            {
                DXMessage.ShowTips("单价上下限应该输入数字！");
                this.txtPriceBound.Focus();
                return false;
            }

            if (int.Parse(this.txtPriceBound.Text.Trim()) <= 0)
            {
                DXMessage.ShowTips("单价上下限应该大于0！");
                this.txtPriceBound.Focus();
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

            if (string.IsNullOrEmpty(this.memoReason.Text.Trim()))
            {
                DXMessage.ShowTips("请输入申请理由！");
                this.memoReason.Focus();
                return false;
            }

            var investorInfo = this.luInvestor.GetSelectedDataRow() as UserInfo;
            var applyDate = CommonHelper.StringToDateTime(this.deApply.EditValue.ToString());
            var tradeType = int.Parse(this.cbOperateType.SelectedValue());
            var price = decimal.Parse(this.txtPrice.Text.Trim());
            var volume = decimal.Parse(this.txtVolume.Text.Trim());
            var amount = Math.Abs(decimal.Parse(this.txtAmount.Text.Trim()) * (int)EnumLibrary.NumericUnit.TenThousand);

            var stock = this.luStock.GetSelectedDataRow() as StockInfoModel;
            var now = _commonService.GetCurrentServerTime();

            var form = new InvestmentDecisionForm
            {
                Amount = this.chkBuy.Checked ? amount : -amount,
                ApplyDate = applyDate,
                ApplyUser = investorInfo.Code,
                CreateTime = now,
                DealFlag = this.chkBuy.Checked ? true : false,
                DepartmentId = investorInfo.DepartmentId,
                Price = price,
                PriceBound = (decimal)priceBound / (int)EnumLibrary.NumericUnit.Hundred,
                Reason = this.memoReason.Text.Trim(),
                RelateTradePlanNo = txtPlanNo.Text.Trim(),
                SerialNo = this.txtSerialNo.Text.Trim(),
                Status = (int)EnumLibrary.IDFormStatus.Submited,
                StockFullCode = stock.FullCode,
                StockName = stock.Name,
                TradeType = tradeType,
                UpdateTime = now,
                Volume = volume,
            };

            _IDService.SubmitInvestmentDecisionApplication(form);

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

        private void CalculatePriceBound()
        {
            if (!string.IsNullOrEmpty(this.txtPriceBound.Text.Trim()) && !string.IsNullOrEmpty(this.txtPrice.Text.Trim()))
            {
                decimal dealUpBound = CommonHelper.SetDecimalDigits((1 + decimal.Parse(this.txtPriceBound.Text.Trim()) / (int)EnumLibrary.NumericUnit.Hundred) * decimal.Parse(this.txtPrice.Text.Trim()), 2);
                decimal dealDownBound = CommonHelper.SetDecimalDigits((1 - decimal.Parse(this.txtPriceBound.Text.Trim()) / (int)EnumLibrary.NumericUnit.Hundred) * decimal.Parse(this.txtPrice.Text.Trim()), 2);
                this.lblPriceBound.Text = dealDownBound.ToString() + " ~ " + dealUpBound.ToString();
            }
        }

        #endregion Utilities

        #region Events

        private void _dialogTradeApplication_Load(object sender, EventArgs e)
        {
            try
            {
                FormInit();
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        private void deApply_EditValueChanged(object sender, EventArgs e)
        {
            var applyDate = CommonHelper.StringToDateTime(this.deApply.EditValue.ToString());
            this.txtSerialNo.Text = _IDService.GenerateIDFSerialNo(applyDate);
        }

        private void chkBuy_CheckedChanged(object sender, EventArgs e) 
        {
            this.chkSell.Checked = !this.chkBuy.Checked;
        }

        private void chkSell_CheckedChanged(object sender, EventArgs e)
        {
            this.chkBuy.Checked = !this.chkSell.Checked;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnSubmit.Enabled = false;

                if (SubmitProcess())
                {
                    RefreshEvent?.Invoke();

                    this.Close();
                }
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
            CalculatePriceBound();
            CalculateAmount();
        }

        private void txtVolume_EditValueChanged(object sender, EventArgs e)
        {
            CalculateAmount();
        }

        private void txtPriceBound_TextChanged(object sender, EventArgs e)
        {
            CalculatePriceBound();
        }

        #endregion Events
    }
}