using System;
using System.Collections.Generic;
using System.Data;
using CTM.Data;
using CTM.Services.Common;
using CTM.Services.InvestmentDecision;
using CTM.Win.Extensions;
using CTM.Win.Models;
using CTM.Win.Util;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Columns;

namespace CTM.Win.Forms.InvestmentDecision
{
    public partial class _dialogPSAEdit : BaseForm
    {
        #region Fields

        private readonly ICommonService _commonService;
        private readonly IInvestmentDecisionService _IDService;

        #endregion Fields

        #region Properties

        public string SerialNo { get; set; }

        public DateTime AnalysisDate { get; set; }

        #endregion Properties

        #region Constructors

        public _dialogPSAEdit(
            ICommonService commonService,
            IInvestmentDecisionService IDService)
        {
            InitializeComponent();

            this._commonService = commonService;
            this._IDService = IDService;
        }

        #endregion Constructors

        #region Utilities

        private void FormInit()
        {
            this.bandedGridView1.SetLayout(showCheckBoxRowSelect: false, editable: true, readOnly: false, showGroupPanel: false, showFilterPanel: false, showAutoFilterRow: false, rowIndicatorWidth: 40);

            this.bandedGridView1.OptionsView.ShowViewCaption = true;
            this.bandedGridView1.ViewCaption = $@"股票池操作建议 - {SerialNo}";

            this.gridBand1.Caption = $@"评判日期： {AnalysisDate.ToShortDateString()}   分析人员： {LoginInfo.CurrentUser.UserName }";

            foreach (GridColumn column in this.bandedGridView1.Columns)
            {
                if (column.Name == this.colStockCode.Name || column.Name == this.colStockName.Name)
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
        }

        private void BindPSADetail()
        {
            var connString = System.Configuration.ConfigurationManager.ConnectionStrings["CTMContext"].ToString();
            var commandText = $@"EXEC [dbo].[sp_GeneratePSADetail] @InvestorCode = '{LoginInfo.CurrentUser.UserCode }', @AnalysisDate = '{AnalysisDate}'";
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
                BindPSADetail();
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

            //操作类型
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

            //决策建议
            if (e.Column.Name == colDecision.Name)
            {
                var cellValue = this.bandedGridView1.GetRowCellValue(e.RowHandle, this.colDecision.FieldName).ToString();

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

        private void bandedGridView1_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            DataRowView drv = (DataRowView)e.Row;
            DataRow row = drv.Row;
            if (row.RowState == DataRowState.Modified)
            {
                var id = int.Parse(row[colId.FieldName].ToString());

                var detail = _IDService.GetPSADetailById(id);

                detail.Accuracy = row[colAccuracy.FieldName].ToString();
                detail.Decision = row[colDecision.FieldName].ToString();
                detail.PriceRange = row[colPriceRange.FieldName].ToString();
                detail.Reason = row[colReason.FieldName].ToString();
                detail.TradeType = int.Parse(row[colTradeType.FieldName].ToString());

                _IDService.UpdatePSADetail(detail);
            }
        }

        #endregion Events
    }
}