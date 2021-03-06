﻿using CTM.Core.Util;
using CTM.Services.Common;
using CTM.Services.MarginTrading;
using CTM.Services.User;
using CTM.Win.Extensions;
using CTM.Win.Forms.Accounting.DataManage;
using CTM.Win.Forms.Accounting.MonthlyStatement;
using CTM.Win.Forms.Accounting.StatisticsReport;
using CTM.Win.Forms.Admin.BaseData;
using CTM.Win.Forms.Admin.Log;
using CTM.Win.Forms.DailyTrading.DataManage;
using CTM.Win.Forms.DailyTrading.ReportExport;
using CTM.Win.Forms.DailyTrading.RiskControl;
using CTM.Win.Forms.DailyTrading.StatisticsReport;
using CTM.Win.Forms.DailyTrading.TradeIdentifier;
using CTM.Win.Forms.InvestmentDecision;
using CTM.Win.Forms.InvestorStudio;
using CTM.Win.Forms.MarginTrading;
using CTM.Win.Forms.Market;
using CTM.Win.Forms.Setting;
using CTM.Win.Models;
using CTM.Win.Util;
using DevExpress.XtraBars;
using System;
using System.Linq;
using System.Windows.Forms;

namespace CTM.Win.Forms
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

            this.Icon = System.Drawing.Icon.ExtractAssociatedIcon(Application.ExecutablePath);

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
            //普通用户
            if (!_loginUserInfo.IsAdmin)
            {
                this.ribbonPageAdmin.Visible = false;
                this.ribbonPageAccounting.Visible = false;

                this.rpgInvestIncomeReport.Visible = false;

                //交易数据维护
                //this.bbiTradeDataManage.Visibility = BarItemVisibility.Never;

                //账户投资收益流水查询
                this.bbiAccountInvestIncomeFlow.Visibility = BarItemVisibility.Always;

                //账户股票持仓查询
                this.bbiAccountStockPosition.Visibility = BarItemVisibility.Always;

                //个人账户投资收益查询
                this.bbiUserInvestIncomeAccount.Visibility = BarItemVisibility.Always;
            }
            //管理员
            else
            {
                this.ribbonPageAdmin.Visible = true;
                this.ribbonPageAccounting.Visible = true;

                this.rpgInvestIncomeReport.Visible = true;

                //交易数据维护
                // this.bbiTradeDataManage.Visibility = BarItemVisibility.Always;

                //账户投资收益流水查询
                this.bbiAccountInvestIncomeFlow.Visibility = BarItemVisibility.Always;

                //账户股票持仓查询
                this.bbiAccountStockPosition.Visibility = BarItemVisibility.Always;

                //个人账户投资收益查询
                this.bbiUserInvestIncomeAccount.Visibility = BarItemVisibility.Always;
            }
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
            if (!LoginInfo.CurrentUser.IsAdmin)
                this.DisplayTabbedForm<FrmHomePage>("个人首页");
        }

        #endregion Utilities

        #region Events

        #region Accounting

        #region DataMange

        /// <summary>
        /// 财务交割单数据导入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiDeliveryDataImport_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.DisplayForm<FrmDeliveryImport>("财务交割单数据导入");
        }

        /// <summary>
        /// 交割单与每日交易记录核对
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiDataVerify_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.DisplayTabbedForm<FrmTradeDataVerify>("交易数据核对");
        }

        /// <summary>
        /// 财务交割单数据维护
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiDeliveryManage_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.DisplayTabbedForm<FrmDeliveryManage>("财务交割单数据维护");
        }

        #endregion DataMange

        #region MonthlyProcess

        private void barButtonItem37_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            this.DisplayTabbedForm<FrmAccountInitVerify>("账户收益持仓核对");
        }

        /// <summary>
        /// 账户期初资金和持仓
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"><
        private void barButtonItem36_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            this.DisplayTabbedForm<FrmAccountMonthlyInit>("账户期初资金和持仓");
        }

        /// <summary>
        /// 账户资金调拨
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiAccountFundTransfer_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.DisplayTabbedForm<FrmAccountFundTransfer>("账户资金调拨");
        }

        /// <summary>
        /// 账户资金月结
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiAccountFundMonthlyStatement_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.DisplayForm<FrmAccountFundMonthlyStatements>("账户资金月结");
        }

        #endregion MonthlyProcess

        #region Report

        /// <summary>
        /// 交割单账户投资收益流水查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiDeliveryAccountInvestIncomeFlow_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.DisplayTabbedForm<FrmDeliveryAccountInvestIncomeFlow>("交割单账户投资收益流水查询");
        }

        /// <summary>
        /// 交割单账户投资收益明细查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiDeliveryAccountInvestIncomeDetail_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.DisplayTabbedForm<FrmDeliveryAccountInvestIncomeDetail>("交割单账户投资收益明细查询");
        }

        /// <summary>
        /// 账户投资资金查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiAccountInvestFund_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.DisplayTabbedForm<FrmAccountInvestFundDetail>("账户投资资金查询");
        }

        private void barButtonItem32_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.DisplayTabbedForm<FrmAccountPositionConfiguration>("账户仓位配置规划");
        }

        #endregion Report

        #endregion Accounting

        #region Admin

        #region BaseData

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
        /// 用户信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem23_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.DisplayTabbedForm<FrmUser>("用户管理");
        }

        private void barButtonItem20_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.DisplayForm<FrmIDCommittee>("决策委员会");
        }

        private void barButtonItem9_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            this.DisplayTabbedForm<FrmIDStockPool>("决策股票池");
        }

        private void barButtonItem24_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.DisplayTabbedForm<FrmIDReason>("决策理由");
        }

        #endregion BaseData

        #region Log

        private void barButtonItem33_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.DisplayTabbedForm<FrmLoginLog>("登录日志");
        }

        #endregion Log

        #endregion Admin

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

        #region DailyTrading

        /// <summary>
        /// 投资人员个人首页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem40_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            if (LoginInfo.CurrentUser.IsAdmin)
                this.DisplayTabbedForm<FrmHomePageAdmin>("统计首页");
            else
                this.DisplayTabbedForm<FrmHomePage>("个人首页");
        }

        /// <summary>
        /// 投资人员交易数据导入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.DisplayForm<FrmTradeDataImport>("投资人员交易数据导入");
        }

        /// <summary>
        /// 交易数据核对
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem35_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            this.DisplayTabbedForm<FrmTradeDataVerify>("交易数据核对");
        }

        /// <summary>
        /// 大盘5分钟趋势预测
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem39_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            this.DisplayForm<FrmIndexTrend5M>("大盘5分钟趋势预测");
        }

        /// <summary>
        /// 分时交易标识
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiDailyTradeIdentification_ItemClick(object sender, ItemClickEventArgs e)
        {
            var dialog = this.CreateDialog<FrmTimeSharingTradeIdentifier>(borderStyle: FormBorderStyle.Sizable, windowState: FormWindowState.Maximized);
            dialog.Text = "分时交易标识";
            dialog.Show();
        }

        /// <summary>
        /// 日K线交易标识
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem38_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            var dialog = this.CreateDialog<FrmKLineTradeIdentifier>(borderStyle: FormBorderStyle.Sizable, windowState: FormWindowState.Maximized);
            dialog.Text = "K线交易标识";
            dialog.Show();
        }

        #region Reports

        /// <summary>
        /// 投资收益报表导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiInvestIncomeReportExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.DisplayForm<FrmDailyReportExport>("投资收益报表导出");
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
        /// 投资人员数据维护
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiTradeDataManage_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.DisplayTabbedForm<FrmTradeDataManage>("投资人员交易数据维护");
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
        /// 个人投资收益流水查询
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
            // this.DisplayTabbedForm<FrmStockPosition>("股票持仓查询");

            this.DisplayTabbedForm<FrmStockPositionQuery>("股票持仓查询");
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
        /// 隔日短差收益查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiMultiDayProfit_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.DisplayTabbedForm<FrmMultiDayProfit>("隔日短差收益查询");
        }

        #endregion Reports

        #endregion DailyTrading

        #region Form Load

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

        #endregion Form Load

        #region InvestmentDecision

        private void barButtonItem6_ItemClick(object sender, ItemClickEventArgs e)
        {
            //this.DisplayTabbedForm<FrmInvestmentDecisionManage>("股票投资决策管理");
            this.DisplayTabbedForm<FrmStockInvestmentDecision>("股票投资决策管理");
        }

        private void barButtonItem7_ItemClick(object sender, ItemClickEventArgs e)
        {
            //this.DisplayTabbedForm<FrmMarketTrendForecastManage>("大盘趋势预测");
            this.DisplayTabbedForm<FrmMarketTrendForecast>("大盘趋势预测");
        }

        private void barButtonItem8_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            this.DisplayTabbedForm<FrmInvestmentPlanRecord>("持仓个股投资计划");
        }

        #endregion InvestmentDecision

        #region MarginTrading

        /// <summary>
        /// 用户融资融券信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiUserDayMarginTrading_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.DisplayTabbedForm<FrmUserMarginTrading>("用户融资融券信息");
        }

        #endregion MarginTrading

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

        #endregion Events

        private void barButtonItem16_ItemClick(object sender, ItemClickEventArgs e)
        {
          
        }

        private void bbiRCAccount_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.DisplayTabbedForm<FrmAccountRC>("账户投资收益及风控");
        }
    }
}