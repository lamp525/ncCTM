﻿namespace CTM.Win.Forms.InvestmentDecision
{
    partial class _dialogPSAEdit
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
            this.btnRefresh = new DevExpress.XtraEditors.SimpleButton();
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
            this.riImageComboBoxDecision = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.colPriceRange = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colDealRange = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colDealAmount = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.repositoryItemTextEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colReason = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colAccuracy = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colAnalysisDate = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colCreateTime = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.esiTitle = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bandedGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riImageComboBoxTradeType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riImageComboBoxDecision)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.esiTitle)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btnRefresh);
            this.layoutControl1.Controls.Add(this.gridControl1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(1181, 605);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(12, 12);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(76, 22);
            this.btnRefresh.StyleController = this.layoutControl1;
            this.btnRefresh.TabIndex = 5;
            this.btnRefresh.Text = "    刷  新    ";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // gridControl1
            // 
            this.gridControl1.Location = new System.Drawing.Point(12, 38);
            this.gridControl1.MainView = this.bandedGridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.riImageComboBoxTradeType,
            this.riImageComboBoxDecision,
            this.repositoryItemTextEdit1,
            this.repositoryItemTextEdit2});
            this.gridControl1.Size = new System.Drawing.Size(1157, 555);
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
            this.colDealRange,
            this.colDealAmount,
            this.colPriceRange,
            this.colReason,
            this.colAccuracy,
            this.colAnalysisDate,
            this.colCreateTime});
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
            this.gridBand1.Columns.Add(this.colDealRange);
            this.gridBand1.Columns.Add(this.colDealAmount);
            this.gridBand1.Columns.Add(this.colReason);
            this.gridBand1.Columns.Add(this.colAccuracy);
            this.gridBand1.Columns.Add(this.colAnalysisDate);
            this.gridBand1.Name = "gridBand1";
            this.gridBand1.VisibleIndex = 0;
            this.gridBand1.Width = 1071;
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
            this.colDecision.Caption = "决策建议";
            this.colDecision.ColumnEdit = this.riImageComboBoxDecision;
            this.colDecision.FieldName = "Decision";
            this.colDecision.Name = "colDecision";
            this.colDecision.Visible = true;
            this.colDecision.Width = 99;
            // 
            // riImageComboBoxDecision
            // 
            this.riImageComboBoxDecision.AutoHeight = false;
            this.riImageComboBoxDecision.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.riImageComboBoxDecision.Name = "riImageComboBoxDecision";
            // 
            // colPriceRange
            // 
            this.colPriceRange.Caption = "价格区间";
            this.colPriceRange.FieldName = "PriceRange";
            this.colPriceRange.Name = "colPriceRange";
            this.colPriceRange.Visible = true;
            this.colPriceRange.Width = 128;
            // 
            // colDealRange
            // 
            this.colDealRange.Caption = "幅度（%）";
            this.colDealRange.ColumnEdit = this.repositoryItemTextEdit1;
            this.colDealRange.DisplayFormat.FormatString = "###################################0";
            this.colDealRange.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colDealRange.FieldName = "DealRange";
            this.colDealRange.Name = "colDealRange";
            this.colDealRange.Visible = true;
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.DisplayFormat.FormatString = "###################################0";
            this.repositoryItemTextEdit1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.repositoryItemTextEdit1.EditFormat.FormatString = "###################################0";
            this.repositoryItemTextEdit1.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            // 
            // colDealAmount
            // 
            this.colDealAmount.Caption = "金额（万元）";
            this.colDealAmount.ColumnEdit = this.repositoryItemTextEdit2;
            this.colDealAmount.DisplayFormat.FormatString = "###################################0";
            this.colDealAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colDealAmount.FieldName = "DealAmount";
            this.colDealAmount.Name = "colDealAmount";
            this.colDealAmount.Visible = true;
            this.colDealAmount.Width = 96;
            // 
            // repositoryItemTextEdit2
            // 
            this.repositoryItemTextEdit2.AutoHeight = false;
            this.repositoryItemTextEdit2.DisplayFormat.FormatString = "###################################0";
            this.repositoryItemTextEdit2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.repositoryItemTextEdit2.EditFormat.FormatString = "###################################0";
            this.repositoryItemTextEdit2.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.repositoryItemTextEdit2.Name = "repositoryItemTextEdit2";
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
            // colAnalysisDate
            // 
            this.colAnalysisDate.FieldName = "AnalysisDate";
            this.colAnalysisDate.Name = "colAnalysisDate";
            this.colAnalysisDate.Width = 84;
            // 
            // colCreateTime
            // 
            this.colCreateTime.FieldName = "CreateTime";
            this.colCreateTime.Name = "colCreateTime";
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.esiTitle});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(1181, 605);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gridControl1;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1161, 559);
            this.layoutControlItem1.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btnRefresh;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(80, 26);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // esiTitle
            // 
            this.esiTitle.AllowHotTrack = false;
            this.esiTitle.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.esiTitle.AppearanceItemCaption.Options.UseFont = true;
            this.esiTitle.AppearanceItemCaption.Options.UseTextOptions = true;
            this.esiTitle.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.esiTitle.Location = new System.Drawing.Point(80, 0);
            this.esiTitle.Name = "esiTitle";
            this.esiTitle.Size = new System.Drawing.Size(1081, 26);
            this.esiTitle.TextSize = new System.Drawing.Size(0, 0);
            this.esiTitle.TextVisible = true;
            // 
            // _dialogPSAEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1181, 605);
            this.Controls.Add(this.layoutControl1);
            this.Name = "_dialogPSAEdit";
            this.Text = "_dialogPSAEdit";
            this.Load += new System.EventHandler(this._dialogPSAEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bandedGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riImageComboBoxTradeType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riImageComboBoxDecision)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.esiTitle)).EndInit();
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
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colAnalysisDate;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox riImageComboBoxTradeType;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox riImageComboBoxDecision;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colCreateTime;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colDealRange;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colDealAmount;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit2;
        private DevExpress.XtraEditors.SimpleButton btnRefresh;
        private DevExpress.XtraLayout.EmptySpaceItem esiTitle;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
    }
}