namespace CTM.Win.Forms.InvestorStudio
{
    partial class FrmInvestorStudio
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
            DevExpress.XtraCharts.Series series19 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.PieSeriesLabel pieSeriesLabel3 = new DevExpress.XtraCharts.PieSeriesLabel();
            DevExpress.XtraCharts.PieSeriesView pieSeriesView3 = new DevExpress.XtraCharts.PieSeriesView();
            DevExpress.XtraCharts.ChartTitle chartTitle3 = new DevExpress.XtraCharts.ChartTitle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmInvestorStudio));
            DevExpress.XtraCharts.XYDiagram xyDiagram7 = new DevExpress.XtraCharts.XYDiagram();
            DevExpress.XtraCharts.ConstantLine constantLine5 = new DevExpress.XtraCharts.ConstantLine();
            DevExpress.XtraCharts.SecondaryAxisY secondaryAxisY3 = new DevExpress.XtraCharts.SecondaryAxisY();
            DevExpress.XtraCharts.ConstantLine constantLine6 = new DevExpress.XtraCharts.ConstantLine();
            DevExpress.XtraCharts.Series series20 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.SideBySideBarSeriesView sideBySideBarSeriesView7 = new DevExpress.XtraCharts.SideBySideBarSeriesView();
            DevExpress.XtraCharts.Series series21 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.PointSeriesLabel pointSeriesLabel5 = new DevExpress.XtraCharts.PointSeriesLabel();
            DevExpress.XtraCharts.SplineSeriesView splineSeriesView11 = new DevExpress.XtraCharts.SplineSeriesView();
            DevExpress.XtraCharts.Series series22 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.PointSeriesLabel pointSeriesLabel6 = new DevExpress.XtraCharts.PointSeriesLabel();
            DevExpress.XtraCharts.SplineSeriesView splineSeriesView12 = new DevExpress.XtraCharts.SplineSeriesView();
            DevExpress.XtraCharts.Series series23 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.SplineSeriesView splineSeriesView13 = new DevExpress.XtraCharts.SplineSeriesView();
            DevExpress.XtraCharts.Series series24 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.SplineSeriesView splineSeriesView14 = new DevExpress.XtraCharts.SplineSeriesView();
            DevExpress.XtraCharts.Series series25 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.SplineSeriesView splineSeriesView15 = new DevExpress.XtraCharts.SplineSeriesView();
            DevExpress.XtraCharts.XYDiagram xyDiagram8 = new DevExpress.XtraCharts.XYDiagram();
            DevExpress.XtraCharts.Series series26 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel5 = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
            DevExpress.XtraCharts.SideBySideBarSeriesView sideBySideBarSeriesView8 = new DevExpress.XtraCharts.SideBySideBarSeriesView();
            DevExpress.XtraCharts.XYDiagram xyDiagram9 = new DevExpress.XtraCharts.XYDiagram();
            DevExpress.XtraCharts.Series series27 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel6 = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
            DevExpress.XtraCharts.SideBySideBarSeriesView sideBySideBarSeriesView9 = new DevExpress.XtraCharts.SideBySideBarSeriesView();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.dePosition = new DevExpress.XtraEditors.DateEdit();
            this.gcPosition = new DevExpress.XtraGrid.GridControl();
            this.gvPosition = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colStockCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStockName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTradeType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTradeTypeName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPreVolume = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPreValue = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCurVolume = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCurValue = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBuyVolume = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBuyValue = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSellVolume = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSellValue = new DevExpress.XtraGrid.Columns.GridColumn();
            this.chartPosition = new DevExpress.XtraCharts.ChartControl();
            this.lblYearR = new DevExpress.XtraEditors.LabelControl();
            this.deProfit = new DevExpress.XtraEditors.DateEdit();
            this.cbTradeTypeProfit = new DevExpress.XtraEditors.ComboBoxEdit();
            this.chbDay = new DevExpress.XtraEditors.CheckEdit();
            this.chbWeek = new DevExpress.XtraEditors.CheckEdit();
            this.chbMonth = new DevExpress.XtraEditors.CheckEdit();
            this.chbYear = new DevExpress.XtraEditors.CheckEdit();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.layoutControl2 = new DevExpress.XtraLayout.LayoutControl();
            this.chartProfitTrend = new DevExpress.XtraCharts.ChartControl();
            this.chartGain = new DevExpress.XtraCharts.ChartControl();
            this.chartLoss = new DevExpress.XtraCharts.ChartControl();
            this.layoutControlGroup8 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem11 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem13 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem15 = new DevExpress.XtraLayout.LayoutControlItem();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.layoutControl3 = new DevExpress.XtraLayout.LayoutControl();
            this.layoutControlGroup9 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lblCurValue = new DevExpress.XtraEditors.LabelControl();
            this.lblDayP = new DevExpress.XtraEditors.LabelControl();
            this.lblDayR = new DevExpress.XtraEditors.LabelControl();
            this.lblWeekP = new DevExpress.XtraEditors.LabelControl();
            this.lblWeekR = new DevExpress.XtraEditors.LabelControl();
            this.lblMonthP = new DevExpress.XtraEditors.LabelControl();
            this.lblMonthR = new DevExpress.XtraEditors.LabelControl();
            this.lblYearP = new DevExpress.XtraEditors.LabelControl();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcgInvestor = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem26 = new DevExpress.XtraLayout.LayoutControlItem();
            this.esiInvestor = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlGroup4 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem27 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem28 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem29 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem30 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup5 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem31 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem32 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem33 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem24 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem5 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlGroup7 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem18 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem19 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem20 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem21 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem23 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem22 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlGroup10 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem4 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.cbTradeTypePosition = new DevExpress.XtraEditors.ComboBoxEdit();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dePosition.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dePosition.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcPosition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvPosition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartPosition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series19)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(pieSeriesLabel3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(pieSeriesView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deProfit.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deProfit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbTradeTypeProfit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chbDay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chbWeek.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chbMonth.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chbYear.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).BeginInit();
            this.layoutControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartProfitTrend)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(secondaryAxisY3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series20)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series21)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(pointSeriesLabel5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(splineSeriesView11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series22)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(pointSeriesLabel6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(splineSeriesView12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series23)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(splineSeriesView13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series24)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(splineSeriesView14)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series25)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(splineSeriesView15)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartGain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series26)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartLoss)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series27)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem15)).BeginInit();
            this.xtraTabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgInvestor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem26)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.esiInvestor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem27)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem28)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem29)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem30)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem31)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem32)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem33)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem24)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem18)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem19)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem20)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem21)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem23)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem22)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbTradeTypePosition.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowOnly;
            this.layoutControl1.Controls.Add(this.dePosition);
            this.layoutControl1.Controls.Add(this.gcPosition);
            this.layoutControl1.Controls.Add(this.chartPosition);
            this.layoutControl1.Controls.Add(this.lblYearR);
            this.layoutControl1.Controls.Add(this.deProfit);
            this.layoutControl1.Controls.Add(this.cbTradeTypeProfit);
            this.layoutControl1.Controls.Add(this.chbDay);
            this.layoutControl1.Controls.Add(this.chbWeek);
            this.layoutControl1.Controls.Add(this.chbMonth);
            this.layoutControl1.Controls.Add(this.chbYear);
            this.layoutControl1.Controls.Add(this.xtraTabControl1);
            this.layoutControl1.Controls.Add(this.lblCurValue);
            this.layoutControl1.Controls.Add(this.lblDayP);
            this.layoutControl1.Controls.Add(this.lblDayR);
            this.layoutControl1.Controls.Add(this.lblWeekP);
            this.layoutControl1.Controls.Add(this.lblWeekR);
            this.layoutControl1.Controls.Add(this.lblMonthP);
            this.layoutControl1.Controls.Add(this.lblMonthR);
            this.layoutControl1.Controls.Add(this.lblYearP);
            this.layoutControl1.Controls.Add(this.cbTradeTypePosition);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(1533, 902);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // dePosition
            // 
            this.dePosition.EditValue = null;
            this.dePosition.Location = new System.Drawing.Point(75, 148);
            this.dePosition.Name = "dePosition";
            this.dePosition.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dePosition.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dePosition.Size = new System.Drawing.Size(125, 20);
            this.dePosition.StyleController = this.layoutControl1;
            this.dePosition.TabIndex = 33;
            this.dePosition.EditValueChanged += new System.EventHandler(this.dePosition_EditValueChanged);
            // 
            // gcPosition
            // 
            this.gcPosition.Location = new System.Drawing.Point(543, 189);
            this.gcPosition.MainView = this.gvPosition;
            this.gcPosition.Name = "gcPosition";
            this.gcPosition.Size = new System.Drawing.Size(966, 260);
            this.gcPosition.TabIndex = 31;
            this.gcPosition.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvPosition});
            // 
            // gvPosition
            // 
            this.gvPosition.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colStockCode,
            this.colStockName,
            this.colTradeType,
            this.colTradeTypeName,
            this.colPreVolume,
            this.colPreValue,
            this.colCurVolume,
            this.colCurValue,
            this.colBuyVolume,
            this.colBuyValue,
            this.colSellVolume,
            this.colSellValue});
            this.gvPosition.GridControl = this.gcPosition;
            this.gvPosition.Name = "gvPosition";
            this.gvPosition.OptionsView.ColumnAutoWidth = false;
            this.gvPosition.OptionsView.ShowGroupPanel = false;
            // 
            // colStockCode
            // 
            this.colStockCode.Caption = "证券代码";
            this.colStockCode.FieldName = "StockCode";
            this.colStockCode.Name = "colStockCode";
            this.colStockCode.Visible = true;
            this.colStockCode.VisibleIndex = 0;
            // 
            // colStockName
            // 
            this.colStockName.Caption = "证券名称";
            this.colStockName.FieldName = "StockName";
            this.colStockName.Name = "colStockName";
            this.colStockName.Visible = true;
            this.colStockName.VisibleIndex = 1;
            this.colStockName.Width = 70;
            // 
            // colTradeType
            // 
            this.colTradeType.FieldName = "TradeType";
            this.colTradeType.Name = "colTradeType";
            // 
            // colTradeTypeName
            // 
            this.colTradeTypeName.AppearanceCell.Options.UseTextOptions = true;
            this.colTradeTypeName.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colTradeTypeName.AppearanceHeader.Options.UseTextOptions = true;
            this.colTradeTypeName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colTradeTypeName.Caption = "交易类别";
            this.colTradeTypeName.FieldName = "TradeTypeName";
            this.colTradeTypeName.Name = "colTradeTypeName";
            this.colTradeTypeName.Visible = true;
            this.colTradeTypeName.VisibleIndex = 2;
            this.colTradeTypeName.Width = 60;
            // 
            // colPreVolume
            // 
            this.colPreVolume.AppearanceCell.Options.UseTextOptions = true;
            this.colPreVolume.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colPreVolume.Caption = "昨日持仓数量";
            this.colPreVolume.DisplayFormat.FormatString = "N0";
            this.colPreVolume.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.colPreVolume.FieldName = "PreVolume";
            this.colPreVolume.Name = "colPreVolume";
            this.colPreVolume.Visible = true;
            this.colPreVolume.VisibleIndex = 3;
            this.colPreVolume.Width = 90;
            // 
            // colPreValue
            // 
            this.colPreValue.AppearanceCell.Options.UseTextOptions = true;
            this.colPreValue.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colPreValue.Caption = "昨日持仓金额";
            this.colPreValue.DisplayFormat.FormatString = "N2";
            this.colPreValue.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.colPreValue.FieldName = "PreValue";
            this.colPreValue.Name = "colPreValue";
            this.colPreValue.Visible = true;
            this.colPreValue.VisibleIndex = 4;
            this.colPreValue.Width = 90;
            // 
            // colCurVolume
            // 
            this.colCurVolume.AppearanceCell.Options.UseTextOptions = true;
            this.colCurVolume.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colCurVolume.Caption = "当日持仓数量";
            this.colCurVolume.DisplayFormat.FormatString = "N0";
            this.colCurVolume.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.colCurVolume.FieldName = "CurVolume";
            this.colCurVolume.Name = "colCurVolume";
            this.colCurVolume.Visible = true;
            this.colCurVolume.VisibleIndex = 5;
            this.colCurVolume.Width = 90;
            // 
            // colCurValue
            // 
            this.colCurValue.AppearanceCell.Options.UseTextOptions = true;
            this.colCurValue.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colCurValue.Caption = "当日持仓金额";
            this.colCurValue.DisplayFormat.FormatString = "N2";
            this.colCurValue.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.colCurValue.FieldName = "CurValue";
            this.colCurValue.Name = "colCurValue";
            this.colCurValue.Visible = true;
            this.colCurValue.VisibleIndex = 6;
            this.colCurValue.Width = 90;
            // 
            // colBuyVolume
            // 
            this.colBuyVolume.AppearanceCell.Options.UseTextOptions = true;
            this.colBuyVolume.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colBuyVolume.Caption = "买入数量";
            this.colBuyVolume.DisplayFormat.FormatString = "N0";
            this.colBuyVolume.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.colBuyVolume.FieldName = "BuyVolume";
            this.colBuyVolume.Name = "colBuyVolume";
            this.colBuyVolume.Visible = true;
            this.colBuyVolume.VisibleIndex = 7;
            this.colBuyVolume.Width = 90;
            // 
            // colBuyValue
            // 
            this.colBuyValue.AppearanceCell.Options.UseTextOptions = true;
            this.colBuyValue.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colBuyValue.Caption = "买入金额";
            this.colBuyValue.DisplayFormat.FormatString = "N2";
            this.colBuyValue.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.colBuyValue.FieldName = "BuyValue";
            this.colBuyValue.Name = "colBuyValue";
            this.colBuyValue.Visible = true;
            this.colBuyValue.VisibleIndex = 8;
            this.colBuyValue.Width = 90;
            // 
            // colSellVolume
            // 
            this.colSellVolume.AppearanceCell.Options.UseTextOptions = true;
            this.colSellVolume.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colSellVolume.Caption = "卖出数量";
            this.colSellVolume.DisplayFormat.FormatString = "N0";
            this.colSellVolume.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.colSellVolume.FieldName = "SellVolume";
            this.colSellVolume.Name = "colSellVolume";
            this.colSellVolume.Visible = true;
            this.colSellVolume.VisibleIndex = 9;
            this.colSellVolume.Width = 90;
            // 
            // colSellValue
            // 
            this.colSellValue.AppearanceCell.Options.UseTextOptions = true;
            this.colSellValue.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colSellValue.Caption = "卖出金额";
            this.colSellValue.DisplayFormat.FormatString = "N2";
            this.colSellValue.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.colSellValue.FieldName = "SellValue";
            this.colSellValue.Name = "colSellValue";
            this.colSellValue.Visible = true;
            this.colSellValue.VisibleIndex = 10;
            this.colSellValue.Width = 90;
            // 
            // chartPosition
            // 
            this.chartPosition.AppearanceNameSerializable = "Nature Colors";
            this.chartPosition.BorderOptions.Visibility = DevExpress.Utils.DefaultBoolean.True;
            this.chartPosition.Legend.AlignmentHorizontal = DevExpress.XtraCharts.LegendAlignmentHorizontal.LeftOutside;
            this.chartPosition.Legend.AlignmentVertical = DevExpress.XtraCharts.LegendAlignmentVertical.Bottom;
            this.chartPosition.Legend.Border.Visibility = DevExpress.Utils.DefaultBoolean.False;
            this.chartPosition.Location = new System.Drawing.Point(24, 189);
            this.chartPosition.Name = "chartPosition";
            series19.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
            series19.CrosshairLabelVisibility = DevExpress.Utils.DefaultBoolean.True;
            pieSeriesLabel3.Border.Visibility = DevExpress.Utils.DefaultBoolean.False;
            pieSeriesLabel3.LineLength = 5;
            pieSeriesLabel3.TextPattern = "{A}: {VP:0.00%}";
            series19.Label = pieSeriesLabel3;
            series19.LegendTextPattern = "{A}: {V:n2} {VP:0.00%}";
            series19.Name = "Series 1";
            series19.View = pieSeriesView3;
            this.chartPosition.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series19};
            this.chartPosition.Size = new System.Drawing.Size(515, 260);
            this.chartPosition.TabIndex = 30;
            chartTitle3.Alignment = System.Drawing.StringAlignment.Near;
            chartTitle3.Font = new System.Drawing.Font("Tahoma", 12F);
            chartTitle3.Indent = 4;
            chartTitle3.Text = "个股持仓\r\n";
            chartTitle3.Visibility = DevExpress.Utils.DefaultBoolean.False;
            this.chartPosition.Titles.AddRange(new DevExpress.XtraCharts.ChartTitle[] {
            chartTitle3});
            // 
            // lblYearR
            // 
            this.lblYearR.Location = new System.Drawing.Point(863, 73);
            this.lblYearR.Name = "lblYearR";
            this.lblYearR.Size = new System.Drawing.Size(70, 14);
            this.lblYearR.StyleController = this.layoutControl1;
            this.lblYearR.TabIndex = 29;
            this.lblYearR.Text = "labelControl1";
            // 
            // deProfit
            // 
            this.deProfit.EditValue = null;
            this.deProfit.Location = new System.Drawing.Point(75, 498);
            this.deProfit.Name = "deProfit";
            this.deProfit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deProfit.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deProfit.Size = new System.Drawing.Size(125, 20);
            this.deProfit.StyleController = this.layoutControl1;
            this.deProfit.TabIndex = 28;
            this.deProfit.EditValueChanged += new System.EventHandler(this.deProfit_EditValueChanged);
            // 
            // cbTradeTypeProfit
            // 
            this.cbTradeTypeProfit.Location = new System.Drawing.Point(255, 498);
            this.cbTradeTypeProfit.Name = "cbTradeTypeProfit";
            this.cbTradeTypeProfit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbTradeTypeProfit.Size = new System.Drawing.Size(95, 20);
            this.cbTradeTypeProfit.StyleController = this.layoutControl1;
            this.cbTradeTypeProfit.TabIndex = 27;
            this.cbTradeTypeProfit.SelectedIndexChanged += new System.EventHandler(this.cbTradeTypeProfit_SelectedIndexChanged);
            this.cbTradeTypeProfit.EditValueChanged += new System.EventHandler(this.cbTradeTypePosition_EditValueChanged);
            // 
            // chbDay
            // 
            this.chbDay.EditValue = true;
            this.chbDay.Location = new System.Drawing.Point(394, 498);
            this.chbDay.Name = "chbDay";
            this.chbDay.Properties.Caption = "日";
            this.chbDay.Size = new System.Drawing.Size(34, 19);
            this.chbDay.StyleController = this.layoutControl1;
            this.chbDay.TabIndex = 26;
            this.chbDay.CheckedChanged += new System.EventHandler(this.chbDay_CheckedChanged);
            // 
            // chbWeek
            // 
            this.chbWeek.Location = new System.Drawing.Point(432, 498);
            this.chbWeek.Name = "chbWeek";
            this.chbWeek.Properties.Caption = "周";
            this.chbWeek.Size = new System.Drawing.Size(34, 19);
            this.chbWeek.StyleController = this.layoutControl1;
            this.chbWeek.TabIndex = 25;
            this.chbWeek.CheckedChanged += new System.EventHandler(this.chbWeek_CheckedChanged);
            // 
            // chbMonth
            // 
            this.chbMonth.Location = new System.Drawing.Point(470, 498);
            this.chbMonth.Name = "chbMonth";
            this.chbMonth.Properties.Caption = "月";
            this.chbMonth.Size = new System.Drawing.Size(34, 19);
            this.chbMonth.StyleController = this.layoutControl1;
            this.chbMonth.TabIndex = 24;
            this.chbMonth.CheckedChanged += new System.EventHandler(this.chbMonth_CheckedChanged);
            // 
            // chbYear
            // 
            this.chbYear.Location = new System.Drawing.Point(508, 498);
            this.chbYear.Name = "chbYear";
            this.chbYear.Properties.Caption = "年";
            this.chbYear.Size = new System.Drawing.Size(34, 19);
            this.chbYear.StyleController = this.layoutControl1;
            this.chbYear.TabIndex = 23;
            this.chbYear.CheckedChanged += new System.EventHandler(this.chbYear_CheckedChanged);
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Location = new System.Drawing.Point(24, 522);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPage1;
            this.xtraTabControl1.Size = new System.Drawing.Size(1485, 321);
            this.xtraTabControl1.TabIndex = 19;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage2});
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.layoutControl2);
            this.xtraTabPage1.Image = ((System.Drawing.Image)(resources.GetObject("xtraTabPage1.Image")));
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(1479, 290);
            this.xtraTabPage1.Text = "统计图";
            // 
            // layoutControl2
            // 
            this.layoutControl2.Controls.Add(this.chartProfitTrend);
            this.layoutControl2.Controls.Add(this.chartGain);
            this.layoutControl2.Controls.Add(this.chartLoss);
            this.layoutControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl2.Location = new System.Drawing.Point(0, 0);
            this.layoutControl2.Name = "layoutControl2";
            this.layoutControl2.Root = this.layoutControlGroup8;
            this.layoutControl2.Size = new System.Drawing.Size(1479, 290);
            this.layoutControl2.TabIndex = 0;
            this.layoutControl2.Text = "layoutControl2";
            // 
            // chartProfitTrend
            // 
            xyDiagram7.AxisX.Label.Angle = 45;
            xyDiagram7.AxisX.Tickmarks.MinorVisible = false;
            xyDiagram7.AxisX.VisibleInPanesSerializable = "-1";
            constantLine5.AxisValueSerializable = "0";
            constantLine5.Color = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(204)))), ((int)(((byte)(228)))));
            constantLine5.Name = "Constant Line 1";
            constantLine5.ShowBehind = true;
            constantLine5.ShowInLegend = false;
            constantLine5.Title.Visible = false;
            xyDiagram7.AxisY.ConstantLines.AddRange(new DevExpress.XtraCharts.ConstantLine[] {
            constantLine5});
            xyDiagram7.AxisY.GridLines.LineStyle.DashStyle = DevExpress.XtraCharts.DashStyle.DashDot;
            xyDiagram7.AxisY.GridLines.Visible = false;
            xyDiagram7.AxisY.Tickmarks.MinorVisible = false;
            xyDiagram7.AxisY.Title.Text = "收益额";
            xyDiagram7.AxisY.Title.TextColor = System.Drawing.Color.DarkBlue;
            xyDiagram7.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
            xyDiagram7.AxisY.VisibleInPanesSerializable = "-1";
            secondaryAxisY3.AxisID = 0;
            constantLine6.AxisValueSerializable = "0";
            constantLine6.Color = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(234)))), ((int)(((byte)(218)))));
            constantLine6.Name = "Constant Line 1";
            constantLine6.ShowInLegend = false;
            constantLine6.Title.Text = "基准线";
            constantLine6.Title.Visible = false;
            secondaryAxisY3.ConstantLines.AddRange(new DevExpress.XtraCharts.ConstantLine[] {
            constantLine6});
            secondaryAxisY3.Label.TextPattern = "{V:0.0%}";
            secondaryAxisY3.Name = "Secondary AxisY 1";
            secondaryAxisY3.Tickmarks.MinorVisible = false;
            secondaryAxisY3.Title.Text = "收益率";
            secondaryAxisY3.Title.TextColor = System.Drawing.Color.OrangeRed;
            secondaryAxisY3.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
            secondaryAxisY3.VisibleInPanesSerializable = "-1";
            xyDiagram7.SecondaryAxesY.AddRange(new DevExpress.XtraCharts.SecondaryAxisY[] {
            secondaryAxisY3});
            this.chartProfitTrend.Diagram = xyDiagram7;
            this.chartProfitTrend.Location = new System.Drawing.Point(610, 29);
            this.chartProfitTrend.Name = "chartProfitTrend";
            series20.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
            series20.CrosshairLabelPattern = "{S}: {V:n2}";
            series20.Name = "投入资金(万元)";
            sideBySideBarSeriesView7.BarWidth = 0.2D;
            sideBySideBarSeriesView7.Border.Color = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(204)))), ((int)(((byte)(228)))));
            sideBySideBarSeriesView7.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            series20.View = sideBySideBarSeriesView7;
            series21.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
            series21.CrosshairLabelPattern = "{S}: {V:n2}";
            series21.CrosshairLabelVisibility = DevExpress.Utils.DefaultBoolean.True;
            pointSeriesLabel5.TextPattern = "{S}: {V:n2}";
            series21.Label = pointSeriesLabel5;
            series21.Name = "收益额(万元)";
            splineSeriesView11.Color = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(172)))), ((int)(((byte)(198)))));
            splineSeriesView11.LineMarkerOptions.Color = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(172)))), ((int)(((byte)(198)))));
            splineSeriesView11.LineMarkerOptions.Kind = DevExpress.XtraCharts.MarkerKind.Triangle;
            splineSeriesView11.LineMarkerOptions.Size = 7;
            splineSeriesView11.LineStyle.Thickness = 1;
            splineSeriesView11.MarkerVisibility = DevExpress.Utils.DefaultBoolean.True;
            series21.View = splineSeriesView11;
            series22.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
            series22.CrosshairLabelPattern = "{S}: {V:0.00%}";
            series22.CrosshairLabelVisibility = DevExpress.Utils.DefaultBoolean.True;
            pointSeriesLabel6.TextPattern = "{V:0.00%}";
            series22.Label = pointSeriesLabel6;
            series22.Name = "收益率";
            splineSeriesView12.AxisYName = "Secondary AxisY 1";
            splineSeriesView12.Color = System.Drawing.Color.OrangeRed;
            splineSeriesView12.LineMarkerOptions.Size = 5;
            splineSeriesView12.LineStyle.Thickness = 1;
            splineSeriesView12.MarkerVisibility = DevExpress.Utils.DefaultBoolean.True;
            series22.View = splineSeriesView12;
            series23.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
            series23.CrosshairLabelPattern = "{S}: {V:n2}";
            series23.CrosshairLabelVisibility = DevExpress.Utils.DefaultBoolean.True;
            series23.Name = "年收益额(万元)";
            splineSeriesView13.Color = System.Drawing.Color.SteelBlue;
            splineSeriesView13.LineMarkerOptions.Kind = DevExpress.XtraCharts.MarkerKind.Triangle;
            splineSeriesView13.LineMarkerOptions.Size = 8;
            series23.View = splineSeriesView13;
            series24.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
            series24.CrosshairLabelPattern = "{S}: {V:0.00%}";
            series24.CrosshairLabelVisibility = DevExpress.Utils.DefaultBoolean.True;
            series24.Name = "年收益率";
            splineSeriesView14.AxisYName = "Secondary AxisY 1";
            splineSeriesView14.Color = System.Drawing.Color.Coral;
            splineSeriesView14.LineMarkerOptions.Size = 8;
            series24.View = splineSeriesView14;
            series25.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
            series25.CrosshairLabelPattern = "{S}: {V:n2}";
            series25.CrosshairLabelVisibility = DevExpress.Utils.DefaultBoolean.True;
            series25.Name = "日均投入资金(十万元)";
            splineSeriesView15.Color = System.Drawing.Color.DodgerBlue;
            splineSeriesView15.LineStyle.DashStyle = DevExpress.XtraCharts.DashStyle.Dash;
            splineSeriesView15.LineStyle.Thickness = 1;
            series25.View = splineSeriesView15;
            this.chartProfitTrend.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series20,
        series21,
        series22,
        series23,
        series24,
        series25};
            this.chartProfitTrend.Size = new System.Drawing.Size(857, 249);
            this.chartProfitTrend.TabIndex = 6;
            // 
            // chartGain
            // 
            this.chartGain.BorderOptions.Visibility = DevExpress.Utils.DefaultBoolean.True;
            xyDiagram8.AxisX.Color = System.Drawing.Color.White;
            xyDiagram8.AxisX.Label.TextColor = System.Drawing.Color.Black;
            xyDiagram8.AxisX.Tickmarks.MinorVisible = false;
            xyDiagram8.AxisX.Tickmarks.Visible = false;
            xyDiagram8.AxisX.VisibleInPanesSerializable = "-1";
            xyDiagram8.AxisY.Alignment = DevExpress.XtraCharts.AxisAlignment.Far;
            xyDiagram8.AxisY.GridLines.Visible = false;
            xyDiagram8.AxisY.Visibility = DevExpress.Utils.DefaultBoolean.False;
            xyDiagram8.AxisY.VisibleInPanesSerializable = "-1";
            xyDiagram8.DefaultPane.BorderVisible = false;
            xyDiagram8.Rotated = true;
            this.chartGain.Diagram = xyDiagram8;
            this.chartGain.Legend.AlignmentHorizontal = DevExpress.XtraCharts.LegendAlignmentHorizontal.Left;
            this.chartGain.Legend.AlignmentVertical = DevExpress.XtraCharts.LegendAlignmentVertical.TopOutside;
            this.chartGain.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
            this.chartGain.Location = new System.Drawing.Point(311, 29);
            this.chartGain.Name = "chartGain";
            series26.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
            series26.CrosshairEnabled = DevExpress.Utils.DefaultBoolean.False;
            sideBySideBarSeriesLabel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            sideBySideBarSeriesLabel5.Border.Visibility = DevExpress.Utils.DefaultBoolean.False;
            sideBySideBarSeriesLabel5.FillStyle.FillMode = DevExpress.XtraCharts.FillMode.Empty;
            sideBySideBarSeriesLabel5.LineVisibility = DevExpress.Utils.DefaultBoolean.False;
            sideBySideBarSeriesLabel5.Position = DevExpress.XtraCharts.BarSeriesLabelPosition.Top;
            sideBySideBarSeriesLabel5.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(80)))), ((int)(((byte)(77)))));
            sideBySideBarSeriesLabel5.TextPattern = "{V:N0}";
            series26.Label = sideBySideBarSeriesLabel5;
            series26.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            series26.Name = "Series 1";
            series26.SeriesPointsSorting = DevExpress.XtraCharts.SortingMode.Ascending;
            series26.SeriesPointsSortingKey = DevExpress.XtraCharts.SeriesPointKey.Value_1;
            sideBySideBarSeriesView8.BarWidth = 0.5D;
            sideBySideBarSeriesView8.Color = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(80)))), ((int)(((byte)(77)))));
            series26.View = sideBySideBarSeriesView8;
            this.chartGain.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series26};
            this.chartGain.Size = new System.Drawing.Size(295, 249);
            this.chartGain.TabIndex = 5;
            // 
            // chartLoss
            // 
            this.chartLoss.BorderOptions.Visibility = DevExpress.Utils.DefaultBoolean.True;
            xyDiagram9.AxisX.Alignment = DevExpress.XtraCharts.AxisAlignment.Far;
            xyDiagram9.AxisX.Color = System.Drawing.Color.White;
            xyDiagram9.AxisX.Label.TextColor = System.Drawing.Color.Black;
            xyDiagram9.AxisX.Tickmarks.MinorVisible = false;
            xyDiagram9.AxisX.Tickmarks.Visible = false;
            xyDiagram9.AxisX.VisibleInPanesSerializable = "-1";
            xyDiagram9.AxisY.Alignment = DevExpress.XtraCharts.AxisAlignment.Far;
            xyDiagram9.AxisY.GridLines.Visible = false;
            xyDiagram9.AxisY.Visibility = DevExpress.Utils.DefaultBoolean.False;
            xyDiagram9.AxisY.VisibleInPanesSerializable = "-1";
            xyDiagram9.DefaultPane.BorderVisible = false;
            xyDiagram9.EnableAxisXScrolling = true;
            xyDiagram9.Rotated = true;
            this.chartLoss.Diagram = xyDiagram9;
            this.chartLoss.Legend.AlignmentHorizontal = DevExpress.XtraCharts.LegendAlignmentHorizontal.Right;
            this.chartLoss.Legend.AlignmentVertical = DevExpress.XtraCharts.LegendAlignmentVertical.TopOutside;
            this.chartLoss.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
            this.chartLoss.Location = new System.Drawing.Point(12, 29);
            this.chartLoss.Name = "chartLoss";
            series27.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
            series27.CrosshairEnabled = DevExpress.Utils.DefaultBoolean.False;
            sideBySideBarSeriesLabel6.BackColor = System.Drawing.Color.White;
            sideBySideBarSeriesLabel6.Border.Visibility = DevExpress.Utils.DefaultBoolean.False;
            sideBySideBarSeriesLabel6.FillStyle.FillMode = DevExpress.XtraCharts.FillMode.Empty;
            sideBySideBarSeriesLabel6.LineVisibility = DevExpress.Utils.DefaultBoolean.False;
            sideBySideBarSeriesLabel6.Position = DevExpress.XtraCharts.BarSeriesLabelPosition.Top;
            sideBySideBarSeriesLabel6.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(187)))), ((int)(((byte)(89)))));
            sideBySideBarSeriesLabel6.TextPattern = "{V:N0}";
            series27.Label = sideBySideBarSeriesLabel6;
            series27.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            series27.Name = "Series 1";
            series27.SeriesPointsSorting = DevExpress.XtraCharts.SortingMode.Descending;
            series27.SeriesPointsSortingKey = DevExpress.XtraCharts.SeriesPointKey.Value_1;
            sideBySideBarSeriesView9.BarWidth = 0.5D;
            sideBySideBarSeriesView9.Color = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(187)))), ((int)(((byte)(89)))));
            series27.View = sideBySideBarSeriesView9;
            this.chartLoss.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series27};
            this.chartLoss.Size = new System.Drawing.Size(295, 249);
            this.chartLoss.TabIndex = 4;
            // 
            // layoutControlGroup8
            // 
            this.layoutControlGroup8.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup8.GroupBordersVisible = false;
            this.layoutControlGroup8.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem11,
            this.layoutControlItem13,
            this.layoutControlItem15});
            this.layoutControlGroup8.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup8.Name = "layoutControlGroup8";
            this.layoutControlGroup8.Size = new System.Drawing.Size(1479, 290);
            this.layoutControlGroup8.TextVisible = false;
            // 
            // layoutControlItem11
            // 
            this.layoutControlItem11.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.layoutControlItem11.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem11.AppearanceItemCaption.Options.UseTextOptions = true;
            this.layoutControlItem11.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.layoutControlItem11.Control = this.chartLoss;
            this.layoutControlItem11.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem11.Name = "layoutControlItem11";
            this.layoutControlItem11.Size = new System.Drawing.Size(299, 270);
            this.layoutControlItem11.Text = "亏损股票";
            this.layoutControlItem11.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem11.TextSize = new System.Drawing.Size(65, 14);
            // 
            // layoutControlItem13
            // 
            this.layoutControlItem13.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.layoutControlItem13.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem13.AppearanceItemCaption.Options.UseTextOptions = true;
            this.layoutControlItem13.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.layoutControlItem13.Control = this.chartGain;
            this.layoutControlItem13.Location = new System.Drawing.Point(299, 0);
            this.layoutControlItem13.Name = "layoutControlItem13";
            this.layoutControlItem13.Size = new System.Drawing.Size(299, 270);
            this.layoutControlItem13.Text = "盈利股票";
            this.layoutControlItem13.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem13.TextSize = new System.Drawing.Size(65, 14);
            // 
            // layoutControlItem15
            // 
            this.layoutControlItem15.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.layoutControlItem15.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem15.AppearanceItemCaption.Options.UseTextOptions = true;
            this.layoutControlItem15.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.layoutControlItem15.Control = this.chartProfitTrend;
            this.layoutControlItem15.Location = new System.Drawing.Point(598, 0);
            this.layoutControlItem15.Name = "layoutControlItem15";
            this.layoutControlItem15.Size = new System.Drawing.Size(861, 270);
            this.layoutControlItem15.Text = "收益趋势图";
            this.layoutControlItem15.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem15.TextSize = new System.Drawing.Size(65, 14);
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Controls.Add(this.layoutControl3);
            this.xtraTabPage2.Image = ((System.Drawing.Image)(resources.GetObject("xtraTabPage2.Image")));
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Size = new System.Drawing.Size(1479, 303);
            this.xtraTabPage2.Text = "列表";
            // 
            // layoutControl3
            // 
            this.layoutControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl3.Location = new System.Drawing.Point(0, 0);
            this.layoutControl3.Name = "layoutControl3";
            this.layoutControl3.Root = this.layoutControlGroup9;
            this.layoutControl3.Size = new System.Drawing.Size(1479, 303);
            this.layoutControl3.TabIndex = 0;
            this.layoutControl3.Text = "layoutControl3";
            // 
            // layoutControlGroup9
            // 
            this.layoutControlGroup9.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup9.GroupBordersVisible = false;
            this.layoutControlGroup9.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup9.Name = "layoutControlGroup9";
            this.layoutControlGroup9.Size = new System.Drawing.Size(1479, 303);
            this.layoutControlGroup9.TextVisible = false;
            // 
            // lblCurValue
            // 
            this.lblCurValue.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblCurValue.Location = new System.Drawing.Point(123, 73);
            this.lblCurValue.Name = "lblCurValue";
            this.lblCurValue.Size = new System.Drawing.Size(82, 26);
            this.lblCurValue.StyleController = this.layoutControl1;
            this.lblCurValue.TabIndex = 29;
            this.lblCurValue.Text = "labelControl1";
            // 
            // lblDayP
            // 
            this.lblDayP.Location = new System.Drawing.Point(308, 55);
            this.lblDayP.Name = "lblDayP";
            this.lblDayP.Size = new System.Drawing.Size(70, 14);
            this.lblDayP.StyleController = this.layoutControl1;
            this.lblDayP.TabIndex = 29;
            this.lblDayP.Text = "labelControl1";
            // 
            // lblDayR
            // 
            this.lblDayR.Location = new System.Drawing.Point(308, 73);
            this.lblDayR.Name = "lblDayR";
            this.lblDayR.Size = new System.Drawing.Size(70, 14);
            this.lblDayR.StyleController = this.layoutControl1;
            this.lblDayR.TabIndex = 29;
            this.lblDayR.Text = "labelControl1";
            // 
            // lblWeekP
            // 
            this.lblWeekP.Location = new System.Drawing.Point(493, 55);
            this.lblWeekP.Name = "lblWeekP";
            this.lblWeekP.Size = new System.Drawing.Size(70, 14);
            this.lblWeekP.StyleController = this.layoutControl1;
            this.lblWeekP.TabIndex = 29;
            this.lblWeekP.Text = "labelControl1";
            // 
            // lblWeekR
            // 
            this.lblWeekR.Location = new System.Drawing.Point(493, 73);
            this.lblWeekR.Name = "lblWeekR";
            this.lblWeekR.Size = new System.Drawing.Size(70, 14);
            this.lblWeekR.StyleController = this.layoutControl1;
            this.lblWeekR.TabIndex = 29;
            this.lblWeekR.Text = "labelControl1";
            // 
            // lblMonthP
            // 
            this.lblMonthP.Location = new System.Drawing.Point(678, 55);
            this.lblMonthP.Name = "lblMonthP";
            this.lblMonthP.Size = new System.Drawing.Size(70, 14);
            this.lblMonthP.StyleController = this.layoutControl1;
            this.lblMonthP.TabIndex = 29;
            this.lblMonthP.Text = "labelControl1";
            // 
            // lblMonthR
            // 
            this.lblMonthR.Location = new System.Drawing.Point(678, 73);
            this.lblMonthR.Name = "lblMonthR";
            this.lblMonthR.Size = new System.Drawing.Size(70, 14);
            this.lblMonthR.StyleController = this.layoutControl1;
            this.lblMonthR.TabIndex = 29;
            this.lblMonthR.Text = "labelControl1";
            // 
            // lblYearP
            // 
            this.lblYearP.Location = new System.Drawing.Point(863, 55);
            this.lblYearP.Name = "lblYearP";
            this.lblYearP.Size = new System.Drawing.Size(70, 14);
            this.lblYearP.StyleController = this.layoutControl1;
            this.lblYearP.TabIndex = 29;
            this.lblYearP.Text = "labelControl1";
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcgInvestor,
            this.layoutControlGroup7,
            this.emptySpaceItem3,
            this.layoutControlGroup10});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(1533, 902);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // lcgInvestor
            // 
            this.lcgInvestor.AppearanceGroup.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lcgInvestor.AppearanceGroup.Options.UseFont = true;
            this.lcgInvestor.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem26,
            this.esiInvestor,
            this.layoutControlGroup4,
            this.layoutControlGroup2,
            this.layoutControlGroup5,
            this.layoutControlGroup3,
            this.emptySpaceItem5});
            this.lcgInvestor.Location = new System.Drawing.Point(0, 0);
            this.lcgInvestor.Name = "lcgInvestor";
            this.lcgInvestor.OptionsItemText.TextAlignMode = DevExpress.XtraLayout.TextAlignModeGroup.AlignWithChildren;
            this.lcgInvestor.Size = new System.Drawing.Size(1513, 103);
            this.lcgInvestor.Text = "投资人员";
            this.lcgInvestor.TextVisible = false;
            // 
            // layoutControlItem26
            // 
            this.layoutControlItem26.Control = this.lblCurValue;
            this.layoutControlItem26.CustomizationFormText = "layoutControlItem24";
            this.layoutControlItem26.Location = new System.Drawing.Point(0, 49);
            this.layoutControlItem26.MaxSize = new System.Drawing.Size(185, 30);
            this.layoutControlItem26.MinSize = new System.Drawing.Size(185, 30);
            this.layoutControlItem26.Name = "layoutControlItem26";
            this.layoutControlItem26.Size = new System.Drawing.Size(185, 30);
            this.layoutControlItem26.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem26.Text = "当前持仓（万元）";
            this.layoutControlItem26.TextSize = new System.Drawing.Size(96, 14);
            // 
            // esiInvestor
            // 
            this.esiInvestor.AllowHotTrack = false;
            this.esiInvestor.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.esiInvestor.AppearanceItemCaption.Options.UseFont = true;
            this.esiInvestor.AppearanceItemCaption.Options.UseTextOptions = true;
            this.esiInvestor.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.esiInvestor.Location = new System.Drawing.Point(0, 0);
            this.esiInvestor.Name = "esiInvestor";
            this.esiInvestor.Size = new System.Drawing.Size(185, 49);
            this.esiInvestor.TextSize = new System.Drawing.Size(96, 0);
            this.esiInvestor.TextVisible = true;
            // 
            // layoutControlGroup4
            // 
            this.layoutControlGroup4.AppearanceGroup.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.layoutControlGroup4.AppearanceGroup.Options.UseFont = true;
            this.layoutControlGroup4.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem27,
            this.layoutControlItem28});
            this.layoutControlGroup4.Location = new System.Drawing.Point(185, 0);
            this.layoutControlGroup4.Name = "layoutControlGroup4";
            this.layoutControlGroup4.OptionsItemText.TextAlignMode = DevExpress.XtraLayout.TextAlignModeGroup.AlignWithChildren;
            this.layoutControlGroup4.Size = new System.Drawing.Size(185, 79);
            this.layoutControlGroup4.Text = "日收益";
            // 
            // layoutControlItem27
            // 
            this.layoutControlItem27.Control = this.lblDayP;
            this.layoutControlItem27.CustomizationFormText = "layoutControlItem24";
            this.layoutControlItem27.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem27.Name = "layoutControlItem27";
            this.layoutControlItem27.Size = new System.Drawing.Size(161, 18);
            this.layoutControlItem27.Text = "收益额（万元）";
            this.layoutControlItem27.TextSize = new System.Drawing.Size(84, 14);
            // 
            // layoutControlItem28
            // 
            this.layoutControlItem28.Control = this.lblDayR;
            this.layoutControlItem28.CustomizationFormText = "layoutControlItem24";
            this.layoutControlItem28.Location = new System.Drawing.Point(0, 18);
            this.layoutControlItem28.Name = "layoutControlItem28";
            this.layoutControlItem28.Size = new System.Drawing.Size(161, 18);
            this.layoutControlItem28.Text = "收益率";
            this.layoutControlItem28.TextSize = new System.Drawing.Size(84, 14);
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.AppearanceGroup.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.layoutControlGroup2.AppearanceGroup.Options.UseFont = true;
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem29,
            this.layoutControlItem30});
            this.layoutControlGroup2.Location = new System.Drawing.Point(370, 0);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.OptionsItemText.TextAlignMode = DevExpress.XtraLayout.TextAlignModeGroup.AlignWithChildren;
            this.layoutControlGroup2.Size = new System.Drawing.Size(185, 79);
            this.layoutControlGroup2.Text = "周收益";
            // 
            // layoutControlItem29
            // 
            this.layoutControlItem29.Control = this.lblWeekP;
            this.layoutControlItem29.CustomizationFormText = "layoutControlItem24";
            this.layoutControlItem29.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem29.Name = "layoutControlItem29";
            this.layoutControlItem29.Size = new System.Drawing.Size(161, 18);
            this.layoutControlItem29.Text = "收益额（万元）";
            this.layoutControlItem29.TextSize = new System.Drawing.Size(84, 14);
            // 
            // layoutControlItem30
            // 
            this.layoutControlItem30.Control = this.lblWeekR;
            this.layoutControlItem30.CustomizationFormText = "layoutControlItem24";
            this.layoutControlItem30.Location = new System.Drawing.Point(0, 18);
            this.layoutControlItem30.Name = "layoutControlItem30";
            this.layoutControlItem30.Size = new System.Drawing.Size(161, 18);
            this.layoutControlItem30.Text = "收益率";
            this.layoutControlItem30.TextSize = new System.Drawing.Size(84, 14);
            // 
            // layoutControlGroup5
            // 
            this.layoutControlGroup5.AppearanceGroup.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.layoutControlGroup5.AppearanceGroup.Options.UseFont = true;
            this.layoutControlGroup5.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem31,
            this.layoutControlItem32});
            this.layoutControlGroup5.Location = new System.Drawing.Point(555, 0);
            this.layoutControlGroup5.Name = "layoutControlGroup5";
            this.layoutControlGroup5.OptionsItemText.TextAlignMode = DevExpress.XtraLayout.TextAlignModeGroup.AlignWithChildren;
            this.layoutControlGroup5.Size = new System.Drawing.Size(185, 79);
            this.layoutControlGroup5.Text = "月收益";
            // 
            // layoutControlItem31
            // 
            this.layoutControlItem31.Control = this.lblMonthP;
            this.layoutControlItem31.CustomizationFormText = "layoutControlItem24";
            this.layoutControlItem31.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem31.Name = "layoutControlItem31";
            this.layoutControlItem31.Size = new System.Drawing.Size(161, 18);
            this.layoutControlItem31.Text = "收益额（万元）";
            this.layoutControlItem31.TextSize = new System.Drawing.Size(84, 14);
            // 
            // layoutControlItem32
            // 
            this.layoutControlItem32.Control = this.lblMonthR;
            this.layoutControlItem32.CustomizationFormText = "layoutControlItem24";
            this.layoutControlItem32.Location = new System.Drawing.Point(0, 18);
            this.layoutControlItem32.Name = "layoutControlItem32";
            this.layoutControlItem32.Size = new System.Drawing.Size(161, 18);
            this.layoutControlItem32.Text = "收益率";
            this.layoutControlItem32.TextSize = new System.Drawing.Size(84, 14);
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.AppearanceGroup.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.layoutControlGroup3.AppearanceGroup.Options.UseFont = true;
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem33,
            this.layoutControlItem24});
            this.layoutControlGroup3.Location = new System.Drawing.Point(740, 0);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.OptionsItemText.TextAlignMode = DevExpress.XtraLayout.TextAlignModeGroup.AlignWithChildren;
            this.layoutControlGroup3.Size = new System.Drawing.Size(185, 79);
            this.layoutControlGroup3.Text = "年收益";
            // 
            // layoutControlItem33
            // 
            this.layoutControlItem33.Control = this.lblYearP;
            this.layoutControlItem33.CustomizationFormText = "layoutControlItem24";
            this.layoutControlItem33.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem33.Name = "layoutControlItem33";
            this.layoutControlItem33.Size = new System.Drawing.Size(161, 18);
            this.layoutControlItem33.Text = "收益额（万元）";
            this.layoutControlItem33.TextSize = new System.Drawing.Size(84, 14);
            // 
            // layoutControlItem24
            // 
            this.layoutControlItem24.Control = this.lblYearR;
            this.layoutControlItem24.Location = new System.Drawing.Point(0, 18);
            this.layoutControlItem24.Name = "layoutControlItem24";
            this.layoutControlItem24.Size = new System.Drawing.Size(161, 18);
            this.layoutControlItem24.Text = "收益率";
            this.layoutControlItem24.TextSize = new System.Drawing.Size(84, 14);
            // 
            // emptySpaceItem5
            // 
            this.emptySpaceItem5.AllowHotTrack = false;
            this.emptySpaceItem5.Location = new System.Drawing.Point(925, 0);
            this.emptySpaceItem5.Name = "emptySpaceItem5";
            this.emptySpaceItem5.Size = new System.Drawing.Size(564, 79);
            this.emptySpaceItem5.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlGroup7
            // 
            this.layoutControlGroup7.AppearanceGroup.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.layoutControlGroup7.AppearanceGroup.Options.UseFont = true;
            this.layoutControlGroup7.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem9,
            this.layoutControlItem18,
            this.layoutControlItem19,
            this.layoutControlItem20,
            this.layoutControlItem21,
            this.emptySpaceItem1,
            this.layoutControlItem23,
            this.layoutControlItem22,
            this.emptySpaceItem2});
            this.layoutControlGroup7.Location = new System.Drawing.Point(0, 453);
            this.layoutControlGroup7.Name = "layoutControlGroup7";
            this.layoutControlGroup7.OptionsItemText.TextAlignMode = DevExpress.XtraLayout.TextAlignModeGroup.AutoSize;
            this.layoutControlGroup7.Size = new System.Drawing.Size(1513, 394);
            this.layoutControlGroup7.Text = "收益";
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.xtraTabControl1;
            this.layoutControlItem9.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(1489, 325);
            this.layoutControlItem9.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem9.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem9.TextVisible = false;
            // 
            // layoutControlItem18
            // 
            this.layoutControlItem18.Control = this.chbYear;
            this.layoutControlItem18.Location = new System.Drawing.Point(484, 0);
            this.layoutControlItem18.Name = "layoutControlItem18";
            this.layoutControlItem18.Size = new System.Drawing.Size(38, 24);
            this.layoutControlItem18.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem18.TextVisible = false;
            // 
            // layoutControlItem19
            // 
            this.layoutControlItem19.Control = this.chbMonth;
            this.layoutControlItem19.Location = new System.Drawing.Point(446, 0);
            this.layoutControlItem19.Name = "layoutControlItem19";
            this.layoutControlItem19.Size = new System.Drawing.Size(38, 24);
            this.layoutControlItem19.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem19.TextVisible = false;
            // 
            // layoutControlItem20
            // 
            this.layoutControlItem20.Control = this.chbWeek;
            this.layoutControlItem20.Location = new System.Drawing.Point(408, 0);
            this.layoutControlItem20.Name = "layoutControlItem20";
            this.layoutControlItem20.Size = new System.Drawing.Size(38, 24);
            this.layoutControlItem20.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem20.TextVisible = false;
            // 
            // layoutControlItem21
            // 
            this.layoutControlItem21.Control = this.chbDay;
            this.layoutControlItem21.Location = new System.Drawing.Point(370, 0);
            this.layoutControlItem21.Name = "layoutControlItem21";
            this.layoutControlItem21.Size = new System.Drawing.Size(38, 24);
            this.layoutControlItem21.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem21.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(522, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(967, 24);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem23
            // 
            this.layoutControlItem23.Control = this.deProfit;
            this.layoutControlItem23.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem23.Name = "layoutControlItem23";
            this.layoutControlItem23.Size = new System.Drawing.Size(180, 24);
            this.layoutControlItem23.Text = "统计日期";
            this.layoutControlItem23.TextSize = new System.Drawing.Size(48, 14);
            // 
            // layoutControlItem22
            // 
            this.layoutControlItem22.Control = this.cbTradeTypeProfit;
            this.layoutControlItem22.Location = new System.Drawing.Point(180, 0);
            this.layoutControlItem22.Name = "layoutControlItem22";
            this.layoutControlItem22.Size = new System.Drawing.Size(150, 24);
            this.layoutControlItem22.Text = "交易类别";
            this.layoutControlItem22.TextSize = new System.Drawing.Size(48, 14);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(330, 0);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(40, 24);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            this.emptySpaceItem3.Location = new System.Drawing.Point(0, 847);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(1513, 35);
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlGroup10
            // 
            this.layoutControlGroup10.AppearanceGroup.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.layoutControlGroup10.AppearanceGroup.Options.UseFont = true;
            this.layoutControlGroup10.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.emptySpaceItem4,
            this.layoutControlItem4,
            this.layoutControlItem3});
            this.layoutControlGroup10.Location = new System.Drawing.Point(0, 103);
            this.layoutControlGroup10.Name = "layoutControlGroup10";
            this.layoutControlGroup10.OptionsItemText.TextAlignMode = DevExpress.XtraLayout.TextAlignModeGroup.AutoSize;
            this.layoutControlGroup10.Size = new System.Drawing.Size(1513, 350);
            this.layoutControlGroup10.Text = "持仓";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.layoutControlItem1.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem1.AppearanceItemCaption.Options.UseTextOptions = true;
            this.layoutControlItem1.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.layoutControlItem1.Control = this.chartPosition;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(519, 281);
            this.layoutControlItem1.Text = "持仓分布(金额：万元)";
            this.layoutControlItem1.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(127, 14);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.layoutControlItem2.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem2.AppearanceItemCaption.Options.UseTextOptions = true;
            this.layoutControlItem2.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.layoutControlItem2.Control = this.gcPosition;
            this.layoutControlItem2.Location = new System.Drawing.Point(519, 24);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(970, 281);
            this.layoutControlItem2.Text = "持仓变动(金额：万元)";
            this.layoutControlItem2.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(127, 14);
            // 
            // emptySpaceItem4
            // 
            this.emptySpaceItem4.AllowHotTrack = false;
            this.emptySpaceItem4.Location = new System.Drawing.Point(330, 0);
            this.emptySpaceItem4.Name = "emptySpaceItem4";
            this.emptySpaceItem4.Size = new System.Drawing.Size(1159, 24);
            this.emptySpaceItem4.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.dePosition;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(180, 24);
            this.layoutControlItem4.Text = "统计日期";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(48, 14);
            // 
            // cbTradeTypePosition
            // 
            this.cbTradeTypePosition.Location = new System.Drawing.Point(255, 148);
            this.cbTradeTypePosition.Name = "cbTradeTypePosition";
            this.cbTradeTypePosition.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbTradeTypePosition.Size = new System.Drawing.Size(95, 20);
            this.cbTradeTypePosition.StyleController = this.layoutControl1;
            this.cbTradeTypePosition.TabIndex = 27;
            this.cbTradeTypePosition.SelectedIndexChanged += new System.EventHandler(this.cbTradeTypePosition_SelectedIndexChanged);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.cbTradeTypePosition;
            this.layoutControlItem3.CustomizationFormText = "交易类别";
            this.layoutControlItem3.Location = new System.Drawing.Point(180, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(150, 24);
            this.layoutControlItem3.Text = "交易类别";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(48, 14);
            // 
            // FrmInvestorStudio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1533, 902);
            this.Controls.Add(this.layoutControl1);
            this.Name = "FrmInvestorStudio";
            this.Text = "FrmInvestorStudio";
            this.Load += new System.EventHandler(this.FrmInvestorStudio_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dePosition.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dePosition.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcPosition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvPosition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(pieSeriesLabel3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(pieSeriesView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series19)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartPosition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deProfit.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deProfit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbTradeTypeProfit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chbDay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chbWeek.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chbMonth.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chbYear.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).EndInit();
            this.layoutControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(secondaryAxisY3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series20)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(pointSeriesLabel5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(splineSeriesView11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series21)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(pointSeriesLabel6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(splineSeriesView12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series22)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(splineSeriesView13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series23)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(splineSeriesView14)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series24)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(splineSeriesView15)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series25)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartProfitTrend)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series26)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartGain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series27)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartLoss)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem15)).EndInit();
            this.xtraTabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgInvestor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem26)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.esiInvestor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem27)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem28)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem29)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem30)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem31)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem32)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem33)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem24)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem18)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem19)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem20)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem21)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem23)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem22)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbTradeTypePosition.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup4;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup5;
        private DevExpress.XtraLayout.LayoutControlGroup lcgInvestor;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem5;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        private DevExpress.XtraLayout.LayoutControl layoutControl2;
        private DevExpress.XtraCharts.ChartControl chartProfitTrend;
        private DevExpress.XtraCharts.ChartControl chartGain;
        private DevExpress.XtraCharts.ChartControl chartLoss;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup8;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem11;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem13;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem15;
        private DevExpress.XtraLayout.LayoutControl layoutControl3;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup9;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup7;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private DevExpress.XtraEditors.DateEdit deProfit;
        private DevExpress.XtraEditors.ComboBoxEdit cbTradeTypeProfit;
        private DevExpress.XtraEditors.CheckEdit chbDay;
        private DevExpress.XtraEditors.CheckEdit chbWeek;
        private DevExpress.XtraEditors.CheckEdit chbMonth;
        private DevExpress.XtraEditors.CheckEdit chbYear;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem18;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem19;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem20;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem21;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem22;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem23;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraEditors.LabelControl lblYearR;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem24;
        private DevExpress.XtraEditors.LabelControl lblCurValue;
        private DevExpress.XtraEditors.LabelControl lblDayP;
        private DevExpress.XtraEditors.LabelControl lblDayR;
        private DevExpress.XtraEditors.LabelControl lblWeekP;
        private DevExpress.XtraEditors.LabelControl lblWeekR;
        private DevExpress.XtraEditors.LabelControl lblMonthP;
        private DevExpress.XtraEditors.LabelControl lblMonthR;
        private DevExpress.XtraEditors.LabelControl lblYearP;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem29;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem30;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem33;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem27;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem28;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem31;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem32;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem26;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private DevExpress.XtraGrid.GridControl gcPosition;
        private DevExpress.XtraGrid.Views.Grid.GridView gvPosition;
        private DevExpress.XtraCharts.ChartControl chartPosition;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup10;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.DateEdit dePosition;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.EmptySpaceItem esiInvestor;
        private DevExpress.XtraGrid.Columns.GridColumn colStockCode;
        private DevExpress.XtraGrid.Columns.GridColumn colStockName;
        private DevExpress.XtraGrid.Columns.GridColumn colTradeType;
        private DevExpress.XtraGrid.Columns.GridColumn colTradeTypeName;
        private DevExpress.XtraGrid.Columns.GridColumn colPreVolume;
        private DevExpress.XtraGrid.Columns.GridColumn colPreValue;
        private DevExpress.XtraGrid.Columns.GridColumn colCurVolume;
        private DevExpress.XtraGrid.Columns.GridColumn colCurValue;
        private DevExpress.XtraGrid.Columns.GridColumn colBuyVolume;
        private DevExpress.XtraGrid.Columns.GridColumn colBuyValue;
        private DevExpress.XtraGrid.Columns.GridColumn colSellVolume;
        private DevExpress.XtraGrid.Columns.GridColumn colSellValue;
        private DevExpress.XtraEditors.ComboBoxEdit cbTradeTypePosition;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
    }
}