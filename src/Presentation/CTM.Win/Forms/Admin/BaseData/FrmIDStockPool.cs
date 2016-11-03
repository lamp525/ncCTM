using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using CTM.Core;
using CTM.Core.Domain.Stock;
using CTM.Data;
using CTM.Services.Common;
using CTM.Services.InvestmentDecision;
using CTM.Services.Stock;
using CTM.Win.Extensions;
using CTM.Win.Models;
using CTM.Win.Util;

namespace CTM.Win.Forms.Admin.BaseData
{
    public partial class FrmIDStockPool : BaseForm
    {
        #region Fields

        private readonly IStockService _stockService;
        private readonly IInvestmentDecisionService _IDService;
        private readonly ICommonService _commonService;

        private IList<StockInfo> _stocks;

        private const string _PoolLogFormatAdd = @"[ {0} ] —— {1} 将股票 [{2}][{3}] 添加到股票池中。【 目标负责人： {4}   波段负责人： {5} 】";
        private const string _PoolLogFormatEdit = @"[ {0} ] —— {1} 修改了股票 [{2}][{3}] 的股票池信息。【 目标负责人： {4}   波段负责人： {5} 】";
        private const string _PoolLogFormatDelete = @"[ {0} ] —— {1} 将股票 [{2}][{3}] 移出了股票池。";

        private const int _RecentLogNumber = 20;

        #endregion Fields

        #region Constructors

        public FrmIDStockPool(IStockService stockService, ICommonService commonService, IInvestmentDecisionService IDService)
        {
            InitializeComponent();

            this._stockService = stockService;
            this._commonService = commonService;
            this._IDService = IDService;
        }

        #endregion Constructors

        #region Utilities

        private void BindStockPool()
        {
            var connString = System.Configuration.ConfigurationManager.ConnectionStrings["CTMContext"].ToString();
            var commandText = $@" SELECT *  FROM [dbo].[v_IDStockPool] ORDER BY Principal";
            var ds = SqlHelper.ExecuteDataset(connString, CommandType.Text, commandText);
            if (ds == null || ds.Tables.Count == 0) return;

            this.gridControl1.DataSource = ds.Tables[0];
        }

        private void RefreshForm()
        {
            BindStockPool();

            BindStockInfoLeft();

            DisplayPoolHistory(0, _RecentLogNumber);
        }

        private void BindStockInfoLeft()
        {
            _stocks = _stockService.GetAllStocks(showDeleted: true);

            var stockCodesInPool = (this.gridView1.DataSource as DataView).Table.AsEnumerable().Select(x => x.Field<string>("StockCode")).ToArray();

            var stocksNotInPool = _stocks.Where(x => !stockCodesInPool.Contains(x.FullCode)).OrderBy(x => x.FullCode).ToList();

            var source = stocksNotInPool.Select(x => new StockInfoModel
            {
                Id = x.Id,
                Code = x.Code,
                FullCode = x.FullCode,
                Name = x.Name,
                DisplayMember = x.FullCode + " - " + x.Name,
            }
            ).ToList();

            this.luStockLeft.Initialize(source, "FullCode", "DisplayMember", enableSearch: true, searchColumnIndex: 1);
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

            this.luStock.Initialize(source, "FullCode", "DisplayMember", enableSearch: true, searchColumnIndex: 0);
        }

        private void DisplayPoolEditDialog(string stockCode)
        {
            var dialog = this.CreateDialog<_dialogIDStockPoolEdit>();
            dialog.RefreshEvent += new _dialogIDStockPoolEdit.RefreshParentForm(RefreshForm);
            dialog.StockCode = stockCode;
            dialog.Text = "决策股票池设置";
            dialog.ShowDialog();
        }

        #endregion Utilities

        #region Events

        private void FrmStockPool_Load(object sender, EventArgs e)
        {
            this.gridView1.SetLayout();

            this.btnEdit.Enabled = false;
            this.btnDelete.Enabled = false;

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
            //var logs = _stockService.GetStockPoolLogs(stockId, logNumber).OrderByDescending(x => x.OperatorTime).ToList();

            //var parsedLogs = new List<string>();
            //foreach (var log in logs)
            //{
            //    var parsedLog = string.Empty;
            //    parsedLogs.Add(parsedLog);

            //    if (string.IsNullOrEmpty(log.BandPricipalName))
            //        log.BandPricipalName = " / ";

            //    if (string.IsNullOrEmpty(log.TargetPricipalName))
            //        log.TargetPricipalName = " / ";

            //    switch (log.Type)
            //    {
            //        case (int)EnumLibrary.OperateType.Add:
            //            parsedLog = string.Format(_PoolLogFormatAdd, log.OperatorTime, log.OperatorName, log.StockName, log.StockFullCode, log.TargetPricipalName, log.BandPricipalName);
            //            break;

            //        case (int)EnumLibrary.OperateType.Edit:
            //            parsedLog = string.Format(_PoolLogFormatEdit, log.OperatorTime, log.OperatorName, log.StockName, log.StockFullCode, log.TargetPricipalName, log.BandPricipalName);
            //            break;

            //        case (int)EnumLibrary.OperateType.Delete:
            //            parsedLog = string.Format(_PoolLogFormatDelete, log.OperatorTime, log.OperatorName, log.StockName, log.StockFullCode);
            //            break;
            //    }

            //    parsedLogs.Add(parsedLog);
            //}

            //this.lbHistoryLog.Items.Clear();
            //this.lbHistoryLog.Items.AddRange(parsedLogs.ToArray());
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
                    var stockCodes = new List<string>();
                    for (int i = 0; i < selectedHandles.Length; i++)
                    {
                        stockCodes.Add(myView.GetRowCellValue(selectedHandles[i], colStockCode).ToString());
                    }

                    _IDService.DeleteIDStockPool(stockCodes);

                    RefreshForm();
                }
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
                this.btnDelete.Enabled = true;
            }
        }

        /// <summary>
        /// 加入股票池
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnAdd.Enabled = false;

                if (string.IsNullOrEmpty(this.luStockLeft.SelectedValue()))
                {
                    DXMessage.ShowTips("请选择要添加的股票信息！");
                    return;
                }

                var stockCode = luStockLeft.SelectedValue();

                DisplayPoolEditDialog(stockCode);
                this.luStockLeft.EditValue = null;
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

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnEdit.Enabled = false;

                var myView = this.gridView1;

                var selectedHandles = myView.GetSelectedRows();
                if (selectedHandles.Any())
                    selectedHandles = selectedHandles.Where(x => x > -1).ToArray();

                var stockCode = myView.GetRowCellValue(selectedHandles[0], colStockCode).ToString();

                DisplayPoolEditDialog(stockCode);
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
                this.btnEdit.Enabled = true;
            }
        }

        private void chkRecent_CheckedChanged(object sender, EventArgs e)
        {
            chkAll.Checked = !chkRecent.Checked;
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            chkRecent.Checked = !chkAll.Checked;
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