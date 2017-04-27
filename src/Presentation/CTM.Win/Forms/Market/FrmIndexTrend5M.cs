using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CTM.Data;
using CTM.Win.Extensions;
using CTM.Win.Util;
using DevExpress.XtraCharts;

namespace CTM.Win.Forms.Market
{
    public partial class FrmIndexTrend5M : BaseForm
    {

        #region Fields

        private string _connString = System.Configuration.ConfigurationManager.ConnectionStrings["CTMContext"].ToString();
        private Series _seCurrentDate;
        private Series _seNextDate;

        private DataTable _trendData = null;
        #endregion

        #region Constructors
        public FrmIndexTrend5M()
        {
            InitializeComponent();
        }
        #endregion

        #region Utilities
        private void FormInit()
        {
            this.gridView1.SetLayout(showCheckBoxRowSelect:false,showAutoFilterRow:false ,rowIndicatorWidth:30);
            this.gridView1.SetColumnHeaderAppearance();
        }


        private void ChartInit()
        {
            #region Series

            _seCurrentDate = new Series(DateTime.Now.ToShortDateString(), ViewType.Line);
            _seCurrentDate.CrosshairHighlightPoints = DevExpress.Utils.DefaultBoolean.False;
            LineSeriesView  myView1 = (LineSeriesView)_seCurrentDate.View;
            myView1.Color =  Color.DeepSkyBlue;
       

            _seNextDate = new Series("明日标准", ViewType.Spline);
            _seNextDate.CrosshairHighlightPoints = DevExpress.Utils.DefaultBoolean.False;
            SplineSeriesView myView2 = (SplineSeriesView)_seNextDate.View;
            myView2.Color = Color.OrangeRed ;
   


            this.chartControl1.Series.Add(_seCurrentDate);
            this.chartControl1.Series.Add(_seNextDate);

            #endregion Series
        }

        private void LoadTrendData()
        {
            var now = DateTime.Now.Date;

            var commandText = $@"EXEC [dbo].[sp_MTIndex5M_sz] @TradeDate ='{now}'";
            var ds = SqlHelper.ExecuteDataset(_connString, CommandType.Text, commandText);

            if (ds != null || ds.Tables.Count >0)
            {
                _trendData = ds.Tables[0];
            }

            this.gridControl1.DataSource = _trendData;

        }

        private void DisplayChart()
        {
      
        }

        #endregion


        #region Events
        private void FrmIndexTrend5M_Load(object sender, EventArgs e)
        {
            try
            {
                FormInit();
                ChartInit();
                LoadTrendData();
                DisplayChart();
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }



        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle > -1)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        #endregion


    }
}
