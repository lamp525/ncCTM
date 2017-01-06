namespace CTM.Win.Forms.Accounting.StatisticsReport
{
    partial class FrmAccountPositionConfiguration
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
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.deFrom = new DevExpress.XtraEditors.DateEdit();
            this.deTo = new DevExpress.XtraEditors.DateEdit();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colUniqueSerialNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSubjectId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSubjectName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSubjectInvestFund = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSubjectNetAsset = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSubjectPositionValue = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSubjectPositionRate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSubjectNetProfit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSubjectNetProfitRate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAccountDetail = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStockCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStockName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCurrentPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCostPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colChangePercentage = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStockPositionVolume = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStockPositionValue = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStockProfit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStockProfitRate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStockProfitInSubjectRate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem4 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.deFrom.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deFrom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deTo.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btnSearch);
            this.layoutControl1.Controls.Add(this.deFrom);
            this.layoutControl1.Controls.Add(this.deTo);
            this.layoutControl1.Controls.Add(this.gridControl1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(1523, 721);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(455, 45);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 22);
            this.btnSearch.StyleController = this.layoutControl1;
            this.btnSearch.TabIndex = 7;
            this.btnSearch.Text = "    查  询    ";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // deFrom
            // 
            this.deFrom.EditValue = null;
            this.deFrom.Location = new System.Drawing.Point(86, 45);
            this.deFrom.Name = "deFrom";
            this.deFrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deFrom.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deFrom.Size = new System.Drawing.Size(145, 20);
            this.deFrom.StyleController = this.layoutControl1;
            this.deFrom.TabIndex = 6;
            this.deFrom.EditValueChanged += new System.EventHandler(this.deFrom_EditValueChanged);
            // 
            // deTo
            // 
            this.deTo.EditValue = null;
            this.deTo.Location = new System.Drawing.Point(296, 45);
            this.deTo.Name = "deTo";
            this.deTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deTo.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deTo.Size = new System.Drawing.Size(145, 20);
            this.deTo.StyleController = this.layoutControl1;
            this.deTo.TabIndex = 5;
            this.deTo.EditValueChanged += new System.EventHandler(this.deTo_EditValueChanged);
            // 
            // gridControl1
            // 
            this.gridControl1.Location = new System.Drawing.Point(24, 116);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1475, 581);
            this.gridControl1.TabIndex = 4;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colUniqueSerialNo,
            this.colSubjectId,
            this.colSubjectName,
            this.colSubjectInvestFund,
            this.colSubjectNetAsset,
            this.colSubjectPositionValue,
            this.colSubjectPositionRate,
            this.colSubjectNetProfit,
            this.colSubjectNetProfitRate,
            this.colAccountDetail,
            this.colStockCode,
            this.colStockName,
            this.colCurrentPrice,
            this.colCostPrice,
            this.colChangePercentage,
            this.colStockPositionVolume,
            this.colStockPositionValue,
            this.colStockProfit,
            this.colStockProfitRate,
            this.colStockProfitInSubjectRate});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDown;
            this.gridView1.OptionsBehavior.ReadOnly = true;
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.gridView1.OptionsSelection.MultiSelect = true;
            this.gridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.EnableAppearanceEvenRow = true;
            this.gridView1.OptionsView.EnableAppearanceOddRow = true;
            this.gridView1.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gridView1_CustomDrawRowIndicator);
            this.gridView1.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gridView1_RowCellStyle);
            this.gridView1.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gridView1_RowStyle);
            // 
            // colUniqueSerialNo
            // 
            this.colUniqueSerialNo.FieldName = "UniqueSerialNo";
            this.colUniqueSerialNo.Name = "colUniqueSerialNo";
            // 
            // colSubjectId
            // 
            this.colSubjectId.FieldName = "SubjectId";
            this.colSubjectId.Name = "colSubjectId";
            // 
            // colSubjectName
            // 
            this.colSubjectName.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.colSubjectName.AppearanceCell.Options.UseFont = true;
            this.colSubjectName.AppearanceCell.Options.UseTextOptions = true;
            this.colSubjectName.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colSubjectName.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.colSubjectName.AppearanceHeader.Options.UseFont = true;
            this.colSubjectName.AppearanceHeader.Options.UseTextOptions = true;
            this.colSubjectName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colSubjectName.Caption = "主体";
            this.colSubjectName.FieldName = "SubjectName";
            this.colSubjectName.Name = "colSubjectName";
            this.colSubjectName.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
            this.colSubjectName.Visible = true;
            this.colSubjectName.VisibleIndex = 0;
            this.colSubjectName.Width = 120;
            // 
            // colSubjectInvestFund
            // 
            this.colSubjectInvestFund.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.colSubjectInvestFund.AppearanceHeader.Options.UseFont = true;
            this.colSubjectInvestFund.AppearanceHeader.Options.UseTextOptions = true;
            this.colSubjectInvestFund.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colSubjectInvestFund.Caption = "投入资金(万元)";
            this.colSubjectInvestFund.DisplayFormat.FormatString = "N0";
            this.colSubjectInvestFund.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.colSubjectInvestFund.FieldName = "SubjectInvestFund";
            this.colSubjectInvestFund.Name = "colSubjectInvestFund";
            this.colSubjectInvestFund.Visible = true;
            this.colSubjectInvestFund.VisibleIndex = 1;
            this.colSubjectInvestFund.Width = 110;
            // 
            // colSubjectNetAsset
            // 
            this.colSubjectNetAsset.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.colSubjectNetAsset.AppearanceHeader.Options.UseFont = true;
            this.colSubjectNetAsset.AppearanceHeader.Options.UseTextOptions = true;
            this.colSubjectNetAsset.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colSubjectNetAsset.Caption = "净资产(万元)";
            this.colSubjectNetAsset.DisplayFormat.FormatString = "N0";
            this.colSubjectNetAsset.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.colSubjectNetAsset.FieldName = "SubjectNetAsset";
            this.colSubjectNetAsset.Name = "colSubjectNetAsset";
            this.colSubjectNetAsset.Visible = true;
            this.colSubjectNetAsset.VisibleIndex = 2;
            this.colSubjectNetAsset.Width = 100;
            // 
            // colSubjectPositionValue
            // 
            this.colSubjectPositionValue.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.colSubjectPositionValue.AppearanceHeader.Options.UseFont = true;
            this.colSubjectPositionValue.AppearanceHeader.Options.UseTextOptions = true;
            this.colSubjectPositionValue.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colSubjectPositionValue.Caption = "持仓市值(万元)";
            this.colSubjectPositionValue.DisplayFormat.FormatString = "N0";
            this.colSubjectPositionValue.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.colSubjectPositionValue.FieldName = "SubjectPositionValue";
            this.colSubjectPositionValue.Name = "colSubjectPositionValue";
            this.colSubjectPositionValue.Visible = true;
            this.colSubjectPositionValue.VisibleIndex = 3;
            this.colSubjectPositionValue.Width = 100;
            // 
            // colSubjectPositionRate
            // 
            this.colSubjectPositionRate.AppearanceCell.Options.UseTextOptions = true;
            this.colSubjectPositionRate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colSubjectPositionRate.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.colSubjectPositionRate.AppearanceHeader.Options.UseFont = true;
            this.colSubjectPositionRate.AppearanceHeader.Options.UseTextOptions = true;
            this.colSubjectPositionRate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colSubjectPositionRate.Caption = "持仓率";
            this.colSubjectPositionRate.FieldName = "SubjectPositionRate";
            this.colSubjectPositionRate.Name = "colSubjectPositionRate";
            this.colSubjectPositionRate.Visible = true;
            this.colSubjectPositionRate.VisibleIndex = 4;
            this.colSubjectPositionRate.Width = 65;
            // 
            // colSubjectNetProfit
            // 
            this.colSubjectNetProfit.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.colSubjectNetProfit.AppearanceHeader.Options.UseFont = true;
            this.colSubjectNetProfit.AppearanceHeader.Options.UseTextOptions = true;
            this.colSubjectNetProfit.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colSubjectNetProfit.Caption = "净收益(万元)";
            this.colSubjectNetProfit.DisplayFormat.FormatString = "N0";
            this.colSubjectNetProfit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.colSubjectNetProfit.FieldName = "SubjectNetProfit";
            this.colSubjectNetProfit.Name = "colSubjectNetProfit";
            this.colSubjectNetProfit.Visible = true;
            this.colSubjectNetProfit.VisibleIndex = 5;
            this.colSubjectNetProfit.Width = 100;
            // 
            // colSubjectNetProfitRate
            // 
            this.colSubjectNetProfitRate.AppearanceCell.Options.UseTextOptions = true;
            this.colSubjectNetProfitRate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colSubjectNetProfitRate.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.colSubjectNetProfitRate.AppearanceHeader.Options.UseFont = true;
            this.colSubjectNetProfitRate.AppearanceHeader.Options.UseTextOptions = true;
            this.colSubjectNetProfitRate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colSubjectNetProfitRate.Caption = "净收益率";
            this.colSubjectNetProfitRate.FieldName = "SubjectNetProfitRate";
            this.colSubjectNetProfitRate.Name = "colSubjectNetProfitRate";
            this.colSubjectNetProfitRate.Visible = true;
            this.colSubjectNetProfitRate.VisibleIndex = 6;
            this.colSubjectNetProfitRate.Width = 65;
            // 
            // colAccountDetail
            // 
            this.colAccountDetail.AppearanceCell.Options.UseTextOptions = true;
            this.colAccountDetail.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colAccountDetail.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.colAccountDetail.AppearanceHeader.Options.UseFont = true;
            this.colAccountDetail.AppearanceHeader.Options.UseTextOptions = true;
            this.colAccountDetail.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colAccountDetail.Caption = "账户";
            this.colAccountDetail.FieldName = "AccountDetail";
            this.colAccountDetail.Name = "colAccountDetail";
            this.colAccountDetail.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
            this.colAccountDetail.Visible = true;
            this.colAccountDetail.VisibleIndex = 7;
            this.colAccountDetail.Width = 140;
            // 
            // colStockCode
            // 
            this.colStockCode.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.colStockCode.AppearanceHeader.Options.UseFont = true;
            this.colStockCode.AppearanceHeader.Options.UseTextOptions = true;
            this.colStockCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colStockCode.Caption = "股票代码";
            this.colStockCode.FieldName = "StockCode";
            this.colStockCode.Name = "colStockCode";
            this.colStockCode.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colStockCode.Visible = true;
            this.colStockCode.VisibleIndex = 8;
            // 
            // colStockName
            // 
            this.colStockName.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.colStockName.AppearanceHeader.Options.UseFont = true;
            this.colStockName.AppearanceHeader.Options.UseTextOptions = true;
            this.colStockName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colStockName.Caption = "股票名称";
            this.colStockName.FieldName = "StockName";
            this.colStockName.Name = "colStockName";
            this.colStockName.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colStockName.Visible = true;
            this.colStockName.VisibleIndex = 9;
            // 
            // colCurrentPrice
            // 
            this.colCurrentPrice.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.colCurrentPrice.AppearanceHeader.Options.UseFont = true;
            this.colCurrentPrice.AppearanceHeader.Options.UseTextOptions = true;
            this.colCurrentPrice.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCurrentPrice.Caption = "当前价";
            this.colCurrentPrice.DisplayFormat.FormatString = "N";
            this.colCurrentPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.colCurrentPrice.FieldName = "CurrentPrice";
            this.colCurrentPrice.Name = "colCurrentPrice";
            this.colCurrentPrice.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colCurrentPrice.Visible = true;
            this.colCurrentPrice.VisibleIndex = 10;
            this.colCurrentPrice.Width = 60;
            // 
            // colCostPrice
            // 
            this.colCostPrice.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.colCostPrice.AppearanceHeader.Options.UseFont = true;
            this.colCostPrice.AppearanceHeader.Options.UseTextOptions = true;
            this.colCostPrice.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCostPrice.Caption = "成本价";
            this.colCostPrice.DisplayFormat.FormatString = "N";
            this.colCostPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.colCostPrice.FieldName = "CostPrice";
            this.colCostPrice.Name = "colCostPrice";
            this.colCostPrice.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colCostPrice.Visible = true;
            this.colCostPrice.VisibleIndex = 11;
            this.colCostPrice.Width = 60;
            // 
            // colChangePercentage
            // 
            this.colChangePercentage.AppearanceCell.Options.UseTextOptions = true;
            this.colChangePercentage.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colChangePercentage.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.colChangePercentage.AppearanceHeader.Options.UseFont = true;
            this.colChangePercentage.AppearanceHeader.Options.UseTextOptions = true;
            this.colChangePercentage.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colChangePercentage.Caption = "当日涨跌幅";
            this.colChangePercentage.FieldName = "ChangePercentage";
            this.colChangePercentage.Name = "colChangePercentage";
            this.colChangePercentage.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colChangePercentage.Visible = true;
            this.colChangePercentage.VisibleIndex = 12;
            // 
            // colStockPositionVolume
            // 
            this.colStockPositionVolume.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.colStockPositionVolume.AppearanceHeader.Options.UseFont = true;
            this.colStockPositionVolume.AppearanceHeader.Options.UseTextOptions = true;
            this.colStockPositionVolume.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colStockPositionVolume.Caption = "股数(万股)";
            this.colStockPositionVolume.FieldName = "StockPositionVolume";
            this.colStockPositionVolume.Name = "colStockPositionVolume";
            this.colStockPositionVolume.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colStockPositionVolume.Visible = true;
            this.colStockPositionVolume.VisibleIndex = 13;
            this.colStockPositionVolume.Width = 90;
            // 
            // colStockPositionValue
            // 
            this.colStockPositionValue.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.colStockPositionValue.AppearanceHeader.Options.UseFont = true;
            this.colStockPositionValue.AppearanceHeader.Options.UseTextOptions = true;
            this.colStockPositionValue.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colStockPositionValue.Caption = "市值(万元)";
            this.colStockPositionValue.DisplayFormat.FormatString = "N";
            this.colStockPositionValue.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.colStockPositionValue.FieldName = "StockPositionValue";
            this.colStockPositionValue.Name = "colStockPositionValue";
            this.colStockPositionValue.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colStockPositionValue.Visible = true;
            this.colStockPositionValue.VisibleIndex = 14;
            this.colStockPositionValue.Width = 90;
            // 
            // colStockProfit
            // 
            this.colStockProfit.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.colStockProfit.AppearanceHeader.Options.UseFont = true;
            this.colStockProfit.AppearanceHeader.Options.UseTextOptions = true;
            this.colStockProfit.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colStockProfit.Caption = "盈亏(万元)";
            this.colStockProfit.DisplayFormat.FormatString = "N";
            this.colStockProfit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.colStockProfit.FieldName = "StockProfit";
            this.colStockProfit.Name = "colStockProfit";
            this.colStockProfit.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colStockProfit.Visible = true;
            this.colStockProfit.VisibleIndex = 15;
            this.colStockProfit.Width = 90;
            // 
            // colStockProfitRate
            // 
            this.colStockProfitRate.AppearanceCell.Options.UseTextOptions = true;
            this.colStockProfitRate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colStockProfitRate.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.colStockProfitRate.AppearanceHeader.Options.UseFont = true;
            this.colStockProfitRate.AppearanceHeader.Options.UseTextOptions = true;
            this.colStockProfitRate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colStockProfitRate.Caption = "盈亏百分比";
            this.colStockProfitRate.FieldName = "StockProfitRate";
            this.colStockProfitRate.Name = "colStockProfitRate";
            this.colStockProfitRate.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colStockProfitRate.Visible = true;
            this.colStockProfitRate.VisibleIndex = 16;
            // 
            // colStockProfitInSubjectRate
            // 
            this.colStockProfitInSubjectRate.AppearanceCell.Options.UseTextOptions = true;
            this.colStockProfitInSubjectRate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colStockProfitInSubjectRate.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.colStockProfitInSubjectRate.AppearanceHeader.Options.UseFont = true;
            this.colStockProfitInSubjectRate.AppearanceHeader.Options.UseTextOptions = true;
            this.colStockProfitInSubjectRate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colStockProfitInSubjectRate.Caption = "占百分比";
            this.colStockProfitInSubjectRate.FieldName = "StockProfitInSubjectRate";
            this.colStockProfitInSubjectRate.Name = "colStockProfitInSubjectRate";
            this.colStockProfitInSubjectRate.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colStockProfitInSubjectRate.Visible = true;
            this.colStockProfitInSubjectRate.VisibleIndex = 17;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup2,
            this.layoutControlGroup3});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(1523, 721);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.AppearanceGroup.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.layoutControlGroup2.AppearanceGroup.Options.UseFont = true;
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3,
            this.layoutControlItem2,
            this.layoutControlItem4,
            this.emptySpaceItem1,
            this.emptySpaceItem2,
            this.emptySpaceItem3,
            this.emptySpaceItem4});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Size = new System.Drawing.Size(1503, 71);
            this.layoutControlGroup2.Text = "查询条件";
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.deFrom;
            this.layoutControlItem3.Location = new System.Drawing.Point(11, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(200, 26);
            this.layoutControlItem3.Text = "开始日期";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(48, 14);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.deTo;
            this.layoutControlItem2.Location = new System.Drawing.Point(221, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(200, 26);
            this.layoutControlItem2.Text = "结束日期";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(48, 14);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.btnSearch;
            this.layoutControlItem4.Location = new System.Drawing.Point(431, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(79, 26);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(11, 26);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(211, 0);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(10, 26);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            this.emptySpaceItem3.Location = new System.Drawing.Point(421, 0);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(10, 26);
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem4
            // 
            this.emptySpaceItem4.AllowHotTrack = false;
            this.emptySpaceItem4.Location = new System.Drawing.Point(510, 0);
            this.emptySpaceItem4.Name = "emptySpaceItem4";
            this.emptySpaceItem4.Size = new System.Drawing.Size(969, 26);
            this.emptySpaceItem4.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.AppearanceGroup.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.layoutControlGroup3.AppearanceGroup.Options.UseFont = true;
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.layoutControlGroup3.Location = new System.Drawing.Point(0, 71);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.Size = new System.Drawing.Size(1503, 630);
            this.layoutControlGroup3.Text = "仓位配置规划一览";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gridControl1;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1479, 585);
            this.layoutControlItem1.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // FrmAccountPositionConfiguration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1523, 721);
            this.Controls.Add(this.layoutControl1);
            this.Name = "FrmAccountPositionConfiguration";
            this.Text = "FrmAccountPositionConfiguration";
            this.Load += new System.EventHandler(this.FrmAccountPositionConfiguration_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.deFrom.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deFrom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deTo.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraEditors.DateEdit deFrom;
        private DevExpress.XtraEditors.DateEdit deTo;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem4;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraGrid.Columns.GridColumn colSubjectName;
        private DevExpress.XtraGrid.Columns.GridColumn colSubjectNetAsset;
        private DevExpress.XtraGrid.Columns.GridColumn colSubjectPositionValue;
        private DevExpress.XtraGrid.Columns.GridColumn colSubjectPositionRate;
        private DevExpress.XtraGrid.Columns.GridColumn colSubjectNetProfit;
        private DevExpress.XtraGrid.Columns.GridColumn colSubjectNetProfitRate;
        private DevExpress.XtraGrid.Columns.GridColumn colAccountDetail;
        private DevExpress.XtraGrid.Columns.GridColumn colStockCode;
        private DevExpress.XtraGrid.Columns.GridColumn colStockName;
        private DevExpress.XtraGrid.Columns.GridColumn colCurrentPrice;
        private DevExpress.XtraGrid.Columns.GridColumn colCostPrice;
        private DevExpress.XtraGrid.Columns.GridColumn colChangePercentage;
        private DevExpress.XtraGrid.Columns.GridColumn colStockPositionVolume;
        private DevExpress.XtraGrid.Columns.GridColumn colStockPositionValue;
        private DevExpress.XtraGrid.Columns.GridColumn colStockProfit;
        private DevExpress.XtraGrid.Columns.GridColumn colStockProfitRate;
        private DevExpress.XtraGrid.Columns.GridColumn colStockProfitInSubjectRate;
        private DevExpress.XtraGrid.Columns.GridColumn colSubjectInvestFund;
        private DevExpress.XtraGrid.Columns.GridColumn colSubjectId;
        private DevExpress.XtraGrid.Columns.GridColumn colUniqueSerialNo;
    }
}