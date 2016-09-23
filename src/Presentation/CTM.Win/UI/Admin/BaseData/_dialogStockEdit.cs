using System;
using System.Collections.Generic;
using System.Linq;
using CTM.Core.Domain.Stock;
using CTM.Core.Util;
using CTM.Services.Stock;
using CTM.Win.Extensions;
using CTM.Win.Models;
using CTM.Win.Util;

namespace CTM.Win.UI.Admin.BaseData
{
    public partial class _dialogStockEdit : BaseForm
    {
        #region Fields

        private readonly IStockService _stockService;

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

        public _dialogStockEdit(IStockService stockService)
        {
            InitializeComponent();

            this._stockService = stockService;
        }

        #endregion Constructors

        #region Utilities

        private void BindStockInfo()
        {
            //交易市场
            var markets = new List<ComboBoxItemModel>
            {
                new ComboBoxItemModel {Value ="",Text ="无" },
                new ComboBoxItemModel {Value =".SH",Text ="上海" },
                new ComboBoxItemModel {Value =".SZ",Text ="深圳" },
            };

            this.cbTradeMarket.Initialize(markets);
            this.cbTradeMarket.SelectedIndex = 0;

            if (this._isEdit)
            {
                var stockInfo = _stockService.GetStockInfoById(_stockId);

                if (stockInfo == null) return;

                this.txtStockCode.Text = CommonHelper.StockCodeZerofill(stockInfo.Code.ToString());
                this.txtStockName.Text = stockInfo.Name;

                if (stockInfo.FullCode.IndexOf(".SH") > 0)
                    this.cbTradeMarket.DefaultSelected(".SH");
                else if (stockInfo.FullCode.IndexOf(".SZ") > 0)
                    this.cbTradeMarket.DefaultSelected(".SZ");

                this.memoRemarks.Text = stockInfo.Remarks;
            }
        }

        private void SetControlProperties()
        {
            this.txtStockCode.Properties.MaxLength = 6;
            this.txtStockName.Properties.MaxLength = 20;
            this.memoRemarks.Properties.MaxLength = 200;
        }

        private bool InputCheck()
        {
            if (string.IsNullOrEmpty(this.txtStockCode.Text.Trim()))
            {
                DXMessage.ShowTips("股票代码不能为空！");
                this.txtStockCode.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(this.txtStockName.Text.Trim()))
            {
                DXMessage.ShowTips("股票名称不能为空！");
                this.txtStockName.Focus();
                return false;
            }

            return true;
        }

        #endregion Utilities

        #region Events

        private void _dialogStockEdit_Load(object sender, EventArgs e)
        {
            this._isEdit = this._stockId > 0 ? true : false;

            SetControlProperties();

            BindStockInfo();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (!InputCheck()) return;

            //编辑的场合
            if (this._isEdit)
            {
                var stockModel = _stockService.GetStockInfoById(_stockId);

                if (stockModel.Code != this.txtStockCode.Text.Trim())
                {
                    var sameCodeStock = _stockService.GetStockInfoByCode(this.txtStockCode.Text.Trim());
                    if (sameCodeStock != null)
                    {
                        DXMessage.ShowTips("该股票代码已经存在，无法修改！");
                        this.txtStockCode.Focus();
                        return;
                    }
                }

                if (stockModel.Name != this.txtStockName.Text.Trim())
                {
                    var sameNameStock = _stockService.GetStockInfoByName(this.txtStockName.Text.Trim());
                    if (sameNameStock != null)
                    {
                        DXMessage.ShowTips("该股票名称已经存在，无法修改！");
                        this.txtStockName.Focus();
                        return;
                    }
                }

                stockModel.Code = this.txtStockCode.Text.Trim();
                stockModel.Name = this.txtStockName.Text.Trim();
                stockModel.FullCode = stockModel.Code + this.cbTradeMarket.SelectedValue();
                stockModel.Remarks = this.memoRemarks.Text.Trim();
                stockModel.IsDeleted = false;

                _stockService.UpdateStockInfo(stockModel);
            }
            //添加的场合
            else
            {
                var stockModel = new StockInfo
                {
                    Code = this.txtStockCode.Text.Trim(),
                    Name = this.txtStockName.Text.Trim(),
                    FullCode = this.txtStockCode.Text.Trim() + this.cbTradeMarket.SelectedValue(),
                    Remarks = this.memoRemarks.Text.Trim(),
                    IsDeleted = false,
                };

                var sameCodeStock = _stockService.GetStockInfoByCode(stockModel.Code);
                if (sameCodeStock != null)
                {
                    DXMessage.ShowTips("该股票代码已经存在，无法添加！");
                    this.txtStockCode.Focus();
                    return;
                }

                var sameNameStock = _stockService.GetStockInfoByName(stockModel.Name);
                if (sameNameStock != null)
                {
                    DXMessage.ShowTips("该股票名称已经存在，无法添加！");
                    this.txtStockName.Focus();
                    return;
                }

                _stockService.AddStockInfo(stockModel);
            }

            RefreshEvent?.Invoke();

            this.Close();
        }

        #endregion Events
    }
}