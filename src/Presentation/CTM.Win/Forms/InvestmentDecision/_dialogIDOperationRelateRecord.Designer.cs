namespace CTM.Win.Forms.InvestmentDecision
{
    partial class _dialogIDOperationRelateRecord
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
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnOk = new DevExpress.XtraEditors.SimpleButton();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colRecordId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStockCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStockName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTradeDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTradeTime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDealFlagName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDealPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDealVolume = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colActualAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDataTypeName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTradeTypeName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBeneficiaryName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colImportUserName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRemarks = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAccountDetail = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btnCancel);
            this.layoutControl1.Controls.Add(this.btnOk);
            this.layoutControl1.Controls.Add(this.gridControl1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(1363, 647);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(1247, 613);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(104, 22);
            this.btnCancel.StyleController = this.layoutControl1;
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "    取  消    ";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(1120, 613);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(104, 22);
            this.btnOk.StyleController = this.layoutControl1;
            this.btnOk.TabIndex = 5;
            this.btnOk.Text = "     确  定     ";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // gridControl1
            // 
            this.gridControl1.Location = new System.Drawing.Point(12, 12);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1339, 597);
            this.gridControl1.TabIndex = 4;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colRecordId,
            this.colStockCode,
            this.colStockName,
            this.colTradeDate,
            this.colTradeTime,
            this.colDealFlagName,
            this.colDealPrice,
            this.colDealVolume,
            this.colActualAmount,
            this.colDataTypeName,
            this.colTradeTypeName,
            this.colBeneficiaryName,
            this.colImportUserName,
            this.colRemarks,
            this.colAccountDetail});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.IndicatorWidth = 35;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDown;
            this.gridView1.OptionsBehavior.ReadOnly = true;
            this.gridView1.OptionsSelection.CheckBoxSelectorColumnWidth = 30;
            this.gridView1.OptionsSelection.MultiSelect = true;
            this.gridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.EnableAppearanceEvenRow = true;
            this.gridView1.OptionsView.EnableAppearanceOddRow = true;
            this.gridView1.OptionsView.ShowAutoFilterRow = true;
            this.gridView1.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.gridView1.OptionsView.ShowFooter = true;
            this.gridView1.OptionsView.ShowGroupedColumns = true;
            this.gridView1.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gridView1_CustomDrawRowIndicator);
            this.gridView1.CustomDrawFooterCell += new DevExpress.XtraGrid.Views.Grid.FooterCellCustomDrawEventHandler(this.gridView1_CustomDrawFooterCell);
            this.gridView1.CustomDrawFooter += new DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventHandler(this.gridView1_CustomDrawFooter);
            this.gridView1.SelectionChanged += new DevExpress.Data.SelectionChangedEventHandler(this.gridView1_SelectionChanged);
            // 
            // colRecordId
            // 
            this.colRecordId.FieldName = "RecordId";
            this.colRecordId.Name = "colRecordId";
            // 
            // colStockCode
            // 
            this.colStockCode.Caption = "股票代码";
            this.colStockCode.FieldName = "StockCode";
            this.colStockCode.Name = "colStockCode";
            this.colStockCode.Visible = true;
            this.colStockCode.VisibleIndex = 2;
            // 
            // colStockName
            // 
            this.colStockName.Caption = "股票名称";
            this.colStockName.FieldName = "StockName";
            this.colStockName.Name = "colStockName";
            this.colStockName.Visible = true;
            this.colStockName.VisibleIndex = 3;
            // 
            // colTradeDate
            // 
            this.colTradeDate.Caption = "交易日期";
            this.colTradeDate.FieldName = "TradeDate";
            this.colTradeDate.Name = "colTradeDate";
            this.colTradeDate.Visible = true;
            this.colTradeDate.VisibleIndex = 4;
            // 
            // colTradeTime
            // 
            this.colTradeTime.Caption = "交易时间";
            this.colTradeTime.FieldName = "TradeTime";
            this.colTradeTime.Name = "colTradeTime";
            this.colTradeTime.Visible = true;
            this.colTradeTime.VisibleIndex = 5;
            // 
            // colDealFlagName
            // 
            this.colDealFlagName.Caption = "买卖标志";
            this.colDealFlagName.FieldName = "DealFlagName";
            this.colDealFlagName.Name = "colDealFlagName";
            this.colDealFlagName.Visible = true;
            this.colDealFlagName.VisibleIndex = 6;
            this.colDealFlagName.Width = 60;
            // 
            // colDealPrice
            // 
            this.colDealPrice.Caption = "成交价格(元)";
            this.colDealPrice.FieldName = "DealPrice";
            this.colDealPrice.Name = "colDealPrice";
            this.colDealPrice.Visible = true;
            this.colDealPrice.VisibleIndex = 7;
            this.colDealPrice.Width = 80;
            // 
            // colDealVolume
            // 
            this.colDealVolume.Caption = "成交数量";
            this.colDealVolume.FieldName = "DealVolume";
            this.colDealVolume.Name = "colDealVolume";
            this.colDealVolume.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "DealVolume", "合计={0:N0}")});
            this.colDealVolume.Visible = true;
            this.colDealVolume.VisibleIndex = 8;
            this.colDealVolume.Width = 110;
            // 
            // colActualAmount
            // 
            this.colActualAmount.Caption = "发生金额(元)";
            this.colActualAmount.FieldName = "ActualAmount";
            this.colActualAmount.Name = "colActualAmount";
            this.colActualAmount.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "ActualAmount", "合计={0:N4}")});
            this.colActualAmount.Visible = true;
            this.colActualAmount.VisibleIndex = 9;
            this.colActualAmount.Width = 160;
            // 
            // colDataTypeName
            // 
            this.colDataTypeName.Caption = "数据类型";
            this.colDataTypeName.FieldName = "DataTypeName";
            this.colDataTypeName.Name = "colDataTypeName";
            this.colDataTypeName.Visible = true;
            this.colDataTypeName.VisibleIndex = 10;
            // 
            // colTradeTypeName
            // 
            this.colTradeTypeName.Caption = "交易类别";
            this.colTradeTypeName.FieldName = "TradeTypeName";
            this.colTradeTypeName.Name = "colTradeTypeName";
            this.colTradeTypeName.Visible = true;
            this.colTradeTypeName.VisibleIndex = 11;
            this.colTradeTypeName.Width = 60;
            // 
            // colBeneficiaryName
            // 
            this.colBeneficiaryName.Caption = "实际受益人";
            this.colBeneficiaryName.FieldName = "BeneficiaryName";
            this.colBeneficiaryName.Name = "colBeneficiaryName";
            this.colBeneficiaryName.Visible = true;
            this.colBeneficiaryName.VisibleIndex = 12;
            this.colBeneficiaryName.Width = 70;
            // 
            // colImportUserName
            // 
            this.colImportUserName.Caption = "导入人";
            this.colImportUserName.FieldName = "ImportUserName";
            this.colImportUserName.Name = "colImportUserName";
            this.colImportUserName.Visible = true;
            this.colImportUserName.VisibleIndex = 13;
            this.colImportUserName.Width = 70;
            // 
            // colRemarks
            // 
            this.colRemarks.Caption = "备注";
            this.colRemarks.FieldName = "Remarks";
            this.colRemarks.Name = "colRemarks";
            this.colRemarks.Visible = true;
            this.colRemarks.VisibleIndex = 14;
            this.colRemarks.Width = 85;
            // 
            // colAccountDetail
            // 
            this.colAccountDetail.Caption = "账户信息";
            this.colAccountDetail.FieldName = "AccountDetail";
            this.colAccountDetail.Name = "colAccountDetail";
            this.colAccountDetail.Visible = true;
            this.colAccountDetail.VisibleIndex = 1;
            this.colAccountDetail.Width = 180;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.emptySpaceItem2,
            this.emptySpaceItem1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(1363, 647);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gridControl1;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1343, 601);
            this.layoutControlItem1.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btnOk;
            this.layoutControlItem2.Location = new System.Drawing.Point(1108, 601);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(108, 26);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btnCancel;
            this.layoutControlItem3.Location = new System.Drawing.Point(1235, 601);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(108, 26);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 601);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(1108, 26);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(1216, 601);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(19, 26);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // _dialogIDOperationRelateRecord
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1363, 647);
            this.Controls.Add(this.layoutControl1);
            this.Name = "_dialogIDOperationRelateRecord";
            this.Text = "_dialogIDOperationRelateRecord";
            this.Load += new System.EventHandler(this._dialogIDOperationRelateRecord_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnOk;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraGrid.Columns.GridColumn colRecordId;
        private DevExpress.XtraGrid.Columns.GridColumn colStockCode;
        private DevExpress.XtraGrid.Columns.GridColumn colStockName;
        private DevExpress.XtraGrid.Columns.GridColumn colTradeDate;
        private DevExpress.XtraGrid.Columns.GridColumn colTradeTime;
        private DevExpress.XtraGrid.Columns.GridColumn colDealFlagName;
        private DevExpress.XtraGrid.Columns.GridColumn colDealPrice;
        private DevExpress.XtraGrid.Columns.GridColumn colDealVolume;
        private DevExpress.XtraGrid.Columns.GridColumn colActualAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colDataTypeName;
        private DevExpress.XtraGrid.Columns.GridColumn colTradeTypeName;
        private DevExpress.XtraGrid.Columns.GridColumn colBeneficiaryName;
        private DevExpress.XtraGrid.Columns.GridColumn colImportUserName;
        private DevExpress.XtraGrid.Columns.GridColumn colRemarks;
        private DevExpress.XtraGrid.Columns.GridColumn colAccountDetail;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
    }
}