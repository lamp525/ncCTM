namespace CTM.Win.UI.InvestmentDecision
{
    partial class _dialogMarketTrendForecast
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
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.colSerialNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colInvestorCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colInvestorName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAcquaintanceGraphDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTrend = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOpen = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colForenoon = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAfternoon = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colClose = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colReason = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAccuracy = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.gridControl1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(939, 518);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(939, 518);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // gridControl1
            // 
            this.gridControl1.Location = new System.Drawing.Point(12, 38);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(915, 468);
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
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
            this.layoutControlItem1.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem1.AppearanceItemCaption.Options.UseTextOptions = true;
            this.layoutControlItem1.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.layoutControlItem1.Control = this.gridControl1;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(919, 498);
            this.layoutControlItem1.Text = "大盘趋势预测";
            this.layoutControlItem1.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(120, 23);
            // 
            // colSerialNo
            // 
            this.colSerialNo.Caption = "gridColumn1";
            this.colSerialNo.FieldName = "SerialNo";
            this.colSerialNo.Name = "colSerialNo";
            this.colSerialNo.Visible = true;
            this.colSerialNo.VisibleIndex = 0;
            // 
            // colInvestorCode
            // 
            this.colInvestorCode.Caption = "gridColumn2";
            this.colInvestorCode.FieldName = "InvestorCode";
            this.colInvestorCode.Name = "colInvestorCode";
            this.colInvestorCode.Visible = true;
            this.colInvestorCode.VisibleIndex = 1;
            // 
            // colInvestorName
            // 
            this.colInvestorName.Caption = "gridColumn3";
            this.colInvestorName.FieldName = "InvestorName";
            this.colInvestorName.Name = "colInvestorName";
            this.colInvestorName.Visible = true;
            this.colInvestorName.VisibleIndex = 2;
            // 
            // colAcquaintanceGraphDate
            // 
            this.colAcquaintanceGraphDate.Caption = "gridColumn4";
            this.colAcquaintanceGraphDate.FieldName = "AcquaintanceGraphDate";
            this.colAcquaintanceGraphDate.Name = "colAcquaintanceGraphDate";
            this.colAcquaintanceGraphDate.Visible = true;
            this.colAcquaintanceGraphDate.VisibleIndex = 3;
            // 
            // colTrend
            // 
            this.colTrend.Caption = "gridColumn5";
            this.colTrend.FieldName = "Trend";
            this.colTrend.Name = "colTrend";
            this.colTrend.Visible = true;
            this.colTrend.VisibleIndex = 4;
            // 
            // colOpen
            // 
            this.colOpen.Caption = "gridColumn6";
            this.colOpen.FieldName = "Open";
            this.colOpen.Name = "colOpen";
            this.colOpen.Visible = true;
            this.colOpen.VisibleIndex = 5;
            // 
            // colForenoon
            // 
            this.colForenoon.Caption = "gridColumn7";
            this.colForenoon.FieldName = "Forenoon";
            this.colForenoon.Name = "colForenoon";
            this.colForenoon.Visible = true;
            this.colForenoon.VisibleIndex = 6;
            // 
            // colAfternoon
            // 
            this.colAfternoon.Caption = "gridColumn8";
            this.colAfternoon.FieldName = "Afternoon";
            this.colAfternoon.Name = "colAfternoon";
            this.colAfternoon.Visible = true;
            this.colAfternoon.VisibleIndex = 7;
            // 
            // colClose
            // 
            this.colClose.Caption = "gridColumn9";
            this.colClose.FieldName = "Close";
            this.colClose.Name = "colClose";
            this.colClose.Visible = true;
            this.colClose.VisibleIndex = 8;
            // 
            // colReason
            // 
            this.colReason.Caption = "gridColumn10";
            this.colReason.FieldName = "Reason";
            this.colReason.Name = "colReason";
            this.colReason.Visible = true;
            this.colReason.VisibleIndex = 10;
            // 
            // colAccuracy
            // 
            this.colAccuracy.Caption = "gridColumn1";
            this.colAccuracy.FieldName = "Accuracy";
            this.colAccuracy.Name = "colAccuracy";
            this.colAccuracy.Visible = true;
            this.colAccuracy.VisibleIndex = 9;
            // 
            // _dialogMarketTrendForecast
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(939, 518);
            this.Controls.Add(this.layoutControl1);
            this.Name = "_dialogMarketTrendForecast";
            this.Text = "_dialogMarketTrendForecast";
            this.Load += new System.EventHandler(this._dialogMarketTrendForecast_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
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
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraGrid.Columns.GridColumn colAccuracy;
    }
}