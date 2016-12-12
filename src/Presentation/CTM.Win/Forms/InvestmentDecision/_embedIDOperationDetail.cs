using System;
using CTM.Services.InvestmentDecision;

namespace CTM.Win.Forms.InvestmentDecision
{
    public partial class _embedIDOperationDetail : BaseForm
    {
        #region Fields

        private readonly IInvestmentDecisionService _IDService;

        #endregion Fields

        #region Constructors

        public _embedIDOperationDetail(IInvestmentDecisionService IDService)
        {
            InitializeComponent();

            this._IDService = IDService;
        }

        #endregion Constructors

        #region Events

        private void _embedIDOperationDetail_Load(object sender, EventArgs e)
        {
        }

        #endregion Events
    }
}