using System;
using System.Configuration;
using System.Globalization;
using System.Linq;

namespace CTM.Core.Util
{
    public class AppConfigHelper
    {
        private static readonly string _initDateString = ConfigurationManager.AppSettings["InitDate"];
        private static readonly string _marginTradingAPR = ConfigurationManager.AppSettings["MarginTradingAPR"];
        private static readonly string _annuallyAccountingDays = ConfigurationManager.AppSettings["AnnuallyAccountingDays"];

        /// <summary>
        /// 取得统计用期初日期
        /// </summary>
        /// <returns></returns>
        public static DateTime StatisticsInitDate
        {
            get
            {
                return CommonHelper.IsDate(_initDateString) ? Convert.ToDateTime(_initDateString, new DateTimeFormatInfo()) : new DateTime(2016, 1, 1);
            }
        }

        /// <summary>
        /// 融资融券日利率
        /// </summary>
        public static decimal MarginTradingDPR
        {
            get
            {
                var accountingDays = string.IsNullOrEmpty(_annuallyAccountingDays) ? 360 : int.Parse(_annuallyAccountingDays);

                decimal dpr = MarginTradingAPR / 360;
                return dpr;
            }
        }

        /// <summary>
        /// 融资融券年利率
        /// </summary>
        public static decimal MarginTradingAPR
        {
            get
            {
                decimal marginTradingAPR = string.IsNullOrEmpty(_marginTradingAPR) ? 0.08M : decimal.Parse(_marginTradingAPR);

                return marginTradingAPR;
            }
        }

        /// <summary>
        /// 默认界面皮肤
        /// </summary>
        public static string DefaultSkinName
        {
            get
            {
                ConfigurationManager.RefreshSection("DefaultSkinName");

                var skinName = ConfigurationManager.AppSettings["DefaultSkinName"];

                return skinName ?? "Office 2013";
            }
        }

        /// <summary>
        /// 设置App Config Settings
        /// </summary>
        /// <param name="keyName"></param>
        /// <param name="value"></param>
        public static void SetAppConfigSettings(string key, string value)
        {
            var configManager = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            if (ConfigurationManager.AppSettings.AllKeys.Contains(key))
                configManager.AppSettings.Settings[key].Value = value;

            configManager.Save();
        }
    }
}