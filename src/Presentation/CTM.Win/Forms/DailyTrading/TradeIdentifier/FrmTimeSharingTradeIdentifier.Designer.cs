namespace CTM.Win.Forms.DailyTrading.TradeIdentifier
{
    partial class FrmTimeSharingTradeIdentifier
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
            DevExpress.XtraCharts.ConstantLine constantLine1 = new DevExpress.XtraCharts.ConstantLine();
            DevExpress.XtraCharts.ConstantLine constantLine2 = new DevExpress.XtraCharts.ConstantLine();
            DevExpress.XtraCharts.ConstantLine constantLine3 = new DevExpress.XtraCharts.ConstantLine();
            DevExpress.XtraCharts.ConstantLine constantLine4 = new DevExpress.XtraCharts.ConstantLine();
            DevExpress.XtraCharts.ConstantLine constantLine5 = new DevExpress.XtraCharts.ConstantLine();
            DevExpress.XtraCharts.ConstantLine constantLine6 = new DevExpress.XtraCharts.ConstantLine();
            DevExpress.XtraCharts.ConstantLine constantLine7 = new DevExpress.XtraCharts.ConstantLine();
            DevExpress.XtraCharts.ConstantLine constantLine8 = new DevExpress.XtraCharts.ConstantLine();
            DevExpress.XtraCharts.XYDiagramPane xyDiagramPane1 = new DevExpress.XtraCharts.XYDiagramPane();
            DevExpress.XtraCharts.SecondaryAxisY secondaryAxisY1 = new DevExpress.XtraCharts.SecondaryAxisY();
            DevExpress.XtraCharts.SecondaryAxisY secondaryAxisY2 = new DevExpress.XtraCharts.SecondaryAxisY();
            DevExpress.XtraCharts.Series series1 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.LineSeriesView lineSeriesView1 = new DevExpress.XtraCharts.LineSeriesView();
            DevExpress.XtraCharts.Series series2 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.SideBySideBarSeriesView sideBySideBarSeriesView1 = new DevExpress.XtraCharts.SideBySideBarSeriesView();
            DevExpress.XtraCharts.Series series3 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.LineSeriesView lineSeriesView2 = new DevExpress.XtraCharts.LineSeriesView();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.chartControl1 = new DevExpress.XtraCharts.ChartControl();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagramPane1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(secondaryAxisY1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(secondaryAxisY2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(lineSeriesView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(lineSeriesView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.chartControl1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(1365, 678);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // chartControl1
            // 
            this.chartControl1.AutoLayout = false;
            this.chartControl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.chartControl1.CrosshairOptions.ArgumentLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(51)))), ((int)(((byte)(0)))));
            this.chartControl1.CrosshairOptions.HighlightPoints = false;
            this.chartControl1.CrosshairOptions.ShowArgumentLabels = true;
            this.chartControl1.CrosshairOptions.ShowOnlyInFocusedPane = false;
            this.chartControl1.CrosshairOptions.ValueLineColor = System.Drawing.Color.White;
            xyDiagram1.AxisX.Color = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(37)))), ((int)(((byte)(0)))));
            constantLine1.AxisValueSerializable = "09:30";
            constantLine1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(37)))), ((int)(((byte)(0)))));
            constantLine1.Name = "Constant Line 1";
            constantLine1.ShowBehind = true;
            constantLine1.ShowInLegend = false;
            constantLine2.AxisValueSerializable = "10:00";
            constantLine2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(37)))), ((int)(((byte)(0)))));
            constantLine2.LineStyle.DashStyle = DevExpress.XtraCharts.DashStyle.Dot;
            constantLine2.Name = "Constant Line 2";
            constantLine2.ShowBehind = true;
            constantLine2.ShowInLegend = false;
            constantLine3.AxisValueSerializable = "10:30";
            constantLine3.Color = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(37)))), ((int)(((byte)(0)))));
            constantLine3.Name = "Constant Line 3";
            constantLine3.ShowBehind = true;
            constantLine3.ShowInLegend = false;
            constantLine4.AxisValueSerializable = "11:00";
            constantLine4.Color = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(37)))), ((int)(((byte)(0)))));
            constantLine4.LineStyle.DashStyle = DevExpress.XtraCharts.DashStyle.Dot;
            constantLine4.Name = "Constant Line 4";
            constantLine4.ShowBehind = true;
            constantLine4.ShowInLegend = false;
            constantLine5.AxisValueSerializable = "11:30";
            constantLine5.Color = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(37)))), ((int)(((byte)(0)))));
            constantLine5.LineStyle.Thickness = 2;
            constantLine5.Name = "Constant Line 5";
            constantLine5.ShowBehind = true;
            constantLine5.ShowInLegend = false;
            constantLine6.AxisValueSerializable = "13:30";
            constantLine6.Color = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(37)))), ((int)(((byte)(0)))));
            constantLine6.LineStyle.DashStyle = DevExpress.XtraCharts.DashStyle.Dot;
            constantLine6.Name = "Constant Line 6";
            constantLine6.ShowBehind = true;
            constantLine6.ShowInLegend = false;
            constantLine7.AxisValueSerializable = "14:00";
            constantLine7.Color = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(37)))), ((int)(((byte)(0)))));
            constantLine7.Name = "Constant Line 7";
            constantLine7.ShowBehind = true;
            constantLine7.ShowInLegend = false;
            constantLine8.AxisValueSerializable = "14:30";
            constantLine8.Color = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(37)))), ((int)(((byte)(0)))));
            constantLine8.LineStyle.DashStyle = DevExpress.XtraCharts.DashStyle.Dot;
            constantLine8.Name = "Constant Line 8";
            constantLine8.ShowBehind = true;
            constantLine8.ShowInLegend = false;
            xyDiagram1.AxisX.ConstantLines.AddRange(new DevExpress.XtraCharts.ConstantLine[] {
            constantLine1,
            constantLine2,
            constantLine3,
            constantLine4,
            constantLine5,
            constantLine6,
            constantLine7,
            constantLine8});
            xyDiagram1.AxisX.GridLines.Color = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(37)))), ((int)(((byte)(0)))));
            xyDiagram1.AxisX.Label.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(51)))), ((int)(((byte)(0)))));
            xyDiagram1.AxisX.Tag = "x";
            xyDiagram1.AxisX.Tickmarks.MinorVisible = false;
            xyDiagram1.AxisX.Tickmarks.Visible = false;
            xyDiagram1.AxisX.Visibility = DevExpress.Utils.DefaultBoolean.True;
            xyDiagram1.AxisX.VisibleInPanesSerializable = "0";
            xyDiagram1.AxisX.VisualRange.Auto = false;
            xyDiagram1.AxisX.VisualRange.MaxValueSerializable = "J";
            xyDiagram1.AxisX.VisualRange.MinValueSerializable = "A";
            xyDiagram1.AxisX.WholeRange.Auto = false;
            xyDiagram1.AxisX.WholeRange.MaxValueSerializable = "J";
            xyDiagram1.AxisX.WholeRange.MinValueSerializable = "A";
            xyDiagram1.AxisY.Color = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(37)))), ((int)(((byte)(0)))));
            xyDiagram1.AxisY.GridLines.Color = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(37)))), ((int)(((byte)(0)))));
            xyDiagram1.AxisY.Label.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(51)))), ((int)(((byte)(0)))));
            xyDiagram1.AxisY.Label.TextPattern = "{V:F2}";
            xyDiagram1.AxisY.Tag = "y";
            xyDiagram1.AxisY.Tickmarks.MinorVisible = false;
            xyDiagram1.AxisY.Tickmarks.Visible = false;
            xyDiagram1.AxisY.VisibleInPanesSerializable = "-1";
            xyDiagram1.DefaultPane.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            xyDiagram1.DefaultPane.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(37)))), ((int)(((byte)(0)))));
            xyDiagramPane1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            xyDiagramPane1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(37)))), ((int)(((byte)(0)))));
            xyDiagramPane1.Name = "Pane 1";
            xyDiagramPane1.PaneID = 0;
            xyDiagramPane1.Weight = 0.26D;
            xyDiagram1.Panes.AddRange(new DevExpress.XtraCharts.XYDiagramPane[] {
            xyDiagramPane1});
            secondaryAxisY1.AxisID = 0;
            secondaryAxisY1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(37)))), ((int)(((byte)(0)))));
            secondaryAxisY1.Label.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(51)))), ((int)(((byte)(0)))));
            secondaryAxisY1.Label.TextPattern = "{V:0.00%}";
            secondaryAxisY1.Name = "Secondary AxisY 1";
            secondaryAxisY1.Tag = "y1";
            secondaryAxisY1.Tickmarks.MinorVisible = false;
            secondaryAxisY1.Tickmarks.Visible = false;
            secondaryAxisY1.VisibleInPanesSerializable = "-1";
            secondaryAxisY2.Alignment = DevExpress.XtraCharts.AxisAlignment.Near;
            secondaryAxisY2.AxisID = 1;
            secondaryAxisY2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(37)))), ((int)(((byte)(0)))));
            secondaryAxisY2.GridLines.Color = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(37)))), ((int)(((byte)(0)))));
            secondaryAxisY2.GridLines.Visible = true;
            secondaryAxisY2.Label.TextColor = System.Drawing.Color.SkyBlue;
            secondaryAxisY2.Name = "Secondary AxisY 2";
            secondaryAxisY2.Tag = "y2";
            secondaryAxisY2.Tickmarks.MinorVisible = false;
            secondaryAxisY2.Tickmarks.Visible = false;
            secondaryAxisY2.VisibleInPanesSerializable = "0";
            xyDiagram1.SecondaryAxesY.AddRange(new DevExpress.XtraCharts.SecondaryAxisY[] {
            secondaryAxisY1,
            secondaryAxisY2});
            this.chartControl1.Diagram = xyDiagram1;
            this.chartControl1.Location = new System.Drawing.Point(12, 29);
            this.chartControl1.Name = "chartControl1";
            this.chartControl1.SelectionMode = DevExpress.XtraCharts.ElementSelectionMode.Single;
            series1.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
            series1.Name = "Price";
            series1.ShowInLegend = false;
            lineSeriesView1.Color = System.Drawing.Color.White;
            lineSeriesView1.LineStyle.Thickness = 1;
            series1.View = lineSeriesView1;
            series2.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
            series2.Name = "Volume";
            series2.ShowInLegend = false;
            sideBySideBarSeriesView1.AxisYName = "Secondary AxisY 2";
            sideBySideBarSeriesView1.BarWidth = 0.1D;
            sideBySideBarSeriesView1.Border.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            sideBySideBarSeriesView1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            sideBySideBarSeriesView1.FillStyle.FillMode = DevExpress.XtraCharts.FillMode.Solid;
            sideBySideBarSeriesView1.PaneName = "Pane 1";
            series2.View = sideBySideBarSeriesView1;
            series3.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
            series3.Name = "AvgPrice";
            series3.ShowInLegend = false;
            lineSeriesView2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(192)))), ((int)(((byte)(143)))));
            lineSeriesView2.LineStyle.Thickness = 1;
            series3.View = lineSeriesView2;
            this.chartControl1.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series1,
        series2,
        series3};
            this.chartControl1.SeriesTemplate.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
            this.chartControl1.Size = new System.Drawing.Size(1341, 637);
            this.chartControl1.TabIndex = 4;
            this.chartControl1.CustomDrawAxisLabel += new DevExpress.XtraCharts.CustomDrawAxisLabelEventHandler(this.chartControl1_CustomDrawAxisLabel);
            this.chartControl1.Paint += new System.Windows.Forms.PaintEventHandler(this.chartControl1_Paint);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(1365, 678);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.chartControl1;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1345, 658);
            this.layoutControlItem1.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(105, 14);
            // 
            // FrmTimeSharingTradeIdentifier
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1365, 678);
            this.Controls.Add(this.layoutControl1);
            this.Name = "FrmTimeSharingTradeIdentifier";
            this.Text = "FrmTimeSharingTradeIdentifier";
            this.Load += new System.EventHandler(this.FrmTimeSharingTradeIdentifier_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(xyDiagramPane1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(secondaryAxisY1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(secondaryAxisY2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(lineSeriesView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(lineSeriesView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraCharts.ChartControl chartControl1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
    }
}