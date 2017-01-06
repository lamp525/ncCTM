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

        public virtual int AddIndustryInfo(IndustryInfo entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _industryInfoRepository.Insert(entity);

            return entity.Id;
        }

        public virtual void DeleteIndunstryInfo(int id)
        {
            var childrenCount = _industryInfoRepository.Table.Where(x => x.ParentId == id).Count();

            if (childrenCount > 0)
                throw new Exception("当前主体下存在下级主体，无法删除！");

            _industryInfoRepository.Delete(_industryInfoRepository.GetById(id));
        }

        #endregion Methods
    }
}