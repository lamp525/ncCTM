using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using CTM.Core;
using CTM.Core.Data;
using CTM.Core.Domain.InvestmentDecision;
using CTM.Core.Util;
using CTM.Data;
using CTM.Services.Common;

namespace CTM.Services.InvestmentDecision
{
    public partial class InvestmentDecisionService : IInvestmentDecisionService
    {
        #region Fields

        private readonly IRepository<CloseStockAnalysisInfo> _CSAInfoRepository;
        private readonly IRepository<CloseStockAnalysisDetail> _CSADetailRepository;

        private readonly IRepository<InvestmentDecisionCommittee> _IDCRepository;
        private readonly IRepository<InvestmentDecisionForm> _IDFRepository;
        private readonly IRepository<InvestmentDecisionVote> _IDVRepository;

        private readonly IRepository<InvestmentDecisionStockPool> _IDStockPoolRepository;

        private readonly IRepository<MarketTrendForecastInfo> _MTFInfoRepository;
        private readonly IRepository<MarketTrendForecastDetail> _MTFDetailRepository;

        private readonly ICommonService _commonService;

        private readonly IDbContext _dbContext;

        #endregion Fields

        #region Constructors

        public InvestmentDecisionService(
            IRepository<CloseStockAnalysisInfo> CSAInfoRepository,
            IRepository<CloseStockAnalysisDetail> CSADetailRepository,
            IRepository<InvestmentDecisionCommittee> IDCRepository,
            IRepository<InvestmentDecisionForm> IDFRepository,
            IRepository<InvestmentDecisionVote> IDVRepository,
            IRepository<InvestmentDecisionStockPool> IDStockPoolRepository,
            IRepository<MarketTrendForecastInfo> MTFInfoRepository,
            IRepository<MarketTrendForecastDetail> MTFDetailRepository,
            ICommonService commonService,
            IDbContext dbContext)
        {
            this._CSAInfoRepository = CSAInfoRepository;
            this._CSADetailRepository = CSADetailRepository;
            this._IDCRepository = IDCRepository;
            this._IDFRepository = IDFRepository;
            this._IDVRepository = IDVRepository;
            this._IDStockPoolRepository = IDStockPoolRepository;
            this._MTFInfoRepository = MTFInfoRepository;
            this._MTFDetailRepository = MTFDetailRepository;

            this._commonService = commonService;
            this._dbContext = dbContext;
        }

        #endregion Constructors

        #region Utilities

        private void UpdateCommitteeWeight()
        {
            var committees = _IDCRepository.Table.ToList();

            if (committees.Any())
            {
                foreach (var item in committees)
                {
                    item.Weight = CommonHelper.SetDecimalDigits(1.000000M / committees.Count, 4);
                }
                _IDCRepository.Update(committees);
            }
        }

        #endregion Utilities

        #region Methods

        public virtual string GenerateIDFSerialNo(DateTime applyDate)
        {
            var serialNo = "SQ" + applyDate.ToString("yyMMdd");

            var info = _IDFRepository.Table.Where(x => x.ApplyDate == applyDate).OrderBy(x => x.CreateTime).ToList().LastOrDefault();

            var suffix = string.Empty;
            if (info == null)
                suffix = "001";
            else
            {
                var lastSuffix = info.SerialNo.Substring(info.SerialNo.Length - 3, 3);
                suffix = (int.Parse(lastSuffix) + 1).ToString("00#");
            }

            serialNo = serialNo + suffix;
            return serialNo;
        }

