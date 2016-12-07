namespace CTM.Win.Forms.InvestmentDecision
{
    partial class FrmStockInvestmentDecision
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
            this.tabPane1 = new DevExpress.XtraBars.Navigation.TabPane();
            this.tpDone = new DevExpress.XtraBars.Navigation.TabNavigationPage();
            this.tpProgressing = new DevExpress.XtraBars.Navigation.TabNavigationPage();
            this.tpAll = new DevExpress.XtraBars.Navigation.TabNavigationPage();
            this.tabPane1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPane1
            // 
            this.tabPane1.Controls.Add(this.tpAll);
            this.tabPane1.Controls.Add(this.tpDone);
            this.tabPane1.Controls.Add(this.tpProgressing);
            this.tabPane1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabPane1.Location = new System.Drawing.Point(0, 0);
            this.tabPane1.Name = "tabPane1";
            this.tabPane1.Pages.AddRange(new DevExpress.XtraBars.Navigation.NavigationPageBase[] {
            this.tpProgressing,
            this.tpDone,
            this.tpAll});
            this.tabPane1.RegularSize = new System.Drawing.Size(1542, 759);
            this.tabPane1.SelectedPage = this.tpDone;
            this.tabPane1.SelectedPageIndex = 0;
            this.tabPane1.Size = new System.Drawing.Size(1542, 759);
            this.tabPane1.TabIndex = 1;
            this.tabPane1.SelectedPageChanged += new DevExpress.XtraBars.Navigation.SelectedPageChangedEventHandler(this.tabPane1_SelectedPageChanged);
            // 
            // tpDone
            // 
            this.tpDone.Caption = "已完成决策单";
            this.tpDone.Name = "tpDone";
            this.tpDone.Size = new System.Drawing.Size(1524, 713);
            // 
            // tpProgressing
            // 
            this.tpProgressing.Caption = "进行中决策单";
            this.tpProgressing.Name = "tpProgressing";
            this.tpProgressing.Size = new System.Drawing.Size(1524, 713);
            // 
            // tpAll
            // 
            this.tpAll.Caption = "全部决策单";
            this.tpAll.Name = "tpAll";
            this.tpAll.Size = new System.Drawing.Size(1524, 713);
            // 
            // FrmStockInvestmentDecision
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1542, 759);
            this.Controls.Add(this.tabPane1);
            this.Name = "FrmStockInvestmentDecision";
            this.Text = "FrmStockInvestmentDecision";
            this.Load += new System.EventHandler(this.FrmStockInvestmentDecision_Load);
            this.tabPane1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraBars.Navigation.TabPane tabPane1;
        private DevExpress.XtraBars.Navigation.TabNavigationPage tpDone;
        private DevExpress.XtraBars.Navigation.TabNavigationPage tpProgressing;
        private DevExpress.XtraBars.Navigation.TabNavigationPage tpAll;
    }
}