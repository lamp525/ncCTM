namespace CTM.Win.Forms.DailyTrading.TradeIdentifier
{
    partial class FrmUserTradeIdentifier
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
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDataType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTradeType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOperatorCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBeneficiary = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTradeDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTradeTime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStockCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStockName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDealFlag = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDealPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDealVolume = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDealAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colActualAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEntrustVolume = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEntrustPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEntrustAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.chartControl1 = new DevExpress.XtraCharts.ChartControl();
            this.lookUpEdit2 = new DevExpress.XtraEditors.LookUpEdit();
            this.lookUpEdit1 = new DevExpress.XtraEditors.LookUpEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.textBox2);
            this.layoutControl1.Controls.Add(this.textBox1);
            this.layoutControl1.Controls.Add(this.gridControl1);
            this.layoutControl1.Controls.Add(this.chartControl1);
            this.layoutControl1.Controls.Add(this.lookUpEdit2);
            this.layoutControl1.Controls.Add(this.lookUpEdit1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(1430, 770);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(825, 503);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(581, 20);
            this.textBox2.TabIndex = 9;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(132, 503);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(581, 20);
            this.textBox1.TabIndex = 8;
            // 
            // gridControl1
            // 
            this.gridControl1.Location = new System.Drawing.Point(24, 544);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1382, 202);
            this.gridControl1.TabIndex = 7;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colId,
            this.colDataType,
            this.colTradeType,
            this.colOperatorCode,
            this.colBeneficiary,
            this.colTradeDate,
            this.colTradeTime,
            this.colStockCode,
            this.colStockName,
            this.colDealFlag,
            this.colDealPrice,
            this.colDealVolume,
            this.colDealAmount,
            this.colActualAmount,
            this.colEntrustVolume,
            this.colEntrustPrice,
            this.colEntrustAmount});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            // 
            // colId
            // 
            this.colId.FieldName = "Id";
            this.colId.Name = "colId";
            // 
            // colDataType
            // 
            this.colDataType.FieldName = "DataType";
            this.colDataType.Name = "colDataType";
            // 
            // colTradeType
            // 
            this.colTradeType.FieldName = "TradeType";
            this.colTradeType.Name = "colTradeType";
            this.colTradeType.Visible = true;
            this.colTradeType.VisibleIndex = 0;
            // 
            // colOperatorCode
            // 
            this.colOperatorCode.FieldName = "OperatorCode";
            this.colOperatorCode.Name = "colOperatorCode";
            this.colOperatorCode.Visible = true;
            this.colOperatorCode.VisibleIndex = 1;
            // 
            // colBeneficiary
            // 
            this.colBeneficiary.FieldName = "Beneficiary";
            this.colBeneficiary.Name = "colBeneficiary";
            this.colBeneficiary.Visible = true;
            this.colBeneficiary.VisibleIndex = 2;
            // 
            // colTradeDate
            // 
            this.colTradeDate.FieldName = "TradeDate";
            this.colTradeDate.Name = "colTradeDate";
            this.colTradeDate.Visible = true;
            this.colTradeDate.VisibleIndex = 3;
            // 
            // colTradeTime
            // 
            this.colTradeTime.FieldName = "TradeTime";
            this.colTradeTime.Name = "colTradeTime";
            this.colTradeTime.Visible = true;
            this.colTradeTime.VisibleIndex = 4;
            // 
            // colStockCode
            // 
            this.colStockCode.FieldName = "StockCode";
            this.colStockCode.Name = "colStockCode";
            this.colStockCode.Visible = true;
            this.colStockCode.VisibleIndex = 5;
            // 
            // colStockName
            // 
            this.colStockName.FieldName = "StockName";
            this.colStockName.Name = "colStockName";
            this.colStockName.Visible = true;
            this.colStockName.VisibleIndex = 6;
            // 
            // colDealFlag
            // 
            this.colDealFlag.FieldName = "DealFlag";
            this.colDealFlag.Name = "colDealFlag";
            this.colDealFlag.Visible = true;
            this.colDealFlag.VisibleIndex = 7;
            // 
            // colDealPrice
            // 
            this.colDealPrice.FieldName = "DealPrice";
            this.colDealPrice.Name = "colDealPrice";
            this.colDealPrice.Visible = true;
            this.colDealPrice.VisibleIndex = 8;
            // 
            // colDealVolume
            // 
            this.colDealVolume.FieldName = "DealVolume";
            this.colDealVolume.Name = "colDealVolume";
            this.colDealVolume.Visible = true;
            this.colDealVolume.VisibleIndex = 9;
            // 
            // colDealAmount
            // 
            this.colDealAmount.FieldName = "DealAmount";
            this.colDealAmount.Name = "colDealAmount";
            this.colDealAmount.Visible = true;
            this.colDealAmount.VisibleIndex = 10;
            // 
            // colActualAmount
            // 
            this.colActualAmount.FieldName = "ActualAmount";
            this.colActualAmount.Name = "colActualAmount";
            this.colActualAmount.Visible = true;
            this.colActualAmount.VisibleIndex = 11;
            // 
            // colEntrustVolume
            // 
            this.colEntrustVolume.FieldName = "EntrustVolume";
            this.colEntrustVolume.Name = "colEntrustVolume";
            this.colEntrustVolume.Visible = true;
            this.colEntrustVolume.VisibleIndex = 12;
            // 
            // colEntrustPrice
            // 
            this.colEntrustPrice.FieldName = "EntrustPrice";
            this.colEntrustPrice.Name = "colEntrustPrice";
            this.colEntrustPrice.Visible = true;
            this.colEntrustPrice.VisibleIndex = 13;
            // 
            // colEntrustAmount
            // 
            this.colEntrustAmount.FieldName = "EntrustAmount";
            this.colEntrustAmount.Name = "colEntrustAmount";
            this.colEntrustAmount.Visible = true;
            this.colEntrustAmount.VisibleIndex = 14;
            // 
            // chartControl1
            // 
            this.chartControl1.Location = new System.Drawing.Point(24, 84);
            this.chartControl1.Name = "chartControl1";
            this.chartControl1.SeriesSerializable = new DevExpress.XtraCharts.Series[0];
            this.chartControl1.Size = new System.Drawing.Size(956, 372);
            this.chartControl1.TabIndex = 6;
            this.chartControl1.Paint += new System.Windows.Forms.PaintEventHandler(this.chartControl1_Paint);
            this.chartControl1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.chartControl1_MouseMove);
            // 
            // lookUpEdit2
            // 
            this.lookUpEdit2.Location = new System.Drawing.Point(132, 43);
            this.lookUpEdit2.Name = "lookUpEdit2";
            this.lookUpEdit2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEdit2.Size = new System.Drawing.Size(592, 20);
            this.lookUpEdit2.StyleController = this.layoutControl1;
            this.lookUpEdit2.TabIndex = 5;
            // 
            // lookUpEdit1
            // 
            this.lookUpEdit1.Location = new System.Drawing.Point(836, 43);
            this.lookUpEdit1.Name = "lookUpEdit1";
            this.lookUpEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEdit1.Size = new System.Drawing.Size(570, 20);
            this.lookUpEdit1.StyleController = this.layoutControl1;
            this.lookUpEdit1.TabIndex = 4;
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
            this.layoutControlGroup1.Size = new System.Drawing.Size(1430, 770);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem6});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 460);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Size = new System.Drawing.Size(1410, 290);
            this.layoutControlGroup2.Text = "当日交易详情";
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.gridControl1;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(1386, 223);
            this.layoutControlItem4.Text = "成交记录:";
            this.layoutControlItem4.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem4.TextSize = new System.Drawing.Size(105, 14);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.textBox1;
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(693, 24);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(105, 14);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.textBox2;
            this.layoutControlItem6.Location = new System.Drawing.Point(693, 0);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(693, 24);
            this.layoutControlItem6.TextSize = new System.Drawing.Size(105, 14);
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3,
            this.layoutControlItem2,
            this.layoutControlItem1,
            this.emptySpaceItem1});
            this.layoutControlGroup3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.Size = new System.Drawing.Size(1410, 460);
            this.layoutControlGroup3.Text = "交易标识";
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.chartControl1;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(960, 393);
            this.layoutControlItem3.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(105, 14);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.lookUpEdit2;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(704, 24);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(105, 14);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.lookUpEdit1;
            this.layoutControlItem1.Location = new System.Drawing.Point(704, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(682, 24);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(105, 14);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(960, 24);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(426, 393);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1430, 770);
            this.Controls.Add(this.layoutControl1);
            this.Name = "FrmUserTradeIdentifier";
            this.Text = "FrmUserTradeIdentifier";
            this.Load += new System.EventHandler(this.Form2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraCharts.ChartControl chartControl1;
        private DevExpress.XtraEditors.LookUpEdit lookUpEdit2;
        private DevExpress.XtraEditors.LookUpEdit lookUpEdit1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraGrid.Columns.GridColumn colId;
        private DevExpress.XtraGrid.Columns.GridColumn colDataType;
        private DevExpress.XtraGrid.Columns.GridColumn colTradeType;
        private DevExpress.XtraGrid.Columns.GridColumn colOperatorCode;
        private DevExpress.XtraGrid.Columns.GridColumn colBeneficiary;
        private DevExpress.XtraGrid.Columns.GridColumn colTradeDate;
        private DevExpress.XtraGrid.Columns.GridColumn colTradeTime;
        private DevExpress.XtraGrid.Columns.GridColumn colStockCode;
        private DevExpress.XtraGrid.Columns.GridColumn colStockName;
        private DevExpress.XtraGrid.Columns.GridColumn colDealFlag;
        private DevExpress.XtraGrid.Columns.GridColumn colDealPrice;
        private DevExpress.XtraGrid.Columns.GridColumn colDealVolume;
        private DevExpress.XtraGrid.Columns.GridColumn colDealAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colActualAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colEntrustVolume;
        private DevExpress.XtraGrid.Columns.GridColumn colEntrustPrice;
        private DevExpress.XtraGrid.Columns.GridColumn colEntrustAmount;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
    }
}