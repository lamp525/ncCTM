using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using CTM.Core;
using CTM.Core.Domain.Account;
using CTM.Core.Domain.User;
using CTM.Services.Account;
using CTM.Services.Dictionary;
using CTM.Services.Industry;
using CTM.Services.User;
using CTM.Win.Extensions;
using CTM.Win.Models;
using CTM.Win.Util;
using DevExpress.XtraEditors.Controls;

namespace CTM.Win.Forms.Admin.BaseData
{
    public partial class _dialogAccountEdit : BaseForm
    {
        #region Constants

        private const string _operatorCheckedListBoxItemDisplayText = "{0}  [{1}]";

        #endregion Constants

        #region Fields

        private readonly IAccountService _accountService;
        private readonly IUserService _userService;
        private readonly IIndustryService _industryService;
        private readonly IDictionaryService _dictionaryService;

        private int _accountId;
        private bool _saveSucceed = false;
        private bool _isEdit;

        #endregion Fields

        #region Properties

        public int AccountId
        {
            get { return this._accountId; }
            set { this._accountId = value; }
        }

        #endregion Properties

        #region Delegates

        public delegate void RefreshParentForm(int industryId);

        public event RefreshParentForm RefreshEvent;

        #endregion Delegates

        #region Constructors

        public _dialogAccountEdit
            (
            IAccountService accountService,
            IUserService userService,
            IDictionaryService dictionaryService,
            IIndustryService industryService
            )
        {
            InitializeComponent();

            this._accountService = accountService;
            this._userService = userService;
            this._dictionaryService = dictionaryService;
            this._industryService = industryService;
        }

        #endregion Constructors

        #region Utilities

        /// <summary>
        /// 设置控件默认属性
        /// </summary>
        private void SetControlProperties()
        {
            this.txtAccountName.Properties.MaxLength = 20;
            this.memoRemarks.Properties.MaxLength = 200;
            this.txtFinacingAmount.SetNumericMask();
            this.txtInvestFund.SetNumericMask();
            this.txtStampDutyRate.SetPercentageMask();
            this.txtCommissionRate.SetPercentageMask();
            this.txtIncidentalsRate.SetPercentageMask();
            this.chkYes.Checked = true;

            //投入资金
            txtInvestFund.Text = "0";

            //融资额
            txtFinacingAmount.Text = "0";

            //印花税率
            txtStampDutyRate.Text = "0.001";

            //佣金率
            txtCommissionRate.Text = "0.0002";

            //其他费率
            txtIncidentalsRate.Text = "0";
        }

        private void BindAccountOperator()
        {
            var dealers = _userService.GetAllDealer();
            this.luOperator.Initialize(dealers, "Id", "Name", true);

            var operators = _accountService.GetAccountOperatorsByAccountId(_accountId).ToList();

            if (operators != null)
            {
                var items = new CheckedListBoxItem[operators.Count];
                for (var i = 0; i < operators.Count; i++)
                {
                    items[i] = new CheckedListBoxItem(value: operators[i].Id, description: string.Format(_operatorCheckedListBoxItemDisplayText, operators[i].Name, operators[i].Code));
                }
                this.clbOperator.Items.AddRange(items);
                this.clbOperator.CheckAll();
            }
            //   this.clbOperator.Initialize(operators, "Id", "Name");
        }

        /// <summary>
        /// 账户信息绑定
        /// </summary>
        private void BindAccountInfo()
        {
            //所属产业
            var industrys = _industryService.GetAllIndustry()
                               .Select(x => new ComboBoxItemModel
                               {
                                   Value = x.Id.ToString(),
                                   Text = x.Name
                               }).ToList();

            this.cbIndustry.Initialize(industrys);

            //账户属性
            var attributes = _dictionaryService.GetDictionaryInfoByTypeId((int)EnumLibrary.DictionaryType.AccountAttribute)
                           .Select(x => new ComboBoxItemModel
                           {
                               Value = x.Code.ToString(),
                               Text = x.Name
                           }).ToList();

            this.cbAttribute.Initialize(attributes);

            //账户规划
            var plans = _dictionaryService.GetDictionaryInfoByTypeId((int)EnumLibrary.DictionaryType.AccountPlan)
                                 .Select(x => new ComboBoxItemModel
                                 {
                                     Value = x.Code.ToString(),
                                     Text = x.Name
                                 }).ToList();

            this.cbPlan.Initialize(plans);

            //账户分类
            var types = _dictionaryService.GetDictionaryInfoByTypeId((int)EnumLibrary.DictionaryType.AccountType)
                                .Select(x => new ComboBoxItemModel
                                {
                                    Value = x.Code.ToString(),
                                    Text = x.Name
                                }).ToList();

            this.cbType.Initialize(types);

            //开户券商
            var securityCompanys = _dictionaryService.GetDictionaryInfoByTypeId((int)EnumLibrary.DictionaryType.SecurityCompay)
                                 .Select(x => new ComboBoxItemModel
                                 {
                                     Value = x.Code.ToString(),
                                     Text = x.Name
                                 }).ToList();

            this.cbSecurity.Initialize(securityCompanys);

            if (this._isEdit)
            {
                var accountInfo = _accountService.GetAccountDetailById(_accountId);

                if (accountInfo == null) return;

                this.cbIndustry.DefaultSelected(accountInfo.IndustryId.ToString());
                this.cbSecurity.DefaultSelected(accountInfo.SecurityCompanyCode.ToString());
                this.cbType.DefaultSelected(accountInfo.TypeCode.ToString());
                this.cbAttribute.DefaultSelected(accountInfo.AttributeCode.ToString());
                this.cbPlan.DefaultSelected(accountInfo.PlanCode.ToString());

                //账户名
                txtAccountName.Text = accountInfo.Name;

                //投入资金
                txtInvestFund.Text = accountInfo.InvestFund.ToString();

                //融资额
                txtFinacingAmount.Text = accountInfo.InvestFund.ToString();

                //印花税率
                txtStampDutyRate.Text = accountInfo.StampDutyRate.ToString();

                //佣金率
                txtCommissionRate.Text = accountInfo.CommissionRate.ToString();

                //其他费率
                txtIncidentalsRate.Text = accountInfo.IncidentalsRate.ToString();

                //核算
                if (accountInfo.NeedAccounting)
                    this.chkYes.Checked = true;
                else
                    this.chkNo.Checked = true;

                //备注说明
                this.memoRemarks.Text = accountInfo.Remarks;
            }
        }