        public virtual void SubmitInvestmentDecisionApplication(InvestmentDecisionForm entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            if (_IDCRepository.Table.Count() == 0)
                throw new Exception("请设置投资决策委员会成员！");

            decimal totalWeight = 1;
            decimal applyUserWeight = 0;
            decimal otherWeight = 0;

            var committeeCodes = _IDCRepository.Table.Select(x => x.Code).ToList();

            if (committeeCodes.Contains(entity.ApplyUser))
            {
                applyUserWeight = 0.35M;
                otherWeight = CommonHelper.SetDecimalDigits((totalWeight - applyUserWeight), 4) / (committeeCodes.Count - 1);
            }
            else
            {
                applyUserWeight = 0;
                otherWeight = CommonHelper.SetDecimalDigits(totalWeight, 4) / committeeCodes.Count;

                committeeCodes.Add(entity.ApplyUser);
            }

            entity.Point = (int)(applyUserWeight * 100);
            _IDFRepository.Insert(entity);

            var defaultVoteInfos = new List<InvestmentDecisionVote>();
            foreach (var code in committeeCodes)
            {
                var info = new InvestmentDecisionVote
                {
                    AuthorityLevel = 0,
                    Flag = code == entity.ApplyUser ? (int)EnumLibrary.IDVoteFlag.Approval : (int)EnumLibrary.IDVoteFlag.None,
                    FormSerialNo = entity.SerialNo,
                    Reason = code == entity.ApplyUser ? entity.Reason : string.Empty,
                    Type = code == entity.ApplyUser ? (int)EnumLibrary.IDVoteType.Applicant : (int)EnumLibrary.IDVoteType.Committee,
                    UserCode = code,
                    VoteTime = _commonService.GetCurrentServerTime(),
                    Weight = code == entity.ApplyUser ? applyUserWeight : otherWeight,
                };

                defaultVoteInfos.Add(info);
            }
            _IDVRepository.Insert(defaultVoteInfos);
        }

        public virtual void DeleteInvestmentDecisionForm(string serialNo)
        {
            if (string.IsNullOrEmpty(serialNo))
                throw new NotImplementedException();

            var info = _IDFRepository.Table.SingleOrDefault(x => x.SerialNo == serialNo);

            _IDFRepository.Delete(info);

            var votes = _IDVRepository.Table.Where(x => x.FormSerialNo == serialNo);

            _IDVRepository.Delete(votes.ToArray());
        }

        public virtual void DeleteInvestmentDecisionForm(IList<string> serialNos)
        {
            if (serialNos == null)
                throw new ArgumentNullException(nameof(serialNos));

            var forms = _IDFRepository.Table.Where(x => serialNos.Contains(x.SerialNo));

            _IDFRepository.Delete(forms.ToArray());

            var votes = _IDVRepository.Table.Where(x => serialNos.Contains(x.FormSerialNo));

            _IDVRepository.Delete(votes.ToArray());
        }

        public virtual InvestmentDecisionVote GetInvestmentDecisionVote(string investorCode, string formSerialNo)
        {
            if (investorCode == null)
                throw new ArgumentNullException(nameof(investorCode));

            if (formSerialNo == null)
                throw new ArgumentNullException(nameof(formSerialNo));

            var info = _IDVRepository.Table.SingleOrDefault(x => x.UserCode == investorCode && x.FormSerialNo == formSerialNo);

            return info;
        }

        public virtual void AddInvestmentDecisionVote(InvestmentDecisionVote entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _IDVRepository.Insert(entity);
        }

        public virtual void DeleteInvestmentDecisionVote(string investorCode, string formSerialNo)
        {
            if (investorCode == null)
                throw new ArgumentNullException(nameof(investorCode));

            if (formSerialNo == null)
                throw new ArgumentNullException(nameof(formSerialNo));

            var info = _IDVRepository.Table.SingleOrDefault(x => x.UserCode == investorCode && x.FormSerialNo == formSerialNo);

            _IDVRepository.Delete(info);
        }

        public virtual void UpdateInvestmentDecisionVote(InvestmentDecisionVote entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _IDVRepository.Update(entity);
        }

        public virtual IList<InvestmentDecisionVote> GetInvestmentDecisionVotes(string investorCode)
        {
            var query = _IDVRepository.TableNoTracking.Where(x => x.UserCode == investorCode && x.Flag != (int)EnumLibrary.IDVoteFlag.None);

            return query.ToList();
        }

