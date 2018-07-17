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
        private int _AccountId;
        private bool _PageAccountFlag = false;
        private bool _PageStockFlag = false;
        private bool _PageTransFlag = false;

        public FrmAccountRC()
        {
            InitializeComponent();
        }

        private void FormInit()
        {
            this.cbInvestor.Enabled = false;

            //投资人员列表
            string sqlText = $@"SELECT
	                                            InvestorCode = T.Principal,InvestorName = U.Name
                                            FROM
                                            ( SELECT DISTINCT Principal FROM RCAccountList ) T
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
                this.cbInvestor.DefaultSelected(string.Empty);
                this.cbInvestor.Enabled = true;
            }
            else
                this.cbInvestor.DefaultSelected(LoginInfo.CurrentUser.UserCode);

            DateTime now = DateTime.Now;

            this.deStart.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.deEnd.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.deStart.EditValue = now.AddMonths(-1);
            this.deEnd.EditValue = now;
        }

        private void BindAccountList()
        {
            string investorCode = this.cbInvestor.SelectedValue();
            string sqlText = @" SELECT R.AccountId,AccountName = A.Name + ' - ' + A.SecurityCompanyName + ' - '  + A.AttributeName,R.InvestFund
                                            FROM RCAccountList  R LEFT JOIN AccountInfo A ON R.AccountId = A.Id  ";
            if (string.IsNullOrEmpty(investorCode))
            {
                sqlText += $@"WHERE R.Principal = '{investorCode}'";
            }

            DataSet ds = SqlHelper.ExecuteDataset(AppConfig._ConnString, CommandType.Text, sqlText);
            if (ds != null && ds.Tables.Count > 0)
            {
                this.gcList.DataSource = ds.Tables[0];
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
            string startDate = deStart.Text;
            string endDate = deEnd.Text;

            string sqlText = $@"EXEC sp_RC_GetAccountProfit @AccountId = {_AccountId}, @StartDate ='{startDate}', @EndDate ='{endDate}'";
            DataSet ds = SqlHelper.ExecuteDataset(AppConfig._ConnString, CommandType.Text, sqlText);
            if (ds != null && ds.Tables.Count > 0)
            {
                gcAccount.DataSource = ds.Tables[0];
            }
        }

        private void GetTransProfit()
        {
            string startDate = deStart.Text;
            string endDate = deEnd.Text;

            string sqlText = $@"EXEC sp_RC_GetStockProfit @AccountId = {_AccountId}, @StartDate ='{startDate}', @EndDate ='{endDate}'";
            DataSet ds = SqlHelper.ExecuteDataset(AppConfig._ConnString, CommandType.Text, sqlText);
            if (ds != null && ds.Tables.Count > 0)
            {
            }
        }

        private void GetStockProfit()
        {
            string startDate = deStart.Text;
            string endDate = deEnd.Text;

            string sqlText = $@"EXEC sp_RC_GetTransProfit @AccountId = {_AccountId}, @StartDate ='{startDate}', @EndDate ='{endDate}'";
            DataSet ds = SqlHelper.ExecuteDataset(AppConfig._ConnString, CommandType.Text, sqlText);
            if (ds != null && ds.Tables.Count > 0)
            {
            }
        }

        private void DisplayAccountChart()
        {
            DataTable dtAccount = this.gcAccount.DataSource as DataTable;

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
            BindAccountList();
        }

        private void gvAccount_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                var gv = sender as DevExpress.XtraGrid.Views.Grid.GridView;
                DataRow row = gv.GetDataRow(gv.FocusedRowHandle);

                if (row == null) return;

                _AccountId = int.Parse(row["AccountId"].ToString());
                DisplayLatestAccountProfit();

                this.xtratabcontrol1.SelectedTabPageIndex = 0;
            }
            catch (Exception)
            {
                throw;
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
    }
}