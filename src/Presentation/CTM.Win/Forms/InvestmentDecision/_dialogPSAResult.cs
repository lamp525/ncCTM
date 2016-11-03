using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using CTM.Services.Common;
using CTM.Services.InvestmentDecision;
using CTM.Win.Extensions;
using CTM.Win.Util;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Columns;

namespace CTM.Win.Forms.InvestmentDecision
{
    public partial class _dialogPSAResult : Form
    {
        #region Fields

        private readonly ICommonService _commonService;
        private readonly IInvestmentDecisionService _IDService;

        private bool _isExpanded = true;

        #endregion Fields

        #region Properties

        public string SerialNo { get; set; }

        public DateTime AnalysisDate { get; set; }

        #endregion Properties

        #region Constructors

        public _dialogPSAResult(ICommonService commonService, IInvestmentDecisionService IDService)
        {
            InitializeComponent();
            this._commonService = commonService;
            this._IDService = IDService;
        }

        #endregion Constructors

        #region Utilities

        private void BindPSASummaryAndDetail()
        {
            this.gridControl1.DataSource = null;

            var connString = System.Configuration.ConfigurationManager.ConnectionStrings["CTMContext"].ToString();

            var summaryCommandText = $@"SELECT * FROM [dbo].[v_PSASummary] WHERE SerialNo ='{SerialNo}' ORDER BY StockCode";

            var summaryAdapter = new SqlDataAdapter(summaryCommandText, connString);
            var summaryTable = new DataTable("Summary");
            summaryAdapter.Fill(summaryTable);

            var detailCommandText = $@"SELECT * FROM [dbo].[v_PSADetail] WHERE SerialNo ='{SerialNo}' ORDER BY InvestorCode";
            var detailAdapter = new SqlDataAdapter(detailCommandText, connString);

            var detailTable = new DataTable("Detail");
            detailAdapter.Fill(detailTable);

            var source = new DataSet();
            source.Tables.Add(summaryTable);
            source.Tables.Add(detailTable);

            DataColumn keyColumn = summaryTable.Columns["StockCode"];
            DataColumn foreignKeyColumn = detailTable.Columns["StockCode"];

            source.Relations.Add("SummaryDetail", keyColumn, foreignKeyColumn);

            this.gridControl1.DataSource = source.Tables["Summary"];

            this.btnExpandOrCollapse.Text = _isExpanded ? " 全部收起 " : " 全部展开 ";
            this.gridViewSummary.SetAllRowsExpanded(true);
        }

        private void FormInit()
        {
            this.esiTitle.Text = $@"股票池操作建议（{SerialNo}） - {AnalysisDate.ToShortDateString()}";

            this.gridViewSummary.SetLayout(showCheckBoxRowSelect: false, editable: true, editorShowMode: EditorShowMode.MouseDown, readOnly: false, showGroupPanel: false, showFilterPanel: false, showAutoFilterRow: true, rowIndicatorWidth: 40);

            foreach (GridColumn column in this.gridViewSummary.Columns)
            {
                if (column.Name == this.colPrincipalName_S.Name || column.Name == this.colStockCode_S.Name || column.Name == this.colStockName_S.Name)
                    column.OptionsColumn.AllowEdit = false;
                else
                    column.OptionsColumn.AllowEdit = true;
            }

            //操作类型
            var tradeTypes = new List<ImageComboBoxItem>();

            var target = new ImageComboBoxItem
            {
                Description = "目标",
                Value = 1,
            };
            tradeTypes.Add(target);
            var band = new ImageComboBoxItem
            {
                Description = "波段",
                Value = 2,
            };
            tradeTypes.Add(band);

            var day = new ImageComboBoxItem
            {
                Description = "隔日短差",
                Value = 3,
            };
            tradeTypes.Add(day);

            var imageComboBoxTradeType = new ImageComboBoxEdit();
            imageComboBoxTradeType.Initialize(tradeTypes, displayAdditionalItem: false);

            this.riImageComboBoxTradeType = imageComboBoxTradeType.Properties;

            //决策建议
            var suggestion = new List<ImageComboBoxItem>();
            var reserve = new ImageComboBoxItem
            {
                Description = "保留",
                Value = 1,
            };
            suggestion.Add(reserve);
            var buy = new ImageComboBoxItem
            {
                Description = "买",
                Value = 2,
            };
            suggestion.Add(buy);

            var sell = new ImageComboBoxItem
            {
                Description = "卖",
                Value = 3,
            };
            suggestion.Add(sell);

            var imageComboBoxSuggestion = new ImageComboBoxEdit();
            imageComboBoxSuggestion.Initialize(suggestion, displayAdditionalItem: false);

            this.riImageComboBoxDecision = imageComboBoxSuggestion.Properties;

            this.gridViewDetail.SetLayout(showCheckBoxRowSelect: false, editable: false, readOnly: true, showGroupPanel: false, showFilterPanel: false, showAutoFilterRow: false, rowIndicatorWidth: 40);
            this.gridViewDetail.ViewCaption = "分析详情一览";
        }

        #endregion Utilities

        #region Events

        private void _dialogPSAResult_Load(object sender, EventArgs e)
        {
            try
            {
                FormInit();

                BindPSASummaryAndDetail();
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        private void btnExpandOrCollapse_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnExpandOrCollapse.Enabled = false;

                this.gridViewSummary.SetAllRowsExpanded(!_isExpanded);

                this._isExpanded = !_isExpanded;
                this.btnExpandOrCollapse.Text = _isExpanded ? " 全部收起 " : " 全部展开 ";
            }
            finally
            {
                this.btnExpandOrCollapse.Enabled = true;
            }
        }

        private void gridViewSummary_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        private void gridViewSummary_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            if (e.RowHandle < 0) return;

            //操作类型
            if (e.Column.Name == colTradeType_S.Name)
            {
                var cellValue = this.gridViewSummary.GetRowCellValue(e.RowHandle, this.colTradeType_S.FieldName).ToString();

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

            //决策建议
            if (e.Column.Name == colDecision_S.Name)
            {
                var cellValue = this.gridViewSummary.GetRowCellValue(e.RowHandle, this.colDecision_S.FieldName).ToString();

                ImageComboBoxEdit imageComboBox = new ImageComboBoxEdit();
                imageComboBox.Properties.Items.AddRange(this.riImageComboBoxDecision.Items);
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

        private void gridViewSummary_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            DataRowView drv = (DataRowView)e.Row;
            DataRow row = drv.Row;
            if (row.RowState == DataRowState.Modified)
            {
                var id = int.Parse(row[colId_S.FieldName].ToString());

                var summary = _IDService.GetPSASummaryById(id);

                summary.Accuracy = row[colAccuracy_S.FieldName].ToString();
                summary.Decision = row[colDecision_S.FieldName].ToString();
                summary.PriceRange = row[colPriceRange_S.FieldName].ToString();
                summary.Reason = row[colReason_S.FieldName].ToString();
                summary.TradeType = int.Parse(row[colTradeType_S.FieldName].ToString());

                _IDService.UpdatePSASummary(summary);
            }
        }

        private void gridViewDetail_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        #endregion Events
    }
}