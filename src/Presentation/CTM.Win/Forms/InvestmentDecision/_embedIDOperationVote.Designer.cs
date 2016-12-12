namespace CTM.Win.Forms.InvestmentDecision
{
    partial class _embedIDOperationVote
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
            this.colInvestorCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colInvestorName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTypeName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colWeightPercentage = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFlagName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colReason = new DevExpress.XtraGrid.Columns.GridColumn();
            this.riRTxtReasonContent = new DevExpress.XtraEditors.Repository.RepositoryItemRichTextEdit();
            this.colVoteTime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colConfirmTime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lciResult = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riRTxtReasonContent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciResult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.gridControl1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(1331, 631);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // gridControl1
            // 
            this.gridControl1.Location = new System.Drawing.Point(12, 32);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.riRTxtReasonContent});
            this.gridControl1.Size = new System.Drawing.Size(1297, 587);
            this.gridControl1.TabIndex = 4;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colInvestorCode,
            this.colInvestorName,
            this.colTypeName,
            this.colWeightPercentage,
            this.colFlagName,
            this.colReason,
            this.colVoteTime,
            this.colConfirmTime});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gridView1_CustomDrawRowIndicator);
            // 
            // colInvestorCode
            // 
            this.colInvestorCode.Caption = "人员编号";
            this.colInvestorCode.FieldName = "InvestorCode";
            this.colInvestorCode.Name = "colInvestorCode";
            this.colInvestorCode.Visible = true;
            this.colInvestorCode.VisibleIndex = 0;
            this.colInvestorCode.Width = 100;
            // 
            // colInvestorName
            // 
            this.colInvestorName.Caption = "姓名";
            this.colInvestorName.FieldName = "InvestorName";
            this.colInvestorName.Name = "colInvestorName";
            this.colInvestorName.Visible = true;
            this.colInvestorName.VisibleIndex = 1;
            this.colInvestorName.Width = 100;
            // 
            // colTypeName
            // 
            this.colTypeName.Caption = "类别";
            this.colTypeName.FieldName = "TypeName";
            this.colTypeName.Name = "colTypeName";
            this.colTypeName.Visible = true;
            this.colTypeName.VisibleIndex = 2;
            this.colTypeName.Width = 140;
            // 
            // colWeightPercentage
            // 
            this.colWeightPercentage.Caption = "权重";
            this.colWeightPercentage.DisplayFormat.FormatString = "0.#0";
            this.colWeightPercentage.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colWeightPercentage.FieldName = "WeightPercentage";
            this.colWeightPercentage.Name = "colWeightPercentage";
            this.colWeightPercentage.Visible = true;
            this.colWeightPercentage.VisibleIndex = 3;
            this.colWeightPercentage.Width = 80;
            // 
            // colFlagName
            // 
            this.colFlagName.Caption = "投票信息";
            this.colFlagName.FieldName = "FlagName";
            this.colFlagName.Name = "colFlagName";
            this.colFlagName.Visible = true;
            this.colFlagName.VisibleIndex = 4;
            this.colFlagName.Width = 80;
            // 
            // colReason
            // 
            this.colReason.AppearanceCell.Options.UseTextOptions = true;
            this.colReason.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colReason.Caption = "理由";
            this.colReason.ColumnEdit = this.riRTxtReasonContent;
            this.colReason.FieldName = "Reason";
            this.colReason.Name = "colReason";
            this.colReason.Visible = true;
            this.colReason.VisibleIndex = 5;
            this.colReason.Width = 550;
            // 
            // riRTxtReasonContent
            // 
            this.riRTxtReasonContent.Name = "riRTxtReasonContent";
            this.riRTxtReasonContent.OptionsExport.PlainText.ExportFinalParagraphMark = DevExpress.XtraRichEdit.Export.PlainText.ExportFinalParagraphMark.Never;
            this.riRTxtReasonContent.ShowCaretInReadOnly = false;
            // 
            // colVoteTime
            // 
            this.colVoteTime.Caption = "投票日期";
            this.colVoteTime.FieldName = "VoteTime";
            this.colVoteTime.Name = "colVoteTime";
            this.colVoteTime.Visible = true;
            this.colVoteTime.VisibleIndex = 6;
            this.colVoteTime.Width = 100;
            // 
            // colConfirmTime
            // 
            this.colConfirmTime.Caption = "确定日期";
            this.colConfirmTime.FieldName = "ConfirmTime";
            this.colConfirmTime.Name = "colConfirmTime";
            this.colConfirmTime.Visible = true;
            this.colConfirmTime.VisibleIndex = 7;
            this.colConfirmTime.Width = 100;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lciResult,
            this.emptySpaceItem1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(1331, 631);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // lciResult
            // 
            this.lciResult.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.lciResult.AppearanceItemCaption.Options.UseFont = true;
            this.lciResult.AppearanceItemCaption.Options.UseTextOptions = true;
            this.lciResult.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lciResult.Control = this.gridControl1;
            this.lciResult.Location = new System.Drawing.Point(0, 0);
            this.lciResult.Name = "lciResult";
            this.lciResult.Size = new System.Drawing.Size(1301, 611);
            this.lciResult.Text = "交易单投票结果";
            this.lciResult.TextLocation = DevExpress.Utils.Locations.Top;
            this.lciResult.TextSize = new System.Drawing.Size(105, 17);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(1301, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(10, 611);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // _embedIDOperationVote
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1331, 631);
            this.Controls.Add(this.layoutControl1);
            this.Name = "_embedIDOperationVote";
            this.Text = "_embedIDOperationVote";
            this.Load += new System.EventHandler(this._dialogIDVoteResult_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riRTxtReasonContent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciResult)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem lciResult;
        private DevExpress.XtraGrid.Columns.GridColumn colInvestorCode;
        private DevExpress.XtraGrid.Columns.GridColumn colInvestorName;
        private DevExpress.XtraGrid.Columns.GridColumn colWeightPercentage;
        private DevExpress.XtraGrid.Columns.GridColumn colFlagName;
        private DevExpress.XtraGrid.Columns.GridColumn colReason;
        private DevExpress.XtraGrid.Columns.GridColumn colVoteTime;
        private DevExpress.XtraGrid.Columns.GridColumn colConfirmTime;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraGrid.Columns.GridColumn colTypeName;
        private DevExpress.XtraEditors.Repository.RepositoryItemRichTextEdit riRTxtReasonContent;
    }
}