using System;
using System.Data;
using System.Linq;
using CTM.Core;
using CTM.Data;
using CTM.Services.Account;
using CTM.Services.Common;
using CTM.Services.Dictionary;
using CTM.Services.MonthlyStatement;
using CTM.Win.Extensions;
using CTM.Win.Models;
using CTM.Win.Util;

namespace CTM.Win.Forms.Accounting.MonthlyStatement
{
    public partial class FrmAccountInitVerify : BaseForm
    {
        #region Fields

        private readonly IDictionaryService _dictionaryService;
        private readonly IAccountService _accountService;
        private readonly ICommonService _commonService;
        private readonly IMonthlyStatementService _statementService;

        private string _connString = System.Configuration.ConfigurationManager.ConnectionStrings["CTMContext"].ToString();        

        #endregion Fields

        #region Constructors

        public FrmAccountInitVerify(IDictionaryService dictionaryService,
            IAccountService accountService,
            ICommonService commonService,
            IMonthlyStatementService statementService)
        {
            InitializeComponent();

            this._dictionaryService = dictionaryService;
            this._accountService = accountService;
            this._commonService = commonService;
            this._statementService = statementService;
        }

        #endregion Constructors

        #region Utilities

        private void FormInit()
        {
            //账户名称
            var accountNames = _accountService.GetAllAccountNames(false).ToList();
            this.cbAccount.Initialize(accountNames, displayAdditionalItem: true);

            //账户属性
            var accountAttributes = _dictionaryService.GetDictionaryInfoByTypeId((int)EnumLibrary.DictionaryType.AccountAttribute)
            .Select(x => new ComboBoxItemModel
            {
                Value = x.Code.ToString(),
                Text = x.Name
            }).ToList();
            this.cbAttribute.Initialize(accountAttributes, displayAdditionalItem: true);

            //证券公司
            var securityCompanys = _dictionaryService.GetDictionaryInfoByTypeId((int)EnumLibrary.DictionaryType.SecurityCompay)

                        .Select(x => new ComboBoxItemModel
                        {
                            Value = x.Code.ToString(),
                            Text = x.Name
                        }).OrderBy(x => x.Text).ToList();

            this.cbSecurity.Initialize(securityCompanys, displayAdditionalItem: true);

            this.gvPosition.SetLayout(showGroupPanel: true, showCheckBoxRowSelect: false);
            this.gvPosition.SetColumnHeaderAppearance();

            this.gvAccountProfit.SetLayout(showGroupPanel: true, showCheckBoxRowSelect: false, rowIndicatorWidth: 30);
            this.gvAccountProfit.SetColumnHeaderAppearance();

            this.gvStockProfit.SetLayout(showGroupPanel: true, showCheckBoxRowSelect: false, rowIndicatorWidth: 30);
            this.gvStockProfit.SetColumnHeaderAppearance();
        }

        private void LoadSelectedPage()
        {
            if (this.xtraTabControl1.SelectedTabPage == this.pagePosition)
                DisplayPositionInfoList();
            else
                DisplayProfitInfoList();
        }

        private void DisplayProfitInfoList()
        {
            this.gcAccountProfit.DataSource = null;

            var commandText = $@"EXEC [dbo].[sp_GetAccountProfitContrastData] @Year={2016}, @Month={12}, @AccountIds='{@"4,58,60,61,66,68,69,101"}'";
            var ds = SqlHelper.ExecuteDataset(_connString, CommandType.Text, commandText);

            if (ds == null || ds.Tables.Count == 0) return;

            this.gcAccountProfit.DataSource = ds.Tables[0];
        }

        private void DisplayPositionInfoList()
        {
            this.gcPosition.DataSource = null;

            var commandText = $@"EXEC [dbo].[sp_GetAccountPositionContrastData] @Year={2016}, @Month={12}, @AccountIds='{@"4,58,60,61,66,68,69,101"}'";
            var ds = SqlHelper.ExecuteDataset(_connString, CommandType.Text, commandText);

            if (ds == null || ds.Tables.Count == 0) return;

            this.gcPosition.DataSource = ds.Tables[0];
            this.gvPosition.ExpandAllGroups();
        }

        #endregion Utilities

        #region Events

        private void FrmAccountInitVerify_Load(object sender, EventArgs e)
        {
            try
            {
                FormInit();

                this.xtraTabControl1.SelectedTabPage = this.pagePosition;

                LoadSelectedPage();
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            try
            {
                LoadSelectedPage();
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        #region Search

        private void cbAccount_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void cbSecurity_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void cbAttribute_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void lbAccount_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
        }

        #endregion Search

        #region PagePosition

        private void gvPosition_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle > -1)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        private void gvPosition_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.RowHandle < 0 || e.CellValue == null) return;

            if (e.Column == this.colAccountingVolume_V
                || e.Column == this.colDeliveryVolume_V
                || e.Column == this.colDeliveryDifference_V
                || e.Column == this.colDailyVolume_V
                || e.Column == this.colDailyDifference_V)
            {
                var cellValue = Convert.ToDecimal(e.CellValue);

                if (cellValue > 0)
                    e.Appearance.ForeColor = System.Drawing.Color.Red;
                else if (cellValue < 0)
                    e.Appearance.ForeColor = System.Drawing.Color.Green;
            }
        }

        #endregion PagePosition

        #region PageProfit

        private void gvAccountProfit_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle > -1)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        private void gvAccountProfit_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.RowHandle < 0 || e.CellValue == null) return;

            if (e.Column == this.colAccountingAmount_A1
                || e.Column == this.colDeliveryAmount_A1
                || e.Column == this.colDeliveryDifference_A1
                || e.Column == this.colDailyAmount_A1
                || e.Column == this.colDailyDifference_A1)
            {
                var cellValue = Convert.ToDecimal(e.CellValue);

                if (cellValue > 0)
                    e.Appearance.ForeColor = System.Drawing.Color.Red;
                else if (cellValue < 0)
                    e.Appearance.ForeColor = System.Drawing.Color.Green;
            }
        }

        private void gvAccountProfit_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                var gv = sender as DevExpress.XtraGrid.Views.Grid.GridView;

                var row = gv.GetDataRow(e.FocusedRowHandle);

                if (row == null) return;
               
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        private void gvStockProfit_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle > -1)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        private void gvStockProfit_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
        }

        #endregion PageProfit

        #endregion Events
    }
}