using System;
using System.Collections.Generic;
using System.Linq;
using CTM.Core;
using CTM.Core.Data;
using CTM.Core.Domain.InvestmentDecision;
using CTM.Services.Common;

namespace CTM.Services.InvestmentDecision
{
    public partial class InvestmentDecisionService : IInvestmentDecisionService
    {
        #region Fields

        private readonly IRepository<InvestmentDecisionCommittee> _IDCRepository;
        private readonly IRepository<InvestmentDecisionForm> _IDFRepository;
        private readonly IRepository<InvestmentDecisionVote> _IDVRepository;

        private readonly ICommonService _commonService;

        #endregion Fields

        #region Constructors

        public InvestmentDecisionService(
        IRepository<InvestmentDecisionCommittee> IDCRepository,
        IRepository<InvestmentDecisionForm> IDFRepository,
        IRepository<InvestmentDecisionVote> IDVRepository,
        ICommonService commonService)
        {
            this._IDCRepository = IDCRepository;
            this._IDFRepository = IDFRepository;
            this._IDVRepository = IDVRepository;

            this._commonService = commonService;
        }

        #endregion Constructors

        #region Methods

        public virtual void SubmitInvestmentDecisionApplication(InvestmentDecisionForm entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _IDFRepository.Insert(entity);

            var committeeCodes = _IDCRepository.Table.Select(x => x.Code).ToList();
            committeeCodes.Add(entity.ApplyUser);
            committeeCodes = committeeCodes.Distinct().ToList();

            var applyUserWeight = 35;
            var otherWeight = (100 - applyUserWeight) / (committeeCodes.Count - 1);

            var defaultVoteInfos = new List<InvestmentDecisionVote>();
            foreach (var code in committeeCodes)
            {
                var info = new InvestmentDecisionVote
                {
                    AuthorityLevel = 0,
                    Flag = code == entity.ApplyUser ? (int)EnumLibrary.IDVoteFlag.Approval : (int)EnumLibrary.IDVoteFlag.None,
                    FormSerialNo = entity.SerialNo,
                    Reason = code == entity.ApplyUser ? "发起人默认赞同。" : string.Empty,
                    UserCode = code,
                    VoteTime = _commonService.GetCurrentServerTime(),
                    Weight = code == entity.ApplyUser ? applyUserWeight : otherWeight,
                };

                defaultVoteInfos.Add(info);
            }
            _IDVRepository.Insert(defaultVoteInfos);
        }

        public virtual void DeleteInvestmentDecisionForm(IList<int> ids)
        {
            if (ids == null)
                throw new ArgumentNullException(nameof(ids));

            var query = _IDFRepository.Table.Where(x => ids.Contains(x.Id));

            _IDFRepository.Delete(query.ToArray());
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
            var query = _IDVRepository.Table.Where(x => x.UserCode == investorCode);

            return query.ToList();
        }

        public void InvestmentDecisionVoteProcess(string investorCode, string formSerialNo, EnumLibrary.IDVoteFlag flag, string reason)
        {
            var info = _IDVRepository.Table.SingleOrDefault(x => x.UserCode == investorCode && x.FormSerialNo == formSerialNo);

            var now = _commonService.GetCurrentServerTime();

            if (info == null)
            {
                info = new InvestmentDecisionVote
                {
                    AuthorityLevel = 0,
                    Flag = (int)flag,
                    FormSerialNo = formSerialNo,
                    Reason = reason,
                    UserCode = investorCode,
                    VoteTime = now,
                    Weight = 0,
                };

                _IDVRepository.Insert(info);
            }
            else
            {
                info.Flag = (int)flag;
                info.Reason = reason;
                info.VoteTime = now;

                _IDVRepository.Update(info);
            }
        }

        #endregion Methods
    }
}