using DevExpress.XtraCharts;
using System;
using System.Windows.Forms;

namespace IndexAmountChart
{
    public partial class VolAmount_5M : Form
    {
        private ChartControl _chartAmount = null;
        private ChartControl _chartStandard = null;
        private ChartControl _chartAmountAcc = null;
        private ChartControl _chartVolAcc = null;

        public VolAmount_5M()
        {
            InitializeComponent();
        }

        private void CreateChartModel()
        {
            CreateChartAmount();
            //CreateChartStandard();
            //CreateChartAmountAcc();
            //CreateChartVolAcc();
        }

        private void CreateChartAmount()
        {
        }

        private void SetAxisY()
        {
            throw new NotImplementedException();
        }

        private void VolAmount_5M_Load(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}