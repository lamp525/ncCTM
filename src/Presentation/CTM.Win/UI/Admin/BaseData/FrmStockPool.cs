using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using CTM.Core;
using CTM.Core.Domain.Stock;
using CTM.Services.Account;
using CTM.Services.Common;
using CTM.Services.Dictionary;
using CTM.Services.Stock;
using CTM.Services.User;
using CTM.Win.Extensions;
using CTM.Win.Models;
using CTM.Win.Util;

namespace CTM.Win.UI.Admin.BaseData
{
    public partial class FrmStockPool : BaseForm
    {
        #region Fields

        private readonly IStockService _stockService;
        private readonly IAccountService _accountService;
        private readonly IUserService _userService;
        private readonly IDictionaryService _dictionaryService;
        private readonly ICommonService _commonService;

        private IList<StockInfo> _stocks;

        private const string _PoolLogFormatAdd = @"[ {0} ] —— {1} 将股票 [{2}][{3}] 添加到股票池中。【 目标负责人： {4}   波段负责人： {5} 】";
        private const string _PoolLogFormatEdit = @"[ {0} ] —— {1} 修改了股票 [{2}][{3}] 的股票池信息。【 目标负责人： {4}   波段负责人： {5} 】";
        private const string _PoolLogFormatDelete = @"[ {0} ] —— {1} 将股票 [{2}][{3}] 移出了股票池。";

        private const string _layoutXmlName = "FrmStockPool";

        private const int _RecentLogNumber = 20;

        #endregion Fields

        #region Constructors

        public FrmStockPool(
            IStockService stockService,
            IAccountService accountservice,
            IUserService userService,
            IDictionaryService dictionaryService,
            ICommonService commonService
            )
        {
            InitializeComponent();

            this._stockService = stockService;
            this._accountService = accountservice;
            this._userService = userService;
            this._dictionaryService = dictionaryService;
            this._commonService = commonService;
        }

        #endregion Constructors

        #region Utilities

        private void SetOperateButtonProperties()
        {
            this.btnEdit.Enabled = false;
            this.btnDelete.Enabled = false;
        }

        private void BindStockPool()
        {
            var stockPoolInfos = new List<StockPoolInfoModel>();

            stockPoolInfos = _stockService.GetAllStockPoolDetail()
                .Select(x => new StockPoolInfoModel
                {
                    BandPrincipal = x.BandPrincipal,
                    BandPrincipalName = x.BandName,
                    Id = x.Id,
                    Remarks = x.Remarks,
                    StockId = x.StockId,
                    StockCode = x.StockInfo.Code,
                    StockFullCode = x.StockInfo.FullCode,
                    StockName = x.StockInfo.Name,
                    TargetPrincipal = x.TargetPrincipal,
                    TargetPrincipalName = x.TargetName,
                }).ToList();

            this.gridControl1.DataSource = stockPoolInfos;
        }

        private void DisplayPoolEditDialog(int stockId)
        {
            var dialog = this.CreateDialog<_dialogStockPoolEdit>();
            dialog.RefreshEvent += new _dialogStockPoolEdit.RefreshParentForm(RefreshForm);
            dialog.StockId = stockId;
            dialog.Text = "股票池设置";
            dialog.ShowDialog();
        }

        private void RefreshForm()
        {
            SetOperateButtonProperties();

            BindStockPool();

            BindStockInfoLeft();

            DisplayPoolHistory(0, _RecentLogNumber);
        }

        private void BindStockInfoLeft()
        {
            _stocks = _stockService.GetAllStocks(showDeleted: true);

            var stocksNotInPool = _stocks.Where(x => !x.IsInPool).OrderBy(x => x.FullCode).ToList();

            var source = stocksNotInPool.Select(x => new StockInfoModel
            {
                Id = x.Id,
                Code = x.Code,
                FullCode = x.FullCode,
                Name = x.Name,
                DisplayMember = x.FullCode + " - " + x.Name,
            }
            ).ToList();

            this.luStockLeft.Initialize(source, "Id", "DisplayMember", enableSearch: true, searchColumnIndex: 1);
        }

        private void BindStockInfo()
        {
            var source = _stocks.Select(x => new StockInfoModel
            {
                Id = x.Id,
                Code = x.Code,
                FullCode = x.FullCode,
                Name = x.Name,
                DisplayMember = x.FullCode + " - " + x.Name,
            }
           ).ToList();

            var all = new StockInfoModel
            {
                Id = 0,
                FullCode = "000000",
                Name = "全部",
                DisplayMember = "000000 - 全部",
            };
            source.Add(all);

            source = source.OrderBy(x => x.FullCode).ToList();

            this.luStock.Initialize(source, "Id", "DisplayMember", enableSearch: true, searchColumnIndex: 0);
        }

        #endregion Utilities

        #region Events

