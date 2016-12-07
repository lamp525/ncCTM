namespace CTM.Win.Forms.InvestmentDecision
{
    partial class _embedIDApplication
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
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            this.viewMyApplyDetail = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridMyApply = new DevExpress.XtraGrid.GridControl();
            this.viewMyApplyMaster = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTradePlanNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colApplyNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colApplyUser = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colApplyUserName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDepartmentId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDepartmentName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colApplyDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStockCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStockName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTradeType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTradeTypeName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStopProfitPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStopProfitBound = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStopLossPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStopLossBound = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStatusName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOperate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.riButtonEditOperate = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.colCreateTime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUpdateTime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btnRefresh = new DevExpress.XtraEditors.SimpleButton();
            this.btnSaveLayout = new DevExpress.XtraEditors.SimpleButton();
            this.btnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.btnAdd = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciDelete = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem11 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem9 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.viewMyApplyDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridMyApply)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewMyApplyMaster)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riButtonEditOperate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciDelete)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            this.SuspendLayout();
            // 
            // viewMyApplyDetail
            // 
            this.viewMyApplyDetail.GridControl = this.gridMyApply;
            this.viewMyApplyDetail.Name = "viewMyApplyDetail";
            // 
            // gridMyApply
            // 
            gridLevelNode1.LevelTemplate = this.viewMyApplyDetail;
            gridLevelNode1.RelationName = "MD";
            this.gridMyApply.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.gridMyApply.Location = new System.Drawing.Point(12, 38);
            this.gridMyApply.MainView = this.viewMyApplyMaster;
            this.gridMyApply.Name = "gridMyApply";
            this.gridMyApply.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.riButtonEditOperate});
            this.gridMyApply.Size = new System.Drawing.Size(1518, 709);
            this.gridMyApply.TabIndex = 4;
            this.gridMyApply.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.viewMyApplyMaster,
            this.viewMyApplyDetail});
            // 
            // viewMyApplyMaster
            // 
            this.viewMyApplyMaster.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colId,
            this.colTradePlanNo,
            this.colApplyNo,
            this.colApplyUser,
            this.colApplyUserName,
            this.colDepartmentId,
            this.colDepartmentName,
            this.colApplyDate,
            this.colStockCode,
            this.colStockName,
            this.colTradeType,
            this.colTradeTypeName,
            this.colStopProfitPrice,
            this.colStopProfitBound,
            this.colStopLossPrice,
            this.colStopLossBound,
            this.colStatusName,
            this.colStatus,
            this.colOperate,
            this.colCreateTime,
            this.colUpdateTime});
            this.viewMyApplyMaster.GridControl = this.gridMyApply;
            this.viewMyApplyMaster.IndicatorWidth = 50;
            this.viewMyApplyMaster.Name = "viewMyApplyMaster";
            this.viewMyApplyMaster.OptionsView.ColumnAutoWidth = false;
            this.viewMyApplyMaster.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.viewMyApplyMaster_CustomDrawRowIndicator);
            this.viewMyApplyMaster.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.viewMyApplyMaster_CustomDrawCell);
            this.viewMyApplyMaster.SelectionChanged += new DevExpress.Data.SelectionChangedEventHandler(this.viewMyApplyMaster_SelectionChanged);
            // 
            // colId
            // 
            this.colId.FieldName = "Id";
            this.colId.Name = "colId";
            this.colId.OptionsColumn.AllowEdit = false;
            // 
            // colTradePlanNo
            // 
            this.colTradePlanNo.Caption = "关联计划单号";
            this.colTradePlanNo.FieldName = "TradePlanNo";
            this.colTradePlanNo.Name = "colTradePlanNo";
            this.colTradePlanNo.OptionsColumn.AllowEdit = false;
            this.colTradePlanNo.Visible = true;
            this.colTradePlanNo.VisibleIndex = 15;
            this.colTradePlanNo.Width = 86;
            // 
            // colApplyNo
            // 
            this.colApplyNo.Caption = "申请编号";
            this.colApplyNo.FieldName = "ApplyNo";
            this.colApplyNo.Name = "colApplyNo";
            this.colApplyNo.OptionsColumn.AllowEdit = false;
            this.colApplyNo.Visible = true;
            this.colApplyNo.VisibleIndex = 0;
            this.colApplyNo.Width = 90;
            // 
            // colApplyUser
            // 
            this.colApplyUser.FieldName = "ApplyUser";
            this.colApplyUser.Name = "colApplyUser";
            this.colApplyUser.OptionsColumn.AllowEdit = false;
            // 
            // colApplyUserName
            // 
            this.colApplyUserName.Caption = "申请人";
            this.colApplyUserName.FieldName = "ApplyUserName";
            this.colApplyUserName.Name = "colApplyUserName";
            this.colApplyUserName.OptionsColumn.AllowEdit = false;
            this.colApplyUserName.Visible = true;
            this.colApplyUserName.VisibleIndex = 2;
            this.colApplyUserName.Width = 60;
            // 
            // colDepartmentId
            // 
            this.colDepartmentId.Caption = "Department Id";
            this.colDepartmentId.FieldName = "DepartmentId";
            this.colDepartmentId.Name = "colDepartmentId";
            this.colDepartmentId.OptionsColumn.AllowEdit = false;
            // 
            // colDepartmentName
            // 
            this.colDepartmentName.Caption = "部门名称";
            this.colDepartmentName.FieldName = "DepartmentName";
            this.colDepartmentName.Name = "colDepartmentName";
            this.colDepartmentName.OptionsColumn.AllowEdit = false;
            this.colDepartmentName.Visible = true;
            this.colDepartmentName.VisibleIndex = 3;
            // 
            // colApplyDate
            // 
            this.colApplyDate.Caption = "申请日期";
            this.colApplyDate.FieldName = "ApplyDate";
            this.colApplyDate.Name = "colApplyDate";
            this.colApplyDate.OptionsColumn.AllowEdit = false;
            this.colApplyDate.Visible = true;
            this.colApplyDate.VisibleIndex = 1;
            this.colApplyDate.Width = 85;
            // 
            // colStockCode
            // 
            this.colStockCode.Caption = "股票代码";
            this.colStockCode.FieldName = "StockCode";
            this.colStockCode.Name = "colStockCode";
            this.colStockCode.OptionsColumn.AllowEdit = false;
            this.colStockCode.Visible = true;
            this.colStockCode.VisibleIndex = 4;
            // 
            // colStockName
            // 
            this.colStockName.Caption = "股票名称";
            this.colStockName.FieldName = "StockName";
            this.colStockName.Name = "colStockName";
            this.colStockName.OptionsColumn.AllowEdit = false;
            this.colStockName.Visible = true;
            this.colStockName.VisibleIndex = 5;
            // 
            // colTradeType
            // 
            this.colTradeType.FieldName = "TradeType";
            this.colTradeType.Name = "colTradeType";
            this.colTradeType.OptionsColumn.AllowEdit = false;
            // 
            // colTradeTypeName
            // 
            this.colTradeTypeName.Caption = "交易类别";
            this.colTradeTypeName.FieldName = "TradeTypeName";
            this.colTradeTypeName.Name = "colTradeTypeName";
            this.colTradeTypeName.OptionsColumn.AllowEdit = false;
            this.colTradeTypeName.Visible = true;
            this.colTradeTypeName.VisibleIndex = 6;
            this.colTradeTypeName.Width = 60;
            // 
            // colStopProfitPrice
            // 
            this.colStopProfitPrice.Caption = "止盈价格";
            this.colStopProfitPrice.FieldName = "StopProfitPrice";
            this.colStopProfitPrice.Name = "colStopProfitPrice";
            this.colStopProfitPrice.Visible = true;
            this.colStopProfitPrice.VisibleIndex = 7;
            // 
            // colStopProfitBound
            // 
            this.colStopProfitBound.Caption = "止盈上下限";
            this.colStopProfitBound.FieldName = "StopProfitBound";
            this.colStopProfitBound.Name = "colStopProfitBound";
            this.colStopProfitBound.Visible = true;
            this.colStopProfitBound.VisibleIndex = 8;
            // 
            // colStopLossPrice
            // 
            this.colStopLossPrice.Caption = "止损价格";
            this.colStopLossPrice.FieldName = "StopLossPrice";
            this.colStopLossPrice.Name = "colStopLossPrice";
            this.colStopLossPrice.Visible = true;
            this.colStopLossPrice.VisibleIndex = 9;
            // 
            // colStopLossBound
            // 
            this.colStopLossBound.Caption = "止损上下限";
            this.colStopLossBound.FieldName = "StopLossBound";
            this.colStopLossBound.Name = "colStopLossBound";
            this.colStopLossBound.Visible = true;
            this.colStopLossBound.VisibleIndex = 10;
            // 
            // colStatusName
            // 
            this.colStatusName.Caption = "状态";
            this.colStatusName.FieldName = "StatusName";
            this.colStatusName.Name = "colStatusName";
            this.colStatusName.OptionsColumn.AllowEdit = false;
            this.colStatusName.Visible = true;
            this.colStatusName.VisibleIndex = 11;
            this.colStatusName.Width = 56;
            // 
            // colStatus
            // 
            this.colStatus.FieldName = "Status";
            this.colStatus.Name = "colStatus";
            this.colStatus.OptionsColumn.AllowEdit = false;
            // 
            // colOperate
            // 
            this.colOperate.Caption = "操作";
            this.colOperate.ColumnEdit = this.riButtonEditOperate;
            this.colOperate.Name = "colOperate";
            this.colOperate.Visible = true;
            this.colOperate.VisibleIndex = 12;
            this.colOperate.Width = 118;
            // 
            // riButtonEditOperate
            // 
            this.riButtonEditOperate.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "买卖申请", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", "Apply", null, true),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "删除", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject2, "", "Delete", null, true)});
            this.riButtonEditOperate.ButtonsStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.riButtonEditOperate.Name = "riButtonEditOperate";
            this.riButtonEditOperate.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.riButtonEditOperate.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.riButtonEditOperate_ButtonClick);
            // 
            // colCreateTime
            // 
            this.colCreateTime.Caption = "创建时间";
            this.colCreateTime.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss";
            this.colCreateTime.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colCreateTime.FieldName = "CreateTime";
            this.colCreateTime.Name = "colCreateTime";
            this.colCreateTime.OptionsColumn.AllowEdit = false;
            this.colCreateTime.Visible = true;
            this.colCreateTime.VisibleIndex = 14;
            this.colCreateTime.Width = 90;
            // 
            // colUpdateTime
            // 
            this.colUpdateTime.Caption = "更新时间";
            this.colUpdateTime.FieldName = "UpdateTime";
            this.colUpdateTime.Name = "colUpdateTime";
            this.colUpdateTime.Visible = true;
            this.colUpdateTime.VisibleIndex = 13;
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btnRefresh);
            this.layoutControl1.Controls.Add(this.btnSaveLayout);
            this.layoutControl1.Controls.Add(this.btnDelete);
            this.layoutControl1.Controls.Add(this.btnAdd);
            this.layoutControl1.Controls.Add(this.gridMyApply);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(1542, 759);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(202, 12);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(76, 22);
            this.btnRefresh.StyleController = this.layoutControl1;
            this.btnRefresh.TabIndex = 14;
            this.btnRefresh.Text = "    刷  新    ";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnSaveLayout
            // 
            this.btnSaveLayout.Location = new System.Drawing.Point(1462, 12);
            this.btnSaveLayout.Name = "btnSaveLayout";
            this.btnSaveLayout.Size = new System.Drawing.Size(68, 22);
            this.btnSaveLayout.StyleController = this.layoutControl1;
            this.btnSaveLayout.TabIndex = 8;
            this.btnSaveLayout.Text = " 保存样式 ";
            this.btnSaveLayout.Click += new System.EventHandler(this.btnSaveLayout_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(112, 12);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(76, 22);
            this.btnDelete.StyleController = this.layoutControl1;
            this.btnDelete.TabIndex = 7;
            this.btnDelete.Text = "    删  除    ";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(22, 12);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(76, 22);
            this.btnAdd.StyleController = this.layoutControl1;
            this.btnAdd.TabIndex = 6;
            this.btnAdd.Text = "    添  加    ";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem2,
            this.layoutControlItem1,
            this.layoutControlItem3,
            this.lciDelete,
            this.emptySpaceItem1,
            this.emptySpaceItem3,
            this.layoutControlItem11,
            this.emptySpaceItem9,
            this.layoutControlItem5});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(1542, 759);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(270, 0);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(1180, 26);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gridMyApply;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1522, 713);
            this.layoutControlItem1.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btnAdd;
            this.layoutControlItem3.Location = new System.Drawing.Point(10, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(80, 26);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // lciDelete
            // 
            this.lciDelete.Control = this.btnDelete;
            this.lciDelete.Location = new System.Drawing.Point(100, 0);
            this.lciDelete.Name = "lciDelete";
            this.lciDelete.Size = new System.Drawing.Size(80, 26);
            this.lciDelete.TextSize = new System.Drawing.Size(0, 0);
            this.lciDelete.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(10, 26);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            this.emptySpaceItem3.Location = new System.Drawing.Point(90, 0);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(10, 26);
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem11
            // 
            this.layoutControlItem11.Control = this.btnRefresh;
            this.layoutControlItem11.Location = new System.Drawing.Point(190, 0);
            this.layoutControlItem11.Name = "layoutControlItem11";
            this.layoutControlItem11.Size = new System.Drawing.Size(80, 26);
            this.layoutControlItem11.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem11.TextVisible = false;
            // 
            // emptySpaceItem9
            // 
            this.emptySpaceItem9.AllowHotTrack = false;
            this.emptySpaceItem9.Location = new System.Drawing.Point(180, 0);
            this.emptySpaceItem9.Name = "emptySpaceItem9";
            this.emptySpaceItem9.Size = new System.Drawing.Size(10, 26);
            this.emptySpaceItem9.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.btnSaveLayout;
            this.layoutControlItem5.Location = new System.Drawing.Point(1450, 0);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(72, 26);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // _embedIDApplication
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1542, 759);
            this.Controls.Add(this.layoutControl1);
            this.Name = "_embedIDApplication";
            this.Text = "_embedIDApplication";
            this.Load += new System.EventHandler(this.FrmStockInvestmentDecision_Load);
            ((System.ComponentModel.ISupportInitialize)(this.viewMyApplyDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridMyApply)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewMyApplyMaster)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riButtonEditOperate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciDelete)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraGrid.GridControl gridMyApply;
        private DevExpress.XtraGrid.Views.Grid.GridView viewMyApplyMaster;
        private DevExpress.XtraEditors.SimpleButton btnDelete;
        private DevExpress.XtraEditors.SimpleButton btnAdd;
        private DevExpress.XtraEditors.SimpleButton btnSaveLayout;
        private DevExpress.XtraGrid.Columns.GridColumn colId;
        private DevExpress.XtraGrid.Columns.GridColumn colApplyNo;
        private DevExpress.XtraGrid.Columns.GridColumn colStatus;
        private DevExpress.XtraGrid.Columns.GridColumn colApplyDate;
        private DevExpress.XtraGrid.Columns.GridColumn colApplyUser;
        private DevExpress.XtraGrid.Columns.GridColumn colStockCode;
        private DevExpress.XtraGrid.Columns.GridColumn colStockName;
        private DevExpress.XtraGrid.Columns.GridColumn colTradeType;
        private DevExpress.XtraGrid.Columns.GridColumn colTradePlanNo;
        private DevExpress.XtraGrid.Columns.GridColumn colCreateTime;
        private DevExpress.XtraGrid.Columns.GridColumn colStatusName;
        private DevExpress.XtraGrid.Columns.GridColumn colApplyUserName;
        private DevExpress.XtraGrid.Columns.GridColumn colTradeTypeName;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit riButtonEditOperate;
        private DevExpress.XtraGrid.Columns.GridColumn colOperate;
        private DevExpress.XtraGrid.Columns.GridColumn colDepartmentId;
        private DevExpress.XtraGrid.Columns.GridColumn colDepartmentName;
        private DevExpress.XtraEditors.SimpleButton btnRefresh;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem lciDelete;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem11;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem9;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraGrid.Columns.GridColumn colStopProfitPrice;
        private DevExpress.XtraGrid.Columns.GridColumn colStopProfitBound;
        private DevExpress.XtraGrid.Columns.GridColumn colStopLossPrice;
        private DevExpress.XtraGrid.Columns.GridColumn colStopLossBound;
        private DevExpress.XtraGrid.Columns.GridColumn colUpdateTime;
        private DevExpress.XtraGrid.Views.Grid.GridView viewMyApplyDetail;
    }
}