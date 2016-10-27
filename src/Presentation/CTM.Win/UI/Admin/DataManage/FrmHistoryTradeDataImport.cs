using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using CTM.Core;
using CTM.Core.Domain.Stock;
using CTM.Core.Domain.TradeRecord;
using CTM.Core.Domain.User;
using CTM.Core.Util;
using CTM.Data;
using CTM.Services.Account;
using CTM.Services.Common;
using CTM.Services.Stock;
using CTM.Services.TradeRecord;
using CTM.Services.User;
using CTM.Win.Models;
using CTM.Win.Util;

namespace CTM.Win.UI.Admin.DataManage
{
    public partial class FrmHistoryTradeDataImport : BaseForm
    {
        #region Fields

        private static readonly string _oldSystemConnectionString = "";
        private static readonly string _currentSystemConnectionString = "";

        private readonly IDailyRecordService _tradeRecordService;
        private readonly IUserService _userService;
        private readonly IAccountService _accountService;
        private readonly IStockService _stockService;
        private readonly ICommonService _commonService;

        private IList<UserInfo> _userInfos;
        private IList<StockInfo> _stockInfos;

        #endregion Fields

        #region Constructors

        public FrmHistoryTradeDataImport(
            IDailyRecordService tradeRecordService,
            IUserService userService,
            IAccountService accountService,
            IStockService stockService,
            ICommonService commonService
            )
        {
            InitializeComponent();

            this._accountService = accountService;
            this._stockService = stockService;
            this._userService = userService;
            this._tradeRecordService = tradeRecordService;
            this._commonService = commonService;
        }

        #endregion Constructors

        #region Utilities

        private string GetUserCodeByName(string name)
        {
            if (string.IsNullOrEmpty(name)) return null;

            var info = _userInfos.Where(x => x.Name == name).SingleOrDefault();

            if (info == null) return null;

            return info.Code;
        }

        private string GetStockFullCodeByCode(string code)
        {
            if (string.IsNullOrEmpty(code)) return null;

            code = CommonHelper.StockCodeZerofill(code);
            var info = _stockInfos.Where(x => x.Code == code).SingleOrDefault();

            if (info == null) return null;

            return info.FullCode;
        }

        private string GetBeneficiaryCodeByTradeType(DataRow historyRecordDataRow, int tradeType)
        {
            if (historyRecordDataRow == null)
                throw new ArgumentNullException("historyRecordDataRow");

            var beneficiary = string.Empty;

            switch (tradeType)
            {
                case (int)EnumLibrary.TradeType.Day:
                    beneficiary = historyRecordDataRow["FRN"].ToString().Trim();
                    break;

                case (int)EnumLibrary.TradeType.Band:
                    beneficiary = historyRecordDataRow["FBD"].ToString().Trim();
                    break;

                case (int)EnumLibrary.TradeType.Target:
                    beneficiary = historyRecordDataRow["FManager"].ToString().Trim();
                    break;
            }

            var beneficiaryCode = GetUserCodeByName(beneficiary);

            return beneficiaryCode;
        }

        private bool DataImportProcess(out int importRecordNumber)
        {
            importRecordNumber = 0;

            var startDate = CommonHelper.StringToDateTime(this.deStart.EditValue.ToString());
            var endDate = CommonHelper.StringToDateTime(this.deEnd.EditValue.ToString());

            string query = string.Format(@"select * from EntrustRecord where (cjQty>0 or cjAmount >0)  and FData >='{0}' and FData <='{1}' ", startDate, endDate);

            var ds = SqlHelper.ExecuteDataset(_oldSystemConnectionString, CommandType.Text, query);

            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0) return true;

            var tradeRecords = new List<DailyRecord>();
            var historyRecords = ds.Tables[0];

            var dataType = (int)EnumLibrary.DataType.History;
            var importUserCode = LoginInfo.CurrentUser.UserCode;
            var importTime = _commonService.GetCurrentServerTime();

