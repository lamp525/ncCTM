using System;
using CTM.Core.Domain.InvestmentDecision;
using CTM.Services.InvestmentDecision;
using CTM.Win.Extensions;
using CTM.Win.Util;

namespace CTM.Win.Forms.Admin.BaseData
{
    public partial class _dialogIDReasonContentEdit : BaseForm
    {
        #region Fields

        private readonly IInvestmentDecisionService _IDService;

        private bool _isEdit;

        #endregion Fields

        #region Properties

        public int ContentId { get; internal set; }
        public int CategoryId { get; internal set; }

        #endregion Properties

        #region Delegates

        public delegate void RefreshParentForm(int categoryId);

        public event RefreshParentForm RefreshEvent;

        #endregion Delegates

        #region Constructors

        public _dialogIDReasonContentEdit(IInvestmentDecisionService IDService)
        {
            InitializeComponent();

            this._IDService = IDService;
        }

        #endregion Constructors

        #region Utilities

        private bool InputCheck()
        {
            if (string.IsNullOrEmpty(this.txtTitle.Text.Trim()))
            {
                DXMessage.ShowTips("标题不能为空！");
                this.txtTitle.Focus();
                return false;
            }

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
                //类别
                var categories = _IDService.GetIDReasonCategories();

                this.treeListLookUpEdit1.Properties.DisplayMember = "Name";
                this.treeListLookUpEdit1.Properties.ValueMember = "Id";
                this.treeListLookUpEdit1TreeList.Initialize(categories, "Id", "ParentId", editable: false, autoWidth: true, showColumns: false, showVertLines: false, showHorzLines: false);

                categories = null;

                this.treeListLookUpEdit1.EditValue = CategoryId;

                this._isEdit = ContentId > 0 ? true : false;

                if (this._isEdit)
                {
                    var reasonContent = _IDService.GetIDReasonContent(ContentId);

                    if (reasonContent == null) return;

                    this.txtTitle.Text = reasonContent.Title;
                    this.memoContent.Text = reasonContent.Content;
                }
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

                //添加
                if (!this._isEdit)
                {
                    var reasonContent = new DecisionReasonContent();

                    reasonContent.CategoryId = int.Parse(this.treeListLookUpEdit1.SelectedValue());
                    reasonContent.Content = this.memoContent.Text.Trim();
                    reasonContent.Title = this.txtTitle.Text.Trim();
                    reasonContent.Remarks = null;

                    _IDService.AddIDReasonContent(reasonContent);
                }
                //修改
                else
                {
                    var reasonContent = _IDService.GetIDReasonContent(ContentId);
                    reasonContent.CategoryId = int.Parse(this.treeListLookUpEdit1.SelectedValue());
                    reasonContent.Content = this.memoContent.Text.Trim();
                    reasonContent.Title = this.txtTitle.Text.Trim();

                    _IDService.UpdateIDReasonContent(reasonContent);
                }

                var categoryId = int.Parse(this.treeListLookUpEdit1.SelectedValue());

                RefreshEvent?.Invoke(categoryId);

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