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

namespace CTM.Win.Forms.DailyTrading.TradeIdentifier
{
    public partial class FrmTimeSharingTradeIdentifier : BaseForm
    {

        #region Constructors
        public FrmTimeSharingTradeIdentifier()
        {
            InitializeComponent();
        }
        #endregion

        #region Utilities
        private void FormInit()
        {
            
        }

        #endregion

        #region Events
        private void FrmTimeSharingTradeIdentifier_Load(object sender, EventArgs e)
        {
            try
            {
                FormInit();
            }
            catch (Exception ex)
            {

                DXMessage.ShowError(ex.Message );
            }
        }

   
        #endregion
    }
}
