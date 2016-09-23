using System;
using System.Collections.Generic;
using System.Linq;
using CTM.Core;
using CTM.Core.Domain.Account;
using CTM.Core.Util;
using CTM.Services.Account;
using CTM.Services.TradeRecord;
using CTM.Win.Extensions;
using CTM.Win.Models;
using CTM.Win.Util;

namespace CTM.Win.UI.Function.StatisticsReport
{
    public partial class FrmAccountStockPosition : BaseForm
    {
        #region Fields

        private readonly IDailyRecordService _tradeRecordService;
        private readonly IAccountService _accountService;

        private readonly DateTime _initDate = AppConfigHelper.StatisticsInitDate;

        private const string _layoutXmlName = "FrmAccountStockPosition";

        #endregion Fields

        #region Constructors

        public FrmAccountStockPosition(
            IDailyRecordService tradeRecordService,
            IAccountService accountService
            )
        {
            InitializeComponent();

            this._tradeRecordService = tradeRecordService;
            this._accountService = accountService;
        }

        #endregion Constructors

        #region Utilities

        private void BindStockPositionInfo(DateTime searchDate)
        {
            //交易记录
            var allRecords = _tradeRecordService.GetDailyRecords(tradeDateFrom: _initDate, tradeDateTo: searchDate).ToList();

            if (allRecords.Count == 0) return;

            var recordsByAccount = allRecords.GroupBy(x => x.AccountId);

            //取得账户信息
            var accountIds = recordsByAccount.Select(x => x.Key).ToArray();
            var accountInfos = _accountService.GetAccountInfos(accountIds: accountIds);

            var stockPositionInfos = new List<StockPositionModel>();

            foreach (var accountGroup in recordsByAccount)
            {
                var recordsByStock = accountGroup.GroupBy(x => x.StockCode);

                //当前账户信息
                var accountInfo = accountInfos.SingleOrDefault(x => x.Id == accountGroup.Key);

                if (accountInfo == null)
                    accountInfo = new AccountInfo();

                foreach (var stockGroup in recordsByStock)
                {
                    var model = new StockPositionModel
                    {
                        AttributeName = accountInfo.AttributeName,
                        AccountName = accountInfo.Name,
                        SecurityCompanyName = accountInfo.SecurityCompanyName,
                        StockFullCode = stockGroup.Key,
                        StockName = stockGroup.First().StockName,
                        StockHoldingVolume = stockGroup.Sum(x => x.DealVolume),
                    };

                    stockPositionInfos.Add(model);
                }
            }

            stockPositionInfos = stockPositionInfos.OrderBy(x => x.AccountName).ThenBy(x => x.SecurityCompanyName).ThenBy(x => x.StockFullCode).ToList();

            this.gridControl1.DataSource = stockPositionInfos;
        }

        private void BindSeachInfo()
        {
            this.deDate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            var now = DateTime.Now;
            if (now.Hour < 15)
                this.deDate.EditValue = now.Date.AddDays(-1);
            else
                this.deDate.EditValue = now.Date;
        }

        #endregion Utilities

        #region Events

        private void FrmAccountStockPosition_Load(object sender, EventArgs e)
        {
            this.gridView1.LoadLayout(_layoutXmlName);

            this.gridView1.SetLayout(showFilterPanel: true, showCheckBoxRowSelect: false);

            BindSeachInfo();

            this.ActiveControl = this.btnSearch;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnSearch.Enabled = false;

                var searchDate = CommonHelper.StringToDateTime(deDate.EditValue.ToString()).Date;

                BindStockPositionInfo(searchDate);
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
            this.gridView1.SaveLayout(_layoutXmlName);
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