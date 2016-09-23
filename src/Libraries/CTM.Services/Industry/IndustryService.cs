using System;
using System.Collections.Generic;
using System.Linq;
using CTM.Core.Data;
using CTM.Core.Domain.Industry;

namespace CTM.Services.Industry
{
    public partial class IndustryService : IIndustryService
    {
        #region Fields

        private readonly IRepository<IndustryInfo> _industryInfoRepository;

        #endregion Fields

        #region Constructors

        public IndustryService(IRepository<IndustryInfo> industryInfoRepository)
        {
            this._industryInfoRepository = industryInfoRepository;
        }

        #endregion Constructors

        #region Methods

        public virtual IList<IndustryInfo> GetAllIndustry(bool showDeleted = false)
        {
            var result = new List<IndustryInfo>();

            var query = _industryInfoRepository.Table;

            if (!showDeleted)
                query = query.Where(x => !x.IsDeleted);

            result = query.ToList();

            return result;
        }

        public virtual string GetIndustryNameById(int industryId)
        {
            var name = string.Empty;

            var query = _industryInfoRepository.GetById(industryId);

            if (query != null) name = query.Name;

            return name;
        }

        #endregion Methods
    }
}