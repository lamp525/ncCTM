using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CTM.Core;
using CTM.Core.Infrastructure;
using CTM.Core.Util;
using CTM.Services.Account;
using CTM.Services.Dictionary;
using CTM.Services.StatisticsReport;
using CTM.Win.Extensions;
using CTM.Win.Models;
using CTM.Win.Util;

namespace CTM.Win.Forms.Accounting.DataManage
{
    public partial class FrmTradeDataVerify : BaseForm
    {
        #region Fields

        private readonly IDictionaryService _dictionaryService;
        private readonly IAccountService _accountService;
        private readonly IDataVerifyService _dataVerifyService;

        private IList<AccountEntity> _accounts;

        private const string _layoutXmlName = "FrmTradeDataVerify";

        private bool _isSearched = false;

        #endregion Fields

        #region Properties

        public bool IsExternalRequested { internal get; set; }
        public int AccountId { internal get; set; }
        public DateTime FromDate { internal get; set; }
        public DateTime ToDate { internal get; set; }

        #endregion Properties

        #region Enums

        private enum ResultDisplayType
        {
            Detail = 0,
            Summary = 1
        }

        #endregion Enums

        #region Constructors

        public FrmTradeDataVerify
            (
             IDictionaryService dictionaryService,
             IAccountService accountService,
             IDataVerifyService dataVerifyService
            )
        {
            InitializeComponent();

            this._dictionaryService = dictionaryService;
            this._accountService = accountService;
            this._dataVerifyService = dataVerifyService;
        }

        #endregion Constructors

        #region Utilities

        private void BindSearchInfo()
        {
            if (LoginInfo.CurrentUser.IsAdmin)
            {
                //账户属性信息
                var accountAttributes = _dictionaryService.GetDictionaryInfoByTypeId((int)EnumLibrary.DictionaryType.AccountAttribute)
                .Select(x => new ComboBoxItemModel
                {
                    Value = x.Code.ToString(),
                    Text = x.Name
                }).ToList();
                this.cbAccountAttribute.Initialize(accountAttributes, displayAdditionalItem: true);
                this.cbAccountAttribute.SelectedIndex = -1;

                //证券公司信息
                var securityCompanys = _dictionaryService.GetDictionaryInfoByTypeId((int)EnumLibrary.DictionaryType.SecurityCompay)

                            .Select(x => new ComboBoxItemModel
                            {
                                Value = x.Code.ToString(),
                                Text = x.Name
                            }).OrderBy(x => x.Text).ToList();

                this.cbSecurity.Initialize(securityCompanys, displayAdditionalItem: true);
                this.cbSecurity.SelectedIndex = -1;

                this.lciAttribute.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                this.lciSecurity.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }
            else
            {
                this.lciAttribute.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                this.lciSecurity.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }

            //账户信息
            _accounts = _accountService.GetAccountDetails(onlyNeedAccounting: true, showDisabled: true).OrderBy(x => x.Name).ToList();

            if (!LoginInfo.CurrentUser.IsAdmin)
            {
                var currentUserAccountIds = _accountService.GetAccountIdByOperatorId(LoginInfo.CurrentUser.UserId);

                if (currentUserAccountIds.Any())
                {
                    _accounts = _accounts.Where(x => currentUserAccountIds.Contains(x.Id)).ToList();

                    if (!_accounts.Any())
                    {
                        throw new Exception("抱歉！没有您可操作的账户信息！");
                    }
                }
            }

            var allAccount = new AccountEntity
            {
                Id = 0,
                Name = " 全部 ",
                DisplayMember = " 全部 ",
            };

            _accounts.Add(allAccount);
            _accounts = _accounts.OrderBy(x => x.Name).ToList();

            this.luAccount.Initialize(_accounts, "Id", "DisplayMember", showHeader: true, enableSearch: true);
            this.luAccount.EditValue = 0;

            var date = DateTime.Now.Date.AddMonths(-1);

            //开始时间
            this.deFrom.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.deFrom.EditValue = CommonHelper.GetFirstDayOfMonth(date);

            //结束时间
            this.deTo.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.deTo.EditValue = CommonHelper.GetLastDayOfMonth(date);
        }

        private void AccountFilter()
        {
            if (!string.IsNullOrEmpty(this.cbAccountAttribute.SelectedValue()) || !string.IsNullOrEmpty(this.cbSecurity.SelectedValue()))
            {
                var source = new List<AccountEntity>();
                source.AddRange(_accounts);

                if (!string.IsNullOrEmpty(this.cbAccountAttribute.SelectedValue()))
                    source = source?.Where(x => x.AttributeCode == int.Parse(this.cbAccountAttribute.SelectedValue())).ToList();

                if (!string.IsNullOrEmpty(this.cbSecurity.SelectedValue()))
                    source = source?.Where(x => x.SecurityCompanyCode == int.Parse(this.cbSecurity.SelectedValue())).ToList();

                this.luAccount.Properties.DataSource = source;
                this.luAccount.Properties.DropDownRows = source.Count;
            }
        }

