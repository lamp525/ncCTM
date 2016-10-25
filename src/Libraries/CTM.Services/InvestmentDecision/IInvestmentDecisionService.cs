using System;
using System.Collections.Generic;
using CTM.Core;
using CTM.Core.Domain.InvestmentDecision;

namespace CTM.Services.InvestmentDecision
{
    public partial interface IInvestmentDecisionService : IBaseService
    {
        string GenerateSerialNo(DateTime applyDate);

        void SubmitInvestmentDecisionApplication(InvestmentDecisionForm entity);

        void DeleteInvestmentDecisionForm(string serialNo);

        void DeleteInvestmentDecisionForm(IList<string> serialNos);

        void InvestmentDecisionVoteProcess(string investorCode, string formSerialNo, EnumLibrary.IDVoteFlag flag, string reason);

        IList<InvestmentDecisionVote> GetInvestmentDecisionVotes(string investorCode);
    }
}