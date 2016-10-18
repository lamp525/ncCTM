using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CTM.Services.InvestmentDecision;
using CTM.Win.Extensions;
using CTM.Win.Util;

namespace CTM.Win.UI.InvestmentDecision
{
    public partial class FrmInvestmentDecisionMange : BaseForm
    {
        private readonly IInvestmentDecisionService _IDService;
        private const string _layoutXmlName = "FrmInvestmentDecisionMange";

        public FrmInvestmentDecisionMange(IInvestmentDecisionService IDService)
        {
            InitializeComponent();

            this._IDService = IDService;
        }

        private void BindApplicationInfo()
        {
            throw new NotImplementedException();
        }


        private void FrmInvestmentDecisionMange_Load(object sender, EventArgs e)
        {
            try
            {
                this.gridView1.LoadLayout(_layoutXmlName);
                this.gridView1.SetLayout(showCheckBoxRowSelect: true, showFilterPanel: true, showAutoFilterRow: false, rowIndicatorWidth: 50);
             

                this.btnDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnAdd.Enabled = false;
                var dialog = this.CreateDialog<_dialogTradeApplication>();
                dialog.RefreshEvent += new _dialogTradeApplication.RefreshParentForm(BindApplicationInfo);
                dialog.Text = "股票投资交易申请";
              

                dialog.ShowDialog();
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
            finally
            {
                this.btnAdd.Enabled = true;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                var myView = this.gridView1;

                var selectedHandles = myView.GetSelectedRows();

                if (selectedHandles.Length == 0) return;

                selectedHandles = myView.GetSelectedRows().Where(x => x > -1).ToArray();

                if (DXMessage.ShowYesNoAndWarning("确定删除选择的信息吗？") == DialogResult.Yes)
                {
                    var ids = new List<int>();

                    for (var rowhandle = 0; rowhandle < selectedHandles.Length; rowhandle++)
                    {
                       // ids.Add(int.Parse(myView.GetRowCellValue(selectedHandles[rowhandle], colId).ToString()));
                    }

                    //this._IDService.DeleteInvestmentDecisionForm(ids.ToArray());

                    BindApplicationInfo();
                }
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

   

        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {

        }

        private void gridView1_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {

        }
    }
}
