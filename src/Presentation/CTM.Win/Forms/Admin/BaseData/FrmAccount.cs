using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using CTM.Core;
using CTM.Core.Domain.Industry;
using CTM.Data;
using CTM.Services.Account;
using CTM.Services.Industry;
using CTM.Win.Extensions;
using CTM.Win.Util;

namespace CTM.Win.Forms.Admin.BaseData
{
    public partial class FrmAccount : BaseForm
    {
        #region Fields

        private readonly IIndustryService _industryService;
        private readonly IAccountService _accountService;

        private const string _layoutXmlName = "FrmAccount";
        private bool _firstFocused = true;
        private int _industryId;

        #endregion Fields

        #region Constructors

        public FrmAccount(
            IIndustryService industryService,
            IAccountService accountService
           )
        {
            InitializeComponent();

            this._industryService = industryService;
            this._accountService = accountService;
        }

        #endregion Constructors

        #region Utilities

        private void SetOperateButtonProperties()
        {
            this.btnEdit.Enabled = false;
            this.btnDisable.Enabled = false;
        }

        private void BindIndustryTree()
        {
            var industryInfos = _industryService.GetAllIndustry();

            var all = new IndustryInfo
            {
                Id = 0,
                ParentId = -1,
                Name = "全部",
                Level = 0,
            };

            industryInfos.Add(all);

            this.treeList1.Initialize(industryInfos, "Id", "ParentId", showColumns: false, autoWidth: true, showHorzLines: false, showVertLines: false, expandAll: true);
            this.treeList1.SetDefaultFocusedNode(0);
        }

        private void BindSubjectInfo(int industryId)
        {
            if (industryId < 1)
                this.lcgSubject.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            else
            {
                this.lcgSubject.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;

                this.lciEdit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                this.lciCancel.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                this.lciConfirm.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

                var connString = System.Configuration.ConfigurationManager.ConnectionStrings["CTMContext"].ToString();

                var sql = $@"SELECT * FROM [dbo].[v_InvestmentSubject] WHERE IndustryId = {industryId}";

                var ds = SqlHelper.ExecuteDataset(connString, System.Data.CommandType.Text, sql);

                var dr = ds?.Tables?[0].Rows?[0];

                if (dr != null)
                {
                    txtName.Text = dr["SubjectFullName"].ToString();
                    txtInvestFund.Text = dr["InvestFund"].ToString();
                    txtNetAsset.Text = dr["NetAsset"].ToString();
                    txtFinancingAmount.Text = dr["FinancingAmount"].ToString();
                    txtRemarks.Text = dr["Remarks"].ToString();
                }
            }
        }

        private void SetSubjectControlStatus(bool isEdit)
        {
            if (isEdit)
            {
                this.lciEdit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                this.lciConfirm.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                this.lciCancel.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }
            else
            {
                this.lciEdit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                this.lciConfirm.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                this.lciCancel.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }

            txtFinancingAmount.ReadOnly = !isEdit;
            txtInvestFund.ReadOnly = !isEdit;
            txtNetAsset.ReadOnly = !isEdit;
            txtRemarks.ReadOnly = !isEdit;
        }

        private bool SubjectEditProcess()
        {
            var investFund = decimal.Parse(this.txtInvestFund.Text.Trim());
            if (investFund < 0)
            {
                DXMessage.ShowTips("投入资金不能小于0！");
                this.txtInvestFund.Focus();
                return false;
            }

            var netAsset = decimal.Parse(this.txtNetAsset.Text.Trim());
            if (netAsset < 0)
            {
                DXMessage.ShowTips("净资产不能小于0！");
                this.txtNetAsset.Focus();
                return false;
            }

            var financingAmount = decimal.Parse(this.txtFinancingAmount.Text.Trim());
            if (financingAmount < 0)
            {
                DXMessage.ShowTips("融资额不能小于0！");
                this.txtFinancingAmount.Focus();
                return false;
            }
            var unit = (int)EnumLibrary.NumericUnit.TenThousand;
            var connString = System.Configuration.ConfigurationManager.ConnectionStrings["CTMContext"].ToString();

            var sql = $@" EXEC [dbo].[sp_InvestmentSubjectEditProcess] @IndustryId={_industryId}, @InvestFund={investFund*unit}, @NetAsset={netAsset*unit}, @FinancingAmount={financingAmount*unit}, @Remarks='{txtRemarks.Text.Trim()}' ";

            SqlHelper.ExecuteNonQuery(connString, System.Data.CommandType.Text, sql);

            return true;
        }

