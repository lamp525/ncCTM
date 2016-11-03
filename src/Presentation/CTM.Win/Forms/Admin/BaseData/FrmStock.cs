using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using CTM.Core;
using CTM.Core.Domain.Stock;
using CTM.Services.Common;
using CTM.Services.Stock;
using CTM.Win.Extensions;
using CTM.Win.Models;
using CTM.Win.Util;

namespace CTM.Win.Forms.Admin.BaseData
{
    public partial class FrmStock : BaseForm
    {
        #region Fields

        private readonly IStockService _stockService;
        private readonly ICommonService _commonService;

        private const string _layoutXmlName = "FrmStock";

        #endregion Fields

        #region Constructors

        public FrmStock(
            IStockService stockService,
            ICommonService commonService
            )
        {
            InitializeComponent();

            this._stockService = stockService;
            this._commonService = commonService;
        }

        #endregion Constructors

        #region Utilities

        private void SetOperateButtonProperties()
        {
            this.btnPool.Enabled = false;
            this.btnPool.Text = "加入|移出 股票池";
            this.btnEdit.Enabled = false;
            this.btnDelete.Enabled = false;
        }

        private void BindStockInfo()
        {
            var stocks = _stockService.GetAllStocks(showDeleted: false)
                .Select(x => new StockInfoModel
                {
                    Id = x.Id,
                    Code = x.Code,
                    FullCode = x.FullCode,
                    Name = x.Name,
                    IsDeleted = x.IsDeleted,
                    Remarks = x.Remarks,
                    IsInPool = x.IsInPool,
                }
               )
               .OrderBy(x => x.Code)
               .ToList();

            this.gridControl1.DataSource = stocks;
        }

        private void RefreshForm()
        {
            SetOperateButtonProperties();

            BindStockInfo();
        }

        private void DisplayPoolEditDialog(int stockId)
        {
            var dialog = this.CreateDialog<_dialogStockPoolEdit>();
            dialog.RefreshEvent += new _dialogStockPoolEdit.RefreshParentForm(RefreshForm);
            dialog.StockId = stockId;
            dialog.Text = "股票池设置";
            dialog.Show();
        }

        private void DisplayStockEditDialog(int stockId)
        {
            var dialog = this.CreateDialog<_dialogStockEdit>();
            dialog.RefreshEvent += new _dialogStockEdit.RefreshParentForm(RefreshForm);
            dialog.StockId = stockId;

            if (stockId > 0)
                dialog.Text = "编辑股票";
            else
                dialog.Text = "添加股票";
            dialog.ShowDialog();
        }

        #endregion Utilities

        #region Events

        private void FrmStock_Load(object sender, EventArgs e)
        {
            this.gridView1.LoadLayout(_layoutXmlName);
            this.gridView1.SetLayout(rowIndicatorWidth: 50);

            SetOperateButtonProperties();

            BindStockInfo();

            this.ActiveControl = this.btnAdd;
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

        /// <summary>
        /// 编辑股票信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, EventArgs e)
        {
            var myView = this.gridView1;

            var selectedHandles = myView.GetSelectedRows();
            if (selectedHandles.Any())
                selectedHandles = selectedHandles.Where(x => x > -1).ToArray();

            if (selectedHandles.Length != 1)
            {
                DXMessage.ShowTips("请选择一个要编辑的股票！");
                return;
            }

            //股票ID
            var stockId = int.Parse(myView.GetRowCellValue(selectedHandles[0], colId).ToString());

            DisplayStockEditDialog(stockId);
        }

        /// <summary>
        /// 添加股票信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            DisplayStockEditDialog(0);
        }

        /// <summary>
        /// 删除股票信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (DXMessage.ShowYesNoAndTips("确定删除该股票吗？") == DialogResult.No) return;

                var myView = this.gridView1;

                var selectedHandles = myView.GetSelectedRows().Where(x => x > -1).ToArray();

                var stockIds = new List<int>();

                foreach (var rowHandle in selectedHandles)
                {
                    stockIds.Add(int.Parse(myView.GetRowCellValue(rowHandle, colId).ToString()));
                }

                _stockService.DeleteStockInfoByIds(stockIds.ToArray());

                RefreshForm();
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        /// <summary>
        /// 加入、移出股票池
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPool_Click(object sender, EventArgs e)
        {
            var myView = this.gridView1;

            var selectedHandles = myView.GetSelectedRows();

            if (selectedHandles.Length != 1)
            {
                DXMessage.ShowTips("请选择一支要操作的股票！");
                return;
            }

            if (bool.Parse(myView.GetRowCellValue(selectedHandles[0], colIsInPool).ToString()))
            {
                if (DXMessage.ShowYesNoAndTips("确定将该股票移出股票池吗？") == DialogResult.Yes)
                {
                    var stockId = int.Parse(myView.GetRowCellValue(selectedHandles[0], colId).ToString());

                    _stockService.DeleteStockPoolInfoByStockId(stockId);

                    BindStockInfo();

                    var logModel = new StockPoolLog
                    {
                        StockId = stockId,
                        Type = (int)EnumLibrary.OperateType.Add,
                        OperatorCode = LoginInfo.CurrentUser.UserCode,
                        OperatorTime = _commonService.GetCurrentServerTime(),
                    };

                    _stockService.AddStockPoolLog(logModel);
                }
            }
            else
            {
                var stockId = int.Parse(myView.GetRowCellValue(selectedHandles[0], colId).ToString());

                DisplayPoolEditDialog(stockId);
            }
        }

        private void gridView1_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            var myView = this.gridView1;
            var selectedHandles = myView.GetSelectedRows();
            if (selectedHandles.Any())
                selectedHandles = selectedHandles.Where(x => x > -1).ToArray();

            if (selectedHandles.Length == 0)
            {
                this.btnPool.Text = "加入|移出 股票池";
                this.btnPool.Enabled = false;
                this.btnEdit.Enabled = false;
                this.btnDelete.Enabled = false;
            }
            else if (selectedHandles.Length > 0)
            {
                btnDelete.Enabled = true;

                if (selectedHandles.Length == 1)
                {
                    if (bool.Parse(myView.GetRowCellValue(selectedHandles[0], colIsInPool).ToString()))
                        this.btnPool.Text = "移出股票池";
                    else
                        this.btnPool.Text = "加入股票池";

                    this.btnPool.Enabled = true;
                    this.btnEdit.Enabled = true;
                }
                else
                {
                    this.btnPool.Text = "加入|移出 股票池";
                    this.btnPool.Enabled = false;
                    this.btnEdit.Enabled = false;
                }
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