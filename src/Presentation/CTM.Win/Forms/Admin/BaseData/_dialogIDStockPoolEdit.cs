using System;
using CTM.Core;
using CTM.Core.Domain.InvestmentDecision;
using CTM.Services.Common;
using CTM.Services.InvestmentDecision;
using CTM.Services.Stock;
using CTM.Services.User;
using CTM.Win.Extensions;
using CTM.Win.Models;
using CTM.Win.Util;

namespace CTM.Win.Forms.Admin.BaseData
{
    public partial class _dialogIDStockPoolEdit : BaseForm
    {
        #region Fields

        private readonly IInvestmentDecisionService _IDService;
        private readonly IUserService _userService;
        private readonly ICommonService _commonService;
        private readonly IStockService _stockService;

        private bool _isEdit;

        #endregion Fields

        #region Properties

        public string StockCode { get; set; }

        #endregion Properties

        #region Delegates

        public delegate void RefreshParentForm();

        public event RefreshParentForm RefreshEvent;

        #endregion Delegates

        #region Constructors

        public _dialogIDStockPoolEdit(
            IInvestmentDecisionService IDService,
            IUserService userService,
            ICommonService commonService,
            IStockService stockService)
        {
            InitializeComponent();

            this._IDService = IDService;
            this._userService = userService;
            this._commonService = commonService;
            this._stockService = stockService;
        }

        #endregion Constructors

        #region Events

        private void _dialogStockPoolEdit_Load(object sender, EventArgs e)
        {
            var stock = _stockService.GetStockInfoByCode(StockCode);

            this.txtCode.Text = stock.FullCode;
            this.txtName.Text = stock.Name;

            var investors = _userService.GetAllOperators();
            this.luPrincipal.Initialize(investors, "Code", "Name", showHeader: true, enableSearch: true);

            var stockPool = _IDService.GetIDStockPoolByCode(StockCode);

            //修改股票池的场合
            if (stockPool != null)
            {
                this._isEdit = true;

                luPrincipal.EditValue = stockPool.Principal;

                this.memoRemarks.Text = stockPool.Remarks;
            }
            else
            {
                this._isEdit = false;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (this.luPrincipal.EditValue == null || this.luPrincipal.EditValue.ToString() == "nulltext")
            {
                DXMessage.ShowTips("请选择主要负责人！");
                return;
            }

            var principal = this.luPrincipal.SelectedValue();

            var logModel = new InvestmentDecisionStockPoolLog()
            {
                StockCode = StockCode,
                Principal = principal,         
                OperatorCode = LoginInfo.CurrentUser.UserCode,
                OperateTime = _commonService.GetCurrentServerTime(),
            };

            //修改股票池的场合
            if (this._isEdit)
            {
                var stockPool = _IDService.GetIDStockPoolByCode(StockCode);
                stockPool.Principal = principal;
                stockPool.Remarks = this.memoRemarks.Text.Trim();

                _IDService.UpdateIDStockPool(stockPool);

                logModel.Type = (int)EnumLibrary.OperateType.Edit;
            }
            //添加股票池的场合
            else
            {
                var stockPool = new InvestmentDecisionStockPool
                {
                    StockCode = this.txtCode.Text.Trim(),
                    StockName = this.txtName.Text.Trim(),
                    Principal = principal,
                    Remarks = this.memoRemarks.Text.Trim(),
                };

                _IDService.AddIDStockPool(stockPool);

                logModel.Type = (int)EnumLibrary.OperateType.Add;
            }

            //添加股票池操作日志
             _IDService.AddIDStockPoolLog(logModel);

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