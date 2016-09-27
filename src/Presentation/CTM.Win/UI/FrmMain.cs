using System;
using System.Linq;
using System.Windows.Forms;
using CTM.Core;
using CTM.Core.Util;
using CTM.Services.Common;
using CTM.Services.MarginTrading;
using CTM.Services.User;
using CTM.Win.Extensions;
using CTM.Win.Models;
using CTM.Win.UI.Admin.BaseData;
using CTM.Win.UI.Admin.DataManage;
using CTM.Win.UI.Function.DataImport;
using CTM.Win.UI.Function.DataManage;
using CTM.Win.UI.Function.MarginTrading;
using CTM.Win.UI.Function.StatisticsReport;
using CTM.Win.UI.Setting;
using CTM.Win.Util;
using DevExpress.XtraBars;

namespace CTM.Win.UI
{
    public partial class FrmMain : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        #region Fields

        private readonly IUserService _userService;
        private readonly IMarginTradingService _marginService;
        private readonly ICommonService _commonService;

        private readonly LoginInfo _loginUserInfo = LoginInfo.CurrentUser;

        private const string SYSTEM_INFO = @"N.C Group     资本运营部 - {0}    当前版本：{1}";
        private const string CURRENT_USER_INFO = @"当前用户：{0}({1})    职位：{2}    部门：{3}    登录时间：{4}";
        private const string INSTALL_URL = @" 安装地址：http://10.10.10.2:8000/publish.htm";

        #endregion Fields

        #region Constructors

        public FrmMain(
            IUserService userService,
            IMarginTradingService marginService,
            ICommonService commonService
            )
        {
            InitializeComponent();

            this._userService = userService;
            this._marginService = marginService;
            this._commonService = commonService;
        }

        #endregion Constructors

        #region Utilities

        private void DisplayStatusBarInfo()
        {
            var deployVer = VersionHelper.GetDeploymentVersion();

            var verInfo = deployVer == null ? "未知" : deployVer.ToString();

            this.bsiSystemInfo.Caption = string.Format(SYSTEM_INFO, this.Text, verInfo);
            this.bsiInstallUrl.Caption = INSTALL_URL;

            var loginDate = DateTime.Now.ToString("yyyy年MM月dd日");

            var loginUserDetail = _userService.GetUserDetails(userIds: new int[] { _loginUserInfo.UserId }).SingleOrDefault();
            if (loginUserDetail != null)
            {
                this.bsiCurrentUserInfo.Caption = string.Format(CURRENT_USER_INFO, loginUserDetail.Name, loginUserDetail.Code, loginUserDetail.PositionName, loginUserDetail.DepartmentName, loginDate);
            }
        }

        private void DisplayMenu()
        {
            //股票池管理（普通用户）
            rpgStockPoolUser.Visible = false;

            //普通用户
            if (!_loginUserInfo.IsAdmin)
            {
                this.ribbonPageAdmin.Visible = false;
                this.ribbonPageAccounting.Visible = false;

                this.rpgUser.Visible = false;
                this.rpgInvestIncomeReport.Visible = false;

                //交易数据维护
                //this.bbiTradeDataManage.Visibility = BarItemVisibility.Never;

                //交易数据钩稽
                this.bbiTradeDataAudit.Visibility = BarItemVisibility.Never;

                //账户投资收益流水查询
                this.bbiAccountInvestIncomeFlow.Visibility = BarItemVisibility.Always;

                //账户股票持仓查询
                this.bbiAccountStockPosition.Visibility = BarItemVisibility.Never;

                //个人账户投资收益查询
                this.bbiUserInvestIncomeAccount.Visibility = BarItemVisibility.Always;
            }
            //管理员
            else
            {
                this.ribbonPageAdmin.Visible = true;
                this.ribbonPageAccounting.Visible = true;

                this.rpgInvestIncomeReport.Visible = false;
                this.rpgUser.Visible = false;

                //交易数据维护
                // this.bbiTradeDataManage.Visibility = BarItemVisibility.Always;

                //交易数据钩稽
                this.bbiTradeDataAudit.Visibility = BarItemVisibility.Always;

                //账户投资收益流水查询
                this.bbiAccountInvestIncomeFlow.Visibility = BarItemVisibility.Always;

                //账户股票持仓查询
                this.bbiAccountStockPosition.Visibility = BarItemVisibility.Always;

                //个人账户投资收益查询
                this.bbiUserInvestIncomeAccount.Visibility = BarItemVisibility.Always;
            }

            //历史交易记录导入
            this.bbiHistoryTradeRecordImport.Enabled = false;
        }

