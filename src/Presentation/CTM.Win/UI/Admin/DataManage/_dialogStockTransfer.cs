using System;
using System.Collections.Generic;
using System.Linq;
using CTM.Core;
using CTM.Core.Domain.Stock;
using CTM.Core.Domain.TradeRecord;
using CTM.Core.Domain.User;
using CTM.Core.Util;
using CTM.Services.Account;
using CTM.Services.Common;
using CTM.Services.Dictionary;
using CTM.Services.Stock;
using CTM.Services.TradeRecord;
using CTM.Services.User;
using CTM.Win.Extensions;
using CTM.Win.Models;
using CTM.Win.Util;

namespace CTM.Win.UI.Admin.DataManage
{
    public partial class _dialogStockTransfer : BaseForm
    {
        #region Fields

        private readonly IDailyRecordService _tradeRecordService;
        private readonly IUserService _userService;
        private readonly IDictionaryService _dictionaryService;
        private readonly IStockService _stockService;
        private readonly ICommonService _commonService;
        private readonly IAccountService _accountService;

        private StockPositionModel _record;

        private const string _layoutXmlName = "_dialogDailyRecordEdit";

        #endregion Fields

        #region Properties

        public StockPositionModel Record
        {
            get { return this._record ?? (new StockPositionModel()); }
            set { this._record = value; }
        }

        #endregion Properties

        #region Delegates

        public delegate void RefreshParentForm();

        public event RefreshParentForm RefreshEvent;

        #endregion Delegates

        #region Constructors

        public _dialogStockTransfer(
           IDailyRecordService tradeRecordService,
           IUserService userService,
           IDictionaryService dictionaryService,
           IStockService stockService,
           ICommonService commonService,
           IAccountService accountService
           )
        {
            InitializeComponent();

            this._tradeRecordService = tradeRecordService;
            this._dictionaryService = dictionaryService;
            this._userService = userService;
            this._stockService = stockService;
            this._commonService = commonService;
            this._accountService = accountService;
        }

        #endregion Constructors

        #region Utilities

        private void BindTransferInfo()
        {
            if (_record == null) _record = new StockPositionModel();

            this.txtDealerFrom.Text = _record.DealerName;
            this.txtAccountName.Text = _record.AccountName;
            this.txtSecurityCompanyName.Text = _record.SecurityCompanyName;
            this.txtAttributeName.Text = _record.AttributeName;
            this.txtStockCode.Text = _record.StockFullCode;
            this.txtStockName.Text = _record.StockName;
            this.txtHoldingVolume.Text = _record.StockHoldingVolume.ToString();
            this.txtCurrentPrice.Text = _record.CurrentPrice.ToString();

            this.txtTransferPrice.Text = this.txtCurrentPrice.Text;
            this.txtTransferVolume.EditValue = this.txtHoldingVolume.Text;

            var dealers = _userService.GetAllDealer(showDeleted: false).Where(x => x.Code != _record.DealerCode).OrderBy(x => x.Code).ToList();

            this.luReceiver.Initialize(dealers, "Code", "Name", enableSearch: true);
        }

        private bool CheckInput()
        {
            if (string.IsNullOrEmpty(this.luReceiver.SelectedValue()))
            {
                DXMessage.ShowTips("请选择接收人员！");
                return false;
            }

            if (string.IsNullOrEmpty(this.luReceivedAccount.SelectedValue()))
            {
                DXMessage.ShowTips("请选择接收账户！");
                return false;
            }

            if (this.txtTransferVolume.Text.Length == 0)
            {
                DXMessage.ShowTips("请输入转移数量！");
                return false;
            }

            var holdingVolume = int.Parse(this.txtHoldingVolume.Text.Trim());
            var transferVolume = int.Parse(this.txtTransferVolume.Text.Trim());

            if (holdingVolume > 0)
            {
                if (transferVolume < 1 || transferVolume > holdingVolume)
                {
                    DXMessage.ShowTips(string.Format("转移数量为 1 ~ {0} 之间！", holdingVolume));
                    return false;
                }
            }
            else
            {
                if (transferVolume > -1 || transferVolume < holdingVolume)
                {
                    DXMessage.ShowTips(string.Format("转移数量为 {0} ~ -1 之间！", holdingVolume));
                    return false;
                }
            }

            if (this.txtTransferPrice.Text.Length == 0)
            {
                DXMessage.ShowTips("请输入转移价格！");
                return false;
            }

            if (decimal.Parse(this.txtTransferPrice.Text.Trim()) <= 0)
            {
                DXMessage.ShowTips("转移价格必须大于0！");
                return false;
            }

            return true;
        }