        public virtual void InvestmentDecisionVoteProcess(string investorCode, string formSerialNo, EnumLibrary.IDVoteFlag flag, string reason)
        {
            if (string.IsNullOrEmpty(reason))
                reason = @"";

            reason = reason.Replace("'", "''");

            var commanText = $@"EXEC [dbo].[sp_InvestmentDecisionVoteProcess]
                                        @InvestorCode = '{investorCode}',
		                                @FormSerialNo = '{formSerialNo}',
		                                @VoteFlag = {(int)flag},
		                                @Reason =N'{@reason}'";

            _dbContext.ExecuteSqlCommand(commanText);
        }

        public virtual MarketTrendForecastDetail GetMTFDetail(string investorCode, string serialNo)
        {
            var detail = _MTFDetailRepository.Table.SingleOrDefault(x => x.InvestorCode == investorCode && x.SerialNo == serialNo);

            return detail;
        }

        public virtual void UpdateMTFDetail(MarketTrendForecastDetail entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _MTFDetailRepository.Update(entity);
        }

        public void DeleteMTFInfo(string serialNo)
        {
            if (string.IsNullOrEmpty(serialNo))
                throw new NotImplementedException();

            var info = _MTFInfoRepository.Table.SingleOrDefault(x => x.SerialNo == serialNo);

            _MTFInfoRepository.Delete(info);

            var votes = _MTFDetailRepository.Table.Where(x => x.SerialNo == serialNo);

            _MTFDetailRepository.Delete(votes.ToArray());
        }

        public void DeleteCSAInfo(string serialNo)
        {
            if (string.IsNullOrEmpty(serialNo))
                throw new NotImplementedException();

            var info = _CSAInfoRepository.Table.SingleOrDefault(x => x.SerialNo == serialNo);

            _CSAInfoRepository.Delete(info);

            var votes = _CSADetailRepository.Table.Where(x => x.SerialNo == serialNo);

            _CSADetailRepository.Delete(votes.ToArray());
        }

        public CloseStockAnalysisDetail GetCSADetailById(int id)
        {
            var detail = _CSADetailRepository.GetById(id);

            return detail;
        }

        public virtual void UpdateCSADetail(CloseStockAnalysisDetail entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _CSADetailRepository.Update(entity);
        }

        public virtual IList<InvestmentDecisionStockPool> GetIDStockPool()
        {
            var query = _IDStockPoolRepository.Table;

            return query.ToList();
        }

        public virtual void DeleteIDStockPool(IList<string> stockCodes)
        {
            if (stockCodes == null)
                throw new NullReferenceException(nameof(stockCodes));

            var infos = _IDStockPoolRepository.Table.Where(x => stockCodes.Contains(x.StockCode)).ToList();

            _IDStockPoolRepository.Delete(infos);
        }

        public virtual void AddIDStockPool(string stockCode, string stockName)
        {
            var info = new InvestmentDecisionStockPool
            {
                StockCode = stockCode,
                StockName = stockName
            };

            _IDStockPoolRepository.Insert(info);
        }

        public virtual IList<InvestmentDecisionCommittee> GetIDCommittees()
        {
            var query = _IDCRepository.Table;

            return query.ToList();
        }

        public virtual void AddIDCommittee(string code, string name)
        {
            var entity = new InvestmentDecisionCommittee
            {
                Code = code,
                Name = name,
            };

            _IDCRepository.Insert(entity);

            UpdateCommitteeWeight();
        }

        public virtual void DeleteIDCommittee(IList<int> ids)
        {
            if (ids == null)
                throw new NullReferenceException(nameof(ids));

            var committees = _IDCRepository.Table.Where(x => ids.Contains(x.Id)).ToList();
            _IDCRepository.Delete(committees);

            UpdateCommitteeWeight();
        }

        #endregion Methods
    }
}