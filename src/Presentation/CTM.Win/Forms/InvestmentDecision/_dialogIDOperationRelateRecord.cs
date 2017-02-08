using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using CTM.Data;
using CTM.Services.InvestmentDecision;
using CTM.Win.Extensions;
using CTM.Win.Util;

namespace CTM.Win.Forms.InvestmentDecision
{
    public partial class _dialogIDOperationRelateRecord : BaseForm
    {
        #region Fields

        private readonly IInvestmentDecisionService _IDService;
        private IList<int> _relatedRecordIds = new List<int>();

        #endregion Fields

        #region Properties

        public string ApplyNo { get; set; }

        public string OperateNo { get; set; }

        #endregion Properties

        #region Delegates

        public delegate void RefreshParentForm();

        public event RefreshParentForm RefreshEvent;

        #endregion Delegates

        #region Constructors

        public _dialogIDOperationRelateRecord(IInvestmentDecisionService IDService)
        {
            InitializeComponent();

            this._IDService = IDService;
        }

        #endregion Constructors

        #region Utilities

        private void FormInit()
        {
            this.gridView1.SetLayout(showGroupPanel: true, showFooter: true, columnAutoWidth: true, rowIndicatorWidth: 40);

            this.btnOk.Enabled = false;
        }

        private void BindRecords()
        {
            var connString = System.Configuration.ConfigurationManager.ConnectionStrings["CTMContext"].ToString();
            var relateRecordCommandText = $@"EXEC [dbo].[sp_GetIDOperationRelateRecord] @OperateNo = '{OperateNo}'";

            var dsRecords = SqlHelper.ExecuteDataset(connString, CommandType.Text, relateRecordCommandText);

            if (dsRecords == null || dsRecords.Tables.Count == 0) return;

            this.gridControl1.DataSource = dsRecords?.Tables?[0];

            this._relatedRecordIds = _IDService.GetIDOperationRelatedRecordIds(OperateNo);

            if (this._relatedRecordIds.Any())
            {
                for (int index = 0; index < dsRecords.Tables[0].Rows.Count; index++)
                {
                    DataRow dr = dsRecords.Tables[0].Rows[index];
                    if (this._relatedRecordIds.Contains(int.Parse(dr["RecordId"].ToString())))
                        this.gridView1.SelectRow(this.gridView1.GetRowHandle(index));
                }
            }
        }

        #endregion Utilities

        #region Events

        private void _dialogIDOperationRelateRecord_Load(object sender, EventArgs e)
        {
            try
            {
                FormInit();

                BindRecords();
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

        private void gridView1_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            var gv = sender as DevExpress.XtraGrid.Views.Grid.GridView;

            gv.InvalidateFooter();

            var selectedHandles = gv.GetSelectedRows().Where(x => x > -1).ToArray();

            if (selectedHandles.Any())
                this.btnOk.Enabled = true;
            else
                this.btnOk.Enabled = false;
        }

        private void gridView1_CustomDrawFooter(object sender, DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs e)
        {
            e.Graphics.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(225, 244, 255)), e.Bounds);
            var footerText = "已选择交易记录数：" + this.gridView1.GetSelectedRows().Where(x => x > -1).Count().ToString();
            var outStringFormat = new System.Drawing.StringFormat();
            outStringFormat.Alignment = System.Drawing.StringAlignment.Near;
            outStringFormat.LineAlignment = System.Drawing.StringAlignment.Center;
            e.Graphics.DrawString(footerText, new System.Drawing.Font("Arial", 11, System.Drawing.FontStyle.Bold), new System.Drawing.SolidBrush(System.Drawing.Color.Black), e.Bounds, outStringFormat);
            e.Handled = true;
        }

        private void gridView1_CustomDrawFooterCell(object sender, DevExpress.XtraGrid.Views.Grid.FooterCellCustomDrawEventArgs e)
        {
            if (e.Column == this.colDealVolume)
            {
                var selectedHandles = this.gridView1.GetSelectedRows().Where(x => x > -1);

                decimal dealVolume = 0;
                foreach (int rowHandle in selectedHandles)
                {
                    dealVolume += decimal.Parse(this.gridView1.GetRowCellValue(rowHandle, this.colDealVolume).ToString());
                }

                e.Info.DisplayText = "合计：" + dealVolume.ToString("N0");
            }
            else if (e.Column == this.colActualAmount)
            {
                var selectedHandles = this.gridView1.GetSelectedRows().Where(x => x > -1);

                decimal actualAmount = 0;
                foreach (int rowHandle in selectedHandles)
                {
                    actualAmount += decimal.Parse(this.gridView1.GetRowCellValue(rowHandle, this.colActualAmount).ToString());
                }

                e.Info.DisplayText = "合计：" + actualAmount.ToString("N4");
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                btnOk.Enabled = false;

                var myView = this.gridView1;

                var selectedHandles = myView.GetSelectedRows().Where(x => x > -1).ToArray();

                if (DXMessage.ShowYesNoAndWarning("确定将选择的交易记录关联到决策操作记录吗？") == System.Windows.Forms.DialogResult.Yes)
                {
                    var recordIds = new List<int>();

                    for (var rowhandle = 0; rowhandle < selectedHandles.Length; rowhandle++)
                    {
                        recordIds.Add(int.Parse(myView.GetRowCellValue(selectedHandles[rowhandle], "RecordId").ToString()));
                    }

                    this._IDService.AddIDOperationRelatedRecords(ApplyNo, OperateNo, recordIds);

                    DXMessage.ShowTips("交易记录关联操作成功！");

                    this.RefreshEvent?.Invoke();

                    this.Close();
                }
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
                btnOk.Enabled = true;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion Events
    }
}