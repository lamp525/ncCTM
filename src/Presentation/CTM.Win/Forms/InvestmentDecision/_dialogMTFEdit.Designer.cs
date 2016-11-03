namespace CTM.Win.Forms.InvestmentDecision
{
    partial class _dialogMTFEdit
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
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colSerialNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colInvestorCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colInvestorName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colWeight = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colWeightPercentage = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAcquaintanceGraphDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemdeGraph = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.colTrend = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOpen = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemtxtOpen = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colForenoon = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemtxtForenoon = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colAfternoon = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemtxtAfternoon = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colClose = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemtxtClose = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colReason = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemtxtReason = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colAccuracy = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemtxtAccuracy = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repositoryItemtxtTrend = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lciMTF = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemdeGraph)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemdeGraph.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemtxtOpen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemtxtForenoon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemtxtAfternoon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemtxtClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemtxtReason)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemtxtAccuracy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemtxtTrend)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciMTF)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btnClose);
            this.layoutControl1.Controls.Add(this.gridControl1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(1378, 609);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(1291, 575);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(65, 22);
            this.btnClose.StyleController = this.layoutControl1;
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "    关  闭  ";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // gridControl1
            // 
            this.gridControl1.Location = new System.Drawing.Point(12, 38);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemdeGraph,
            this.repositoryItemtxtTrend,
            this.repositoryItemtxtOpen,
            this.repositoryItemtxtForenoon,
            this.repositoryItemtxtAfternoon,
            this.repositoryItemtxtClose,
            this.repositoryItemtxtReason,
            this.repositoryItemtxtAccuracy});
            this.gridControl1.Size = new System.Drawing.Size(1354, 533);
            this.gridControl1.TabIndex = 4;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colSerialNo,
            this.colInvestorCode,
            this.colInvestorName,
            this.colWeight,
            this.colWeightPercentage,
            this.colAcquaintanceGraphDate,
            this.colTrend,
            this.colOpen,
            this.colForenoon,
            this.colAfternoon,
            this.colClose,
            this.colReason,
            this.colAccuracy});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gridView1_CustomDrawRowIndicator);
            this.gridView1.ShowingEditor += new System.ComponentModel.CancelEventHandler(this.gridView1_ShowingEditor);
            this.gridView1.RowUpdated += new DevExpress.XtraGrid.Views.Base.RowObjectEventHandler(this.gridView1_RowUpdated);
            // 
            // colSerialNo
            // 
            this.colSerialNo.Caption = "申请编号";
            this.colSerialNo.FieldName = "SerialNo";
            this.colSerialNo.Name = "colSerialNo";
            this.colSerialNo.Visible = true;
            this.colSerialNo.VisibleIndex = 0;
            this.colSerialNo.Width = 71;
            // 
            // colInvestorCode
            // 
            this.colInvestorCode.FieldName = "InvestorCode";
            this.colInvestorCode.Name = "colInvestorCode";
            this.colInvestorCode.Width = 73;
            // 
            // colInvestorName
            // 
            this.colInvestorName.Caption = "姓名";
            this.colInvestorName.FieldName = "InvestorName";
            this.colInvestorName.Name = "colInvestorName";
            this.colInvestorName.Visible = true;
            this.colInvestorName.VisibleIndex = 1;
            this.colInvestorName.Width = 58;
            // 
            // colWeight
            // 
            this.colWeight.FieldName = "Weight";
            this.colWeight.Name = "colWeight";
            // 
            // colWeightPercentage
            // 
            this.colWeightPercentage.Caption = "权重";
            this.colWeightPercentage.FieldName = "WeightPercentage";
            this.colWeightPercentage.Name = "colWeightPercentage";
            this.colWeightPercentage.Visible = true;
            this.colWeightPercentage.VisibleIndex = 3;
            // 
            // colAcquaintanceGraphDate
            // 
            this.colAcquaintanceGraphDate.Caption = "相似图形日期";
            this.colAcquaintanceGraphDate.ColumnEdit = this.repositoryItemdeGraph;
            this.colAcquaintanceGraphDate.FieldName = "AcquaintanceGraphDate";
            this.colAcquaintanceGraphDate.Name = "colAcquaintanceGraphDate";
            this.colAcquaintanceGraphDate.Visible = true;
            this.colAcquaintanceGraphDate.VisibleIndex = 2;
            this.colAcquaintanceGraphDate.Width = 84;
            // 
            // repositoryItemdeGraph
            // 
            this.repositoryItemdeGraph.AutoHeight = false;
            this.repositoryItemdeGraph.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemdeGraph.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemdeGraph.Name = "repositoryItemdeGraph";
            // 
            // colTrend
            // 
            this.colTrend.Caption = "走势判断";
            this.colTrend.FieldName = "Trend";
            this.colTrend.Name = "colTrend";
            this.colTrend.Visible = true;
            this.colTrend.VisibleIndex = 4;
            this.colTrend.Width = 137;
            // 
            // colOpen
            // 
            this.colOpen.Caption = "开盘";
            this.colOpen.ColumnEdit = this.repositoryItemtxtOpen;
            this.colOpen.FieldName = "Open";
            this.colOpen.Name = "colOpen";
            this.colOpen.Visible = true;
            this.colOpen.VisibleIndex = 5;
            this.colOpen.Width = 137;
            // 
            // repositoryItemtxtOpen
            // 
            this.repositoryItemtxtOpen.AutoHeight = false;
            this.repositoryItemtxtOpen.Name = "repositoryItemtxtOpen";
            // 
            // colForenoon
            // 
            this.colForenoon.Caption = "上午盘";
            this.colForenoon.ColumnEdit = this.repositoryItemtxtForenoon;
            this.colForenoon.FieldName = "Forenoon";
            this.colForenoon.Name = "colForenoon";
            this.colForenoon.Visible = true;
            this.colForenoon.VisibleIndex = 6;
            this.colForenoon.Width = 137;
            // 
            // repositoryItemtxtForenoon
            // 
            this.repositoryItemtxtForenoon.AutoHeight = false;
            this.repositoryItemtxtForenoon.Name = "repositoryItemtxtForenoon";
            // 
            // colAfternoon
            // 
            this.colAfternoon.Caption = "下午盘";
            this.colAfternoon.ColumnEdit = this.repositoryItemtxtAfternoon;
            this.colAfternoon.FieldName = "Afternoon";
            this.colAfternoon.Name = "colAfternoon";
            this.colAfternoon.Visible = true;
            this.colAfternoon.VisibleIndex = 7;
            this.colAfternoon.Width = 137;
            // 
            // repositoryItemtxtAfternoon
            // 
            this.repositoryItemtxtAfternoon.AutoHeight = false;
            this.repositoryItemtxtAfternoon.Name = "repositoryItemtxtAfternoon";
            // 
            // colClose
            // 
            this.colClose.Caption = "收盘";
            this.colClose.ColumnEdit = this.repositoryItemtxtClose;
            this.colClose.FieldName = "Close";
            this.colClose.Name = "colClose";
            this.colClose.Visible = true;
            this.colClose.VisibleIndex = 8;
            this.colClose.Width = 137;
            // 
            // repositoryItemtxtClose
            // 
            this.repositoryItemtxtClose.AutoHeight = false;
            this.repositoryItemtxtClose.Name = "repositoryItemtxtClose";
            // 
            // colReason
            // 
            this.colReason.Caption = "理由及判断";
            this.colReason.ColumnEdit = this.repositoryItemtxtReason;
            this.colReason.FieldName = "Reason";
            this.colReason.Name = "colReason";
            this.colReason.Visible = true;
            this.colReason.VisibleIndex = 10;
            this.colReason.Width = 117;
            // 
            // repositoryItemtxtReason
            // 
            this.repositoryItemtxtReason.AutoHeight = false;
            this.repositoryItemtxtReason.Name = "repositoryItemtxtReason";
            // 
            // colAccuracy
            // 
            this.colAccuracy.Caption = "正确性判断";
            this.colAccuracy.ColumnEdit = this.repositoryItemtxtAccuracy;
            this.colAccuracy.FieldName = "Accuracy";
            this.colAccuracy.Name = "colAccuracy";
            this.colAccuracy.Visible = true;
            this.colAccuracy.VisibleIndex = 9;
            this.colAccuracy.Width = 201;
            // 
            // repositoryItemtxtAccuracy
            // 
            this.repositoryItemtxtAccuracy.AutoHeight = false;
            this.repositoryItemtxtAccuracy.Name = "repositoryItemtxtAccuracy";
            // 
            // repositoryItemtxtTrend
            // 
            this.repositoryItemtxtTrend.AutoHeight = false;
            this.repositoryItemtxtTrend.Name = "repositoryItemtxtTrend";
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lciMTF,
            this.emptySpaceItem1,
            this.layoutControlItem3,
            this.emptySpaceItem2});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(1378, 609);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // lciMTF
            // 
            this.lciMTF.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
            this.lciMTF.AppearanceItemCaption.Options.UseFont = true;
            this.lciMTF.AppearanceItemCaption.Options.UseTextOptions = true;
            this.lciMTF.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lciMTF.Control = this.gridControl1;
            this.lciMTF.Location = new System.Drawing.Point(0, 0);
            this.lciMTF.Name = "lciMTF";
            this.lciMTF.Size = new System.Drawing.Size(1358, 563);
            this.lciMTF.Text = "大盘趋势预测";
            this.lciMTF.TextLocation = DevExpress.Utils.Locations.Top;
            this.lciMTF.TextSize = new System.Drawing.Size(120, 23);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 563);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(1279, 26);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btnClose;
            this.layoutControlItem3.Location = new System.Drawing.Point(1279, 563);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(69, 26);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(1348, 563);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(10, 26);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // _dialogMarketTrendForecast
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1378, 609);
            this.Controls.Add(this.layoutControl1);
            this.Name = "_dialogMTFEdit";
            this.Text = "_dialogMTFEdit";
            this.Load += new System.EventHandler(this._dialogMTFEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemdeGraph.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemdeGraph)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemtxtOpen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemtxtForenoon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemtxtAfternoon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemtxtClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemtxtReason)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemtxtAccuracy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemtxtTrend)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciMTF)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn colSerialNo;
        private DevExpress.XtraGrid.Columns.GridColumn colInvestorCode;
        private DevExpress.XtraGrid.Columns.GridColumn colInvestorName;
        private DevExpress.XtraGrid.Columns.GridColumn colAcquaintanceGraphDate;
        private DevExpress.XtraGrid.Columns.GridColumn colTrend;
        private DevExpress.XtraGrid.Columns.GridColumn colOpen;
        private DevExpress.XtraGrid.Columns.GridColumn colForenoon;
        private DevExpress.XtraGrid.Columns.GridColumn colAfternoon;
        private DevExpress.XtraGrid.Columns.GridColumn colClose;
        private DevExpress.XtraGrid.Columns.GridColumn colReason;
        private DevExpress.XtraLayout.LayoutControlItem lciMTF;
        private DevExpress.XtraGrid.Columns.GridColumn colAccuracy;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit repositoryItemdeGraph;
        private DevExpress.XtraGrid.Columns.GridColumn colWeight;
        private DevExpress.XtraGrid.Columns.GridColumn colWeightPercentage;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemtxtTrend;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemtxtOpen;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemtxtForenoon;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemtxtAfternoon;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemtxtClose;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemtxtReason;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemtxtAccuracy;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
    }
}