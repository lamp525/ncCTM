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
        private readonly IRepository<InvestmentDecisionForm> _IDFRepository;
        private readonly IRepository<InvestmentDecisionVote> _IDVRepository;

        private readonly ICommonService _commonService;

        public InvestmentDecisionService(
            IRepository<InvestmentDecisionForm> IDFRepository,
            IRepository<InvestmentDecisionVote> IDVRepository,
            ICommonService commonService)
        {
            this._IDFRepository = IDFRepository;
            this._IDVRepository = IDVRepository;

            this._commonService = commonService;
        }

        public virtual void AddInvestmentDecisionForm(InvestmentDecisionForm entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _IDFRepository.Insert(entity);
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
    }
}