using CTM.Core;
using CTM.Core.Util;
using CTM.Data;
using CTM.Win.Extensions;
using CTM.Win.Models;
using CTM.Win.Util;
using DevExpress.XtraCharts;
using System;
using System.Data;
using System.Linq;

namespace CTM.Win.Forms.DailyTrading.RiskControl
{
    public partial class FrmAccountRC : BaseForm
    {
        private DataRow _AccountRCInfo;
        private DataRow _StockRCInfo;
        private DataRow _TransRCInfo;

        private int _AccountId;
        private bool _PageAccountFlag = false;
        private bool _PageStockFlag = false;
        private bool _PageTransFlag = false;

        public FrmAccountRC()
        {
            InitializeComponent();

            GetRCInfo();
        }

        private void GetRCInfo()
        {
            try
            {
                string sqlText = @"SELECT * FROM RCInfo ";
                DataSet ds = SqlHelper.ExecuteDataset(AppConfig._ConnString, CommandType.Text, sqlText);
                if(ds!=null && ds.Tables.Count == 1 )
                {
                    _AccountRCInfo = ds.Tables[0].AsEnumerable().Where(x => x.Field<string>("Type") == "A").FirstOrDefault();
                    _StockRCInfo = ds.Tables[0].AsEnumerable().Where(x => x.Field<string>("Type") == "S").FirstOrDefault();
                    _TransRCInfo = ds.Tables[0].AsEnumerable().Where(x => x.Field<string>("Type") == "T").FirstOrDefault();
                }
            }
            catch (Exception ex)
            {

                DXMessage.ShowError (ex.Message);
            }
        }

        private void FormInit()
        {
            DateTime now = DateTime.Now;

            this.deStart.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.deEnd.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.deStart.EditValue = now.AddMonths(-1);
            this.deEnd.EditValue = now;

            this.btnView.Enabled = false;

            this.cbInvestor.Enabled = false;

            //投资人员列表
            string sqlText = $@"SELECT
	                                            InvestorCode = T.Principal,InvestorName = U.Name
                                            FROM ( SELECT DISTINCT Principal FROM RCAccountList ) T
                                            INNER JOIN UserInfo U ON T.Principal = U.Code
                                            ORDER BY U.Name";

            DataSet ds = SqlHelper.ExecuteDataset(AppConfig._ConnString, CommandType.Text, sqlText);
            if (ds != null && ds.Tables.Count == 1)
            {
                var investors = ds.Tables[0].AsEnumerable().Select(x => new ComboBoxItemModel
                {
                    Text = x.Field<string>("InvestorName"),
                    Value = x.Field<string>("InvestorCode"),
                }).ToList();

                this.cbInvestor.Initialize(investors, displayAdditionalItem: true, additionalItemValue: null);
            }

            if (LoginInfo.CurrentUser.IsAdmin)
            {
                this.cbInvestor.SelectedIndex = 0;
                this.cbInvestor.Enabled = true;
            }
            else
                this.cbInvestor.DefaultSelected(LoginInfo.CurrentUser.UserCode);


        }

        private void BindAccountList()
        {
            this.gcList.DataSource = null;           

            string investorCode = this.cbInvestor.SelectedValue();
            string sqlText = @" SELECT R.AccountId,AccountName = A.Name + ' - ' + A.SecurityCompanyName + ' - '  + A.AttributeName,R.InvestFund
                                            FROM RCAccountList  R LEFT JOIN AccountInfo A ON R.AccountId = A.Id  ";
            if (!string.IsNullOrEmpty(investorCode))
            {
                sqlText += $@"WHERE R.Principal = '{investorCode}'";
            }

            DataSet ds = SqlHelper.ExecuteDataset(AppConfig._ConnString, CommandType.Text, sqlText);
            if (ds != null && ds.Tables.Count > 0)
            {
                this.gcList.DataSource = ds.Tables[0];
                this.btnView.Enabled = true;
            }
        }

        private void DisplayLatestAccountProfit()
        {
            string sqlText = $@" EXEC [sp_RC_GetLatestAccountProfit] @AccountId = {_AccountId} ";

            DataSet ds = SqlHelper.ExecuteDataset(AppConfig._ConnString, CommandType.Text, sqlText);
            if (ds != null && ds.Tables.Count > 0)
            {
                DataRow row = ds.Tables[0].Rows[0];

                if (row != null)
                    esiAccountProfit.Text = $@"日期：{row["TradeDate"]}   累计收益额：{row["AccProfit"]}   累计收益率：{row["AccRate"]}   当日盈亏：{row["DayProfit"]}   当日收益率：{row["DayRate"]}   最大回撤率：{row["MaxRetraceRate"]}";
            }
        }

