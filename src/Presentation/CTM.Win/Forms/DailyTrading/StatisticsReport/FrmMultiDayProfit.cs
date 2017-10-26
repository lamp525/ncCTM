using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using CTM.Core.Util;
using CTM.Data;
using CTM.Win.Extensions;
using CTM.Win.Models;
using CTM.Win.Util;
using DevExpress.Utils;

namespace CTM.Win.Forms.DailyTrading.StatisticsReport
{
    public partial class FrmMultiDayProfit : BaseForm
    {
        public FrmMultiDayProfit()
        {
            InitializeComponent();
        }

        private void FormInit()
        {
            this.deStart.Properties.AllowNullInput = DefaultBoolean.False;
            this.deEnd.Properties.AllowNullInput = DefaultBoolean.False;

            deStart.EditValue = "2017/08/04";
            deEnd.EditValue = DateTime.Now;

            string sqlText1 = $@"SELECT TeamId,TeamName FROM TeamInfo WHERE IsDeleted =0 ORDER BY TeamName ";
            var ds = SqlHelper.ExecuteDataset(AppConfig._ConnString, CommandType.Text, sqlText1);

            if (ds == null || ds.Tables.Count == 0) return;

            List<ComboBoxItemModel> teamInfos = new List<ComboBoxItemModel>();
            var allTeam = new ComboBoxItemModel { Text = "全部小组", Value = "-1" };
            teamInfos.Add(allTeam);

            teamInfos.AddRange(ds.Tables[0].AsEnumerable()
                .Select(x => new ComboBoxItemModel
                {
                    Text = x.Field<string>("TeamName"),
                    Value = x.Field<int>("TeamId").ToString(),
                }));

            this.cbTeam.Initialize(teamInfos, displayAdditionalItem: false);

            string defaultTeam = string.Empty;

            if (!LoginInfo.CurrentUser.IsAdmin)
            {
                string sqlText2 = $@"SELECT TeamId FROM TeamMember WHERE InvestorCode = '{LoginInfo.CurrentUser.UserCode}'";

                var obj = SqlHelper.ExecuteScalar(AppConfig._ConnString, CommandType.Text, sqlText2);

                if (obj != null)
                    defaultTeam = obj.ToString();
                else
                {
                    btnSearch.Enabled = false;
                    return;
                }
                cbTeam.ReadOnly = true;
            }
            else            
                defaultTeam = "-1";

            this.cbTeam.DefaultSelected(defaultTeam);

            this.bandedGridView1.SetLayout(showAutoFilterRow: false, showCheckBoxRowSelect: false);
            this.bandedGridView1.SetColumnHeaderAppearance();

            this.ActiveControl = this.btnSearch;
        }

        private void FrmMultiDayProfit_Load(object sender, EventArgs e)
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

        private void cbTeam_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string sqlText1 = string.Empty;
                int teamId = int.Parse(this.cbTeam.SelectedValue());

                if (teamId > 0)
                    sqlText1 = $@"SELECT InvestorCode,InvestorName FROM TeamMember  WHERE IsDeleted =0 AND TeamId = {teamId} ORDER BY InvestorName ";
                else
                    sqlText1 = $@"SELECT InvestorCode,InvestorName FROM TeamMember  WHERE IsDeleted =0 ORDER BY InvestorName ";

                var ds = SqlHelper.ExecuteDataset(AppConfig._ConnString, CommandType.Text, sqlText1);

                if (ds == null || ds.Tables.Count == 0) return;

                List<ComboBoxItemModel> investorInfos = new List<ComboBoxItemModel>();
                var allInvestor = new ComboBoxItemModel { Text = "全部人员", Value = "All" };
                investorInfos.Add(allInvestor);

                investorInfos.AddRange(ds.Tables[0].AsEnumerable()
                    .Select(x => new ComboBoxItemModel
                    {
                        Text = x.Field<string>("InvestorName"),
                        Value = x.Field<string>("InvestorCode"),
                    }));
                luInvestor.Initialize(investorInfos, "Value", "Text");

                if (LoginInfo.CurrentUser.IsAdmin)
                {
                    luInvestor.EditValue = "All";
                }
                else
                {
                    luInvestor.ReadOnly = true;
                    luInvestor.EditValue = LoginInfo.CurrentUser.UserCode;
                }
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                btnSearch.Enabled = false;

                DateTime dtStart = CommonHelper.StringToDateTime(deStart.EditValue.ToString());
                DateTime dtEnd = CommonHelper.StringToDateTime(deEnd.EditValue.ToString());
                int teamId = int.Parse(this.cbTeam.SelectedValue());
                string InvestorCode = this.luInvestor.SelectedValue();
                string sqlText = $@"EXEC [dbo].[sp_GetMultiDayProfit] @StartDate = '{dtStart}',@EndDate = '{dtEnd}',@TeamId = {teamId},@InvestorCode = N'{InvestorCode}'";

                var ds = SqlHelper.ExecuteDataset(AppConfig._ConnString, CommandType.Text, sqlText);
                if (ds == null || ds.Tables.Count == 0) return;

                this.gridControl1.DataSource = ds.Tables[0];
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
            finally
            {
                btnSearch.Enabled = true;
            }
        }
    }
}