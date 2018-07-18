namespace CTM.Win.Forms.DailyTrading.RiskControl
{
    partial class FrmAccountRC
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
            DevExpress.XtraCharts.XYDiagram xyDiagram1 = new DevExpress.XtraCharts.XYDiagram();
            DevExpress.XtraCharts.SecondaryAxisY secondaryAxisY1 = new DevExpress.XtraCharts.SecondaryAxisY();
            DevExpress.XtraCharts.ConstantLine constantLine1 = new DevExpress.XtraCharts.ConstantLine();
            DevExpress.XtraCharts.Series series1 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.SplineSeriesView splineSeriesView1 = new DevExpress.XtraCharts.SplineSeriesView();
            DevExpress.XtraCharts.Series series2 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.PointSeriesLabel pointSeriesLabel1 = new DevExpress.XtraCharts.PointSeriesLabel();
            DevExpress.XtraCharts.SplineSeriesView splineSeriesView2 = new DevExpress.XtraCharts.SplineSeriesView();
            DevExpress.XtraCharts.Series series3 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.SplineSeriesView splineSeriesView3 = new DevExpress.XtraCharts.SplineSeriesView();
            DevExpress.XtraCharts.Series series4 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.PointSeriesLabel pointSeriesLabel2 = new DevExpress.XtraCharts.PointSeriesLabel();
            DevExpress.XtraCharts.SplineSeriesView splineSeriesView4 = new DevExpress.XtraCharts.SplineSeriesView();
            this.sidePanel1 = new DevExpress.XtraEditors.SidePanel();
            this.layoutControl2 = new DevExpress.XtraLayout.LayoutControl();
            this.gcList = new DevExpress.XtraGrid.GridControl();
            this.gvList = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colAccountId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAccountName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colInvestFund = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cbInvestor = new DevExpress.XtraEditors.ComboBoxEdit();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem4 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.sidePanel2 = new DevExpress.XtraEditors.SidePanel();
            this.layoutControl3 = new DevExpress.XtraLayout.LayoutControl();
            this.lblDate = new DevExpress.XtraEditors.LabelControl();
            this.lblDP = new DevExpress.XtraEditors.LabelControl();
            this.lblDR = new DevExpress.XtraEditors.LabelControl();
            this.lblAP = new DevExpress.XtraEditors.LabelControl();
            this.lblAR = new DevExpress.XtraEditors.LabelControl();
            this.lblMax = new DevExpress.XtraEditors.LabelControl();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem16 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem12 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem17 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem18 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem19 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem20 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem5 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btnView = new DevExpress.XtraEditors.SimpleButton();
            this.xtratabcontrol1 = new DevExpress.XtraTab.XtraTabControl();
            this.pageAccount = new DevExpress.XtraTab.XtraTabPage();
            this.layoutControl4 = new DevExpress.XtraLayout.LayoutControl();
            this.chartAccount = new DevExpress.XtraCharts.ChartControl();
            this.gcAccount = new DevExpress.XtraGrid.GridControl();
            this.gvAccount = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTradeDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDayProfit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDayRate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAccProfit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAccRate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRetraceAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRetraceRate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlGroup4 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.pageStock = new DevExpress.XtraTab.XtraTabPage();
            this.layoutControl5 = new DevExpress.XtraLayout.LayoutControl();
            this.gcStock = new DevExpress.XtraGrid.GridControl();
            this.gvStock = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colTradeId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStockCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStockName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPeriodProfit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPeriodRate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlGroup5 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.pageTran = new DevExpress.XtraTab.XtraTabPage();
            this.layoutControl6 = new DevExpress.XtraLayout.LayoutControl();
            this.gcTrans = new DevExpress.XtraGrid.GridControl();
            this.gvTrans = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTransactionId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn14 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn15 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlGroup6 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem11 = new DevExpress.XtraLayout.LayoutControlItem();
            this.deEnd = new DevExpress.XtraEditors.DateEdit();
            this.deStart = new DevExpress.XtraEditors.DateEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.sidePanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).BeginInit();
            this.layoutControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbInvestor.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).BeginInit();
            this.sidePanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl3)).BeginInit();
            this.layoutControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem16)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem17)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem18)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem19)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem20)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtratabcontrol1)).BeginInit();
            this.xtratabcontrol1.SuspendLayout();
            this.pageAccount.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl4)).BeginInit();
            this.layoutControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartAccount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(secondaryAxisY1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(splineSeriesView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(pointSeriesLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(splineSeriesView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(splineSeriesView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(pointSeriesLabel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(splineSeriesView4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcAccount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvAccount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            this.pageStock.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl5)).BeginInit();
            this.layoutControl5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcStock)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvStock)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            this.pageTran.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl6)).BeginInit();
            this.layoutControl6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcTrans)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvTrans)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deEnd.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deEnd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deStart.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deStart.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            this.SuspendLayout();
            // 
            // sidePanel1
            // 
            this.sidePanel1.Controls.Add(this.layoutControl2);
            this.sidePanel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.sidePanel1.Location = new System.Drawing.Point(0, 0);
            this.sidePanel1.Name = "sidePanel1";
            this.sidePanel1.Size = new System.Drawing.Size(337, 757);
            this.sidePanel1.TabIndex = 0;
            this.sidePanel1.Text = "sidePanel1";
            // 
            // layoutControl2
            // 
            this.layoutControl2.Controls.Add(this.gcList);
            this.layoutControl2.Controls.Add(this.cbInvestor);
            this.layoutControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl2.Location = new System.Drawing.Point(0, 0);
            this.layoutControl2.Name = "layoutControl2";
            this.layoutControl2.Root = this.layoutControlGroup2;
            this.layoutControl2.Size = new System.Drawing.Size(336, 757);
            this.layoutControl2.TabIndex = 0;
            this.layoutControl2.Text = "layoutControl2";
            // 
            // gcList
            // 
            this.gcList.Location = new System.Drawing.Point(12, 36);
            this.gcList.MainView = this.gvList;
            this.gcList.Name = "gcList";
            this.gcList.Size = new System.Drawing.Size(312, 709);
            this.gcList.TabIndex = 5;
            this.gcList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvList});
            // 
            // gvList
            // 
            this.gvList.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colAccountId,
            this.colAccountName,
            this.colInvestFund});
            this.gvList.GridControl = this.gcList;
            this.gvList.IndicatorWidth = 30;
            this.gvList.Name = "gvList";
            this.gvList.OptionsBehavior.Editable = false;
            this.gvList.OptionsView.ColumnAutoWidth = false;
            this.gvList.OptionsView.ShowGroupPanel = false;
            this.gvList.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gvList_CustomDrawRowIndicator);
            this.gvList.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gvList_FocusedRowChanged);
            // 
            // colAccountId
            // 
            this.colAccountId.Name = "colAccountId";
            // 
            // colAccountName
            // 
            this.colAccountName.Caption = "账户信息";
            this.colAccountName.FieldName = "AccountName";
            this.colAccountName.Name = "colAccountName";
            this.colAccountName.Visible = true;
            this.colAccountName.VisibleIndex = 0;
            this.colAccountName.Width = 179;
            // 
            // colInvestFund
            // 
            this.colInvestFund.Caption = "投入资金";
            this.colInvestFund.DisplayFormat.FormatString = "N0";
            this.colInvestFund.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.colInvestFund.FieldName = "InvestFund";
            this.colInvestFund.Name = "colInvestFund";
            this.colInvestFund.Visible = true;
            this.colInvestFund.VisibleIndex = 1;
            this.colInvestFund.Width = 95;
            // 
            // cbInvestor
            // 
            this.cbInvestor.Location = new System.Drawing.Point(63, 12);
            this.cbInvestor.Name = "cbInvestor";
            this.cbInvestor.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbInvestor.Size = new System.Drawing.Size(156, 20);
            this.cbInvestor.StyleController = this.layoutControl2;
            this.cbInvestor.TabIndex = 4;
            this.cbInvestor.SelectedIndexChanged += new System.EventHandler(this.cbInvestor_SelectedIndexChanged);
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup2.GroupBordersVisible = false;
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.emptySpaceItem4});
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Size = new System.Drawing.Size(336, 757);
            this.layoutControlGroup2.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.cbInvestor;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.MaxSize = new System.Drawing.Size(211, 24);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(211, 24);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(211, 24);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.Text = "投资人员";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(48, 14);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.gcList;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(316, 713);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // emptySpaceItem4
            // 
            this.emptySpaceItem4.AllowHotTrack = false;
            this.emptySpaceItem4.Location = new System.Drawing.Point(211, 0);
            this.emptySpaceItem4.Name = "emptySpaceItem4";
            this.emptySpaceItem4.Size = new System.Drawing.Size(105, 24);
            this.emptySpaceItem4.TextSize = new System.Drawing.Size(0, 0);
            // 
            // sidePanel2
            // 
            this.sidePanel2.Controls.Add(this.layoutControl3);
            this.sidePanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.sidePanel2.Location = new System.Drawing.Point(337, 0);
            this.sidePanel2.Name = "sidePanel2";
            this.sidePanel2.Size = new System.Drawing.Size(1118, 53);
            this.sidePanel2.TabIndex = 1;
            this.sidePanel2.Text = "sidePanel2";
            // 
            // layoutControl3
            // 
            this.layoutControl3.Controls.Add(this.lblDate);
            this.layoutControl3.Controls.Add(this.lblDP);
            this.layoutControl3.Controls.Add(this.lblDR);
            this.layoutControl3.Controls.Add(this.lblAP);
            this.layoutControl3.Controls.Add(this.lblAR);
            this.layoutControl3.Controls.Add(this.lblMax);
            this.layoutControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl3.Location = new System.Drawing.Point(0, 0);
            this.layoutControl3.Name = "layoutControl3";
            this.layoutControl3.Root = this.layoutControlGroup3;
            this.layoutControl3.Size = new System.Drawing.Size(1118, 52);
            this.layoutControl3.TabIndex = 0;
            this.layoutControl3.Text = "layoutControl3";
            // 
            // lblDate
            // 
            this.lblDate.Appearance.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.lblDate.Appearance.Options.UseFont = true;
            this.lblDate.Location = new System.Drawing.Point(54, 12);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(104, 26);
            this.lblDate.StyleController = this.layoutControl3;
            this.lblDate.TabIndex = 4;
            this.lblDate.Text = "labelControl1";
            // 
            // lblDP
            // 
            this.lblDP.Appearance.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.lblDP.Appearance.Options.UseFont = true;
            this.lblDP.Location = new System.Drawing.Point(230, 12);
            this.lblDP.Name = "lblDP";
            this.lblDP.Size = new System.Drawing.Size(98, 26);
            this.lblDP.StyleController = this.layoutControl3;
            this.lblDP.TabIndex = 4;
            this.lblDP.Text = "labelControl1";
            // 
            // lblDR
            // 
            this.lblDR.Appearance.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.lblDR.Appearance.Options.UseFont = true;
            this.lblDR.Location = new System.Drawing.Point(413, 12);
            this.lblDR.Name = "lblDR";
            this.lblDR.Size = new System.Drawing.Size(65, 26);
            this.lblDR.StyleController = this.layoutControl3;
            this.lblDR.TabIndex = 4;
            this.lblDR.Text = "labelControl1";
            // 
            // lblAP
            // 
            this.lblAP.Appearance.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.lblAP.Appearance.Options.UseFont = true;
            this.lblAP.Location = new System.Drawing.Point(550, 12);
            this.lblAP.Name = "lblAP";
            this.lblAP.Size = new System.Drawing.Size(98, 26);
            this.lblAP.StyleController = this.layoutControl3;
            this.lblAP.TabIndex = 4;
            this.lblAP.Text = "labelControl1";
            // 
            // lblAR
            // 
            this.lblAR.Appearance.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.lblAR.Appearance.Options.UseFont = true;
            this.lblAR.Location = new System.Drawing.Point(733, 12);
            this.lblAR.Name = "lblAR";
            this.lblAR.Size = new System.Drawing.Size(65, 26);
            this.lblAR.StyleController = this.layoutControl3;
            this.lblAR.TabIndex = 4;
            this.lblAR.Text = "labelControl1";
            // 
            // lblMax
            // 
            this.lblMax.Appearance.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.lblMax.Appearance.Options.UseFont = true;
            this.lblMax.Location = new System.Drawing.Point(883, 12);
            this.lblMax.Name = "lblMax";
            this.lblMax.Size = new System.Drawing.Size(85, 26);
            this.lblMax.StyleController = this.layoutControl3;
            this.lblMax.TabIndex = 4;
            this.lblMax.Text = "labelControl1";
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup3.GroupBordersVisible = false;
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem16,
            this.layoutControlItem12,
            this.layoutControlItem17,
            this.layoutControlItem18,
            this.layoutControlItem19,
            this.layoutControlItem20,
            this.emptySpaceItem5});
            this.layoutControlGroup3.Name = "Root";
            this.layoutControlGroup3.Size = new System.Drawing.Size(1118, 52);
            this.layoutControlGroup3.TextVisible = false;
            // 
            // layoutControlItem16
            // 
            this.layoutControlItem16.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.layoutControlItem16.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem16.Control = this.lblDP;
            this.layoutControlItem16.CustomizationFormText = "layoutControlItem10";
            this.layoutControlItem16.Location = new System.Drawing.Point(150, 0);
            this.layoutControlItem16.MaxSize = new System.Drawing.Size(170, 30);
            this.layoutControlItem16.MinSize = new System.Drawing.Size(125, 30);
            this.layoutControlItem16.Name = "layoutControlItem16";
            this.layoutControlItem16.Size = new System.Drawing.Size(170, 32);
            this.layoutControlItem16.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem16.Text = "当日盈亏：";
            this.layoutControlItem16.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.layoutControlItem16.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem16.TextSize = new System.Drawing.Size(65, 14);
            this.layoutControlItem16.TextToControlDistance = 3;
            // 
            // layoutControlItem12
            // 
            this.layoutControlItem12.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.layoutControlItem12.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem12.Control = this.lblDate;
            this.layoutControlItem12.CustomizationFormText = "layoutControlItem10";
            this.layoutControlItem12.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem12.MaxSize = new System.Drawing.Size(150, 30);
            this.layoutControlItem12.MinSize = new System.Drawing.Size(125, 30);
            this.layoutControlItem12.Name = "layoutControlItem12";
            this.layoutControlItem12.Size = new System.Drawing.Size(150, 32);
            this.layoutControlItem12.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem12.Text = "日期：";
            this.layoutControlItem12.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.layoutControlItem12.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem12.TextSize = new System.Drawing.Size(39, 14);
            this.layoutControlItem12.TextToControlDistance = 3;
            // 
            // layoutControlItem17
            // 
            this.layoutControlItem17.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.layoutControlItem17.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem17.Control = this.lblDR;
            this.layoutControlItem17.CustomizationFormText = "layoutControlItem10";
            this.layoutControlItem17.Location = new System.Drawing.Point(320, 0);
            this.layoutControlItem17.MaxSize = new System.Drawing.Size(150, 30);
            this.layoutControlItem17.MinSize = new System.Drawing.Size(125, 30);
            this.layoutControlItem17.Name = "layoutControlItem17";
            this.layoutControlItem17.Size = new System.Drawing.Size(150, 32);
            this.layoutControlItem17.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem17.Text = "当日收益率：";
            this.layoutControlItem17.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.layoutControlItem17.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem17.TextSize = new System.Drawing.Size(78, 14);
            this.layoutControlItem17.TextToControlDistance = 3;
            // 
            // layoutControlItem18
            // 
            this.layoutControlItem18.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.layoutControlItem18.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem18.Control = this.lblAP;
            this.layoutControlItem18.CustomizationFormText = "layoutControlItem10";
            this.layoutControlItem18.Location = new System.Drawing.Point(470, 0);
            this.layoutControlItem18.MaxSize = new System.Drawing.Size(170, 30);
            this.layoutControlItem18.MinSize = new System.Drawing.Size(125, 30);
            this.layoutControlItem18.Name = "layoutControlItem18";
            this.layoutControlItem18.Size = new System.Drawing.Size(170, 32);
            this.layoutControlItem18.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem18.Text = "累计盈亏：";
            this.layoutControlItem18.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.layoutControlItem18.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem18.TextSize = new System.Drawing.Size(65, 14);
            this.layoutControlItem18.TextToControlDistance = 3;
            // 
            // layoutControlItem19
            // 
            this.layoutControlItem19.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.layoutControlItem19.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem19.Control = this.lblAR;
            this.layoutControlItem19.CustomizationFormText = "layoutControlItem10";
            this.layoutControlItem19.Location = new System.Drawing.Point(640, 0);
            this.layoutControlItem19.MaxSize = new System.Drawing.Size(150, 30);
            this.layoutControlItem19.MinSize = new System.Drawing.Size(125, 30);
            this.layoutControlItem19.Name = "layoutControlItem19";
            this.layoutControlItem19.Size = new System.Drawing.Size(150, 32);
            this.layoutControlItem19.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem19.Text = "累计收益率：";
            this.layoutControlItem19.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.layoutControlItem19.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem19.TextSize = new System.Drawing.Size(78, 14);
            this.layoutControlItem19.TextToControlDistance = 3;
            // 
            // layoutControlItem20
            // 
            this.layoutControlItem20.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.layoutControlItem20.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem20.Control = this.lblMax;
            this.layoutControlItem20.CustomizationFormText = "layoutControlItem10";
            this.layoutControlItem20.Location = new System.Drawing.Point(790, 0);
            this.layoutControlItem20.MaxSize = new System.Drawing.Size(170, 30);
            this.layoutControlItem20.MinSize = new System.Drawing.Size(125, 30);
            this.layoutControlItem20.Name = "layoutControlItem20";
            this.layoutControlItem20.Size = new System.Drawing.Size(170, 32);
            this.layoutControlItem20.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem20.Text = "最大回撤率：";
            this.layoutControlItem20.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.layoutControlItem20.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem20.TextSize = new System.Drawing.Size(78, 14);
            this.layoutControlItem20.TextToControlDistance = 3;
            // 
            // emptySpaceItem5
            // 
            this.emptySpaceItem5.AllowHotTrack = false;
            this.emptySpaceItem5.Location = new System.Drawing.Point(960, 0);
            this.emptySpaceItem5.Name = "emptySpaceItem5";
            this.emptySpaceItem5.Size = new System.Drawing.Size(138, 32);
            this.emptySpaceItem5.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btnView);
            this.layoutControl1.Controls.Add(this.xtratabcontrol1);
            this.layoutControl1.Controls.Add(this.deEnd);
            this.layoutControl1.Controls.Add(this.deStart);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(337, 53);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(1118, 704);
            this.layoutControl1.TabIndex = 2;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // btnView
            // 
            this.btnView.Location = new System.Drawing.Point(394, 12);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(68, 22);
            this.btnView.StyleController = this.layoutControl1;
            this.btnView.TabIndex = 7;
            this.btnView.Text = "   查  看   ";
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // xtratabcontrol1
            // 
            this.xtratabcontrol1.AppearancePage.Header.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.xtratabcontrol1.AppearancePage.Header.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.xtratabcontrol1.AppearancePage.Header.Options.UseFont = true;
            this.xtratabcontrol1.AppearancePage.Header.Options.UseForeColor = true;
            this.xtratabcontrol1.HeaderLocation = DevExpress.XtraTab.TabHeaderLocation.Left;
            this.xtratabcontrol1.HeaderOrientation = DevExpress.XtraTab.TabOrientation.Horizontal;
            this.xtratabcontrol1.Location = new System.Drawing.Point(12, 38);
            this.xtratabcontrol1.Name = "xtratabcontrol1";
            this.xtratabcontrol1.SelectedTabPage = this.pageAccount;
            this.xtratabcontrol1.Size = new System.Drawing.Size(1094, 654);
            this.xtratabcontrol1.TabIndex = 6;
            this.xtratabcontrol1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.pageAccount,
            this.pageStock,
            this.pageTran});
            this.xtratabcontrol1.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.xtratabcontrol1_SelectedPageChanged);
            // 
            // pageAccount
            // 
            this.pageAccount.Controls.Add(this.layoutControl4);
            this.pageAccount.Name = "pageAccount";
            this.pageAccount.Size = new System.Drawing.Size(1035, 652);
            this.pageAccount.Text = "  账户  ";
            // 
            // layoutControl4
            // 
            this.layoutControl4.Controls.Add(this.chartAccount);
            this.layoutControl4.Controls.Add(this.gcAccount);
            this.layoutControl4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl4.Location = new System.Drawing.Point(0, 0);
            this.layoutControl4.Name = "layoutControl4";
            this.layoutControl4.Root = this.layoutControlGroup4;
            this.layoutControl4.Size = new System.Drawing.Size(1035, 652);
            this.layoutControl4.TabIndex = 0;
            this.layoutControl4.Text = "layoutControl4";
            // 
            // chartAccount
            // 
            xyDiagram1.AxisX.Label.Angle = -45;
            xyDiagram1.AxisX.Tickmarks.MinorVisible = false;
            xyDiagram1.AxisX.VisibleInPanesSerializable = "-1";
            xyDiagram1.AxisY.Label.TextPattern = "{V:n2}";
            xyDiagram1.AxisY.Tickmarks.MinorVisible = false;
            xyDiagram1.AxisY.VisibleInPanesSerializable = "-1";
            secondaryAxisY1.AxisID = 0;
            constantLine1.AxisValueSerializable = "0";
            constantLine1.Color = System.Drawing.Color.Red;
            constantLine1.LineStyle.DashStyle = DevExpress.XtraCharts.DashStyle.Dot;
            constantLine1.Name = "Constant Line 1";
            constantLine1.ShowInLegend = false;
            constantLine1.Title.Visible = false;
            secondaryAxisY1.ConstantLines.AddRange(new DevExpress.XtraCharts.ConstantLine[] {
            constantLine1});
            secondaryAxisY1.Label.TextPattern = "{V:0.0%}";
            secondaryAxisY1.Name = "Secondary AxisY 1";
            secondaryAxisY1.Tickmarks.MinorVisible = false;
            secondaryAxisY1.VisibleInPanesSerializable = "-1";
            xyDiagram1.SecondaryAxesY.AddRange(new DevExpress.XtraCharts.SecondaryAxisY[] {
            secondaryAxisY1});
            this.chartAccount.Diagram = xyDiagram1;
            this.chartAccount.Legend.MarkerMode = DevExpress.XtraCharts.LegendMarkerMode.CheckBox;
            this.chartAccount.Legend.Name = "Default Legend";
            this.chartAccount.Location = new System.Drawing.Point(12, 329);
            this.chartAccount.Name = "chartAccount";
            series1.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
            series1.CheckedInLegend = false;
            series1.CrosshairLabelPattern = "{S}：{V:n2}万元";
            series1.Name = "累计收益";
            splineSeriesView1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(129)))), ((int)(((byte)(189)))));
            splineSeriesView1.LineMarkerOptions.Color = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(129)))), ((int)(((byte)(189)))));
            splineSeriesView1.LineMarkerOptions.Size = 5;
            splineSeriesView1.MarkerVisibility = DevExpress.Utils.DefaultBoolean.True;
            series1.View = splineSeriesView1;
            series2.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
            series2.CrosshairLabelPattern = "{S}：{V:0.00%}";
            pointSeriesLabel1.TextPattern = "{V:0.00%}";
            series2.Label = pointSeriesLabel1;
            series2.Name = "累计收益率";
            splineSeriesView2.AxisYName = "Secondary AxisY 1";
            splineSeriesView2.Color = System.Drawing.Color.OrangeRed;
            splineSeriesView2.LineMarkerOptions.Color = System.Drawing.Color.OrangeRed;
            splineSeriesView2.LineMarkerOptions.Size = 5;
            splineSeriesView2.MarkerVisibility = DevExpress.Utils.DefaultBoolean.True;
            series2.View = splineSeriesView2;
            series3.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
            series3.CheckedInLegend = false;
            series3.CrosshairLabelPattern = "{S}：{V:n2}万元";
            series3.Name = "回撤金额";
            splineSeriesView3.Color = System.Drawing.Color.DeepSkyBlue;
            splineSeriesView3.LineMarkerOptions.Color = System.Drawing.Color.DeepSkyBlue;
            splineSeriesView3.LineMarkerOptions.Kind = DevExpress.XtraCharts.MarkerKind.Triangle;
            splineSeriesView3.LineMarkerOptions.Size = 5;
            splineSeriesView3.MarkerVisibility = DevExpress.Utils.DefaultBoolean.True;
            series3.View = splineSeriesView3;
            series4.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
            series4.CrosshairLabelPattern = "{S}：{V:0.00%}";
            pointSeriesLabel2.TextPattern = "{V:0.00%}";
            series4.Label = pointSeriesLabel2;
            series4.Name = "回撤率";
            splineSeriesView4.AxisYName = "Secondary AxisY 1";
            splineSeriesView4.Color = System.Drawing.Color.DarkGreen;
            splineSeriesView4.LineMarkerOptions.Color = System.Drawing.Color.DarkGreen;
            splineSeriesView4.LineMarkerOptions.Kind = DevExpress.XtraCharts.MarkerKind.Triangle;
            splineSeriesView4.LineMarkerOptions.Size = 5;
            splineSeriesView4.MarkerVisibility = DevExpress.Utils.DefaultBoolean.True;
            series4.View = splineSeriesView4;
            this.chartAccount.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series1,
        series2,
        series3,
        series4};
            this.chartAccount.Size = new System.Drawing.Size(1011, 311);
            this.chartAccount.TabIndex = 1;
            // 
            // gcAccount
            // 
            this.gcAccount.Location = new System.Drawing.Point(12, 12);
            this.gcAccount.MainView = this.gvAccount;
            this.gcAccount.Name = "gcAccount";
            this.gcAccount.Size = new System.Drawing.Size(1011, 313);
            this.gcAccount.TabIndex = 0;
            this.gcAccount.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvAccount});
            // 
            // gvAccount
            // 
            this.gvAccount.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.colTradeDate,
            this.colDayProfit,
            this.colDayRate,
            this.colAccProfit,
            this.colAccRate,
            this.colRetraceAmount,
            this.colRetraceRate});
            this.gvAccount.GridControl = this.gcAccount;
            this.gvAccount.IndicatorWidth = 40;
            this.gvAccount.Name = "gvAccount";
            this.gvAccount.OptionsBehavior.Editable = false;
            this.gvAccount.OptionsView.ColumnAutoWidth = false;
            this.gvAccount.OptionsView.ShowGroupedColumns = true;
            this.gvAccount.OptionsView.ShowGroupPanel = false;
            this.gvAccount.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gvAccount_CustomDrawRowIndicator);
            this.gvAccount.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.gvAccount_CustomDrawCell);
            this.gvAccount.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gvAccount_RowCellStyle);
            // 
            // gridColumn1
            // 
            this.gridColumn1.FieldName = "AccountId";
            this.gridColumn1.Name = "gridColumn1";
            // 
            // colTradeDate
            // 
            this.colTradeDate.Caption = "日期";
            this.colTradeDate.DisplayFormat.FormatString = "d";
            this.colTradeDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colTradeDate.FieldName = "TradeDate";
            this.colTradeDate.Name = "colTradeDate";
            this.colTradeDate.Visible = true;
            this.colTradeDate.VisibleIndex = 0;
            this.colTradeDate.Width = 90;
            // 
            // colDayProfit
            // 
            this.colDayProfit.Caption = "日收益(万元)";
            this.colDayProfit.DisplayFormat.FormatString = "N2";
            this.colDayProfit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.colDayProfit.FieldName = "DayProfit";
            this.colDayProfit.Name = "colDayProfit";
            this.colDayProfit.Visible = true;
            this.colDayProfit.VisibleIndex = 1;
            this.colDayProfit.Width = 120;
            // 
            // colDayRate
            // 
            this.colDayRate.Caption = "日收益率";
            this.colDayRate.DisplayFormat.FormatString = "P2";
            this.colDayRate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.colDayRate.FieldName = "DayRate";
            this.colDayRate.Name = "colDayRate";
            this.colDayRate.Visible = true;
            this.colDayRate.VisibleIndex = 2;
            // 
            // colAccProfit
            // 
            this.colAccProfit.Caption = "累计收益(万元)";
            this.colAccProfit.DisplayFormat.FormatString = "N2";
            this.colAccProfit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.colAccProfit.FieldName = "AccProfit";
            this.colAccProfit.Name = "colAccProfit";
            this.colAccProfit.Visible = true;
            this.colAccProfit.VisibleIndex = 3;
            this.colAccProfit.Width = 120;
            // 
            // colAccRate
            // 
            this.colAccRate.Caption = "累计收益率";
            this.colAccRate.DisplayFormat.FormatString = "P2";
            this.colAccRate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.colAccRate.FieldName = "AccRate";
            this.colAccRate.Name = "colAccRate";
            this.colAccRate.Visible = true;
            this.colAccRate.VisibleIndex = 4;
            // 
            // colRetraceAmount
            // 
            this.colRetraceAmount.Caption = "回撤金额(万元)";
            this.colRetraceAmount.DisplayFormat.FormatString = "N2";
            this.colRetraceAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.colRetraceAmount.FieldName = "RetraceAmount";
            this.colRetraceAmount.Name = "colRetraceAmount";
            this.colRetraceAmount.Visible = true;
            this.colRetraceAmount.VisibleIndex = 5;
            this.colRetraceAmount.Width = 120;
            // 
            // colRetraceRate
            // 
            this.colRetraceRate.Caption = "回撤率";
            this.colRetraceRate.DisplayFormat.FormatString = "P2";
            this.colRetraceRate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.colRetraceRate.FieldName = "RetraceRate";
            this.colRetraceRate.Name = "colRetraceRate";
            this.colRetraceRate.Visible = true;
            this.colRetraceRate.VisibleIndex = 6;
            // 
            // layoutControlGroup4
            // 
            this.layoutControlGroup4.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup4.GroupBordersVisible = false;
            this.layoutControlGroup4.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem7,
            this.layoutControlItem6});
            this.layoutControlGroup4.Name = "layoutControlGroup4";
            this.layoutControlGroup4.Size = new System.Drawing.Size(1035, 652);
            this.layoutControlGroup4.TextVisible = false;
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.chartAccount;
            this.layoutControlItem7.Location = new System.Drawing.Point(0, 317);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(1015, 315);
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.gcAccount;
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(1015, 317);
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextVisible = false;
            // 
            // pageStock
            // 
            this.pageStock.Controls.Add(this.layoutControl5);
            this.pageStock.Name = "pageStock";
            this.pageStock.Size = new System.Drawing.Size(1035, 652);
            this.pageStock.Text = "  单票  ";
            // 
            // layoutControl5
            // 
            this.layoutControl5.Controls.Add(this.gcStock);
            this.layoutControl5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl5.Location = new System.Drawing.Point(0, 0);
            this.layoutControl5.Name = "layoutControl5";
            this.layoutControl5.Root = this.layoutControlGroup5;
            this.layoutControl5.Size = new System.Drawing.Size(1035, 652);
            this.layoutControl5.TabIndex = 0;
            this.layoutControl5.Text = "layoutControl5";
            // 
            // gcStock
            // 
            this.gcStock.Location = new System.Drawing.Point(12, 12);
            this.gcStock.MainView = this.gvStock;
            this.gcStock.Name = "gcStock";
            this.gcStock.Size = new System.Drawing.Size(1011, 628);
            this.gcStock.TabIndex = 4;
            this.gcStock.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvStock});
            // 
            // gvStock
            // 
            this.gvStock.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colTradeId,
            this.gridColumn2,
            this.colStockCode,
            this.colStockName,
            this.gridColumn3,
            this.gridColumn4,
            this.colPeriodProfit,
            this.colPeriodRate,
            this.gridColumn5,
            this.gridColumn6});
            this.gvStock.GridControl = this.gcStock;
            this.gvStock.GroupCount = 1;
            this.gvStock.IndicatorWidth = 40;
            this.gvStock.Name = "gvStock";
            this.gvStock.OptionsBehavior.AutoExpandAllGroups = true;
            this.gvStock.OptionsBehavior.Editable = false;
            this.gvStock.OptionsView.ColumnAutoWidth = false;
            this.gvStock.OptionsView.ShowGroupedColumns = true;
            this.gvStock.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colStockName, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.gvStock.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gvStock_CustomDrawRowIndicator);
            this.gvStock.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.gvStock_CustomDrawCell);
            this.gvStock.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gvStock_RowCellStyle);
            // 
            // colTradeId
            // 
            this.colTradeId.Caption = "开仓编号";
            this.colTradeId.FieldName = "TradeId";
            this.colTradeId.Name = "colTradeId";
            this.colTradeId.Visible = true;
            this.colTradeId.VisibleIndex = 0;
            this.colTradeId.Width = 150;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "日期";
            this.gridColumn2.FieldName = "TradeDate";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 90;
            // 
            // colStockCode
            // 
            this.colStockCode.Caption = "证券代码";
            this.colStockCode.FieldName = "StockCode";
            this.colStockCode.Name = "colStockCode";
            this.colStockCode.Visible = true;
            this.colStockCode.VisibleIndex = 2;
            // 
            // colStockName
            // 
            this.colStockName.Caption = "证券名称";
            this.colStockName.FieldName = "StockName";
            this.colStockName.Name = "colStockName";
            this.colStockName.Visible = true;
            this.colStockName.VisibleIndex = 3;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "日收益(万元)";
            this.gridColumn3.DisplayFormat.FormatString = "N2";
            this.gridColumn3.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gridColumn3.FieldName = "DayProfit";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 4;
            this.gridColumn3.Width = 100;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "日收益率";
            this.gridColumn4.DisplayFormat.FormatString = "P2";
            this.gridColumn4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gridColumn4.FieldName = "DayRate";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 5;
            // 
            // colPeriodProfit
            // 
            this.colPeriodProfit.Caption = "累计收益(万元)";
            this.colPeriodProfit.DisplayFormat.FormatString = "N2";
            this.colPeriodProfit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.colPeriodProfit.FieldName = "PeriodProfit";
            this.colPeriodProfit.Name = "colPeriodProfit";
            this.colPeriodProfit.Visible = true;
            this.colPeriodProfit.VisibleIndex = 6;
            this.colPeriodProfit.Width = 100;
            // 
            // colPeriodRate
            // 
            this.colPeriodRate.Caption = "累计收益率";
            this.colPeriodRate.DisplayFormat.FormatString = "P2";
            this.colPeriodRate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.colPeriodRate.FieldName = "PeriodRate";
            this.colPeriodRate.Name = "colPeriodRate";
            this.colPeriodRate.Visible = true;
            this.colPeriodRate.VisibleIndex = 7;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "回撤金额(万元)";
            this.gridColumn5.DisplayFormat.FormatString = "N2";
            this.gridColumn5.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gridColumn5.FieldName = "RetraceAmount";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 8;
            this.gridColumn5.Width = 100;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "回撤率";
            this.gridColumn6.DisplayFormat.FormatString = "P2";
            this.gridColumn6.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gridColumn6.FieldName = "RetraceRate";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 9;
            // 
            // layoutControlGroup5
            // 
            this.layoutControlGroup5.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup5.GroupBordersVisible = false;
            this.layoutControlGroup5.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem9});
            this.layoutControlGroup5.Name = "layoutControlGroup5";
            this.layoutControlGroup5.Size = new System.Drawing.Size(1035, 652);
            this.layoutControlGroup5.TextVisible = false;
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.gcStock;
            this.layoutControlItem9.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(1015, 632);
            this.layoutControlItem9.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem9.TextVisible = false;
            // 
            // pageTran
            // 
            this.pageTran.Controls.Add(this.layoutControl6);
            this.pageTran.Name = "pageTran";
            this.pageTran.Size = new System.Drawing.Size(1035, 652);
            this.pageTran.Text = "  单笔  ";
            // 
            // layoutControl6
            // 
            this.layoutControl6.Controls.Add(this.gcTrans);
            this.layoutControl6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl6.Location = new System.Drawing.Point(0, 0);
            this.layoutControl6.Name = "layoutControl6";
            this.layoutControl6.Root = this.layoutControlGroup6;
            this.layoutControl6.Size = new System.Drawing.Size(1035, 652);
            this.layoutControl6.TabIndex = 0;
            this.layoutControl6.Text = "layoutControl6";
            // 
            // gcTrans
            // 
            this.gcTrans.Location = new System.Drawing.Point(12, 12);
            this.gcTrans.MainView = this.gvTrans;
            this.gcTrans.Name = "gcTrans";
            this.gcTrans.Size = new System.Drawing.Size(1011, 628);
            this.gcTrans.TabIndex = 4;
            this.gcTrans.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvTrans});
            // 
            // gvTrans
            // 
            this.gvTrans.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn7,
            this.gridColumn8,
            this.colTransactionId,
            this.gridColumn9,
            this.gridColumn10,
            this.gridColumn11,
            this.gridColumn12,
            this.gridColumn13,
            this.gridColumn14,
            this.gridColumn15});
            this.gvTrans.GridControl = this.gcTrans;
            this.gvTrans.GroupCount = 2;
            this.gvTrans.IndicatorWidth = 40;
            this.gvTrans.Name = "gvTrans";
            this.gvTrans.OptionsBehavior.AutoExpandAllGroups = true;
            this.gvTrans.OptionsBehavior.Editable = false;
            this.gvTrans.OptionsView.ColumnAutoWidth = false;
            this.gvTrans.OptionsView.ShowGroupedColumns = true;
            this.gvTrans.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gridColumn8, DevExpress.Data.ColumnSortOrder.Ascending),
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colTransactionId, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.gvTrans.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gvTrans_CustomDrawRowIndicator);
            this.gvTrans.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.gvTrans_CustomDrawCell);
            this.gvTrans.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gvTrans_RowCellStyle);
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "证券代码";
            this.gridColumn7.FieldName = "StockCode";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 1;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "证券名称";
            this.gridColumn8.FieldName = "StockName";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 2;
            // 
            // colTransactionId
            // 
            this.colTransactionId.Caption = "单笔编号";
            this.colTransactionId.FieldName = "TransactionId";
            this.colTransactionId.Name = "colTransactionId";
            this.colTransactionId.Visible = true;
            this.colTransactionId.VisibleIndex = 0;
            this.colTransactionId.Width = 180;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "日期";
            this.gridColumn9.FieldName = "TradeDate";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 3;
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "日收益(万元)";
            this.gridColumn10.DisplayFormat.FormatString = "N2";
            this.gridColumn10.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gridColumn10.FieldName = "DayProfit";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 4;
            this.gridColumn10.Width = 100;
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "日收益率";
            this.gridColumn11.DisplayFormat.FormatString = "P2";
            this.gridColumn11.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gridColumn11.FieldName = "DayRate";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.Visible = true;
            this.gridColumn11.VisibleIndex = 5;
            // 
            // gridColumn12
            // 
            this.gridColumn12.Caption = "累计收益(万元)";
            this.gridColumn12.DisplayFormat.FormatString = "N2";
            this.gridColumn12.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gridColumn12.FieldName = "AccProfit";
            this.gridColumn12.Name = "gridColumn12";
            this.gridColumn12.Visible = true;
            this.gridColumn12.VisibleIndex = 6;
            this.gridColumn12.Width = 100;
            // 
            // gridColumn13
            // 
            this.gridColumn13.Caption = "累计收益率";
            this.gridColumn13.DisplayFormat.FormatString = "P2";
            this.gridColumn13.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gridColumn13.FieldName = "AccRate";
            this.gridColumn13.Name = "gridColumn13";
            this.gridColumn13.Visible = true;
            this.gridColumn13.VisibleIndex = 7;
            // 
            // gridColumn14
            // 
            this.gridColumn14.Caption = "回撤金额(万元)";
            this.gridColumn14.DisplayFormat.FormatString = "N2";
            this.gridColumn14.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gridColumn14.FieldName = "RetraceAmount";
            this.gridColumn14.Name = "gridColumn14";
            this.gridColumn14.Visible = true;
            this.gridColumn14.VisibleIndex = 8;
            this.gridColumn14.Width = 100;
            // 
            // gridColumn15
            // 
            this.gridColumn15.Caption = "回撤率";
            this.gridColumn15.DisplayFormat.FormatString = "P2";
            this.gridColumn15.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gridColumn15.FieldName = "RetraceRate";
            this.gridColumn15.Name = "gridColumn15";
            this.gridColumn15.Visible = true;
            this.gridColumn15.VisibleIndex = 9;
            // 
            // layoutControlGroup6
            // 
            this.layoutControlGroup6.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup6.GroupBordersVisible = false;
            this.layoutControlGroup6.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem11});
            this.layoutControlGroup6.Name = "layoutControlGroup6";
            this.layoutControlGroup6.Size = new System.Drawing.Size(1035, 652);
            this.layoutControlGroup6.TextVisible = false;
            // 
            // layoutControlItem11
            // 
            this.layoutControlItem11.Control = this.gcTrans;
            this.layoutControlItem11.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem11.Name = "layoutControlItem11";
            this.layoutControlItem11.Size = new System.Drawing.Size(1015, 632);
            this.layoutControlItem11.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem11.TextVisible = false;
            // 
            // deEnd
            // 
            this.deEnd.EditValue = null;
            this.deEnd.Location = new System.Drawing.Point(254, 12);
            this.deEnd.Name = "deEnd";
            this.deEnd.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deEnd.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deEnd.Size = new System.Drawing.Size(125, 20);
            this.deEnd.StyleController = this.layoutControl1;
            this.deEnd.TabIndex = 5;
            // 
            // deStart
            // 
            this.deStart.EditValue = null;
            this.deStart.Location = new System.Drawing.Point(63, 12);
            this.deStart.Name = "deStart";
            this.deStart.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deStart.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deStart.Size = new System.Drawing.Size(125, 20);
            this.deStart.StyleController = this.layoutControl1;
            this.deStart.TabIndex = 4;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.emptySpaceItem1,
            this.emptySpaceItem2,
            this.layoutControlItem8,
            this.emptySpaceItem3});
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(1118, 704);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.deStart;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.MaxSize = new System.Drawing.Size(180, 24);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(180, 24);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(180, 26);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.Text = "开始日期";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(48, 14);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.deEnd;
            this.layoutControlItem4.Location = new System.Drawing.Point(191, 0);
            this.layoutControlItem4.MaxSize = new System.Drawing.Size(180, 24);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(180, 24);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(180, 26);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.Text = "结束日期";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(48, 14);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.xtratabcontrol1;
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(1098, 658);
            this.layoutControlItem5.Text = "收益及回撤报表";
            this.layoutControlItem5.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(180, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(11, 26);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(454, 0);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(644, 26);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.btnView;
            this.layoutControlItem8.Location = new System.Drawing.Point(382, 0);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(72, 26);
            this.layoutControlItem8.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem8.TextVisible = false;
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            this.emptySpaceItem3.Location = new System.Drawing.Point(371, 0);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(11, 26);
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // FrmAccountRC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1455, 757);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.sidePanel2);
            this.Controls.Add(this.sidePanel1);
            this.Name = "FrmAccountRC";
            this.Text = "FrmAccountRC";
            this.Load += new System.EventHandler(this.FrmAccountRC_Load);
            this.sidePanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).EndInit();
            this.layoutControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbInvestor.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).EndInit();
            this.sidePanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl3)).EndInit();
            this.layoutControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem16)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem17)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem18)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem19)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem20)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xtratabcontrol1)).EndInit();
            this.xtratabcontrol1.ResumeLayout(false);
            this.pageAccount.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl4)).EndInit();
            this.layoutControl4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(secondaryAxisY1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(splineSeriesView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(pointSeriesLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(splineSeriesView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(splineSeriesView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(pointSeriesLabel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(splineSeriesView4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartAccount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcAccount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvAccount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            this.pageStock.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl5)).EndInit();
            this.layoutControl5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcStock)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvStock)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            this.pageTran.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl6)).EndInit();
            this.layoutControl6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcTrans)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvTrans)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deEnd.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deEnd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deStart.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deStart.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SidePanel sidePanel1;
        private DevExpress.XtraEditors.SidePanel sidePanel2;
        private DevExpress.XtraLayout.LayoutControl layoutControl3;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraLayout.LayoutControl layoutControl2;
        private DevExpress.XtraEditors.ComboBoxEdit cbInvestor;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraGrid.GridControl gcList;
        private DevExpress.XtraGrid.Views.Grid.GridView gvList;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraGrid.Columns.GridColumn colAccountId;
        private DevExpress.XtraGrid.Columns.GridColumn colInvestFund;
        private DevExpress.XtraGrid.Columns.GridColumn colAccountName;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraTab.XtraTabControl xtratabcontrol1;
        private DevExpress.XtraTab.XtraTabPage pageAccount;
        private DevExpress.XtraCharts.ChartControl chartAccount;
        private DevExpress.XtraGrid.GridControl gcAccount;
        private DevExpress.XtraGrid.Views.Grid.GridView gvAccount;
        private DevExpress.XtraTab.XtraTabPage pageStock;
        private DevExpress.XtraTab.XtraTabPage pageTran;
        private DevExpress.XtraEditors.DateEdit deEnd;
        private DevExpress.XtraEditors.DateEdit deStart;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraGrid.Columns.GridColumn colTradeDate;
        private DevExpress.XtraGrid.Columns.GridColumn colDayProfit;
        private DevExpress.XtraGrid.Columns.GridColumn colDayRate;
        private DevExpress.XtraGrid.Columns.GridColumn colAccProfit;
        private DevExpress.XtraGrid.Columns.GridColumn colAccRate;
        private DevExpress.XtraGrid.Columns.GridColumn colRetraceAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colRetraceRate;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraLayout.LayoutControl layoutControl4;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraEditors.SimpleButton btnView;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem4;
        private DevExpress.XtraLayout.LayoutControl layoutControl5;
        private DevExpress.XtraGrid.GridControl gcStock;
        private DevExpress.XtraGrid.Views.Grid.GridView gvStock;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private DevExpress.XtraLayout.LayoutControl layoutControl6;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup6;
        private DevExpress.XtraGrid.Columns.GridColumn colTradeId;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn colStockCode;
        private DevExpress.XtraGrid.Columns.GridColumn colStockName;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn colPeriodProfit;
        private DevExpress.XtraGrid.Columns.GridColumn colPeriodRate;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.GridControl gcTrans;
        private DevExpress.XtraGrid.Views.Grid.GridView gvTrans;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem11;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn colTransactionId;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn14;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn15;
        private DevExpress.XtraEditors.LabelControl lblDate;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem12;
        private DevExpress.XtraEditors.LabelControl lblDP;
        private DevExpress.XtraEditors.LabelControl lblDR;
        private DevExpress.XtraEditors.LabelControl lblAP;
        private DevExpress.XtraEditors.LabelControl lblAR;
        private DevExpress.XtraEditors.LabelControl lblMax;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem16;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem17;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem18;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem19;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem20;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem5;
    }
}