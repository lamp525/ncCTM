using System;
using System.Collections.Generic;
using System.Linq;
using CTM.Core.Domain.TradeRecord;
using CTM.Core.Util;
using CTM.Services.Common;
using CTM.Services.TradeRecord;
using CTM.Win.Models;
using CTM.Win.Util;

namespace CTM.Win.UI.Function.DataManage
{
    public partial class _dialogDailyRecordSplit : BaseForm
    {
        #region Fields

        private readonly IDailyRecordService _dailyRecordService;
        private readonly ICommonService _commonService;

        private TradeRecordModel _record;

        #endregion Fields

        #region Properties

        public TradeRecordModel Record
        {
            set { this._record = value; }
        }

        #endregion Properties

        #region Delegates

        public delegate void RefreshParentForm();

        public event RefreshParentForm RefreshEvent;

        #endregion Delegates

        #region Constructors

        public _dialogDailyRecordSplit(IDailyRecordService dailyRecordService, ICommonService commonService)
        {
            InitializeComponent();

            this._dailyRecordService = dailyRecordService;
            this._commonService = commonService;
        }

        #endregion Constructors

        #region Utilities

        private void BindRecordInfo()
        {
            if (this._record == null) return;

            this.txtTradeDate.Text = this._record.TradeDate.ToString("yyyy-MM-dd");
            this.txtTradeTime.Text = this._record.TradeTime;
            this.txtStockCode.Text = this._record.StockCode;
            this.txtStockName.Text = this._record.StockName;
            this.txtTradeType.Text = this._record.TradeTypeName;
            this.txtDealFlag.Text = this._record.DealFlagName;
            this.txtOperator.Text = this._record.OperatorName;
            this.txtBeneficiary.Text = this._record.BeneficiaryName;
            this.txtDealVolume.Text = Math.Abs(this._record.DealVolume).ToString();
            this.txtDealPrice.Text = this._record.DealPrice.ToString();
            this.txtSplitVolume.Text = string.Empty;
            this.txtSplitNo.Text = string.IsNullOrEmpty(this._record.SplitNo) ? this._record.RecordId.ToString() + "-s" : this._record.SplitNo + "-s";

            this.ActiveControl = this.txtSplitVolume;
        }

        private void SplitProcess()
        {
            var splitVolume = int.Parse(this.txtSplitVolume.Text.Trim());
            var splitRate = (decimal)splitVolume / Math.Abs(this._record.DealVolume);

            var splitRecord = new DailyRecord
            {
                AccountId = this._record.AccountId,
                ActualAmount = CommonHelper.SetDecimalDigits(this._record.ActualAmount * splitRate, 4),
                AuditFlag = this._record.AuditFlag,
                AuditNo = this._record.AuditNo,
                AuditTime = this._record.AuditTime,
                Beneficiary = this._record.Beneficiary,
                Commission = CommonHelper.SetDecimalDigits(this._record.Commission * splitRate, 4),
                ContractNo = this._record.ContractNo,
                DataType = this._record.DataType,
                DealAmount = CommonHelper.SetDecimalDigits(this._record.DealAmount * splitRate, 4),
                DealFlag = this._record.DealFlag,
                DealNo = this._record.DealNo,
                DealPrice = this._record.DealPrice,
                DealVolume = this._record.DealFlag == true ? splitVolume : 0 - splitVolume,
                ImportTime = this._record.ImportTime,
                ImportUser = this._record.ImportUser,
                Incidentals = CommonHelper.SetDecimalDigits(this._record.Incidentals * splitRate, 4),
                OperatorCode = this._record.OperatorCode,
                Remarks = this._record.Remarks,
                SplitNo = this.txtSplitNo.Text.Trim(),
                StampDuty = CommonHelper.SetDecimalDigits(this._record.StampDuty * splitRate, 4),
                StockCode = this._record.StockCode,
                StockHolderCode = this._record.StockHolderCode,
                StockName = this._record.StockName,
                TradeDate = this._record.TradeDate,
                TradeTime = this._record.TradeTime,
                TradeType = this._record.TradeType,
                UpdateTime = this._commonService.GetCurrentServerTime(),
                UpdateUser = LoginInfo.CurrentUser.UserCode,
            };

            var orginalRecord = this._dailyRecordService.GetDailyRecordById(this._record.RecordId);

            decimal orginalRate = 1 - splitRate;
            orginalRecord.ActualAmount = CommonHelper.SetDecimalDigits(this._record.ActualAmount * orginalRate, 4);
            orginalRecord.Commission = CommonHelper.SetDecimalDigits(this._record.Commission * orginalRate, 4);
            orginalRecord.DealAmount = CommonHelper.SetDecimalDigits(this._record.DealAmount * orginalRate, 4);
            orginalRecord.DealVolume = this._record.DealFlag == true ? this._record.DealVolume - splitVolume : this._record.DealVolume + splitVolume;
            orginalRecord.Incidentals = CommonHelper.SetDecimalDigits(this._record.Incidentals * orginalRate, 4);
            orginalRecord.SplitNo = string.IsNullOrEmpty(this._record.SplitNo) ? this._record.RecordId.ToString() : this._record.SplitNo;
            orginalRecord.StampDuty = CommonHelper.SetDecimalDigits(this._record.StampDuty * orginalRate, 4);
            orginalRecord.UpdateTime = this._commonService.GetCurrentServerTime();
            orginalRecord.UpdateUser = LoginInfo.CurrentUser.UserCode;

            //插入拆分出的新交易记录
            this._dailyRecordService.InsertDailyRecord(splitRecord);

            //更新原始交易记录
            this._dailyRecordService.UpdateDailyRecord(orginalRecord);
        }

        #endregion Utilities

        #region Events

        private void _dialogDailyRecordSplit_Load(object sender, EventArgs e)
        {
            BindRecordInfo();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnOk.Enabled = false;

                if (this.txtSplitVolume.Text.Trim().Length == 0 || int.Parse(this.txtSplitVolume.Text.Trim()) < 1 || int.Parse(this.txtSplitVolume.Text.Trim()) >= Math.Abs(this._record.DealVolume))
                {
                    DXMessage.ShowTips(string.Format("拆单数量应该为 0 ~ {0} 之间！", Math.Abs(this._record.DealVolume)));
                    this.txtSplitVolume.Focus();
                    return;
                }

                if (DXMessage.ShowYesNoAndTips("确定进行本次拆单操作么？") == System.Windows.Forms.DialogResult.Yes)
                {
                    //交易记录拆单处理
                    SplitProcess();

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
            }
        }

        #endregion Events
    }
}