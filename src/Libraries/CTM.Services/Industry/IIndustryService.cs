using System.Collections.Generic;
using CTM.Core.Domain.Industry;

namespace CTM.Services.Industry
{
    public partial interface IIndustryService : IBaseService
    {
        IList<IndustryInfo> GetAllIndustry(bool showDeleted = false);

        string GetIndustryNameById(int industryId);

        int AddIndustryInfo(IndustryInfo entity);

        void DeleteIndunstryInfo(int id);
    }
}