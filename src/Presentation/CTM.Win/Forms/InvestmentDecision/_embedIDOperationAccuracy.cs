using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CTM.Win.Util;

namespace CTM.Win.Forms.InvestmentDecision
{
    public partial class _embedIDOperationAccuracy : BaseForm
    {

        #region Fields


        #endregion

        #region Constructors
        public _embedIDOperationAccuracy()
        {
            InitializeComponent();
        }
        #endregion

        #region Utilities

        #endregion

        #region Events
        private void _dialogIDAccuracyVoteResult_Load(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);                
            }
        }
        #endregion
    }
}