        private void FrmStockPool_Load(object sender, EventArgs e)
        {
            this.gridView1.LoadLayout(_layoutXmlName);
            this.gridView1.SetLayout();

            SetOperateButtonProperties();

            BindStockPool();

            BindStockInfoLeft();

            BindStockInfo();

            this.chkAll.Checked = false;
            this.chkRecent.Checked = true;

            DisplayPoolHistory(0, _RecentLogNumber);

            this.ActiveControl = this.btnAdd;
        }

        private void DisplayPoolHistory(int stockId, int logNumber)
        {
            var logs = _stockService.GetStockPoolLogs(stockId, logNumber).OrderByDescending(x => x.OperatorTime).ToList();

            var parsedLogs = new List<string>();
            foreach (var log in logs)
            {
                var parsedLog = string.Empty;
                parsedLogs.Add(parsedLog);

                if (string.IsNullOrEmpty(log.BandPricipalName))
                    log.BandPricipalName = " / ";

                if (string.IsNullOrEmpty(log.TargetPricipalName))
                    log.TargetPricipalName = " / ";

                switch (log.Type)
                {
                    case (int)EnumLibrary.OperateType.Add:
                        parsedLog = string.Format(_PoolLogFormatAdd, log.OperatorTime, log.OperatorName, log.StockName, log.StockFullCode, log.TargetPricipalName, log.BandPricipalName);
                        break;

                    case (int)EnumLibrary.OperateType.Edit:
                        parsedLog = string.Format(_PoolLogFormatEdit, log.OperatorTime, log.OperatorName, log.StockName, log.StockFullCode, log.TargetPricipalName, log.BandPricipalName);
                        break;

                    case (int)EnumLibrary.OperateType.Delete:
                        parsedLog = string.Format(_PoolLogFormatDelete, log.OperatorTime, log.OperatorName, log.StockName, log.StockFullCode);
                        break;
                }

                parsedLogs.Add(parsedLog);
            }

            this.lbHistoryLog.Items.Clear();
            this.lbHistoryLog.Items.AddRange(parsedLogs.ToArray());
        }

        private void gridView1_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            var myView = this.gridView1;
            var selectedHandles = myView.GetSelectedRows();
            if (selectedHandles.Any())
                selectedHandles = selectedHandles.Where(x => x > -1).ToArray();

            if (selectedHandles.Length == 0)
            {
                this.btnEdit.Enabled = false;
                this.btnDelete.Enabled = false;
            }
            else if (selectedHandles.Length > 0)
            {
                btnDelete.Enabled = true;

                if (selectedHandles.Length == 1)
                {
                    this.btnEdit.Enabled = true;
                }
                else
                {
                    this.btnEdit.Enabled = false;
                }
            }
        }

        /// <summary>
        /// 移出股票池
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnDelete.Enabled = false;

                var myView = this.gridView1;

                var selectedHandles = myView.GetSelectedRows().Where(x => x > -1).ToArray();

                if (DXMessage.ShowYesNoAndTips("确定将该股票移出股票池吗？") == DialogResult.Yes)
                {
                    var stockId = int.Parse(myView.GetRowCellValue(selectedHandles[0], colStockId).ToString());

                    _stockService.DeleteStockPoolInfoByStockId(stockId);

                    var logModel = new StockPoolLog
                    {
                        StockId = stockId,
                        Type = (int)EnumLibrary.OperateType.Delete,
                        OperatorCode = LoginInfo.CurrentUser.UserCode,
                        OperatorTime = _commonService.GetCurrentServerTime(),
                    };

                    _stockService.AddStockPoolLog(logModel);

                    RefreshForm();
                }
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
            finally
            {
                this.btnDelete.Enabled = true;
            }
        }

        /// <summary>
        /// 修改股票池信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnEdit.Enabled = false;

                var myView = this.gridView1;

                var selectedHandles = myView.GetSelectedRows();
                if (selectedHandles.Any())
                    selectedHandles = selectedHandles.Where(x => x > -1).ToArray();

                var stockId = int.Parse(myView.GetRowCellValue(selectedHandles[0], colStockId).ToString());

                DisplayPoolEditDialog(stockId);
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
            finally
            {
                this.btnEdit.Enabled = true;
            }
        }

        /// <summary>
        /// 加入股票池
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.luStockLeft.SelectedValue()))
            {
                DXMessage.ShowTips("请选择要添加的股票信息！");
                return;
            }

            var stockId = int.Parse(luStockLeft.SelectedValue());

            DisplayPoolEditDialog(stockId);
        }

        /// <summary>
        /// 保存样式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveLayout_Click(object sender, EventArgs e)
        {
            this.gridView1.SaveLayout(_layoutXmlName);
        }

        private void chkRecent_CheckedChanged(object sender, EventArgs e)
        {
            chkAll.Checked = chkRecent.Checked ? false : true;
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            chkRecent.Checked = chkAll.Checked ? false : true;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnSearch.Enabled = false;

                var stockId = luStock.SelectedValue() == null ? 0 : int.Parse(luStock.SelectedValue());
                var logNumber = chkRecent.Checked ? _RecentLogNumber : 0;

                DisplayPoolHistory(stockId, logNumber);
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

        /// <summary>
        /// 显示数据行号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        #endregion Events
    }
}