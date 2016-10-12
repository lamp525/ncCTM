using System;
using CTM.Core.Util;
using CTM.Services.Account;
using CTM.Services.Department;
using CTM.Services.Dictionary;
using CTM.Services.StatisticsReport;
using CTM.Services.TKLine;
using CTM.Services.TradeRecord;
using CTM.Win.Extensions;
using CTM.Win.Util;
using DevExpress.Utils;

namespace CTM.Win.UI.Accounting.StatisticsReport
{
    public partial class FrmDeliveryAccountInvestIncomeDetail : BaseForm
    {
        #region Fields

        private readonly IAccountService _accountService;
        private readonly IDeliveryRecordService _deliveryRecordService;
        private readonly IDeliveryStatisticsReportService _deliveryReportService;
        private readonly IDepartmentService _deptService;
        private readonly IDictionaryService _dictionaryService;
        private readonly ITKLineService _tKLineService;

        private readonly DateTime _initDate = AppConfigHelper.StatisticsInitDate;

        private const string _layoutXmlName = "FrmDeliveryAccountInvestIncomeDetail";

        #endregion Fields

        #region Constructors

        public FrmDeliveryAccountInvestIncomeDetail(
            IAccountService accountService,
            IDeliveryRecordService deliveryRecordService,
            IDeliveryStatisticsReportService deliveryReportService,
            IDepartmentService deptService,
            IDictionaryService dictionaryService,
            ITKLineService tKLineService)
        {
            InitializeComponent();

            this._accountService = accountService;
            this._deliveryRecordService = deliveryRecordService;
            this._deliveryReportService = deliveryReportService;
            this._deptService = deptService;
            this._dictionaryService = dictionaryService;
            this._tKLineService = tKLineService;
        }

        #endregion Constructors

        #region Utilities

        private void DisplaySearchResult()
        {
            var dateFrom = CommonHelper.StringToDateTime(this.deFrom.EditValue.ToString());
            var dateTo = CommonHelper.StringToDateTime(this.deTo.EditValue.ToString());

            var source = _deliveryReportService.GetDeliveryAccountInvestIncomeDetail(dateFrom, dateTo);

            this.gridControl1.DataSource = source;
        }

        #endregion Utilities

        #region Events

        private void FrmUserInvestIncomeAccount_Load(object sender, EventArgs e)
        {
            this.deFrom.Properties.AllowNullInput = DefaultBoolean.False;
            this.deFrom.EditValue = CommonHelper.GetFirstDayOfMonth(DateTime.Now.Date);

            this.deTo.Properties.AllowNullInput = DefaultBoolean.False;
            var now = DateTime.Now;
            if (now.Hour < 15)
                this.deTo.EditValue = now.Date.AddDays(-1);
            else
                this.deTo.EditValue = now.Date;

            this.gridView1.LoadLayout(_layoutXmlName);
            this.gridView1.SetLayout(showGroupPanel: true, showFilterPanel: true, showCheckBoxRowSelect: false);

            this.ActiveControl = this.btnSearch;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnSearch.Enabled = false;
                this.gridControl1.DataSource = null;

                DisplaySearchResult();
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