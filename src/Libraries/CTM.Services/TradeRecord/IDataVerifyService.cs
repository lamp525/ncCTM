using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTM.Services.TradeRecord
{
   public partial interface IDataVerifyService:IBaseService 
    {
       IList<DataVerifyEntity> GetDiffBetweenDeliveryAndDailyData(int accountId, DateTime dateFrom, DateTime dateTo);
    }
}
