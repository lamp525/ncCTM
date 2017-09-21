using System.Configuration;


namespace CTM.Win.Util
{
    public class AppConfig
    {
        public static string _ConnString = ConfigurationManager.ConnectionStrings["CTMContext"].ToString();

        public static string _ReportTemplateTradeTypeProfit = ConfigurationManager.AppSettings["TradeTypeProfitTemplate"].ToString();
    }
}