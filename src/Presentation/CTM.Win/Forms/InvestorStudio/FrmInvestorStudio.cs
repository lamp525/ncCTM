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

namespace CTM.Win.Forms.InvestorStudio
{
    public partial class FrmInvestorStudio : BaseForm
    {
        public FrmInvestorStudio()
        {
            InitializeComponent();
        }

        private void FrmInvestorStudio_Load(object sender, EventArgs e)
        {
            try
            {
                    
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
                    }
    }
}