        private void BindAccountInfo(int industryId)
        {
            var accounts = _accountService.GetAccountDetails(industryId: industryId, showDisabled: true, tableNoTracking: true).OrderBy(x => x.Name);

            this.gridControl1.DataSource = accounts;
        }

        private void RefreshForm(int industryId)
        {
            SetOperateButtonProperties();

            BindAccountInfo(industryId);
        }

        private void DisplayEditDialog(int accountId)
        {
            var dialog = this.CreateDialog<_dialogAccountEdit>();
            dialog.RefreshEvent += new _dialogAccountEdit.RefreshParentForm(RefreshForm);
            dialog.AccountId = accountId;

            if (accountId > 0)
                dialog.Text = "编辑账户";
            else
                dialog.Text = "添加账户";

            dialog.ShowDialog();
        }

        #endregion Utilities

        #region Events

        private void FrmAccount_Load(object sender, EventArgs e)
        {
            try
            {
                SetOperateButtonProperties();

                this.gridView1.LoadLayout(_layoutXmlName);
                this.gridView1.SetLayout(showFilterPanel: true, showGroupPanel: true);

                this.txtInvestFund.SetNumericMask(2);
                this.txtNetAsset.SetNumericMask(2);
                this.txtFinancingAmount.SetNumericMask(2);

                BindIndustryTree();
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        private void btnEditSubject_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnEditSubject.Enabled = false;

                SetSubjectControlStatus(true);
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
            finally
            {
                this.btnEditSubject.Enabled = true;
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnConfirm.Enabled = false;

                SubjectEditProcess();

                SetSubjectControlStatus(false);

                BindSubjectInfo(_industryId);
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
            finally
            {
                this.btnConfirm.Enabled = true;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnCancel.Enabled = false;

                SetSubjectControlStatus(false);

                BindSubjectInfo(_industryId);
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
            finally
            {
                this.btnCancel.Enabled = true;
            }
        }

        /// <summary>
        /// 编辑账户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                var myView = this.gridView1;

                var selectedHandles = myView.GetSelectedRows();
                if (selectedHandles.Any())
                    selectedHandles = selectedHandles.Where(x => x > -1).ToArray();

                if (selectedHandles.Length != 1)
                {
                    DXMessage.ShowTips("请选择一个要编辑的账户！");
                    return;
                }

                //账户Id
                var accountId = int.Parse(myView.GetRowCellValue(selectedHandles[0], colAccountId).ToString());

                DisplayEditDialog(accountId);
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        /// <summary>
        /// 禁用账户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDisable_Click(object sender, EventArgs e)
        {
            try
            {
                var myView = this.gridView1;

                var selectedHandles = myView.GetSelectedRows();
                if (selectedHandles.Length == 0) return;

                selectedHandles = myView.GetSelectedRows().Where(x => x > -1).ToArray();

                if (DXMessage.ShowYesNoAndWarning("确定禁用选择的账户吗？") == DialogResult.Yes)
                {
                    var accountIds = new List<int>();

                    for (var rowhandle = 0; rowhandle < selectedHandles.Length; rowhandle++)
                    {
                        accountIds.Add(int.Parse(myView.GetRowCellValue(selectedHandles[rowhandle], colAccountId).ToString()));
                    }

                    this._accountService.DisableAccount(accountIds.ToArray());

                    RefreshForm(_industryId);
                }
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        /// <summary>
        /// 添加账户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            DisplayEditDialog(0);
        }

        private void treeList1_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            try
            {
                var selectedNode = this.treeList1.FocusedNode;

                if (selectedNode == null) return;

                if (!this._firstFocused)
                {
                    _industryId = int.Parse(selectedNode.GetValue(treeColId).ToString());

                    BindSubjectInfo(_industryId);
                    BindAccountInfo(_industryId);
                }
                this._firstFocused = false;
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.gridView1.SaveLayout(_layoutXmlName);
        }

        private void gridView1_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            try
            {
                var myView = this.gridView1;
                var selectedHandles = myView.GetSelectedRows();
                if (selectedHandles.Any())
                    selectedHandles = selectedHandles.Where(x => x > -1).ToArray();

                if (selectedHandles.Length == 0)
                {
                    this.btnEdit.Enabled = false;
                    this.btnDisable.Enabled = false;
                }
                else if (selectedHandles.Length > 0)
                {
                    btnDisable.Enabled = true;

                    if (selectedHandles.Length == 1)
                    {
                        this.btnEdit.Enabled = true;
                    }
                    else
                    {
                        this.btnEdit.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        /// <summary>
        /// 显示数据行号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        #endregion Events
    }
}