using System;
using System.Data;
using System.Linq;
using CTM.Core.Domain.User;
using CTM.Core.Util;
using CTM.Data;
using CTM.Services.Common;
using CTM.Services.Log;
using CTM.Services.User;
using CTM.Win.Extensions;
using CTM.Win.Util;

namespace CTM.Win.Forms.Admin.Log
{
    public partial class FrmLoginLog : BaseForm
    {
        #region Fields

        private readonly IUserService _userService;
        private readonly ILogService _logService;
        private readonly ICommonService _commonService;

        #endregion Fields

        #region Constructors

        public FrmLoginLog(IUserService userService, ILogService logService, ICommonService commonService)

        {
            InitializeComponent();

            this._userService = userService;
            this._logService = logService;
            this._commonService = commonService;
        }

        #endregion Constructors

        #region Utilities

        private void FormInit()
        {
            this.deLogin.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.deLogin.EditValue = _commonService.GetCurrentServerTime().Date;

            var investors = _userService.GetAllUsers(true);
            var allUserModel = new UserInfo()
            {
                Code = string.Empty,
                Name = "全部",
            };

            investors.Add(allUserModel);
            investors = investors.OrderBy(x => x.Name).ToList();

            this.luInvestor.Initialize(investors, "Code", "Name");

            this.gridView1.SetLayout(showAutoFilterRow: false, showCheckBoxRowSelect: false, columnAutoWidth: true);
        }

        private void BindLogInfo()
        {
            var logDate = CommonHelper.StringToDateTime(this.deLogin.EditValue.ToString());
            var investor = this.luInvestor.SelectedValue();

            var connString = System.Configuration.ConfigurationManager.ConnectionStrings["CTMContext"].ToString();
            var sqlScript = $@"EXEC [dbo].[sp_GetLogInfo] @LogDate ='{logDate}', @InvestorCode = '{investor}' ";

            var ds = SqlHelper.ExecuteDataset(connString, CommandType.Text, sqlScript);

            this.gridControl1.DataSource = ds?.Tables[0];
        }

        #endregion Utilities

        #region Events

        private void LoginLog_Load(object sender, EventArgs e)
        {
            try
            {
                FormInit();
                BindLogInfo();
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnSearch.Enabled = false;
                var now = _commonService.GetCurrentServerTime().Date;
                if (CommonHelper.StringToDateTime(this.deLogin.EditValue.ToString()) > now)
                    this.deLogin.EditValue = now;

                BindLogInfo();
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
            finally
            {
                this.btnSearch.Enabled = true;
            }
        }

        #endregion Events
    }
}