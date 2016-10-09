using System;

namespace CTM.Services.Common
{
    public partial interface ICommonService : IBaseService
    {
        /// <summary>
        /// Get Db Server Current Time
        /// </summary>
        /// <returns></returns>
        DateTime GetCurrentServerTime();
    }
}