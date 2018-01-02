namespace CTM.Win.Forms.InvestorStudio
{
    partial class Dashboard1
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DevExpress.DashboardCommon.DashboardLayoutGroup dashboardLayoutGroup1 = new DevExpress.DashboardCommon.DashboardLayoutGroup();
            DevExpress.DashboardCommon.DashboardLayoutItem dashboardLayoutItem1 = new DevExpress.DashboardCommon.DashboardLayoutItem();
            DevExpress.DashboardCommon.DashboardLayoutItem dashboardLayoutItem2 = new DevExpress.DashboardCommon.DashboardLayoutItem();
            this.cardDashboardItem1 = new DevExpress.DashboardCommon.CardDashboardItem();
            this.cardDashboardItem2 = new DevExpress.DashboardCommon.CardDashboardItem();
            ((System.ComponentModel.ISupportInitialize)(this.cardDashboardItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cardDashboardItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // cardDashboardItem1
            // 
            this.cardDashboardItem1.ComponentName = "cardDashboardItem1";
            this.cardDashboardItem1.DataItemRepository.Clear();
            this.cardDashboardItem1.InteractivityOptions.IgnoreMasterFilters = false;
            this.cardDashboardItem1.Name = "Cards 1";
            this.cardDashboardItem1.ShowCaption = true;
            // 
            // cardDashboardItem2
            // 
            this.cardDashboardItem2.ComponentName = "cardDashboardItem2";
            this.cardDashboardItem2.DataItemRepository.Clear();
            this.cardDashboardItem2.InteractivityOptions.IgnoreMasterFilters = false;
            this.cardDashboardItem2.Name = "Cards 2";
            this.cardDashboardItem2.ShowCaption = true;
            // 
            // Dashboard1
            // 
            this.Items.AddRange(new DevExpress.DashboardCommon.DashboardItem[] {
            this.cardDashboardItem1,
            this.cardDashboardItem2});
            dashboardLayoutItem1.DashboardItem = this.cardDashboardItem2;
            dashboardLayoutItem1.Weight = 49.939098660170522D;
            dashboardLayoutItem2.DashboardItem = this.cardDashboardItem1;
            dashboardLayoutItem2.Weight = 50.060901339829478D;
            dashboardLayoutGroup1.ChildNodes.AddRange(new DevExpress.DashboardCommon.DashboardLayoutNode[] {
            dashboardLayoutItem1,
            dashboardLayoutItem2});
            dashboardLayoutGroup1.DashboardItem = null;
            dashboardLayoutGroup1.Orientation = DevExpress.DashboardCommon.DashboardLayoutGroupOrientation.Vertical;
            this.LayoutRoot = dashboardLayoutGroup1;
            this.Title.Text = "Dashboard";
            ((System.ComponentModel.ISupportInitialize)(this.cardDashboardItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cardDashboardItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.DashboardCommon.CardDashboardItem cardDashboardItem1;
        private DevExpress.DashboardCommon.CardDashboardItem cardDashboardItem2;
    }
}
