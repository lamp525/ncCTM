namespace CTM.Win.Forms.Accounting.DataManage
{
    partial class _dialogTradeDataContrast
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
            this.gridControl2 = new DevExpress.XtraGrid.GridControl();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colStockCode_R = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStockName_R = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTradeTime_R = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDealFlagName_R = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDealPrice_R = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDealVolume_R = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colActualAmount_R = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDataTypeName_R = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTradeTypeName_R = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBeneficaryName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colImportUserName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colStockCode_L = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStockName_L = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTradeTime_L = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDealFlagName_L = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDealPrice_L = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDealVolume_L = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colActualAmount_L = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.esiTitle = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.esiTitle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.gridControl2);
            this.layoutControl1.Controls.Add(this.gridControl1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(1432, 715);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // gridControl2
            // 
            this.gridControl2.Location = new System.Drawing.Point(581, 71);
            this.gridControl2.MainView = this.gridView2;
            this.gridControl2.Name = "gridControl2";
            this.gridControl2.Size = new System.Drawing.Size(839, 632);
            this.gridControl2.TabIndex = 5;
            this.gridControl2.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView2});
            // 
            // gridView2
            // 
            this.gridView2.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colStockCode_R,
            this.colStockName_R,
            this.colTradeTime_R,
            this.colDealFlagName_R,
            this.colDealPrice_R,
            this.colDealVolume_R,
            this.colActualAmount_R,
            this.colDataTypeName_R,
            this.colTradeTypeName_R,
            this.colBeneficaryName,
            this.colImportUserName});
            this.gridView2.GridControl = this.gridControl2;
            this.gridView2.IndicatorWidth = 40;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsBehavior.Editable = false;
            this.gridView2.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDown;
            this.gridView2.OptionsBehavior.ReadOnly = true;
            this.gridView2.OptionsSelection.MultiSelect = true;
            this.gridView2.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gridView2.OptionsView.ColumnAutoWidth = false;
            this.gridView2.OptionsView.EnableAppearanceEvenRow = true;
            this.gridView2.OptionsView.EnableAppearanceOddRow = true;
            this.gridView2.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.gridView2.OptionsView.ShowGroupPanel = false;
            this.gridView2.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gridView2_CustomDrawRowIndicator);
            // 
            // colStockCode_R
            // 
            this.colStockCode_R.Caption = "证券代码";
            this.colStockCode_R.FieldName = "StockCode";
            this.colStockCode_R.Name = "colStockCode_R";
            this.colStockCode_R.Visible = true;
            this.colStockCode_R.VisibleIndex = 0;
            this.colStockCode_R.Width = 70;
            // 
            // colStockName_R
            // 
            this.colStockName_R.Caption = "证券名称";
            this.colStockName_R.FieldName = "StockName";
            this.colStockName_R.Name = "colStockName_R";
            this.colStockName_R.Visible = true;
            this.colStockName_R.VisibleIndex = 1;
            this.colStockName_R.Width = 70;
            // 
            // colTradeTime_R
            // 
            this.colTradeTime_R.Caption = "交易时间";
            this.colTradeTime_R.FieldName = "TradeTime";
            this.colTradeTime_R.Name = "colTradeTime_R";
            this.colTradeTime_R.Visible = true;
            this.colTradeTime_R.VisibleIndex = 2;
            this.colTradeTime_R.Width = 70;
            // 
            // colDealFlagName_R
            // 
            this.colDealFlagName_R.Caption = "买卖标志";
            this.colDealFlagName_R.FieldName = "DealFlagName";
            this.colDealFlagName_R.Name = "colDealFlagName_R";
            this.colDealFlagName_R.Visible = true;
            this.colDealFlagName_R.VisibleIndex = 3;
            this.colDealFlagName_R.Width = 60;
            // 
            // colDealPrice_R
            // 
            this.colDealPrice_R.Caption = "成交价格";
            this.colDealPrice_R.FieldName = "DealPrice";
            this.colDealPrice_R.Name = "colDealPrice_R";
            this.colDealPrice_R.Visible = true;
            this.colDealPrice_R.VisibleIndex = 4;
            this.colDealPrice_R.Width = 60;
            // 
            // colDealVolume_R
            // 
            this.colDealVolume_R.Caption = "成交数量";
            this.colDealVolume_R.FieldName = "DealVolume";
            this.colDealVolume_R.Name = "colDealVolume_R";
            this.colDealVolume_R.Visible = true;
            this.colDealVolume_R.VisibleIndex = 5;
            // 
            // colActualAmount_R
            // 
            this.colActualAmount_R.Caption = "发生金额";
            this.colActualAmount_R.FieldName = "ActualAmount";
            this.colActualAmount_R.Name = "colActualAmount_R";
            this.colActualAmount_R.Visible = true;
            this.colActualAmount_R.VisibleIndex = 6;
            this.colActualAmount_R.Width = 100;
            // 
            // colDataTypeName_R
            // 
            this.colDataTypeName_R.Caption = "数据类型";
            this.colDataTypeName_R.FieldName = "DataTypeName";
            this.colDataTypeName_R.Name = "colDataTypeName_R";
            this.colDataTypeName_R.Visible = true;
            this.colDataTypeName_R.VisibleIndex = 7;
            // 
            // colTradeTypeName_R
            // 
            this.colTradeTypeName_R.Caption = "交易类别";
            this.colTradeTypeName_R.FieldName = "TradeTypeName";
            this.colTradeTypeName_R.Name = "colTradeTypeName_R";
            this.colTradeTypeName_R.Visible = true;
            this.colTradeTypeName_R.VisibleIndex = 8;
            this.colTradeTypeName_R.Width = 60;
            // 
            // colBeneficaryName
            // 
            this.colBeneficaryName.Caption = "受益人";
            this.colBeneficaryName.FieldName = "BeneficiaryName";
            this.colBeneficaryName.Name = "colBeneficaryName";
            this.colBeneficaryName.Visible = true;
            this.colBeneficaryName.VisibleIndex = 9;
            this.colBeneficaryName.Width = 70;
            // 
            // colImportUserName
            // 
            this.colImportUserName.Caption = "导入人";
            this.colImportUserName.FieldName = "ImportUserName";
            this.colImportUserName.Name = "colImportUserName";
            this.colImportUserName.Visible = true;
            this.colImportUserName.VisibleIndex = 10;
            this.colImportUserName.Width = 70;
            // 
            // gridControl1
            // 
            this.gridControl1.Location = new System.Drawing.Point(12, 71);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(565, 632);
            this.gridControl1.TabIndex = 4;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colStockCode_L,
            this.colStockName_L,
            this.colTradeTime_L,
            this.colDealFlagName_L,
            this.colDealPrice_L,
            this.colDealVolume_L,
            this.colActualAmount_L});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.IndicatorWidth = 40;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDown;
            this.gridView1.OptionsBehavior.ReadOnly = true;
            this.gridView1.OptionsSelection.MultiSelect = true;
            this.gridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.EnableAppearanceEvenRow = true;
            this.gridView1.OptionsView.EnableAppearanceOddRow = true;
            this.gridView1.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gridView1_CustomDrawRowIndicator);
            // 
            // colStockCode_L
            // 
            this.colStockCode_L.Caption = "证券代码";
            this.colStockCode_L.FieldName = "StockCode";
            this.colStockCode_L.Name = "colStockCode_L";
            this.colStockCode_L.Visible = true;
            this.colStockCode_L.VisibleIndex = 0;
            this.colStockCode_L.Width = 70;
            // 
            // colStockName_L
            // 
            this.colStockName_L.Caption = "证券名称";
            this.colStockName_L.FieldName = "StockName";
            this.colStockName_L.Name = "colStockName_L";
            this.colStockName_L.Visible = true;
            this.colStockName_L.VisibleIndex = 1;
            this.colStockName_L.Width = 70;
            // 
            // colTradeTime_L
            // 
            this.colTradeTime_L.Caption = "交易时间";
            this.colTradeTime_L.FieldName = "TradeTime";
            this.colTradeTime_L.Name = "colTradeTime_L";
            this.colTradeTime_L.Visible = true;
            this.colTradeTime_L.VisibleIndex = 2;
            this.colTradeTime_L.Width = 70;
            // 
            // colDealFlagName_L
            // 
            this.colDealFlagName_L.Caption = "买卖标志";
            this.colDealFlagName_L.FieldName = "DealFlagName";
            this.colDealFlagName_L.Name = "colDealFlagName_L";
            this.colDealFlagName_L.Visible = true;
            this.colDealFlagName_L.VisibleIndex = 3;
            this.colDealFlagName_L.Width = 60;
            // 
            // colDealPrice_L
            // 
            this.colDealPrice_L.Caption = "成交价";
            this.colDealPrice_L.FieldName = "DealPrice";
            this.colDealPrice_L.Name = "colDealPrice_L";
            this.colDealPrice_L.Visible = true;
            this.colDealPrice_L.VisibleIndex = 4;
            this.colDealPrice_L.Width = 60;
            // 
            // colDealVolume_L
            // 
            this.colDealVolume_L.Caption = "成交数量";
            this.colDealVolume_L.FieldName = "DealVolume";
            this.colDealVolume_L.Name = "colDealVolume_L";
            this.colDealVolume_L.Visible = true;
            this.colDealVolume_L.VisibleIndex = 5;
            // 
            // colActualAmount_L
            // 
            this.colActualAmount_L.Caption = "发生金额";
            this.colActualAmount_L.FieldName = "ActualAmount";
            this.colActualAmount_L.Name = "colActualAmount_L";
            this.colActualAmount_L.Visible = true;
            this.colActualAmount_L.VisibleIndex = 6;
            this.colActualAmount_L.Width = 100;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.esiTitle,
            this.emptySpaceItem1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(1432, 715);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.layoutControlItem1.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem1.AppearanceItemCaption.Options.UseTextOptions = true;
            this.layoutControlItem1.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.layoutControlItem1.Control = this.gridControl1;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 39);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(569, 656);
            this.layoutControlItem1.Text = "财务核算交割单";
            this.layoutControlItem1.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(105, 17);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.layoutControlItem2.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem2.AppearanceItemCaption.Options.UseTextOptions = true;
            this.layoutControlItem2.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.layoutControlItem2.Control = this.gridControl2;
            this.layoutControlItem2.Location = new System.Drawing.Point(569, 39);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(843, 656);
            this.layoutControlItem2.Text = "每日交易数据";
            this.layoutControlItem2.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(105, 17);
            // 
            // esiTitle
            // 
            this.esiTitle.AllowHotTrack = false;
            this.esiTitle.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.esiTitle.AppearanceItemCaption.Options.UseFont = true;
            this.esiTitle.AppearanceItemCaption.Options.UseTextOptions = true;
            this.esiTitle.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.esiTitle.Location = new System.Drawing.Point(0, 0);
            this.esiTitle.Name = "esiTitle";
            this.esiTitle.Size = new System.Drawing.Size(1412, 29);
            this.esiTitle.TextSize = new System.Drawing.Size(105, 0);
            this.esiTitle.TextVisible = true;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 29);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(1412, 10);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // _dialogTradeDataContrast
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1432, 715);
            this.Controls.Add(this.layoutControl1);
            this.Name = "_dialogTradeDataContrast";
            this.Text = "_dialogTradeDataContrast";
            this.Load += new System.EventHandler(this._dialogTradeDataContrast_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.esiTitle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraGrid.GridControl gridControl2;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.EmptySpaceItem esiTitle;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraGrid.Columns.GridColumn colStockCode_R;
        private DevExpress.XtraGrid.Columns.GridColumn colStockName_R;
        private DevExpress.XtraGrid.Columns.GridColumn colTradeTime_R;
        private DevExpress.XtraGrid.Columns.GridColumn colDealFlagName_R;
        private DevExpress.XtraGrid.Columns.GridColumn colDealPrice_R;
        private DevExpress.XtraGrid.Columns.GridColumn colDealVolume_R;
        private DevExpress.XtraGrid.Columns.GridColumn colActualAmount_R;
        private DevExpress.XtraGrid.Columns.GridColumn colDataTypeName_R;
        private DevExpress.XtraGrid.Columns.GridColumn colTradeTypeName_R;
        private DevExpress.XtraGrid.Columns.GridColumn colStockCode_L;
        private DevExpress.XtraGrid.Columns.GridColumn colStockName_L;
        private DevExpress.XtraGrid.Columns.GridColumn colTradeTime_L;
        private DevExpress.XtraGrid.Columns.GridColumn colDealFlagName_L;
        private DevExpress.XtraGrid.Columns.GridColumn colDealPrice_L;
        private DevExpress.XtraGrid.Columns.GridColumn colDealVolume_L;
        private DevExpress.XtraGrid.Columns.GridColumn colActualAmount_L;
        private DevExpress.XtraGrid.Columns.GridColumn colBeneficaryName;
        private DevExpress.XtraGrid.Columns.GridColumn colImportUserName;
    }
}