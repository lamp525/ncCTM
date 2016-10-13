using System;
using CTM.Core.Util;
using CTM.Services.Common;
using CTM.Services.StatisticsReport;
using CTM.Win.Extensions;
using CTM.Win.Util;

namespace CTM.Win.UI.Accounting.StatisticsReport
{
    public partial class FrmAccountInvestFundDetail : BaseForm
    {
        #region Fields

        private readonly ICommonService _commonService;
        private readonly IDeliveryStatisticsReportService _deliveryReportService;

        private const string _layoutXmlName = "FrmAccountInvestFundDetail";

        #endregion Fields

        #region Constructors

        public FrmAccountInvestFundDetail(ICommonService commonService, IDeliveryStatisticsReportService deliveryReportService)
        {
            InitializeComponent();

            this._commonService = commonService;
            this._deliveryReportService = deliveryReportService;
        }

        #endregion Constructors

        #region Utitlities

        private void BindSearchInfo()
        {
            var now = _commonService.GetCurrentServerTime();

            //开始时间
            this.deFrom.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.deFrom.EditValue = CommonHelper.GetFirstDayOfMonth(now);

            //结束时间
            this.deTo.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.deTo.EditValue = CommonHelper.GetLastDayOfMonth(now);
        }

        private void DisplaySearchResult()
        {
            var dateFrom = CommonHelper.StringToDateTime(this.deFrom.EditValue.ToString());
            var dateTo = CommonHelper.StringToDateTime(this.deTo.EditValue.ToString());

            var source = _deliveryReportService.GetAccountInvestFundDetail(dateFrom, dateTo);

            this.gridControl1.DataSource = source;
        }

        #endregion Utitlities

        #region Events

        private void FrmAccountInvestFundDetail_Load(object sender, EventArgs e)
        {
            try
            {
                BindSearchInfo();

                this.gridView1.LoadLayout(_layoutXmlName);
                this.gridView1.SetLayout(showGroupPanel: true, showFilterPanel: true, showCheckBoxRowSelect: false);

                this.ActiveControl = this.btnSearch;
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
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