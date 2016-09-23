using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using CTM.Core.Domain.Industry;
using CTM.Services.Account;
using CTM.Services.Industry;
using CTM.Win.Extensions;
using CTM.Win.Util;

namespace CTM.Win.UI.Admin.BaseData
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

            this.treeList1.Initialize(industryInfos, "Id", "ParentId", expandAll: true);
            this.treeList1.SetDefaultFocusedNode(0);
        }

        private void BindAccountInfo(int industryId)
        {
            var sss = DateTime.Now;
            var accounts = _accountService.GetAccountDetails(industryId: industryId, showDisabled: true, tableNoTracking: true).OrderBy(x => x.Name);
            var eee = DateTime.Now;
            System.Diagnostics.Debug.WriteLine("【" + this.Name + "】帐户信息查询用时：" + (eee - sss));

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

                BindIndustryTree();

                this.ActiveControl = this.btnAdd;
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
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

                if (selectedHandles.Length == 0)
                {
                    DXMessage.ShowTips("请选择要禁用的账户！");
                    return;
                }

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