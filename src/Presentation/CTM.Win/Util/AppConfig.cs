namespace CTM.Win.Util
{
    public class AppConfig
    {
        public static string _ConnString = System.Configuration.ConfigurationManager.ConnectionStrings["CTMContext"].ToString();
    }
}