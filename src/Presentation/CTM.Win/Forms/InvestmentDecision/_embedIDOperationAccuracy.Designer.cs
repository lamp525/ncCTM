namespace CTM.Win.Forms.InvestmentDecision
{
    partial class _embedIDOperationAccuracy
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
            this.chkAdminVeto = new DevExpress.XtraEditors.CheckEdit();
            this.btnRevoke = new DevExpress.XtraEditors.SimpleButton();
            this.btnAbstain = new DevExpress.XtraEditors.SimpleButton();
            this.btnOppose = new DevExpress.XtraEditors.SimpleButton();
            this.btnApproval = new DevExpress.XtraEditors.SimpleButton();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colInvestorCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colInvestorName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colWeightPercentage = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFlagName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colReasonContent = new DevExpress.XtraGrid.Columns.GridColumn();
            this.riMemoReasonContent = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.colJudgeTime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lcgResult = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lciResult = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcgVote = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem4 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem5 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem6 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lciAdminVeto = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem7 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.esiVoteStatusInfo = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkAdminVeto.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riMemoReasonContent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgResult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciResult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgVote)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciAdminVeto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.esiVoteStatusInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.chkAdminVeto);
            this.layoutControl1.Controls.Add(this.btnRevoke);
            this.layoutControl1.Controls.Add(this.btnAbstain);
            this.layoutControl1.Controls.Add(this.btnOppose);
            this.layoutControl1.Controls.Add(this.btnApproval);
            this.layoutControl1.Controls.Add(this.gridControl1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(999, 631);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // chkAdminVeto
            // 
            this.chkAdminVeto.Location = new System.Drawing.Point(120, 573);
            this.chkAdminVeto.Name = "chkAdminVeto";
            this.chkAdminVeto.Properties.Caption = "管理员一票决定权";
            this.chkAdminVeto.Size = new System.Drawing.Size(118, 19);
            this.chkAdminVeto.StyleController = this.layoutControl1;
            this.chkAdminVeto.TabIndex = 11;
            this.chkAdminVeto.CheckedChanged += new System.EventHandler(this.chkAdminVeto_CheckedChanged);
            // 
            // btnRevoke
            // 
            this.btnRevoke.Location = new System.Drawing.Point(600, 573);
            this.btnRevoke.Name = "btnRevoke";
            this.btnRevoke.Size = new System.Drawing.Size(75, 22);
            this.btnRevoke.StyleController = this.layoutControl1;
            this.btnRevoke.TabIndex = 10;
            this.btnRevoke.Text = "    撤  销    ";
            this.btnRevoke.Click += new System.EventHandler(this.btnRevoke_Click);
            // 
            // btnAbstain
            // 
            this.btnAbstain.Location = new System.Drawing.Point(511, 573);
            this.btnAbstain.Name = "btnAbstain";
            this.btnAbstain.Size = new System.Drawing.Size(75, 22);
            this.btnAbstain.StyleController = this.layoutControl1;
            this.btnAbstain.TabIndex = 9;
            this.btnAbstain.Text = "    弃  权    ";
            this.btnAbstain.Click += new System.EventHandler(this.btnAbstain_Click);
            // 
            // btnOppose
            // 
            this.btnOppose.Location = new System.Drawing.Point(418, 573);
            this.btnOppose.Name = "btnOppose";
            this.btnOppose.Size = new System.Drawing.Size(79, 22);
            this.btnOppose.StyleController = this.layoutControl1;
            this.btnOppose.TabIndex = 8;
            this.btnOppose.Text = "    不准确    ";
            this.btnOppose.Click += new System.EventHandler(this.btnOppose_Click);
            // 
            // btnApproval
            // 
            this.btnApproval.Location = new System.Drawing.Point(325, 573);
            this.btnApproval.Name = "btnApproval";
            this.btnApproval.Size = new System.Drawing.Size(79, 22);
            this.btnApproval.StyleController = this.layoutControl1;
            this.btnApproval.TabIndex = 7;
            this.btnApproval.Text = "    准  确     ";
            this.btnApproval.Click += new System.EventHandler(this.btnApproval_Click);
            // 
            // gridControl1
            // 
            this.gridControl1.Location = new System.Drawing.Point(24, 68);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.riMemoReasonContent});
            this.gridControl1.Size = new System.Drawing.Size(941, 470);
            this.gridControl1.TabIndex = 4;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colInvestorCode,
            this.colInvestorName,
            this.colWeightPercentage,
            this.colFlagName,
            this.colReasonContent,
            this.colJudgeTime});
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
            this.colInvestorCode.Width = 65;
            // 
            // colInvestorName
            // 
            this.colInvestorName.Caption = "投票人员";
            this.colInvestorName.FieldName = "InvestorName";
            this.colInvestorName.Name = "colInvestorName";
            this.colInvestorName.Visible = true;
            this.colInvestorName.VisibleIndex = 0;
            this.colInvestorName.Width = 102;
            // 
            // colWeightPercentage
            // 
            this.colWeightPercentage.Caption = "权重";
            this.colWeightPercentage.DisplayFormat.FormatString = "0.#0";
            this.colWeightPercentage.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colWeightPercentage.FieldName = "WeightPercentage";
            this.colWeightPercentage.Name = "colWeightPercentage";
            this.colWeightPercentage.Visible = true;
            this.colWeightPercentage.VisibleIndex = 1;
            this.colWeightPercentage.Width = 94;
            // 
            // colFlagName
            // 
            this.colFlagName.Caption = "评定信息";
            this.colFlagName.FieldName = "FlagName";
            this.colFlagName.Name = "colFlagName";
            this.colFlagName.Visible = true;
            this.colFlagName.VisibleIndex = 2;
            this.colFlagName.Width = 65;
            // 
            // colReasonContent
            // 
            this.colReasonContent.AppearanceCell.Options.UseTextOptions = true;
            this.colReasonContent.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colReasonContent.Caption = "理由说明";
            this.colReasonContent.ColumnEdit = this.riMemoReasonContent;
            this.colReasonContent.FieldName = "ReasonContent";
            this.colReasonContent.Name = "colReasonContent";
            this.colReasonContent.Visible = true;
            this.colReasonContent.VisibleIndex = 3;
            this.colReasonContent.Width = 469;
            // 
            // riMemoReasonContent
            // 
            this.riMemoReasonContent.Name = "riMemoReasonContent";
            // 
            // colJudgeTime
            // 
            this.colJudgeTime.Caption = "评定日期";
            this.colJudgeTime.FieldName = "JudgeTime";
            this.colJudgeTime.Name = "colJudgeTime";
            this.colJudgeTime.Visible = true;
            this.colJudgeTime.VisibleIndex = 4;
            this.colJudgeTime.Width = 100;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem1,
            this.lcgResult});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(999, 631);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(969, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(10, 611);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lcgResult
            // 
            this.lcgResult.AppearanceGroup.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lcgResult.AppearanceGroup.Options.UseFont = true;
            this.lcgResult.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lciResult,
            this.lcgVote,
            this.esiVoteStatusInfo});
            this.lcgResult.Location = new System.Drawing.Point(0, 0);
            this.lcgResult.Name = "lcgResult";
            this.lcgResult.Size = new System.Drawing.Size(969, 611);
            this.lcgResult.Text = "准确度投票结果";
            // 
            // lciResult
            // 
            this.lciResult.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.lciResult.AppearanceItemCaption.Options.UseFont = true;
            this.lciResult.AppearanceItemCaption.Options.UseTextOptions = true;
            this.lciResult.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lciResult.Control = this.gridControl1;
            this.lciResult.Location = new System.Drawing.Point(0, 25);
            this.lciResult.Name = "lciResult";
            this.lciResult.Size = new System.Drawing.Size(945, 474);
            this.lciResult.TextLocation = DevExpress.Utils.Locations.Top;
            this.lciResult.TextSize = new System.Drawing.Size(0, 0);
            this.lciResult.TextVisible = false;
            // 
            // lcgVote
            // 
            this.lcgVote.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem2,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem3,
            this.layoutControlItem6,
            this.emptySpaceItem3,
            this.emptySpaceItem4,
            this.emptySpaceItem5,
            this.emptySpaceItem6,
            this.lciAdminVeto,
            this.emptySpaceItem7});
            this.lcgVote.Location = new System.Drawing.Point(0, 499);
            this.lcgVote.Name = "lcgVote";
            this.lcgVote.Size = new System.Drawing.Size(945, 69);
            this.lcgVote.Text = "投票操作";
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(643, 0);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(278, 26);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.btnOppose;
            this.layoutControlItem4.Location = new System.Drawing.Point(382, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(83, 26);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.btnAbstain;
            this.layoutControlItem5.Location = new System.Drawing.Point(475, 0);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(79, 26);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btnApproval;
            this.layoutControlItem3.Location = new System.Drawing.Point(289, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(83, 26);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.btnRevoke;
            this.layoutControlItem6.Location = new System.Drawing.Point(564, 0);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(79, 26);
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextVisible = false;
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            this.emptySpaceItem3.Location = new System.Drawing.Point(0, 0);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(84, 26);
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem4
            // 
            this.emptySpaceItem4.AllowHotTrack = false;
            this.emptySpaceItem4.Location = new System.Drawing.Point(372, 0);
            this.emptySpaceItem4.Name = "emptySpaceItem4";
            this.emptySpaceItem4.Size = new System.Drawing.Size(10, 26);
            this.emptySpaceItem4.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem5
            // 
            this.emptySpaceItem5.AllowHotTrack = false;
            this.emptySpaceItem5.Location = new System.Drawing.Point(465, 0);
            this.emptySpaceItem5.Name = "emptySpaceItem5";
            this.emptySpaceItem5.Size = new System.Drawing.Size(10, 26);
            this.emptySpaceItem5.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem6
            // 
            this.emptySpaceItem6.AllowHotTrack = false;
            this.emptySpaceItem6.Location = new System.Drawing.Point(554, 0);
            this.emptySpaceItem6.Name = "emptySpaceItem6";
            this.emptySpaceItem6.Size = new System.Drawing.Size(10, 26);
            this.emptySpaceItem6.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lciAdminVeto
            // 
            this.lciAdminVeto.Control = this.chkAdminVeto;
            this.lciAdminVeto.Location = new System.Drawing.Point(84, 0);
            this.lciAdminVeto.Name = "lciAdminVeto";
            this.lciAdminVeto.Size = new System.Drawing.Size(122, 26);
            this.lciAdminVeto.TextSize = new System.Drawing.Size(0, 0);
            this.lciAdminVeto.TextVisible = false;
            // 
            // emptySpaceItem7
            // 
            this.emptySpaceItem7.AllowHotTrack = false;
            this.emptySpaceItem7.Location = new System.Drawing.Point(206, 0);
            this.emptySpaceItem7.Name = "emptySpaceItem7";
            this.emptySpaceItem7.Size = new System.Drawing.Size(83, 26);
            this.emptySpaceItem7.TextSize = new System.Drawing.Size(0, 0);
            // 
            // esiVoteStatusInfo
            // 
            this.esiVoteStatusInfo.AllowHotTrack = false;
            this.esiVoteStatusInfo.Location = new System.Drawing.Point(0, 0);
            this.esiVoteStatusInfo.Name = "esiVoteStatusInfo";
            this.esiVoteStatusInfo.Size = new System.Drawing.Size(945, 25);
            this.esiVoteStatusInfo.Text = "投票状态：     投票分数：";
            this.esiVoteStatusInfo.TextSize = new System.Drawing.Size(0, 0);
            this.esiVoteStatusInfo.TextVisible = true;
            // 
            // _embedIDOperationAccuracy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(999, 631);
            this.Controls.Add(this.layoutControl1);
            this.Name = "_embedIDOperationAccuracy";
            this.Text = "_embedIDOperationAccuracy";
            this.Load += new System.EventHandler(this._dialogIDVoteResult_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkAdminVeto.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riMemoReasonContent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgResult)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciResult)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgVote)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciAdminVeto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.esiVoteStatusInfo)).EndInit();
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
        private DevExpress.XtraGrid.Columns.GridColumn colReasonContent;
        private DevExpress.XtraGrid.Columns.GridColumn colJudgeTime;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit riMemoReasonContent;
        private DevExpress.XtraEditors.SimpleButton btnRevoke;
        private DevExpress.XtraEditors.SimpleButton btnAbstain;
        private DevExpress.XtraEditors.SimpleButton btnOppose;
        private DevExpress.XtraEditors.SimpleButton btnApproval;
        private DevExpress.XtraLayout.LayoutControlGroup lcgVote;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlGroup lcgResult;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem4;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem5;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem6;
        private DevExpress.XtraLayout.EmptySpaceItem esiVoteStatusInfo;
        private DevExpress.XtraEditors.CheckEdit chkAdminVeto;
        private DevExpress.XtraLayout.LayoutControlItem lciAdminVeto;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem7;
    }
}