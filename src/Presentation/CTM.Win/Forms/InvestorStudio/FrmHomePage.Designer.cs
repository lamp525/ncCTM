namespace CTM.Win.Forms.InvestorStudio
{
    partial class FrmHomePage
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmHomePage));
            DevExpress.XtraCharts.Series series1 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.PieSeriesLabel pieSeriesLabel1 = new DevExpress.XtraCharts.PieSeriesLabel();
            DevExpress.XtraCharts.PieSeriesView pieSeriesView1 = new DevExpress.XtraCharts.PieSeriesView();
            DevExpress.XtraCharts.ChartTitle chartTitle1 = new DevExpress.XtraCharts.ChartTitle();
            DevExpress.XtraCharts.XYDiagram xyDiagram1 = new DevExpress.XtraCharts.XYDiagram();
            DevExpress.XtraCharts.ConstantLine constantLine1 = new DevExpress.XtraCharts.ConstantLine();
            DevExpress.XtraCharts.SecondaryAxisY secondaryAxisY1 = new DevExpress.XtraCharts.SecondaryAxisY();
            DevExpress.XtraCharts.ConstantLine constantLine2 = new DevExpress.XtraCharts.ConstantLine();
            DevExpress.XtraCharts.Series series2 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.SplineSeriesView splineSeriesView1 = new DevExpress.XtraCharts.SplineSeriesView();
            DevExpress.XtraCharts.Series series3 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.SplineSeriesView splineSeriesView2 = new DevExpress.XtraCharts.SplineSeriesView();
            DevExpress.XtraCharts.Series series4 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.SideBySideBarSeriesView sideBySideBarSeriesView1 = new DevExpress.XtraCharts.SideBySideBarSeriesView();
            DevExpress.XtraCharts.Series series5 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.PointSeriesLabel pointSeriesLabel1 = new DevExpress.XtraCharts.PointSeriesLabel();
            DevExpress.XtraCharts.SplineSeriesView splineSeriesView3 = new DevExpress.XtraCharts.SplineSeriesView();
            DevExpress.XtraCharts.Series series6 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.PointSeriesLabel pointSeriesLabel2 = new DevExpress.XtraCharts.PointSeriesLabel();
            DevExpress.XtraCharts.SplineSeriesView splineSeriesView4 = new DevExpress.XtraCharts.SplineSeriesView();
            DevExpress.XtraCharts.Series series7 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.SplineSeriesView splineSeriesView5 = new DevExpress.XtraCharts.SplineSeriesView();
            DevExpress.XtraCharts.Series series8 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.SplineSeriesView splineSeriesView6 = new DevExpress.XtraCharts.SplineSeriesView();
            DevExpress.XtraCharts.XYDiagram xyDiagram2 = new DevExpress.XtraCharts.XYDiagram();
            DevExpress.XtraCharts.Series series9 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel1 = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
            DevExpress.XtraCharts.SideBySideBarSeriesView sideBySideBarSeriesView2 = new DevExpress.XtraCharts.SideBySideBarSeriesView();
            DevExpress.XtraCharts.XYDiagram xyDiagram3 = new DevExpress.XtraCharts.XYDiagram();
            DevExpress.XtraCharts.Series series10 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel2 = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
            DevExpress.XtraCharts.SideBySideBarSeriesView sideBySideBarSeriesView3 = new DevExpress.XtraCharts.SideBySideBarSeriesView();
            this.lcMain = new DevExpress.XtraLayout.LayoutControl();
            this.lblInvestor = new DevExpress.XtraEditors.LabelControl();
            this.deInvestor = new DevExpress.XtraEditors.DateEdit();
            this.rgReportType = new DevExpress.XtraEditors.RadioGroup();
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
            this.colPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCurValue = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDiffVol = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDiffValue = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBuyVolume = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBuyValue = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSellVolume = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSellValue = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBuyAvgPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSellAvgPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ttcPosition = new DevExpress.Utils.ToolTipController(this.components);
            this.chartPosition = new DevExpress.XtraCharts.ChartControl();
            this.lblYearR = new DevExpress.XtraEditors.LabelControl();
            this.deProfit = new DevExpress.XtraEditors.DateEdit();
            this.cbTradeTypeProfit = new DevExpress.XtraEditors.ComboBoxEdit();
            this.tabProfit = new DevExpress.XtraTab.XtraTabControl();
            this.tpProfitChart = new DevExpress.XtraTab.XtraTabPage();
            this.layoutControl2 = new DevExpress.XtraLayout.LayoutControl();
            this.cbIndex = new DevExpress.XtraEditors.ComboBoxEdit();
            this.chartProfitTrend = new DevExpress.XtraCharts.ChartControl();
            this.chartGain = new DevExpress.XtraCharts.ChartControl();
            this.chartLoss = new DevExpress.XtraCharts.ChartControl();
            this.layoutControlGroup8 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem11 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem13 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcgTrendChart = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem15 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem16 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem8 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.tpProfitList = new DevExpress.XtraTab.XtraTabPage();
            this.layoutControl3 = new DevExpress.XtraLayout.LayoutControl();
            this.gcStockProfit = new DevExpress.XtraGrid.GridControl();
            this.gvStockProfit = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn14 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcInvestorProfit = new DevExpress.XtraGrid.GridControl();
            this.gvInvestorProfit = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colDataType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colReportType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colReportDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTradeDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProfit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFund = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAccProfit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAccRate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAccAvgFund = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlGroup9 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lblCurValue = new DevExpress.XtraEditors.LabelControl();
            this.lblDayP = new DevExpress.XtraEditors.LabelControl();
            this.lblDayR = new DevExpress.XtraEditors.LabelControl();
            this.lblWeekP = new DevExpress.XtraEditors.LabelControl();
            this.lblWeekR = new DevExpress.XtraEditors.LabelControl();
            this.lblMonthP = new DevExpress.XtraEditors.LabelControl();
            this.lblMonthR = new DevExpress.XtraEditors.LabelControl();
            this.lblYearP = new DevExpress.XtraEditors.LabelControl();
            this.cbTradeTypePosition = new DevExpress.XtraEditors.ComboBoxEdit();
            this.lblAccProfit = new DevExpress.XtraEditors.LabelControl();
            this.lblAccRate = new DevExpress.XtraEditors.LabelControl();
            this.lcgMain = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcgInvestor = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem27 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem28 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem29 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem30 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem31 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem32 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem33 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem24 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem7 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem5 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem26 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem10 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem9 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem12 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem14 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup7 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem23 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem22 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup10 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem4 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem6 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.lcMain)).BeginInit();
            this.lcMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.deInvestor.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deInvestor.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgReportType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dePosition.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dePosition.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcPosition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvPosition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartPosition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(pieSeriesLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(pieSeriesView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deProfit.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deProfit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbTradeTypeProfit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabProfit)).BeginInit();
            this.tabProfit.SuspendLayout();
            this.tpProfitChart.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).BeginInit();
            this.layoutControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbIndex.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartProfitTrend)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(secondaryAxisY1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(splineSeriesView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(splineSeriesView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(pointSeriesLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(splineSeriesView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(pointSeriesLabel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(splineSeriesView4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(splineSeriesView5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(splineSeriesView6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartGain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartLoss)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgTrendChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem15)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem16)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem8)).BeginInit();
            this.tpProfitList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl3)).BeginInit();
            this.layoutControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcStockProfit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvStockProfit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcInvestorProfit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvInvestorProfit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbTradeTypePosition.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgInvestor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem27)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem28)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem29)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem30)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem31)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem32)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem33)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem24)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem26)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem14)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem23)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem22)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem6)).BeginInit();
            this.SuspendLayout();
            // 
            // lcMain
            // 
            this.lcMain.BackColor = System.Drawing.Color.White;
            this.lcMain.Controls.Add(this.lblInvestor);
            this.lcMain.Controls.Add(this.deInvestor);
            this.lcMain.Controls.Add(this.rgReportType);
            this.lcMain.Controls.Add(this.dePosition);
            this.lcMain.Controls.Add(this.gcPosition);
            this.lcMain.Controls.Add(this.chartPosition);
            this.lcMain.Controls.Add(this.lblYearR);
            this.lcMain.Controls.Add(this.deProfit);
            this.lcMain.Controls.Add(this.cbTradeTypeProfit);
            this.lcMain.Controls.Add(this.tabProfit);
            this.lcMain.Controls.Add(this.lblCurValue);
            this.lcMain.Controls.Add(this.lblDayP);
            this.lcMain.Controls.Add(this.lblDayR);
            this.lcMain.Controls.Add(this.lblWeekP);
            this.lcMain.Controls.Add(this.lblWeekR);
            this.lcMain.Controls.Add(this.lblMonthP);
            this.lcMain.Controls.Add(this.lblMonthR);
            this.lcMain.Controls.Add(this.lblYearP);
            this.lcMain.Controls.Add(this.cbTradeTypePosition);
            this.lcMain.Controls.Add(this.lblAccProfit);
            this.lcMain.Controls.Add(this.lblAccRate);
            this.lcMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.lcMain.Location = new System.Drawing.Point(0, 0);
            this.lcMain.Name = "lcMain";
            this.lcMain.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(904, 1164, 900, 1440);
            this.lcMain.OptionsView.AlwaysScrollActiveControlIntoView = false;
            this.lcMain.Root = this.lcgMain;
            this.lcMain.Size = new System.Drawing.Size(1550, 898);
            this.lcMain.TabIndex = 0;
            this.lcMain.Text = "layoutControl1";
            // 
            // lblInvestor
            // 
            this.lblInvestor.Appearance.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.lblInvestor.Appearance.ForeColor = System.Drawing.Color.DarkOrange;
            this.lblInvestor.Appearance.Image = ((System.Drawing.Image)(resources.GetObject("lblInvestor.Appearance.Image")));
            this.lblInvestor.Appearance.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblInvestor.Appearance.Options.UseFont = true;
            this.lblInvestor.Appearance.Options.UseForeColor = true;
            this.lblInvestor.Appearance.Options.UseImage = true;
            this.lblInvestor.Appearance.Options.UseImageAlign = true;
            this.lblInvestor.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.lblInvestor.Location = new System.Drawing.Point(24, 24);
            this.lblInvestor.Name = "lblInvestor";
            this.lblInvestor.Size = new System.Drawing.Size(276, 56);
            this.lblInvestor.StyleController = this.lcMain;
            this.lblInvestor.TabIndex = 36;
            this.lblInvestor.Text = "labelControl1";
            // 
            // deInvestor
            // 
            this.deInvestor.EditValue = null;
            this.deInvestor.Location = new System.Drawing.Point(372, 24);
            this.deInvestor.Name = "deInvestor";
            this.deInvestor.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deInvestor.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deInvestor.Size = new System.Drawing.Size(113, 20);
            this.deInvestor.StyleController = this.lcMain;
            this.deInvestor.TabIndex = 35;
            this.deInvestor.EditValueChanged += new System.EventHandler(this.deInvestor_EditValueChanged);
            // 
            // rgReportType
            // 
            this.rgReportType.EditValue = "D";
            this.rgReportType.Location = new System.Drawing.Point(406, 437);
            this.rgReportType.Name = "rgReportType";
            this.rgReportType.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            this.rgReportType.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("D", "日"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("W", "周"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("M", "月"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("Y", "年")});
            this.rgReportType.Size = new System.Drawing.Size(152, 25);
            this.rgReportType.StyleController = this.lcMain;
            this.rgReportType.TabIndex = 34;
            this.rgReportType.SelectedIndexChanged += new System.EventHandler(this.rgReportType_SelectedIndexChanged);
            // 
            // dePosition
            // 
            this.dePosition.EditValue = null;
            this.dePosition.Location = new System.Drawing.Point(75, 129);
            this.dePosition.Name = "dePosition";
            this.dePosition.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dePosition.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dePosition.Size = new System.Drawing.Size(126, 20);
            this.dePosition.StyleController = this.lcMain;
            this.dePosition.TabIndex = 33;
            this.dePosition.EditValueChanged += new System.EventHandler(this.dePosition_EditValueChanged);
            // 
            // gcPosition
            // 
            this.gcPosition.Location = new System.Drawing.Point(558, 170);
            this.gcPosition.MainView = this.gvPosition;
            this.gcPosition.Name = "gcPosition";
            this.gcPosition.Size = new System.Drawing.Size(968, 218);
            this.gcPosition.TabIndex = 31;
            this.gcPosition.ToolTipController = this.ttcPosition;
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
            this.colPrice,
            this.colCurValue,
            this.colDiffVol,
            this.colDiffValue,
            this.colBuyVolume,
            this.colBuyValue,
            this.colSellVolume,
            this.colSellValue,
            this.colBuyAvgPrice,
            this.colSellAvgPrice});
            this.gvPosition.GridControl = this.gcPosition;
            this.gvPosition.IndicatorWidth = 30;
            this.gvPosition.Name = "gvPosition";
            this.gvPosition.OptionsBehavior.Editable = false;
            this.gvPosition.OptionsBehavior.ReadOnly = true;
            this.gvPosition.OptionsView.ColumnAutoWidth = false;
            this.gvPosition.OptionsView.ShowGroupPanel = false;
            this.gvPosition.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gvPosition_CustomDrawRowIndicator);
            this.gvPosition.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.gvPosition_CustomDrawCell);
            this.gvPosition.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gvPosition_RowCellStyle);
            this.gvPosition.DoubleClick += new System.EventHandler(this.gvPosition_DoubleClick);
            // 
            // colStockCode
            // 
            this.colStockCode.AppearanceHeader.Options.UseTextOptions = true;
            this.colStockCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colStockCode.Caption = "证券代码";
            this.colStockCode.FieldName = "StockCode";
            this.colStockCode.Name = "colStockCode";
            this.colStockCode.Visible = true;
            this.colStockCode.VisibleIndex = 0;
            // 
            // colStockName
            // 
            this.colStockName.AppearanceHeader.Options.UseTextOptions = true;
            this.colStockName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
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
            this.colTradeTypeName.Width = 65;
            // 
            // colPreVolume
            // 
            this.colPreVolume.AppearanceCell.Options.UseTextOptions = true;
            this.colPreVolume.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colPreVolume.Caption = "昨日数量";
            this.colPreVolume.DisplayFormat.FormatString = "N0";
            this.colPreVolume.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.colPreVolume.FieldName = "PreVolume";
            this.colPreVolume.Name = "colPreVolume";
            this.colPreVolume.Width = 85;
            // 
            // colPreValue
            // 
            this.colPreValue.AppearanceCell.Options.UseTextOptions = true;
            this.colPreValue.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colPreValue.Caption = "昨日市值";
            this.colPreValue.DisplayFormat.FormatString = "N2";
            this.colPreValue.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.colPreValue.FieldName = "PreValue";
            this.colPreValue.Name = "colPreValue";
            this.colPreValue.Width = 85;
            // 
            // colCurVolume
            // 
            this.colCurVolume.AppearanceCell.Options.UseTextOptions = true;
            this.colCurVolume.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colCurVolume.AppearanceHeader.Options.UseTextOptions = true;
            this.colCurVolume.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCurVolume.Caption = "当日数量";
            this.colCurVolume.DisplayFormat.FormatString = "N0";
            this.colCurVolume.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.colCurVolume.FieldName = "CurVolume";
            this.colCurVolume.Name = "colCurVolume";
            this.colCurVolume.Visible = true;
            this.colCurVolume.VisibleIndex = 3;
            this.colCurVolume.Width = 77;
            // 
            // colPrice
            // 
            this.colPrice.AppearanceHeader.Options.UseTextOptions = true;
            this.colPrice.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colPrice.Caption = "最新价";
            this.colPrice.DisplayFormat.FormatString = "N2";
            this.colPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.colPrice.FieldName = "Price";
            this.colPrice.Name = "colPrice";
            this.colPrice.Visible = true;
            this.colPrice.VisibleIndex = 4;
            this.colPrice.Width = 53;
            // 
            // colCurValue
            // 
            this.colCurValue.AppearanceCell.Options.UseTextOptions = true;
            this.colCurValue.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colCurValue.AppearanceHeader.Options.UseTextOptions = true;
            this.colCurValue.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCurValue.Caption = "当日市值";
            this.colCurValue.DisplayFormat.FormatString = "N2";
            this.colCurValue.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.colCurValue.FieldName = "CurValue";
            this.colCurValue.Name = "colCurValue";
            this.colCurValue.Visible = true;
            this.colCurValue.VisibleIndex = 5;
            this.colCurValue.Width = 79;
            // 
            // colDiffVol
            // 
            this.colDiffVol.AppearanceCell.Options.UseTextOptions = true;
            this.colDiffVol.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colDiffVol.AppearanceHeader.Options.UseTextOptions = true;
            this.colDiffVol.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDiffVol.Caption = "数量变动";
            this.colDiffVol.DisplayFormat.FormatString = "N0";
            this.colDiffVol.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.colDiffVol.FieldName = "DiffVol";
            this.colDiffVol.Name = "colDiffVol";
            this.colDiffVol.Visible = true;
            this.colDiffVol.VisibleIndex = 10;
            this.colDiffVol.Width = 85;
            // 
            // colDiffValue
            // 
            this.colDiffValue.AppearanceCell.Options.UseTextOptions = true;
            this.colDiffValue.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colDiffValue.AppearanceHeader.Options.UseTextOptions = true;
            this.colDiffValue.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDiffValue.Caption = "市值变动";
            this.colDiffValue.DisplayFormat.FormatString = "N2";
            this.colDiffValue.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.colDiffValue.FieldName = "DiffValue";
            this.colDiffValue.Name = "colDiffValue";
            this.colDiffValue.Visible = true;
            this.colDiffValue.VisibleIndex = 11;
            this.colDiffValue.Width = 85;
            // 
            // colBuyVolume
            // 
            this.colBuyVolume.AppearanceCell.Options.UseTextOptions = true;
            this.colBuyVolume.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colBuyVolume.AppearanceHeader.Options.UseTextOptions = true;
            this.colBuyVolume.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colBuyVolume.Caption = "买入数量";
            this.colBuyVolume.DisplayFormat.FormatString = "N0";
            this.colBuyVolume.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.colBuyVolume.FieldName = "BuyVolume";
            this.colBuyVolume.Name = "colBuyVolume";
            this.colBuyVolume.Visible = true;
            this.colBuyVolume.VisibleIndex = 6;
            this.colBuyVolume.Width = 76;
            // 
            // colBuyValue
            // 
            this.colBuyValue.AppearanceCell.Options.UseTextOptions = true;
            this.colBuyValue.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colBuyValue.AppearanceHeader.Options.UseTextOptions = true;
            this.colBuyValue.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colBuyValue.Caption = "买入金额";
            this.colBuyValue.DisplayFormat.FormatString = "N2";
            this.colBuyValue.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.colBuyValue.FieldName = "BuyValue";
            this.colBuyValue.Name = "colBuyValue";
            this.colBuyValue.Width = 86;
            // 
            // colSellVolume
            // 
            this.colSellVolume.AppearanceCell.Options.UseTextOptions = true;
            this.colSellVolume.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colSellVolume.AppearanceHeader.Options.UseTextOptions = true;
            this.colSellVolume.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colSellVolume.Caption = "卖出数量";
            this.colSellVolume.DisplayFormat.FormatString = "N0";
            this.colSellVolume.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.colSellVolume.FieldName = "SellVolume";
            this.colSellVolume.Name = "colSellVolume";
            this.colSellVolume.Visible = true;
            this.colSellVolume.VisibleIndex = 8;
            this.colSellVolume.Width = 74;
            // 
            // colSellValue
            // 
            this.colSellValue.AppearanceCell.Options.UseTextOptions = true;
            this.colSellValue.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colSellValue.AppearanceHeader.Options.UseTextOptions = true;
            this.colSellValue.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colSellValue.Caption = "卖出金额";
            this.colSellValue.DisplayFormat.FormatString = "N2";
            this.colSellValue.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.colSellValue.FieldName = "SellValue";
            this.colSellValue.Name = "colSellValue";
            this.colSellValue.Width = 83;
            // 
            // colBuyAvgPrice
            // 
            this.colBuyAvgPrice.AppearanceHeader.Options.UseTextOptions = true;
            this.colBuyAvgPrice.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colBuyAvgPrice.Caption = "买入均价";
            this.colBuyAvgPrice.DisplayFormat.FormatString = "N2";
            this.colBuyAvgPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.colBuyAvgPrice.FieldName = "BuyAvgPrice";
            this.colBuyAvgPrice.Name = "colBuyAvgPrice";
            this.colBuyAvgPrice.Visible = true;
            this.colBuyAvgPrice.VisibleIndex = 7;
            // 
            // colSellAvgPrice
            // 
            this.colSellAvgPrice.AppearanceHeader.Options.UseTextOptions = true;
            this.colSellAvgPrice.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colSellAvgPrice.Caption = "卖出均价";
            this.colSellAvgPrice.DisplayFormat.FormatString = "N2";
            this.colSellAvgPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.colSellAvgPrice.FieldName = "SellAvgPrice";
            this.colSellAvgPrice.Name = "colSellAvgPrice";
            this.colSellAvgPrice.Visible = true;
            this.colSellAvgPrice.VisibleIndex = 9;
            // 
            // ttcPosition
            // 
            this.ttcPosition.Appearance.ForeColor = System.Drawing.Color.Tomato;
            this.ttcPosition.Appearance.Options.UseForeColor = true;
            this.ttcPosition.Rounded = true;
            this.ttcPosition.ShowBeak = true;
            this.ttcPosition.ToolTipAnchor = DevExpress.Utils.ToolTipAnchor.Object;
            this.ttcPosition.ToolTipLocation = DevExpress.Utils.ToolTipLocation.TopLeft;
            // 
            // chartPosition
            // 
            this.chartPosition.AppearanceNameSerializable = "Light";
            this.chartPosition.BorderOptions.Visibility = DevExpress.Utils.DefaultBoolean.True;
            this.chartPosition.Legend.AlignmentHorizontal = DevExpress.XtraCharts.LegendAlignmentHorizontal.LeftOutside;
            this.chartPosition.Legend.Border.Visibility = DevExpress.Utils.DefaultBoolean.False;
            this.chartPosition.Legend.MarkerSize = new System.Drawing.Size(15, 12);
            this.chartPosition.Legend.Name = "Default Legend";
            this.chartPosition.Location = new System.Drawing.Point(24, 170);
            this.chartPosition.Name = "chartPosition";
            this.chartPosition.PaletteName = "Northern Lights";
            series1.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
            series1.CrosshairLabelVisibility = DevExpress.Utils.DefaultBoolean.True;
            pieSeriesLabel1.Border.Visibility = DevExpress.Utils.DefaultBoolean.False;
            pieSeriesLabel1.LineLength = 5;
            pieSeriesLabel1.TextPattern = "{A}: {VP:0.00%}";
            series1.Label = pieSeriesLabel1;
            series1.LegendName = "Default Legend";
            series1.LegendTextPattern = "{A}: {V:n2}";
            series1.Name = "Series 1";
            series1.View = pieSeriesView1;
            this.chartPosition.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series1};
            this.chartPosition.Size = new System.Drawing.Size(520, 218);
            this.chartPosition.TabIndex = 30;
            chartTitle1.Alignment = System.Drawing.StringAlignment.Near;
            chartTitle1.Font = new System.Drawing.Font("Tahoma", 12F);
            chartTitle1.Indent = 4;
            chartTitle1.Text = "个股持仓\r\n";
            chartTitle1.Visibility = DevExpress.Utils.DefaultBoolean.False;
            this.chartPosition.Titles.AddRange(new DevExpress.XtraCharts.ChartTitle[] {
            chartTitle1});
            // 
            // lblYearR
            // 
            this.lblYearR.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.lblYearR.Appearance.Options.UseFont = true;
            this.lblYearR.Appearance.Options.UseTextOptions = true;
            this.lblYearR.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblYearR.Location = new System.Drawing.Point(1292, 54);
            this.lblYearR.Name = "lblYearR";
            this.lblYearR.Size = new System.Drawing.Size(113, 26);
            this.lblYearR.StyleController = this.lcMain;
            this.lblYearR.TabIndex = 29;
            this.lblYearR.Text = "labelControl1";
            // 
            // deProfit
            // 
            this.deProfit.EditValue = null;
            this.deProfit.Location = new System.Drawing.Point(75, 437);
            this.deProfit.Name = "deProfit";
            this.deProfit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deProfit.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deProfit.Size = new System.Drawing.Size(126, 20);
            this.deProfit.StyleController = this.lcMain;
            this.deProfit.TabIndex = 28;
            this.deProfit.EditValueChanged += new System.EventHandler(this.deProfit_EditValueChanged);
            // 
            // cbTradeTypeProfit
            // 
            this.cbTradeTypeProfit.Location = new System.Drawing.Point(256, 437);
            this.cbTradeTypeProfit.Name = "cbTradeTypeProfit";
            this.cbTradeTypeProfit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbTradeTypeProfit.Size = new System.Drawing.Size(115, 20);
            this.cbTradeTypeProfit.StyleController = this.lcMain;
            this.cbTradeTypeProfit.TabIndex = 27;
            this.cbTradeTypeProfit.SelectedIndexChanged += new System.EventHandler(this.cbTradeTypeProfit_SelectedIndexChanged);
            // 
            // tabProfit
            // 
            this.tabProfit.Location = new System.Drawing.Point(24, 466);
            this.tabProfit.Name = "tabProfit";
            this.tabProfit.SelectedTabPage = this.tpProfitChart;
            this.tabProfit.Size = new System.Drawing.Size(1502, 408);
            this.tabProfit.TabIndex = 19;
            this.tabProfit.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tpProfitChart,
            this.tpProfitList});
            this.tabProfit.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.tabProfit_SelectedPageChanged);
            // 
            // tpProfitChart
            // 
            this.tpProfitChart.Controls.Add(this.layoutControl2);
            this.tpProfitChart.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("tpProfitChart.ImageOptions.Image")));
            this.tpProfitChart.ImageOptions.Padding = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.tpProfitChart.Name = "tpProfitChart";
            this.tpProfitChart.Size = new System.Drawing.Size(1496, 377);
            this.tpProfitChart.Text = "统计图";
            // 
            // layoutControl2
            // 
            this.layoutControl2.Controls.Add(this.cbIndex);
            this.layoutControl2.Controls.Add(this.chartProfitTrend);
            this.layoutControl2.Controls.Add(this.chartGain);
            this.layoutControl2.Controls.Add(this.chartLoss);
            this.layoutControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl2.Location = new System.Drawing.Point(0, 0);
            this.layoutControl2.Name = "layoutControl2";
            this.layoutControl2.Root = this.layoutControlGroup8;
            this.layoutControl2.Size = new System.Drawing.Size(1496, 377);
            this.layoutControl2.TabIndex = 0;
            this.layoutControl2.Text = "layoutControl2";
            // 
            // cbIndex
            // 
            this.cbIndex.Location = new System.Drawing.Point(612, 43);
            this.cbIndex.Name = "cbIndex";
            this.cbIndex.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbIndex.Size = new System.Drawing.Size(141, 20);
            this.cbIndex.StyleController = this.layoutControl2;
            this.cbIndex.TabIndex = 7;
            this.cbIndex.SelectedIndexChanged += new System.EventHandler(this.cbIndex_SelectedIndexChanged);
            // 
            // chartProfitTrend
            // 
            xyDiagram1.AxisX.Label.Angle = 45;
            xyDiagram1.AxisX.Label.Font = new System.Drawing.Font("Tahoma", 7F);
            xyDiagram1.AxisX.Label.ResolveOverlappingOptions.AllowHide = false;
            xyDiagram1.AxisX.LabelVisibilityMode = DevExpress.XtraCharts.AxisLabelVisibilityMode.AutoGeneratedAndCustom;
            xyDiagram1.AxisX.QualitativeScaleOptions.AutoGrid = false;
            xyDiagram1.AxisX.Tickmarks.MinorVisible = false;
            xyDiagram1.AxisX.VisibleInPanesSerializable = "-1";
            constantLine1.AxisValueSerializable = "0";
            constantLine1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(204)))), ((int)(((byte)(228)))));
            constantLine1.Name = "Constant Line 1";
            constantLine1.ShowBehind = true;
            constantLine1.ShowInLegend = false;
            constantLine1.Title.Visible = false;
            xyDiagram1.AxisY.ConstantLines.AddRange(new DevExpress.XtraCharts.ConstantLine[] {
            constantLine1});
            xyDiagram1.AxisY.GridLines.LineStyle.DashStyle = DevExpress.XtraCharts.DashStyle.DashDot;
            xyDiagram1.AxisY.GridLines.Visible = false;
            xyDiagram1.AxisY.Label.Font = new System.Drawing.Font("Tahoma", 7F);
            xyDiagram1.AxisY.Tickmarks.MinorVisible = false;
            xyDiagram1.AxisY.Title.Font = new System.Drawing.Font("Tahoma", 10F);
            xyDiagram1.AxisY.Title.Text = "收益额";
            xyDiagram1.AxisY.Title.TextColor = System.Drawing.Color.DarkBlue;
            xyDiagram1.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
            xyDiagram1.AxisY.VisibleInPanesSerializable = "-1";
            xyDiagram1.EnableAxisXScrolling = true;
            xyDiagram1.EnableAxisYScrolling = true;
            secondaryAxisY1.AxisID = 0;
            constantLine2.AxisValueSerializable = "0";
            constantLine2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(234)))), ((int)(((byte)(218)))));
            constantLine2.Name = "Constant Line 1";
            constantLine2.ShowBehind = true;
            constantLine2.ShowInLegend = false;
            constantLine2.Title.Text = "基准线";
            constantLine2.Title.Visible = false;
            secondaryAxisY1.ConstantLines.AddRange(new DevExpress.XtraCharts.ConstantLine[] {
            constantLine2});
            secondaryAxisY1.Label.Font = new System.Drawing.Font("Tahoma", 7F);
            secondaryAxisY1.Label.TextPattern = "{V:0.0%}";
            secondaryAxisY1.Name = "Secondary AxisY 1";
            secondaryAxisY1.Tickmarks.MinorVisible = false;
            secondaryAxisY1.Title.Font = new System.Drawing.Font("Tahoma", 10F);
            secondaryAxisY1.Title.Text = "收益率";
            secondaryAxisY1.Title.TextColor = System.Drawing.Color.OrangeRed;
            secondaryAxisY1.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
            secondaryAxisY1.VisibleInPanesSerializable = "-1";
            xyDiagram1.SecondaryAxesY.AddRange(new DevExpress.XtraCharts.SecondaryAxisY[] {
            secondaryAxisY1});
            this.chartProfitTrend.Diagram = xyDiagram1;
            this.chartProfitTrend.Legend.AlignmentHorizontal = DevExpress.XtraCharts.LegendAlignmentHorizontal.LeftOutside;
            this.chartProfitTrend.Legend.BackColor = System.Drawing.Color.MistyRose;
            this.chartProfitTrend.Legend.MarkerMode = DevExpress.XtraCharts.LegendMarkerMode.CheckBox;
            this.chartProfitTrend.Legend.Name = "Default Legend";
            this.chartProfitTrend.Location = new System.Drawing.Point(557, 67);
            this.chartProfitTrend.Name = "chartProfitTrend";
            series2.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
            series2.CrosshairLabelPattern = "{S}: {V:0.00%}";
            series2.Name = "对标指数";
            splineSeriesView1.AxisYName = "Secondary AxisY 1";
            splineSeriesView1.Color = System.Drawing.Color.DarkGreen;
            splineSeriesView1.LineMarkerOptions.Kind = DevExpress.XtraCharts.MarkerKind.Cross;
            splineSeriesView1.LineMarkerOptions.Size = 4;
            splineSeriesView1.LineStyle.Thickness = 1;
            splineSeriesView1.MarkerVisibility = DevExpress.Utils.DefaultBoolean.True;
            series2.View = splineSeriesView1;
            series3.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
            series3.CheckedInLegend = false;
            series3.CrosshairLabelPattern = "{S}: {V:n2}";
            series3.CrosshairLabelVisibility = DevExpress.Utils.DefaultBoolean.True;
            series3.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;
            series3.Name = "日均投入资金(万元)";
            series3.ToolTipPointPattern = "{S}{V}";
            splineSeriesView2.Color = System.Drawing.Color.DarkBlue;
            splineSeriesView2.LineStyle.DashStyle = DevExpress.XtraCharts.DashStyle.Dash;
            splineSeriesView2.LineStyle.Thickness = 1;
            series3.View = splineSeriesView2;
            series4.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
            series4.CheckedInLegend = false;
            series4.CrosshairLabelPattern = "{S}: {V:n2}";
            series4.Name = "投入资金(万元)";
            sideBySideBarSeriesView1.BarWidth = 0.2D;
            sideBySideBarSeriesView1.Border.Color = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(204)))), ((int)(((byte)(228)))));
            sideBySideBarSeriesView1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            series4.View = sideBySideBarSeriesView1;
            series5.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
            series5.CheckedInLegend = false;
            series5.CrosshairLabelPattern = "{S}: {V:n2}";
            series5.CrosshairLabelVisibility = DevExpress.Utils.DefaultBoolean.True;
            pointSeriesLabel1.TextPattern = "{S}: {V:n2}";
            series5.Label = pointSeriesLabel1;
            series5.Name = "收益额(万元)";
            splineSeriesView3.Color = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(172)))), ((int)(((byte)(198)))));
            splineSeriesView3.LineMarkerOptions.Color = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(172)))), ((int)(((byte)(198)))));
            splineSeriesView3.LineMarkerOptions.Kind = DevExpress.XtraCharts.MarkerKind.Triangle;
            splineSeriesView3.LineMarkerOptions.Size = 7;
            splineSeriesView3.LineStyle.Thickness = 1;
            splineSeriesView3.MarkerVisibility = DevExpress.Utils.DefaultBoolean.True;
            series5.View = splineSeriesView3;
            series6.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
            series6.CrosshairLabelPattern = "{S}: {V:0.00%}";
            series6.CrosshairLabelVisibility = DevExpress.Utils.DefaultBoolean.True;
            pointSeriesLabel2.TextPattern = "{V:0.00%}";
            series6.Label = pointSeriesLabel2;
            series6.Name = "收益率";
            splineSeriesView4.AxisYName = "Secondary AxisY 1";
            splineSeriesView4.Color = System.Drawing.Color.OrangeRed;
            splineSeriesView4.LineMarkerOptions.Size = 5;
            splineSeriesView4.LineStyle.Thickness = 1;
            splineSeriesView4.MarkerVisibility = DevExpress.Utils.DefaultBoolean.True;
            series6.View = splineSeriesView4;
            series7.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
            series7.CrosshairLabelPattern = "{S}: {V:n2}";
            series7.CrosshairLabelVisibility = DevExpress.Utils.DefaultBoolean.True;
            series7.Name = "累计收益额(万元)";
            splineSeriesView5.Color = System.Drawing.Color.SteelBlue;
            splineSeriesView5.LineMarkerOptions.Kind = DevExpress.XtraCharts.MarkerKind.Triangle;
            splineSeriesView5.LineMarkerOptions.Size = 8;
            splineSeriesView5.MarkerVisibility = DevExpress.Utils.DefaultBoolean.True;
            series7.View = splineSeriesView5;
            series8.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
            series8.CrosshairLabelPattern = "{S}: {V:0.00%}";
            series8.CrosshairLabelVisibility = DevExpress.Utils.DefaultBoolean.True;
            series8.Name = "累计收益率";
            splineSeriesView6.AxisYName = "Secondary AxisY 1";
            splineSeriesView6.Color = System.Drawing.Color.Coral;
            splineSeriesView6.LineMarkerOptions.Size = 8;
            splineSeriesView6.MarkerVisibility = DevExpress.Utils.DefaultBoolean.True;
            series8.View = splineSeriesView6;
            this.chartProfitTrend.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series2,
        series3,
        series4,
        series5,
        series6,
        series7,
        series8};
            this.chartProfitTrend.Size = new System.Drawing.Size(915, 286);
            this.chartProfitTrend.TabIndex = 6;
            // 
            // chartGain
            // 
            this.chartGain.BorderOptions.Visibility = DevExpress.Utils.DefaultBoolean.True;
            xyDiagram2.AxisX.Color = System.Drawing.Color.White;
            xyDiagram2.AxisX.Label.TextColor = System.Drawing.Color.Black;
            xyDiagram2.AxisX.Tickmarks.MinorVisible = false;
            xyDiagram2.AxisX.Tickmarks.Visible = false;
            xyDiagram2.AxisX.VisibleInPanesSerializable = "-1";
            xyDiagram2.AxisY.Alignment = DevExpress.XtraCharts.AxisAlignment.Far;
            xyDiagram2.AxisY.GridLines.Visible = false;
            xyDiagram2.AxisY.Visibility = DevExpress.Utils.DefaultBoolean.False;
            xyDiagram2.AxisY.VisibleInPanesSerializable = "-1";
            xyDiagram2.AxisY.WholeRange.AutoSideMargins = false;
            xyDiagram2.AxisY.WholeRange.SideMarginsValue = 1.5D;
            xyDiagram2.DefaultPane.BorderVisible = false;
            xyDiagram2.Rotated = true;
            this.chartGain.Diagram = xyDiagram2;
            this.chartGain.Legend.AlignmentHorizontal = DevExpress.XtraCharts.LegendAlignmentHorizontal.Left;
            this.chartGain.Legend.AlignmentVertical = DevExpress.XtraCharts.LegendAlignmentVertical.TopOutside;
            this.chartGain.Legend.Name = "Default Legend";
            this.chartGain.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
            this.chartGain.Location = new System.Drawing.Point(279, 29);
            this.chartGain.Name = "chartGain";
            series9.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
            series9.CrosshairEnabled = DevExpress.Utils.DefaultBoolean.False;
            sideBySideBarSeriesLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            sideBySideBarSeriesLabel1.Border.Visibility = DevExpress.Utils.DefaultBoolean.False;
            sideBySideBarSeriesLabel1.FillStyle.FillMode = DevExpress.XtraCharts.FillMode.Empty;
            sideBySideBarSeriesLabel1.LineVisibility = DevExpress.Utils.DefaultBoolean.False;
            sideBySideBarSeriesLabel1.Position = DevExpress.XtraCharts.BarSeriesLabelPosition.Top;
            sideBySideBarSeriesLabel1.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(80)))), ((int)(((byte)(77)))));
            sideBySideBarSeriesLabel1.TextPattern = "{V:N2}";
            series9.Label = sideBySideBarSeriesLabel1;
            series9.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            series9.Name = "Series 1";
            series9.SeriesPointsSorting = DevExpress.XtraCharts.SortingMode.Ascending;
            series9.SeriesPointsSortingKey = DevExpress.XtraCharts.SeriesPointKey.Value_1;
            sideBySideBarSeriesView2.BarWidth = 0.2D;
            sideBySideBarSeriesView2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(80)))), ((int)(((byte)(77)))));
            series9.View = sideBySideBarSeriesView2;
            this.chartGain.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series9};
            this.chartGain.Size = new System.Drawing.Size(262, 336);
            this.chartGain.TabIndex = 5;
            // 
            // chartLoss
            // 
            this.chartLoss.BorderOptions.Visibility = DevExpress.Utils.DefaultBoolean.True;
            xyDiagram3.AxisX.Alignment = DevExpress.XtraCharts.AxisAlignment.Far;
            xyDiagram3.AxisX.Color = System.Drawing.Color.White;
            xyDiagram3.AxisX.Label.TextColor = System.Drawing.Color.Black;
            xyDiagram3.AxisX.Tickmarks.MinorVisible = false;
            xyDiagram3.AxisX.Tickmarks.Visible = false;
            xyDiagram3.AxisX.VisibleInPanesSerializable = "-1";
            xyDiagram3.AxisX.WholeRange.AutoSideMargins = false;
            xyDiagram3.AxisX.WholeRange.SideMarginsValue = 0.5D;
            xyDiagram3.AxisY.Alignment = DevExpress.XtraCharts.AxisAlignment.Far;
            xyDiagram3.AxisY.GridLines.Visible = false;
            xyDiagram3.AxisY.Visibility = DevExpress.Utils.DefaultBoolean.False;
            xyDiagram3.AxisY.VisibleInPanesSerializable = "-1";
            xyDiagram3.AxisY.WholeRange.AutoSideMargins = false;
            xyDiagram3.AxisY.WholeRange.SideMarginsValue = 1.5D;
            xyDiagram3.DefaultPane.BorderVisible = false;
            xyDiagram3.EnableAxisXScrolling = true;
            xyDiagram3.Rotated = true;
            this.chartLoss.Diagram = xyDiagram3;
            this.chartLoss.Legend.AlignmentHorizontal = DevExpress.XtraCharts.LegendAlignmentHorizontal.Right;
            this.chartLoss.Legend.AlignmentVertical = DevExpress.XtraCharts.LegendAlignmentVertical.TopOutside;
            this.chartLoss.Legend.Name = "Default Legend";
            this.chartLoss.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
            this.chartLoss.Location = new System.Drawing.Point(12, 29);
            this.chartLoss.Name = "chartLoss";
            series10.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
            series10.CrosshairEnabled = DevExpress.Utils.DefaultBoolean.False;
            sideBySideBarSeriesLabel2.BackColor = System.Drawing.Color.White;
            sideBySideBarSeriesLabel2.Border.Visibility = DevExpress.Utils.DefaultBoolean.False;
            sideBySideBarSeriesLabel2.FillStyle.FillMode = DevExpress.XtraCharts.FillMode.Empty;
            sideBySideBarSeriesLabel2.LineVisibility = DevExpress.Utils.DefaultBoolean.False;
            sideBySideBarSeriesLabel2.Position = DevExpress.XtraCharts.BarSeriesLabelPosition.Top;
            sideBySideBarSeriesLabel2.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(187)))), ((int)(((byte)(89)))));
            sideBySideBarSeriesLabel2.TextPattern = "{V:N2}";
            series10.Label = sideBySideBarSeriesLabel2;
            series10.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            series10.Name = "Series 1";
            series10.SeriesPointsSorting = DevExpress.XtraCharts.SortingMode.Descending;
            series10.SeriesPointsSortingKey = DevExpress.XtraCharts.SeriesPointKey.Value_1;
            sideBySideBarSeriesView3.BarWidth = 0.2D;
            sideBySideBarSeriesView3.Color = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(187)))), ((int)(((byte)(89)))));
            series10.View = sideBySideBarSeriesView3;
            this.chartLoss.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series10};
            this.chartLoss.Size = new System.Drawing.Size(263, 336);
            this.chartLoss.TabIndex = 4;
            // 
            // layoutControlGroup8
            // 
            this.layoutControlGroup8.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup8.GroupBordersVisible = false;
            this.layoutControlGroup8.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem11,
            this.layoutControlItem13,
            this.lcgTrendChart});
            this.layoutControlGroup8.Name = "layoutControlGroup8";
            this.layoutControlGroup8.Size = new System.Drawing.Size(1496, 377);
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
            this.layoutControlItem11.Size = new System.Drawing.Size(267, 357);
            this.layoutControlItem11.Text = "亏损股票";
            this.layoutControlItem11.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem11.TextSize = new System.Drawing.Size(52, 14);
            // 
            // layoutControlItem13
            // 
            this.layoutControlItem13.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.layoutControlItem13.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem13.AppearanceItemCaption.Options.UseTextOptions = true;
            this.layoutControlItem13.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.layoutControlItem13.Control = this.chartGain;
            this.layoutControlItem13.Location = new System.Drawing.Point(267, 0);
            this.layoutControlItem13.Name = "layoutControlItem13";
            this.layoutControlItem13.Size = new System.Drawing.Size(266, 357);
            this.layoutControlItem13.Text = "盈利股票";
            this.layoutControlItem13.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem13.TextSize = new System.Drawing.Size(52, 14);
            // 
            // lcgTrendChart
            // 
            this.lcgTrendChart.AppearanceGroup.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lcgTrendChart.AppearanceGroup.Options.UseFont = true;
            this.lcgTrendChart.AppearanceGroup.Options.UseTextOptions = true;
            this.lcgTrendChart.AppearanceGroup.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lcgTrendChart.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem15,
            this.layoutControlItem16,
            this.emptySpaceItem8});
            this.lcgTrendChart.Location = new System.Drawing.Point(533, 0);
            this.lcgTrendChart.Name = "lcgTrendChart";
            this.lcgTrendChart.Size = new System.Drawing.Size(943, 357);
            this.lcgTrendChart.Text = "收益趋势图";
            // 
            // layoutControlItem15
            // 
            this.layoutControlItem15.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.layoutControlItem15.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem15.AppearanceItemCaption.Options.UseTextOptions = true;
            this.layoutControlItem15.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.layoutControlItem15.Control = this.chartProfitTrend;
            this.layoutControlItem15.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem15.Name = "layoutControlItem15";
            this.layoutControlItem15.Size = new System.Drawing.Size(919, 290);
            this.layoutControlItem15.Text = "收益趋势图";
            this.layoutControlItem15.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem15.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem15.TextVisible = false;
            // 
            // layoutControlItem16
            // 
            this.layoutControlItem16.Control = this.cbIndex;
            this.layoutControlItem16.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem16.MaxSize = new System.Drawing.Size(200, 24);
            this.layoutControlItem16.MinSize = new System.Drawing.Size(200, 24);
            this.layoutControlItem16.Name = "layoutControlItem16";
            this.layoutControlItem16.Size = new System.Drawing.Size(200, 24);
            this.layoutControlItem16.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem16.Text = "对标指数";
            this.layoutControlItem16.TextSize = new System.Drawing.Size(52, 14);
            // 
            // emptySpaceItem8
            // 
            this.emptySpaceItem8.AllowHotTrack = false;
            this.emptySpaceItem8.Location = new System.Drawing.Point(200, 0);
            this.emptySpaceItem8.Name = "emptySpaceItem8";
            this.emptySpaceItem8.Size = new System.Drawing.Size(719, 24);
            this.emptySpaceItem8.TextSize = new System.Drawing.Size(0, 0);
            // 
            // tpProfitList
            // 
            this.tpProfitList.Controls.Add(this.layoutControl3);
            this.tpProfitList.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("tpProfitList.ImageOptions.Image")));
            this.tpProfitList.ImageOptions.Padding = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.tpProfitList.Name = "tpProfitList";
            this.tpProfitList.Size = new System.Drawing.Size(1496, 377);
            this.tpProfitList.Text = "列表";
            // 
            // layoutControl3
            // 
            this.layoutControl3.Controls.Add(this.gcStockProfit);
            this.layoutControl3.Controls.Add(this.gcInvestorProfit);
            this.layoutControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl3.Location = new System.Drawing.Point(0, 0);
            this.layoutControl3.Name = "layoutControl3";
            this.layoutControl3.Root = this.layoutControlGroup9;
            this.layoutControl3.Size = new System.Drawing.Size(1496, 377);
            this.layoutControl3.TabIndex = 0;
            this.layoutControl3.Text = "layoutControl3";
            // 
            // gcStockProfit
            // 
            this.gcStockProfit.Location = new System.Drawing.Point(646, 29);
            this.gcStockProfit.MainView = this.gvStockProfit;
            this.gcStockProfit.Name = "gcStockProfit";
            this.gcStockProfit.Size = new System.Drawing.Size(775, 336);
            this.gcStockProfit.TabIndex = 5;
            this.gcStockProfit.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvStockProfit});
            // 
            // gvStockProfit
            // 
            this.gvStockProfit.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn7,
            this.gridColumn14,
            this.gridColumn8,
            this.gridColumn9,
            this.gridColumn10,
            this.gridColumn11,
            this.gridColumn12,
            this.gridColumn13});
            this.gvStockProfit.GridControl = this.gcStockProfit;
            this.gvStockProfit.IndicatorWidth = 30;
            this.gvStockProfit.Name = "gvStockProfit";
            this.gvStockProfit.OptionsBehavior.Editable = false;
            this.gvStockProfit.OptionsBehavior.ReadOnly = true;
            this.gvStockProfit.OptionsView.ColumnAutoWidth = false;
            this.gvStockProfit.OptionsView.ShowGroupPanel = false;
            this.gvStockProfit.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gvStockProfit_CustomDrawRowIndicator);
            this.gvStockProfit.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.gvStockProfit_CustomDrawCell);
            this.gvStockProfit.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gvStockProfit_RowCellStyle);
            // 
            // gridColumn2
            // 
            this.gridColumn2.FieldName = "DataType";
            this.gridColumn2.Name = "gridColumn2";
            // 
            // gridColumn3
            // 
            this.gridColumn3.FieldName = "ReportType";
            this.gridColumn3.Name = "gridColumn3";
            // 
            // gridColumn4
            // 
            this.gridColumn4.FieldName = "TradeType";
            this.gridColumn4.Name = "gridColumn4";
            // 
            // gridColumn5
            // 
            this.gridColumn5.FieldName = "ReportDate";
            this.gridColumn5.Name = "gridColumn5";
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "交易日期";
            this.gridColumn6.FieldName = "TradeDate";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 0;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "证券代码";
            this.gridColumn7.FieldName = "StockCode";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 1;
            // 
            // gridColumn14
            // 
            this.gridColumn14.Caption = "证券名称";
            this.gridColumn14.FieldName = "StockName";
            this.gridColumn14.Name = "gridColumn14";
            this.gridColumn14.Visible = true;
            this.gridColumn14.VisibleIndex = 2;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "收益额";
            this.gridColumn8.DisplayFormat.FormatString = "N2";
            this.gridColumn8.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gridColumn8.FieldName = "Profit";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 3;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "收益率";
            this.gridColumn9.DisplayFormat.FormatString = "P2";
            this.gridColumn9.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gridColumn9.FieldName = "Rate";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 4;
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "投入资金";
            this.gridColumn10.DisplayFormat.FormatString = "N2";
            this.gridColumn10.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gridColumn10.FieldName = "Fund";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 5;
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "累计收益额";
            this.gridColumn11.DisplayFormat.FormatString = "N2";
            this.gridColumn11.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gridColumn11.FieldName = "AccProfit";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.Visible = true;
            this.gridColumn11.VisibleIndex = 6;
            // 
            // gridColumn12
            // 
            this.gridColumn12.Caption = "累计收益率";
            this.gridColumn12.DisplayFormat.FormatString = "P2";
            this.gridColumn12.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gridColumn12.FieldName = "AccRate";
            this.gridColumn12.Name = "gridColumn12";
            this.gridColumn12.Visible = true;
            this.gridColumn12.VisibleIndex = 7;
            // 
            // gridColumn13
            // 
            this.gridColumn13.Caption = "日均投资资金";
            this.gridColumn13.DisplayFormat.FormatString = "N2";
            this.gridColumn13.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gridColumn13.FieldName = "AccAvgFund";
            this.gridColumn13.Name = "gridColumn13";
            this.gridColumn13.Visible = true;
            this.gridColumn13.VisibleIndex = 8;
            this.gridColumn13.Width = 100;
            // 
            // gcInvestorProfit
            // 
            this.gcInvestorProfit.Location = new System.Drawing.Point(12, 29);
            this.gcInvestorProfit.MainView = this.gvInvestorProfit;
            this.gcInvestorProfit.Name = "gcInvestorProfit";
            this.gcInvestorProfit.Size = new System.Drawing.Size(620, 336);
            this.gcInvestorProfit.TabIndex = 4;
            this.gcInvestorProfit.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvInvestorProfit});
            // 
            // gvInvestorProfit
            // 
            this.gvInvestorProfit.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colDataType,
            this.colReportType,
            this.gridColumn1,
            this.colReportDate,
            this.colTradeDate,
            this.colProfit,
            this.colRate,
            this.colFund,
            this.colAccProfit,
            this.colAccRate,
            this.colAccAvgFund});
            this.gvInvestorProfit.GridControl = this.gcInvestorProfit;
            this.gvInvestorProfit.IndicatorWidth = 30;
            this.gvInvestorProfit.Name = "gvInvestorProfit";
            this.gvInvestorProfit.OptionsBehavior.Editable = false;
            this.gvInvestorProfit.OptionsBehavior.ReadOnly = true;
            this.gvInvestorProfit.OptionsView.ColumnAutoWidth = false;
            this.gvInvestorProfit.OptionsView.ShowGroupPanel = false;
            this.gvInvestorProfit.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gvInvestorProfit_CustomDrawRowIndicator);
            this.gvInvestorProfit.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.gvInvestorProfit_CustomDrawCell);
            this.gvInvestorProfit.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gvInvestorProfit_RowCellStyle);
            this.gvInvestorProfit.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gvInvestorProfit_FocusedRowChanged);
            // 
            // colDataType
            // 
            this.colDataType.FieldName = "DataType";
            this.colDataType.Name = "colDataType";
            // 
            // colReportType
            // 
            this.colReportType.FieldName = "ReportType";
            this.colReportType.Name = "colReportType";
            // 
            // gridColumn1
            // 
            this.gridColumn1.FieldName = "TradeType";
            this.gridColumn1.Name = "gridColumn1";
            // 
            // colReportDate
            // 
            this.colReportDate.FieldName = "ReportDate";
            this.colReportDate.Name = "colReportDate";
            // 
            // colTradeDate
            // 
            this.colTradeDate.Caption = "交易日期";
            this.colTradeDate.FieldName = "TradeDate";
            this.colTradeDate.Name = "colTradeDate";
            this.colTradeDate.Visible = true;
            this.colTradeDate.VisibleIndex = 0;
            // 
            // colProfit
            // 
            this.colProfit.Caption = "收益额";
            this.colProfit.DisplayFormat.FormatString = "N2";
            this.colProfit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.colProfit.FieldName = "Profit";
            this.colProfit.Name = "colProfit";
            this.colProfit.Visible = true;
            this.colProfit.VisibleIndex = 1;
            // 
            // colRate
            // 
            this.colRate.Caption = "收益率";
            this.colRate.DisplayFormat.FormatString = "P2";
            this.colRate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.colRate.FieldName = "Rate";
            this.colRate.Name = "colRate";
            this.colRate.Visible = true;
            this.colRate.VisibleIndex = 2;
            // 
            // colFund
            // 
            this.colFund.Caption = "投入资金";
            this.colFund.DisplayFormat.FormatString = "N2";
            this.colFund.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.colFund.FieldName = "Fund";
            this.colFund.Name = "colFund";
            this.colFund.Visible = true;
            this.colFund.VisibleIndex = 3;
            // 
            // colAccProfit
            // 
            this.colAccProfit.Caption = "累计收益额";
            this.colAccProfit.DisplayFormat.FormatString = "N2";
            this.colAccProfit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.colAccProfit.FieldName = "AccProfit";
            this.colAccProfit.Name = "colAccProfit";
            this.colAccProfit.Visible = true;
            this.colAccProfit.VisibleIndex = 4;
            // 
            // colAccRate
            // 
            this.colAccRate.Caption = "累计收益率";
            this.colAccRate.DisplayFormat.FormatString = "P2";
            this.colAccRate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.colAccRate.FieldName = "AccRate";
            this.colAccRate.Name = "colAccRate";
            this.colAccRate.Visible = true;
            this.colAccRate.VisibleIndex = 5;
            // 
            // colAccAvgFund
            // 
            this.colAccAvgFund.Caption = "日均投入资金";
            this.colAccAvgFund.DisplayFormat.FormatString = "N2";
            this.colAccAvgFund.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.colAccAvgFund.FieldName = "AccAvgFund";
            this.colAccAvgFund.Name = "colAccAvgFund";
            this.colAccAvgFund.Visible = true;
            this.colAccAvgFund.VisibleIndex = 6;
            this.colAccAvgFund.Width = 103;
            // 
            // layoutControlGroup9
            // 
            this.layoutControlGroup9.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup9.GroupBordersVisible = false;
            this.layoutControlGroup9.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem6,
            this.layoutControlItem7,
            this.emptySpaceItem2,
            this.emptySpaceItem3});
            this.layoutControlGroup9.Name = "layoutControlGroup9";
            this.layoutControlGroup9.Size = new System.Drawing.Size(1496, 377);
            this.layoutControlGroup9.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.layoutControlItem6.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem6.AppearanceItemCaption.Options.UseTextOptions = true;
            this.layoutControlItem6.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.layoutControlItem6.Control = this.gcInvestorProfit;
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(624, 357);
            this.layoutControlItem6.Text = "投资人员收益流水（金额：万元）";
            this.layoutControlItem6.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem6.TextSize = new System.Drawing.Size(195, 14);
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.layoutControlItem7.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem7.AppearanceItemCaption.Options.UseTextOptions = true;
            this.layoutControlItem7.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.layoutControlItem7.Control = this.gcStockProfit;
            this.layoutControlItem7.Location = new System.Drawing.Point(634, 0);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(779, 357);
            this.layoutControlItem7.Text = "股票收益明细（金额：万元）";
            this.layoutControlItem7.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem7.TextSize = new System.Drawing.Size(195, 14);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(624, 0);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(10, 357);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            this.emptySpaceItem3.Location = new System.Drawing.Point(1413, 0);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(63, 357);
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lblCurValue
            // 
            this.lblCurValue.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.lblCurValue.Appearance.Options.UseFont = true;
            this.lblCurValue.Appearance.Options.UseTextOptions = true;
            this.lblCurValue.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblCurValue.Location = new System.Drawing.Point(372, 54);
            this.lblCurValue.Name = "lblCurValue";
            this.lblCurValue.Size = new System.Drawing.Size(113, 26);
            this.lblCurValue.StyleController = this.lcMain;
            this.lblCurValue.TabIndex = 29;
            this.lblCurValue.Text = "labelControl1";
            // 
            // lblDayP
            // 
            this.lblDayP.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.lblDayP.Appearance.Options.UseFont = true;
            this.lblDayP.Appearance.Options.UseTextOptions = true;
            this.lblDayP.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblDayP.Location = new System.Drawing.Point(752, 24);
            this.lblDayP.Name = "lblDayP";
            this.lblDayP.Size = new System.Drawing.Size(113, 26);
            this.lblDayP.StyleController = this.lcMain;
            this.lblDayP.TabIndex = 29;
            this.lblDayP.Text = "labelControl1";
            // 
            // lblDayR
            // 
            this.lblDayR.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.lblDayR.Appearance.Options.UseFont = true;
            this.lblDayR.Appearance.Options.UseTextOptions = true;
            this.lblDayR.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblDayR.Location = new System.Drawing.Point(752, 54);
            this.lblDayR.Name = "lblDayR";
            this.lblDayR.Size = new System.Drawing.Size(113, 26);
            this.lblDayR.StyleController = this.lcMain;
            this.lblDayR.TabIndex = 29;
            this.lblDayR.Text = "labelControl1";
            // 
            // lblWeekP
            // 
            this.lblWeekP.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.lblWeekP.Appearance.Options.UseFont = true;
            this.lblWeekP.Appearance.Options.UseTextOptions = true;
            this.lblWeekP.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblWeekP.Location = new System.Drawing.Point(932, 24);
            this.lblWeekP.Name = "lblWeekP";
            this.lblWeekP.Size = new System.Drawing.Size(113, 26);
            this.lblWeekP.StyleController = this.lcMain;
            this.lblWeekP.TabIndex = 29;
            this.lblWeekP.Text = "labelControl1";
            // 
            // lblWeekR
            // 
            this.lblWeekR.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.lblWeekR.Appearance.Options.UseFont = true;
            this.lblWeekR.Appearance.Options.UseTextOptions = true;
            this.lblWeekR.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblWeekR.Location = new System.Drawing.Point(932, 54);
            this.lblWeekR.Name = "lblWeekR";
            this.lblWeekR.Size = new System.Drawing.Size(113, 26);
            this.lblWeekR.StyleController = this.lcMain;
            this.lblWeekR.TabIndex = 29;
            this.lblWeekR.Text = "labelControl1";
            // 
            // lblMonthP
            // 
            this.lblMonthP.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.lblMonthP.Appearance.Options.UseFont = true;
            this.lblMonthP.Appearance.Options.UseTextOptions = true;
            this.lblMonthP.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblMonthP.Location = new System.Drawing.Point(1112, 24);
            this.lblMonthP.Name = "lblMonthP";
            this.lblMonthP.Size = new System.Drawing.Size(113, 26);
            this.lblMonthP.StyleController = this.lcMain;
            this.lblMonthP.TabIndex = 29;
            this.lblMonthP.Text = "labelControl1";
            // 
            // lblMonthR
            // 
            this.lblMonthR.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.lblMonthR.Appearance.Options.UseFont = true;
            this.lblMonthR.Appearance.Options.UseTextOptions = true;
            this.lblMonthR.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblMonthR.Location = new System.Drawing.Point(1112, 54);
            this.lblMonthR.Name = "lblMonthR";
            this.lblMonthR.Size = new System.Drawing.Size(113, 26);
            this.lblMonthR.StyleController = this.lcMain;
            this.lblMonthR.TabIndex = 29;
            this.lblMonthR.Text = "labelControl1";
            // 
            // lblYearP
            // 
            this.lblYearP.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.lblYearP.Appearance.Options.UseFont = true;
            this.lblYearP.Appearance.Options.UseTextOptions = true;
            this.lblYearP.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblYearP.Location = new System.Drawing.Point(1292, 24);
            this.lblYearP.Name = "lblYearP";
            this.lblYearP.Size = new System.Drawing.Size(113, 26);
            this.lblYearP.StyleController = this.lcMain;
            this.lblYearP.TabIndex = 29;
            this.lblYearP.Text = "labelControl1";
            // 
            // cbTradeTypePosition
            // 
            this.cbTradeTypePosition.Location = new System.Drawing.Point(256, 129);
            this.cbTradeTypePosition.Name = "cbTradeTypePosition";
            this.cbTradeTypePosition.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbTradeTypePosition.Size = new System.Drawing.Size(115, 20);
            this.cbTradeTypePosition.StyleController = this.lcMain;
            this.cbTradeTypePosition.TabIndex = 27;
            this.cbTradeTypePosition.SelectedIndexChanged += new System.EventHandler(this.cbTradeTypePosition_SelectedIndexChanged);
            // 
            // lblAccProfit
            // 
            this.lblAccProfit.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.lblAccProfit.Appearance.Options.UseFont = true;
            this.lblAccProfit.Appearance.Options.UseTextOptions = true;
            this.lblAccProfit.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblAccProfit.Location = new System.Drawing.Point(572, 24);
            this.lblAccProfit.Name = "lblAccProfit";
            this.lblAccProfit.Size = new System.Drawing.Size(113, 26);
            this.lblAccProfit.StyleController = this.lcMain;
            this.lblAccProfit.TabIndex = 29;
            this.lblAccProfit.Text = "labelControl1";
            // 
            // lblAccRate
            // 
            this.lblAccRate.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.lblAccRate.Appearance.Options.UseFont = true;
            this.lblAccRate.Appearance.Options.UseTextOptions = true;
            this.lblAccRate.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblAccRate.Location = new System.Drawing.Point(572, 54);
            this.lblAccRate.Name = "lblAccRate";
            this.lblAccRate.Size = new System.Drawing.Size(113, 26);
            this.lblAccRate.StyleController = this.lcMain;
            this.lblAccRate.TabIndex = 29;
            this.lblAccRate.Text = "labelControl1";
            // 
            // lcgMain
            // 
            this.lcgMain.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.lcgMain.GroupBordersVisible = false;
            this.lcgMain.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcgInvestor,
            this.layoutControlGroup7,
            this.layoutControlGroup10});
            this.lcgMain.Name = "Root";
            this.lcgMain.Size = new System.Drawing.Size(1550, 898);
            this.lcgMain.TextVisible = false;
            // 
            // lcgInvestor
            // 
            this.lcgInvestor.AppearanceGroup.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lcgInvestor.AppearanceGroup.Options.UseFont = true;
            this.lcgInvestor.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem27,
            this.layoutControlItem28,
            this.layoutControlItem29,
            this.layoutControlItem30,
            this.layoutControlItem31,
            this.layoutControlItem32,
            this.layoutControlItem33,
            this.layoutControlItem24,
            this.emptySpaceItem7,
            this.emptySpaceItem5,
            this.layoutControlItem8,
            this.layoutControlItem26,
            this.layoutControlItem10,
            this.emptySpaceItem9,
            this.layoutControlItem12,
            this.layoutControlItem14});
            this.lcgInvestor.Location = new System.Drawing.Point(0, 0);
            this.lcgInvestor.Name = "lcgInvestor";
            this.lcgInvestor.OptionsItemText.TextAlignMode = DevExpress.XtraLayout.TextAlignModeGroup.AlignWithChildren;
            this.lcgInvestor.Size = new System.Drawing.Size(1530, 84);
            this.lcgInvestor.Text = "投资人员";
            this.lcgInvestor.TextVisible = false;
            // 
            // layoutControlItem27
            // 
            this.layoutControlItem27.Control = this.lblDayP;
            this.layoutControlItem27.CustomizationFormText = "layoutControlItem24";
            this.layoutControlItem27.Location = new System.Drawing.Point(665, 0);
            this.layoutControlItem27.MaxSize = new System.Drawing.Size(180, 30);
            this.layoutControlItem27.MinSize = new System.Drawing.Size(180, 30);
            this.layoutControlItem27.Name = "layoutControlItem27";
            this.layoutControlItem27.Size = new System.Drawing.Size(180, 30);
            this.layoutControlItem27.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem27.Text = "日收益额";
            this.layoutControlItem27.TextSize = new System.Drawing.Size(60, 14);
            // 
            // layoutControlItem28
            // 
            this.layoutControlItem28.Control = this.lblDayR;
            this.layoutControlItem28.CustomizationFormText = "layoutControlItem24";
            this.layoutControlItem28.Location = new System.Drawing.Point(665, 30);
            this.layoutControlItem28.MaxSize = new System.Drawing.Size(180, 30);
            this.layoutControlItem28.MinSize = new System.Drawing.Size(180, 30);
            this.layoutControlItem28.Name = "layoutControlItem28";
            this.layoutControlItem28.Size = new System.Drawing.Size(180, 30);
            this.layoutControlItem28.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem28.Text = "日收益率";
            this.layoutControlItem28.TextSize = new System.Drawing.Size(60, 14);
            // 
            // layoutControlItem29
            // 
            this.layoutControlItem29.Control = this.lblWeekP;
            this.layoutControlItem29.CustomizationFormText = "layoutControlItem24";
            this.layoutControlItem29.Location = new System.Drawing.Point(845, 0);
            this.layoutControlItem29.MaxSize = new System.Drawing.Size(180, 30);
            this.layoutControlItem29.MinSize = new System.Drawing.Size(180, 30);
            this.layoutControlItem29.Name = "layoutControlItem29";
            this.layoutControlItem29.Size = new System.Drawing.Size(180, 30);
            this.layoutControlItem29.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem29.Text = "周收益额";
            this.layoutControlItem29.TextSize = new System.Drawing.Size(60, 14);
            // 
            // layoutControlItem30
            // 
            this.layoutControlItem30.Control = this.lblWeekR;
            this.layoutControlItem30.CustomizationFormText = "layoutControlItem24";
            this.layoutControlItem30.Location = new System.Drawing.Point(845, 30);
            this.layoutControlItem30.MaxSize = new System.Drawing.Size(180, 30);
            this.layoutControlItem30.MinSize = new System.Drawing.Size(180, 30);
            this.layoutControlItem30.Name = "layoutControlItem30";
            this.layoutControlItem30.Size = new System.Drawing.Size(180, 30);
            this.layoutControlItem30.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem30.Text = "周收益率";
            this.layoutControlItem30.TextSize = new System.Drawing.Size(60, 14);
            // 
            // layoutControlItem31
            // 
            this.layoutControlItem31.Control = this.lblMonthP;
            this.layoutControlItem31.CustomizationFormText = "layoutControlItem24";
            this.layoutControlItem31.Location = new System.Drawing.Point(1025, 0);
            this.layoutControlItem31.MaxSize = new System.Drawing.Size(180, 30);
            this.layoutControlItem31.MinSize = new System.Drawing.Size(180, 30);
            this.layoutControlItem31.Name = "layoutControlItem31";
            this.layoutControlItem31.Size = new System.Drawing.Size(180, 30);
            this.layoutControlItem31.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem31.Text = "月收益额";
            this.layoutControlItem31.TextSize = new System.Drawing.Size(60, 14);
            // 
            // layoutControlItem32
            // 
            this.layoutControlItem32.Control = this.lblMonthR;
            this.layoutControlItem32.CustomizationFormText = "layoutControlItem24";
            this.layoutControlItem32.Location = new System.Drawing.Point(1025, 30);
            this.layoutControlItem32.MaxSize = new System.Drawing.Size(180, 30);
            this.layoutControlItem32.MinSize = new System.Drawing.Size(180, 30);
            this.layoutControlItem32.Name = "layoutControlItem32";
            this.layoutControlItem32.Size = new System.Drawing.Size(180, 30);
            this.layoutControlItem32.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem32.Text = "月收益率";
            this.layoutControlItem32.TextSize = new System.Drawing.Size(60, 14);
            // 
            // layoutControlItem33
            // 
            this.layoutControlItem33.Control = this.lblYearP;
            this.layoutControlItem33.CustomizationFormText = "layoutControlItem24";
            this.layoutControlItem33.Location = new System.Drawing.Point(1205, 0);
            this.layoutControlItem33.MaxSize = new System.Drawing.Size(180, 30);
            this.layoutControlItem33.MinSize = new System.Drawing.Size(180, 30);
            this.layoutControlItem33.Name = "layoutControlItem33";
            this.layoutControlItem33.Size = new System.Drawing.Size(180, 30);
            this.layoutControlItem33.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem33.Text = "年收益额";
            this.layoutControlItem33.TextSize = new System.Drawing.Size(60, 14);
            // 
            // layoutControlItem24
            // 
            this.layoutControlItem24.Control = this.lblYearR;
            this.layoutControlItem24.Location = new System.Drawing.Point(1205, 30);
            this.layoutControlItem24.MaxSize = new System.Drawing.Size(180, 30);
            this.layoutControlItem24.MinSize = new System.Drawing.Size(180, 30);
            this.layoutControlItem24.Name = "layoutControlItem24";
            this.layoutControlItem24.Size = new System.Drawing.Size(180, 30);
            this.layoutControlItem24.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem24.Text = "年收益率";
            this.layoutControlItem24.TextSize = new System.Drawing.Size(60, 14);
            // 
            // emptySpaceItem7
            // 
            this.emptySpaceItem7.AllowHotTrack = false;
            this.emptySpaceItem7.Location = new System.Drawing.Point(465, 0);
            this.emptySpaceItem7.MaxSize = new System.Drawing.Size(20, 30);
            this.emptySpaceItem7.MinSize = new System.Drawing.Size(20, 30);
            this.emptySpaceItem7.Name = "emptySpaceItem7";
            this.emptySpaceItem7.Size = new System.Drawing.Size(20, 60);
            this.emptySpaceItem7.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem7.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem5
            // 
            this.emptySpaceItem5.AllowHotTrack = false;
            this.emptySpaceItem5.Location = new System.Drawing.Point(280, 0);
            this.emptySpaceItem5.MaxSize = new System.Drawing.Size(5, 30);
            this.emptySpaceItem5.MinSize = new System.Drawing.Size(5, 30);
            this.emptySpaceItem5.Name = "emptySpaceItem5";
            this.emptySpaceItem5.Size = new System.Drawing.Size(5, 60);
            this.emptySpaceItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem5.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.layoutControlItem8.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem8.Control = this.deInvestor;
            this.layoutControlItem8.Location = new System.Drawing.Point(285, 0);
            this.layoutControlItem8.MaxSize = new System.Drawing.Size(180, 30);
            this.layoutControlItem8.MinSize = new System.Drawing.Size(180, 30);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(180, 30);
            this.layoutControlItem8.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem8.Text = "统计日期";
            this.layoutControlItem8.TextSize = new System.Drawing.Size(60, 14);
            // 
            // layoutControlItem26
            // 
            this.layoutControlItem26.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.layoutControlItem26.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem26.Control = this.lblCurValue;
            this.layoutControlItem26.CustomizationFormText = "layoutControlItem24";
            this.layoutControlItem26.Location = new System.Drawing.Point(285, 30);
            this.layoutControlItem26.MaxSize = new System.Drawing.Size(185, 30);
            this.layoutControlItem26.MinSize = new System.Drawing.Size(180, 30);
            this.layoutControlItem26.Name = "layoutControlItem26";
            this.layoutControlItem26.Size = new System.Drawing.Size(180, 30);
            this.layoutControlItem26.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem26.Text = "当前持仓";
            this.layoutControlItem26.TextSize = new System.Drawing.Size(60, 14);
            // 
            // layoutControlItem10
            // 
            this.layoutControlItem10.Control = this.lblInvestor;
            this.layoutControlItem10.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem10.MaxSize = new System.Drawing.Size(280, 60);
            this.layoutControlItem10.MinSize = new System.Drawing.Size(280, 60);
            this.layoutControlItem10.Name = "layoutControlItem10";
            this.layoutControlItem10.Size = new System.Drawing.Size(280, 60);
            this.layoutControlItem10.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem10.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem10.TextVisible = false;
            // 
            // emptySpaceItem9
            // 
            this.emptySpaceItem9.AllowHotTrack = false;
            this.emptySpaceItem9.Location = new System.Drawing.Point(1385, 0);
            this.emptySpaceItem9.Name = "emptySpaceItem9";
            this.emptySpaceItem9.Size = new System.Drawing.Size(121, 60);
            this.emptySpaceItem9.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem12
            // 
            this.layoutControlItem12.Control = this.lblAccProfit;
            this.layoutControlItem12.CustomizationFormText = "layoutControlItem24";
            this.layoutControlItem12.Location = new System.Drawing.Point(485, 0);
            this.layoutControlItem12.MaxSize = new System.Drawing.Size(180, 30);
            this.layoutControlItem12.MinSize = new System.Drawing.Size(180, 30);
            this.layoutControlItem12.Name = "layoutControlItem12";
            this.layoutControlItem12.Size = new System.Drawing.Size(180, 30);
            this.layoutControlItem12.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem12.Text = "累计收益额";
            this.layoutControlItem12.TextSize = new System.Drawing.Size(60, 14);
            // 
            // layoutControlItem14
            // 
            this.layoutControlItem14.Control = this.lblAccRate;
            this.layoutControlItem14.CustomizationFormText = "layoutControlItem24";
            this.layoutControlItem14.Location = new System.Drawing.Point(485, 30);
            this.layoutControlItem14.MaxSize = new System.Drawing.Size(180, 30);
            this.layoutControlItem14.MinSize = new System.Drawing.Size(180, 30);
            this.layoutControlItem14.Name = "layoutControlItem14";
            this.layoutControlItem14.Size = new System.Drawing.Size(180, 30);
            this.layoutControlItem14.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem14.Text = "累计收益率";
            this.layoutControlItem14.TextSize = new System.Drawing.Size(60, 14);
            // 
            // layoutControlGroup7
            // 
            this.layoutControlGroup7.AppearanceGroup.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.layoutControlGroup7.AppearanceGroup.Options.UseFont = true;
            this.layoutControlGroup7.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem9,
            this.emptySpaceItem1,
            this.layoutControlItem23,
            this.layoutControlItem22,
            this.layoutControlItem5});
            this.layoutControlGroup7.Location = new System.Drawing.Point(0, 392);
            this.layoutControlGroup7.Name = "layoutControlGroup7";
            this.layoutControlGroup7.OptionsItemText.TextAlignMode = DevExpress.XtraLayout.TextAlignModeGroup.AutoSize;
            this.layoutControlGroup7.Size = new System.Drawing.Size(1530, 486);
            this.layoutControlGroup7.Text = "收益";
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.tabProfit;
            this.layoutControlItem9.Location = new System.Drawing.Point(0, 29);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(1506, 412);
            this.layoutControlItem9.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem9.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem9.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(538, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(968, 29);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem23
            // 
            this.layoutControlItem23.Control = this.deProfit;
            this.layoutControlItem23.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem23.Name = "layoutControlItem23";
            this.layoutControlItem23.Size = new System.Drawing.Size(181, 29);
            this.layoutControlItem23.Text = "统计日期";
            this.layoutControlItem23.TextSize = new System.Drawing.Size(48, 14);
            // 
            // layoutControlItem22
            // 
            this.layoutControlItem22.Control = this.cbTradeTypeProfit;
            this.layoutControlItem22.Location = new System.Drawing.Point(181, 0);
            this.layoutControlItem22.Name = "layoutControlItem22";
            this.layoutControlItem22.Size = new System.Drawing.Size(170, 29);
            this.layoutControlItem22.Text = "交易类别";
            this.layoutControlItem22.TextSize = new System.Drawing.Size(48, 14);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.rgReportType;
            this.layoutControlItem5.Location = new System.Drawing.Point(351, 0);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(187, 29);
            this.layoutControlItem5.Text = "周期 ";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(28, 14);
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
            this.layoutControlItem3,
            this.emptySpaceItem6});
            this.layoutControlGroup10.Location = new System.Drawing.Point(0, 84);
            this.layoutControlGroup10.Name = "layoutControlGroup10";
            this.layoutControlGroup10.OptionsItemText.TextAlignMode = DevExpress.XtraLayout.TextAlignModeGroup.AutoSize;
            this.layoutControlGroup10.Size = new System.Drawing.Size(1530, 308);
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
            this.layoutControlItem1.Size = new System.Drawing.Size(524, 239);
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
            this.layoutControlItem2.Location = new System.Drawing.Point(534, 24);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(972, 239);
            this.layoutControlItem2.Text = "持仓变动(金额：万元)";
            this.layoutControlItem2.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(127, 14);
            // 
            // emptySpaceItem4
            // 
            this.emptySpaceItem4.AllowHotTrack = false;
            this.emptySpaceItem4.Location = new System.Drawing.Point(351, 0);
            this.emptySpaceItem4.Name = "emptySpaceItem4";
            this.emptySpaceItem4.Size = new System.Drawing.Size(1155, 24);
            this.emptySpaceItem4.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.dePosition;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(181, 24);
            this.layoutControlItem4.Text = "统计日期";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(48, 14);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.cbTradeTypePosition;
            this.layoutControlItem3.CustomizationFormText = "交易类别";
            this.layoutControlItem3.Location = new System.Drawing.Point(181, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(170, 24);
            this.layoutControlItem3.Text = "交易类别";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(48, 14);
            // 
            // emptySpaceItem6
            // 
            this.emptySpaceItem6.AllowHotTrack = false;
            this.emptySpaceItem6.Location = new System.Drawing.Point(524, 24);
            this.emptySpaceItem6.Name = "emptySpaceItem6";
            this.emptySpaceItem6.Size = new System.Drawing.Size(10, 239);
            this.emptySpaceItem6.TextSize = new System.Drawing.Size(0, 0);
            // 
            // FrmHomePage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1550, 898);
            this.Controls.Add(this.lcMain);
            this.Name = "FrmHomePage";
            this.Text = "FrmHomePage";
            this.Load += new System.EventHandler(this.FrmInvestorStudio_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lcMain)).EndInit();
            this.lcMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.deInvestor.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deInvestor.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgReportType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dePosition.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dePosition.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcPosition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvPosition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(pieSeriesLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(pieSeriesView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartPosition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deProfit.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deProfit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbTradeTypeProfit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabProfit)).EndInit();
            this.tabProfit.ResumeLayout(false);
            this.tpProfitChart.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).EndInit();
            this.layoutControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cbIndex.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(secondaryAxisY1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(splineSeriesView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(splineSeriesView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(pointSeriesLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(splineSeriesView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(pointSeriesLabel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(splineSeriesView4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(splineSeriesView5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(splineSeriesView6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartProfitTrend)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartGain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartLoss)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgTrendChart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem15)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem16)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem8)).EndInit();
            this.tpProfitList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl3)).EndInit();
            this.layoutControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcStockProfit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvStockProfit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcInvestorProfit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvInvestorProfit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbTradeTypePosition.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgInvestor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem27)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem28)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem29)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem30)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem31)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem32)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem33)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem24)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem26)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem14)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem23)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem22)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem6)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl lcMain;
        private DevExpress.XtraLayout.LayoutControlGroup lcgMain;
        private DevExpress.XtraLayout.LayoutControlGroup lcgInvestor;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem5;
        private DevExpress.XtraTab.XtraTabControl tabProfit;
        private DevExpress.XtraTab.XtraTabPage tpProfitChart;
        private DevExpress.XtraTab.XtraTabPage tpProfitList;
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
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem22;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem23;
        private DevExpress.XtraEditors.LabelControl lblYearR;
        private DevExpress.XtraEditors.LabelControl lblCurValue;
        private DevExpress.XtraEditors.LabelControl lblDayP;
        private DevExpress.XtraEditors.LabelControl lblDayR;
        private DevExpress.XtraEditors.LabelControl lblWeekP;
        private DevExpress.XtraEditors.LabelControl lblWeekR;
        private DevExpress.XtraEditors.LabelControl lblMonthP;
        private DevExpress.XtraEditors.LabelControl lblMonthR;
        private DevExpress.XtraEditors.LabelControl lblYearP;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem26;
        private DevExpress.XtraGrid.GridControl gcPosition;
        private DevExpress.XtraGrid.Views.Grid.GridView gvPosition;
        private DevExpress.XtraCharts.ChartControl chartPosition;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup10;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.DateEdit dePosition;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
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
        private DevExpress.XtraEditors.RadioGroup rgReportType;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraGrid.GridControl gcStockProfit;
        private DevExpress.XtraGrid.Views.Grid.GridView gvStockProfit;
        private DevExpress.XtraGrid.GridControl gcInvestorProfit;
        private DevExpress.XtraGrid.Views.Grid.GridView gvInvestorProfit;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraGrid.Columns.GridColumn colDataType;
        private DevExpress.XtraGrid.Columns.GridColumn colReportType;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn colReportDate;
        private DevExpress.XtraGrid.Columns.GridColumn colTradeDate;
        private DevExpress.XtraGrid.Columns.GridColumn colProfit;
        private DevExpress.XtraGrid.Columns.GridColumn colRate;
        private DevExpress.XtraGrid.Columns.GridColumn colFund;
        private DevExpress.XtraGrid.Columns.GridColumn colAccProfit;
        private DevExpress.XtraGrid.Columns.GridColumn colAccRate;
        private DevExpress.XtraGrid.Columns.GridColumn colAccAvgFund;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn14;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem6;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem27;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem28;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem29;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem30;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem31;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem32;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem33;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem24;
        private DevExpress.XtraEditors.DateEdit deInvestor;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem7;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem9;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraEditors.LabelControl lblInvestor;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem10;
        private DevExpress.XtraGrid.Columns.GridColumn colDiffVol;
        private DevExpress.XtraGrid.Columns.GridColumn colDiffValue;
        private DevExpress.Utils.ToolTipController ttcPosition;
        private DevExpress.XtraGrid.Columns.GridColumn colPrice;
        private DevExpress.XtraEditors.LabelControl lblAccProfit;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem12;
        private DevExpress.XtraEditors.LabelControl lblAccRate;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem14;
        private DevExpress.XtraGrid.Columns.GridColumn colBuyAvgPrice;
        private DevExpress.XtraGrid.Columns.GridColumn colSellAvgPrice;
        private DevExpress.XtraEditors.ComboBoxEdit cbIndex;
        private DevExpress.XtraLayout.LayoutControlGroup lcgTrendChart;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem16;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem8;
    }
}