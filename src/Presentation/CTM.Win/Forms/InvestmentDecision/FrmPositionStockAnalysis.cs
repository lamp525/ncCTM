using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using CTM.Core.Domain.User;
using CTM.Core.Infrastructure;
using CTM.Core.Util;
using CTM.Data;
using CTM.Services.Common;
using CTM.Services.InvestmentDecision;
using CTM.Win.Extensions;
using CTM.Win.Models;
using CTM.Win.Util;
using DevExpress.Utils.Drawing;
using DevExpress.XtraEditors.ViewInfo;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace CTM.Win.Forms.InvestmentDecision
{
    public partial class FrmPositionStockAnalysis : BaseForm
    {
        #region Fields

        private readonly ICommonService _commonService;
        private readonly IInvestmentDecisionService _IDService;

        private bool _isExpanded = true;

        #endregion Fields

        #region Constructors

        public FrmPositionStockAnalysis(ICommonService commonService, IInvestmentDecisionService IDService)
        {
            InitializeComponent();

            this._commonService = commonService;
            this._IDService = IDService;
        }

        #endregion Constructors

        #region Utilities

        private void FormInit()
        {
            this.tabPane1.SelectedPage = this.tpRecent;

            var now = _commonService.GetCurrentServerTime().Date;

            #region Page Recent

            this.deTradeDate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.deTradeDate.EditValue = now;

            this.gridView1.SetLayout(showCheckBoxRowSelect: false, editable: true, editorShowMode: DevExpress.Utils.EditorShowMode.MouseDown, readOnly: false, showGroupPanel: true, showFilterPanel: false, showAutoFilterRow: true, rowIndicatorWidth: 40);

            foreach (GridColumn column in this.gridView1.Columns)
            {
                if (column.Name == this.colOperate.Name)
                    column.OptionsColumn.AllowEdit = true;
                else
                    column.OptionsColumn.AllowEdit = false;
            }

            #endregion Page Recent

            #region Page Search

            this.deFrom.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.deFrom.EditValue = now.AddMonths(-1);
            this.deTo.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.deTo.EditValue = now.AddDays(1);
            this.gridView2.SetLayout(showGroupPanel: true, showAutoFilterRow: true, showCheckBoxRowSelect: false);

            this.btnExpand.Enabled = false;

            #endregion Page Search
        }

        private void BindPSAInfo()
        {
            this.gridControl1.DataSource = null;

            var connString = System.Configuration.ConfigurationManager.ConnectionStrings["CTMContext"].ToString();

            var commandText = $@"SELECT TOP 25 * FROM [dbo].[PositionStockAnalysisInfo] ORDER BY SerialNo DESC";

            var ds = SqlHelper.ExecuteDataset(connString, CommandType.Text, commandText);

            if (ds == null || ds.Tables.Count == 0) return;

            var source = ds.Tables[0];
            this.gridControl1.DataSource = source;
        }

        private void DisplayPSAEdit(DataRow dr)
        {
            var dialog = EngineContext.Current.Resolve<_dialogPSAEdit>();
            dialog.Owner = this.ParentForm;
            dialog.Text = "股票池操作建议";
            dialog.StartPosition = FormStartPosition.CenterScreen;
            dialog.SerialNo = dr[colSerialNo.FieldName].ToString();
            dialog.AnalysisDate = CommonHelper.StringToDateTime(dr[colAnalysisDate.FieldName].ToString());
            dialog.Show();
        }

        private void DisplayPSAResult(DataRow dr)
        {
            var dialog = EngineContext.Current.Resolve<_dialogPSAResult>();
            dialog.Owner = this.ParentForm;
            dialog.Text = "股票池操作建议一览";
            dialog.StartPosition = FormStartPosition.CenterScreen;
            dialog.SerialNo = dr[colSerialNo.FieldName].ToString();
            dialog.AnalysisDate = CommonHelper.StringToDateTime(dr[colAnalysisDate.FieldName].ToString());
            dialog.Show();
        }

        private void OperateButtonStatusSetting(DataRow dr, ButtonEditViewInfo buttonVI)
        {
            buttonVI.RightButtons[0].Button.Enabled = true;
            buttonVI.RightButtons[0].State = ObjectState.Normal;

            buttonVI.RightButtons[1].Button.Enabled = LoginInfo.CurrentUser.IsAdmin;
            buttonVI.RightButtons[1].State = ObjectState.Normal;

            buttonVI.RightButtons[2].Button.Enabled = true;
            buttonVI.RightButtons[2].State = ObjectState.Normal;
        }

        private void ExpandOrCollapse()
        {
            if (_isExpanded)
                this.gridView2.CollapseAllGroups();
            else
                this.gridView2.ExpandAllGroups();

            _isExpanded = !_isExpanded;
            this.btnExpand.Text = _isExpanded ? " 全部收起 " : " 全部展开 ";
        }

        #endregion Utilities

        #region Events

        private void FrmCloseStockAnalysis_Load(object sender, EventArgs e)
        {
            try
            {
                FormInit();

                BindPSAInfo();
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        private void tabPane1_SelectedPageChanged(object sender, DevExpress.XtraBars.Navigation.SelectedPageChangedEventArgs e)
        {
            try
            {
                if (e.Page.Caption == tpSearch.Caption)
                {
                    var connString = System.Configuration.ConfigurationManager.ConnectionStrings["CTMContext"].ToString();

                    //股票信息
                    var stockCommandText = $@"SELECT DISTINCT StockCode, StockName FROM [dbo].[v_PSADetail] ";

                    var dsStock = SqlHelper.ExecuteDataset(connString, CommandType.Text, stockCommandText);

                    if (dsStock == null || dsStock.Tables.Count == 0) return;

                    var stocks = dsStock.Tables[0].AsEnumerable().Select(x => new StockInfoModel
                    {
                        FullCode = x.Field<string>("StockCode").Trim(),
                        Name = x.Field<string>("StockName").Trim(),
                        DisplayMember = x.Field<string>("StockCode").Trim() + " - " + x.Field<string>("StockName").Trim(),
                    }
                    ).ToList();

                    var allStock = new StockInfoModel()
                    {
                        FullCode = string.Empty,
                        Name = "全部",
                        DisplayMember = "全部",
                    };
                    stocks.Add(allStock);
                    stocks = stocks.OrderBy(x => x.FullCode).ToList();

                    this.luStock.Initialize(stocks, "FullCode", "DisplayMember", enableSearch: true);

                    //投资人员
                    var investorCommandText = $@"SELECT DISTINCT InvestorCode , InvestorName FROM [dbo].[v_PSADetail] ";

                    var dsInvestor = SqlHelper.ExecuteDataset(connString, CommandType.Text, investorCommandText);

                    if (dsInvestor == null || dsInvestor.Tables.Count == 0) return;

                    var investors = dsInvestor.Tables[0].AsEnumerable().Select(x => new UserInfo
                    {
                        Code = x.Field<string>("InvestorCode").Trim(),
                        Name = x.Field<string>("InvestorName").Trim(),
                    }
                    ).ToList();

                    var allInvestor = new UserInfo
                    {
                        Code = string.Empty,
                        Name = "全部",
                    };
                    investors.Add(allInvestor);
                    investors = investors.OrderBy(x => x.Code).ToList();

                    this.luInvestor.Initialize(investors, "Code", "Name", enableSearch: true);
                }
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        #region Page Recent

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnAdd.Enabled = false;

                var analysisDate = CommonHelper.StringToDateTime(this.deTradeDate.EditValue.ToString());

                var connString = System.Configuration.ConfigurationManager.ConnectionStrings["CTMContext"].ToString();
                var commandText = $@"EXEC [dbo].[sp_GeneratePSAInfo]  @AnalysisDate = '{analysisDate}'";
                SqlHelper.ExecuteNonQuery(connString, CommandType.Text, commandText);

                BindPSAInfo();
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

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnRefresh.Enabled = false;

                BindPSAInfo();
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
            finally
            {
                this.btnRefresh.Enabled = true;
            }
        }

        private void gridView1_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        private void gridView1_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            var myView = sender as GridView;

            DataRow dr = myView.GetDataRow(e.RowHandle);

            if (dr == null) return;

            //操作
            if (e.Column.Name == this.colOperate.Name)
            {
                ButtonEditViewInfo buttonVI = (ButtonEditViewInfo)((GridCellInfo)e.Cell).ViewInfo;
                OperateButtonStatusSetting(dr, buttonVI);
            }
        }

        private void riButtonEditOperate_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                e.Button.Enabled = false;

                var myView = this.gridView1;

                DataRow dr = myView.GetDataRow(myView.FocusedRowHandle);

                var serialNo = dr?[colSerialNo.FieldName]?.ToString();

                if (string.IsNullOrEmpty(serialNo)) return;

                var buttonTag = e.Button.Tag.ToString().Trim();

                if (string.IsNullOrEmpty(buttonTag)) return;

                if (buttonTag == "Delete")
                {
                    if (DXMessage.ShowYesNoAndWarning("确定删除该记录所有投资人员的分析信息吗？") == DialogResult.Yes)
                    {
                        this._IDService.DeletePSAInfo(serialNo);

                        BindPSAInfo();
                    }
                }
                else if (buttonTag == "Edit")
                {
                    DisplayPSAEdit(dr);
                }
                else if (buttonTag == "View")
                {
                    DisplayPSAResult(dr);
                }
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
            finally
            {
                e.Button.Enabled = true;
            }
        }

        #endregion Page Recent

        #region Page Search

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnSearch.Enabled = false;

                var dateFrom = CommonHelper.StringToDateTime(this.deFrom.EditValue.ToString());
                var dateTo = CommonHelper.StringToDateTime(this.deTo.EditValue.ToString());

                var stockCode = this.luStock.SelectedValue();
                var investorCode = this.luInvestor.SelectedValue();

                var connString = System.Configuration.ConfigurationManager.ConnectionStrings["CTMContext"].ToString();

                var commandText = $@" SELECT *  FROM [dbo].[v_PSADetail]  WHERE AnalysisDate BETWEEN '{dateFrom}' AND '{dateTo}' ";

                if (!string.IsNullOrEmpty(stockCode))
                    commandText += $@" AND StockCode = '{stockCode}' ";

                if (!string.IsNullOrEmpty(investorCode))
                    commandText += $@" AND InvestorCode = '{investorCode}' ";

                commandText += $@" ORDER BY StockCode, AnalysisDate DESC, InvestorName ";

                var dsStock = SqlHelper.ExecuteDataset(connString, CommandType.Text, commandText);

                this.gridControl2.DataSource = dsStock?.Tables?[0];

                _isExpanded = false;
                this.btnExpand.Enabled = true;
                ExpandOrCollapse();
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

        private void gridView2_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        private void btnExpand_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnExpand.Enabled = false;

                ExpandOrCollapse();
            }
            finally
            {
                this.btnExpand.Enabled = true;
            }
        }

        #endregion Page Search

        #endregion Events
    }
}