using System.Collections.Generic;
using CTM.Core;
using CTM.Core.Domain.InvestmentDecision;

namespace CTM.Services.InvestmentDecision
{
    public partial interface IInvestmentDecisionService : IBaseService
    {
        void AddInvestmentDecisionForm(InvestmentDecisionForm entity);

        void DeleteInvestmentDecisionForm(IList<int> ids);

        void AddInvestmentDecisionVote(InvestmentDecisionVote entity);

        void DeleteInvestmentDecisionVote(string investorCode, string formSerialNo);

        void UpdateInvestmentDecisionVote(InvestmentDecisionVote entity);

        void InvestmentDecisionVoteProcess(string investorCode, string formSerialNo, EnumLibrary.IDVoteFlag flag, string reason);

        InvestmentDecisionVote GetInvestmentDecisionVote(string investorCode, string formSerialNo);

        IList<InvestmentDecisionVote> GetInvestmentDecisionVotes(string investorCode);
    }
}