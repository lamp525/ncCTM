namespace CTM.Win.Forms.Accounting.DataManage
{
    partial class FrmDeliveryImport
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
            this.wizardControl1 = new DevExpress.XtraWizard.WizardControl();
            this.PageAccount = new DevExpress.XtraWizard.WelcomeWizardPage();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.gridControlAccount = new DevExpress.XtraGrid.GridControl();
            this.gridViewAccount = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colAccountId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAccountName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOwnerName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIndustryId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIndustryName = new DevExpress.XtraGrid.Columns.GridColumn();
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
            this.colRemarks = new DevExpress.XtraGrid.Columns.GridColumn();
            this.luSecurityCompany = new DevExpress.XtraEditors.LookUpEdit();
            this.cbAccountAttribute = new DevExpress.XtraEditors.ComboBoxEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlGroup4 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem13 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem14 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlGroup5 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.PageImport = new DevExpress.XtraWizard.WizardPage();
            this.layoutControl2 = new DevExpress.XtraLayout.LayoutControl();
            this.txtFilePath = new DevExpress.XtraEditors.TextEdit();
            this.btnFileSelect = new DevExpress.XtraEditors.SimpleButton();
            this.btnExcelTemplate = new DevExpress.XtraEditors.SimpleButton();
            this.gridControlPreview = new DevExpress.XtraGrid.GridControl();
            this.gridViewPreview = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.txtAccountInfo = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem4 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem6 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem5 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.PageFinish = new DevExpress.XtraWizard.CompletionWizardPage();
            this.layoutControl3 = new DevExpress.XtraLayout.LayoutControl();
            this.btnExportSkip = new DevExpress.XtraEditors.SimpleButton();
            this.gridControlSkip = new DevExpress.XtraGrid.GridControl();
            this.gridViewSkip = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.marqueeProgressBarControl1 = new DevExpress.XtraEditors.MarqueeProgressBarControl();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lciProgress = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem7 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem8 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lcgSkip = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lciSkip = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem9 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem10 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.esiImportResult = new DevExpress.XtraLayout.EmptySpaceItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.wizardControl1)).BeginInit();
            this.wizardControl1.SuspendLayout();
            this.PageAccount.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlAccount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewAccount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.luSecurityCompany.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbAccountAttribute.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem14)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.PageImport.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).BeginInit();
            this.layoutControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtFilePath.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlPreview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewPreview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAccountInfo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem5)).BeginInit();
            this.PageFinish.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl3)).BeginInit();
            this.layoutControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlSkip)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewSkip)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.marqueeProgressBarControl1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciProgress)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgSkip)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciSkip)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.esiImportResult)).BeginInit();
            this.SuspendLayout();
            // 
            // wizardControl1
            // 
            this.wizardControl1.Controls.Add(this.PageAccount);
            this.wizardControl1.Controls.Add(this.PageImport);
            this.wizardControl1.Controls.Add(this.PageFinish);
            this.wizardControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wizardControl1.Location = new System.Drawing.Point(0, 0);
            this.wizardControl1.Name = "wizardControl1";
            this.wizardControl1.Pages.AddRange(new DevExpress.XtraWizard.BaseWizardPage[] {
            this.PageAccount,
            this.PageImport,
            this.PageFinish});
            this.wizardControl1.Size = new System.Drawing.Size(1350, 729);
            this.wizardControl1.CancelClick += new System.ComponentModel.CancelEventHandler(this.wizardControl1_CancelClick);
            this.wizardControl1.FinishClick += new System.ComponentModel.CancelEventHandler(this.wizardControl1_FinishClick);
            this.wizardControl1.NextClick += new DevExpress.XtraWizard.WizardCommandButtonClickEventHandler(this.wizardControl1_NextClick);
            // 
            // PageAccount
            // 
            this.PageAccount.Controls.Add(this.layoutControl1);
            this.PageAccount.IntroductionText = "";
            this.PageAccount.Name = "PageAccount";
            this.PageAccount.ProceedText = "";
            this.PageAccount.Size = new System.Drawing.Size(1133, 596);
            this.PageAccount.Text = "选择交割单数据来源";
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.gridControlAccount);
            this.layoutControl1.Controls.Add(this.luSecurityCompany);
            this.layoutControl1.Controls.Add(this.cbAccountAttribute);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(1133, 596);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // gridControlAccount
            // 
            this.gridControlAccount.Location = new System.Drawing.Point(24, 145);
            this.gridControlAccount.MainView = this.gridViewAccount;
            this.gridControlAccount.Name = "gridControlAccount";
            this.gridControlAccount.Size = new System.Drawing.Size(1085, 427);
            this.gridControlAccount.TabIndex = 11;
            this.gridControlAccount.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewAccount});
            // 
            // gridViewAccount
            // 
            this.gridViewAccount.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colAccountId,
            this.colAccountName,
            this.colOwnerName,
            this.colIndustryId,
            this.colIndustryName,
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
            this.colRemarks});
            this.gridViewAccount.GridControl = this.gridControlAccount;
            this.gridViewAccount.Name = "gridViewAccount";
            this.gridViewAccount.OptionsBehavior.Editable = false;
            this.gridViewAccount.OptionsBehavior.ReadOnly = true;
            this.gridViewAccount.OptionsSelection.CheckBoxSelectorColumnWidth = 30;
            this.gridViewAccount.OptionsSelection.MultiSelect = true;
            this.gridViewAccount.OptionsSelection.UseIndicatorForSelection = false;
            this.gridViewAccount.OptionsView.ColumnAutoWidth = false;
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
            this.colAccountName.VisibleIndex = 0;
            this.colAccountName.Width = 120;
            // 
            // colOwnerName
            // 
            this.colOwnerName.Caption = "负责人";
            this.colOwnerName.FieldName = "OwnerName";
            this.colOwnerName.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.colOwnerName.Name = "colOwnerName";
            this.colOwnerName.Visible = true;
            this.colOwnerName.VisibleIndex = 1;
            // 
            // colIndustryId
            // 
            this.colIndustryId.FieldName = "IndustryId";
            this.colIndustryId.Name = "colIndustryId";
            // 
            // colIndustryName
            // 
            this.colIndustryName.Caption = "所属产业";
            this.colIndustryName.FieldName = "IndustryName";
            this.colIndustryName.Name = "colIndustryName";
            this.colIndustryName.Width = 92;
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
            this.colSecurityCompanyName.Width = 100;
            // 
            // colTypeName
            // 
            this.colTypeName.Caption = "账户类型";
            this.colTypeName.FieldName = "TypeName";
            this.colTypeName.Name = "colTypeName";
            this.colTypeName.Visible = true;
            this.colTypeName.VisibleIndex = 5;
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
            this.colOperatorNames.Width = 200;
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
            this.colStampDutyRate.Width = 100;
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
            this.colCommissionRate.Width = 100;
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
            this.colIncidentalsRate.Width = 100;
            // 
            // colRemarks
            // 
            this.colRemarks.Caption = "备注说明";
            this.colRemarks.FieldName = "Remarks";
            this.colRemarks.Name = "colRemarks";
            this.colRemarks.Width = 160;
            // 
            // luSecurityCompany
            // 
            this.luSecurityCompany.Location = new System.Drawing.Point(87, 43);
            this.luSecurityCompany.Name = "luSecurityCompany";
            this.luSecurityCompany.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.luSecurityCompany.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Name")});
            this.luSecurityCompany.Size = new System.Drawing.Size(433, 20);
            this.luSecurityCompany.StyleController = this.layoutControl1;
            this.luSecurityCompany.TabIndex = 10;
            this.luSecurityCompany.EditValueChanged += new System.EventHandler(this.luSecurityCompany_EditValueChanged);
            // 
            // cbAccountAttribute
            // 
            this.cbAccountAttribute.EditValue = "请选择...";
            this.cbAccountAttribute.Location = new System.Drawing.Point(87, 67);
            this.cbAccountAttribute.Name = "cbAccountAttribute";
            this.cbAccountAttribute.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbAccountAttribute.Properties.DropDownRows = 1;
            this.cbAccountAttribute.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cbAccountAttribute.Size = new System.Drawing.Size(433, 20);
            this.cbAccountAttribute.StyleController = this.layoutControl1;
            this.cbAccountAttribute.TabIndex = 9;
            this.cbAccountAttribute.EditValueChanged += new System.EventHandler(this.cbAccountAttribute_EditValueChanged);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem1,
            this.layoutControlGroup4,
            this.layoutControlGroup5});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(1133, 596);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 91);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(1113, 11);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlGroup4
            // 
            this.layoutControlGroup4.AppearanceGroup.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.layoutControlGroup4.AppearanceGroup.Options.UseFont = true;
            this.layoutControlGroup4.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem13,
            this.layoutControlItem14,
            this.emptySpaceItem2});
            this.layoutControlGroup4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup4.Name = "layoutControlGroup4";
            this.layoutControlGroup4.Size = new System.Drawing.Size(1113, 91);
            this.layoutControlGroup4.Text = "券商信息";
            // 
            // layoutControlItem13
            // 
            this.layoutControlItem13.Control = this.luSecurityCompany;
            this.layoutControlItem13.CustomizationFormText = "证券公司：";
            this.layoutControlItem13.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem13.Name = "layoutControlItem13";
            this.layoutControlItem13.Size = new System.Drawing.Size(500, 24);
            this.layoutControlItem13.Text = "证券公司：";
            this.layoutControlItem13.TextSize = new System.Drawing.Size(60, 14);
            // 
            // layoutControlItem14
            // 
            this.layoutControlItem14.Control = this.cbAccountAttribute;
            this.layoutControlItem14.CustomizationFormText = "账户属性：";
            this.layoutControlItem14.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem14.Name = "layoutControlItem14";
            this.layoutControlItem14.Size = new System.Drawing.Size(500, 24);
            this.layoutControlItem14.Text = "账户属性：";
            this.layoutControlItem14.TextSize = new System.Drawing.Size(60, 14);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(500, 0);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(589, 48);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlGroup5
            // 
            this.layoutControlGroup5.AppearanceGroup.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.layoutControlGroup5.AppearanceGroup.Options.UseFont = true;
            this.layoutControlGroup5.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.layoutControlGroup5.Location = new System.Drawing.Point(0, 102);
            this.layoutControlGroup5.Name = "layoutControlGroup5";
            this.layoutControlGroup5.Size = new System.Drawing.Size(1113, 474);
            this.layoutControlGroup5.Text = "账户信息";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.layoutControlItem1.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem1.Control = this.gridControlAccount;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1089, 431);
            this.layoutControlItem1.Text = "账户信息";
            this.layoutControlItem1.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // PageImport
            // 
            this.PageImport.Controls.Add(this.layoutControl2);
            this.PageImport.DescriptionText = "注意：请再次核对选择的目标账号是否正确。";
            this.PageImport.Name = "PageImport";
            this.PageImport.Size = new System.Drawing.Size(1318, 584);
            this.PageImport.Text = "数据导入预览";
            // 
            // layoutControl2
            // 
            this.layoutControl2.Controls.Add(this.txtFilePath);
            this.layoutControl2.Controls.Add(this.btnFileSelect);
            this.layoutControl2.Controls.Add(this.btnExcelTemplate);
            this.layoutControl2.Controls.Add(this.gridControlPreview);
            this.layoutControl2.Controls.Add(this.txtAccountInfo);
            this.layoutControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl2.Location = new System.Drawing.Point(0, 0);
            this.layoutControl2.Name = "layoutControl2";
            this.layoutControl2.Root = this.layoutControlGroup2;
            this.layoutControl2.Size = new System.Drawing.Size(1318, 584);
            this.layoutControl2.TabIndex = 0;
            this.layoutControl2.Text = "layoutControl2";
            // 
            // txtFilePath
            // 
            this.txtFilePath.Location = new System.Drawing.Point(103, 48);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.Properties.ReadOnly = true;
            this.txtFilePath.Size = new System.Drawing.Size(414, 20);
            this.txtFilePath.StyleController = this.layoutControl2;
            this.txtFilePath.TabIndex = 8;
            // 
            // btnFileSelect
            // 
            this.btnFileSelect.Location = new System.Drawing.Point(521, 48);
            this.btnFileSelect.Name = "btnFileSelect";
            this.btnFileSelect.Size = new System.Drawing.Size(123, 22);
            this.btnFileSelect.StyleController = this.layoutControl2;
            this.btnFileSelect.TabIndex = 7;
            this.btnFileSelect.Text = "    浏  览    ";
            this.btnFileSelect.Click += new System.EventHandler(this.btnFileSelect_Click);
            // 
            // btnExcelTemplate
            // 
            this.btnExcelTemplate.Location = new System.Drawing.Point(521, 12);
            this.btnExcelTemplate.Name = "btnExcelTemplate";
            this.btnExcelTemplate.Size = new System.Drawing.Size(123, 22);
            this.btnExcelTemplate.StyleController = this.layoutControl2;
            this.btnExcelTemplate.TabIndex = 6;
            this.btnExcelTemplate.Text = " 查看导入Excel模板 ";
            this.btnExcelTemplate.Click += new System.EventHandler(this.btnExcelTemplate_Click);
            // 
            // gridControlPreview
            // 
            this.gridControlPreview.Location = new System.Drawing.Point(12, 101);
            this.gridControlPreview.MainView = this.gridViewPreview;
            this.gridControlPreview.Name = "gridControlPreview";
            this.gridControlPreview.Size = new System.Drawing.Size(1294, 471);
            this.gridControlPreview.TabIndex = 5;
            this.gridControlPreview.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewPreview});
            // 
            // gridViewPreview
            // 
            this.gridViewPreview.GridControl = this.gridControlPreview;
            this.gridViewPreview.Name = "gridViewPreview";
            this.gridViewPreview.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gridViewPreview_CustomDrawRowIndicator);
            // 
            // txtAccountInfo
            // 
            this.txtAccountInfo.Location = new System.Drawing.Point(103, 12);
            this.txtAccountInfo.Name = "txtAccountInfo";
            this.txtAccountInfo.Properties.ReadOnly = true;
            this.txtAccountInfo.Size = new System.Drawing.Size(414, 20);
            this.txtAccountInfo.StyleController = this.layoutControl2;
            this.txtAccountInfo.TabIndex = 4;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup2.GroupBordersVisible = false;
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.emptySpaceItem4,
            this.layoutControlItem5,
            this.layoutControlItem6,
            this.emptySpaceItem6,
            this.layoutControlItem4,
            this.emptySpaceItem3,
            this.emptySpaceItem5});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Size = new System.Drawing.Size(1318, 584);
            this.layoutControlGroup2.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.txtAccountInfo;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(509, 26);
            this.layoutControlItem2.Text = "账户信息：";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(88, 14);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.layoutControlItem3.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem3.Control = this.gridControlPreview;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 72);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(1298, 492);
            this.layoutControlItem3.Text = "导入数据预览";
            this.layoutControlItem3.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(88, 14);
            // 
            // emptySpaceItem4
            // 
            this.emptySpaceItem4.AllowHotTrack = false;
            this.emptySpaceItem4.Location = new System.Drawing.Point(636, 36);
            this.emptySpaceItem4.Name = "emptySpaceItem4";
            this.emptySpaceItem4.Size = new System.Drawing.Size(662, 26);
            this.emptySpaceItem4.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.btnFileSelect;
            this.layoutControlItem5.Location = new System.Drawing.Point(509, 36);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(127, 26);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.txtFilePath;
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 36);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(509, 26);
            this.layoutControlItem6.Text = "Excel文件路径：";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(88, 14);
            // 
            // emptySpaceItem6
            // 
            this.emptySpaceItem6.AllowHotTrack = false;
            this.emptySpaceItem6.Location = new System.Drawing.Point(0, 62);
            this.emptySpaceItem6.Name = "emptySpaceItem6";
            this.emptySpaceItem6.Size = new System.Drawing.Size(1298, 10);
            this.emptySpaceItem6.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.btnExcelTemplate;
            this.layoutControlItem4.Location = new System.Drawing.Point(509, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(127, 26);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            this.emptySpaceItem3.Location = new System.Drawing.Point(636, 0);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(662, 26);
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem5
            // 
            this.emptySpaceItem5.AllowHotTrack = false;
            this.emptySpaceItem5.Location = new System.Drawing.Point(0, 26);
            this.emptySpaceItem5.Name = "emptySpaceItem5";
            this.emptySpaceItem5.Size = new System.Drawing.Size(1298, 10);
            this.emptySpaceItem5.TextSize = new System.Drawing.Size(0, 0);
            // 
            // PageFinish
            // 
            this.PageFinish.AllowCancel = false;
            this.PageFinish.AllowNext = false;
            this.PageFinish.Controls.Add(this.layoutControl3);
            this.PageFinish.FinishText = "";
            this.PageFinish.Name = "PageFinish";
            this.PageFinish.ProceedText = "";
            this.PageFinish.Size = new System.Drawing.Size(1133, 596);
            this.PageFinish.Text = "数据导入处理";
            this.PageFinish.PageInit += new System.EventHandler(this.PageFinish_PageInit);
            // 
            // layoutControl3
            // 
            this.layoutControl3.Controls.Add(this.btnExportSkip);
            this.layoutControl3.Controls.Add(this.gridControlSkip);
            this.layoutControl3.Controls.Add(this.marqueeProgressBarControl1);
            this.layoutControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl3.Location = new System.Drawing.Point(0, 0);
            this.layoutControl3.Name = "layoutControl3";
            this.layoutControl3.Root = this.layoutControlGroup3;
            this.layoutControl3.Size = new System.Drawing.Size(1133, 596);
            this.layoutControl3.TabIndex = 1;
            this.layoutControl3.Text = "layoutControl3";
            // 
            // btnExportSkip
            // 
            this.btnExportSkip.Location = new System.Drawing.Point(34, 66);
            this.btnExportSkip.Name = "btnExportSkip";
            this.btnExportSkip.Size = new System.Drawing.Size(85, 22);
            this.btnExportSkip.StyleController = this.layoutControl3;
            this.btnExportSkip.TabIndex = 5;
            this.btnExportSkip.Text = " 导出到Excel ";
            this.btnExportSkip.Click += new System.EventHandler(this.btnExportSkip_Click);
            // 
            // gridControlSkip
            // 
            this.gridControlSkip.Location = new System.Drawing.Point(24, 92);
            this.gridControlSkip.MainView = this.gridViewSkip;
            this.gridControlSkip.Name = "gridControlSkip";
            this.gridControlSkip.Size = new System.Drawing.Size(1085, 480);
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
            // marqueeProgressBarControl1
            // 
            this.marqueeProgressBarControl1.EditValue = "";
            this.marqueeProgressBarControl1.Location = new System.Drawing.Point(22, 12);
            this.marqueeProgressBarControl1.Name = "marqueeProgressBarControl1";
            this.marqueeProgressBarControl1.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.marqueeProgressBarControl1.Properties.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.marqueeProgressBarControl1.Properties.MarqueeAnimationSpeed = 40;
            this.marqueeProgressBarControl1.Properties.MarqueeWidth = 80;
            this.marqueeProgressBarControl1.Properties.ProgressAnimationMode = DevExpress.Utils.Drawing.ProgressAnimationMode.Cycle;
            this.marqueeProgressBarControl1.Size = new System.Drawing.Size(671, 17);
            this.marqueeProgressBarControl1.StyleController = this.layoutControl3;
            this.marqueeProgressBarControl1.TabIndex = 0;
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup3.GroupBordersVisible = false;
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lciProgress,
            this.emptySpaceItem7,
            this.emptySpaceItem8,
            this.lcgSkip,
            this.esiImportResult});
            this.layoutControlGroup3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.Size = new System.Drawing.Size(1133, 596);
            this.layoutControlGroup3.TextVisible = false;
            // 
            // lciProgress
            // 
            this.lciProgress.Control = this.marqueeProgressBarControl1;
            this.lciProgress.Location = new System.Drawing.Point(10, 0);
            this.lciProgress.Name = "lciProgress";
            this.lciProgress.Size = new System.Drawing.Size(675, 21);
            this.lciProgress.TextSize = new System.Drawing.Size(0, 0);
            this.lciProgress.TextVisible = false;
            // 
            // emptySpaceItem7
            // 
            this.emptySpaceItem7.AllowHotTrack = false;
            this.emptySpaceItem7.Location = new System.Drawing.Point(0, 0);
            this.emptySpaceItem7.Name = "emptySpaceItem7";
            this.emptySpaceItem7.Size = new System.Drawing.Size(10, 21);
            this.emptySpaceItem7.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem8
            // 
            this.emptySpaceItem8.AllowHotTrack = false;
            this.emptySpaceItem8.Location = new System.Drawing.Point(1103, 0);
            this.emptySpaceItem8.Name = "emptySpaceItem8";
            this.emptySpaceItem8.Size = new System.Drawing.Size(10, 21);
            this.emptySpaceItem8.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lcgSkip
            // 
            this.lcgSkip.AppearanceGroup.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.lcgSkip.AppearanceGroup.Options.UseFont = true;
            this.lcgSkip.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lciSkip,
            this.layoutControlItem7,
            this.emptySpaceItem9,
            this.emptySpaceItem10});
            this.lcgSkip.Location = new System.Drawing.Point(0, 21);
            this.lcgSkip.Name = "lcgSkip";
            this.lcgSkip.Size = new System.Drawing.Size(1113, 555);
            this.lcgSkip.Text = "已忽略的数据";
            // 
            // lciSkip
            // 
            this.lciSkip.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lciSkip.AppearanceItemCaption.Options.UseFont = true;
            this.lciSkip.Control = this.gridControlSkip;
            this.lciSkip.Location = new System.Drawing.Point(0, 26);
            this.lciSkip.Name = "lciSkip";
            this.lciSkip.Size = new System.Drawing.Size(1089, 484);
            this.lciSkip.Text = "已忽略的交易记录";
            this.lciSkip.TextLocation = DevExpress.Utils.Locations.Top;
            this.lciSkip.TextSize = new System.Drawing.Size(0, 0);
            this.lciSkip.TextVisible = false;
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.btnExportSkip;
            this.layoutControlItem7.Location = new System.Drawing.Point(10, 0);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(89, 26);
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextVisible = false;
            // 
            // emptySpaceItem9
            // 
            this.emptySpaceItem9.AllowHotTrack = false;
            this.emptySpaceItem9.Location = new System.Drawing.Point(0, 0);
            this.emptySpaceItem9.Name = "emptySpaceItem9";
            this.emptySpaceItem9.Size = new System.Drawing.Size(10, 26);
            this.emptySpaceItem9.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem10
            // 
            this.emptySpaceItem10.AllowHotTrack = false;
            this.emptySpaceItem10.Location = new System.Drawing.Point(99, 0);
            this.emptySpaceItem10.Name = "emptySpaceItem10";
            this.emptySpaceItem10.Size = new System.Drawing.Size(990, 26);
            this.emptySpaceItem10.TextSize = new System.Drawing.Size(0, 0);
            // 
            // esiImportResult
            // 
            this.esiImportResult.AllowHotTrack = false;
            this.esiImportResult.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.esiImportResult.AppearanceItemCaption.Options.UseFont = true;
            this.esiImportResult.AppearanceItemCaption.Options.UseTextOptions = true;
            this.esiImportResult.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.esiImportResult.Location = new System.Drawing.Point(685, 0);
            this.esiImportResult.Name = "esiImportResult";
            this.esiImportResult.Size = new System.Drawing.Size(418, 21);
            this.esiImportResult.TextSize = new System.Drawing.Size(0, 0);
            this.esiImportResult.TextVisible = true;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // FrmDeliveryImport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1350, 729);
            this.Controls.Add(this.wizardControl1);
            this.Name = "FrmDeliveryImport";
            this.Text = "FrmDeliveryImport";
            this.Load += new System.EventHandler(this.FrmDeliveryImport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.wizardControl1)).EndInit();
            this.wizardControl1.ResumeLayout(false);
            this.PageAccount.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlAccount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewAccount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.luSecurityCompany.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbAccountAttribute.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem14)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.PageImport.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).EndInit();
            this.layoutControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtFilePath.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlPreview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewPreview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAccountInfo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem5)).EndInit();
            this.PageFinish.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl3)).EndInit();
            this.layoutControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlSkip)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewSkip)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.marqueeProgressBarControl1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciProgress)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgSkip)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciSkip)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.esiImportResult)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraWizard.WizardControl wizardControl1;
        private DevExpress.XtraWizard.WelcomeWizardPage PageAccount;
        private DevExpress.XtraWizard.WizardPage PageImport;
        private DevExpress.XtraWizard.CompletionWizardPage PageFinish;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.LookUpEdit luSecurityCompany;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem13;
        private DevExpress.XtraEditors.ComboBoxEdit cbAccountAttribute;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem14;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraGrid.GridControl gridControlAccount;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewAccount;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraGrid.Columns.GridColumn colAccountId;
        private DevExpress.XtraGrid.Columns.GridColumn colAccountName;
        private DevExpress.XtraGrid.Columns.GridColumn colIndustryId;
        private DevExpress.XtraGrid.Columns.GridColumn colIndustryName;
        private DevExpress.XtraGrid.Columns.GridColumn colTypeCode;
        private DevExpress.XtraGrid.Columns.GridColumn colSecurityCompanyName;
        private DevExpress.XtraGrid.Columns.GridColumn colTypeName;
        private DevExpress.XtraGrid.Columns.GridColumn colAttributeCode;
        private DevExpress.XtraGrid.Columns.GridColumn colAttributeName;
        private DevExpress.XtraGrid.Columns.GridColumn colPlanCode;
        private DevExpress.XtraGrid.Columns.GridColumn colPlanName;
        private DevExpress.XtraGrid.Columns.GridColumn colSecurityCompanyCode;
        private DevExpress.XtraGrid.Columns.GridColumn colOperatorNames;
        private DevExpress.XtraGrid.Columns.GridColumn colStampDutyRate;
        private DevExpress.XtraGrid.Columns.GridColumn colCommissionRate;
        private DevExpress.XtraGrid.Columns.GridColumn colIncidentalsRate;
        private DevExpress.XtraGrid.Columns.GridColumn colRemarks;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.LayoutControl layoutControl2;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraGrid.GridControl gridControlPreview;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewPreview;
        private DevExpress.XtraEditors.TextEdit txtAccountInfo;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem4;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private DevExpress.XtraEditors.SimpleButton btnExcelTemplate;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraEditors.SimpleButton btnFileSelect;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraEditors.TextEdit txtFilePath;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem6;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem5;
        private DevExpress.XtraEditors.MarqueeProgressBarControl marqueeProgressBarControl1;
        private DevExpress.XtraLayout.LayoutControl layoutControl3;
        private DevExpress.XtraGrid.GridControl gridControlSkip;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewSkip;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraLayout.LayoutControlItem lciProgress;
        private DevExpress.XtraLayout.LayoutControlItem lciSkip;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem7;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem8;
        private DevExpress.XtraLayout.EmptySpaceItem esiImportResult;
        private DevExpress.XtraEditors.SimpleButton btnExportSkip;
        private DevExpress.XtraLayout.LayoutControlGroup lcgSkip;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem9;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem10;
        private DevExpress.XtraGrid.Columns.GridColumn colOwnerName;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup4;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup5;
    }
}