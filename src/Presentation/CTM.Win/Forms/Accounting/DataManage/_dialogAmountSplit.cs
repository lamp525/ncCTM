using System;
using System.Collections.Generic;
using System.Data;
using CTM.Core.Domain.TradeRecord;
using CTM.Core.Util;
using CTM.Data;
using CTM.Services.TradeRecord;
using CTM.Win.Extensions;
using CTM.Win.Models;
using CTM.Win.Util;

namespace CTM.Win.Forms.Accounting.DataManage
{
    public partial class _dialogAmountSplit : BaseForm
    {
        #region Fields

        private readonly IDailyRecordService _dailyService;
        private readonly IDeliveryRecordService _deliveryService;

        #endregion Fields

        #region Delegates

        public delegate void RefreshParentForm();
        public event RefreshParentForm RefreshEvent;

        #endregion Delegates

        #region Properties

        public int DeliveryId { get; set; }

        public int AccountId { get; set; }

        public string AccountInfo { get; set; }

        public string TradeDate { get; set; }

        public string StockCode { get; set; }

        public string StockName { get; set; }

        public decimal ActualAmount { get; set; }

        public decimal DealVolume { get; set; }

        public bool SplitFlag { get; set; }       

        #endregion Properties

        #region Constructors

        public _dialogAmountSplit(IDailyRecordService dailyService, IDeliveryRecordService deliveryService)
        {
            InitializeComponent();

            this._dailyService = dailyService;
            this._deliveryService = deliveryService;
        }

        #endregion Constructors

        private void DisplayYestodayPosition()
        {
            string sqlText = $@"EXEC [dbo].[sp_GetAccountPositionRate] @AccountId = {AccountId}, @StockCode = '{StockCode}', @TradeDate = '{TradeDate}'";
            var ds = SqlHelper.ExecuteDataset(AppConfig._ConnString, CommandType.Text, sqlText);
            if (ds != null && ds.Tables.Count == 1 && ds.Tables[0].Rows.Count > 0)
            {
                this.gridControl1.DataSource = ds.Tables[0];
                this.btnOk.Enabled = true;
            }
        }

        private void FormInit()
        {
            this.esiTitle.Text = $@"{TradeDate.Split(' ')[0]}  [{AccountInfo}] - [{StockCode} - {StockName}] （总金额：{ActualAmount}）";
            this.gridView1.SetLayout(showAutoFilterRow: false, showCheckBoxRowSelect: true, editable: false, readOnly: true, rowIndicatorWidth: 30);
            this.btnOk.Enabled = false;
        }

        private void _dialogAmountSplit_Load(object sender, EventArgs e)
        {
            try
            {
                FormInit();

                DisplayYestodayPosition();

                this.AcceptButton = btnOk;
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        private void gridView1_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            var myView = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            var dr = myView.GetRow(e.RowHandle) as DataRowView;
            if (dr == null) return;

            if (e.Column.Name == colSplitAmount.Name)
            {
                e.DisplayText = CommonHelper.SetDecimalDigits(decimal.Parse(dr[colRate.FieldName].ToString()) * ActualAmount).ToString();
            }
        }

        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }
        private void gridView1_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {

        }

        private void repositoryItemTextEdit1_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
        }

        private void gridView1_InvalidValueException(object sender, DevExpress.XtraEditors.Controls.InvalidValueExceptionEventArgs e)
        {
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnOk.Enabled = false;
                var source = gridControl1.DataSource as DataTable;
                if (source == null) return;

                if (DXMessage.ShowYesNoAndTips("是否确定导入？") == System.Windows.Forms.DialogResult.Yes)
                {
                    IList<DailyRecord> dailyRecords = new List<DailyRecord>();
                    var deliveryRecord = _deliveryService.GetDeliveryRecordById(DeliveryId);
                    foreach (DataRow dr in source.Rows)
                    {
                        var dailyRecord = new DailyRecord
                        {
                            AccountCode = deliveryRecord.AccountCode,
                            AccountId = deliveryRecord.AccountId,
                            ActualAmount = CommonHelper.SetDecimalDigits(decimal.Parse(dr[this.colRate.FieldName].ToString()) * deliveryRecord.ActualAmount),
                            Beneficiary = dr[this.colBeneficiary.FieldName].ToString(),
                            Commission = deliveryRecord.Commission,
                            ContractNo = deliveryRecord.ContractNo,
                            DataType = deliveryRecord.DataType,
                            DealAmount = 0,
                            DealFlag = deliveryRecord.DealFlag,
                            DealNo = deliveryRecord.DealNo,
                            DealPrice = deliveryRecord.DealPrice,
                            DealVolume = 0,
                            ImportTime = DateTime.Now,
                            ImportUser = LoginInfo.CurrentUser.UserCode,
                            Incidentals = deliveryRecord.Incidentals,
                            OperatorCode = dr[this.colBeneficiary.FieldName].ToString(),
                            Remarks = "财务交割单利息分配",
                            StampDuty = deliveryRecord.StampDuty,
                            StockCode = deliveryRecord.StockCode,
                            StockHolderCode = deliveryRecord.StockHolderCode,
                            StockName = deliveryRecord.StockName,
                            TradeDate = deliveryRecord.TradeDate,
                            TradeTime = deliveryRecord.TradeTime,
                            TradeType = int.Parse(dr[this.colTradeType.FieldName].ToString()),
                            UpdateTime = DateTime.Now,
                            UpdateUser = LoginInfo.CurrentUser.UserCode
                        };

                        dailyRecords.Add(dailyRecord);
                    }
                    _dailyService.InsertDailyRecords(dailyRecords);
                    this.SplitFlag = true;
                    this.Close();
                    this.RefreshEvent?.Invoke();
                }
                else
                    this.btnOk.Enabled = true;
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.SplitFlag = false;
            this.Close();
        }


    }
}