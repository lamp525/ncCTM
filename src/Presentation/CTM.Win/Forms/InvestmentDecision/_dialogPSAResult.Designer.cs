namespace CTM.Win.Forms.InvestmentDecision
{
    partial class _dialogPSAResult
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
            DevExpress.XtraGrid.GridLevelNode gridLevelNode2 = new DevExpress.XtraGrid.GridLevelNode();
            this.gridViewDetail = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colInvestorCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colInvestorName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTradeTypeName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDecisionName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPriceRange = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDealRange = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemTextEdit4 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colDealAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemTextEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colReason = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAccuracy = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridViewSummary = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colId_S = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSerialNo_S = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPrincipal_S = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPrincipalName_S = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStockCode_S = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStockName_S = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTradeType_S = new DevExpress.XtraGrid.Columns.GridColumn();
            this.riImageComboBoxTradeType = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.colDealRange_S = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colDealAmount_S = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemTextEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colDecision_S = new DevExpress.XtraGrid.Columns.GridColumn();
            this.riImageComboBoxDecision = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.colPriceRange_S = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colReason_S = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAccuracy_S = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btnRefresh = new DevExpress.XtraEditors.SimpleButton();
            this.btnExpandOrCollapse = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lciResult = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.esiTitle = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewSummary)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riImageComboBoxTradeType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riImageComboBoxDecision)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciResult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.esiTitle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // gridViewDetail
            // 
            this.gridViewDetail.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colInvestorCode,
            this.colInvestorName,
            this.colTradeTypeName,
            this.colDecisionName,
            this.colPriceRange,
            this.colDealRange,
            this.colDealAmount,
            this.colReason,
            this.colAccuracy});
            this.gridViewDetail.GridControl = this.gridControl1;
            this.gridViewDetail.Name = "gridViewDetail";
            this.gridViewDetail.OptionsView.ColumnAutoWidth = false;
            this.gridViewDetail.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gridViewDetail_CustomDrawRowIndicator);
            // 
            // colInvestorCode
            // 
            this.colInvestorCode.Caption = "编码";
            this.colInvestorCode.FieldName = "InvestorCode";
            this.colInvestorCode.Name = "colInvestorCode";
            this.colInvestorCode.Visible = true;
            this.colInvestorCode.VisibleIndex = 0;
            // 
            // colInvestorName
            // 
            this.colInvestorName.Caption = "投资人员";
            this.colInvestorName.FieldName = "InvestorName";
            this.colInvestorName.Name = "colInvestorName";
            this.colInvestorName.Visible = true;
            this.colInvestorName.VisibleIndex = 1;
            // 
            // colTradeTypeName
            // 
            this.colTradeTypeName.Caption = "操作类型";
            this.colTradeTypeName.FieldName = "TradeTypeName";
            this.colTradeTypeName.Name = "colTradeTypeName";
            this.colTradeTypeName.Visible = true;
            this.colTradeTypeName.VisibleIndex = 2;
            // 
            // colDecisionName
            // 
            this.colDecisionName.Caption = "决策建议";
            this.colDecisionName.FieldName = "DecisionName";
            this.colDecisionName.Name = "colDecisionName";
            this.colDecisionName.Visible = true;
            this.colDecisionName.VisibleIndex = 3;
            // 
            // colPriceRange
            // 
            this.colPriceRange.Caption = "价格区间";
            this.colPriceRange.FieldName = "PriceRange";
            this.colPriceRange.Name = "colPriceRange";
            this.colPriceRange.Visible = true;
            this.colPriceRange.VisibleIndex = 4;
            this.colPriceRange.Width = 120;
            // 
            // colDealRange
            // 
            this.colDealRange.Caption = "幅度（%）";
            this.colDealRange.ColumnEdit = this.repositoryItemTextEdit4;
            this.colDealRange.DisplayFormat.FormatString = "###################################0";
            this.colDealRange.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colDealRange.FieldName = "DealRange";
            this.colDealRange.Name = "colDealRange";
            this.colDealRange.Visible = true;
            this.colDealRange.VisibleIndex = 5;
            // 
            // repositoryItemTextEdit4
            // 
            this.repositoryItemTextEdit4.AutoHeight = false;
            this.repositoryItemTextEdit4.DisplayFormat.FormatString = "###################################0";
            this.repositoryItemTextEdit4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.repositoryItemTextEdit4.EditFormat.FormatString = "###################################0";
            this.repositoryItemTextEdit4.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.repositoryItemTextEdit4.Name = "repositoryItemTextEdit4";
            // 
            // colDealAmount
            // 
            this.colDealAmount.Caption = "金额（万元）";
            this.colDealAmount.ColumnEdit = this.repositoryItemTextEdit3;
            this.colDealAmount.DisplayFormat.FormatString = "###################################0";
            this.colDealAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colDealAmount.FieldName = "DealAmount";
            this.colDealAmount.Name = "colDealAmount";
            this.colDealAmount.Visible = true;
            this.colDealAmount.VisibleIndex = 6;
            this.colDealAmount.Width = 95;
            // 
            // repositoryItemTextEdit3
            // 
            this.repositoryItemTextEdit3.AutoHeight = false;
            this.repositoryItemTextEdit3.DisplayFormat.FormatString = "###################################0";
            this.repositoryItemTextEdit3.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.repositoryItemTextEdit3.EditFormat.FormatString = "###################################0";
            this.repositoryItemTextEdit3.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.repositoryItemTextEdit3.Name = "repositoryItemTextEdit3";
            // 
            // colReason
            // 
            this.colReason.Caption = "判断理由";
            this.colReason.FieldName = "Reason";
            this.colReason.Name = "colReason";
            this.colReason.Visible = true;
            this.colReason.VisibleIndex = 7;
            this.colReason.Width = 450;
            // 
            // colAccuracy
            // 
            this.colAccuracy.Caption = "正确判断";
            this.colAccuracy.FieldName = "Accuracy";
            this.colAccuracy.Name = "colAccuracy";
            this.colAccuracy.Visible = true;
            this.colAccuracy.VisibleIndex = 8;
            this.colAccuracy.Width = 130;
            // 
            // gridControl1
            // 
            gridLevelNode2.LevelTemplate = this.gridViewDetail;
            gridLevelNode2.RelationName = "SummaryDetail";
            this.gridControl1.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode2});
            this.gridControl1.Location = new System.Drawing.Point(12, 68);
            this.gridControl1.MainView = this.gridViewSummary;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.riImageComboBoxTradeType,
            this.riImageComboBoxDecision,
            this.repositoryItemTextEdit1,
            this.repositoryItemTextEdit2,
            this.repositoryItemTextEdit3,
            this.repositoryItemTextEdit4});
            this.gridControl1.Size = new System.Drawing.Size(1419, 628);
            this.gridControl1.TabIndex = 4;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewSummary,
            this.gridViewDetail});
            // 
            // gridViewSummary
            // 
            this.gridViewSummary.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colId_S,
            this.colSerialNo_S,
            this.colPrincipal_S,
            this.colPrincipalName_S,
            this.colStockCode_S,
            this.colStockName_S,
            this.colTradeType_S,
            this.colDealRange_S,
            this.colDealAmount_S,
            this.colDecision_S,
            this.colPriceRange_S,
            this.colReason_S,
            this.colAccuracy_S});
            this.gridViewSummary.GridControl = this.gridControl1;
            this.gridViewSummary.Name = "gridViewSummary";
            this.gridViewSummary.OptionsView.ColumnAutoWidth = false;
            this.gridViewSummary.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gridViewSummary_CustomDrawRowIndicator);
            this.gridViewSummary.CustomRowCellEdit += new DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventHandler(this.gridViewSummary_CustomRowCellEdit);
            this.gridViewSummary.ShowingEditor += new System.ComponentModel.CancelEventHandler(this.gridViewSummary_ShowingEditor);
            this.gridViewSummary.RowUpdated += new DevExpress.XtraGrid.Views.Base.RowObjectEventHandler(this.gridViewSummary_RowUpdated);
            // 
            // colId_S
            // 
            this.colId_S.FieldName = "Id";
            this.colId_S.Name = "colId_S";
            // 
            // colSerialNo_S
            // 
            this.colSerialNo_S.FieldName = "SerialNo";
            this.colSerialNo_S.Name = "colSerialNo_S";
            // 
            // colPrincipal_S
            // 
            this.colPrincipal_S.FieldName = "Principal";
            this.colPrincipal_S.Name = "colPrincipal_S";
            // 
            // colPrincipalName_S
            // 
            this.colPrincipalName_S.Caption = "主要负责人";
            this.colPrincipalName_S.FieldName = "PrincipalName";
            this.colPrincipalName_S.Name = "colPrincipalName_S";
            this.colPrincipalName_S.Visible = true;
            this.colPrincipalName_S.VisibleIndex = 2;
            this.colPrincipalName_S.Width = 100;
            // 
            // colStockCode_S
            // 
            this.colStockCode_S.Caption = "股票代码";
            this.colStockCode_S.FieldName = "StockCode";
            this.colStockCode_S.Name = "colStockCode_S";
            this.colStockCode_S.Visible = true;
            this.colStockCode_S.VisibleIndex = 0;
            this.colStockCode_S.Width = 100;
            // 
            // colStockName_S
            // 
            this.colStockName_S.Caption = "股票名称";
            this.colStockName_S.FieldName = "StockName";
            this.colStockName_S.Name = "colStockName_S";
            this.colStockName_S.Visible = true;
            this.colStockName_S.VisibleIndex = 1;
            this.colStockName_S.Width = 100;
            // 
            // colTradeType_S
            // 
            this.colTradeType_S.Caption = "操作类型";
            this.colTradeType_S.ColumnEdit = this.riImageComboBoxTradeType;
            this.colTradeType_S.FieldName = "TradeType";
            this.colTradeType_S.Name = "colTradeType_S";
            this.colTradeType_S.Visible = true;
            this.colTradeType_S.VisibleIndex = 3;
            // 
            // riImageComboBoxTradeType
            // 
            this.riImageComboBoxTradeType.AutoHeight = false;
            this.riImageComboBoxTradeType.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.riImageComboBoxTradeType.Name = "riImageComboBoxTradeType";
            // 
            // colDealRange_S
            // 
            this.colDealRange_S.Caption = "幅度（%）";
            this.colDealRange_S.ColumnEdit = this.repositoryItemTextEdit1;
            this.colDealRange_S.DisplayFormat.FormatString = "###################################0";
            this.colDealRange_S.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colDealRange_S.FieldName = "DealRange";
            this.colDealRange_S.Name = "colDealRange_S";
            this.colDealRange_S.Visible = true;
            this.colDealRange_S.VisibleIndex = 6;
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
            // colDealAmount_S
            // 
            this.colDealAmount_S.Caption = "金额（万元）";
            this.colDealAmount_S.ColumnEdit = this.repositoryItemTextEdit2;
            this.colDealAmount_S.DisplayFormat.FormatString = "###################################0";
            this.colDealAmount_S.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colDealAmount_S.FieldName = "DealAmount";
            this.colDealAmount_S.Name = "colDealAmount_S";
            this.colDealAmount_S.Visible = true;
            this.colDealAmount_S.VisibleIndex = 7;
            this.colDealAmount_S.Width = 101;
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
            // colDecision_S
            // 
            this.colDecision_S.Caption = "决策建议";
            this.colDecision_S.ColumnEdit = this.riImageComboBoxDecision;
            this.colDecision_S.FieldName = "Decision";
            this.colDecision_S.Name = "colDecision_S";
            this.colDecision_S.Visible = true;
            this.colDecision_S.VisibleIndex = 4;
            this.colDecision_S.Width = 100;
            // 
            // riImageComboBoxDecision
            // 
            this.riImageComboBoxDecision.AutoHeight = false;
            this.riImageComboBoxDecision.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.riImageComboBoxDecision.Name = "riImageComboBoxDecision";
            // 
            // colPriceRange_S
            // 
            this.colPriceRange_S.Caption = "价格区间";
            this.colPriceRange_S.FieldName = "PriceRange";
            this.colPriceRange_S.Name = "colPriceRange_S";
            this.colPriceRange_S.Visible = true;
            this.colPriceRange_S.VisibleIndex = 5;
            this.colPriceRange_S.Width = 130;
            // 
            // colReason_S
            // 
            this.colReason_S.Caption = "判断理由";
            this.colReason_S.FieldName = "Reason";
            this.colReason_S.Name = "colReason_S";
            this.colReason_S.Visible = true;
            this.colReason_S.VisibleIndex = 8;
            this.colReason_S.Width = 450;
            // 
            // colAccuracy_S
            // 
            this.colAccuracy_S.Caption = "正确判断";
            this.colAccuracy_S.FieldName = "Accuracy";
            this.colAccuracy_S.Name = "colAccuracy_S";
            this.colAccuracy_S.Visible = true;
            this.colAccuracy_S.VisibleIndex = 9;
            this.colAccuracy_S.Width = 130;
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btnRefresh);
            this.layoutControl1.Controls.Add(this.btnExpandOrCollapse);
            this.layoutControl1.Controls.Add(this.gridControl1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(1443, 708);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(125, 42);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 22);
            this.btnRefresh.StyleController = this.layoutControl1;
            this.btnRefresh.TabIndex = 6;
            this.btnRefresh.Text = "    刷  新    ";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnExpandOrCollapse
            // 
            this.btnExpandOrCollapse.Location = new System.Drawing.Point(12, 42);
            this.btnExpandOrCollapse.Name = "btnExpandOrCollapse";
            this.btnExpandOrCollapse.Size = new System.Drawing.Size(99, 22);
            this.btnExpandOrCollapse.StyleController = this.layoutControl1;
            this.btnExpandOrCollapse.TabIndex = 5;
            this.btnExpandOrCollapse.Text = "全部展开/收起";
            this.btnExpandOrCollapse.Click += new System.EventHandler(this.btnExpandOrCollapse_Click);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lciResult,
            this.emptySpaceItem3,
            this.layoutControlItem1,
            this.esiTitle,
            this.layoutControlItem2,
            this.emptySpaceItem1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(1443, 708);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // lciResult
            // 
            this.lciResult.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.lciResult.AppearanceItemCaption.Options.UseFont = true;
            this.lciResult.AppearanceItemCaption.Options.UseTextOptions = true;
            this.lciResult.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lciResult.Control = this.gridControl1;
            this.lciResult.Location = new System.Drawing.Point(0, 56);
            this.lciResult.Name = "lciResult";
            this.lciResult.Size = new System.Drawing.Size(1423, 632);
            this.lciResult.TextLocation = DevExpress.Utils.Locations.Top;
            this.lciResult.TextSize = new System.Drawing.Size(0, 0);
            this.lciResult.TextVisible = false;
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            this.emptySpaceItem3.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.emptySpaceItem3.AppearanceItemCaption.Options.UseFont = true;
            this.emptySpaceItem3.Location = new System.Drawing.Point(192, 30);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(1231, 26);
            this.emptySpaceItem3.Text = "（各股票负责人请填写汇总操作建议。）";
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            this.emptySpaceItem3.TextVisible = true;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.btnExpandOrCollapse;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 30);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(103, 26);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
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
            this.esiTitle.Size = new System.Drawing.Size(1423, 30);
            this.esiTitle.TextSize = new System.Drawing.Size(0, 0);
            this.esiTitle.TextVisible = true;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btnRefresh;
            this.layoutControlItem2.Location = new System.Drawing.Point(113, 30);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(79, 26);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(103, 30);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(10, 26);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // _dialogPSAResult
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1443, 708);
            this.Controls.Add(this.layoutControl1);
            this.Name = "_dialogPSAResult";
            this.Text = "_dialogPSAResult";
            this.Load += new System.EventHandler(this._dialogPSAResult_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridViewDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewSummary)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riImageComboBoxTradeType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riImageComboBoxDecision)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciResult)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.esiTitle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewSummary;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem lciResult;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private DevExpress.XtraGrid.Columns.GridColumn colSerialNo_S;
        private DevExpress.XtraGrid.Columns.GridColumn colPrincipalName_S;
        private DevExpress.XtraGrid.Columns.GridColumn colStockCode_S;
        private DevExpress.XtraGrid.Columns.GridColumn colStockName_S;
        private DevExpress.XtraGrid.Columns.GridColumn colTradeType_S;
        private DevExpress.XtraGrid.Columns.GridColumn colDecision_S;
        private DevExpress.XtraGrid.Columns.GridColumn colPriceRange_S;
        private DevExpress.XtraGrid.Columns.GridColumn colReason_S;
        private DevExpress.XtraGrid.Columns.GridColumn colAccuracy_S;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewDetail;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox riImageComboBoxTradeType;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox riImageComboBoxDecision;
        private DevExpress.XtraGrid.Columns.GridColumn colInvestorCode;
        private DevExpress.XtraGrid.Columns.GridColumn colInvestorName;
        private DevExpress.XtraGrid.Columns.GridColumn colTradeTypeName;
        private DevExpress.XtraGrid.Columns.GridColumn colDecisionName;
        private DevExpress.XtraGrid.Columns.GridColumn colPriceRange;
        private DevExpress.XtraGrid.Columns.GridColumn colReason;
        private DevExpress.XtraGrid.Columns.GridColumn colAccuracy;
        private DevExpress.XtraGrid.Columns.GridColumn colId_S;
        private DevExpress.XtraEditors.SimpleButton btnExpandOrCollapse;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.EmptySpaceItem esiTitle;
        private DevExpress.XtraGrid.Columns.GridColumn colDealRange_S;
        private DevExpress.XtraGrid.Columns.GridColumn colDealAmount_S;
        private DevExpress.XtraGrid.Columns.GridColumn colDealRange;
        private DevExpress.XtraGrid.Columns.GridColumn colDealAmount;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit4;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit3;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit2;
        private DevExpress.XtraEditors.SimpleButton btnRefresh;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraGrid.Columns.GridColumn colPrincipal_S;
    }
}