        /// <summary>
        /// 画面输入检查
        /// </summary>
        /// <returns></returns>
        private bool InputCheck()
        {
            if (string.IsNullOrEmpty(this.txtAccountName.Text.Trim()))
            {
                DXMessage.ShowTips("账户名称不能为空！");
                this.txtAccountName.Focus();
                return false;
            }

            if (this.cbIndustry.SelectedIndex == -1)
            {
                DXMessage.ShowTips("请选择所属产业！");
                return false;
            }

            if (this.cbSecurity.SelectedIndex == -1)
            {
                DXMessage.ShowTips("请选择开户券商！");
                return false;
            }

            if (this.cbAttribute.SelectedIndex == -1)
            {
                DXMessage.ShowTips("请选择账户属性！");
                return false;
            }

            if (this.cbType.SelectedIndex == -1)
            {
                DXMessage.ShowTips("请选择账户分类！");
                return false;
            }

            if (this.cbType.SelectedIndex == -1)
            {
                DXMessage.ShowTips("请选择账户规划！");
                return false;
            }

            return true;
        }

        /// <summary>
        /// 保存账户信息
        /// </summary>
        private bool SaveAccountInfo()
        {
            try
            {
                //添加账户信息
                if (!this._isEdit)
                {
                    var newAccount = new AccountInfo()
                    {
                        AttributeCode = int.Parse(this.cbAttribute.SelectedValue()),
                        AttributeName = this.cbAttribute.Text.Trim(),
                        CommissionRate = decimal.Parse(this.txtCommissionRate.EditValue.ToString()),
                        FinancingAmount = decimal.Parse(this.txtFinacingAmount.Text.Trim()),
                        IncidentalsRate = decimal.Parse(this.txtIncidentalsRate.EditValue.ToString()),
                        IndustryId = int.Parse(this.cbIndustry.SelectedValue()),
                        InvestFund = decimal.Parse(this.txtInvestFund.Text.Trim()),
                        IsDisabled = false,
                        Name = this.txtAccountName.Text.Trim(),
                        NeedAccounting = this.chkYes.Checked ? true : false,
                        PlanCode = int.Parse(this.cbPlan.SelectedValue()),
                        PlanName = this.cbPlan.Text.Trim(),
                        Remarks = this.memoRemarks.Text.Trim(),
                        StampDutyRate = decimal.Parse(this.txtStampDutyRate.EditValue.ToString()),
                        SecurityCompanyCode = int.Parse(this.cbSecurity.SelectedValue()),
                        SecurityCompanyName = this.cbSecurity.Text.Trim(),
                        TypeCode = int.Parse(this.cbType.SelectedValue()),
                        TypeName = this.cbType.Text.Trim(),
                    };

                    var isExisted = _accountService.IsExistedAccount(newAccount.Name, newAccount.SecurityCompanyCode, newAccount.AttributeCode);

                    if (!isExisted)
                    {
                        _accountService.AddAccountInfo(newAccount);

                        this._accountId = newAccount.Id;
                    }
                    else
                    {
                        DXMessage.ShowTips("系统已经存在账户名称、开发券商和账户属性相同的账户信息，无法添加！");
                        return false;
                    }
                }
                //修改账户信息
                else
                {
                    var account = _accountService.GetAccountInfoById(this._accountId);

                    account.IndustryId = int.Parse(this.cbIndustry.SelectedValue());
                    account.SecurityCompanyCode = int.Parse(this.cbSecurity.SelectedValue());
                    account.SecurityCompanyName = this.cbSecurity.Text.Trim();
                    account.AttributeCode = int.Parse(this.cbAttribute.SelectedValue());
                    account.AttributeName = this.cbAttribute.Text.Trim();
                    account.TypeCode = int.Parse(this.cbType.SelectedValue());
                    account.TypeName = this.cbType.Text.Trim();
                    account.PlanCode = int.Parse(this.cbType.SelectedValue());
                    account.PlanName = this.cbPlan.Text.Trim();
                    account.Name = this.txtAccountName.Text.Trim();
                    account.InvestFund = decimal.Parse(this.txtInvestFund.Text.Trim());
                    account.FinancingAmount = decimal.Parse(this.txtFinacingAmount.Text.Trim());
                    account.StampDutyRate = decimal.Parse(this.txtStampDutyRate.EditValue.ToString());
                    account.CommissionRate = decimal.Parse(this.txtCommissionRate.EditValue.ToString());
                    account.IncidentalsRate = decimal.Parse(this.txtIncidentalsRate.EditValue.ToString());
                    account.NeedAccounting = this.chkYes.Checked ? true : false;
                    account.Remarks = this.memoRemarks.Text.Trim();

                    var isExisted = _accountService.IsExistedAccount(account.Name, account.SecurityCompanyCode, account.AttributeCode, account.Id);

                    if (!isExisted)
                    {
                        _accountService.UpdateAccountInfo(account);
                    }
                    else
                    {
                        DXMessage.ShowTips("系统已经存在账户名称、开发券商和账户属性相同的账户信息，无法修改！");
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
                return false;
            }

            return true;
        }

        /// <summary>
        /// 保存账户操作人
        /// </summary>
        private bool SaveAccountOperators()
        {
            try
            {
                if (this.clbOperator.Items.Count == 0) return true;

                //添加账户的场合
                if (!this._isEdit)
                {
                    var checkedOperators = new List<AccountOperator>();

                    foreach (CheckedListBoxItem item in this.clbOperator.CheckedItems)
                    {
                        checkedOperators.Add(new AccountOperator { AccountId = this._accountId, OperatorId = int.Parse(item.Value.ToString()) });
                    }

                    _accountService.AddAccountOperator(checkedOperators);
                }
                //修改账户的场合
                else
                {
                    foreach (CheckedListBoxItem item in this.clbOperator.Items)
                    {
                        var info = _accountService.GetAccountOperatorByAccountIdAndOperatorId(this._accountId, int.Parse(item.Value.ToString()));

                        // 添加操作人员
                        if (info == null && item.CheckState == CheckState.Checked)
                        {
                            _accountService.AddAccountOperator(new AccountOperator { AccountId = this._accountId, OperatorId = int.Parse(item.Value.ToString()) });
                        }
                        // 删除操作人员
                        if (info != null && item.CheckState == CheckState.Unchecked)
                        {
                            _accountService.DeleteAccountOperator(info);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
                return false;
            }
            return true;
        }

        #endregion Utilities

        #region Events

        private void _dialogAccountOperatorEdit_Load(object sender, EventArgs e)
        {
            this._isEdit = this._accountId > 0 ? true : false;

            SetControlProperties();
            BindAccountInfo();
            BindAccountOperator();
        }

        /// <summary>
        /// 返回按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReturn_Click(object sender, EventArgs e)
        {
            if (this._saveSucceed)
            {
                var industryId = int.Parse(this.cbIndustry.SelectedValue());

                RefreshEvent?.Invoke(industryId);
            }

            this.Close();
        }

        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!InputCheck()) return;

            if (!SaveAccountInfo()) return;

            if (!SaveAccountOperators()) return;

            this._saveSucceed = true;
            this._isEdit = true;

            DXMessage.ShowTips("保存操作成功！");
        }

        /// <summary>
        /// 添加按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.luOperator.EditValue == null || this.luOperator.EditValue.ToString() == "nulltext") return;

            var userId = int.Parse(this.luOperator.EditValue.ToString().Trim());

            var selectedOperator = luOperator.Properties.GetDataSourceRowByKeyValue(userId) as UserInfo;

            CheckedListBoxItem item = new CheckedListBoxItem(
                value: selectedOperator.Id,
                description: string.Format(_operatorCheckedListBoxItemDisplayText, selectedOperator.Name, selectedOperator.Code),
                checkState: CheckState.Checked,
                enabled: true
                );

            if (this.clbOperator.Items.Where(x => item.Value.ToString() == x.Value.ToString()).Count() == 0)
            {
                this.clbOperator.Items.Add(item);
                this.clbOperator.ResetBindings();

                this.luOperator.EditValue = null;
            }
        }

        private void chkYes_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkYes.Checked)
                this.chkNo.Checked = false;
            else
                this.chkNo.Checked = true;
        }

        private void chkNo_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkNo.Checked)
                this.chkYes.Checked = false;
            else
                this.chkYes.Checked = true;
        }

        #endregion Events
    }
}