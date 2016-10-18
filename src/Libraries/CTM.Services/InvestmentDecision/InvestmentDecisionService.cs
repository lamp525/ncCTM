using System;
using System.Collections.Generic;
using System.Linq;
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

        public virtual void DeleteInvestmentDecisionForm(IList<int> ids)
        {
            if (ids == null)
                throw new ArgumentNullException(nameof(ids));

            var query = _IDFRepository.Table.Where(x => ids.Contains(x.Id));

            _IDFRepository.Delete(query.ToArray());
        }
    }
}