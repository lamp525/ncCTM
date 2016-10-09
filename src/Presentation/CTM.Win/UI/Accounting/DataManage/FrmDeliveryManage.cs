using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using CTM.Core;
using CTM.Core.Domain.TradeRecord;
using CTM.Core.Domain.User;
using CTM.Core.Util;
using CTM.Services.Account;
using CTM.Services.Common;
using CTM.Services.Stock;
using CTM.Services.TradeRecord;
using CTM.Services.User;
using CTM.Win.Extensions;
using CTM.Win.Models;
using CTM.Win.Util;

namespace CTM.Win.UI.Accounting.DataManage
{
    public partial class FrmDeliveryManage : BaseForm
    {
        #region Fields

        private readonly IDeliveryRecordService _deliveryRecordService;
        private readonly IStockService _stockService;
        private readonly IAccountService _accountService;
        private readonly IUserService _userService;
        private readonly ICommonService _commonService;

        private SearchModel _searchCondition;

        private const string _layoutXmlName = "FrmDeliveryManage";

        #endregion Fields

        #region Nested

        private class SearchModel
        {
            public string StockCode { get; set; }

            public int AccountId { get; set; }

            public bool? DealFlag { get; set; }

            public DateTime? TradeDateFrom { get; set; }

            public DateTime? TradeDateTo { get; set; }

            public string ImportUser { get; set; }

            public DateTime? ImportDateFrom { get; set; }

            public DateTime? ImportDateTo { get; set; }
        }

        #endregion Nested

        #region Constructors

        public FrmDeliveryManage(
            IDeliveryRecordService deliveryRecordService,
            IStockService stockService,
            IAccountService accountService,
            IUserService userService,
            ICommonService commonService
            )
        {
            InitializeComponent();

            this._deliveryRecordService = deliveryRecordService;
            this._accountService = accountService;
            this._stockService = stockService;
            this._userService = userService;
            this._commonService = commonService;
        }

        #endregion Constructors

        #region Utilities

        /// <summary>
        /// 绑定查询信息
        /// </summary>
        private void BindSearchInfo()
        {
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
           ).ToList();

            var allStockModel = new StockInfoModel
            {
                Id = 0,
                FullCode = "000000",
                Name = "全部",
                DisplayMember = "000000 - 全部",
            };
            stocks.Add(allStockModel);
            stocks = stocks.OrderBy(x => x.FullCode).ToList();
            luStock.Initialize(stocks, "FullCode", "DisplayMember", enableSearch: true);

            //账户
            var accounts = _accountService.GetAccountDetails(showDisabled: true).ToList();

            var allAccountModel = new AccountEntity
            {
                Id = 0,
                Name = "  全部  ",
                AttributeName = "  全部  ",
                SecurityCompanyName = "  全部  ",
                DisplayMember = "  全部  ",
            };
            accounts.Add(allAccountModel);
            accounts = accounts.OrderBy(x => x.Name).ThenBy(x => x.SecurityCompanyName).ToList();
            luAccount.Initialize(accounts, "Id", "DisplayMember", enableSearch: true);

            //导入人
            var importors = _userService.GetAllAdmins(showDeleted: true);

            var allUserModel = new UserInfo
            {
                Code = string.Empty,
                Name = "全部",
            };

            importors.Add(allUserModel);

            importors = importors.OrderBy(x => x.Code).ToList();

            luImport.Initialize(importors, "Code", "Name", showHeader: true, enableSearch: true);

            SetDefaultSearchInfo();
        }

        private void SetDefaultSearchInfo()
        {
            var today = _commonService.GetCurrentServerTime().Date;

            //交易时间（开始）
            deTradeFrom.EditValue = today.AddMonths(-1);
            //交易时间（结束）
            deTradeTo.EditValue = today;

            //导入时间（开始）
            deImportFrom.EditValue = today.AddDays(-1);
            //导入事件（结束）
            deImportTo.EditValue = today;

            //买卖标志
            this.chkBuy.Checked = false;
            this.chkSell.Checked = false;

            luStock.EditValue = null;
            luAccount.EditValue = null;

            //导入人
            luImport.EditValue = null;
        }

        /// <summary>
        /// 绑定交割单记录列表
        /// </summary>
        private void BindDeliveryRecord(SearchModel seachCondition)
        {
            var today = _commonService.GetCurrentServerTime().Date;

            if (seachCondition == null)
            {
                seachCondition = new SearchModel
                {
                    ImportDateFrom = today.AddDays(-1),
                    ImportDateTo = today,
                    ImportUser = LoginInfo.CurrentUser.UserCode,
                };
            }

            var records = _deliveryRecordService.GetDeliveryRecordsDetail(
                stockCode: seachCondition.StockCode,
                accountId: seachCondition.AccountId,
                dealFlag: seachCondition.DealFlag,
                tradeDateFrom: seachCondition.TradeDateFrom,
                tradeDateTo: seachCondition.TradeDateTo,
                importUserCode: seachCondition.ImportUser,
                importDateFrom: seachCondition.ImportDateFrom,
                importDateTo: seachCondition.ImportDateTo
                )
                .OrderBy(x => x.TradeDate)
                .ThenBy(x => x.TradeTime)
                .ToList();

            this.gridControl1.DataSource = records;
        }

