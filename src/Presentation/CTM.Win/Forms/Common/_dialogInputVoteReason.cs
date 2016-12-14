using System;
using CTM.Services.InvestmentDecision;
using CTM.Win.Extensions;
using CTM.Win.Util;

namespace CTM.Win.Forms.Common
{
    public partial class _dialogInputVoteReason : BaseForm
    {
        #region Fields

        private readonly IInvestmentDecisionService _IDService;

        #endregion Fields

        #region Properties

        public string ContentTitle { get; set; }

        #endregion Properties

        #region Delegates

        public delegate void ReturnContentToParentForm(int categoryId, string content);

        public event ReturnContentToParentForm ReturnEvent;

        #endregion Delegates

        #region Constructors

        public _dialogInputVoteReason(IInvestmentDecisionService IDService)
        {
            InitializeComponent();

            this._IDService = IDService;
        }

        #endregion Constructors

        #region Utilities

        private bool InputCheck()
        {
            if (string.IsNullOrEmpty(this.treeListLookUpEdit1.SelectedValue()))
            {
                DXMessage.ShowTips("请选择类别！");
                return false;
            }

            if (string.IsNullOrEmpty(this.memoContent.Text.Trim()))
            {
                DXMessage.ShowTips("内容不能为空！");
                this.memoContent.Focus();
                return false;
            }

            return true;
        }

        #endregion Utilities

        #region Events

        private void _dialogIDReasonContentEdit_Load(object sender, EventArgs e)
        {
            try
            {
                this.Text = ContentTitle;

                //类别
                var categories = _IDService.GetIDReasonCategories();

                this.treeListLookUpEdit1.Properties.DisplayMember = "FullName";
                this.treeListLookUpEdit1.Properties.ValueMember = "Id";
                this.treeListLookUpEdit1TreeList.Initialize(categories, "Id", "ParentId", editable: false, autoWidth: true, showColumns: false, showVertLines: false, showHorzLines: false);

                categories = null;
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        private void treeListLookUpEdit1TreeList_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            if (e.Node == null) return;

            e.Node.ExpandAll();

            this.treeListLookUpEdit1.EditValue = e.Node.GetValue(this.tcId);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnOk.Enabled = false;

                if (!InputCheck()) return;

                var content = this.memoContent.Text.Trim();
                var categoryId = int.Parse(this.treeListLookUpEdit1.SelectedValue());
                this.DialogResult = System.Windows.Forms.DialogResult.OK;

                ReturnEvent?.Invoke(categoryId, content);

                this.Close();
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
            finally
            {
                this.btnOk.Enabled = true;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion Events
    }
}