        private void RefreshForm()
        {
            DisplayStatusBarInfo();
            DisplayMenu();

            //关闭已经打开的Mdi子窗体
            foreach (var child in this.MdiChildren)
            {
                child.Close();
            }

            //关闭已经打开的子窗体
            foreach (var child in this.OwnedForms)
            {
                child.Close();
            }
        }

        private void DisplayStartupPage()
        {
            if (!LoginInfo.CurrentUser.IsAdmin && this._loginUserInfo.DepartmentId == (int)EnumLibrary.AccountingDepartment.Day)
            {
                var now = this._commonService.GetCurrentServerTime().Date;
                if (now.DayOfWeek != DayOfWeek.Saturday && now.DayOfWeek != DayOfWeek.Sunday)
                {
                    var todaySecuritiesLoanInfo = this._marginService.GetUserInMarginTradingInfo(new string[] { _loginUserInfo.UserCode }, (int)EnumLibrary.TradeType.Day, now, now);

                    if (todaySecuritiesLoanInfo?.Count == 0)
                        this.DisplayTabbedForm<FrmUserMarginTrading>("用户融资融券信息");
                }
            }
        }

        #endregion Utilities

        #region Events

        private void FrmMain_Load(object sender, System.EventArgs e)
        {
            try
            {
                this.defaultLookAndFeelMainForm.LookAndFeel.SkinName = AppConfigHelper.DefaultSkinName;

                DisplayStatusBarInfo();

                DisplayMenu();

                DisplayStartupPage();
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        #region Admin

        /// <summary>
        /// 数据字典
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem28_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.DisplayTabbedForm<FrmDictionary>("数据字典");
        }

        /// <summary>
        /// 部门
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem18_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.DisplayTabbedForm<FrmDepartment>("部门管理");
        }

