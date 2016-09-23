using System;
using System.Collections.Generic;
using System.Linq;
using CTM.Data;

namespace CTM.Services.Common
{
    public partial class CommonService : ICommonService
    {
        #region Fields

        private readonly IDbContext _context;

        #endregion Fields

        #region Constructors

        public CommonService(IDbContext context)
        {
            this._context = context;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Get Db Server Current Time
        /// </summary>
        /// <returns></returns>
        public virtual DateTime GetCurrentServerTime()
        {
            string sql = "select getdate() as CurrentTime";
            var query = _context.SqlQuery<DateTime>(sql);

            var result = query.FirstOrDefault();
            return result;
        }

        #endregion Methods
    }
}