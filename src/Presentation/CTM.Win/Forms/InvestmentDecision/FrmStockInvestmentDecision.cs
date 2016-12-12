using System;
using System.Windows.Forms;
using CTM.Core.Infrastructure;
using CTM.Win.Extensions;
using CTM.Win.Util;

namespace CTM.Win.Forms.InvestmentDecision
{
    public partial class FrmStockInvestmentDecision : BaseForm
    {
        #region Fields

        private _embedIDApplication _progressingEmbedForm = null;
        private _embedIDApplication _doneEmbedForm = null;
        private _embedIDApplication _allEmbedForm = null;

        #endregion Fields

        #region Constructors

        public FrmStockInvestmentDecision()
        {
            InitializeComponent();
        }

        #endregion Constructors

        #region Utilities

        private void ShowEmbedIDApplication(DevExpress.XtraBars.Navigation.TabNavigationPage currentPage, _embedIDApplication embedForm)
        {
            if (embedForm == null)
                embedForm = EngineContext.Current.Resolve<_embedIDApplication>();

            embedForm.FormBorderStyle = FormBorderStyle.None;
            embedForm.TopLevel = false;
            embedForm.Parent = currentPage;
            embedForm.Dock = DockStyle.Fill;
            currentPage.Controls.Add(embedForm);

            embedForm.Show();
        }

        private void RefreshProgressingPage()
        {
            this.tabPane1.SelectedPage = this.tpProgressing;

            _progressingEmbedForm.BindApplicationInfo();
        }

        #endregion Utilities

        #region Events

        private void FrmStockInvestmentDecision_Load(object sender, EventArgs e)
        {
            try
            {
                this.tabPane1.SelectedPage = this.tpProgressing;
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnAdd.Enabled = false;
                var dialog = this.CreateDialog<_dialogIDApplication>(borderStyle: FormBorderStyle.Sizable);
                dialog.RefreshEvent += new _dialogIDApplication.RefreshParentForm(RefreshProgressingPage);
                dialog.CurrentPageMode = _dialogIDApplication.PageMode.NewApplication;
                dialog.ApplyNo = string.Empty;
                dialog.OperateNo = string.Empty;
                dialog.Text = "股票投资交易申请";

                dialog.ShowDialog();
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
            finally
            {
                this.btnAdd.Enabled = true;
            }
        }

        private void tabPane1_SelectedPageChanged(object sender, DevExpress.XtraBars.Navigation.SelectedPageChangedEventArgs e)
        {
            try
            {
                var currentPage = (DevExpress.XtraBars.Navigation.TabNavigationPage)e.Page;

                if (currentPage == this.tpProgressing)
                {
                    ShowEmbedIDApplication(currentPage, _progressingEmbedForm);
                }
                else if (currentPage == this.tpDone)
                {
                    ShowEmbedIDApplication(currentPage, _doneEmbedForm);
                }
                else if (currentPage == this.tpAll)
                {
                    ShowEmbedIDApplication(currentPage, _allEmbedForm);
                }
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        #endregion Events
    }
}