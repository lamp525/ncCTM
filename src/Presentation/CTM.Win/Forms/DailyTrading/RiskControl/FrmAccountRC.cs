using CTM.Data;
using CTM.Win.Models;
using CTM.Win.Util;
using System;
using System.Collections.Generic;
using System.Data;

namespace CTM.Win.Forms.DailyTrading.RiskControl
{
    public partial class FrmAccountRC : BaseForm
    {
        public FrmAccountRC()
        {
            InitializeComponent();
        }

        private void FormInit()
        {
        }

        private void BindAccountList()
        {
            string sqlText = @" SELECT R.AccountId,AccountName = A.Name + ' - ' + A.SecurityCompanyName + ' - '  + A.AttributeName,R.InvestFund 
                                            FROM RCAccountList  R LEFT JOIN AccountInfo A ON R.AccountId = A.Id  ";
            if (LoginInfo.CurrentUser.IsAdmin)
            {
                sqlText += $@"WHERE R.Principal = '{LoginInfo.CurrentUser.UserCode}'";
            }

            DataSet ds = SqlHelper.ExecuteDataset(AppConfig._ConnString, CommandType.Text, sqlText);
            if (ds != null && ds.Tables.Count > 0)
            {
                this.gcAccount.DataSource = ds.Tables[0];          
            }
        }

        private void DisplayAccountProfit(int accountId)
        {
            string sqlText = $@" EXEC [sp_RC_GetAccountProfit] @AccountId = {accountId} ";

            DataSet ds = SqlHelper.ExecuteDataset(AppConfig._ConnString, CommandType.Text, sqlText);
            if (ds != null && ds.Tables.Count > 0)
            {
                DataRow row = ds.Tables[0].Rows[0];

                if (row != null )
                esiAccountProfit.Text = $@"日期：{row["TradeDate"]}   累计收益额：{row["AccProfit"]}   累计收益率：{row["AccRate"]}   当日盈亏：{row["DayProfit"]}   当日收益率：{row["DayRate"]}   最大回撤率：{row["MaxRetraceRate"]}";
            }
        }

        private void FrmAccountRC_Load(object sender, EventArgs e)
        {
            try
            {
                FormInit();

                BindAccountList();
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        private void gvAccount_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                var gv = sender as DevExpress.XtraGrid.Views.Grid.GridView;
                DataRow row = gv.GetDataRow(gv.FocusedRowHandle);

                if (row == null) return;

                int accountId = int.Parse(row["AccountId"].ToString());
                DisplayAccountProfit(accountId);
            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}