        private void BindDiffInfo()
        {
            var displayType = this.rgDisplayType.SelectedIndex;
            var dateFrom = CommonHelper.StringToDateTime(this.deFrom.EditValue.ToString());
            var dateTo = CommonHelper.StringToDateTime(this.deTo.EditValue.ToString());

            var curAccountId = int.Parse(this.luAccount.SelectedValue());
            IList<int> accountIds = new List<int>();

            if (curAccountId == 0)
                accountIds = (this.luAccount.Properties.DataSource as IList<AccountEntity>).Select(x => x.Id).ToList();
            else
                accountIds.Add(curAccountId);

            var diffData = _dataVerifyService.sp_GetDeliveryAndEntrustDiffData(displayType, accountIds, dateFrom, dateTo);

            this.gridControl1.DataSource = diffData;
        }

        private void DisplayDataContrast(DataVerifyEntity entity)
        {
            var dialog = this.CreateDialog<_dialogTradeDataContrast>();

            dialog.AccountInfo = entity.AccountInfo;
            dialog.AccountId = entity.AccountId;
            dialog.StockCode = string.IsNullOrEmpty(entity.DE_StockCode) ? entity.DA_StockCode : entity.DE_StockCode;
            dialog.StockName = string.IsNullOrEmpty(entity.DE_StockName) ? entity.DA_StockName : entity.DE_StockName;
            dialog.FromDate = entity.DE_TradeDate.HasValue ? entity.DE_TradeDate.Value : CommonHelper.StringToDateTime(this.deFrom.EditValue.ToString());
            dialog.ToDate = entity.DE_TradeDate.HasValue ? entity.DE_TradeDate.Value : CommonHelper.StringToDateTime(this.deTo.EditValue.ToString());
            dialog.DealFlag = entity.DE_DealFlag.HasValue ? entity.DE_DealFlag.Value : entity.DA_DealFlag.Value;
            dialog.RefreshEvent += BindDiffInfo;
            dialog.Show();
        }

        #endregion Utilities

        #region Events

        private void FrmTradeDataVerify_Load(object sender, EventArgs e)
        {
            try
            {
                this.bandedGridView1.LoadLayout(_layoutXmlName);
                this.bandedGridView1.SetLayout(showCheckBoxRowSelect: true, showFilterPanel: true, showGroupPanel: true, showAutoFilterRow: true, rowIndicatorWidth: 50);

                BindSearchInfo();

                this.rgDisplayType.SelectedIndex = 0;

                if (IsExternalRequested)
                {
                    deFrom.EditValue = FromDate;
                    deTo.EditValue = ToDate;
                    luAccount.EditValue = AccountId;

                    BindDiffInfo();

                    _isSearched = true;
                }
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        private void cbAccountAttribute_SelectedIndexChanged(object sender, EventArgs e)
        {
            AccountFilter();
        }

        private void cbSecurity_SelectedIndexChanged(object sender, EventArgs e)
        {
            AccountFilter();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                this._isSearched = true;
                this.btnSearch.Enabled = false;

                if (string.IsNullOrEmpty(this.luAccount.SelectedValue()))
                {
                    DXMessage.ShowTips("请选择账号信息！");
                    return;
                }

                BindDiffInfo();
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
                this.gridControl1.DataSource = null;
            }
            finally
            {
                this.btnSearch.Enabled = true;
            }
        }

        private void btnSaveLayout_Click(object sender, EventArgs e)
        {
            this.bandedGridView1.SaveLayout(_layoutXmlName);
        }

        private void bandedGridView1_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            var currentView = sender as DevExpress.XtraGrid.Views.BandedGrid.BandedGridView;

            if (currentView == null || e.RowHandle < 0) return;

            var row = currentView.GetRow(e.RowHandle) as DataVerifyEntity;
            if (row == null) return;

            //数量差额
            if (e.Column == this.colVolumeDiff)
            {
                if (int.Parse(e.CellValue.ToString()) != 0)
                    e.Appearance.ForeColor = Color.Red;
            }

            //金额差额
            if (e.Column == this.colAmountDiff)
            {
                if (row.VolumeDiff != 0)
                    e.Appearance.ForeColor = Color.Red;
                else
                {
                    var diffRate = CommonHelper.CalculateRate(Math.Abs(row.AmountDiff), Math.Abs(row.DE_TotalActualAmount == null ? 0 : row.DE_TotalActualAmount.Value));

                    if (diffRate > 0.001M)
                        e.Appearance.ForeColor = Color.Red;
                }
            }
        }

        private void bandedGridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        private void rgDisplayType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_isSearched) return;

            try
            {
                this.rgDisplayType.Enabled = false;

                BindDiffInfo();
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
            finally
            {
                this.rgDisplayType.Enabled = true;
            }
        }

        private void bandedGridView1_DoubleClick(object sender, EventArgs e)
        {
            Point pt = bandedGridView1.GridControl.PointToClient(Control.MousePosition);

            var ghi = this.bandedGridView1.CalcHitInfo(pt);
            if (ghi.InRow)
            {
                var row = this.bandedGridView1.GetRow(ghi.RowHandle) as DataVerifyEntity;
                if (row != null)
                {
                    DisplayDataContrast(row);
                }
            }
        }

        private void bandedGridView1_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;

            var currentSerialNo = int.Parse(this.bandedGridView1.GetRowCellValue(e.RowHandle, this.col_SerialNo).ToString());
            if (currentSerialNo % 2 == 1)
                e.Appearance.BackColor = System.Drawing.Color.FromArgb(225, 244, 255);

            e.HighPriority = true;
        }

        #endregion Events
    }
}