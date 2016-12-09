using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using CTM.Core;
using CTM.Core.Domain.InvestmentDecision;
using CTM.Core.Infrastructure;
using CTM.Data;
using CTM.Services.Common;
using CTM.Services.InvestmentDecision;
using CTM.Win.Extensions;
using CTM.Win.Forms.Common;
using CTM.Win.Models;
using CTM.Win.Util;
using DevExpress.Utils.Drawing;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.ViewInfo;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace CTM.Win.Forms.InvestmentDecision
{
    public partial class _dialogIDOperationDetail : BaseForm
    {

        #region Fields

        private readonly IInvestmentDecisionService _IDService;


        #endregion Fields

        #region Constructors
        public _dialogIDOperationDetail(IInvestmentDecisionService IDService)
        {
            InitializeComponent();

            this._IDService = IDService;
        }
        #endregion

        #region Events
        private void _embedIDOperationDetail_Load(object sender, EventArgs e)
        {

        }
        #endregion

    }
}
