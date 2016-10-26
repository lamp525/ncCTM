using System;
using System.Data;
using System.Windows.Forms;
using CTM.Core.Infrastructure;
using CTM.Core.Util;
using CTM.Data;
using CTM.Services.Common;
using CTM.Services.InvestmentDecision;
using CTM.Win.Extensions;
using CTM.Win.Models;

namespace CTM.Win.UI.InvestmentDecision
{
    public partial class FrmMarketTrendForecastManage : BaseForm
    {
        #region Fields

        private readonly ICommonService _commonService;
        private readonly IInvestmentDecisionService _IDService;

        #endregion Fields

        #region Constructors

        public FrmMarketTrendForecastManage(ICommonService commonService, IInvestmentDecisionService IDService)
        {
            InitializeComponent();

            this._commonService = commonService;
            this._IDService = IDService;
        }

        #endregion Constructors

        #region Utilities

        private void FormInit()
        {
            this.deForecast.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.deForecast.EditValue = _commonService.GetCurrentServerTime().Date;

            this.gridView1.SetLayout(showGroupPanel: true, multiSelect: false, showCheckBoxRowSelect: false);
        }

        private void DisplayInputForm(string serialNo)
        {
            var dialog = EngineContext.Current.Resolve<_dialogMarketTrendForecast>();
            dialog.Owner = this.ParentForm;
            dialog.SerialNo = serialNo;
            dialog.Text = "大盘趋势预测详情";
            dialog.StartPosition = FormStartPosition.CenterScreen;
            dialog.Show();
        }

        private void BindMTFInfo()
        {
            var connString = System.Configuration.ConfigurationManager.ConnectionStrings["CTMContext"].ToString();
            var commandText = $@"EXEC [dbo].[sp_GetMarketTrendForecastInfo]";

            var ds = SqlHelper.ExecuteDataset (connString, CommandType.Text, commandText);

            if (ds == null || ds.Tables.Count == 0) return;

            var source = ds.Tables[0];
            this.gridControl1.DataSource = source;

            this.gridView1.PopulateColumns();
        }

        #endregion Utilities

        #region Events

        private void FrmMarketTrendForecast_Load(object sender, EventArgs e)
        {
            try
            {
                FormInit();

                BindMTFInfo();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                var applyDate = CommonHelper.StringToDateTime(this.deForecast.EditValue.ToString());

                var connString = System.Configuration.ConfigurationManager.ConnectionStrings["CTMContext"].ToString();
                var commandText = $@"DECLARE @serialNo  varchar(20)
                                        EXEC [dbo].[sp_GenerateMarketTrendInfo]
                                        @ApplyUser = '{LoginInfo.CurrentUser.UserCode }',
                                        @ApplyDate = '{applyDate}',
                                        @SerialNo = @serialNo OUTPUT
                                        SELECT @serialNo ";

                var serialNo = SqlHelper.ExecuteScalar(connString, CommandType.Text, commandText);

                DisplayInputForm(serialNo.ToString());
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion Events
    }
}