        private void RefreshForm()
        {
            try
            {
                BindDeliveryRecord(_searchCondition);
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        #endregion Utilities

        #region Events

        private void FrmTradeDataManage_Load(object sender, EventArgs e)
        {
            try
            {
                this.btnDelete.Enabled = false;

                this.gridView1.LoadLayout(_layoutXmlName);

                this.gridView1.SetLayout(showGroupPanel: true, showFilterPanel: true, rowIndicatorWidth: 70, showCheckBoxRowSelect: true);

                BindSearchInfo();

                this.ActiveControl = this.btnSearch;

                BindDeliveryRecord(null);
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        /// <summary>
        /// 重置查询条件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, EventArgs e)
        {
            SetDefaultSearchInfo();
        }

        /// <summary>
        /// 查询交易数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnSearch.Enabled = false;
                this.btnClear.Enabled = false;

                _searchCondition = new SearchModel();

                //股票代码
                var stockInfo = this.luStock.GetSelectedDataRow() as StockInfoModel;
                _searchCondition.StockCode = (stockInfo == null || stockInfo.Id == 0) ? string.Empty : this.luStock.SelectedValue();

                //账号ID
                _searchCondition.AccountId = luAccount.EditValue == null ? 0 : int.Parse(luAccount.EditValue.ToString());

                //导入人
                _searchCondition.ImportUser = luImport.EditValue == null ? null : luImport.EditValue.ToString();

                //买卖标志
                if (chkBuy.Checked)
                    _searchCondition.DealFlag = true;
                else if (chkSell.Checked)
                    _searchCondition.DealFlag = false;

                //导入日期
                if (deImportFrom.EditValue == null)
                    _searchCondition.ImportDateFrom = null;
                else
                    _searchCondition.ImportDateFrom = CommonHelper.StringToDateTime(deImportFrom.EditValue.ToString());

                if (deImportTo.EditValue == null)
                    _searchCondition.ImportDateTo = null;
                else
                    _searchCondition.ImportDateTo = CommonHelper.StringToDateTime(deImportTo.EditValue.ToString());

                //交易日期
                if (deTradeFrom.EditValue == null)
                    _searchCondition.TradeDateFrom = null;
                else
                    _searchCondition.TradeDateFrom = CommonHelper.StringToDateTime(deTradeFrom.EditValue.ToString());

                if (deTradeTo.EditValue == null)
                    _searchCondition.TradeDateTo = null;
                else
                    _searchCondition.TradeDateTo = CommonHelper.StringToDateTime(deTradeTo.EditValue.ToString());

                BindDeliveryRecord(_searchCondition);
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
            finally
            {
                this.btnSearch.Enabled = true;
                this.btnClear.Enabled = true;
            }
        }

        private void btnSaveLayout_Click(object sender, EventArgs e)
        {
            this.gridView1.SaveLayout(_layoutXmlName);
        }

        /// <summary>
        /// 修改交易记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnImport_Click(object sender, EventArgs e)
        {
            this.ParentForm.DisplayForm<FrmDeliveryImport>("交割单数据导入");
        }

        /// <summary>
        /// 删除交易记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnDelete.Enabled = false;

                var myView = this.gridView1;

                var selectedHandles = myView.GetSelectedRows();

                if (DXMessage.ShowYesNoAndWarning("确定删除所选的交割单记录吗？") == DialogResult.Yes)
                {
                    var recordIds = new List<int>();

                    for (var rowhandle = 0; rowhandle < selectedHandles.Length; rowhandle++)
                    {
                        recordIds.Add(int.Parse(myView.GetRowCellValue(selectedHandles[rowhandle], colRecordId).ToString()));
                    }

                    this._deliveryRecordService.DeleteDeliveryRecords(recordIds.ToArray());

                    myView.DeleteSelectedRows();
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

        private void chkSell_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSell.Checked)
                chkBuy.Checked = false;
        }

        private void chkBuy_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBuy.Checked)
                chkSell.Checked = false;
        }

        private void gridView1_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            var myView = this.gridView1;
            var selectedHandles = myView.GetSelectedRows();

            if (selectedHandles.Length == 0)
                this.btnDelete.Enabled = false;
            else
                this.btnDelete.Enabled = true;
        }

        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column == this.colDealFlagName)
            {
                var row = this.gridView1.GetRow(e.ListSourceRowIndex) as DeliveryRecord;
                if (row == null) return;

                switch (row.DealFlag)
                {
                    case false:
                        e.DisplayText = "卖出";
                        break;

                    case true:
                        e.DisplayText = "买入";
                        break;

                    default:
                        e.DisplayText = "";
                        break;
                }
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