        private void GetAccountProfit()
        {
            gcAccount.DataSource = null;

            string startDate = deStart.Text;
            string endDate = deEnd.Text;

            string sqlText = $@"EXEC sp_RC_GetAccountProfit @AccountId = {_AccountId}, @StartDate ='{startDate}', @EndDate ='{endDate}'";
            DataSet ds = SqlHelper.ExecuteDataset(AppConfig._ConnString, CommandType.Text, sqlText);
            if (ds != null && ds.Tables.Count > 0)
            {
                gcAccount.DataSource = ds.Tables[0];
            }
        }

        private void GetStockProfit()
        {
            gcStock.DataSource = null;

            string startDate = deStart.Text;
            string endDate = deEnd.Text;

            string sqlText = $@"EXEC sp_RC_GetStockProfit @AccountId = {_AccountId}, @StartDate ='{startDate}', @EndDate ='{endDate}'";
            DataSet ds = SqlHelper.ExecuteDataset(AppConfig._ConnString, CommandType.Text, sqlText);
            if (ds != null && ds.Tables.Count > 0)
            {
                gcStock.DataSource = ds.Tables[0];
            }
        }

        private void GetTransProfit()
        {
            gcTrans.DataSource = null;

            string startDate = deStart.Text;
            string endDate = deEnd.Text;

            string sqlText = $@"EXEC sp_RC_GetTransProfit @AccountId = {_AccountId}, @StartDate ='{startDate}', @EndDate ='{endDate}'";
            DataSet ds = SqlHelper.ExecuteDataset(AppConfig._ConnString, CommandType.Text, sqlText);
            if (ds != null && ds.Tables.Count > 0)
            {
                gcTrans.DataSource = ds.Tables[0];
            }
        }

        private void DisplayAccountChart()
        {
            object source = this.gcAccount.DataSource;

            if (source == null) return;

            DataTable dtAccount = (source as DataTable).AsEnumerable().OrderBy(x => x.Field<DateTime>("TradeDate")).CopyToDataTable();

            if (dtAccount == null) return;

            //累计收益
            Series seAccP = this.chartAccount.Series[0];
            //累计收益率
            Series seAccR = this.chartAccount.Series[1];
            //累计回撤
            Series seRetraceA = this.chartAccount.Series[2];
            //累计回撤率
            Series seRetraceR = this.chartAccount.Series[3];

            seAccP.Points.Clear();
            seAccR.Points.Clear();
            seRetraceA.Points.Clear();
            seRetraceR.Points.Clear();

            string argument = string.Empty;
            decimal accProfit;
            decimal accRate;
            decimal retraceAmount;
            decimal retraceRate;
            foreach (DataRow row in dtAccount.Rows)
            {
                argument = CommonHelper.StringToDateTime(row["TradeDate"].ToString()).ToString("yy/MM/dd");
                accProfit = CommonHelper.StringToDecimal(row["AccProfit"].ToString());
                accRate = CommonHelper.StringToDecimal(row["AccRate"].ToString());
                retraceAmount = CommonHelper.StringToDecimal(row["RetraceAmount"].ToString());
                retraceRate = CommonHelper.StringToDecimal(row["RetraceRate"].ToString());

                seAccP.Points.Add(new SeriesPoint(argument, accProfit));
                seAccR.Points.Add(new SeriesPoint(argument, accRate));
                seRetraceA.Points.Add(new SeriesPoint(argument, retraceAmount));
                seRetraceR.Points.Add(new SeriesPoint(argument, retraceRate));
            }
        }

        private void DisplayResultView()
        {
            int pageIndex = this.xtratabcontrol1.SelectedTabPageIndex;

            switch (pageIndex)
            {
                case 0:
                    if (_PageAccountFlag == false)
                    {
                        GetAccountProfit();
                        DisplayAccountChart();
                        _PageAccountFlag = true;
                    }
                    break;

                case 1:
                    if (_PageStockFlag == false)
                    {
                        GetStockProfit();
                        _PageStockFlag = true;
                    }
                    break;

                case 2:
                    if (_PageTransFlag == false)
                    {
                        GetTransProfit();
                        _PageTransFlag = true;
                    }
                    break;

                default:
                    break;
            }
        }

        private void FrmAccountRC_Load(object sender, EventArgs e)
        {
            try
            {
                FormInit();
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        private void cbInvestor_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BindAccountList();
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        private void gvList_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                var gv = sender as DevExpress.XtraGrid.Views.Grid.GridView;
                DataRow row = gv.GetDataRow(gv.FocusedRowHandle);

                if (row == null) return;

                _AccountId = int.Parse(row["AccountId"].ToString());
                DisplayLatestAccountProfit();

                this._PageAccountFlag = false;
                this._PageStockFlag = false;
                this._PageTransFlag = false;
                DisplayResultView();
            }
            catch (Exception ex)
            {
                DXMessage.ShowError (ex.Message );
            }
        }

