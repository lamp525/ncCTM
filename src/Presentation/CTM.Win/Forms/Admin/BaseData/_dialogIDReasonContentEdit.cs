using System;
using CTM.Services.InvestmentDecision;
using CTM.Win.Extensions;

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

        private void _dialogIDReasonContentEdit_Load(object sender, EventArgs e)
        {
            var categories = _IDService.GetIDReasonCategories();

            this.treeListLookUpEdit1.Properties.DisplayMember = "Name";
            this.treeListLookUpEdit1.Properties.ValueMember = "Id";
            this.treeListLookUpEdit1TreeList.Initialize(categories, "Id", "ParentId", editable: false, autoWidth: true, showColumns: false, showVertLines: false, showHorzLines: false);

            categories = null;
        }

        private void treeListLookUpEdit1TreeList_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            if (e.Node == null) return;

            e.Node.ExpandAll();

            this.treeListLookUpEdit1.EditValue = e.Node.GetValue(this.tcId);
        }
    }
}