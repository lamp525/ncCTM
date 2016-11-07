namespace CTM.Win.Forms.Admin.BaseData
{
    partial class FrmIDReason
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
            this.gvContent = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.tlCategory = new DevExpress.XtraTreeList.TreeList();
            this.tcId = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.tcParentId = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.tcName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.tcParentName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.tcRemarks = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvContent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tlCategory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.gridControl1);
            this.layoutControl1.Controls.Add(this.tlCategory);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(1456, 697);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // gridControl1
            // 
            this.gridControl1.Location = new System.Drawing.Point(536, 29);
            this.gridControl1.MainView = this.gvContent;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(908, 634);
            this.gridControl1.TabIndex = 5;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvContent});
            // 
            // gvContent
            // 
            this.gvContent.GridControl = this.gridControl1;
            this.gvContent.Name = "gvContent";
            // 
            // tlCategory
            // 
            this.tlCategory.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.tcId,
            this.tcParentId,
            this.tcName,
            this.tcParentName,
            this.tcRemarks});
            this.tlCategory.Location = new System.Drawing.Point(12, 29);
            this.tlCategory.Name = "tlCategory";
            this.tlCategory.OptionsBehavior.PopulateServiceColumns = true;
            this.tlCategory.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.True;
            this.tlCategory.OptionsClipboard.CopyNodeHierarchy = DevExpress.Utils.DefaultBoolean.True;
            this.tlCategory.OptionsView.AutoWidth = false;
            this.tlCategory.Size = new System.Drawing.Size(520, 634);
            this.tlCategory.TabIndex = 4;
            this.tlCategory.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.tlCategory_FocusedNodeChanged);
            this.tlCategory.DragDrop += new System.Windows.Forms.DragEventHandler(this.tlCategory_DragDrop);
            // 
            // tcId
            // 
            this.tcId.FieldName = "Id";
            this.tcId.Name = "tcId";
            // 
            // tcParentId
            // 
            this.tcParentId.FieldName = "ParentId";
            this.tcParentId.Name = "tcParentId";
            // 
            // tcName
            // 
            this.tcName.Caption = "类别名称";
            this.tcName.FieldName = "Name";
            this.tcName.Name = "tcName";
            this.tcName.Visible = true;
            this.tcName.VisibleIndex = 0;
            this.tcName.Width = 118;
            // 
            // tcParentName
            // 
            this.tcParentName.Caption = "上级类别名称";
            this.tcParentName.FieldName = "ParentName";
            this.tcParentName.Name = "tcParentName";
            this.tcParentName.Visible = true;
            this.tcParentName.VisibleIndex = 1;
            this.tcParentName.Width = 127;
            // 
            // tcRemarks
            // 
            this.tcRemarks.Caption = "备注";
            this.tcRemarks.FieldName = "Remarks";
            this.tcRemarks.Name = "tcRemarks";
            this.tcRemarks.Visible = true;
            this.tcRemarks.VisibleIndex = 2;
            this.tcRemarks.Width = 196;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.emptySpaceItem1,
            this.layoutControlItem2});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(1456, 697);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.tlCategory;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(524, 655);
            this.layoutControlItem1.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(105, 14);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 655);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(1436, 22);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.gridControl1;
            this.layoutControlItem2.Location = new System.Drawing.Point(524, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(912, 655);
            this.layoutControlItem2.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(105, 14);
            // 
            // FrmIDReason
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1456, 697);
            this.Controls.Add(this.layoutControl1);
            this.Name = "FrmIDReason";
            this.Text = "FrmIDReason";
            this.Load += new System.EventHandler(this.FrmIDReason_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvContent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tlCategory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gvContent;
        private DevExpress.XtraTreeList.TreeList tlCategory;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraTreeList.Columns.TreeListColumn tcId;
        private DevExpress.XtraTreeList.Columns.TreeListColumn tcParentId;
        private DevExpress.XtraTreeList.Columns.TreeListColumn tcName;
        private DevExpress.XtraTreeList.Columns.TreeListColumn tcParentName;
        private DevExpress.XtraTreeList.Columns.TreeListColumn tcRemarks;
    }
}