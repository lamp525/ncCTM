using System;
using System.Collections.Generic;
using System.Linq;
using CTM.Core.Util;
using CTM.Data;

namespace CTM.Services.StatisticsReport
{
    public partial class DataVerifyService : IDataVerifyService
    {
        #region Fields

        private readonly IDbContext _dbContext;

        #endregion Fields

        #region Constructors

        public DataVerifyService(IDbContext context)
        {
            this._dbContext = context;
        }

        #endregion Constructors

        #region Methods

        public virtual IList<DataVerifyEntity> sp_GetDeliveryAndEntrustDiffData(int displayType, IList<int> accountIds, DateTime dateFrom, DateTime dateTo)
        {
            string ids = CommonHelper.ArrayListToSqlConditionString(accountIds);
            var commanText = $@"EXEC [dbo].[sp_GetDeliveryAndEntrustDiffData] @DisplayType= {displayType}, @AccountIds = '{ids}', @DateFrom = '{dateFrom}', @DateTo = '{dateTo}'";
            var result = _dbContext.SqlQuery<DataVerifyEntity>(commanText).ToList();

            return result;
        }

        #endregion Methods
    }
}