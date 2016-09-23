using System;
using System.Collections.Generic;
using System.Linq;

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