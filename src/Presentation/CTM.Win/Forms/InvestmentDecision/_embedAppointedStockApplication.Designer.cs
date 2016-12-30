namespace CTM.Win.Forms.InvestmentDecision
{
    partial class _embedAppointedStockApplication
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
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            this.viewDetail = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colOperate_D = new DevExpress.XtraGrid.Columns.GridColumn();
            this.riBtnOperate_D = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOperateNo_D = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDealFlag = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDealPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBoundDetail = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDealVolume = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFormattedDealAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOperateUserName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOperateDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDealFlagName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colVoteStatusName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAccuracyStatusName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridApplication = new DevExpress.XtraGrid.GridControl();
            this.viewMaster = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTradePlanNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colApplyNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colApplyUserName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDepartmentName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colApplyDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStockCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStockName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTradeTypeName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStatusName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btnExpandOrCollapse = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcgApplicationList = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lciIDApplicationList = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lciExpand = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.viewDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riBtnOperate_D)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridApplication)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewMaster)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgApplicationList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciIDApplicationList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciExpand)).BeginInit();
            this.SuspendLayout();
            // 
            // viewDetail
            // 
            this.viewDetail.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colOperate_D,
            this.gridColumn1,
            this.gridColumn2,
            this.colOperateNo_D,
            this.colDealFlag,
            this.colDealPrice,
            this.colBoundDetail,
            this.colDealVolume,
            this.colFormattedDealAmount,
            this.gridColumn8,
            this.gridColumn7,
            this.colOperateUserName,
            this.colOperateDate,
            this.colDealFlagName,
            this.colVoteStatusName,
            this.colAccuracyStatusName});
            this.viewDetail.GridControl = this.gridApplication;
            this.viewDetail.Name = "viewDetail";
            this.viewDetail.OptionsView.ColumnAutoWidth = false;
            this.viewDetail.ViewCaption = "操作记录";
            // 
            // colOperate_D
            // 
            this.colOperate_D.AppearanceHeader.Options.UseTextOptions = true;
            this.colOperate_D.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colOperate_D.Caption = "操作";
            this.colOperate_D.ColumnEdit = this.riBtnOperate_D;
            this.colOperate_D.Name = "colOperate_D";
            this.colOperate_D.Visible = true;
            this.colOperate_D.VisibleIndex = 11;
            // 
            // riBtnOperate_D
            // 
            this.riBtnOperate_D.AutoHeight = false;
            this.riBtnOperate_D.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "查看详情", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", "View", null, true)});
            this.riBtnOperate_D.ButtonsStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.riBtnOperate_D.Name = "riBtnOperate_D";
            this.riBtnOperate_D.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.riBtnOperate_D.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.riBtnOperate_D_ButtonClick);
            // 
            // gridColumn1
            // 
            this.gridColumn1.FieldName = "Id";
            this.gridColumn1.Name = "gridColumn1";
            // 
            // gridColumn2
            // 
            this.gridColumn2.FieldName = "ApplyNo";
            this.gridColumn2.Name = "gridColumn2";
            // 
            // colOperateNo_D
            // 
            this.colOperateNo_D.Caption = "操作编号";
            this.colOperateNo_D.FieldName = "OperateNo";
            this.colOperateNo_D.Name = "colOperateNo_D";
            this.colOperateNo_D.Visible = true;
            this.colOperateNo_D.VisibleIndex = 0;
            this.colOperateNo_D.Width = 70;
            // 
            // colDealFlag
            // 
            this.colDealFlag.FieldName = "DealFlag";
            this.colDealFlag.Name = "colDealFlag";
            // 
            // colDealPrice
            // 
            this.colDealPrice.Caption = "单价(元)";
            this.colDealPrice.DisplayFormat.FormatString = "0.0#";
            this.colDealPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colDealPrice.FieldName = "DealPrice";
            this.colDealPrice.Name = "colDealPrice";
            this.colDealPrice.Visible = true;
            this.colDealPrice.VisibleIndex = 4;
            this.colDealPrice.Width = 65;
            // 
            // colBoundDetail
            // 
            this.colBoundDetail.Caption = "上下限";
            this.colBoundDetail.FieldName = "BoundDetail";
            this.colBoundDetail.Name = "colBoundDetail";
            this.colBoundDetail.Width = 120;
            // 
            // colDealVolume
            // 
            this.colDealVolume.Caption = "数量";
            this.colDealVolume.FieldName = "DealVolume";
            this.colDealVolume.Name = "colDealVolume";
            this.colDealVolume.Visible = true;
            this.colDealVolume.VisibleIndex = 5;
            this.colDealVolume.Width = 65;
            // 
            // colFormattedDealAmount
            // 
            this.colFormattedDealAmount.Caption = "金额(万元)";
            this.colFormattedDealAmount.FieldName = "FormattedDealAmount";
            this.colFormattedDealAmount.Name = "colFormattedDealAmount";
            this.colFormattedDealAmount.Visible = true;
            this.colFormattedDealAmount.VisibleIndex = 6;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "执行状态";
            this.gridColumn8.FieldName = "ExecuteFlagName";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 8;
            this.gridColumn8.Width = 65;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "交易关联";
            this.gridColumn7.FieldName = "RelateFlagName";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 9;
            this.gridColumn7.Width = 65;
            // 
            // colOperateUserName
            // 
            this.colOperateUserName.Caption = "操作人员";
            this.colOperateUserName.FieldName = "OperateUserName";
            this.colOperateUserName.Name = "colOperateUserName";
            this.colOperateUserName.Visible = true;
            this.colOperateUserName.VisibleIndex = 1;
            this.colOperateUserName.Width = 65;
            // 
            // colOperateDate
            // 
            this.colOperateDate.Caption = "操作日期";
            this.colOperateDate.FieldName = "OperateDate";
            this.colOperateDate.Name = "colOperateDate";
            this.colOperateDate.Visible = true;
            this.colOperateDate.VisibleIndex = 2;
            // 
            // colDealFlagName
            // 
            this.colDealFlagName.Caption = "买卖";
            this.colDealFlagName.FieldName = "DealFlagName";
            this.colDealFlagName.Name = "colDealFlagName";
            this.colDealFlagName.Visible = true;
            this.colDealFlagName.VisibleIndex = 3;
            this.colDealFlagName.Width = 50;
            // 
            // colVoteStatusName
            // 
            this.colVoteStatusName.Caption = "决策状态";
            this.colVoteStatusName.FieldName = "VoteStatusName";
            this.colVoteStatusName.Name = "colVoteStatusName";
            this.colVoteStatusName.Visible = true;
            this.colVoteStatusName.VisibleIndex = 7;
            this.colVoteStatusName.Width = 65;
            // 
            // colAccuracyStatusName
            // 
            this.colAccuracyStatusName.Caption = "准确度评定";
            this.colAccuracyStatusName.FieldName = "AccuracyStatusName";
            this.colAccuracyStatusName.Name = "colAccuracyStatusName";
            this.colAccuracyStatusName.Visible = true;
            this.colAccuracyStatusName.VisibleIndex = 10;
            this.colAccuracyStatusName.Width = 70;
            // 
            // gridApplication
            // 
            gridLevelNode1.LevelTemplate = this.viewDetail;
            gridLevelNode1.RelationName = "MD";
            this.gridApplication.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.gridApplication.Location = new System.Drawing.Point(24, 69);
            this.gridApplication.MainView = this.viewMaster;
            this.gridApplication.Name = "gridApplication";
            this.gridApplication.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.riBtnOperate_D});
            this.gridApplication.Size = new System.Drawing.Size(963, 567);
            this.gridApplication.TabIndex = 4;
            this.gridApplication.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.viewMaster,
            this.viewDetail});
            // 
            // viewMaster
            // 
            this.viewMaster.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colId,
            this.colTradePlanNo,
            this.colApplyNo,
            this.colApplyUserName,
            this.colDepartmentName,
            this.colApplyDate,
            this.colStockCode,
            this.colStockName,
            this.colTradeTypeName,
            this.colStatusName,
            this.gridColumn9,
            this.gridColumn10,
            this.gridColumn11,
            this.gridColumn12});
            this.viewMaster.GridControl = this.gridApplication;
            this.viewMaster.IndicatorWidth = 30;
            this.viewMaster.Name = "viewMaster";
            this.viewMaster.OptionsView.ColumnAutoWidth = false;
            this.viewMaster.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.viewMaster_CustomDrawRowIndicator);
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
            this.colTradePlanNo.Width = 85;
            // 
            // colApplyNo
            // 
            this.colApplyNo.Caption = "申请编号";
            this.colApplyNo.FieldName = "ApplyNo";
            this.colApplyNo.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.colApplyNo.Name = "colApplyNo";
            this.colApplyNo.OptionsColumn.AllowEdit = false;
            this.colApplyNo.Visible = true;
            this.colApplyNo.VisibleIndex = 0;
            this.colApplyNo.Width = 80;
            // 
            // colApplyUserName
            // 
            this.colApplyUserName.Caption = "申请人";
            this.colApplyUserName.FieldName = "ApplyUserName";
            this.colApplyUserName.Name = "colApplyUserName";
            this.colApplyUserName.OptionsColumn.AllowEdit = false;
            this.colApplyUserName.Visible = true;
            this.colApplyUserName.VisibleIndex = 2;
            this.colApplyUserName.Width = 70;
            // 
            // colDepartmentName
            // 
            this.colDepartmentName.Caption = "部门名称";
            this.colDepartmentName.FieldName = "DepartmentName";
            this.colDepartmentName.Name = "colDepartmentName";
            this.colDepartmentName.OptionsColumn.AllowEdit = false;
            this.colDepartmentName.Visible = true;
            this.colDepartmentName.VisibleIndex = 3;
            this.colDepartmentName.Width = 70;
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
            this.colStockCode.Width = 70;
            // 
            // colStockName
            // 
            this.colStockName.Caption = "股票名称";
            this.colStockName.FieldName = "StockName";
            this.colStockName.Name = "colStockName";
            this.colStockName.OptionsColumn.AllowEdit = false;
            this.colStockName.Visible = true;
            this.colStockName.VisibleIndex = 5;
            this.colStockName.Width = 70;
            // 
            // colTradeTypeName
            // 
            this.colTradeTypeName.Caption = "交易类别";
            this.colTradeTypeName.FieldName = "TradeTypeName";
            this.colTradeTypeName.Name = "colTradeTypeName";
            this.colTradeTypeName.OptionsColumn.AllowEdit = false;
            this.colTradeTypeName.Visible = true;
            this.colTradeTypeName.VisibleIndex = 6;
            this.colTradeTypeName.Width = 65;
            // 
            // colStatusName
            // 
            this.colStatusName.Caption = "状态";
            this.colStatusName.FieldName = "StatusName";
            this.colStatusName.Name = "colStatusName";
            this.colStatusName.OptionsColumn.AllowEdit = false;
            this.colStatusName.Visible = true;
            this.colStatusName.VisibleIndex = 7;
            this.colStatusName.Width = 52;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "持仓数量";
            this.gridColumn9.FieldName = "CurrentPosition";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 8;
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "平均成本";
            this.gridColumn10.FieldName = "AvgCostPrice";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 9;
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "收益(万元)";
            this.gridColumn11.FieldName = "CurrentProfit";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.Visible = true;
            this.gridColumn11.VisibleIndex = 10;
            this.gridColumn11.Width = 80;
            // 
            // gridColumn12
            // 
            this.gridColumn12.Caption = "最新收盘价";
            this.gridColumn12.FieldName = "LatestClosePrice";
            this.gridColumn12.Name = "gridColumn12";
            this.gridColumn12.Visible = true;
            this.gridColumn12.VisibleIndex = 11;
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btnExpandOrCollapse);
            this.layoutControl1.Controls.Add(this.gridApplication);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(1011, 660);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // btnExpandOrCollapse
            // 
            this.btnExpandOrCollapse.Location = new System.Drawing.Point(24, 43);
            this.btnExpandOrCollapse.Name = "btnExpandOrCollapse";
            this.btnExpandOrCollapse.Size = new System.Drawing.Size(88, 22);
            this.btnExpandOrCollapse.StyleController = this.layoutControl1;
            this.btnExpandOrCollapse.TabIndex = 5;
            this.btnExpandOrCollapse.Text = "全部收起/展开";
            this.btnExpandOrCollapse.Click += new System.EventHandler(this.btnExpandOrCollapse_Click);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcgApplicationList});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(1011, 660);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // lcgApplicationList
            // 
            this.lcgApplicationList.AppearanceGroup.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lcgApplicationList.AppearanceGroup.Options.UseFont = true;
            this.lcgApplicationList.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lciIDApplicationList,
            this.emptySpaceItem1,
            this.lciExpand});
            this.lcgApplicationList.Location = new System.Drawing.Point(0, 0);
            this.lcgApplicationList.Name = "lcgApplicationList";
            this.lcgApplicationList.Size = new System.Drawing.Size(991, 640);
            this.lcgApplicationList.Text = "股票{0} - 决策申请单一览";
            // 
            // lciIDApplicationList
            // 
            this.lciIDApplicationList.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.lciIDApplicationList.AppearanceItemCaption.Options.UseFont = true;
            this.lciIDApplicationList.Control = this.gridApplication;
            this.lciIDApplicationList.Location = new System.Drawing.Point(0, 26);
            this.lciIDApplicationList.Name = "lciIDApplicationList";
            this.lciIDApplicationList.Size = new System.Drawing.Size(967, 571);
            this.lciIDApplicationList.Text = "股票{0} - 决策申请单一览";
            this.lciIDApplicationList.TextLocation = DevExpress.Utils.Locations.Top;
            this.lciIDApplicationList.TextSize = new System.Drawing.Size(0, 0);
            this.lciIDApplicationList.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(92, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(875, 26);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lciExpand
            // 
            this.lciExpand.Control = this.btnExpandOrCollapse;
            this.lciExpand.Location = new System.Drawing.Point(0, 0);
            this.lciExpand.Name = "lciExpand";
            this.lciExpand.Size = new System.Drawing.Size(92, 26);
            this.lciExpand.TextSize = new System.Drawing.Size(0, 0);
            this.lciExpand.TextVisible = false;
            // 
            // _embedAppointedStockApplication
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1011, 660);
            this.Controls.Add(this.layoutControl1);
            this.Name = "_embedAppointedStockApplication";
            this.Text = "_embedAppointedStockApplication";
            this.Load += new System.EventHandler(this._embedAppointedStockApplication_Load);
            ((System.ComponentModel.ISupportInitialize)(this.viewDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riBtnOperate_D)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridApplication)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewMaster)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgApplicationList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciIDApplicationList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciExpand)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraGrid.GridControl gridApplication;
        private DevExpress.XtraGrid.Views.Grid.GridView viewMaster;
        private DevExpress.XtraLayout.LayoutControlItem lciIDApplicationList;
        private DevExpress.XtraGrid.Views.Grid.GridView viewDetail;
        private DevExpress.XtraGrid.Columns.GridColumn colOperate_D;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn colDealFlag;
        private DevExpress.XtraGrid.Columns.GridColumn colDealPrice;
        private DevExpress.XtraGrid.Columns.GridColumn colDealVolume;
        private DevExpress.XtraGrid.Columns.GridColumn colOperateUserName;
        private DevExpress.XtraGrid.Columns.GridColumn colOperateDate;
        private DevExpress.XtraGrid.Columns.GridColumn colDealFlagName;
        private DevExpress.XtraGrid.Columns.GridColumn colVoteStatusName;
        private DevExpress.XtraGrid.Columns.GridColumn colAccuracyStatusName;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn colId;
        private DevExpress.XtraGrid.Columns.GridColumn colTradePlanNo;
        private DevExpress.XtraGrid.Columns.GridColumn colApplyNo;
        private DevExpress.XtraGrid.Columns.GridColumn colApplyUserName;
        private DevExpress.XtraGrid.Columns.GridColumn colDepartmentName;
        private DevExpress.XtraGrid.Columns.GridColumn colApplyDate;
        private DevExpress.XtraGrid.Columns.GridColumn colStockCode;
        private DevExpress.XtraGrid.Columns.GridColumn colStockName;
        private DevExpress.XtraGrid.Columns.GridColumn colTradeTypeName;
        private DevExpress.XtraGrid.Columns.GridColumn colStatusName;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit riBtnOperate_D;
        private DevExpress.XtraEditors.SimpleButton btnExpandOrCollapse;
        private DevExpress.XtraLayout.LayoutControlGroup lcgApplicationList;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem lciExpand;
        private DevExpress.XtraGrid.Columns.GridColumn colOperateNo_D;
        private DevExpress.XtraGrid.Columns.GridColumn colBoundDetail;
        private DevExpress.XtraGrid.Columns.GridColumn colFormattedDealAmount;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
    }
}