namespace CTM.Win.Forms.DailyTrading.DataManage
{
    partial class _dialogDailyRecordEdit
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
            this.deTradeDate = new DevExpress.XtraEditors.DateEdit();
            this.luAccount = new DevExpress.XtraEditors.LookUpEdit();
            this.btnReturn = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colRecordId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTradeDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTradeTime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStockCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStockName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOperatorName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTradeTypeName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBeneficiaryName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDealFlagName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDealPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDealVolume = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDealAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colActualAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAccountName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDataTypeName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.luBeneficiary = new DevExpress.XtraEditors.LookUpEdit();
            this.cbTradeType = new DevExpress.XtraEditors.ComboBoxEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem5 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.deTradeDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deTradeDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.luAccount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.luBeneficiary.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbTradeType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.deTradeDate);
            this.layoutControl1.Controls.Add(this.luAccount);
            this.layoutControl1.Controls.Add(this.btnReturn);
            this.layoutControl1.Controls.Add(this.btnSave);
            this.layoutControl1.Controls.Add(this.gridControl1);
            this.layoutControl1.Controls.Add(this.luBeneficiary);
            this.layoutControl1.Controls.Add(this.cbTradeType);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(1382, 755);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // deTradeDate
            // 
            this.deTradeDate.EditValue = null;
            this.deTradeDate.Location = new System.Drawing.Point(87, 67);
            this.deTradeDate.Name = "deTradeDate";
            this.deTradeDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deTradeDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deTradeDate.Size = new System.Drawing.Size(123, 20);
            this.deTradeDate.StyleController = this.layoutControl1;
            this.deTradeDate.TabIndex = 10;
            // 
            // luAccount
            // 
            this.luAccount.Location = new System.Drawing.Point(87, 43);
            this.luAccount.Name = "luAccount";
            this.luAccount.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.luAccount.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Id", "账户ID", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", 90, "账户名称"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("SecurityCompanyName", 90, "开户券商"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("AttributeName", 90, "账户属性")});
            this.luAccount.Size = new System.Drawing.Size(473, 20);
            this.luAccount.StyleController = this.layoutControl1;
            this.luAccount.TabIndex = 9;
            // 
            // btnReturn
            // 
            this.btnReturn.Location = new System.Drawing.Point(651, 65);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(73, 22);
            this.btnReturn.StyleController = this.layoutControl1;
            this.btnReturn.TabIndex = 8;
            this.btnReturn.Text = "    返  回    ";
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(574, 65);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(73, 22);
            this.btnSave.StyleController = this.layoutControl1;
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "    保  存    ";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // gridControl1
            // 
            this.gridControl1.Location = new System.Drawing.Point(24, 134);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1334, 597);
            this.gridControl1.TabIndex = 6;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colRecordId,
            this.colTradeDate,
            this.colTradeTime,
            this.colStockCode,
            this.colStockName,
            this.colOperatorName,
            this.colTradeTypeName,
            this.colBeneficiaryName,
            this.colDealFlagName,
            this.colDealPrice,
            this.colDealVolume,
            this.colDealAmount,
            this.colActualAmount,
            this.colAccountName,
            this.colDataTypeName});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsBehavior.ReadOnly = true;
            this.gridView1.OptionsSelection.CheckBoxSelectorColumnWidth = 30;
            this.gridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gridView1.OptionsSelection.UseIndicatorForSelection = false;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gridView1_CustomDrawRowIndicator);
            // 
            // colRecordId
            // 
            this.colRecordId.FieldName = "RecordId";
            this.colRecordId.Name = "colRecordId";
            // 
            // colTradeDate
            // 
            this.colTradeDate.Caption = "交易日期";
            this.colTradeDate.FieldName = "TradeDate";
            this.colTradeDate.Name = "colTradeDate";
            this.colTradeDate.Visible = true;
            this.colTradeDate.VisibleIndex = 0;
            this.colTradeDate.Width = 80;
            // 
            // colTradeTime
            // 
            this.colTradeTime.Caption = "交易时间";
            this.colTradeTime.FieldName = "TradeTime";
            this.colTradeTime.Name = "colTradeTime";
            this.colTradeTime.Visible = true;
            this.colTradeTime.VisibleIndex = 1;
            this.colTradeTime.Width = 80;
            // 
            // colStockCode
            // 
            this.colStockCode.Caption = "股票代码";
            this.colStockCode.FieldName = "StockCode";
            this.colStockCode.Name = "colStockCode";
            this.colStockCode.Visible = true;
            this.colStockCode.VisibleIndex = 5;
            this.colStockCode.Width = 80;
            // 
            // colStockName
            // 
            this.colStockName.Caption = "股票名称";
            this.colStockName.FieldName = "StockName";
            this.colStockName.Name = "colStockName";
            this.colStockName.Visible = true;
            this.colStockName.VisibleIndex = 6;
            this.colStockName.Width = 80;
            // 
            // colOperatorName
            // 
            this.colOperatorName.Caption = "交易员";
            this.colOperatorName.FieldName = "OperatorName";
            this.colOperatorName.Name = "colOperatorName";
            this.colOperatorName.Visible = true;
            this.colOperatorName.VisibleIndex = 2;
            this.colOperatorName.Width = 80;
            // 
            // colTradeTypeName
            // 
            this.colTradeTypeName.Caption = "交易类别";
            this.colTradeTypeName.FieldName = "TradeTypeName";
            this.colTradeTypeName.Name = "colTradeTypeName";
            this.colTradeTypeName.Visible = true;
            this.colTradeTypeName.VisibleIndex = 7;
            this.colTradeTypeName.Width = 70;
            // 
            // colBeneficiaryName
            // 
            this.colBeneficiaryName.Caption = "实际受益人";
            this.colBeneficiaryName.FieldName = "BeneficiaryName";
            this.colBeneficiaryName.Name = "colBeneficiaryName";
            this.colBeneficiaryName.Visible = true;
            this.colBeneficiaryName.VisibleIndex = 4;
            this.colBeneficiaryName.Width = 80;
            // 
            // colDealFlagName
            // 
            this.colDealFlagName.Caption = "买卖标志";
            this.colDealFlagName.FieldName = "DealFlagName";
            this.colDealFlagName.Name = "colDealFlagName";
            this.colDealFlagName.Visible = true;
            this.colDealFlagName.VisibleIndex = 8;
            this.colDealFlagName.Width = 70;
            // 
            // colDealPrice
            // 
            this.colDealPrice.Caption = "成交价";
            this.colDealPrice.FieldName = "DealPrice";
            this.colDealPrice.Name = "colDealPrice";
            this.colDealPrice.Visible = true;
            this.colDealPrice.VisibleIndex = 10;
            this.colDealPrice.Width = 90;
            // 
            // colDealVolume
            // 
            this.colDealVolume.Caption = "成交数量";
            this.colDealVolume.DisplayFormat.FormatString = "N";
            this.colDealVolume.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.colDealVolume.FieldName = "DealVolume";
            this.colDealVolume.Name = "colDealVolume";
            this.colDealVolume.Visible = true;
            this.colDealVolume.VisibleIndex = 9;
            this.colDealVolume.Width = 100;
            // 
            // colDealAmount
            // 
            this.colDealAmount.Caption = "成交金额";
            this.colDealAmount.DisplayFormat.FormatString = "N";
            this.colDealAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.colDealAmount.FieldName = "DealAmount";
            this.colDealAmount.Name = "colDealAmount";
            this.colDealAmount.Visible = true;
            this.colDealAmount.VisibleIndex = 11;
            this.colDealAmount.Width = 100;
            // 
            // colActualAmount
            // 
            this.colActualAmount.Caption = "发生金额";
            this.colActualAmount.DisplayFormat.FormatString = "N";
            this.colActualAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.colActualAmount.FieldName = "ActualAmount";
            this.colActualAmount.Name = "colActualAmount";
            this.colActualAmount.Visible = true;
            this.colActualAmount.VisibleIndex = 12;
            this.colActualAmount.Width = 100;
            // 
            // colAccountName
            // 
            this.colAccountName.Caption = "账户信息";
            this.colAccountName.FieldName = "AccountName";
            this.colAccountName.Name = "colAccountName";
            this.colAccountName.Visible = true;
            this.colAccountName.VisibleIndex = 3;
            this.colAccountName.Width = 200;
            // 
            // colDataTypeName
            // 
            this.colDataTypeName.Caption = "数据类型";
            this.colDataTypeName.FieldName = "DataTypeName";
            this.colDataTypeName.Name = "colDataTypeName";
            this.colDataTypeName.Visible = true;
            this.colDataTypeName.VisibleIndex = 13;
            this.colDataTypeName.Width = 90;
            // 
            // luBeneficiary
            // 
            this.luBeneficiary.Location = new System.Drawing.Point(427, 67);
            this.luBeneficiary.Name = "luBeneficiary";
            this.luBeneficiary.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.luBeneficiary.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Code", "编码"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "姓名")});
            this.luBeneficiary.Size = new System.Drawing.Size(133, 20);
            this.luBeneficiary.StyleController = this.layoutControl1;
            this.luBeneficiary.TabIndex = 5;
            // 
            // cbTradeType
            // 
            this.cbTradeType.Location = new System.Drawing.Point(277, 67);
            this.cbTradeType.Name = "cbTradeType";
            this.cbTradeType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbTradeType.Size = new System.Drawing.Size(83, 20);
            this.cbTradeType.StyleController = this.layoutControl1;
            this.cbTradeType.TabIndex = 4;
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
            this.layoutControlGroup1.Size = new System.Drawing.Size(1382, 755);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.AppearanceGroup.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.layoutControlGroup2.AppearanceGroup.Options.UseFont = true;
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 91);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Size = new System.Drawing.Size(1362, 644);
            this.layoutControlGroup2.Text = "已选记录";
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.gridControl1;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(1338, 601);
            this.layoutControlItem3.Text = "已选择交易记录";
            this.layoutControlItem3.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.AppearanceGroup.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.layoutControlGroup3.AppearanceGroup.Options.UseFont = true;
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem6,
            this.emptySpaceItem5,
            this.emptySpaceItem3,
            this.emptySpaceItem1,
            this.layoutControlItem2,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem7,
            this.layoutControlItem1});
            this.layoutControlGroup3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.Size = new System.Drawing.Size(1362, 91);
            this.layoutControlGroup3.Text = "修改操作";
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.luAccount;
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(540, 24);
            this.layoutControlItem6.Text = "账户信息";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(60, 14);
            // 
            // emptySpaceItem5
            // 
            this.emptySpaceItem5.AllowHotTrack = false;
            this.emptySpaceItem5.Location = new System.Drawing.Point(540, 0);
            this.emptySpaceItem5.Name = "emptySpaceItem5";
            this.emptySpaceItem5.Size = new System.Drawing.Size(10, 48);
            this.emptySpaceItem5.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            this.emptySpaceItem3.Location = new System.Drawing.Point(550, 0);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(154, 22);
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(704, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(634, 48);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.luBeneficiary;
            this.layoutControlItem2.Location = new System.Drawing.Point(340, 24);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(200, 24);
            this.layoutControlItem2.Text = "实际受益人";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(60, 14);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.btnSave;
            this.layoutControlItem4.Location = new System.Drawing.Point(550, 22);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(77, 26);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.btnReturn;
            this.layoutControlItem5.Location = new System.Drawing.Point(627, 22);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(77, 26);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.deTradeDate;
            this.layoutControlItem7.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(190, 24);
            this.layoutControlItem7.Text = "交易日期";
            this.layoutControlItem7.TextSize = new System.Drawing.Size(60, 14);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.cbTradeType;
            this.layoutControlItem1.Location = new System.Drawing.Point(190, 24);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(150, 24);
            this.layoutControlItem1.Text = "交易类别";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(60, 14);
            // 
            // _dialogDailyRecordEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1382, 755);
            this.Controls.Add(this.layoutControl1);
            this.LookAndFeel.SkinName = "Office 2013";
            this.Name = "_dialogDailyRecordEdit";
            this.Text = "_dialogDailyRecordEdit";
            this.Load += new System.EventHandler(this._dialogDailyRecordEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.deTradeDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deTradeDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.luAccount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.luBeneficiary.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbTradeType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.SimpleButton btnReturn;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.LookUpEdit luBeneficiary;
        private DevExpress.XtraEditors.ComboBoxEdit cbTradeType;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraGrid.Columns.GridColumn colTradeDate;
        private DevExpress.XtraGrid.Columns.GridColumn colTradeTime;
        private DevExpress.XtraGrid.Columns.GridColumn colStockCode;
        private DevExpress.XtraGrid.Columns.GridColumn colStockName;
        private DevExpress.XtraGrid.Columns.GridColumn colDealFlagName;
        private DevExpress.XtraGrid.Columns.GridColumn colDealPrice;
        private DevExpress.XtraGrid.Columns.GridColumn colDealVolume;
        private DevExpress.XtraGrid.Columns.GridColumn colDealAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colActualAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colDataTypeName;
        private DevExpress.XtraGrid.Columns.GridColumn colTradeTypeName;
        private DevExpress.XtraGrid.Columns.GridColumn colAccountName;
        private DevExpress.XtraGrid.Columns.GridColumn colBeneficiaryName;
        private DevExpress.XtraGrid.Columns.GridColumn colOperatorName;
        private DevExpress.XtraGrid.Columns.GridColumn colRecordId;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraEditors.LookUpEdit luAccount;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem5;
        private DevExpress.XtraEditors.DateEdit deTradeDate;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
    }
}