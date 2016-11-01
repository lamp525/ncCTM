using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CTM.Win.UI.InvestmentDecision
{
    public partial class _dialogPSAResult : Form
    {

        #region Properties

        public string SerialNo { get; set; }

        public DateTime AnalysisDate { get; set; }

        #endregion Properties

        public _dialogPSAResult()
        {
            InitializeComponent();
        }



        private void _dialogPSAResult_Load(object sender, EventArgs e)
        {

        }
    }
}
