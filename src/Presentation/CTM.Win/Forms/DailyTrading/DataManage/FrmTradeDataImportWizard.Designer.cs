namespace CTM.Win.Forms.DailyTrading.DataManage
{
    partial class FrmTradeDataImportWizard
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
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.PagePreview = new DevExpress.XtraWizard.WizardPage();
            this.layoutControl3 = new DevExpress.XtraLayout.LayoutControl();
            this.luTargetPrincipal = new DevExpress.XtraEditors.LookUpEdit();
            this.luBandPrincipal = new DevExpress.XtraEditors.LookUpEdit();
            this.btnExcelTemplate = new DevExpress.XtraEditors.SimpleButton();
            this.gridControlPreview = new DevExpress.XtraGrid.GridControl();
            this.gridViewPreview = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutControlGroup5 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem18 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem4 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem20 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem21 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem19 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem14 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem15 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.PageSelectFile = new DevExpress.XtraWizard.WizardPage();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.chkEntrust = new DevExpress.XtraEditors.CheckEdit();
            this.txtImportUser = new DevExpress.XtraEditors.TextEdit();
            this.txtAccountPlan = new DevExpress.XtraEditors.TextEdit();
            this.checkDelivery = new DevExpress.XtraEditors.CheckEdit();
            this.checkDaily = new DevExpress.XtraEditors.CheckEdit();
            this.lblDataType = new DevExpress.XtraEditors.LabelControl();
            this.txtAccountType = new DevExpress.XtraEditors.TextEdit();
            this.luOperator = new DevExpress.XtraEditors.LookUpEdit();
            this.btnBrowse = new DevExpress.XtraEditors.SimpleButton();
            this.txtImportFileName = new DevExpress.XtraEditors.TextEdit();
            this.txtAccountAttribute = new DevExpress.XtraEditors.TextEdit();
            this.txtSecurityCompany = new DevExpress.XtraEditors.TextEdit();
            this.txtAccountName = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem7 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem13 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem16 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem12 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem11 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem5 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem17 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem10 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem8 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.PageFinish = new DevExpress.XtraWizard.CompletionWizardPage();
            this.layoutControl4 = new DevExpress.XtraLayout.LayoutControl();
            this.gridControlSkip = new DevExpress.XtraGrid.GridControl();
            this.gridViewSkip = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutControlGroup6 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lciSkip = new DevExpress.XtraLayout.LayoutControlItem();
            this.esiImportResult = new DevExpress.XtraLayout.EmptySpaceItem();
            this.PageAccount = new DevExpress.XtraWizard.WelcomeWizardPage();
            this.layoutControl2 = new DevExpress.XtraLayout.LayoutControl();
            this.luSecurityCompany = new DevExpress.XtraEditors.LookUpEdit();
            this.gridControlAccount = new DevExpress.XtraGrid.GridControl();
            this.gridViewAccount = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colAccountId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAccountName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTypeCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSecurityCompanyName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTypeName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAttributeCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAttributeName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPlanCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPlanName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSecurityCompanyCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOperatorNames = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStampDutyRate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCommissionRate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIncidentalsRate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNeedAccounting = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsDisabled = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRemarks = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cbAccountAttribute = new DevExpress.XtraEditors.ComboBoxEdit();
            this.layoutControlGroup4 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem13 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem14 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem15 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem9 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem11 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem10 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.wizardControl1 = new DevExpress.XtraWizard.WizardControl();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.PagePreview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl3)).BeginInit();
            this.layoutControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.luTargetPrincipal.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.luBandPrincipal.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlPreview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewPreview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem18)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem20)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem21)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem19)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem14)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem15)).BeginInit();
            this.PageSelectFile.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkEntrust.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtImportUser.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAccountPlan.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkDelivery.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkDaily.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAccountType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.luOperator.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtImportFileName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAccountAttribute.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSecurityCompany.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAccountName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem16)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem17)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            this.PageFinish.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl4)).BeginInit();
            this.layoutControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlSkip)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewSkip)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciSkip)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.esiImportResult)).BeginInit();
            this.PageAccount.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).BeginInit();
            this.layoutControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.luSecurityCompany.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlAccount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewAccount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbAccountAttribute.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem14)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem15)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.wizardControl1)).BeginInit();
            this.wizardControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // PagePreview
            // 
            this.PagePreview.Controls.Add(this.layoutControl3);
            this.PagePreview.DescriptionText = "请核对导入记录的列名格式和数据是否正确无误";
            this.PagePreview.Name = "PagePreview";
            this.PagePreview.Size = new System.Drawing.Size(895, 399);
            this.PagePreview.Text = "待导入交易记录预览";
            // 
            // layoutControl3
            // 
            this.layoutControl3.Controls.Add(this.luTargetPrincipal);
            this.layoutControl3.Controls.Add(this.luBandPrincipal);
            this.layoutControl3.Controls.Add(this.btnExcelTemplate);
            this.layoutControl3.Controls.Add(this.gridControlPreview);
            this.layoutControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl3.Location = new System.Drawing.Point(0, 0);
            this.layoutControl3.Name = "layoutControl3";
            this.layoutControl3.Root = this.layoutControlGroup5;
            this.layoutControl3.Size = new System.Drawing.Size(895, 399);
            this.layoutControl3.TabIndex = 1;
            this.layoutControl3.Text = "layoutControl3";
            // 
            // luTargetPrincipal
            // 
            this.luTargetPrincipal.Location = new System.Drawing.Point(342, 12);
            this.luTargetPrincipal.Name = "luTargetPrincipal";
            this.luTargetPrincipal.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.luTargetPrincipal.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Code", "编码"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "姓名")});
            this.luTargetPrincipal.Size = new System.Drawing.Size(169, 20);
            this.luTargetPrincipal.StyleController = this.layoutControl3;
            this.luTargetPrincipal.TabIndex = 6;
            // 
            // luBandPrincipal
            // 
            this.luBandPrincipal.Location = new System.Drawing.Point(92, 12);
            this.luBandPrincipal.Name = "luBandPrincipal";
            this.luBandPrincipal.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.luBandPrincipal.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Code", "编码"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "姓名")});
            this.luBandPrincipal.Size = new System.Drawing.Size(169, 20);
            this.luBandPrincipal.StyleController = this.layoutControl3;
            this.luBandPrincipal.TabIndex = 5;
            // 
            // btnExcelTemplate
            // 
            this.btnExcelTemplate.Location = new System.Drawing.Point(754, 12);
            this.btnExcelTemplate.Name = "btnExcelTemplate";
            this.btnExcelTemplate.Size = new System.Drawing.Size(119, 22);
            this.btnExcelTemplate.StyleController = this.layoutControl3;
            this.btnExcelTemplate.TabIndex = 4;
            this.btnExcelTemplate.Text = " 查看导入Excel模板 ";
            this.btnExcelTemplate.Click += new System.EventHandler(this.btnExcelTemplate_Click);
            // 
            // gridControlPreview
            // 
            this.gridControlPreview.Location = new System.Drawing.Point(12, 52);
            this.gridControlPreview.MainView = this.gridViewPreview;
            this.gridControlPreview.Name = "gridControlPreview";
            this.gridControlPreview.Size = new System.Drawing.Size(871, 335);
            this.gridControlPreview.TabIndex = 0;
            this.gridControlPreview.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewPreview});
            // 
            // gridViewPreview
            // 
            this.gridViewPreview.GridControl = this.gridControlPreview;
            this.gridViewPreview.Name = "gridViewPreview";
            this.gridViewPreview.OptionsBehavior.Editable = false;
            this.gridViewPreview.OptionsView.ShowFooter = true;
            this.gridViewPreview.OptionsView.ShowGroupPanel = false;
            this.gridViewPreview.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gridViewPreview_CustomDrawRowIndicator);
            // 
            // layoutControlGroup5
            // 
            this.layoutControlGroup5.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup5.GroupBordersVisible = false;
            this.layoutControlGroup5.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem18,
            this.emptySpaceItem1,
            this.emptySpaceItem2,
            this.emptySpaceItem4,
            this.layoutControlItem20,
            this.layoutControlItem21,
            this.layoutControlItem19,
            this.emptySpaceItem14,
            this.emptySpaceItem15});
            this.layoutControlGroup5.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup5.Name = "layoutControlGroup5";
            this.layoutControlGroup5.Size = new System.Drawing.Size(895, 399);
            this.layoutControlGroup5.TextVisible = false;
            // 
            // layoutControlItem18
            // 
            this.layoutControlItem18.Control = this.gridControlPreview;
            this.layoutControlItem18.Location = new System.Drawing.Point(0, 40);
            this.layoutControlItem18.Name = "layoutControlItem18";
            this.layoutControlItem18.Size = new System.Drawing.Size(875, 339);
            this.layoutControlItem18.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem18.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem18.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(865, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(10, 26);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 0);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(13, 26);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem4
            // 
            this.emptySpaceItem4.AllowHotTrack = false;
            this.emptySpaceItem4.Location = new System.Drawing.Point(0, 26);
            this.emptySpaceItem4.Name = "emptySpaceItem4";
            this.emptySpaceItem4.Size = new System.Drawing.Size(875, 14);
            this.emptySpaceItem4.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem20
            // 
            this.layoutControlItem20.Control = this.luBandPrincipal;
            this.layoutControlItem20.Location = new System.Drawing.Point(13, 0);
            this.layoutControlItem20.Name = "layoutControlItem20";
            this.layoutControlItem20.Size = new System.Drawing.Size(240, 26);
            this.layoutControlItem20.Text = "波段收益人:";
            this.layoutControlItem20.TextSize = new System.Drawing.Size(64, 14);
            // 
            // layoutControlItem21
            // 
            this.layoutControlItem21.Control = this.luTargetPrincipal;
            this.layoutControlItem21.Location = new System.Drawing.Point(263, 0);
            this.layoutControlItem21.Name = "layoutControlItem21";
            this.layoutControlItem21.Size = new System.Drawing.Size(240, 26);
            this.layoutControlItem21.Text = "目标收益人:";
            this.layoutControlItem21.TextSize = new System.Drawing.Size(64, 14);
            // 
            // layoutControlItem19
            // 
            this.layoutControlItem19.Control = this.btnExcelTemplate;
            this.layoutControlItem19.Location = new System.Drawing.Point(742, 0);
            this.layoutControlItem19.Name = "layoutControlItem19";
            this.layoutControlItem19.Size = new System.Drawing.Size(123, 26);
            this.layoutControlItem19.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem19.TextVisible = false;
            // 
            // emptySpaceItem14
            // 
            this.emptySpaceItem14.AllowHotTrack = false;
            this.emptySpaceItem14.Location = new System.Drawing.Point(253, 0);
            this.emptySpaceItem14.Name = "emptySpaceItem14";
            this.emptySpaceItem14.Size = new System.Drawing.Size(10, 26);
            this.emptySpaceItem14.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem15
            // 
            this.emptySpaceItem15.AllowHotTrack = false;
            this.emptySpaceItem15.Location = new System.Drawing.Point(503, 0);
            this.emptySpaceItem15.Name = "emptySpaceItem15";
            this.emptySpaceItem15.Size = new System.Drawing.Size(239, 26);
            this.emptySpaceItem15.TextSize = new System.Drawing.Size(0, 0);
            // 
            // PageSelectFile
            // 
            this.PageSelectFile.Controls.Add(this.layoutControl1);
            this.PageSelectFile.DescriptionText = "交易数据导入信息设置";
            this.PageSelectFile.Name = "PageSelectFile";
            this.PageSelectFile.Size = new System.Drawing.Size(895, 399);
            this.PageSelectFile.Text = "提示：请再次确认交易数据来源信息是否正确！";
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.chkEntrust);
            this.layoutControl1.Controls.Add(this.txtImportUser);
            this.layoutControl1.Controls.Add(this.txtAccountPlan);
            this.layoutControl1.Controls.Add(this.checkDelivery);
            this.layoutControl1.Controls.Add(this.checkDaily);
            this.layoutControl1.Controls.Add(this.lblDataType);
            this.layoutControl1.Controls.Add(this.txtAccountType);
            this.layoutControl1.Controls.Add(this.luOperator);
            this.layoutControl1.Controls.Add(this.btnBrowse);
            this.layoutControl1.Controls.Add(this.txtImportFileName);
            this.layoutControl1.Controls.Add(this.txtAccountAttribute);
            this.layoutControl1.Controls.Add(this.txtSecurityCompany);
            this.layoutControl1.Controls.Add(this.txtAccountName);
            this.layoutControl1.Location = new System.Drawing.Point(59, 13);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(784, 360);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // chkEntrust
            // 
            this.chkEntrust.EditValue = true;
            this.chkEntrust.Location = new System.Drawing.Point(135, 291);
            this.chkEntrust.Name = "chkEntrust";
            this.chkEntrust.Properties.Caption = "当日委托";
            this.chkEntrust.Size = new System.Drawing.Size(91, 19);
            this.chkEntrust.StyleController = this.layoutControl1;
            this.chkEntrust.TabIndex = 21;
            this.chkEntrust.CheckedChanged += new System.EventHandler(this.chkEntrust_CheckedChanged);
            // 
            // txtImportUser
            // 
            this.txtImportUser.Location = new System.Drawing.Point(124, 243);
            this.txtImportUser.Name = "txtImportUser";
            this.txtImportUser.Properties.ReadOnly = true;
            this.txtImportUser.Size = new System.Drawing.Size(285, 20);
            this.txtImportUser.StyleController = this.layoutControl1;
            this.txtImportUser.TabIndex = 20;
            // 
            // txtAccountPlan
            // 
            this.txtAccountPlan.Location = new System.Drawing.Point(123, 138);
            this.txtAccountPlan.Name = "txtAccountPlan";
            this.txtAccountPlan.Properties.ReadOnly = true;
            this.txtAccountPlan.Size = new System.Drawing.Size(285, 20);
            this.txtAccountPlan.StyleController = this.layoutControl1;
            this.txtAccountPlan.TabIndex = 19;
            // 
            // checkDelivery
            // 
            this.checkDelivery.Location = new System.Drawing.Point(230, 291);
            this.checkDelivery.Name = "checkDelivery";
            this.checkDelivery.Properties.Caption = "交割单";
            this.checkDelivery.Size = new System.Drawing.Size(72, 19);
            this.checkDelivery.StyleController = this.layoutControl1;
            this.checkDelivery.TabIndex = 18;
            this.checkDelivery.CheckedChanged += new System.EventHandler(this.checkDelivery_CheckedChanged);
            // 
            // checkDaily
            // 
            this.checkDaily.Enabled = false;
            this.checkDaily.Location = new System.Drawing.Point(306, 291);
            this.checkDaily.Name = "checkDaily";
            this.checkDaily.Properties.Caption = "当日成交";
            this.checkDaily.Size = new System.Drawing.Size(103, 19);
            this.checkDaily.StyleController = this.layoutControl1;
            this.checkDaily.TabIndex = 17;
            this.checkDaily.CheckedChanged += new System.EventHandler(this.checkDay_CheckedChanged);
            // 
            // lblDataType
            // 
            this.lblDataType.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblDataType.Location = new System.Drawing.Point(37, 291);
            this.lblDataType.Name = "lblDataType";
            this.lblDataType.Size = new System.Drawing.Size(94, 14);
            this.lblDataType.StyleController = this.layoutControl1;
            this.lblDataType.TabIndex = 16;
            this.lblDataType.Text = "数据类型：";
            // 
            // txtAccountType
            // 
            this.txtAccountType.Location = new System.Drawing.Point(123, 114);
            this.txtAccountType.Name = "txtAccountType";
            this.txtAccountType.Properties.ReadOnly = true;
            this.txtAccountType.Size = new System.Drawing.Size(285, 20);
            this.txtAccountType.StyleController = this.layoutControl1;
            this.txtAccountType.TabIndex = 13;
            // 
            // luOperator
            // 
            this.luOperator.Location = new System.Drawing.Point(124, 267);
            this.luOperator.Name = "luOperator";
            this.luOperator.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.luOperator.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Code", 15, "编码"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", 15, "交易员")});
            this.luOperator.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.luOperator.Size = new System.Drawing.Size(285, 20);
            this.luOperator.StyleController = this.layoutControl1;
            this.luOperator.TabIndex = 11;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(672, 314);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(88, 22);
            this.btnBrowse.StyleController = this.layoutControl1;
            this.btnBrowse.TabIndex = 9;
            this.btnBrowse.Text = "    浏  览    ";
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // txtImportFileName
            // 
            this.txtImportFileName.Location = new System.Drawing.Point(124, 314);
            this.txtImportFileName.Name = "txtImportFileName";
            this.txtImportFileName.Size = new System.Drawing.Size(544, 20);
            this.txtImportFileName.StyleController = this.layoutControl1;
            this.txtImportFileName.TabIndex = 8;
            // 
            // txtAccountAttribute
            // 
            this.txtAccountAttribute.Location = new System.Drawing.Point(123, 90);
            this.txtAccountAttribute.Name = "txtAccountAttribute";
            this.txtAccountAttribute.Properties.ReadOnly = true;
            this.txtAccountAttribute.Size = new System.Drawing.Size(285, 20);
            this.txtAccountAttribute.StyleController = this.layoutControl1;
            this.txtAccountAttribute.TabIndex = 7;
            // 
            // txtSecurityCompany
            // 
            this.txtSecurityCompany.Location = new System.Drawing.Point(123, 66);
            this.txtSecurityCompany.Name = "txtSecurityCompany";
            this.txtSecurityCompany.Properties.ReadOnly = true;
            this.txtSecurityCompany.Size = new System.Drawing.Size(285, 20);
            this.txtSecurityCompany.StyleController = this.layoutControl1;
            this.txtSecurityCompany.TabIndex = 6;
            // 
            // txtAccountName
            // 
            this.txtAccountName.Location = new System.Drawing.Point(123, 42);
            this.txtAccountName.Name = "txtAccountName";
            this.txtAccountName.Properties.ReadOnly = true;
            this.txtAccountName.Size = new System.Drawing.Size(285, 20);
            this.txtAccountName.StyleController = this.layoutControl1;
            this.txtAccountName.TabIndex = 4;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup2,
            this.layoutControlGroup3,
            this.emptySpaceItem3});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(784, 360);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.AppearanceGroup.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.layoutControlGroup2.AppearanceGroup.Options.UseFont = true;
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem3,
            this.emptySpaceItem7,
            this.emptySpaceItem13,
            this.layoutControlItem4,
            this.layoutControlItem16,
            this.layoutControlItem9});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Size = new System.Drawing.Size(764, 162);
            this.layoutControlGroup2.Text = "交易数据来源信息";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.txtAccountName;
            this.layoutControlItem1.Location = new System.Drawing.Point(12, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(376, 24);
            this.layoutControlItem1.Text = "账户名称：";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(84, 14);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.txtSecurityCompany;
            this.layoutControlItem3.Location = new System.Drawing.Point(12, 24);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(376, 24);
            this.layoutControlItem3.Text = "开户券商：";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(84, 14);
            // 
            // emptySpaceItem7
            // 
            this.emptySpaceItem7.AllowHotTrack = false;
            this.emptySpaceItem7.Location = new System.Drawing.Point(0, 0);
            this.emptySpaceItem7.Name = "emptySpaceItem7";
            this.emptySpaceItem7.Size = new System.Drawing.Size(12, 120);
            this.emptySpaceItem7.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem13
            // 
            this.emptySpaceItem13.AllowHotTrack = false;
            this.emptySpaceItem13.Location = new System.Drawing.Point(388, 0);
            this.emptySpaceItem13.Name = "emptySpaceItem13";
            this.emptySpaceItem13.Size = new System.Drawing.Size(352, 120);
            this.emptySpaceItem13.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.txtAccountAttribute;
            this.layoutControlItem4.Location = new System.Drawing.Point(12, 48);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(376, 24);
            this.layoutControlItem4.Text = "账户属性:";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(84, 14);
            // 
            // layoutControlItem16
            // 
            this.layoutControlItem16.Control = this.txtAccountPlan;
            this.layoutControlItem16.Location = new System.Drawing.Point(12, 96);
            this.layoutControlItem16.Name = "layoutControlItem16";
            this.layoutControlItem16.Size = new System.Drawing.Size(376, 24);
            this.layoutControlItem16.Text = "账户规划：";
            this.layoutControlItem16.TextSize = new System.Drawing.Size(84, 14);
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.txtAccountType;
            this.layoutControlItem9.Location = new System.Drawing.Point(12, 72);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(376, 24);
            this.layoutControlItem9.Text = "账户类型：";
            this.layoutControlItem9.TextSize = new System.Drawing.Size(84, 14);
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.AppearanceGroup.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.layoutControlGroup3.AppearanceGroup.Options.UseFont = true;
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem5,
            this.layoutControlItem8,
            this.layoutControlItem12,
            this.layoutControlItem6,
            this.layoutControlItem11,
            this.layoutControlItem7,
            this.emptySpaceItem5,
            this.layoutControlItem17,
            this.layoutControlItem10,
            this.emptySpaceItem8});
            this.layoutControlGroup3.Location = new System.Drawing.Point(0, 201);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.Size = new System.Drawing.Size(764, 139);
            this.layoutControlGroup3.Text = "交易数据导入信息";
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.txtImportFileName;
            this.layoutControlItem5.Location = new System.Drawing.Point(13, 71);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(635, 26);
            this.layoutControlItem5.Text = "Excel 文件路径:";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(84, 14);
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.luOperator;
            this.layoutControlItem8.Location = new System.Drawing.Point(13, 24);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(376, 24);
            this.layoutControlItem8.Text = "交易员：";
            this.layoutControlItem8.TextSize = new System.Drawing.Size(84, 14);
            // 
            // layoutControlItem12
            // 
            this.layoutControlItem12.Control = this.lblDataType;
            this.layoutControlItem12.Location = new System.Drawing.Point(13, 48);
            this.layoutControlItem12.Name = "layoutControlItem12";
            this.layoutControlItem12.Size = new System.Drawing.Size(98, 23);
            this.layoutControlItem12.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem12.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.btnBrowse;
            this.layoutControlItem6.Location = new System.Drawing.Point(648, 71);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(92, 26);
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextVisible = false;
            // 
            // layoutControlItem11
            // 
            this.layoutControlItem11.Control = this.checkDelivery;
            this.layoutControlItem11.Location = new System.Drawing.Point(206, 48);
            this.layoutControlItem11.Name = "layoutControlItem11";
            this.layoutControlItem11.Size = new System.Drawing.Size(76, 23);
            this.layoutControlItem11.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem11.TextVisible = false;
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.txtImportUser;
            this.layoutControlItem7.Location = new System.Drawing.Point(13, 0);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(376, 24);
            this.layoutControlItem7.Text = "数据导入人：";
            this.layoutControlItem7.TextSize = new System.Drawing.Size(84, 14);
            // 
            // emptySpaceItem5
            // 
            this.emptySpaceItem5.AllowHotTrack = false;
            this.emptySpaceItem5.Location = new System.Drawing.Point(389, 0);
            this.emptySpaceItem5.Name = "emptySpaceItem5";
            this.emptySpaceItem5.Size = new System.Drawing.Size(351, 71);
            this.emptySpaceItem5.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem17
            // 
            this.layoutControlItem17.Control = this.chkEntrust;
            this.layoutControlItem17.Location = new System.Drawing.Point(111, 48);
            this.layoutControlItem17.Name = "layoutControlItem17";
            this.layoutControlItem17.Size = new System.Drawing.Size(95, 23);
            this.layoutControlItem17.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem17.TextVisible = false;
            // 
            // layoutControlItem10
            // 
            this.layoutControlItem10.Control = this.checkDaily;
            this.layoutControlItem10.Location = new System.Drawing.Point(282, 48);
            this.layoutControlItem10.Name = "layoutControlItem10";
            this.layoutControlItem10.Size = new System.Drawing.Size(107, 23);
            this.layoutControlItem10.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem10.TextVisible = false;
            // 
            // emptySpaceItem8
            // 
            this.emptySpaceItem8.AllowHotTrack = false;
            this.emptySpaceItem8.Location = new System.Drawing.Point(0, 0);
            this.emptySpaceItem8.Name = "emptySpaceItem8";
            this.emptySpaceItem8.Size = new System.Drawing.Size(13, 97);
            this.emptySpaceItem8.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            this.emptySpaceItem3.Location = new System.Drawing.Point(0, 162);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(764, 39);
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // PageFinish
            // 
            this.PageFinish.AllowBack = false;
            this.PageFinish.AllowCancel = false;
            this.PageFinish.AllowNext = false;
            this.PageFinish.Controls.Add(this.layoutControl4);
            this.PageFinish.FinishText = "";
            this.PageFinish.Name = "PageFinish";
            this.PageFinish.ProceedText = "";
            this.PageFinish.Size = new System.Drawing.Size(710, 411);
            this.PageFinish.Text = "交易数据导入";
            this.PageFinish.PageInit += new System.EventHandler(this.PageFinish_PageInit);
            // 
            // layoutControl4
            // 
            this.layoutControl4.Controls.Add(this.gridControlSkip);
            this.layoutControl4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl4.Location = new System.Drawing.Point(0, 0);
            this.layoutControl4.Name = "layoutControl4";
            this.layoutControl4.Root = this.layoutControlGroup6;
            this.layoutControl4.Size = new System.Drawing.Size(710, 411);
            this.layoutControl4.TabIndex = 1;
            this.layoutControl4.Text = "layoutControl4";
            // 
            // gridControlSkip
            // 
            this.gridControlSkip.Location = new System.Drawing.Point(12, 59);
            this.gridControlSkip.MainView = this.gridViewSkip;
            this.gridControlSkip.Name = "gridControlSkip";
            this.gridControlSkip.Size = new System.Drawing.Size(686, 340);
            this.gridControlSkip.TabIndex = 4;
            this.gridControlSkip.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewSkip});
            // 
            // gridViewSkip
            // 
            this.gridViewSkip.GridControl = this.gridControlSkip;
            this.gridViewSkip.Name = "gridViewSkip";
            this.gridViewSkip.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gridViewSkip_CustomDrawRowIndicator);
            // 
            // layoutControlGroup6
            // 
            this.layoutControlGroup6.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup6.GroupBordersVisible = false;
            this.layoutControlGroup6.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lciSkip,
            this.esiImportResult});
            this.layoutControlGroup6.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup6.Name = "layoutControlGroup6";
            this.layoutControlGroup6.Size = new System.Drawing.Size(710, 411);
            this.layoutControlGroup6.TextVisible = false;
            // 
            // lciSkip
            // 
            this.lciSkip.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lciSkip.AppearanceItemCaption.Options.UseFont = true;
            this.lciSkip.Control = this.gridControlSkip;
            this.lciSkip.Location = new System.Drawing.Point(0, 30);
            this.lciSkip.Name = "lciSkip";
            this.lciSkip.Size = new System.Drawing.Size(690, 361);
            this.lciSkip.Text = "已忽略的交易记录：";
            this.lciSkip.TextLocation = DevExpress.Utils.Locations.Top;
            this.lciSkip.TextSize = new System.Drawing.Size(117, 14);
            // 
            // esiImportResult
            // 
            this.esiImportResult.AllowHotTrack = false;
            this.esiImportResult.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.esiImportResult.AppearanceItemCaption.Options.UseFont = true;
            this.esiImportResult.AppearanceItemCaption.Options.UseTextOptions = true;
            this.esiImportResult.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.esiImportResult.Location = new System.Drawing.Point(0, 0);
            this.esiImportResult.Name = "esiImportResult";
            this.esiImportResult.Size = new System.Drawing.Size(690, 30);
            this.esiImportResult.TextSize = new System.Drawing.Size(117, 0);
            this.esiImportResult.TextVisible = true;
            // 
            // PageAccount
            // 
            this.PageAccount.Controls.Add(this.layoutControl2);
            this.PageAccount.IntroductionText = "";
            this.PageAccount.Name = "PageAccount";
            this.PageAccount.ProceedText = "";
            this.PageAccount.Size = new System.Drawing.Size(710, 411);
            this.PageAccount.Text = "选择交易数据导入目标账户";
            // 
            // layoutControl2
            // 
            this.layoutControl2.Controls.Add(this.luSecurityCompany);
            this.layoutControl2.Controls.Add(this.gridControlAccount);
            this.layoutControl2.Controls.Add(this.cbAccountAttribute);
            this.layoutControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl2.Location = new System.Drawing.Point(0, 0);
            this.layoutControl2.Name = "layoutControl2";
            this.layoutControl2.Root = this.layoutControlGroup4;
            this.layoutControl2.Size = new System.Drawing.Size(710, 411);
            this.layoutControl2.TabIndex = 12;
            this.layoutControl2.Text = "layoutControl2";
            // 
            // luSecurityCompany
            // 
            this.luSecurityCompany.Location = new System.Drawing.Point(75, 12);
            this.luSecurityCompany.Name = "luSecurityCompany";
            this.luSecurityCompany.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.luSecurityCompany.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Name")});
            this.luSecurityCompany.Size = new System.Drawing.Size(271, 20);
            this.luSecurityCompany.StyleController = this.layoutControl2;
            this.luSecurityCompany.TabIndex = 10;
            this.luSecurityCompany.EditValueChanged += new System.EventHandler(this.luSecurityCompany_EditValueChanged);
            // 
            // gridControlAccount
            // 
            this.gridControlAccount.Location = new System.Drawing.Point(12, 94);
            this.gridControlAccount.MainView = this.gridViewAccount;
            this.gridControlAccount.Name = "gridControlAccount";
            this.gridControlAccount.Size = new System.Drawing.Size(686, 305);
            this.gridControlAccount.TabIndex = 0;
            this.gridControlAccount.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewAccount});
            // 
            // gridViewAccount
            // 
            this.gridViewAccount.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colAccountId,
            this.colAccountName,
            this.colTypeCode,
            this.colSecurityCompanyName,
            this.colTypeName,
            this.colAttributeCode,
            this.colAttributeName,
            this.colPlanCode,
            this.colPlanName,
            this.colSecurityCompanyCode,
            this.colOperatorNames,
            this.colStampDutyRate,
            this.colCommissionRate,
            this.colIncidentalsRate,
            this.colNeedAccounting,
            this.colIsDisabled,
            this.colRemarks});
            this.gridViewAccount.GridControl = this.gridControlAccount;
            this.gridViewAccount.Name = "gridViewAccount";
            this.gridViewAccount.OptionsBehavior.Editable = false;
            this.gridViewAccount.OptionsBehavior.ReadOnly = true;
            this.gridViewAccount.OptionsSelection.CheckBoxSelectorColumnWidth = 30;
            this.gridViewAccount.OptionsSelection.MultiSelect = true;
            this.gridViewAccount.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gridViewAccount.OptionsSelection.UseIndicatorForSelection = false;
            this.gridViewAccount.OptionsView.ColumnAutoWidth = false;
            this.gridViewAccount.OptionsView.ShowAutoFilterRow = true;
            this.gridViewAccount.OptionsView.ShowGroupPanel = false;
            this.gridViewAccount.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gridViewAccount_CustomDrawRowIndicator);
            this.gridViewAccount.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridViewAccount_FocusedRowChanged);
            // 
            // colAccountId
            // 
            this.colAccountId.FieldName = "Id";
            this.colAccountId.Name = "colAccountId";
            // 
            // colAccountName
            // 
            this.colAccountName.Caption = "账户名称";
            this.colAccountName.FieldName = "Name";
            this.colAccountName.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.colAccountName.Name = "colAccountName";
            this.colAccountName.Visible = true;
            this.colAccountName.VisibleIndex = 1;
            this.colAccountName.Width = 64;
            // 
            // colTypeCode
            // 
            this.colTypeCode.FieldName = "TypeCode";
            this.colTypeCode.Name = "colTypeCode";
            // 
            // colSecurityCompanyName
            // 
            this.colSecurityCompanyName.Caption = "开户券商";
            this.colSecurityCompanyName.FieldName = "SecurityCompanyName";
            this.colSecurityCompanyName.Name = "colSecurityCompanyName";
            this.colSecurityCompanyName.Visible = true;
            this.colSecurityCompanyName.VisibleIndex = 3;
            this.colSecurityCompanyName.Width = 98;
            // 
            // colTypeName
            // 
            this.colTypeName.Caption = "账户类型";
            this.colTypeName.FieldName = "TypeName";
            this.colTypeName.Name = "colTypeName";
            this.colTypeName.Visible = true;
            this.colTypeName.VisibleIndex = 5;
            this.colTypeName.Width = 61;
            // 
            // colAttributeCode
            // 
            this.colAttributeCode.FieldName = "AttributeCode";
            this.colAttributeCode.Name = "colAttributeCode";
            // 
            // colAttributeName
            // 
            this.colAttributeName.Caption = "账户属性";
            this.colAttributeName.FieldName = "AttributeName";
            this.colAttributeName.Name = "colAttributeName";
            this.colAttributeName.Visible = true;
            this.colAttributeName.VisibleIndex = 4;
            this.colAttributeName.Width = 79;
            // 
            // colPlanCode
            // 
            this.colPlanCode.FieldName = "PlanCode";
            this.colPlanCode.Name = "colPlanCode";
            // 
            // colPlanName
            // 
            this.colPlanName.Caption = "账户规划";
            this.colPlanName.FieldName = "PlanName";
            this.colPlanName.Name = "colPlanName";
            this.colPlanName.Visible = true;
            this.colPlanName.VisibleIndex = 6;
            this.colPlanName.Width = 61;
            // 
            // colSecurityCompanyCode
            // 
            this.colSecurityCompanyCode.FieldName = "SecurityCompanyCode";
            this.colSecurityCompanyCode.Name = "colSecurityCompanyCode";
            // 
            // colOperatorNames
            // 
            this.colOperatorNames.Caption = "操作人员";
            this.colOperatorNames.FieldName = "OperatorNames";
            this.colOperatorNames.Name = "colOperatorNames";
            this.colOperatorNames.Visible = true;
            this.colOperatorNames.VisibleIndex = 2;
            this.colOperatorNames.Width = 137;
            // 
            // colStampDutyRate
            // 
            this.colStampDutyRate.Caption = "印花税（率）";
            this.colStampDutyRate.DisplayFormat.FormatString = "#0.000%";
            this.colStampDutyRate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colStampDutyRate.FieldName = "StampDutyRate";
            this.colStampDutyRate.Name = "colStampDutyRate";
            this.colStampDutyRate.Visible = true;
            this.colStampDutyRate.VisibleIndex = 7;
            this.colStampDutyRate.Width = 83;
            // 
            // colCommissionRate
            // 
            this.colCommissionRate.Caption = "佣金（率）";
            this.colCommissionRate.DisplayFormat.FormatString = "#0.000%";
            this.colCommissionRate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colCommissionRate.FieldName = "CommissionRate";
            this.colCommissionRate.Name = "colCommissionRate";
            this.colCommissionRate.Visible = true;
            this.colCommissionRate.VisibleIndex = 8;
            this.colCommissionRate.Width = 68;
            // 
            // colIncidentalsRate
            // 
            this.colIncidentalsRate.Caption = "其他费（率）";
            this.colIncidentalsRate.DisplayFormat.FormatString = "#0.000%";
            this.colIncidentalsRate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colIncidentalsRate.FieldName = "IncidentalsRate";
            this.colIncidentalsRate.Name = "colIncidentalsRate";
            this.colIncidentalsRate.Visible = true;
            this.colIncidentalsRate.VisibleIndex = 9;
            this.colIncidentalsRate.Width = 81;
            // 
            // colNeedAccounting
            // 
            this.colNeedAccounting.Caption = "是否核算";
            this.colNeedAccounting.FieldName = "NeedAccounting";
            this.colNeedAccounting.Name = "colNeedAccounting";
            this.colNeedAccounting.Width = 57;
            // 
            // colIsDisabled
            // 
            this.colIsDisabled.Caption = "是否禁用";
            this.colIsDisabled.FieldName = "IsDisabled";
            this.colIsDisabled.Name = "colIsDisabled";
            this.colIsDisabled.Width = 56;
            // 
            // colRemarks
            // 
            this.colRemarks.Caption = "备注说明";
            this.colRemarks.FieldName = "Remarks";
            this.colRemarks.Name = "colRemarks";
            // 
            // cbAccountAttribute
            // 
            this.cbAccountAttribute.EditValue = "请选择...";
            this.cbAccountAttribute.Location = new System.Drawing.Point(75, 36);
            this.cbAccountAttribute.Name = "cbAccountAttribute";
            this.cbAccountAttribute.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbAccountAttribute.Properties.DropDownRows = 1;
            this.cbAccountAttribute.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cbAccountAttribute.Size = new System.Drawing.Size(271, 20);
            this.cbAccountAttribute.StyleController = this.layoutControl2;
            this.cbAccountAttribute.TabIndex = 9;
            this.cbAccountAttribute.SelectedIndexChanged += new System.EventHandler(this.cbAccountAttribute_SelectedIndexChanged);
            // 
            // layoutControlGroup4
            // 
            this.layoutControlGroup4.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup4.GroupBordersVisible = false;
            this.layoutControlGroup4.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem13,
            this.layoutControlItem14,
            this.layoutControlItem15,
            this.emptySpaceItem9,
            this.emptySpaceItem11,
            this.emptySpaceItem10});
            this.layoutControlGroup4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup4.Name = "layoutControlGroup4";
            this.layoutControlGroup4.Size = new System.Drawing.Size(710, 411);
            this.layoutControlGroup4.TextVisible = false;
            // 
            // layoutControlItem13
            // 
            this.layoutControlItem13.Control = this.luSecurityCompany;
            this.layoutControlItem13.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem13.Name = "layoutControlItem13";
            this.layoutControlItem13.Size = new System.Drawing.Size(338, 24);
            this.layoutControlItem13.Text = "证券公司：";
            this.layoutControlItem13.TextSize = new System.Drawing.Size(60, 14);
            // 
            // layoutControlItem14
            // 
            this.layoutControlItem14.Control = this.cbAccountAttribute;
            this.layoutControlItem14.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem14.Name = "layoutControlItem14";
            this.layoutControlItem14.Size = new System.Drawing.Size(338, 24);
            this.layoutControlItem14.Text = "账户属性：";
            this.layoutControlItem14.TextSize = new System.Drawing.Size(60, 14);
            // 
            // layoutControlItem15
            // 
            this.layoutControlItem15.Control = this.gridControlAccount;
            this.layoutControlItem15.Location = new System.Drawing.Point(0, 65);
            this.layoutControlItem15.Name = "layoutControlItem15";
            this.layoutControlItem15.Size = new System.Drawing.Size(690, 326);
            this.layoutControlItem15.Text = "账户信息";
            this.layoutControlItem15.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem15.TextSize = new System.Drawing.Size(60, 14);
            // 
            // emptySpaceItem9
            // 
            this.emptySpaceItem9.AllowHotTrack = false;
            this.emptySpaceItem9.Location = new System.Drawing.Point(338, 0);
            this.emptySpaceItem9.Name = "emptySpaceItem9";
            this.emptySpaceItem9.Size = new System.Drawing.Size(352, 24);
            this.emptySpaceItem9.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem11
            // 
            this.emptySpaceItem11.AllowHotTrack = false;
            this.emptySpaceItem11.Location = new System.Drawing.Point(338, 24);
            this.emptySpaceItem11.Name = "emptySpaceItem11";
            this.emptySpaceItem11.Size = new System.Drawing.Size(352, 24);
            this.emptySpaceItem11.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem10
            // 
            this.emptySpaceItem10.AllowHotTrack = false;
            this.emptySpaceItem10.Location = new System.Drawing.Point(0, 48);
            this.emptySpaceItem10.Name = "emptySpaceItem10";
            this.emptySpaceItem10.Size = new System.Drawing.Size(690, 17);
            this.emptySpaceItem10.TextSize = new System.Drawing.Size(0, 0);
            // 
            // wizardControl1
            // 
            this.wizardControl1.Controls.Add(this.PageAccount);
            this.wizardControl1.Controls.Add(this.PageFinish);
            this.wizardControl1.Controls.Add(this.PageSelectFile);
            this.wizardControl1.Controls.Add(this.PagePreview);
            this.wizardControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wizardControl1.Location = new System.Drawing.Point(0, 0);
            this.wizardControl1.Name = "wizardControl1";
            this.wizardControl1.Pages.AddRange(new DevExpress.XtraWizard.BaseWizardPage[] {
            this.PageAccount,
            this.PageSelectFile,
            this.PagePreview,
            this.PageFinish});
            this.wizardControl1.Size = new System.Drawing.Size(927, 544);
            this.wizardControl1.CancelClick += new System.ComponentModel.CancelEventHandler(this.wizardControl1_CancelClick);
            this.wizardControl1.FinishClick += new System.ComponentModel.CancelEventHandler(this.wizardControl1_FinishClick);
            this.wizardControl1.NextClick += new DevExpress.XtraWizard.WizardCommandButtonClickEventHandler(this.wizardControl1_NextClick);
            this.wizardControl1.PrevClick += new DevExpress.XtraWizard.WizardCommandButtonClickEventHandler(this.wizardControl1_PrevClick);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "编号";
            this.gridColumn1.FieldName = "Id";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 71;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "账号名称";
            this.gridColumn2.FieldName = "AccountName";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 88;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "账号名称";
            this.gridColumn3.FieldName = "AccountName";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 1;
            this.gridColumn3.Width = 88;
            // 
            // FrmTradeDataImportWizard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(927, 544);
            this.Controls.Add(this.wizardControl1);
            this.Name = "FrmTradeDataImportWizard";
            this.Text = "FrmTradeDataImportWizard";
            this.Load += new System.EventHandler(this.FrmTradeDataImportWizard_Load);
            this.PagePreview.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl3)).EndInit();
            this.layoutControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.luTargetPrincipal.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.luBandPrincipal.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlPreview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewPreview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem18)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem20)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem21)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem19)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem14)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem15)).EndInit();
            this.PageSelectFile.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkEntrust.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtImportUser.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAccountPlan.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkDelivery.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkDaily.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAccountType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.luOperator.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtImportFileName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAccountAttribute.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSecurityCompany.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAccountName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem16)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem17)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            this.PageFinish.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl4)).EndInit();
            this.layoutControl4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlSkip)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewSkip)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciSkip)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.esiImportResult)).EndInit();
            this.PageAccount.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).EndInit();
            this.layoutControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.luSecurityCompany.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlAccount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewAccount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbAccountAttribute.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem14)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem15)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.wizardControl1)).EndInit();
            this.wizardControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private DevExpress.XtraWizard.WizardPage PagePreview;
        private DevExpress.XtraGrid.GridControl gridControlPreview;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewPreview;
        private DevExpress.XtraWizard.WizardPage PageSelectFile;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.LookUpEdit luOperator;
        private DevExpress.XtraEditors.SimpleButton btnBrowse;
        private DevExpress.XtraEditors.TextEdit txtImportFileName;
        private DevExpress.XtraEditors.TextEdit txtAccountAttribute;
        private DevExpress.XtraEditors.TextEdit txtSecurityCompany;
        private DevExpress.XtraEditors.TextEdit txtAccountName;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraWizard.CompletionWizardPage PageFinish;
        private DevExpress.XtraWizard.WelcomeWizardPage PageAccount;
        private DevExpress.XtraWizard.WizardControl wizardControl1;
        private DevExpress.XtraEditors.TextEdit txtAccountType;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private DevExpress.XtraEditors.LabelControl lblDataType;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem12;
        private DevExpress.XtraEditors.CheckEdit checkDelivery;
        private DevExpress.XtraEditors.CheckEdit checkDaily;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem10;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem11;
        private DevExpress.XtraLayout.LayoutControl layoutControl2;
        private DevExpress.XtraEditors.LookUpEdit luSecurityCompany;
        private DevExpress.XtraGrid.GridControl gridControlAccount;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewAccount;
        private DevExpress.XtraGrid.Columns.GridColumn colAccountId;
        private DevExpress.XtraGrid.Columns.GridColumn colAccountName;
        private DevExpress.XtraGrid.Columns.GridColumn colTypeName;
        private DevExpress.XtraGrid.Columns.GridColumn colAttributeName;
        private DevExpress.XtraGrid.Columns.GridColumn colSecurityCompanyName;
        private DevExpress.XtraGrid.Columns.GridColumn colRemarks;
        private DevExpress.XtraGrid.Columns.GridColumn colTypeCode;
        private DevExpress.XtraGrid.Columns.GridColumn colAttributeCode;
        private DevExpress.XtraEditors.ComboBoxEdit cbAccountAttribute;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem13;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem14;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem15;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem9;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem11;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem10;
        private DevExpress.XtraGrid.Columns.GridColumn colPlanName;
        private DevExpress.XtraGrid.Columns.GridColumn colPlanCode;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraEditors.TextEdit txtAccountPlan;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem16;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraEditors.TextEdit txtImportUser;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraGrid.Columns.GridColumn colSecurityCompanyCode;
        private DevExpress.XtraGrid.Columns.GridColumn colOperatorNames;
        private DevExpress.XtraGrid.Columns.GridColumn colStampDutyRate;
        private DevExpress.XtraGrid.Columns.GridColumn colCommissionRate;
        private DevExpress.XtraGrid.Columns.GridColumn colIncidentalsRate;
        private DevExpress.XtraGrid.Columns.GridColumn colNeedAccounting;
        private DevExpress.XtraGrid.Columns.GridColumn colIsDisabled;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem13;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem5;
        private DevExpress.XtraEditors.CheckEdit chkEntrust;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem17;
        private DevExpress.XtraLayout.LayoutControl layoutControl3;
        private DevExpress.XtraEditors.SimpleButton btnExcelTemplate;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem18;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem19;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem4;
        private DevExpress.XtraEditors.LookUpEdit luTargetPrincipal;
        private DevExpress.XtraEditors.LookUpEdit luBandPrincipal;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem20;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem21;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem14;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem15;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem7;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem8;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private DevExpress.XtraLayout.LayoutControl layoutControl4;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup6;
        private DevExpress.XtraGrid.GridControl gridControlSkip;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewSkip;
        private DevExpress.XtraLayout.LayoutControlItem lciSkip;
        private DevExpress.XtraLayout.EmptySpaceItem esiImportResult;
    }
}