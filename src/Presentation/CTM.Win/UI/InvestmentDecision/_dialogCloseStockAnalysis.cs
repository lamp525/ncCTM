using System;
using System.Data;
using System.Linq;
using CTM.Core;
using CTM.Data;
using CTM.Services.Common;
using CTM.Services.Dictionary;
using CTM.Services.InvestmentDecision;
using CTM.Win.Extensions;
using CTM.Win.Util;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Columns;

namespace CTM.Win.UI.InvestmentDecision
{
    public partial class _dialogCloseStockAnalysis : BaseForm
    {
        #region Fields

        private readonly ICommonService _commonService;
        private readonly IInvestmentDecisionService _IDService;
        private readonly IDictionaryService _dictionaryService;

        #endregion Fields

        #region Properties

        public string SerialNo { get; set; }

        public DateTime JudgmentDate { get; set; }

        public string InvestorName { get; set; }

        public bool IsReadOnly { get; set; }

        #endregion Properties

        #region Constructors

        public _dialogCloseStockAnalysis(
            ICommonService commonService,
            IDictionaryService dictionaryService,
            IInvestmentDecisionService IDService)
        {
            InitializeComponent();

            this._commonService = commonService;
            this._dictionaryService = dictionaryService;
            this._IDService = IDService;
        }

        #endregion Constructors

        #region Utilities

        private void FormInit()
        {
            if (IsReadOnly)
                this.bandedGridView1.SetLayout(showCheckBoxRowSelect: false, editable: false, readOnly: true, showGroupPanel: false, showFilterPanel: false, showAutoFilterRow: false, rowIndicatorWidth: 40);
            else
                this.bandedGridView1.SetLayout(showCheckBoxRowSelect: false, editable: true, readOnly: false, showGroupPanel: false, showFilterPanel: false, showAutoFilterRow: false, rowIndicatorWidth: 40);

            this.bandedGridView1.OptionsView.ShowViewCaption = true;
            this.bandedGridView1.ViewCaption = $@"收盘个股分析 - {SerialNo}";

            this.gridBand1.Caption = $@"评判日期： {JudgmentDate.ToShortDateString()}   分析人员： {InvestorName}";

            foreach (GridColumn column in this.bandedGridView1.Columns)
            {
                if (column.Name == this.colStockCode.Name || column.Name == this.colStockName.Name)
                    column.OptionsColumn.AllowEdit = false;
                else
                    column.OptionsColumn.AllowEdit = true;
            }

            //交易类别
            var tradeTypes = _dictionaryService.GetDictionaryInfoByTypeId((int)EnumLibrary.DictionaryType.TradeType)
                .Select(x => new DevExpress.XtraEditors.Controls.ImageComboBoxItem
                {
                    Description = x.Name,
                    Value = x.Code.ToString(),
                }).ToList();
            var imageComboBox = new ImageComboBoxEdit();
            imageComboBox.Initialize(tradeTypes, displayAdditionalItem: false);

            this.riImageComboBoxTradeType = imageComboBox.Properties;
        }

        private void BindCSADetail()
        {
            var connString = System.Configuration.ConfigurationManager.ConnectionStrings["CTMContext"].ToString();

            var commandText = $@"  SELECT * FROM [dbo].[v_CSADetail] WHERE SerialNo ='{SerialNo}' ORDER BY StockCode";

            var ds = SqlHelper.ExecuteDataset(connString, CommandType.Text, commandText);

            if (ds == null || ds.Tables.Count == 0) return;

            var source = ds.Tables[0];
            this.gridControl1.DataSource = source;
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

        private void bandedGridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        private void bandedGridView1_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            if (e.RowHandle < 0) return;

            if (e.Column.Name == colTradeType.Name)
            {
                var cellValue = this.bandedGridView1.GetRowCellValue(e.RowHandle, this.colTradeType.FieldName).ToString();

                ImageComboBoxEdit imageComboBox = new ImageComboBoxEdit();
                imageComboBox.Properties.Items.AddRange(this.riImageComboBoxTradeType.Items);
                e.RepositoryItem = imageComboBox.Properties;

                foreach (ImageComboBoxItem item in imageComboBox.Properties.Items)
                {
                    if (cellValue == item.Value.ToString())
                    {
                        imageComboBox.SelectedItem = item;
                        return;
                    }
                }
            }
        }

        private void bandedGridView1_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            DataRowView drv = (DataRowView)e.Row;
            DataRow row = drv.Row;
            if (row.RowState == DataRowState.Modified)
            {
                var id = int.Parse(row[colId.FieldName].ToString());

                var detail = _IDService.GetCSADetailById(id);

                detail.Accuracy = row[colAccuracy.FieldName].ToString();
                detail.AnalysisTime = _commonService.GetCurrentServerTime();
                detail.Decision = row[colDecision.FieldName].ToString();
                detail.PriceRange = row[colPriceRange.FieldName].ToString();
                detail.Reason = row[colReason.FieldName].ToString();
                detail.TradeType = int.Parse(row[colTradeType.FieldName].ToString());

                _IDService.UpdateCSADetail(detail);
            }
        }

        #endregion Events
    }
}