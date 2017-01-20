namespace CTM.Win.Forms.DailyTrading.StatisticsReport
{
    partial class FrmUserInvestIncomeDaily
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
            this.lblInterest = new DevExpress.XtraEditors.LabelControl();
            this.btnSaveLayout = new DevExpress.XtraEditors.SimpleButton();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.cbDepartment = new DevExpress.XtraEditors.ComboBoxEdit();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colTradeTime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDepartmentName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colInvestor = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAnnualProfit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCurrentProfit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCurrentActualProfit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCurrentIncomeRate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDealAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPlanMarginAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colActualMarginAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCurrentInterest = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPositionValue = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAllotFund = new DevExpress.XtraGrid.Columns.GridColumn();
            this.deTradeDate = new DevExpress.XtraEditors.DateEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lciDepartment = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem4 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem5 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbDepartment.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deTradeDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deTradeDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciDepartment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.lblInterest);
            this.layoutControl1.Controls.Add(this.btnSaveLayout);
            this.layoutControl1.Controls.Add(this.btnSearch);
            this.layoutControl1.Controls.Add(this.cbDepartment);
            this.layoutControl1.Controls.Add(this.gridControl1);
            this.layoutControl1.Controls.Add(this.deTradeDate);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(1531, 747);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // lblInterest
            // 
            this.lblInterest.Appearance.ForeColor = System.Drawing.Color.OrangeRed;
            this.lblInterest.Location = new System.Drawing.Point(24, 116);
            this.lblInterest.Name = "lblInterest";
            this.lblInterest.Size = new System.Drawing.Size(70, 14);
            this.lblInterest.StyleController = this.layoutControl1;
            this.lblInterest.TabIndex = 9;
            this.lblInterest.Text = "labelControl1";
            // 
            // btnSaveLayout
            // 
            this.btnSaveLayout.Location = new System.Drawing.Point(1419, 116);
            this.btnSaveLayout.Name = "btnSaveLayout";
            this.btnSaveLayout.Size = new System.Drawing.Size(88, 22);
            this.btnSaveLayout.StyleController = this.layoutControl1;
            this.btnSaveLayout.TabIndex = 8;
            this.btnSaveLayout.Text = " 保存样式 ";
            this.btnSaveLayout.Click += new System.EventHandler(this.btnSaveLayout_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(410, 45);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(77, 22);
            this.btnSearch.StyleController = this.layoutControl1;
            this.btnSearch.TabIndex = 7;
            this.btnSearch.Text = "    查  询    ";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // cbDepartment
            // 
            this.cbDepartment.Location = new System.Drawing.Point(268, 45);
            this.cbDepartment.Name = "cbDepartment";
            this.cbDepartment.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbDepartment.Size = new System.Drawing.Size(126, 20);
            this.cbDepartment.StyleController = this.layoutControl1;
            this.cbDepartment.TabIndex = 6;
            // 
            // gridControl1
            // 
            this.gridControl1.Location = new System.Drawing.Point(24, 142);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1483, 581);
            this.gridControl1.TabIndex = 5;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colTradeTime,
            this.colDepartmentName,
            this.colInvestor,
            this.colAnnualProfit,
            this.colCurrentProfit,
            this.colCurrentActualProfit,
            this.colCurrentIncomeRate,
            this.colDealAmount,
            this.colPlanMarginAmount,
            this.colActualMarginAmount,
            this.colCurrentInterest,
            this.colPositionValue,
            this.colAllotFund});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsBehavior.ReadOnly = true;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.EnableAppearanceEvenRow = true;
            this.gridView1.OptionsView.EnableAppearanceOddRow = true;
            this.gridView1.OptionsView.ShowAutoFilterRow = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gridView1_CustomDrawRowIndicator);
            this.gridView1.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gridView1_RowCellStyle);
            // 
            // colTradeTime
            // 
            this.colTradeTime.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.colTradeTime.AppearanceHeader.Options.UseFont = true;
            this.colTradeTime.Caption = "交易日期";
            this.colTradeTime.FieldName = "TradeTime";
            this.colTradeTime.Name = "colTradeTime";
            this.colTradeTime.Visible = true;
            this.colTradeTime.VisibleIndex = 0;
            this.colTradeTime.Width = 99;
            // 
            // colDepartmentName
            // 
            this.colDepartmentName.AppearanceCell.Options.UseTextOptions = true;
            this.colDepartmentName.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDepartmentName.Caption = "部门名称";
            this.colDepartmentName.FieldName = "DepartmentName";
            this.colDepartmentName.Name = "colDepartmentName";
            this.colDepartmentName.Visible = true;
            this.colDepartmentName.VisibleIndex = 1;
            this.colDepartmentName.Width = 91;
            // 
            // colInvestor
            // 
            this.colInvestor.Caption = "投资人员";
            this.colInvestor.FieldName = "Investor";
            this.colInvestor.Name = "colInvestor";
            this.colInvestor.Visible = true;
            this.colInvestor.VisibleIndex = 2;
            this.colInvestor.Width = 88;
            // 
            // colAnnualProfit
            // 
            this.colAnnualProfit.Caption = "本年累计收益额";
            this.colAnnualProfit.DisplayFormat.FormatString = "N";
            this.colAnnualProfit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.colAnnualProfit.FieldName = "AnnualProfit";
            this.colAnnualProfit.Name = "colAnnualProfit";
            this.colAnnualProfit.Visible = true;
            this.colAnnualProfit.VisibleIndex = 3;
            this.colAnnualProfit.Width = 100;
            // 
            // colCurrentProfit
            // 
            this.colCurrentProfit.AppearanceCell.Options.UseTextOptions = true;
            this.colCurrentProfit.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colCurrentProfit.Caption = "日收益额";
            this.colCurrentProfit.DisplayFormat.FormatString = "N";
            this.colCurrentProfit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.colCurrentProfit.FieldName = "CurrentProfit";
            this.colCurrentProfit.Name = "colCurrentProfit";
            this.colCurrentProfit.Visible = true;
            this.colCurrentProfit.VisibleIndex = 4;
            this.colCurrentProfit.Width = 100;
            // 
            // colCurrentActualProfit
            // 
            this.colCurrentActualProfit.Caption = "日净收益额";
            this.colCurrentActualProfit.DisplayFormat.FormatString = "N";
            this.colCurrentActualProfit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.colCurrentActualProfit.FieldName = "CurrentActualProfit";
            this.colCurrentActualProfit.Name = "colCurrentActualProfit";
            this.colCurrentActualProfit.Visible = true;
            this.colCurrentActualProfit.VisibleIndex = 5;
            this.colCurrentActualProfit.Width = 100;
            // 
            // colCurrentIncomeRate
            // 
            this.colCurrentIncomeRate.AppearanceCell.Options.UseTextOptions = true;
            this.colCurrentIncomeRate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colCurrentIncomeRate.Caption = "日收益率";
            this.colCurrentIncomeRate.DisplayFormat.FormatString = "#####0.00%";
            this.colCurrentIncomeRate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colCurrentIncomeRate.FieldName = "CurrentIncomeRate";
            this.colCurrentIncomeRate.Name = "colCurrentIncomeRate";
            this.colCurrentIncomeRate.Visible = true;
            this.colCurrentIncomeRate.VisibleIndex = 6;
            this.colCurrentIncomeRate.Width = 90;
            // 
            // colDealAmount
            // 
            this.colDealAmount.AppearanceCell.Options.UseTextOptions = true;
            this.colDealAmount.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colDealAmount.Caption = "成交额";
            this.colDealAmount.DisplayFormat.FormatString = "N";
            this.colDealAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.colDealAmount.FieldName = "DealAmount";
            this.colDealAmount.Name = "colDealAmount";
            this.colDealAmount.Visible = true;
            this.colDealAmount.VisibleIndex = 7;
            this.colDealAmount.Width = 100;
            // 
            // colPlanMarginAmount
            // 
            this.colPlanMarginAmount.AppearanceCell.Options.UseTextOptions = true;
            this.colPlanMarginAmount.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colPlanMarginAmount.Caption = "计划融资融券额";
            this.colPlanMarginAmount.DisplayFormat.FormatString = "N";
            this.colPlanMarginAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.colPlanMarginAmount.FieldName = "PlanMarginAmount";
            this.colPlanMarginAmount.Name = "colPlanMarginAmount";
            this.colPlanMarginAmount.Visible = true;
            this.colPlanMarginAmount.VisibleIndex = 8;
            this.colPlanMarginAmount.Width = 100;
            // 
            // colActualMarginAmount
            // 
            this.colActualMarginAmount.Caption = "实际融资融券额";
            this.colActualMarginAmount.DisplayFormat.FormatString = "N";
            this.colActualMarginAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.colActualMarginAmount.FieldName = "ActualMarginAmount";
            this.colActualMarginAmount.Name = "colActualMarginAmount";
            this.colActualMarginAmount.Visible = true;
            this.colActualMarginAmount.VisibleIndex = 9;
            this.colActualMarginAmount.Width = 100;
            // 
            // colCurrentInterest
            // 
            this.colCurrentInterest.Caption = "日利息(单位：元)";
            this.colCurrentInterest.DisplayFormat.FormatString = "N";
            this.colCurrentInterest.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.colCurrentInterest.FieldName = "CurrentInterest";
            this.colCurrentInterest.Name = "colCurrentInterest";
            this.colCurrentInterest.Visible = true;
            this.colCurrentInterest.VisibleIndex = 10;
            this.colCurrentInterest.Width = 119;
            // 
            // colPositionValue
            // 
            this.colPositionValue.Caption = "持仓市值";
            this.colPositionValue.DisplayFormat.FormatString = "N";
            this.colPositionValue.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.colPositionValue.FieldName = "PositionValue";
            this.colPositionValue.Name = "colPositionValue";
            this.colPositionValue.Visible = true;
            this.colPositionValue.VisibleIndex = 11;
            this.colPositionValue.Width = 100;
            // 
            // colAllotFund
            // 
            this.colAllotFund.AppearanceCell.Options.UseTextOptions = true;
            this.colAllotFund.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colAllotFund.Caption = "分配资金";
            this.colAllotFund.FieldName = "AllotFund";
            this.colAllotFund.Name = "colAllotFund";
            this.colAllotFund.Width = 100;
            // 
            // deTradeDate
            // 
            this.deTradeDate.EditValue = null;
            this.deTradeDate.Location = new System.Drawing.Point(75, 45);
            this.deTradeDate.Name = "deTradeDate";
            this.deTradeDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deTradeDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deTradeDate.Size = new System.Drawing.Size(126, 20);
            this.deTradeDate.StyleController = this.layoutControl1;
            this.deTradeDate.TabIndex = 4;
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
            this.layoutControlGroup1.Size = new System.Drawing.Size(1531, 747);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.AppearanceGroup.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.layoutControlGroup2.AppearanceGroup.Options.UseFont = true;
            this.layoutControlGroup2.ExpandButtonVisible = true;
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.emptySpaceItem1,
            this.emptySpaceItem3,
            this.lciDepartment,
            this.emptySpaceItem4,
            this.layoutControlItem4});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Size = new System.Drawing.Size(1511, 71);
            this.layoutControlGroup2.Text = "查询条件";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.deTradeDate;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(181, 26);
            this.layoutControlItem1.Text = "交易日期";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(48, 14);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(181, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(12, 26);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            this.emptySpaceItem3.Location = new System.Drawing.Point(374, 0);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(12, 26);
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lciDepartment
            // 
            this.lciDepartment.Control = this.cbDepartment;
            this.lciDepartment.Location = new System.Drawing.Point(193, 0);
            this.lciDepartment.Name = "lciDepartment";
            this.lciDepartment.Size = new System.Drawing.Size(181, 26);
            this.lciDepartment.Text = "部门名称";
            this.lciDepartment.TextSize = new System.Drawing.Size(48, 14);
            // 
            // emptySpaceItem4
            // 
            this.emptySpaceItem4.AllowHotTrack = false;
            this.emptySpaceItem4.Location = new System.Drawing.Point(467, 0);
            this.emptySpaceItem4.Name = "emptySpaceItem4";
            this.emptySpaceItem4.Size = new System.Drawing.Size(1020, 26);
            this.emptySpaceItem4.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.btnSearch;
            this.layoutControlItem4.Location = new System.Drawing.Point(386, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(81, 26);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.AppearanceGroup.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.layoutControlGroup3.AppearanceGroup.Options.UseFont = true;
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.emptySpaceItem5,
            this.layoutControlItem5,
            this.layoutControlItem6});
            this.layoutControlGroup3.Location = new System.Drawing.Point(0, 71);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.Size = new System.Drawing.Size(1511, 656);
            this.layoutControlGroup3.Text = "日收益明细（金额单位：万元）";
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.gridControl1;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(1487, 585);
            this.layoutControlItem2.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // emptySpaceItem5
            // 
            this.emptySpaceItem5.AllowHotTrack = false;
            this.emptySpaceItem5.Location = new System.Drawing.Point(74, 0);
            this.emptySpaceItem5.Name = "emptySpaceItem5";
            this.emptySpaceItem5.Size = new System.Drawing.Size(1321, 26);
            this.emptySpaceItem5.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.btnSaveLayout;
            this.layoutControlItem5.Location = new System.Drawing.Point(1395, 0);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(92, 26);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.lblInterest;
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(74, 26);
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextVisible = false;
            // 
            // FrmUserInvestIncomeDaily
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1531, 747);
            this.Controls.Add(this.layoutControl1);
            this.Name = "FrmUserInvestIncomeDaily";
            this.Text = "FrmUserInvestIncomeDaily";
            this.Load += new System.EventHandler(this.FrmUserInvestIncomeDaily_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cbDepartment.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deTradeDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deTradeDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciDepartment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.DateEdit deTradeDate;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraEditors.ComboBoxEdit cbDepartment;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private DevExpress.XtraLayout.LayoutControlItem lciDepartment;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem4;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraEditors.SimpleButton btnSaveLayout;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraGrid.Columns.GridColumn colInvestor;
        private DevExpress.XtraGrid.Columns.GridColumn colTradeTime;
        private DevExpress.XtraGrid.Columns.GridColumn colCurrentProfit;
        private DevExpress.XtraGrid.Columns.GridColumn colCurrentIncomeRate;
        private DevExpress.XtraGrid.Columns.GridColumn colDealAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colDepartmentName;
        private DevExpress.XtraGrid.Columns.GridColumn colAllotFund;
        private DevExpress.XtraGrid.Columns.GridColumn colPlanMarginAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colActualMarginAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colCurrentInterest;
        private DevExpress.XtraGrid.Columns.GridColumn colCurrentActualProfit;
        private DevExpress.XtraEditors.LabelControl lblInterest;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraGrid.Columns.GridColumn colAnnualProfit;
        private DevExpress.XtraGrid.Columns.GridColumn colPositionValue;
    }
}