        private void xtratabcontrol1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            try
            {
                DisplayResultView();
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnView.Enabled = false;
                this._PageAccountFlag = false;
                this._PageStockFlag = false;
                this._PageTransFlag = false;

                DisplayResultView();
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
            finally
            {
                this.btnView.Enabled = true;
            }
        }

        private void gvList_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            gvList.DrawRowIndicator(e);
        }

        private void gvAccount_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            gvAccount.DrawRowIndicator(e);
        }

        private void gvStock_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            gvStock.DrawRowIndicator(e);
        }

        private void gvTrans_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            gvTrans.DrawRowIndicator(e);
        }

        private void gvAccount_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.RowHandle < 0 || e.CellValue == null) return;

            decimal cellValue;

            if (decimal.TryParse(e.CellValue.ToString(), out cellValue))
            {
                if (cellValue == 0)
                    e.DisplayText = "-";
            }
        }

        private void gvStock_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.RowHandle < 0 || e.CellValue == null) return;

            decimal cellValue;

            if (decimal.TryParse(e.CellValue.ToString(), out cellValue))
            {
                if (cellValue == 0)
                    e.DisplayText = "-";
            }
        }

        private void gvTrans_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.RowHandle < 0 || e.CellValue == null) return;

            decimal cellValue;

            if (decimal.TryParse(e.CellValue.ToString(), out cellValue))
            {
                if (cellValue == 0)
                    e.DisplayText = "-";
            }
        }

        private void gvTrans_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.RowHandle < 0 || e.CellValue == null) return;

            if (e.Column.FieldName.IndexOf("Retrace") >= 0 && e.Column.FieldName.IndexOf("Rate") >=0)
            {
                decimal plan = decimal.Parse(_TransRCInfo["PlanRetracement"].ToString());
                decimal max = decimal.Parse(_TransRCInfo["MaxRetracement"].ToString());
                decimal cellValue = decimal.Parse(e.CellValue.ToString());
                if (cellValue >= max)
                {
                    e.Appearance.BackColor = System.Drawing.Color.Red;
                }
                else if (cellValue >= plan)
                {
                    e.Appearance.BackColor = System.Drawing.Color.Yellow;
                }
            }
            else if (e.Column.FieldName.IndexOf("Profit") >= 0 || e.Column.FieldName.IndexOf("Rate") >= 0)
            {
                var cellValue = decimal.Parse(e.CellValue.ToString());
                if (cellValue > 0)
                    e.Appearance.ForeColor = System.Drawing.Color.Red;
                else if (cellValue < 0)
                    e.Appearance.ForeColor = System.Drawing.Color.Green;
            }
        }

        private void gvStock_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.RowHandle < 0 || e.CellValue == null) return;

            if (e.Column.FieldName.IndexOf("Retrace") >= 0 && e.Column.FieldName.IndexOf("Rate") >= 0)
            {
                decimal plan = decimal.Parse(_StockRCInfo["PlanRetracement"].ToString());
                decimal max = decimal.Parse(_StockRCInfo["MaxRetracement"].ToString());
                decimal cellValue = decimal.Parse(e.CellValue.ToString());
                if (cellValue >= max)
                {
                    e.Appearance.BackColor = System.Drawing.Color.Red;
                }
                else if (cellValue >= plan)
                {
                    e.Appearance.BackColor = System.Drawing.Color.Yellow;
                }
            }
            else if (e.Column.FieldName.IndexOf("Profit") >= 0 || e.Column.FieldName.IndexOf("Rate") >= 0)
            {
                var cellValue = decimal.Parse(e.CellValue.ToString());
                if (cellValue > 0)
                    e.Appearance.ForeColor = System.Drawing.Color.Red;
                else if (cellValue < 0)
                    e.Appearance.ForeColor = System.Drawing.Color.Green;
            }
        }

        private void gvAccount_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.RowHandle < 0 || e.CellValue == null) return;

            if (e.Column.FieldName.IndexOf("Retrace") >= 0 && e.Column.FieldName.IndexOf("Rate") >= 0)
            {
                decimal plan = decimal.Parse(_AccountRCInfo["PlanRetracement"].ToString());
                decimal max = decimal.Parse(_AccountRCInfo["MaxRetracement"].ToString());
                decimal cellValue = decimal.Parse(e.CellValue.ToString());
                if(cellValue >= max)
                {
                    e.Appearance.BackColor = System.Drawing.Color.Red;
                }
                else if (cellValue >= plan)
                {
                    e.Appearance.BackColor = System.Drawing.Color.Yellow;
                }
            }
            else  if (e.Column.FieldName.IndexOf("Profit") >= 0 || e.Column.FieldName.IndexOf("Rate") >= 0)
            {
                var cellValue = decimal.Parse(e.CellValue.ToString());
                if (cellValue > 0)
                    e.Appearance.ForeColor = System.Drawing.Color.Red;
                else if (cellValue < 0)
                    e.Appearance.ForeColor = System.Drawing.Color.Green;
            }
        }
    }
}