            foreach (DataRow row in historyRecords.Rows)
            {
                importRecordNumber++;

                var tradeRecord = new DailyRecord();

                tradeRecord.DataType = dataType;
                tradeRecord.ImportUser = importUserCode;
                tradeRecord.ImportTime = importTime;
                tradeRecord.UpdateUser = importUserCode;
                tradeRecord.UpdateTime = importTime;

                tradeRecord.AccountId = int.Parse(row["zqzh"].ToString().Trim());

                tradeRecord.OperatorCode = GetUserCodeByName(row["Operator"].ToString().Trim());

                tradeRecord.StockName = row["zqmc"].ToString().Trim();

                tradeRecord.StockCode = GetStockFullCodeByCode(row["zqdm"].ToString().Trim());

                if (string.IsNullOrEmpty(tradeRecord.StockCode))
                {
                    DXMessage.ShowTips(string.Format("系统不存在股票信息【{0}】【{1}】，导入操作终止！", row["zqmc"].ToString().Trim(), row["zqdm"].ToString().Trim()));
                    return false;
                }

                tradeRecord.SetTradeType(row["jyType"].ToString().Trim());

                tradeRecord.Beneficiary = GetBeneficiaryCodeByTradeType(row, tradeRecord.TradeType);

                tradeRecord.TradeDate = CommonHelper.StringToDateTime(row["FData"].ToString().Trim());

                tradeRecord.TradeTime = row["Ftime"].ToString().Trim();

                if (row["cz"].ToString().Trim().IndexOf("买") > -1)
                    tradeRecord.DealFlag = true;
                else
                    tradeRecord.DealFlag = false;

                tradeRecord.DealNo = string.Empty;

                tradeRecord.DealPrice = decimal.Parse(row["cjPrice"].ToString().Trim());

                tradeRecord.DealAmount = decimal.Parse(row["cjAmount"].ToString().Trim());

                //买入
                if (tradeRecord.DealFlag)
                {
                    tradeRecord.DealVolume = CommonHelper.ConvertToPositive(int.Parse(row["cjQty"].ToString().Trim()));

                    tradeRecord.ActualAmount = CommonHelper.ConvertToNegtive(decimal.Parse(row["fsAmount"].ToString().Trim()));
                }
                //卖出
                else
                {
                    tradeRecord.DealVolume = CommonHelper.ConvertToNegtive(int.Parse(row["cjQty"].ToString().Trim()));

                    tradeRecord.ActualAmount = CommonHelper.ConvertToPositive(decimal.Parse(row["fsAmount"].ToString().Trim()));
                }

                tradeRecord.StockHolderCode = row["gdzh"].ToString().Trim();

                tradeRecord.ContractNo = row["ContractID"].ToString().Trim();

                if (row["yj"] != DBNull.Value)
                    tradeRecord.Commission = decimal.Parse(row["yj"].ToString().Trim());

                if (row["yhs"] != DBNull.Value)
                    tradeRecord.StampDuty = decimal.Parse(row["yhs"].ToString().Trim());

                if (row["ghf"] != DBNull.Value)
                    tradeRecord.Incidentals = decimal.Parse(row["ghf"].ToString().Trim());

                tradeRecord.AuditFlag = row["gj"].ToString().Trim() == "1" ? true : false;

                if (row["gjtime"] != DBNull.Value)
                    tradeRecord.AuditTime = CommonHelper.StringToDateTime(row["gjtime"].ToString().Trim());

                tradeRecord.AuditNo = null;

                tradeRecord.Remarks = row["remark"].ToString().Trim();

                tradeRecords.Add(tradeRecord);
            }

            var tradeDateFrom = historyRecords.AsEnumerable().Select(x => x.Field<DateTime>("FData")).Min();
            var tradeDateTo = historyRecords.AsEnumerable().Select(x => x.Field<DateTime>("FData")).Max();

            if (DeleteExistedRecords(tradeDateFrom, tradeDateTo))
                _tradeRecordService.BatchInsertDailyRecords(tradeRecords);

            return true;
        }

        private bool DeleteExistedRecords(DateTime tradeDateFrom, DateTime tradeDateTo)
        {
            var query = string.Format(@"delete from DailyRecord where TradeDate >='{0}' and TradeDate <='{1}'  ", tradeDateFrom, tradeDateTo);

            SqlHelper.ExecuteNonQuery(_currentSystemConnectionString, CommandType.Text, query);

            return true;
        }

        #endregion Utilities

        #region Events

        private void FrmHistoryTradeDataImport_Load(object sender, EventArgs e)
        {
            this.deStart.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.deEnd.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.deStart.EditValue = new DateTime(2016, 01, 01);
            this.deEnd.EditValue = DateTime.Now.Date;

            _stockInfos = _stockService.GetAllStocks(showDeleted: true);
            _userInfos = _userService.GetAllUsers(true);
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            this.btnImport.Enabled = false;
            this.btnCancel.Enabled = false;

            if (DXMessage.ShowYesNoAndTips("导入操作将清除系统中该时间段内的已有数据，是否确定继续导入？") == DialogResult.No) return;

            try
            {
                Stopwatch myWatch = new Stopwatch();
                myWatch.Start();

                var importRecordNumer = 0;
                var result = DataImportProcess(out importRecordNumer);

                myWatch.Stop();

                long proceedTime = myWatch.ElapsedMilliseconds;

                if (result)
                    DXMessage.ShowTips(string.Format("历史数据导入成功！共导入{0}条数据，耗时：{1}", importRecordNumer, CommonHelper.FormatMillisecondsToString(proceedTime)));

                this.Close();
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
            finally
            {
                this.btnImport.Enabled = true;
                this.btnCancel.Enabled = true;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion Events
    }
}