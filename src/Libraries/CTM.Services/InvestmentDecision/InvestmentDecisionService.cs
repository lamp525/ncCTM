using System;
using CTM.Core.Data;
using CTM.Core.Domain.InvestmentDecision;

namespace CTM.Services.InvestmentDecision
{
    public partial class InvestmentDecisionService : IInvestmentDecisionService
    {
        private readonly IRepository<InvestmentDecisionForm> _IDFRepository;

        public InvestmentDecisionService(IRepository<InvestmentDecisionForm> IDFRepository)
        {
            this._IDFRepository = IDFRepository;
        }

        public virtual void AddInvestmentDecisionForm(InvestmentDecisionForm entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _IDFRepository.Insert(entity);
        }

        public virtual void DeleteInvestmentDecisionForm(InvestmentDecisionForm entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _IDFRepository.Delete(entity);
        }
    }
}