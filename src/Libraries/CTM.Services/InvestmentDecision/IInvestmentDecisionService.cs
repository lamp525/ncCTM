using System.Collections.Generic;
using CTM.Core.Domain.InvestmentDecision;

namespace CTM.Services.InvestmentDecision
{
    public partial interface IInvestmentDecisionService : IBaseService
    {
        void AddInvestmentDecisionForm(InvestmentDecisionForm entity);

        void DeleteInvestmentDecisionForm(IList<int> ids);
    }
}