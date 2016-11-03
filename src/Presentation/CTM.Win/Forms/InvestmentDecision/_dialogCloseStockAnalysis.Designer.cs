namespace CTM.Win.Forms.InvestmentDecision
{
    partial class _dialogCloseStockAnalysis
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.bandedGridView1 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            this.gridBand1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colId = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colSerialNo = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colStockCode = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colStockName = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colTradeType = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.riImageComboBoxTradeType = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.colTradeTypeName = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colDecision = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colPriceRange = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colReason = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colAccuracy = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colAnalysisTime = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bandedGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riImageComboBoxTradeType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.gridControl1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(1004, 592);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // gridControl1
            // 
            this.gridControl1.Location = new System.Drawing.Point(12, 12);
            this.gridControl1.MainView = this.bandedGridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.riImageComboBoxTradeType});
            this.gridControl1.Size = new System.Drawing.Size(980, 568);
            this.gridControl1.TabIndex = 4;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.bandedGridView1});
            // 
            // bandedGridView1
            // 
            this.bandedGridView1.Appearance.ViewCaption.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.bandedGridView1.Appearance.ViewCaption.Options.UseFont = true;
            this.bandedGridView1.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBand1});
            this.bandedGridView1.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.colId,
            this.colSerialNo,
            this.colStockCode,
            this.colStockName,
            this.colTradeType,
            this.colTradeTypeName,
            this.colDecision,
            this.colPriceRange,
            this.colReason,
            this.colAccuracy,
            this.colAnalysisTime});
            this.bandedGridView1.GridControl = this.gridControl1;
            this.bandedGridView1.IndicatorWidth = 40;
            this.bandedGridView1.Name = "bandedGridView1";
            this.bandedGridView1.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDown;
            this.bandedGridView1.OptionsSelection.MultiSelect = true;
            this.bandedGridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.bandedGridView1.OptionsSelection.UseIndicatorForSelection = false;
            this.bandedGridView1.OptionsView.ColumnAutoWidth = false;
            this.bandedGridView1.OptionsView.EnableAppearanceEvenRow = true;
            this.bandedGridView1.OptionsView.EnableAppearanceOddRow = true;
            this.bandedGridView1.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.bandedGridView1.OptionsView.ShowGroupPanel = false;
            this.bandedGridView1.OptionsView.ShowViewCaption = true;
            this.bandedGridView1.ViewCaption = "股票池操作建议 - YC161028001";
            this.bandedGridView1.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.bandedGridView1_CustomDrawRowIndicator);
            this.bandedGridView1.CustomRowCellEdit += new DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventHandler(this.bandedGridView1_CustomRowCellEdit);
            this.bandedGridView1.RowUpdated += new DevExpress.XtraGrid.Views.Base.RowObjectEventHandler(this.bandedGridView1_RowUpdated);
            // 
            // gridBand1
            // 
            this.gridBand1.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.gridBand1.AppearanceHeader.Options.UseFont = true;
            this.gridBand1.Caption = "bandTitle";
            this.gridBand1.Columns.Add(this.colId);
            this.gridBand1.Columns.Add(this.colSerialNo);
            this.gridBand1.Columns.Add(this.colStockCode);
            this.gridBand1.Columns.Add(this.colStockName);
            this.gridBand1.Columns.Add(this.colTradeType);
            this.gridBand1.Columns.Add(this.colTradeTypeName);
            this.gridBand1.Columns.Add(this.colDecision);
            this.gridBand1.Columns.Add(this.colPriceRange);
            this.gridBand1.Columns.Add(this.colReason);
            this.gridBand1.Columns.Add(this.colAccuracy);
            this.gridBand1.Columns.Add(this.colAnalysisTime);
            this.gridBand1.Name = "gridBand1";
            this.gridBand1.VisibleIndex = 0;
            this.gridBand1.Width = 900;
            // 
            // colId
            // 
            this.colId.FieldName = "Id";
            this.colId.Name = "colId";
            this.colId.Width = 77;
            // 
            // colSerialNo
            // 
            this.colSerialNo.FieldName = "SerialNo";
            this.colSerialNo.Name = "colSerialNo";
            this.colSerialNo.Width = 77;
            // 
            // colStockCode
            // 
            this.colStockCode.Caption = "股票代码";
            this.colStockCode.FieldName = "StockCode";
            this.colStockCode.Name = "colStockCode";
            this.colStockCode.Visible = true;
            this.colStockCode.Width = 92;
            // 
            // colStockName
            // 
            this.colStockName.Caption = "股票名称";
            this.colStockName.FieldName = "StockName";
            this.colStockName.Name = "colStockName";
            this.colStockName.Visible = true;
            this.colStockName.Width = 97;
            // 
            // colTradeType
            // 
            this.colTradeType.Caption = "操作类型";
            this.colTradeType.ColumnEdit = this.riImageComboBoxTradeType;
            this.colTradeType.FieldName = "TradeType";
            this.colTradeType.Name = "colTradeType";
            this.colTradeType.Visible = true;
            this.colTradeType.Width = 77;
            // 
            // riImageComboBoxTradeType
            // 
            this.riImageComboBoxTradeType.AutoHeight = false;
            this.riImageComboBoxTradeType.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.riImageComboBoxTradeType.Name = "riImageComboBoxTradeType";
            // 
            // colTradeTypeName
            // 
            this.colTradeTypeName.FieldName = "TradeTypeName";
            this.colTradeTypeName.Name = "colTradeTypeName";
            this.colTradeTypeName.Width = 77;
            // 
            // colDecision
            // 
            this.colDecision.Caption = "决策判断";
            this.colDecision.FieldName = "Decision";
            this.colDecision.Name = "colDecision";
            this.colDecision.Visible = true;
            this.colDecision.Width = 99;
            // 
            // colPriceRange
            // 
            this.colPriceRange.Caption = "价格区间";
            this.colPriceRange.FieldName = "PriceRange";
            this.colPriceRange.Name = "colPriceRange";
            this.colPriceRange.Visible = true;
            this.colPriceRange.Width = 128;
            // 
            // colReason
            // 
            this.colReason.Caption = "判断及理由";
            this.colReason.FieldName = "Reason";
            this.colReason.Name = "colReason";
            this.colReason.Visible = true;
            this.colReason.Width = 278;
            // 
            // colAccuracy
            // 
            this.colAccuracy.Caption = "正确判断";
            this.colAccuracy.FieldName = "Accuracy";
            this.colAccuracy.Name = "colAccuracy";
            this.colAccuracy.Visible = true;
            this.colAccuracy.Width = 129;
            // 
            // colAnalysisTime
            // 
            this.colAnalysisTime.FieldName = "AnalysisTime";
            this.colAnalysisTime.Name = "colAnalysisTime";
            this.colAnalysisTime.Width = 84;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(1004, 592);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gridControl1;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(984, 572);
            this.layoutControlItem1.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // _dialogCloseStockAnalysis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1004, 592);
            this.Controls.Add(this.layoutControl1);
            this.Name = "_dialogCloseStockAnalysis";
            this.Text = "_dialogCloseStockAnalysis";
            this.Load += new System.EventHandler(this._dialogCloseStockAnalysis_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bandedGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riImageComboBoxTradeType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView bandedGridView1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colId;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colSerialNo;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colStockCode;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colStockName;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colTradeType;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colTradeTypeName;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colDecision;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colPriceRange;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colReason;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colAccuracy;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colAnalysisTime;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox riImageComboBoxTradeType;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand1;
    }
}