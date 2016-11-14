using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using CTM.Core;
using CTM.Core.Domain.TKLine;
using CTM.Core.Domain.User;
using CTM.Core.Util;
using CTM.Services.Account;
using CTM.Services.Stock;
using CTM.Services.TKLine;
using CTM.Services.TradeRecord;
using CTM.Services.User;
using CTM.Win.Extensions;
using CTM.Win.Models;
using CTM.Win.Util;

namespace CTM.Win.Forms.DailyTrading.DataManage
{
    public partial class FrmStockTransfer : BaseForm
    {
        #region Fields

        private readonly IUserService _userService;
        private readonly IAccountService _accountService;
        private readonly IDailyRecordService _tradeRecordService;
        private readonly IStockService _stockService;
        private readonly ITKLineService _tkLineService;

        private readonly DateTime _initDate = AppConfigHelper.StatisticsInitDate;

        private const string _layoutXmlName = "FrmStockTransfer";
        private const string _layoutXmlNameRight = "FrmStockTransfer_Log";

        #endregion Fields

        #region Constructors

        public FrmStockTransfer(
            IDailyRecordService tradeRecordService,
            IUserService userService,
            IAccountService accountService,
            IStockService stockService,
            ITKLineService tkLineService
            )
        {
            InitializeComponent();

            this._userService = userService;
            this._tradeRecordService = tradeRecordService;
            this._accountService = accountService;
            this._stockService = stockService;
            this._tkLineService = tkLineService;
        }

        #endregion Constructors

        #region Utilities

        private void BindOperators()
        {
            var allUserModel = new UserInfo
            {
                Code = string.Empty,
                Name = "全部",
            };

            //交易员
            var dealers = _userService.GetAllDealer(showDeleted: true);
            dealers = dealers.OrderBy(x => x.Code).ToList();
            luDealer.Initialize(dealers, "Code", "Name", showHeader: false, enableSearch: true);

            if(!LoginInfo.CurrentUser .IsAdmin )
            {
                luDealer.ReadOnly = true;
                luDealer.EditValue = LoginInfo.CurrentUser.UserCode;
            }
        }

        private void BindStockPosition(string dealerCode)
        {
            this.gridControl1.DataSource = null;

            var records = _tradeRecordService.GetDailyRecordsBySearchCondition(beneficiary: dealerCode, tradeDateFrom: _initDate);

            if (!records.Any()) return;

            IList<StockPositionModel> userStockPositionInfos = new List<StockPositionModel>();

            var stockFullCodes = records.Select(x => x.StockCode).Distinct().ToArray();

            var today = DateTime.Now.Date;

            var currentClosePrices = this._tkLineService.GetStockClosePrices(today, stockFullCodes);

            var recordsByAccount = records.GroupBy(x => x.AccountId);

            var accountIds = records.Select(x => x.AccountId).Distinct().ToArray();
            var accountInfos = _accountService.GetAccountInfos(accountIds: accountIds);

            foreach (var accountGroup in recordsByAccount)
            {
                var currentAccount = accountInfos.SingleOrDefault(x => x.Id == accountGroup.Key);

                if (currentAccount == null) continue;

                var recordsByStock = accountGroup.GroupBy(x => x.StockCode);
                foreach (var stockGroup in recordsByStock)
                {
                    var holdingVolume = stockGroup.Sum(x => x.DealVolume);

                    if (holdingVolume == 0) continue;

                    decimal closePrice = (currentClosePrices.SingleOrDefault(x => x.StockCode.Trim() == stockGroup.Key) ?? new TKLineToday()).Close;

                    decimal positionValue = holdingVolume * closePrice;

                    var userPositionModel = new StockPositionModel
                    {
                        AccountId = currentAccount.Id,
                        AccountName = currentAccount.Name,
                        SecurityCompanyName = currentAccount.SecurityCompanyName,
                        AttributeName = currentAccount.AttributeName,
                        CurrentPrice = closePrice,
                        DealerCode = dealerCode,
                        DealerName = this.luDealer.SelectedText,
                        PositionValue = positionValue,
                        StockFullCode = stockGroup.Key,
                        StockHoldingVolume = holdingVolume,
                        StockName = stockGroup.First().StockName,
                    };

                    userStockPositionInfos.Add(userPositionModel);
                }
            }

            userStockPositionInfos = userStockPositionInfos.OrderBy(x => x.AccountName).ThenBy(x => x.StockFullCode).ToList();

            this.gridControl1.DataSource = userStockPositionInfos;
        }

        private void DisplayEditDialog(StockPositionModel selectedRecord)
        {
            var dialog = this.CreateDialog<_dialogStockTransfer>();
            dialog.RefreshEvent += new _dialogStockTransfer.RefreshParentForm(RefreshForm);
            dialog.Record = selectedRecord;
            dialog.Text = "股票转移";

            dialog.ShowDialog();
        }

        private void RefreshForm()
        {
            var dealerCode = this.luDealer.SelectedValue();

            if (string.IsNullOrEmpty(dealerCode)) return;

            BindStockPosition(dealerCode);

            BindTransferLog();
        }

        private void BindTransferLog(string holderCode = null, string receiverCode = null)
        {
            var logs = _stockService.GetStockTransferInfo(holderCode: holderCode, receiverCode: receiverCode).OrderBy(x => x.TransferTime).ThenBy(x => x.HolderName).ToList();

            this.gridControl2.DataSource = logs;
        }

        #endregion Utilities

        #region Event

        private void FrmStockTransfer_Load(object sender, EventArgs e)
        {
            this.gridView1.LoadLayout(_layoutXmlName);
            this.gridView1.SetLayout(showFilterPanel: false);

            this.gridView2.LoadLayout(_layoutXmlNameRight);
            this.gridView2.SetLayout();

            this.btnTransfer.Enabled = false;

            this.btnSearch.Enabled = false;

            BindOperators();

            BindTransferLog();
        }

        private void btnTransfer_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnTransfer.Enabled = false;

                var myView = this.gridView1;

                var selectedHandles = myView.GetSelectedRows();

                if (selectedHandles.Length != 1)
                {
                    DXMessage.ShowTips("请选择要转移的股票信息！");
                    return;
                }

                var record = myView.GetRow(selectedHandles[0]) as StockPositionModel;

                if (record.StockHoldingVolume == 0)
                {
                    DXMessage.ShowTips("持仓数量为0的股票无法进行转移操作！");
                    return;
                }

                DisplayEditDialog(record);
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
            finally
            {
                this.btnTransfer.Enabled = true;
            }
        }

        private void btnSaveLayout_Click(object sender, EventArgs e)
        {
            this.gridView1.SaveLayout(_layoutXmlName);
        }

        private void btnSaveLayoutRight_Click(object sender, EventArgs e)
        {
            this.gridView2.SaveLayout(_layoutXmlNameRight);
        }

        private void luDealer_EditValueChanged(object sender, EventArgs e)
        {
            var dealerCode = this.luDealer.SelectedValue();

            if (string.IsNullOrEmpty(dealerCode)) return;

            BindStockPosition(dealerCode);
        }

        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        private void gridView2_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        private void gridView1_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            var myView = this.gridView1;
            var selectedHandles = myView.GetSelectedRows();

            if (selectedHandles.Any())
                selectedHandles = selectedHandles.Where(x => x > -1).ToArray();

            if (selectedHandles.Length == 1)
            {
                var record = myView.GetRow(selectedHandles[0]) as StockPositionModel;

                if (record.StockHoldingVolume != 0)
                    this.btnTransfer.Enabled = true;
            }
            else
                this.btnTransfer.Enabled = false;
        }

        #endregion Event
    }
}