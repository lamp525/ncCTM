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
using CTM.Win.Util;

namespace CTM.Win.Forms.InvestmentDecision
{
    public partial class _dialogIDOperationRelateRecord : BaseForm
    {

        public string OperateNo { get; set; }

        public _dialogIDOperationRelateRecord()
        {
            InitializeComponent();
        }

        private void BindRecord()
        {
            var connString = System.Configuration.ConfigurationManager.ConnectionStrings["CTMContext"].ToString();
            var relateRecordCommandText = $@"EXEC [dbo].[sp_GetIDOperationRelateRecord] @OperateNo = '{OperateNo}'";

            var dsRecords = SqlHelper.ExecuteDataset(connString, CommandType.Text, relateRecordCommandText);

            this.gridControl1.DataSource = dsRecords?.Tables?[0];
            this.gridView1.PopulateColumns();
        }

        private void _dialogIDOperationRelateRecord_Load(object sender, EventArgs e)
        {
            try
            {
                BindRecord();
            }
            catch (Exception ex)
            {

                DXMessage.ShowError(ex.Message);
            }
        }


    }
}
