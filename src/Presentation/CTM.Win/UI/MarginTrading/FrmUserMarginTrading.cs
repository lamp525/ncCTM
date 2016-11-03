using System;
using System.Collections.Generic;
using System.Linq;
using CTM.Core;
using CTM.Core.Domain.User;
using CTM.Core.Util;
using CTM.Services.Account;
using CTM.Services.Department;
using CTM.Services.Dictionary;
using CTM.Services.MarginTrading;
using CTM.Services.Stock;
using CTM.Services.User;
using CTM.Win.Extensions;
using CTM.Win.Models;
using CTM.Win.Util;
using DevExpress.Utils;
using DevExpress.XtraLayout.Utils;

namespace CTM.Win.UI.MarginTrading
{
    public partial class FrmUserMarginTrading : BaseForm
    {
        #region Fields

        private readonly IStockService _stockService;
        private readonly IAccountService _accountService;
        private readonly IMarginTradingService _marginService;
        private readonly IUserService _userService;
        private readonly IDictionaryService _dictionaryService;
        private readonly IDepartmentService _departmentService;

        private IList<UserInfo> _investors = null;

        private const string _layoutXmlName = "FrmMarginTrading";

        #endregion Fields

        #region Constructors

        public FrmUserMarginTrading(
            IStockService stockService,
            IAccountService accountService,
            IMarginTradingService marginService,
            IUserService userService,
            IDictionaryService dictionaryService,
            IDepartmentService departmentService
            )
        {
            InitializeComponent();

            this._stockService = stockService;
            this._accountService = accountService;
            this._marginService = marginService;
            this._userService = userService;
            this._dictionaryService = dictionaryService;
            this._departmentService = departmentService;
        }

        #endregion Constructors

        #region Utilities

        private void BindSearchInfo()
        {
            var endDate = DateTime.Now.Date;
            var startDate = endDate.AddMonths(-1);

            //开始时间
            this.deStart.Properties.AllowNullInput = DefaultBoolean.False;
            this.deStart.EditValue = startDate;

            //结束时间
            this.deEnd.Properties.AllowNullInput = DefaultBoolean.False;
            this.deEnd.EditValue = endDate;

            //部门
            var deptInfos = this._departmentService.GetAllAccountingDepartmentInfo()
                .Select(x => new ComboBoxItemModel
                {
                    Text = x.Name,
                    Value = x.Id.ToString(),
                }).ToList();
            this.cbDepartment.Initialize(deptInfos, displayAdditionalItem: false);
        }

        private void AccessControl()
        {
            if (LoginInfo.CurrentUser.IsAdmin)
            {
                this.luInvestor.EditValue = string.Empty;
                this.cbDepartment.SelectedIndex = 0;
                this.lciRepayMargin.Visibility = LayoutVisibility.Always;
            }
            else
            {
                this.cbDepartment.ReadOnly = true;
                this.cbDepartment.DefaultSelected(LoginInfo.CurrentUser.DepartmentId.ToString());
                this.luInvestor.ReadOnly = true;

                var investors = luInvestor.Properties.DataSource as List<UserInfo>;
                if (investors != null && investors.Any())
                {
                    if (investors.Exists(x => x.Code == LoginInfo.CurrentUser.UserCode))
                        this.luInvestor.EditValue = LoginInfo.CurrentUser.UserCode;
                    else
                        this.luInvestor.EditValue = string.Empty;
                }

                //查询条件
                this.lciOnWorking.Visibility = LayoutVisibility.Never;
                this.lciQuit.Visibility = LayoutVisibility.Never;

                if (LoginInfo.CurrentUser.DepartmentId == (int)EnumLibrary.AccountingDepartment.Day)
                    this.lciRepayMargin.Visibility = LayoutVisibility.Never;
            }

            //this.btnRepayMargin.Enabled = false;
            this.btnDelete.Enabled = false;
        }

        private void BindInvestor(bool isOnWorking)
        {
            if (this._investors == null || !this._investors.Any()) return;

            var source = this._investors.Where(x => x.IsDeleted == !isOnWorking).OrderBy(x => x.Code).ToList();

            this.luInvestor.Initialize(source, "Code", "Name", showHeader: true, enableSearch: true);
        }

        private void DisplaySearchResult()
        {
            var startDate = CommonHelper.StringToDateTime(this.deStart.EditValue.ToString());
            var endDate = CommonHelper.StringToDateTime(this.deEnd.EditValue.ToString());

            var deptId = int.Parse(this.cbDepartment.SelectedValue());
            var investorCode = this.luInvestor.SelectedValue();

            var investorCodes = new List<string>();
            if (string.IsNullOrEmpty(investorCode))
                investorCodes = (this.luInvestor.Properties.DataSource as List<UserInfo>).Where(x => !string.IsNullOrEmpty(x.Code)).Select(x => x.Code).ToList();
            else
                investorCodes.Add(investorCode);

            var marginInfos = this._marginService.GetUserAllMarginTradingDetails(dateFrom: startDate, dateTo: endDate, investorCodes: investorCodes.ToArray())
                .OrderByDescending(x => x.MarginDate)
                .ThenBy(x => x.InvestorName)
                .ThenBy(x => x.AccountInfo)
                .ThenByDescending(x => x.Id)
                .ToList();

            this.gridControl1.DataSource = marginInfos;
        }

