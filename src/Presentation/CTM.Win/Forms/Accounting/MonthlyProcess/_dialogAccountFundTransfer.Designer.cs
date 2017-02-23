namespace CTM.Win.Forms.Accounting.MonthlyProcess
{
    partial class _dialogAccountFundTransfer
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
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAccountDetail = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTransferDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTransferType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTransferAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFlowFlagName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFlowFlag = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTargetAccountId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTargetAccountCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBalance = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOperateTime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOperator = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRemarks = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnAdd = new DevExpress.XtraEditors.SimpleButton();
            this.btnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.chkOut = new DevExpress.XtraEditors.CheckEdit();
            this.chkIn = new DevExpress.XtraEditors.CheckEdit();
            this.txtAmount = new DevExpress.XtraEditors.TextEdit();
            this.deTransfer = new DevExpress.XtraEditors.DateEdit();
            this.luAccount = new DevExpress.XtraEditors.LookUpEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem4 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem8 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem5 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem7 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkOut.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIn.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAmount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deTransfer.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deTransfer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.luAccount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.gridControl1);
            this.layoutControl1.Controls.Add(this.btnAdd);
            this.layoutControl1.Controls.Add(this.btnDelete);
            this.layoutControl1.Controls.Add(this.chkOut);
            this.layoutControl1.Controls.Add(this.chkIn);
            this.layoutControl1.Controls.Add(this.txtAmount);
            this.layoutControl1.Controls.Add(this.deTransfer);
            this.layoutControl1.Controls.Add(this.luAccount);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(991, 612);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // gridControl1
            // 
            this.gridControl1.Location = new System.Drawing.Point(24, 138);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(943, 450);
            this.gridControl1.TabIndex = 11;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colId,
            this.colAccountDetail,
            this.colTransferDate,
            this.colTransferType,
            this.colTransferAmount,
            this.colFlowFlagName,
            this.colFlowFlag,
            this.colTargetAccountId,
            this.colTargetAccountCode,
            this.colBalance,
            this.colOperateTime,
            this.colOperator,
            this.colRemarks});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.IndicatorWidth = 50;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsBehavior.ReadOnly = true;
            this.gridView1.OptionsSelection.CheckBoxSelectorColumnWidth = 30;
            this.gridView1.OptionsSelection.MultiSelect = true;
            this.gridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gridView1.OptionsSelection.UseIndicatorForSelection = false;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.EnableAppearanceEvenRow = true;
            this.gridView1.OptionsView.EnableAppearanceOddRow = true;
            this.gridView1.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.ShowAlways;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gridView1_CustomDrawRowIndicator);
            this.gridView1.SelectionChanged += new DevExpress.Data.SelectionChangedEventHandler(this.gridView1_SelectionChanged);
            // 
            // colId
            // 
            this.colId.FieldName = "Id";
            this.colId.Name = "colId";
            // 
            // colAccountDetail
            // 
            this.colAccountDetail.Caption = "账户信息";
            this.colAccountDetail.FieldName = "AccountDetail";
            this.colAccountDetail.Name = "colAccountDetail";
            this.colAccountDetail.Visible = true;
            this.colAccountDetail.VisibleIndex = 2;
            this.colAccountDetail.Width = 200;
            // 
            // colTransferDate
            // 
            this.colTransferDate.Caption = "日期";
            this.colTransferDate.FieldName = "TransferDate";
            this.colTransferDate.Name = "colTransferDate";
            this.colTransferDate.Visible = true;
            this.colTransferDate.VisibleIndex = 1;
            this.colTransferDate.Width = 90;
            // 
            // colTransferType
            // 
            this.colTransferType.FieldName = "TransferType";
            this.colTransferType.Name = "colTransferType";
            // 
            // colTransferAmount
            // 
            this.colTransferAmount.Caption = "金额（万元）";
            this.colTransferAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colTransferAmount.FieldName = "TransferAmount";
            this.colTransferAmount.Name = "colTransferAmount";
            this.colTransferAmount.Visible = true;
            this.colTransferAmount.VisibleIndex = 4;
            this.colTransferAmount.Width = 120;
            // 
            // colFlowFlagName
            // 
            this.colFlowFlagName.Caption = "操作";
            this.colFlowFlagName.FieldName = "FlowFlagName";
            this.colFlowFlagName.Name = "colFlowFlagName";
            this.colFlowFlagName.Visible = true;
            this.colFlowFlagName.VisibleIndex = 3;
            this.colFlowFlagName.Width = 80;
            // 
            // colFlowFlag
            // 
            this.colFlowFlag.FieldName = "FlowFlag";
            this.colFlowFlag.Name = "colFlowFlag";
            // 
            // colTargetAccountId
            // 
            this.colTargetAccountId.FieldName = "TargetAccountId";
            this.colTargetAccountId.Name = "colTargetAccountId";
            // 
            // colTargetAccountCode
            // 
            this.colTargetAccountCode.FieldName = "TargetAccountCode";
            this.colTargetAccountCode.Name = "colTargetAccountCode";
            // 
            // colBalance
            // 
            this.colBalance.Caption = "余额（元）";
            this.colBalance.DisplayFormat.FormatString = "0.#0";
            this.colBalance.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colBalance.FieldName = "Balance";
            this.colBalance.Name = "colBalance";
            this.colBalance.Width = 120;
            // 
            // colOperateTime
            // 
            this.colOperateTime.Caption = "操作时间";
            this.colOperateTime.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss";
            this.colOperateTime.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colOperateTime.FieldName = "OperateTime";
            this.colOperateTime.Name = "colOperateTime";
            this.colOperateTime.Visible = true;
            this.colOperateTime.VisibleIndex = 6;
            this.colOperateTime.Width = 168;
            // 
            // colOperator
            // 
            this.colOperator.Caption = "操作人";
            this.colOperator.FieldName = "Operator";
            this.colOperator.Name = "colOperator";
            this.colOperator.Visible = true;
            this.colOperator.VisibleIndex = 5;
            this.colOperator.Width = 80;
            // 
            // colRemarks
            // 
            this.colRemarks.FieldName = "Remarks";
            this.colRemarks.Name = "colRemarks";
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(864, 43);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 22);
            this.btnAdd.StyleController = this.layoutControl1;
            this.btnAdd.TabIndex = 10;
            this.btnAdd.Text = "    添  加    ";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(34, 112);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 22);
            this.btnDelete.StyleController = this.layoutControl1;
            this.btnDelete.TabIndex = 9;
            this.btnDelete.Text = "    删  除    ";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // chkOut
            // 
            this.chkOut.Location = new System.Drawing.Point(574, 43);
            this.chkOut.Name = "chkOut";
            this.chkOut.Properties.Caption = "转出";
            this.chkOut.Size = new System.Drawing.Size(46, 19);
            this.chkOut.StyleController = this.layoutControl1;
            this.chkOut.TabIndex = 8;
            this.chkOut.CheckedChanged += new System.EventHandler(this.chkOut_CheckedChanged);
            // 
            // chkIn
            // 
            this.chkIn.EditValue = true;
            this.chkIn.Location = new System.Drawing.Point(524, 43);
            this.chkIn.Name = "chkIn";
            this.chkIn.Properties.Caption = "转入";
            this.chkIn.Size = new System.Drawing.Size(46, 19);
            this.chkIn.StyleController = this.layoutControl1;
            this.chkIn.TabIndex = 7;
            this.chkIn.CheckedChanged += new System.EventHandler(this.chkIn_CheckedChanged);
            // 
            // txtAmount
            // 
            this.txtAmount.Location = new System.Drawing.Point(719, 43);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(141, 20);
            this.txtAmount.StyleController = this.layoutControl1;
            this.txtAmount.TabIndex = 6;
            // 
            // deTransfer
            // 
            this.deTransfer.EditValue = null;
            this.deTransfer.Location = new System.Drawing.Point(419, 43);
            this.deTransfer.Name = "deTransfer";
            this.deTransfer.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deTransfer.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deTransfer.Size = new System.Drawing.Size(91, 20);
            this.deTransfer.StyleController = this.layoutControl1;
            this.deTransfer.TabIndex = 5;
            // 
            // luAccount
            // 
            this.luAccount.Location = new System.Drawing.Point(109, 43);
            this.luAccount.Name = "luAccount";
            this.luAccount.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.luAccount.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "名称"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("SecurityCompanyName", "开户券商"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("AttributeName", "属性"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Id", "ID", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default)});
            this.luAccount.Size = new System.Drawing.Size(211, 20);
            this.luAccount.StyleController = this.layoutControl1;
            this.luAccount.TabIndex = 4;
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
            this.layoutControlGroup1.Size = new System.Drawing.Size(991, 612);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.AppearanceGroup.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.layoutControlGroup2.AppearanceGroup.Options.UseFont = true;
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem8,
            this.emptySpaceItem4,
            this.layoutControlItem6,
            this.emptySpaceItem8});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 69);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Size = new System.Drawing.Size(971, 523);
            this.layoutControlGroup2.Text = "调拨记录";
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.layoutControlItem8.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem8.Control = this.gridControl1;
            this.layoutControlItem8.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(947, 454);
            this.layoutControlItem8.Text = "账户资金调拨记录";
            this.layoutControlItem8.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem8.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem8.TextVisible = false;
            // 
            // emptySpaceItem4
            // 
            this.emptySpaceItem4.AllowHotTrack = false;
            this.emptySpaceItem4.Location = new System.Drawing.Point(89, 0);
            this.emptySpaceItem4.Name = "emptySpaceItem4";
            this.emptySpaceItem4.Size = new System.Drawing.Size(858, 26);
            this.emptySpaceItem4.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.btnDelete;
            this.layoutControlItem6.Location = new System.Drawing.Point(10, 0);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(79, 26);
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextVisible = false;
            // 
            // emptySpaceItem8
            // 
            this.emptySpaceItem8.AllowHotTrack = false;
            this.emptySpaceItem8.Location = new System.Drawing.Point(0, 0);
            this.emptySpaceItem8.Name = "emptySpaceItem8";
            this.emptySpaceItem8.Size = new System.Drawing.Size(10, 26);
            this.emptySpaceItem8.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.AppearanceGroup.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.layoutControlGroup3.AppearanceGroup.Options.UseFont = true;
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem5,
            this.layoutControlItem4,
            this.emptySpaceItem2,
            this.layoutControlItem2,
            this.emptySpaceItem5,
            this.layoutControlItem7,
            this.emptySpaceItem7,
            this.layoutControlItem3,
            this.layoutControlItem1,
            this.emptySpaceItem1});
            this.layoutControlGroup3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.Size = new System.Drawing.Size(971, 69);
            this.layoutControlGroup3.Text = "调拨信息";
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.chkOut;
            this.layoutControlItem5.Location = new System.Drawing.Point(550, 0);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(50, 26);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.chkIn;
            this.layoutControlItem4.Location = new System.Drawing.Point(500, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(50, 26);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(600, 0);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(10, 26);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.deTransfer;
            this.layoutControlItem2.Location = new System.Drawing.Point(310, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(180, 26);
            this.layoutControlItem2.Text = "操作日期";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(82, 14);
            // 
            // emptySpaceItem5
            // 
            this.emptySpaceItem5.AllowHotTrack = false;
            this.emptySpaceItem5.Location = new System.Drawing.Point(490, 0);
            this.emptySpaceItem5.Name = "emptySpaceItem5";
            this.emptySpaceItem5.Size = new System.Drawing.Size(10, 26);
            this.emptySpaceItem5.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.btnAdd;
            this.layoutControlItem7.Location = new System.Drawing.Point(840, 0);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(79, 26);
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextVisible = false;
            // 
            // emptySpaceItem7
            // 
            this.emptySpaceItem7.AllowHotTrack = false;
            this.emptySpaceItem7.Location = new System.Drawing.Point(919, 0);
            this.emptySpaceItem7.Name = "emptySpaceItem7";
            this.emptySpaceItem7.Size = new System.Drawing.Size(28, 26);
            this.emptySpaceItem7.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.txtAmount;
            this.layoutControlItem3.Location = new System.Drawing.Point(610, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(230, 26);
            this.layoutControlItem3.Text = "操作金额(万元)";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(82, 14);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.luAccount;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(300, 26);
            this.layoutControlItem1.Text = "账户信息";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(82, 14);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(300, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(10, 26);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // _dialogAccountFundTransfer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(991, 612);
            this.Controls.Add(this.layoutControl1);
            this.Name = "_dialogAccountFundTransfer";
            this.Text = "_dialogAccountFundTransfer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this._dialogAccountFundTransfer_FormClosing);
            this.Load += new System.EventHandler(this._dialogAccountFundTransfer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkOut.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIn.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAmount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deTransfer.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deTransfer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.luAccount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.CheckEdit chkIn;
        private DevExpress.XtraEditors.TextEdit txtAmount;
        private DevExpress.XtraEditors.DateEdit deTransfer;
        private DevExpress.XtraEditors.LookUpEdit luAccount;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraEditors.CheckEdit chkOut;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem4;
        private DevExpress.XtraEditors.SimpleButton btnAdd;
        private DevExpress.XtraEditors.SimpleButton btnDelete;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem5;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem7;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem8;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraGrid.Columns.GridColumn colId;
        private DevExpress.XtraGrid.Columns.GridColumn colAccountDetail;
        private DevExpress.XtraGrid.Columns.GridColumn colTransferDate;
        private DevExpress.XtraGrid.Columns.GridColumn colTransferType;
        private DevExpress.XtraGrid.Columns.GridColumn colTransferAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colFlowFlag;
        private DevExpress.XtraGrid.Columns.GridColumn colTargetAccountId;
        private DevExpress.XtraGrid.Columns.GridColumn colTargetAccountCode;
        private DevExpress.XtraGrid.Columns.GridColumn colBalance;
        private DevExpress.XtraGrid.Columns.GridColumn colOperateTime;
        private DevExpress.XtraGrid.Columns.GridColumn colOperator;
        private DevExpress.XtraGrid.Columns.GridColumn colRemarks;
        private DevExpress.XtraGrid.Columns.GridColumn colFlowFlagName;
    }
}