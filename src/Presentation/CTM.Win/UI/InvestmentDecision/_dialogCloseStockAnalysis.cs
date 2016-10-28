using System;
using System.Data;
using CTM.Core.Util;
using CTM.Data;
using CTM.Services.Common;
using CTM.Services.InvestmentDecision;
using CTM.Win.Extensions;
using CTM.Win.Models;
using CTM.Win.Util;
using DevExpress.XtraGrid.Columns;

namespace CTM.Win.UI.InvestmentDecision
{
    public partial class _dialogCloseStockAnalysis : BaseForm
    {

        #region Fields

        private readonly ICommonService _commonService;
        private readonly IInvestmentDecisionService _IDService;

        #endregion Fields

        #region Properties

        public string SerialNo { get; set; }

        #endregion Properties




        #region Constructors
        public _dialogCloseStockAnalysis(ICommonService commonService, IInvestmentDecisionService IDService)
        {
            InitializeComponent();

            this._commonService = commonService;
            this._IDService = IDService;
        }
        #endregion

        #region Utilities

        private void FormInit()
        {
            this.gridView1.SetLayout(showCheckBoxRowSelect: false, editable: true, readOnly: false, showGroupPanel: false, showFilterPanel: false, showAutoFilterRow: false, rowIndicatorWidth: 40);
            this.gridView1.OptionsView.ShowViewCaption = true;
            this.gridView1.ViewCaption = $@"收盘个股分析 - {SerialNo}";

            //foreach (GridColumn column in this.gridView1.Columns)
            //{
            //    if (column.Name == this.colInvestorCode.Name || column.Name == this.colSerialNo.Name || column.Name == this.colWeight.Name)
            //        column.OptionsColumn.AllowEdit = false;
            //    else
            //        column.OptionsColumn.AllowEdit = true;
            //}


        }

        private void BindCSADetail()
        {
            var connString = System.Configuration.ConfigurationManager.ConnectionStrings["CTMContext"].ToString();

            var commandText = $@"  SELECT * FROM [dbo].[v_CSADetail] WHERE SerialNo ='{SerialNo}' ORDER BY StockCode";

            var ds = SqlHelper.ExecuteDataset(connString, CommandType.Text, commandText);

            if (ds == null || ds.Tables.Count == 0) return;

            var source = ds.Tables[0];
            this.gridControl1.DataSource = source;
            this.gridView1.PopulateColumns();
            this.gridView1.SaveLayout("11");
        }

        #endregion Utilities

        #region Events
        private void _dialogCloseStockAnalysis_Load(object sender, EventArgs e)
        {
            try
            {
                FormInit();
                BindCSADetail();
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        } 
        #endregion
    }
}
