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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmInvestorStudio));
            DevExpress.XtraCharts.Series series1 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.PieSeriesLabel pieSeriesLabel1 = new DevExpress.XtraCharts.PieSeriesLabel();
            DevExpress.XtraCharts.PieSeriesView pieSeriesView1 = new DevExpress.XtraCharts.PieSeriesView();
            DevExpress.XtraCharts.ChartTitle chartTitle1 = new DevExpress.XtraCharts.ChartTitle();
            DevExpress.XtraCharts.XYDiagram xyDiagram1 = new DevExpress.XtraCharts.XYDiagram();
            DevExpress.XtraCharts.ConstantLine constantLine1 = new DevExpress.XtraCharts.ConstantLine();
            DevExpress.XtraCharts.SecondaryAxisY secondaryAxisY1 = new DevExpress.XtraCharts.SecondaryAxisY();
            DevExpress.XtraCharts.ConstantLine constantLine2 = new DevExpress.XtraCharts.ConstantLine();
            DevExpress.XtraCharts.Series series2 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.SideBySideBarSeriesView sideBySideBarSeriesView1 = new DevExpress.XtraCharts.SideBySideBarSeriesView();
            DevExpress.XtraCharts.Series series3 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.PointSeriesLabel pointSeriesLabel1 = new DevExpress.XtraCharts.PointSeriesLabel();
            DevExpress.XtraCharts.SplineSeriesView splineSeriesView1 = new DevExpress.XtraCharts.SplineSeriesView();
            DevExpress.XtraCharts.Series series4 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.PointSeriesLabel pointSeriesLabel2 = new DevExpress.XtraCharts.PointSeriesLabel();
            DevExpress.XtraCharts.SplineSeriesView splineSeriesView2 = new DevExpress.XtraCharts.SplineSeriesView();
            DevExpress.XtraCharts.Series series5 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.SplineSeriesView splineSeriesView3 = new DevExpress.XtraCharts.SplineSeriesView();
            DevExpress.XtraCharts.Series series6 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.SplineSeriesView splineSeriesView4 = new DevExpress.XtraCharts.SplineSeriesView();
            DevExpress.XtraCharts.Series series7 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.SplineSeriesView splineSeriesView5 = new DevExpress.XtraCharts.SplineSeriesView();
            DevExpress.XtraCharts.XYDiagram xyDiagram2 = new DevExpress.XtraCharts.XYDiagram();
            DevExpress.XtraCharts.Series series8 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel1 = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
            DevExpress.XtraCharts.SideBySideBarSeriesView sideBySideBarSeriesView2 = new DevExpress.XtraCharts.SideBySideBarSeriesView();
            DevExpress.XtraCharts.XYDiagram xyDiagram3 = new DevExpress.XtraCharts.XYDiagram();
            DevExpress.XtraCharts.Series series9 = new DevExpress.XtraCharts.Series();
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
            this.colCurValue = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBuyVolume = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBuyValue = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSellVolume = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSellValue = new DevExpress.XtraGrid.Columns.GridColumn();
            this.chartPosition = new DevExpress.XtraCharts.ChartControl();
            this.lblYearR = new DevExpress.XtraEditors.LabelControl();
            this.deProfit = new DevExpress.XtraEditors.DateEdit();
            this.cbTradeTypeProfit = new DevExpress.XtraEditors.ComboBoxEdit();
            this.tabProfit = new DevExpress.XtraTab.XtraTabControl();
            this.tpProfitChart = new DevExpress.XtraTab.XtraTabPage();
            this.layoutControl2 = new DevExpress.XtraLayout.LayoutControl();
            this.chartProfitTrend = new DevExpress.XtraCharts.ChartControl();
            this.chartGain = new DevExpress.XtraCharts.ChartControl();
            this.chartLoss = new DevExpress.XtraCharts.ChartControl();
            this.layoutControlGroup8 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem11 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem13 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem15 = new DevExpress.XtraLayout.LayoutControlItem();
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
            this.colYearProfit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colYearRate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colYearAvgFund = new DevExpress.XtraGrid.Columns.GridColumn();
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
            this.emptySpaceItem9 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem5 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem26 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem10 = new DevExpress.XtraLayout.LayoutControlItem();
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
            ((System.ComponentModel.ISupportInitialize)(this.chartProfitTrend)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(secondaryAxisY1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(pointSeriesLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(splineSeriesView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(pointSeriesLabel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(splineSeriesView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(splineSeriesView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(splineSeriesView4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(splineSeriesView5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartGain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartLoss)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem15)).BeginInit();
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
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem26)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).BeginInit();
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
            this.lblInvestor.Size = new System.Drawing.Size(196, 56);
            this.lblInvestor.StyleController = this.lcMain;
            this.lblInvestor.TabIndex = 36;
            this.lblInvestor.Text = "labelControl1";
            // 
            // deInvestor
            // 
            this.deInvestor.EditValue = null;
            this.deInvestor.Location = new System.Drawing.Point(284, 24);
            this.deInvestor.Name = "deInvestor";
            this.deInvestor.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deInvestor.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deInvestor.Size = new System.Drawing.Size(121, 20);
            this.deInvestor.StyleController = this.lcMain;
            this.deInvestor.TabIndex = 35;
            this.deInvestor.EditValueChanged += new System.EventHandler(this.deInvestor_EditValueChanged);
            // 
            // rgReportType
            // 
            this.rgReportType.EditValue = "D";
            this.rgReportType.Location = new System.Drawing.Point(406, 459);
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
            this.dePosition.Location = new System.Drawing.Point(75, 127);
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
            this.gcPosition.Location = new System.Drawing.Point(558, 168);
            this.gcPosition.MainView = this.gvPosition;
            this.gcPosition.Name = "gcPosition";
            this.gcPosition.Size = new System.Drawing.Size(968, 244);
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
            this.gvPosition.IndicatorWidth = 30;
            this.gvPosition.Name = "gvPosition";
            this.gvPosition.OptionsBehavior.Editable = false;
            this.gvPosition.OptionsBehavior.ReadOnly = true;
            this.gvPosition.OptionsView.ColumnAutoWidth = false;
            this.gvPosition.OptionsView.ShowGroupPanel = false;
            this.gvPosition.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gvPosition_CustomDrawRowIndicator);
            this.gvPosition.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.gvPosition_CustomDrawCell);
            this.gvPosition.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gvPosition_RowCellStyle);
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
            this.colPreVolume.Width = 85;
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
            this.colPreValue.Width = 85;
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
            this.colCurVolume.Width = 85;
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
            this.colCurValue.Width = 85;
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
            this.colBuyVolume.Width = 80;
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
            this.colSellVolume.Width = 80;
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
            this.chartPosition.AppearanceNameSerializable = "Light";
            this.chartPosition.BorderOptions.Visibility = DevExpress.Utils.DefaultBoolean.True;
            this.chartPosition.Legend.AlignmentHorizontal = DevExpress.XtraCharts.LegendAlignmentHorizontal.LeftOutside;
            this.chartPosition.Legend.Border.Visibility = DevExpress.Utils.DefaultBoolean.False;
            this.chartPosition.Legend.MarkerSize = new System.Drawing.Size(15, 12);
            this.chartPosition.Legend.Name = "Default Legend";
            this.chartPosition.Location = new System.Drawing.Point(24, 168);
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
            this.chartPosition.Size = new System.Drawing.Size(520, 244);
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
            this.lblYearR.Location = new System.Drawing.Point(1024, 54);
            this.lblYearR.Name = "lblYearR";
            this.lblYearR.Size = new System.Drawing.Size(121, 26);
            this.lblYearR.StyleController = this.lcMain;
            this.lblYearR.TabIndex = 29;
            this.lblYearR.Text = "labelControl1";
            // 
            // deProfit
            // 
            this.deProfit.EditValue = null;
            this.deProfit.Location = new System.Drawing.Point(75, 459);
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
            this.cbTradeTypeProfit.Location = new System.Drawing.Point(256, 459);
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
            this.tabProfit.Location = new System.Drawing.Point(24, 488);
            this.tabProfit.Name = "tabProfit";
            this.tabProfit.SelectedTabPage = this.tpProfitChart;
            this.tabProfit.Size = new System.Drawing.Size(1502, 386);
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
            this.tpProfitChart.Size = new System.Drawing.Size(1500, 358);
            this.tpProfitChart.Text = "统计图";
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
            this.layoutControl2.Size = new System.Drawing.Size(1500, 358);
            this.layoutControl2.TabIndex = 0;
            this.layoutControl2.Text = "layoutControl2";
            // 
            // chartProfitTrend
            // 
            xyDiagram1.AxisX.Label.Angle = 45;
            xyDiagram1.AxisX.Label.Font = new System.Drawing.Font("Tahoma", 7F);
            xyDiagram1.AxisX.LabelVisibilityMode = DevExpress.XtraCharts.AxisLabelVisibilityMode.AutoGeneratedAndCustom;
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
            this.chartProfitTrend.Legend.BackColor = System.Drawing.Color.MistyRose;
            this.chartProfitTrend.Legend.MarkerMode = DevExpress.XtraCharts.LegendMarkerMode.CheckBox;
            this.chartProfitTrend.Legend.Name = "Default Legend";
            this.chartProfitTrend.Location = new System.Drawing.Point(547, 29);
            this.chartProfitTrend.Name = "chartProfitTrend";
            series2.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
            series2.CrosshairLabelPattern = "{S}: {V:n2}";
            series2.Name = "投入资金(万元)";
            sideBySideBarSeriesView1.BarWidth = 0.2D;
            sideBySideBarSeriesView1.Border.Color = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(204)))), ((int)(((byte)(228)))));
            sideBySideBarSeriesView1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            series2.View = sideBySideBarSeriesView1;
            series3.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
            series3.CrosshairLabelPattern = "{S}: {V:n2}";
            series3.CrosshairLabelVisibility = DevExpress.Utils.DefaultBoolean.True;
            pointSeriesLabel1.TextPattern = "{S}: {V:n2}";
            series3.Label = pointSeriesLabel1;
            series3.Name = "收益额(万元)";
            splineSeriesView1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(172)))), ((int)(((byte)(198)))));
            splineSeriesView1.LineMarkerOptions.Color = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(172)))), ((int)(((byte)(198)))));
            splineSeriesView1.LineMarkerOptions.Kind = DevExpress.XtraCharts.MarkerKind.Triangle;
            splineSeriesView1.LineMarkerOptions.Size = 7;
            splineSeriesView1.LineStyle.Thickness = 1;
            series3.View = splineSeriesView1;
            series4.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
            series4.CrosshairLabelPattern = "{S}: {V:0.00%}";
            series4.CrosshairLabelVisibility = DevExpress.Utils.DefaultBoolean.True;
            pointSeriesLabel2.TextPattern = "{V:0.00%}";
            series4.Label = pointSeriesLabel2;
            series4.Name = "收益率";
            splineSeriesView2.AxisYName = "Secondary AxisY 1";
            splineSeriesView2.Color = System.Drawing.Color.OrangeRed;
            splineSeriesView2.LineMarkerOptions.Size = 5;
            splineSeriesView2.LineStyle.Thickness = 1;
            series4.View = splineSeriesView2;
            series5.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
            series5.CrosshairLabelPattern = "{S}: {V:n2}";
            series5.CrosshairLabelVisibility = DevExpress.Utils.DefaultBoolean.True;
            series5.Name = "年收益额(万元)";
            splineSeriesView3.Color = System.Drawing.Color.SteelBlue;
            splineSeriesView3.LineMarkerOptions.Kind = DevExpress.XtraCharts.MarkerKind.Triangle;
            splineSeriesView3.LineMarkerOptions.Size = 8;
            series5.View = splineSeriesView3;
            series6.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
            series6.CrosshairLabelPattern = "{S}: {V:0.00%}";
            series6.CrosshairLabelVisibility = DevExpress.Utils.DefaultBoolean.True;
            series6.Name = "年收益率";
            splineSeriesView4.AxisYName = "Secondary AxisY 1";
            splineSeriesView4.Color = System.Drawing.Color.Coral;
            splineSeriesView4.LineMarkerOptions.Size = 8;
            series6.View = splineSeriesView4;
            series7.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
            series7.CrosshairLabelPattern = "{S}: {V:n2}";
            series7.CrosshairLabelVisibility = DevExpress.Utils.DefaultBoolean.True;
            series7.Name = "日均投入资金(十万元)";
            splineSeriesView5.Color = System.Drawing.Color.DarkBlue;
            splineSeriesView5.LineStyle.DashStyle = DevExpress.XtraCharts.DashStyle.Dash;
            splineSeriesView5.LineStyle.Thickness = 1;
            series7.View = splineSeriesView5;
            this.chartProfitTrend.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series2,
        series3,
        series4,
        series5,
        series6,
        series7};
            this.chartProfitTrend.Size = new System.Drawing.Size(941, 317);
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
            this.chartGain.Location = new System.Drawing.Point(280, 29);
            this.chartGain.Name = "chartGain";
            series8.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
            series8.CrosshairEnabled = DevExpress.Utils.DefaultBoolean.False;
            sideBySideBarSeriesLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            sideBySideBarSeriesLabel1.Border.Visibility = DevExpress.Utils.DefaultBoolean.False;
            sideBySideBarSeriesLabel1.FillStyle.FillMode = DevExpress.XtraCharts.FillMode.Empty;
            sideBySideBarSeriesLabel1.LineVisibility = DevExpress.Utils.DefaultBoolean.False;
            sideBySideBarSeriesLabel1.Position = DevExpress.XtraCharts.BarSeriesLabelPosition.Top;
            sideBySideBarSeriesLabel1.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(80)))), ((int)(((byte)(77)))));
            sideBySideBarSeriesLabel1.TextPattern = "{V:N0}";
            series8.Label = sideBySideBarSeriesLabel1;
            series8.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            series8.Name = "Series 1";
            series8.SeriesPointsSorting = DevExpress.XtraCharts.SortingMode.Ascending;
            series8.SeriesPointsSortingKey = DevExpress.XtraCharts.SeriesPointKey.Value_1;
            sideBySideBarSeriesView2.BarWidth = 0.2D;
            sideBySideBarSeriesView2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(80)))), ((int)(((byte)(77)))));
            series8.View = sideBySideBarSeriesView2;
            this.chartGain.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series8};
            this.chartGain.Size = new System.Drawing.Size(263, 317);
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
            series9.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
            series9.CrosshairEnabled = DevExpress.Utils.DefaultBoolean.False;
            sideBySideBarSeriesLabel2.BackColor = System.Drawing.Color.White;
            sideBySideBarSeriesLabel2.Border.Visibility = DevExpress.Utils.DefaultBoolean.False;
            sideBySideBarSeriesLabel2.FillStyle.FillMode = DevExpress.XtraCharts.FillMode.Empty;
            sideBySideBarSeriesLabel2.LineVisibility = DevExpress.Utils.DefaultBoolean.False;
            sideBySideBarSeriesLabel2.Position = DevExpress.XtraCharts.BarSeriesLabelPosition.Top;
            sideBySideBarSeriesLabel2.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(187)))), ((int)(((byte)(89)))));
            sideBySideBarSeriesLabel2.TextPattern = "{V:N0}";
            series9.Label = sideBySideBarSeriesLabel2;
            series9.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            series9.Name = "Series 1";
            series9.SeriesPointsSorting = DevExpress.XtraCharts.SortingMode.Descending;
            series9.SeriesPointsSortingKey = DevExpress.XtraCharts.SeriesPointKey.Value_1;
            sideBySideBarSeriesView3.BarWidth = 0.2D;
            sideBySideBarSeriesView3.Color = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(187)))), ((int)(((byte)(89)))));
            series9.View = sideBySideBarSeriesView3;
            this.chartLoss.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series9};
            this.chartLoss.Size = new System.Drawing.Size(264, 317);
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
            this.layoutControlGroup8.Name = "layoutControlGroup8";
            this.layoutControlGroup8.Size = new System.Drawing.Size(1500, 358);
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
            this.layoutControlItem11.Size = new System.Drawing.Size(268, 338);
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
            this.layoutControlItem13.Location = new System.Drawing.Point(268, 0);
            this.layoutControlItem13.Name = "layoutControlItem13";
            this.layoutControlItem13.Size = new System.Drawing.Size(267, 338);
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
            this.layoutControlItem15.Location = new System.Drawing.Point(535, 0);
            this.layoutControlItem15.Name = "layoutControlItem15";
            this.layoutControlItem15.Size = new System.Drawing.Size(945, 338);
            this.layoutControlItem15.Text = "收益趋势图";
            this.layoutControlItem15.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem15.TextSize = new System.Drawing.Size(65, 14);
            // 
            // tpProfitList
            // 
            this.tpProfitList.Controls.Add(this.layoutControl3);
            this.tpProfitList.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("tpProfitList.ImageOptions.Image")));
            this.tpProfitList.ImageOptions.Padding = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.tpProfitList.Name = "tpProfitList";
            this.tpProfitList.Size = new System.Drawing.Size(1500, 358);
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
            this.layoutControl3.Size = new System.Drawing.Size(1500, 358);
            this.layoutControl3.TabIndex = 0;
            this.layoutControl3.Text = "layoutControl3";
            // 
            // gcStockProfit
            // 
            this.gcStockProfit.Location = new System.Drawing.Point(648, 29);
            this.gcStockProfit.MainView = this.gvStockProfit;
            this.gcStockProfit.Name = "gcStockProfit";
            this.gcStockProfit.Size = new System.Drawing.Size(777, 317);
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
            this.gridColumn11.Caption = "年收益额";
            this.gridColumn11.DisplayFormat.FormatString = "N2";
            this.gridColumn11.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gridColumn11.FieldName = "YearProfit";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.Visible = true;
            this.gridColumn11.VisibleIndex = 6;
            // 
            // gridColumn12
            // 
            this.gridColumn12.Caption = "年收益率";
            this.gridColumn12.DisplayFormat.FormatString = "P2";
            this.gridColumn12.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gridColumn12.FieldName = "YearRate";
            this.gridColumn12.Name = "gridColumn12";
            this.gridColumn12.Visible = true;
            this.gridColumn12.VisibleIndex = 7;
            // 
            // gridColumn13
            // 
            this.gridColumn13.Caption = "年日均投资资金";
            this.gridColumn13.DisplayFormat.FormatString = "N2";
            this.gridColumn13.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gridColumn13.FieldName = "YearAvgFund";
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
            this.gcInvestorProfit.Size = new System.Drawing.Size(622, 317);
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
            this.colYearProfit,
            this.colYearRate,
            this.colYearAvgFund});
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
            // colYearProfit
            // 
            this.colYearProfit.Caption = "年收益额";
            this.colYearProfit.DisplayFormat.FormatString = "N2";
            this.colYearProfit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.colYearProfit.FieldName = "YearProfit";
            this.colYearProfit.Name = "colYearProfit";
            this.colYearProfit.Visible = true;
            this.colYearProfit.VisibleIndex = 4;
            // 
            // colYearRate
            // 
            this.colYearRate.Caption = "年收益率";
            this.colYearRate.DisplayFormat.FormatString = "P2";
            this.colYearRate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.colYearRate.FieldName = "YearRate";
            this.colYearRate.Name = "colYearRate";
            this.colYearRate.Visible = true;
            this.colYearRate.VisibleIndex = 5;
            // 
            // colYearAvgFund
            // 
            this.colYearAvgFund.Caption = "年日均投入资金";
            this.colYearAvgFund.DisplayFormat.FormatString = "N2";
            this.colYearAvgFund.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.colYearAvgFund.FieldName = "YearAvgFund";
            this.colYearAvgFund.Name = "colYearAvgFund";
            this.colYearAvgFund.Visible = true;
            this.colYearAvgFund.VisibleIndex = 6;
            this.colYearAvgFund.Width = 103;
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
            this.layoutControlGroup9.Size = new System.Drawing.Size(1500, 358);
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
            this.layoutControlItem6.Size = new System.Drawing.Size(626, 338);
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
            this.layoutControlItem7.Location = new System.Drawing.Point(636, 0);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(781, 338);
            this.layoutControlItem7.Text = "股票收益明细（金额：万元）";
            this.layoutControlItem7.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem7.TextSize = new System.Drawing.Size(195, 14);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(626, 0);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(10, 338);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            this.emptySpaceItem3.Location = new System.Drawing.Point(1417, 0);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(63, 338);
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lblCurValue
            // 
            this.lblCurValue.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.lblCurValue.Appearance.Options.UseFont = true;
            this.lblCurValue.Appearance.Options.UseTextOptions = true;
            this.lblCurValue.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblCurValue.Location = new System.Drawing.Point(284, 54);
            this.lblCurValue.Name = "lblCurValue";
            this.lblCurValue.Size = new System.Drawing.Size(121, 26);
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
            this.lblDayP.Location = new System.Drawing.Point(484, 24);
            this.lblDayP.Name = "lblDayP";
            this.lblDayP.Size = new System.Drawing.Size(121, 26);
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
            this.lblDayR.Location = new System.Drawing.Point(484, 54);
            this.lblDayR.Name = "lblDayR";
            this.lblDayR.Size = new System.Drawing.Size(121, 26);
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
            this.lblWeekP.Location = new System.Drawing.Point(664, 24);
            this.lblWeekP.Name = "lblWeekP";
            this.lblWeekP.Size = new System.Drawing.Size(121, 26);
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
            this.lblWeekR.Location = new System.Drawing.Point(664, 54);
            this.lblWeekR.Name = "lblWeekR";
            this.lblWeekR.Size = new System.Drawing.Size(121, 26);
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
            this.lblMonthP.Location = new System.Drawing.Point(844, 24);
            this.lblMonthP.Name = "lblMonthP";
            this.lblMonthP.Size = new System.Drawing.Size(121, 26);
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
            this.lblMonthR.Location = new System.Drawing.Point(844, 54);
            this.lblMonthR.Name = "lblMonthR";
            this.lblMonthR.Size = new System.Drawing.Size(121, 26);
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
            this.lblYearP.Location = new System.Drawing.Point(1024, 24);
            this.lblYearP.Name = "lblYearP";
            this.lblYearP.Size = new System.Drawing.Size(121, 26);
            this.lblYearP.StyleController = this.lcMain;
            this.lblYearP.TabIndex = 29;
            this.lblYearP.Text = "labelControl1";
            // 
            // cbTradeTypePosition
            // 
            this.cbTradeTypePosition.Location = new System.Drawing.Point(256, 127);
            this.cbTradeTypePosition.Name = "cbTradeTypePosition";
            this.cbTradeTypePosition.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbTradeTypePosition.Size = new System.Drawing.Size(115, 20);
            this.cbTradeTypePosition.StyleController = this.lcMain;
            this.cbTradeTypePosition.TabIndex = 27;
            this.cbTradeTypePosition.SelectedIndexChanged += new System.EventHandler(this.cbTradeTypePosition_SelectedIndexChanged);
            // 
            // lcgMain
            // 
            this.lcgMain.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.lcgMain.GroupBordersVisible = false;
            this.lcgMain.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcgInvestor,
            this.layoutControlGroup7,
            this.layoutControlGroup10});
            this.lcgMain.Name = "lcgMain";
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
            this.emptySpaceItem9});
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
            this.layoutControlItem27.Location = new System.Drawing.Point(405, 0);
            this.layoutControlItem27.MaxSize = new System.Drawing.Size(180, 30);
            this.layoutControlItem27.MinSize = new System.Drawing.Size(180, 30);
            this.layoutControlItem27.Name = "layoutControlItem27";
            this.layoutControlItem27.Size = new System.Drawing.Size(180, 30);
            this.layoutControlItem27.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem27.Text = "日收益额";
            this.layoutControlItem27.TextSize = new System.Drawing.Size(52, 14);
            // 
            // layoutControlItem28
            // 
            this.layoutControlItem28.Control = this.lblDayR;
            this.layoutControlItem28.CustomizationFormText = "layoutControlItem24";
            this.layoutControlItem28.Location = new System.Drawing.Point(405, 30);
            this.layoutControlItem28.MaxSize = new System.Drawing.Size(180, 30);
            this.layoutControlItem28.MinSize = new System.Drawing.Size(180, 30);
            this.layoutControlItem28.Name = "layoutControlItem28";
            this.layoutControlItem28.Size = new System.Drawing.Size(180, 30);
            this.layoutControlItem28.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem28.Text = "日收益率";
            this.layoutControlItem28.TextSize = new System.Drawing.Size(52, 14);
            // 
            // layoutControlItem29
            // 
            this.layoutControlItem29.Control = this.lblWeekP;
            this.layoutControlItem29.CustomizationFormText = "layoutControlItem24";
            this.layoutControlItem29.Location = new System.Drawing.Point(585, 0);
            this.layoutControlItem29.MaxSize = new System.Drawing.Size(180, 30);
            this.layoutControlItem29.MinSize = new System.Drawing.Size(180, 30);
            this.layoutControlItem29.Name = "layoutControlItem29";
            this.layoutControlItem29.Size = new System.Drawing.Size(180, 30);
            this.layoutControlItem29.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem29.Text = "周收益额";
            this.layoutControlItem29.TextSize = new System.Drawing.Size(52, 14);
            // 
            // layoutControlItem30
            // 
            this.layoutControlItem30.Control = this.lblWeekR;
            this.layoutControlItem30.CustomizationFormText = "layoutControlItem24";
            this.layoutControlItem30.Location = new System.Drawing.Point(585, 30);
            this.layoutControlItem30.MaxSize = new System.Drawing.Size(180, 30);
            this.layoutControlItem30.MinSize = new System.Drawing.Size(180, 30);
            this.layoutControlItem30.Name = "layoutControlItem30";
            this.layoutControlItem30.Size = new System.Drawing.Size(180, 30);
            this.layoutControlItem30.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem30.Text = "周收益率";
            this.layoutControlItem30.TextSize = new System.Drawing.Size(52, 14);
            // 
            // layoutControlItem31
            // 
            this.layoutControlItem31.Control = this.lblMonthP;
            this.layoutControlItem31.CustomizationFormText = "layoutControlItem24";
            this.layoutControlItem31.Location = new System.Drawing.Point(765, 0);
            this.layoutControlItem31.MaxSize = new System.Drawing.Size(180, 30);
            this.layoutControlItem31.MinSize = new System.Drawing.Size(180, 30);
            this.layoutControlItem31.Name = "layoutControlItem31";
            this.layoutControlItem31.Size = new System.Drawing.Size(180, 30);
            this.layoutControlItem31.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem31.Text = "月收益额";
            this.layoutControlItem31.TextSize = new System.Drawing.Size(52, 14);
            // 
            // layoutControlItem32
            // 
            this.layoutControlItem32.Control = this.lblMonthR;
            this.layoutControlItem32.CustomizationFormText = "layoutControlItem24";
            this.layoutControlItem32.Location = new System.Drawing.Point(765, 30);
            this.layoutControlItem32.MaxSize = new System.Drawing.Size(180, 30);
            this.layoutControlItem32.MinSize = new System.Drawing.Size(180, 30);
            this.layoutControlItem32.Name = "layoutControlItem32";
            this.layoutControlItem32.Size = new System.Drawing.Size(180, 30);
            this.layoutControlItem32.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem32.Text = "月收益率";
            this.layoutControlItem32.TextSize = new System.Drawing.Size(52, 14);
            // 
            // layoutControlItem33
            // 
            this.layoutControlItem33.Control = this.lblYearP;
            this.layoutControlItem33.CustomizationFormText = "layoutControlItem24";
            this.layoutControlItem33.Location = new System.Drawing.Point(945, 0);
            this.layoutControlItem33.MaxSize = new System.Drawing.Size(180, 30);
            this.layoutControlItem33.MinSize = new System.Drawing.Size(180, 30);
            this.layoutControlItem33.Name = "layoutControlItem33";
            this.layoutControlItem33.Size = new System.Drawing.Size(180, 30);
            this.layoutControlItem33.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem33.Text = "年收益额";
            this.layoutControlItem33.TextSize = new System.Drawing.Size(52, 14);
            // 
            // layoutControlItem24
            // 
            this.layoutControlItem24.Control = this.lblYearR;
            this.layoutControlItem24.Location = new System.Drawing.Point(945, 30);
            this.layoutControlItem24.MaxSize = new System.Drawing.Size(180, 30);
            this.layoutControlItem24.MinSize = new System.Drawing.Size(180, 30);
            this.layoutControlItem24.Name = "layoutControlItem24";
            this.layoutControlItem24.Size = new System.Drawing.Size(180, 30);
            this.layoutControlItem24.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem24.Text = "年收益率";
            this.layoutControlItem24.TextSize = new System.Drawing.Size(52, 14);
            // 
            // emptySpaceItem7
            // 
            this.emptySpaceItem7.AllowHotTrack = false;
            this.emptySpaceItem7.Location = new System.Drawing.Point(385, 0);
            this.emptySpaceItem7.MaxSize = new System.Drawing.Size(20, 30);
            this.emptySpaceItem7.MinSize = new System.Drawing.Size(20, 30);
            this.emptySpaceItem7.Name = "emptySpaceItem7";
            this.emptySpaceItem7.Size = new System.Drawing.Size(20, 60);
            this.emptySpaceItem7.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem7.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem9
            // 
            this.emptySpaceItem9.AllowHotTrack = false;
            this.emptySpaceItem9.Location = new System.Drawing.Point(1125, 0);
            this.emptySpaceItem9.Name = "emptySpaceItem9";
            this.emptySpaceItem9.Size = new System.Drawing.Size(381, 60);
            this.emptySpaceItem9.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem5
            // 
            this.emptySpaceItem5.AllowHotTrack = false;
            this.emptySpaceItem5.Location = new System.Drawing.Point(200, 0);
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
            this.layoutControlItem8.Location = new System.Drawing.Point(205, 0);
            this.layoutControlItem8.MaxSize = new System.Drawing.Size(180, 30);
            this.layoutControlItem8.MinSize = new System.Drawing.Size(180, 30);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(180, 30);
            this.layoutControlItem8.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem8.Text = "统计日期";
            this.layoutControlItem8.TextSize = new System.Drawing.Size(52, 14);
            // 
            // layoutControlItem26
            // 
            this.layoutControlItem26.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.layoutControlItem26.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem26.Control = this.lblCurValue;
            this.layoutControlItem26.CustomizationFormText = "layoutControlItem24";
            this.layoutControlItem26.Location = new System.Drawing.Point(205, 30);
            this.layoutControlItem26.MaxSize = new System.Drawing.Size(185, 30);
            this.layoutControlItem26.MinSize = new System.Drawing.Size(180, 30);
            this.layoutControlItem26.Name = "layoutControlItem26";
            this.layoutControlItem26.Size = new System.Drawing.Size(180, 30);
            this.layoutControlItem26.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem26.Text = "当前持仓";
            this.layoutControlItem26.TextSize = new System.Drawing.Size(52, 14);
            // 
            // layoutControlItem10
            // 
            this.layoutControlItem10.Control = this.lblInvestor;
            this.layoutControlItem10.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem10.MaxSize = new System.Drawing.Size(200, 60);
            this.layoutControlItem10.MinSize = new System.Drawing.Size(200, 60);
            this.layoutControlItem10.Name = "layoutControlItem10";
            this.layoutControlItem10.Size = new System.Drawing.Size(200, 60);
            this.layoutControlItem10.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem10.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem10.TextVisible = false;
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
            this.layoutControlGroup7.Location = new System.Drawing.Point(0, 416);
            this.layoutControlGroup7.Name = "layoutControlGroup7";
            this.layoutControlGroup7.OptionsItemText.TextAlignMode = DevExpress.XtraLayout.TextAlignModeGroup.AutoSize;
            this.layoutControlGroup7.Size = new System.Drawing.Size(1530, 462);
            this.layoutControlGroup7.Text = "收益";
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.tabProfit;
            this.layoutControlItem9.Location = new System.Drawing.Point(0, 29);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(1506, 390);
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
            this.layoutControlGroup10.Size = new System.Drawing.Size(1530, 332);
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
            this.layoutControlItem1.Size = new System.Drawing.Size(524, 265);
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
            this.layoutControlItem2.Size = new System.Drawing.Size(972, 265);
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
            this.emptySpaceItem6.Size = new System.Drawing.Size(10, 265);
            this.emptySpaceItem6.TextSize = new System.Drawing.Size(0, 0);
            // 
            // FrmInvestorStudio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1550, 898);
            this.Controls.Add(this.lcMain);
            this.Name = "FrmInvestorStudio";
            this.Text = "FrmInvestorStudio";
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
            ((System.ComponentModel.ISupportInitialize)(secondaryAxisY1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(pointSeriesLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(splineSeriesView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(pointSeriesLabel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(splineSeriesView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(splineSeriesView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(splineSeriesView4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(splineSeriesView5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartProfitTrend)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartGain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartLoss)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem15)).EndInit();
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
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem26)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).EndInit();
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
        private DevExpress.XtraGrid.Columns.GridColumn colYearProfit;
        private DevExpress.XtraGrid.Columns.GridColumn colYearRate;
        private DevExpress.XtraGrid.Columns.GridColumn colYearAvgFund;
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
    }
}