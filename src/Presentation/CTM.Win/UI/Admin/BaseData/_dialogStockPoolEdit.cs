using System;
using System.Collections.Generic;
using System.Linq;
using CTM.Core;
using CTM.Core.Domain.Stock;
using CTM.Services.Common;
using CTM.Services.Stock;
using CTM.Services.User;
using CTM.Win.Extensions;
using CTM.Win.Models;

namespace CTM.Win.UI.Admin.BaseData
{
    public partial class _dialogStockPoolEdit : BaseForm
    {
        #region Fields

        private readonly IStockService _stockService;
        private readonly IUserService _userService;
        private readonly ICommonService _commonService;

        private int _stockId;
        private bool _isEdit;

        #endregion Fields

        #region Properties

        public int StockId
        {
            get { return this._stockId; }
            set { this._stockId = value; }
        }

        #endregion Properties

        #region Delegates

        public delegate void RefreshParentForm();

        public event RefreshParentForm RefreshEvent;

        #endregion Delegates

        #region Constructors

        public _dialogStockPoolEdit(IStockService stockService, IUserService userService, ICommonService commonService)
        {
            InitializeComponent();

            this._stockService = stockService;
            this._userService = userService;
            this._commonService = commonService;
        }

        #endregion Constructors

        #region Events

        private void _dialogStockPoolEdit_Load(object sender, EventArgs e)
        {
            var stock = _stockService.GetStockInfoById(_stockId);

            this.txtCode.Text = stock.FullCode;
            this.txtName.Text = stock.Name;

            var managers = _userService.GetAllManager();
            this.luBand.Initialize(managers, "Code", "Name", showHeader: true, enableSearch: true);
            this.luTarget.Initialize(managers, "Code", "Name", showHeader: true, enableSearch: true);

            var stockPool = _stockService.GetStockPoolInfoByStockId(_stockId);

            //修改股票池的场合
            if (stockPool != null)
            {
                this._isEdit = true;

                luBand.EditValue = stockPool.BandPrincipal;

                luTarget.EditValue = stockPool.TargetPrincipal;

                this.memoRemarks.Text = stockPool.Remarks;
            }
            else
            {
                this._isEdit = false;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            //if (this.luTarget.EditValue == null || this.luTarget.EditValue.ToString() == "nulltext")
            //{
            //    DXMessage.ShowTips("请选择目标负责人！");
            //    return;
            //}

            //if (this.luBand.EditValue == null || this.luBand.EditValue.ToString() == "nulltext")
            //{
            //    DXMessage.ShowTips("请选择波段负责人！");
            //    return;
            //}

            var targetPrincipal = this.luTarget.SelectedValue();

            var bandPrincipal = this.luBand.SelectedValue();

            var logModel = new StockPoolLog()
            {
                StockId = _stockId,
                BandPrincipal = bandPrincipal,
                TargetPrincipal = targetPrincipal,
                OperatorCode = LoginInfo.CurrentUser.UserCode,
                OperatorTime = _commonService.GetCurrentServerTime(),
            };

            //修改股票池的场合
            if (this._isEdit)
            {
                var stockPool = _stockService.GetStockPoolInfoByStockId(this._stockId);
                stockPool.BandPrincipal = bandPrincipal;
                stockPool.TargetPrincipal = targetPrincipal;
                stockPool.StockId = _stockId;
                stockPool.Remarks = this.memoRemarks.Text.Trim();

                _stockService.UpdateStockPoolInfo(stockPool);

                logModel.Type = (int)EnumLibrary.OperateType.Edit;
            }
            //添加股票池的场合
            else
            {
                var stockPool = new StockPoolInfo
                {
                    BandPrincipal = bandPrincipal,
                    TargetPrincipal = targetPrincipal,
                    StockId = _stockId,
                    Remarks = this.memoRemarks.Text.Trim(),
                };

                _stockService.AddStockPoolInfo(stockPool);

                logModel.Type = (int)EnumLibrary.OperateType.Add;
            }

            //添加股票池操作日志
            _stockService.AddStockPoolLog(logModel);

            RefreshEvent?.Invoke();

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion Events
    }
}