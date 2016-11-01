using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using CTM.Core;
using CTM.Core.Util;
using CTM.Services.Account;
using CTM.Services.Dictionary;
using CTM.Services.StatisticsReport;
using CTM.Win.Extensions;
using CTM.Win.Models;
using CTM.Win.Util;

namespace CTM.Win.UI.Accounting.DataManage
{
    public partial class FrmTradeDataVerify : BaseForm
    {
        #region Fields

        private readonly IDictionaryService _dictionaryService;
        private readonly IAccountService _accountService;
        private readonly IDataVerifyService _dataVerifyService;

        private IList<AccountEntity> _accounts;

        private const string _layoutXmlName = "FrmTradeDataVerify";

        #endregion Fields

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
            //账户属性信息
            var accountAttributes = _dictionaryService.GetDictionaryInfoByTypeId((int)EnumLibrary.DictionaryType.AccountAttribute)
                .Select(x => new ComboBoxItemModel
                {
                    Value = x.Code.ToString(),
                    Text = x.Name
                }).ToList();
            this.cbAccountAttribute.Initialize(accountAttributes);

            //证券公司信息
            var securityCompanys = _dictionaryService.GetDictionaryInfoByTypeId((int)EnumLibrary.DictionaryType.SecurityCompay)

                        .Select(x => new ComboBoxItemModel
                        {
                            Value = x.Code.ToString(),
                            Text = x.Name
                        }).OrderBy(x => x.Text).ToList();

            this.cbSecurity.Initialize(securityCompanys);

            //账户信息
            _accounts = _accountService.GetAccountDetails(onlyNeedAccounting: true, showDisabled: true).OrderBy(x => x.Name).ToList();

            this.luAccount.Initialize(_accounts, "Id", "DisplayMember", showHeader: true, enableSearch: true);

            var now = DateTime.Now.Date;

            //开始时间
            this.deFrom.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.deFrom.EditValue = CommonHelper.GetFirstDayOfMonth(now);

            //结束时间
            this.deTo.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.deTo.EditValue = CommonHelper.GetLastDayOfMonth(now);
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

        #endregion Utilities

        #region Events

        private void FrmTradeDataVerify_Load(object sender, EventArgs e)
        {
            try
            {
                this.bandedGridView1.LoadLayout(_layoutXmlName);
                this.bandedGridView1.SetLayout(showCheckBoxRowSelect: true, showFilterPanel: true, showAutoFilterRow: true, rowIndicatorWidth: 50);

                BindSearchInfo();
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
                this.btnSearch.Enabled = false;

                if (string.IsNullOrEmpty(this.luAccount.SelectedValue()))
                {
                    DXMessage.ShowTips("请选择账号信息！");
                    return;
                }

                var dateFrom = CommonHelper.StringToDateTime(this.deFrom.EditValue.ToString());
                var dateTo = CommonHelper.StringToDateTime(this.deTo.EditValue.ToString());
                var accountId = int.Parse(this.luAccount.SelectedValue());

                var diffInfos = _dataVerifyService.GetDiffBetweenDeliveryAndDailyData(accountId, dateFrom, dateTo);

                this.gridControl1.DataSource = diffInfos;
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

        #endregion Events
    }
}