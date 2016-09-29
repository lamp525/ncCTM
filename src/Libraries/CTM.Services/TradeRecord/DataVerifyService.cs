using System;
using System.Collections.Generic;
using System.Linq;
using CTM.Data;

namespace CTM.Services.TradeRecord
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

        public virtual IList<DataVerifyEntity> GetDiffBetweenDeliveryAndDailyData(int accountId, DateTime dateFrom, DateTime dateTo)
        {
            var commanText = $@"EXEC [dbo].[sp_GetDiffBetweenDeliveryAndDailyData] 
                                                @AccountId = {accountId}, 
                                                @DateFrom = '{dateFrom}', 
                                                @DateTo = '{dateTo}'";

            var result = _dbContext.SqlQuery<DataVerifyEntity>(commanText).ToList();

            return result;
        }

        #endregion Methods
    }
}