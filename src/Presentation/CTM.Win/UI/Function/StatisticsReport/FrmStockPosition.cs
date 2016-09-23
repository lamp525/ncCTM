using System;
using System.Collections.Generic;
using System.Linq;
using CTM.Core;
using CTM.Core.Util;
using CTM.Services.TradeRecord;
using CTM.Win.Extensions;
using CTM.Win.Models;
using CTM.Win.Util;

namespace CTM.Win.UI.Function.StatisticsReport
{
    public partial class FrmStockPosition : BaseForm
    {
        #region Fields

        private readonly IDailyRecordService _tradeRecordService;

        // private readonly DateTime _initDate = new DateTime(2016, 2, 14);

        private readonly DateTime _initDate = AppConfigHelper.StatisticsInitDate;

        private const string _layoutXmlName = "FrmStockPosition";

        #endregion Fields

        #region Constructors

        public FrmStockPosition(IDailyRecordService tradeRecordService)
        {
            InitializeComponent();

            this._tradeRecordService = tradeRecordService;
        }

        #endregion Constructors

        #region Utilities

        private void BindStockPositionInfo(DateTime searchDate)
        {
            //交易记录
            var records = _tradeRecordService.GetDailyRecords(tradeDateFrom: _initDate, tradeDateTo: searchDate).ToList();

            if (records.Count == 0) return;

            var recordsByStock = records.GroupBy(x => x.StockCode);

            var stockPositionInfos = new List<StockPositionModel>();

            foreach (var stockGroup in recordsByStock)
            {
                var model = new StockPositionModel();

                model.StockFullCode = stockGroup.Key;
                model.StockName = stockGroup.First().StockName;
                //持股数
                model.StockHoldingVolume = stockGroup.Sum(x => x.DealVolume);

                stockPositionInfos.Add(model);
            }

            stockPositionInfos = stockPositionInfos.OrderBy(x => x.StockFullCode).ToList();

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

        private void FrmStockPosition_Load(object sender, EventArgs e)
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