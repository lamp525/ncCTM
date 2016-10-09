using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CTM.Core.Domain.Account;
using CTM.Core.Domain.TKLine;
using CTM.Core.Domain.TradeRecord;
using CTM.Services.Account;

namespace CTM.Services.StatisticsReport
{
  public partial   interface IDeliveryStatisticsReportService:IBaseService 
    {

        IList<AccountInvestIncomeEntity> CalculateAccountInvestIncome(IList<DeliveryRecord> deliveryRecords, IList<DateTime> queryDates, IList<TKLineToday> stockClosePrices, AccountEntity accountInfo);
    }
}
