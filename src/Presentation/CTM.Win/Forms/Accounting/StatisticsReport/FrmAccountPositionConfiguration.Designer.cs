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
            this.colOwnerName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNetAsset = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPositionValue = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPositionRate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNetProfit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNetProfitRate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAccountId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAccountDetail = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStockCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStockName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCurrentPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCostPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colChangePercentage = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPositionVolume = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStockPositionValue = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStockProfit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStockProfitRate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStockProfitInOwnerRate = new DevExpress.XtraGrid.Columns.GridColumn();
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
            this.btnSearch.Location = new System.Drawing.Point(455, 43);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(73, 22);
            this.btnSearch.StyleController = this.layoutControl1;
            this.btnSearch.TabIndex = 7;
            this.btnSearch.Text = "    查  询    ";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // deFrom
            // 
            this.deFrom.EditValue = null;
            this.deFrom.Location = new System.Drawing.Point(86, 43);
            this.deFrom.Name = "deFrom";
            this.deFrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deFrom.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deFrom.Size = new System.Drawing.Size(145, 20);
            this.deFrom.StyleController = this.layoutControl1;
            this.deFrom.TabIndex = 6;
            // 
            // deTo
            // 
            this.deTo.EditValue = null;
            this.deTo.Location = new System.Drawing.Point(296, 43);
            this.deTo.Name = "deTo";
            this.deTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deTo.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deTo.Size = new System.Drawing.Size(145, 20);
            this.deTo.StyleController = this.layoutControl1;
            this.deTo.TabIndex = 5;
            // 
            // gridControl1
            // 
            this.gridControl1.Location = new System.Drawing.Point(24, 112);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1475, 585);
            this.gridControl1.TabIndex = 4;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colOwnerName,
            this.colNetAsset,
            this.colPositionValue,
            this.colPositionRate,
            this.colNetProfit,
            this.colNetProfitRate,
            this.colAccountId,
            this.colAccountDetail,
            this.colStockCode,
            this.colStockName,
            this.colCurrentPrice,
            this.colCostPrice,
            this.colChangePercentage,
            this.colPositionVolume,
            this.colStockPositionValue,
            this.colStockProfit,
            this.colStockProfitRate,
            this.colStockProfitInOwnerRate});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDown;
            this.gridView1.OptionsBehavior.ReadOnly = true;
            this.gridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.EnableAppearanceEvenRow = true;
            this.gridView1.OptionsView.EnableAppearanceOddRow = true;
            this.gridView1.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gridView1_CustomDrawRowIndicator);
            // 
            // colOwnerName
            // 
            this.colOwnerName.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.colOwnerName.AppearanceHeader.Options.UseFont = true;
            this.colOwnerName.AppearanceHeader.Options.UseTextOptions = true;
            this.colOwnerName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colOwnerName.Caption = "主体";
            this.colOwnerName.FieldName = "OwnerName";
            this.colOwnerName.Name = "colOwnerName";
            this.colOwnerName.Visible = true;
            this.colOwnerName.VisibleIndex = 0;
            // 
            // colNetAsset
            // 
            this.colNetAsset.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.colNetAsset.AppearanceHeader.Options.UseFont = true;
            this.colNetAsset.AppearanceHeader.Options.UseTextOptions = true;
            this.colNetAsset.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colNetAsset.Caption = "净资产(万元)";
            this.colNetAsset.FieldName = "NetAsset";
            this.colNetAsset.Name = "colNetAsset";
            this.colNetAsset.Visible = true;
            this.colNetAsset.VisibleIndex = 1;
            this.colNetAsset.Width = 100;
            // 
            // colPositionValue
            // 
            this.colPositionValue.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.colPositionValue.AppearanceHeader.Options.UseFont = true;
            this.colPositionValue.AppearanceHeader.Options.UseTextOptions = true;
            this.colPositionValue.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colPositionValue.Caption = "持仓市值(万元)";
            this.colPositionValue.FieldName = "PositionValue";
            this.colPositionValue.Name = "colPositionValue";
            this.colPositionValue.Visible = true;
            this.colPositionValue.VisibleIndex = 2;
            this.colPositionValue.Width = 100;
            // 
            // colPositionRate
            // 
            this.colPositionRate.AppearanceCell.Options.UseTextOptions = true;
            this.colPositionRate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colPositionRate.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.colPositionRate.AppearanceHeader.Options.UseFont = true;
            this.colPositionRate.AppearanceHeader.Options.UseTextOptions = true;
            this.colPositionRate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colPositionRate.Caption = "持仓率";
            this.colPositionRate.FieldName = "PositionRate";
            this.colPositionRate.Name = "colPositionRate";
            this.colPositionRate.Visible = true;
            this.colPositionRate.VisibleIndex = 3;
            this.colPositionRate.Width = 65;
            // 
            // colNetProfit
            // 
            this.colNetProfit.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.colNetProfit.AppearanceHeader.Options.UseFont = true;
            this.colNetProfit.AppearanceHeader.Options.UseTextOptions = true;
            this.colNetProfit.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colNetProfit.Caption = "净收益(万元)";
            this.colNetProfit.FieldName = "NetProfit";
            this.colNetProfit.Name = "colNetProfit";
            this.colNetProfit.Visible = true;
            this.colNetProfit.VisibleIndex = 4;
            this.colNetProfit.Width = 100;
            // 
            // colNetProfitRate
            // 
            this.colNetProfitRate.AppearanceCell.Options.UseTextOptions = true;
            this.colNetProfitRate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colNetProfitRate.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.colNetProfitRate.AppearanceHeader.Options.UseFont = true;
            this.colNetProfitRate.AppearanceHeader.Options.UseTextOptions = true;
            this.colNetProfitRate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colNetProfitRate.Caption = "净收益率";
            this.colNetProfitRate.FieldName = "NetProfitRate";
            this.colNetProfitRate.Name = "colNetProfitRate";
            this.colNetProfitRate.Visible = true;
            this.colNetProfitRate.VisibleIndex = 5;
            this.colNetProfitRate.Width = 65;
            // 
            // colAccountId
            // 
            this.colAccountId.FieldName = "AccountId";
            this.colAccountId.Name = "colAccountId";
            // 
            // colAccountDetail
            // 
            this.colAccountDetail.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.colAccountDetail.AppearanceHeader.Options.UseFont = true;
            this.colAccountDetail.AppearanceHeader.Options.UseTextOptions = true;
            this.colAccountDetail.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colAccountDetail.Caption = "账户";
            this.colAccountDetail.FieldName = "AccountDetail";
            this.colAccountDetail.Name = "colAccountDetail";
            this.colAccountDetail.Visible = true;
            this.colAccountDetail.VisibleIndex = 6;
            this.colAccountDetail.Width = 150;
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
            this.colStockCode.Visible = true;
            this.colStockCode.VisibleIndex = 7;
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
            this.colStockName.Visible = true;
            this.colStockName.VisibleIndex = 8;
            // 
            // colCurrentPrice
            // 
            this.colCurrentPrice.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.colCurrentPrice.AppearanceHeader.Options.UseFont = true;
            this.colCurrentPrice.AppearanceHeader.Options.UseTextOptions = true;
            this.colCurrentPrice.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCurrentPrice.Caption = "当前价";
            this.colCurrentPrice.FieldName = "CurrentPrice";
            this.colCurrentPrice.Name = "colCurrentPrice";
            this.colCurrentPrice.Visible = true;
            this.colCurrentPrice.VisibleIndex = 9;
            this.colCurrentPrice.Width = 60;
            // 
            // colCostPrice
            // 
            this.colCostPrice.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.colCostPrice.AppearanceHeader.Options.UseFont = true;
            this.colCostPrice.AppearanceHeader.Options.UseTextOptions = true;
            this.colCostPrice.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCostPrice.Caption = "成本价";
            this.colCostPrice.FieldName = "CostPrice";
            this.colCostPrice.Name = "colCostPrice";
            this.colCostPrice.Visible = true;
            this.colCostPrice.VisibleIndex = 10;
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
            this.colChangePercentage.Visible = true;
            this.colChangePercentage.VisibleIndex = 11;
            // 
            // colPositionVolume
            // 
            this.colPositionVolume.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.colPositionVolume.AppearanceHeader.Options.UseFont = true;
            this.colPositionVolume.AppearanceHeader.Options.UseTextOptions = true;
            this.colPositionVolume.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colPositionVolume.Caption = "股数(万股)";
            this.colPositionVolume.FieldName = "PositionVolume";
            this.colPositionVolume.Name = "colPositionVolume";
            this.colPositionVolume.Visible = true;
            this.colPositionVolume.VisibleIndex = 12;
            this.colPositionVolume.Width = 90;
            // 
            // colStockPositionValue
            // 
            this.colStockPositionValue.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.colStockPositionValue.AppearanceHeader.Options.UseFont = true;
            this.colStockPositionValue.AppearanceHeader.Options.UseTextOptions = true;
            this.colStockPositionValue.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colStockPositionValue.Caption = "市值(万元)";
            this.colStockPositionValue.FieldName = "StockPositionValue";
            this.colStockPositionValue.Name = "colStockPositionValue";
            this.colStockPositionValue.Visible = true;
            this.colStockPositionValue.VisibleIndex = 13;
            this.colStockPositionValue.Width = 90;
            // 
            // colStockProfit
            // 
            this.colStockProfit.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.colStockProfit.AppearanceHeader.Options.UseFont = true;
            this.colStockProfit.AppearanceHeader.Options.UseTextOptions = true;
            this.colStockProfit.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colStockProfit.Caption = "盈亏(万元)";
            this.colStockProfit.FieldName = "StockProfit";
            this.colStockProfit.Name = "colStockProfit";
            this.colStockProfit.Visible = true;
            this.colStockProfit.VisibleIndex = 14;
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
            this.colStockProfitRate.Visible = true;
            this.colStockProfitRate.VisibleIndex = 15;
            // 
            // colStockProfitInOwnerRate
            // 
            this.colStockProfitInOwnerRate.AppearanceCell.Options.UseTextOptions = true;
            this.colStockProfitInOwnerRate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colStockProfitInOwnerRate.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.colStockProfitInOwnerRate.AppearanceHeader.Options.UseFont = true;
            this.colStockProfitInOwnerRate.AppearanceHeader.Options.UseTextOptions = true;
            this.colStockProfitInOwnerRate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colStockProfitInOwnerRate.Caption = "占百分比";
            this.colStockProfitInOwnerRate.FieldName = "StockProfitInOwnerRate";
            this.colStockProfitInOwnerRate.Name = "colStockProfitInOwnerRate";
            this.colStockProfitInOwnerRate.Visible = true;
            this.colStockProfitInOwnerRate.VisibleIndex = 16;
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
            this.layoutControlGroup2.Size = new System.Drawing.Size(1503, 69);
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
            this.layoutControlItem4.Size = new System.Drawing.Size(77, 26);
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
            this.emptySpaceItem4.Location = new System.Drawing.Point(508, 0);
            this.emptySpaceItem4.Name = "emptySpaceItem4";
            this.emptySpaceItem4.Size = new System.Drawing.Size(971, 26);
            this.emptySpaceItem4.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.AppearanceGroup.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.layoutControlGroup3.AppearanceGroup.Options.UseFont = true;
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.layoutControlGroup3.Location = new System.Drawing.Point(0, 69);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.Size = new System.Drawing.Size(1503, 632);
            this.layoutControlGroup3.Text = "仓位配置规划一览";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gridControl1;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1479, 589);
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
        private DevExpress.XtraGrid.Columns.GridColumn colOwnerName;
        private DevExpress.XtraGrid.Columns.GridColumn colNetAsset;
        private DevExpress.XtraGrid.Columns.GridColumn colPositionValue;
        private DevExpress.XtraGrid.Columns.GridColumn colPositionRate;
        private DevExpress.XtraGrid.Columns.GridColumn colNetProfit;
        private DevExpress.XtraGrid.Columns.GridColumn colNetProfitRate;
        private DevExpress.XtraGrid.Columns.GridColumn colAccountId;
        private DevExpress.XtraGrid.Columns.GridColumn colAccountDetail;
        private DevExpress.XtraGrid.Columns.GridColumn colStockCode;
        private DevExpress.XtraGrid.Columns.GridColumn colStockName;
        private DevExpress.XtraGrid.Columns.GridColumn colCurrentPrice;
        private DevExpress.XtraGrid.Columns.GridColumn colCostPrice;
        private DevExpress.XtraGrid.Columns.GridColumn colChangePercentage;
        private DevExpress.XtraGrid.Columns.GridColumn colPositionVolume;
        private DevExpress.XtraGrid.Columns.GridColumn colStockPositionValue;
        private DevExpress.XtraGrid.Columns.GridColumn colStockProfit;
        private DevExpress.XtraGrid.Columns.GridColumn colStockProfitRate;
        private DevExpress.XtraGrid.Columns.GridColumn colStockProfitInOwnerRate;
    }
}