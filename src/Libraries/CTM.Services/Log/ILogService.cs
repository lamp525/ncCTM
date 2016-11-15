using CTM.Core.Domain.Log;

namespace CTM.Services.Log
{
    public partial interface ILogService : IBaseService
    {
        void SaveLoginLog(LoginLog entity);
    }
}