        /// <summary>
        /// 账户管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiAccountManage_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.DisplayTabbedForm<FrmAccount>("账户管理");
        }

        /// <summary>
        /// 股票池
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem22_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.DisplayTabbedForm<FrmStockPool>("股票池管理");
        }

        /// <summary>
        /// 股票信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem21_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.DisplayTabbedForm<FrmStock>("股票信息管理");
        }

        /// <summary>
        /// 历史交易数据导入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem41_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // if (this.ActiveOpenedForm(typeof(FrmHistoryTradeDataImport).Name, false)) return;

            var dialog = this.CreateDialog<FrmHistoryTradeDataImport>();
            dialog.Text = "历史交易数据导入";
            dialog.ShowDialog();
        }

        /// <summary>
        /// 股票转移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiStockTransfer_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.DisplayTabbedForm<FrmStockTransfer>("股票转移");
        }

        /// <summary>
        /// 用户信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem23_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.DisplayTabbedForm<FrmUser>("用户管理");
        }

        #endregion Admin

        #region Function

        /// <summary>
        /// 用户融资融券信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiUserDayMarginTrading_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.DisplayTabbedForm<FrmUserMarginTrading>("用户融资融券信息");
        }

        /// <summary>
        /// 股票池管理（一般用户）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiStockPoolUser_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.DisplayTabbedForm<FrmStockPool>("股票池管理");
        }

        /// <summary>
        /// 交易数据导入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.DisplayForm<FrmTradeDataImportWizard>("交易数据导入");
        }

        /// <summary>
        /// 个人投资收益报表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //this.DisplayTabbedForm<FrmInvestIncomeReport>("");
        }

        /// <summary>
        /// 部门投资收益报表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //  this.DisplayTabbedForm<FrmStatisticsReportIndex>("");
        }

        /// <summary>
        /// 公司投资收益报表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
        }

        /// <summary>
        /// 数据维护
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiTradeDataManage_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.DisplayTabbedForm<FrmTradeDataManage>("交易数据维护");
        }

        /// <summary>
        /// 账户投资收益表（25日）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem36_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.DisplayTabbedForm<FrmAccountInvestIncomeFlow>("账户投资收益流水查询");
        }

        /// <summary>
        /// 个人投资收益表（25日）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem35_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.DisplayTabbedForm<FrmUserInvestIncomeFlow>("个人投资收益流水查询");
        }

        /// <summary>
        /// 股票持仓查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem37_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.DisplayTabbedForm<FrmStockPosition>("股票持仓查询");
        }

        /// <summary>
        /// 账户股票持仓查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem38_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.DisplayTabbedForm<FrmAccountStockPosition>("账户股票持仓查询");
        }

        /// <summary>
        /// 个人日收益查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem39_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.DisplayTabbedForm<FrmUserInvestIncomeDaily>("个人日收益查询");
        }

        /// <summary>
        /// 个人月收益查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem40_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.DisplayTabbedForm<FrmUserInvestIncomeMonthly>("个人月收益查询");
        }

        /// <summary>
        /// 个人投资收益汇总查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem42_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.DisplayTabbedForm<FrmUserInvestIncomeSummary>("个人投资收益汇总查询");
        }

        /// <summary>
        /// 个人投资收益回撤信息查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiUserInvestIncomeRetracement_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.DisplayTabbedForm<FrmUserInvestIncomeRetracement>("个人投资收益回撤信息查询");
        }

        /// <summary>
        /// 股票投资收益汇总查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiStockInvestIncomeSummary_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.DisplayTabbedForm<FrmStockInvestIncomeSummary>("股票投资收益汇总查询");
        }

        /// <summary>
        /// 个人账户投资收益查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiUserInvestIncomeAccount_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.DisplayTabbedForm<FrmUserInvestIncomeAccount>("个人账户投资收益查询");
        }

        /// <summary>
        /// 个人每日交易标识查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiDailyTradeIdentification_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                var argument = LoginInfo.CurrentUser.IsAdmin ? string.Empty : LoginInfo.CurrentUser.DepartmentId.ToString();

                ProcessHelper.StartExternalProgram("Client.exe", @".\External\DailyTradeIdentification", argument, System.Diagnostics.ProcessWindowStyle.Maximized);
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        #endregion Function

        #region Accounting
        /// <summary>
        /// 交割单数据导入（财务核算人员用）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiDeliveryDataImport_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.DisplayForm<FrmDeliveryImport>("交割单数据导入");
        }
        #endregion 

        #region Setting

        /// <summary>
        /// 用户密码修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem17_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // if (this.ActiveOpenedForm(typeof(_dialogChangePwd).Name, false)) return;

            var dialog = this.CreateDialog<_dialogChangePwd>();
            dialog.Text = "登录密码修改";
            dialog.ShowDialog();
        }

        /// <summary>
        /// 保存用户自定义界面皮肤
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void skinRibbonGalleryBarItem1_GalleryItemClick(object sender, DevExpress.XtraBars.Ribbon.GalleryItemClickEventArgs e)
        {
            string skinName = e.Item.Tag == null ? string.Empty : e.Item.Tag.ToString();

            AppConfigHelper.SetAppConfigSettings("DefaultSkinName", skinName);
        }

        #endregion Setting

        #region SystemMenu

        /// <summary>
        /// 重新登录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem11_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // if (this.ActiveOpenedForm(typeof(_dialogLogin).Name, false)) return;

            var frmLogin = this.CreateDialog<_dialogLogin>();
            frmLogin.RefreshEvent += new _dialogLogin.RefreshParentForm(RefreshForm);
            frmLogin.ReLogin = true;
            frmLogin.ShowDialog();
        }

        /// <summary>
        /// 退出系统
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem12_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        #endregion SystemMenu

        #region Application

        /// <summary>
        /// 窗体关闭X按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmMain_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            if (DXMessage.ShowYesNoAndTips("确认退出吗？") == DialogResult.Yes)
            {
                Dispose();
                Application.Exit();
            }
            else
                e.Cancel = true;
        }

        #endregion Application

        #endregion Events

      
    }
}