        private void TransferProcess()
        {
            var holderAccountId = _record.AccountId;
            var holderAccountInfo = _record.AccountName + "-" + _record.SecurityCompanyName + "-" + _record.AttributeName;
            var holder = _record.DealerCode;
            var holderName = _record.DealerName;
            var holderTradeType = CTMHelper.GetTradeTypeByDepartment(_record.DepartmentId);

            var receiver = this.luReceiver.GetSelectedDataRow() as UserInfo;
            if (receiver == null) return;

            var receiverTradeType = CTMHelper.GetTradeTypeByDepartment(receiver.DepartmentId);
            var transferVolume = int.Parse(this.txtTransferVolume.Text.Trim());
            var transferPrice = decimal.Parse(this.txtTransferPrice.Text.Trim());
            var stockFullCode = this.txtStockCode.Text.Trim();
            var stockName = this.txtStockName.Text.Trim();
            var receivedAccountId = int.Parse(this.luReceivedAccount.SelectedValue());
            var receivedAccountInfo = (this.luReceivedAccount.GetSelectedDataRow() as AccountEntity).DisplayMember;

            var now = _commonService.GetCurrentServerTime();
            var tradeDate = CommonHelper.GetPreviousWorkDay(now);

            var stockTransferInfo = new StockTransferInfo()
            {
                HolderAccountId = holderAccountId,
                HolderAccountInfo = holderAccountInfo,
                ReceivedAccountId = receivedAccountId,
                ReceivedAccountInfo = receivedAccountInfo,
                StockFullCode = stockFullCode,
                StockName = stockName,
                Holder = holder,
                HolderName = holderName,
                Receiver = receiver.Code,
                ReceiverName = receiver.Name,
                OperatorCode = LoginInfo.CurrentUser.UserCode,
                OperatorName = LoginInfo.CurrentUser.UserName,
                TransferVolume = transferVolume,
                TransferPrice = transferPrice,
                TransferTime = now,
            };

            var holderRecord = new DailyRecord()
            {
                DataType = (int)EnumLibrary.DataType.StockTransfer,
                AccountId = holderAccountId,
                ActualAmount = transferPrice * transferVolume,
                Beneficiary = holder,
                DealAmount = Math.Abs(transferPrice * transferVolume),
                DealFlag = transferVolume > 0 ? false : true,
                DealPrice = transferPrice,
                DealVolume = -transferVolume,
                ImportTime = now,
                ImportUser = LoginInfo.CurrentUser.UserCode,
                OperatorCode = holder,
                StockCode = stockFullCode,
                StockName = stockName,
                TradeDate = tradeDate,
                TradeTime = now.ToString("HH:mm:ss"),
                TradeType = (int)holderTradeType,
                UpdateTime = now,
                UpdateUser = LoginInfo.CurrentUser.UserCode,
            };

            var receiverRecord = new DailyRecord()
            {
                DataType = (int)EnumLibrary.DataType.StockTransfer,
                AccountId = receivedAccountId,
                ActualAmount = transferPrice * (-transferVolume),
                Beneficiary = receiver.Code,
                DealAmount = Math.Abs(transferPrice * transferVolume),
                DealFlag = transferVolume < 0 ? false : true,
                DealPrice = transferPrice,
                DealVolume = transferVolume,
                ImportTime = now,
                ImportUser = LoginInfo.CurrentUser.UserCode,
                OperatorCode = receiver.Code,
                StockCode = stockFullCode,
                StockName = stockName,
                TradeDate = tradeDate,
                TradeTime = now.ToString("HH:mm:ss"),
                TradeType = (int)receiverTradeType,
                UpdateTime = now,
                UpdateUser = LoginInfo.CurrentUser.UserCode,
            };

            _stockService.AddStockTransferInfo(stockTransferInfo);

            _tradeRecordService.InsertDailyRecords(new List<DailyRecord> { holderRecord, receiverRecord });

            var stockTransferRecords = new List<StockTransferRecord>
            {
                new StockTransferRecord { TransferId =stockTransferInfo.Id, RecordId = holderRecord.Id },
                new StockTransferRecord {TransferId =stockTransferInfo.Id , RecordId = receiverRecord .Id  }
            };

            _stockService.AddStockTransferRecord(stockTransferRecords);
        }

        #endregion Utilities

        #region Events

        private void _dialogStockTransfer_Load(object sender, EventArgs e)
        {
            this.txtCurrentPrice.SetNumericMask();
            this.txtTransferPrice.SetNumericMask();
            this.txtTransferVolume.Properties.MaxLength = 18;

            this.ActiveControl = this.luReceiver;

            BindTransferInfo();
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnOk.Enabled = false;
                this.btnReturn.Enabled = false;

                if (!CheckInput()) return;

                if (DXMessage.ShowYesNoAndTips("确定进行本次转移操作么？") == System.Windows.Forms.DialogResult.Yes)
                {
                    //股票转移处理
                    TransferProcess();

                    this.RefreshEvent?.Invoke();

                    this.Close();
                }
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
            finally
            {
                this.btnOk.Enabled = true;
                this.btnReturn.Enabled = true;
            }
        }

        private void luReceiver_EditValueChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.luReceiver.SelectedValue())) return;

            var receiverInfo = this.luReceiver.GetSelectedDataRow() as UserInfo;

            var accountIds = _accountService.GetAccountIdByOperatorId(receiverInfo.Id);

            if (accountIds == null || !accountIds.Any())
            {
                DXMessage.ShowTips(string.Format("接收人员【{0}】尚未设置操作账号！", receiverInfo.Name));
                return;
            }

            var accountInfos = _accountService.GetAccountInfos(accountIds: accountIds.ToArray())
                  .Select(x => new AccountEntity
                  {
                      AccountId = x.Id,
                      AccountName = x.Name,
                      AttributeName = x.AttributeName,
                      SecurityCompanyName = x.SecurityCompanyName,
                      DisplayMember = x.Name + " - " + x.SecurityCompanyName + " - " + x.AttributeName,
                  }
                 )
                .ToList();

            this.luReceivedAccount.Initialize(accountInfos, "AccountId", "DisplayMember", showHeader: true, enableSearch: true);
        }

        #endregion Events
    }
}