        private void DisplayMarginDialog(bool isRepay)
        {
            var dialog = this.CreateDialog<_dialogAddMarginTrading>();
            dialog.RefreshEvent += new _dialogAddMarginTrading.RefreshParentForm(RefreshForm);
            dialog.Text = isRepay ? "添加还资还券信息" : "添加融资融券信息";
            dialog.IsRepay = isRepay;
            dialog.ShowDialog();
        }

        private void RefreshForm()
        {
            DisplaySearchResult();
        }

        #endregion Utilities

        #region Events

        private void FrmUserMarginTrading_Load(object sender, EventArgs e)
        {
            try
            {
                this.gridView1.LoadLayout(_layoutXmlName);
                this.gridView1.SetLayout(showAutoFilterRow: true, showCheckBoxRowSelect: true, showGroupPanel: true);

                BindSearchInfo();

                AccessControl();

                if (!LoginInfo.CurrentUser.IsAdmin)
                    DisplaySearchResult();

                this.ActiveControl = this.btnSearch;
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        private void cbDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            //投资人员
            var selectedDeptId = int.Parse(this.cbDepartment.SelectedValue());
            this._investors = this._userService.GetUserDetails(departmentId: selectedDeptId);
            var working = new UserInfo
            {
                Code = string.Empty,
                Name = "在职人员",
                IsDeleted = false,
            };
            var quit = new UserInfo
            {
                Code = string.Empty,
                Name = "离职人员",
                IsDeleted = true,
            };
            this._investors.Add(working);
            this._investors.Add(quit);

            var isOnWorking = this.chkQuit.Checked ? false : true;

            var souce = this._investors.Where(x => x.IsDeleted == !isOnWorking).OrderBy(x => x.Code).ToList();
            this.luInvestor.Initialize(souce, "Code", "Name", showHeader: true, enableSearch: true);
        }

        private void chkOnWorking_CheckedChanged(object sender, EventArgs e)
        {
            this.chkQuit.Checked = !this.chkOnWorking.Checked;

            if (this.chkOnWorking.Checked)
                BindInvestor(true);
        }

        private void chkQuit_CheckedChanged(object sender, EventArgs e)
        {
            this.chkOnWorking.Checked = !this.chkQuit.Checked;
            if (this.chkQuit.Checked)
                BindInvestor(false);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnSearch.Enabled = false;

                DisplaySearchResult();
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
            finally
            {
                this.btnSearch.Enabled = true;
            }
        }

        /// <summary>
        /// 添加融资融券信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddMargin_Click(object sender, EventArgs e)
        {
            DisplayMarginDialog(false);
        }

        /// <summary>
        /// 添加还资还券信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRepayMargin_Click(object sender, EventArgs e)
        {
            DisplayMarginDialog(true);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnDelete.Enabled = false;

                var myView = this.gridView1;
                var selectedHandles = myView.GetSelectedRows();

                if (selectedHandles.Length == 0) return;

                selectedHandles = myView.GetSelectedRows().Where(x => x > -1).ToArray();

                if (DXMessage.ShowYesNoAndTips("确定移除所选的信息么？") == System.Windows.Forms.DialogResult.Yes)
                {
                    var ids = new List<int>();
                    foreach (var rowhandle in selectedHandles)
                    {
                        ids.Add(int.Parse(myView.GetRowCellValue(selectedHandles[rowhandle], colId).ToString()));
                    }

                    this._marginService.DeleteMarginTradingInfo(ids);

                    DisplaySearchResult();
                }
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
            finally
            {
                this.btnDelete.Enabled = true;
            }
        }

        private void btnSaveLayout_Click(object sender, EventArgs e)
        {
            this.gridView1.SaveLayout(_layoutXmlName);
        }

        private void gridView1_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            var myView = this.gridView1;
            var selectedHandles = myView.GetSelectedRows();
            if (selectedHandles.Any())
                selectedHandles = selectedHandles.Where(x => x > -1).ToArray();

            //this.btnRepayMargin.Enabled = false;
            if (selectedHandles.Length == 0)
            {
                this.btnDelete.Enabled = false;
            }
            else
            {
                this.btnDelete.Enabled = true;

                //if (selectedHandles.Length == 1)
                //{
                //    if (!bool.Parse(myView.GetRowCellValue(selectedHandles[0], colIsRepay).ToString()))
                //    {
                //        this.btnRepayMargin.Enabled = true;

                //        if (bool.Parse(myView.GetRowCellValue(selectedHandles[0], colIsFinancing).ToString()))
                //            this.btnRepayMargin.Text = "    还  资    ";
                //        else
                //            this.btnRepayMargin.Text = "    还  券    ";
                //    }
                //}
            }
        }

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