using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

namespace CTM.Core.Util
{
    public class CommonHelper
    {
        #region 【编码】

        /// <summary>
        /// 每一层级的两位编码生成
        /// </summary>
        /// <param name="peerNumber"></param>
        /// <param name="parentCode"></param>
        /// <returns></returns>
        public static string GenerateCode(int peerNumber, string parentCode)
        {
            peerNumber += 1;

            if (peerNumber > 99) return null;

            string code = null;

            if (peerNumber > 9)
                code = peerNumber.ToString();
            else
                code = "0" + peerNumber.ToString();

            if (!string.IsNullOrEmpty(parentCode))
                code = parentCode + code;

            return code;
        }

        /// <summary>
        /// 股票代码补零
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string StockCodeZerofill(string code)
        {
            if (string.IsNullOrEmpty(code))
                return null;

            var num = 6 - code.Length;

            for (var i = 0; i < num; i++)
            {
                code = "0" + code;
            }

            return code;
        }

        #endregion 【编码】

        #region 【数值】

        /// <summary>
        /// 比率计算
        /// </summary>
        /// <param name="dividend">被除数</param>
        /// <param name="divisor">除数</param>
        /// <returns></returns>
        public static decimal CalculateRate(decimal dividend, decimal divisor)
        {
            return divisor == 0 ? 0 : dividend / divisor;
        }

        /// <summary>
        /// 将数值转换为正数
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int ConvertToPositive(int value)
        {
            return value < 0 ? -value : value;
        }

        /// <summary>
        /// 将数值转换为正数
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static decimal ConvertToPositive(decimal value)
        {
            return value < 0 ? -value : value;
        }

        /// <summary>
        /// 将数值转换为负数
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int ConvertToNegtive(int value)
        {
            return value > 0 ? -value : value;
        }

        /// <summary>
        /// 将数值转换为负数
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static decimal ConvertToNegtive(decimal value)
        {
            return value > 0 ? -value : value;
        }

        /// <summary>
        /// 保留小数点位数（默认两位）
        /// </summary>
        /// <param name="value"></param>
        /// <param name="digits"></param>
        /// <returns></returns>
        public static decimal SetDecimalDigits(decimal value, int digits = 2)
        {
            var result = Math.Round(value, digits, MidpointRounding.AwayFromZero);

            return result;
        }

        /// <summary>
        /// 将数值转换为百分比字符串(默认保留小数点后两位）
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ConvertToPercentage(decimal value, int digits = 2)
        {
            var round = Math.Round(value * 100, digits, MidpointRounding.AwayFromZero);
            var result = round.ToString("#0.#0") + "%";

            return result;
        }

        /// <summary>
        /// 将数值转换为百分比字符串(默认保留小数点后两位）
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ConvertToPercentage(double value, int digits = 2)
        {
            var round = Math.Round(value * 100, digits, MidpointRounding.AwayFromZero);
            var result = round.ToString("#0.#0") + "%";

            return result;
        }

        /// <summary>
        /// 判断由正数、负数、零组成的字符串
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNumeric(string value)
        {
            var result = Regex.IsMatch(value, @"^(\-|\+)?\d+(\.\d+)?$");

            return result;
        }

        public static bool IsInteger(string value)
        {
            var result = Regex.IsMatch(value, @"^-?[1-9]\d*|0$");

            return result;
        }

        /// <summary>
        /// 判断由数字和26个英文字母组成的字符串
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNumberAndAlphabet(string value)
        {
            var result = Regex.IsMatch(value, @"^[A-Za-z0-9]+$");

            return result;
        }

        #endregion 【数值】

        #region 【日期/时间】

        /// <summary>
        /// 取得指定日期的前一个工作日
        /// （如果指定日期为工作日则返回该指定日期）
        /// </summary>
        /// <param name="currentDate"></param>
        /// <returns></returns>
        public static DateTime GetPreviousWorkDay(DateTime currentDate)
        {
            var dayOfWeek = currentDate.DayOfWeek;

            if (dayOfWeek == DayOfWeek.Sunday)
                return currentDate.AddDays(-2).Date;
            else if (dayOfWeek == DayOfWeek.Saturday)
                return currentDate.AddDays(-1).Date;
            else
                return currentDate.Date;
        }

        /// <summary>
        ///  取得当前日期开始的N个工作日日期
        /// </summary>
        /// <param name="endDate"></param>
        /// <param name="number"></param>
        /// <returns></returns>
        public static IList<DateTime> GetWorkdaysBeforeCurrentDay(DateTime currentDate, int number = 25)
        {
            IList<DateTime> result = new List<DateTime>();

            var targetDate = currentDate.Date;

            for (var i = 0; i < number; i++)
            {
                if (targetDate.Date.DayOfWeek == DayOfWeek.Saturday)
                {
                    targetDate = targetDate.AddDays(-1);
                }
                else if (targetDate.Date.DayOfWeek == DayOfWeek.Sunday)
                {
                    targetDate = targetDate.AddDays(-2);
                }

                result.Add(targetDate);

                targetDate = targetDate.AddDays(-1);
            }

            return result;
        }

        /// <summary>
        /// 取得当前日期所在月份的最后一天
        /// </summary>
        /// <param name="currentDate"></param>
        /// <returns></returns>
        public static DateTime GetLastDayOfMonth(DateTime currentDate)
        {
            var firstDayOfCurrrentMonth = new DateTime(currentDate.Year, currentDate.Month, 1);

            var firstDayOfNextMothn = firstDayOfCurrrentMonth.AddMonths(1);

            var lastDayOfCurrentMonth = firstDayOfNextMothn.AddDays(-1);

            return lastDayOfCurrentMonth;
        }

        /// <summary>
        /// 取得当前日期所在月份的第一天
        /// </summary>
        /// <param name="currentDate"></param>
        /// <returns></returns>
        public static DateTime GetFirstDayOfMonth(DateTime currentDate)
        {
            var firstDayOfCurrrentMonth = new DateTime(currentDate.Year, currentDate.Month, 1);

            return firstDayOfCurrrentMonth;
        }

        /// <summary>
        /// 取得当前月份开始的前N个月的最后一天日期
        /// </summary>
        /// <param name="currentDate"></param>
        /// <param name="number"></param>
        /// <returns></returns>
        public static IList<DateTime> GetLastDateOfMonthBeforeCurrentDate(DateTime currentDate, int number = 25)
        {
            var result = new List<DateTime>();

            var targetDate = GetLastDayOfMonth(currentDate);

            for (var i = 0; i < number; i++)
            {
                result.Add(targetDate);

                targetDate = GetLastDayOfMonth(targetDate.AddMonths(-1));
            }

            return result;
        }

        /// <summary>
        /// 取得当前日期所在月份的所有工作日
        /// </summary>
        /// <param name="currentDate"></param>
        /// <returns></returns>
        public static IList<DateTime> GetAllWorkDaysOfCurrentMonth(DateTime currentDate)
        {
            var result = new List<DateTime>();
            var firstDay = GetFirstDayOfMonth(currentDate);
            var lastDay = GetLastDayOfMonth(currentDate);

            if (lastDay > DateTime.Now.Date)
                lastDay = DateTime.Now.Date;

            for (var date = firstDay; date <= lastDay; date = date.AddDays(1))
            {
                if (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday)
                    result.Add(date);
            }

            return result;
        }

        /// <summary>
        /// 取得时间段内的所有工作日
        /// </summary>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <returns></returns>
        public static IList<DateTime> GetAllWorkDays(DateTime dateFrom, DateTime dateTo)
        {
            var result = new List<DateTime>();

            var startDate = dateFrom.Date;
            var endDate = dateTo.AddDays(1).Date;

            for (var date = startDate; date < endDate; date = date.AddDays(1))
            {
                if (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday)
                    result.Add(date);
            }

            return result;
        }

        /// <summary>
        /// 取得上周日的日期
        /// </summary>
        /// <param name="currentDate"></param>
        /// <returns></returns>
        public static DateTime GetLastSunday(DateTime currentDate)
        {
            var dayOfWeek = currentDate.DayOfWeek == DayOfWeek.Sunday ? 7 : (int)currentDate.DayOfWeek;

            //上周日
            var result = currentDate.AddDays(-dayOfWeek);

            return result;
        }

        /// <summary>
        /// 取得本周一的日期
        /// </summary>
        /// <param name="currentDate"></param>
        /// <returns></returns>
        public static DateTime GetCurrentMonday(DateTime currentDate)
        {
            var dayOfWeek = currentDate.DayOfWeek == DayOfWeek.Sunday ? 7 : (int)currentDate.DayOfWeek;

            var result = currentDate.AddDays(-dayOfWeek + 1);

            return result;
        }

        /// <summary>
        /// 取得包括当前日期的N的周日日期
        /// </summary>
        /// <param name="currentDate"></param>
        /// <param name="number"></param>
        /// <returns></returns>
        public static IList<DateTime> GetLastDateOfWeekBeforeCurrentDate(DateTime currentDate, int number = 25)
        {
            var result = new List<DateTime>();

            var targetDate = currentDate;

            for (var i = 0; i < number; i++)
            {
                result.Add(targetDate);

                var dayOfWeek = targetDate.DayOfWeek == DayOfWeek.Sunday ? 7 : (int)targetDate.DayOfWeek;

                //上周日
                targetDate = targetDate.AddDays(1 - dayOfWeek - 1);
            }

            return result;
        }

        /// <summary>
        /// 取得时间段内的工作日数
        /// </summary>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <returns></returns>
        public static int GetWorkDayNumber(DateTime dateFrom, DateTime dateTo)
        {
            int number = 0;

            var days = (dateTo.Date - dateFrom.Date).Days;
            for (int i = 0; i < days; i++)
            {
                dateFrom = dateFrom.AddDays(1);

                if (dateFrom.DayOfWeek != DayOfWeek.Saturday && dateFrom.DayOfWeek != DayOfWeek.Sunday)
                    number++;
            }

            return number;
        }

        /// <summary>
        /// 取得时间段内的天数
        /// </summary>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <returns></returns>
        public static int GetDayNumber(DateTime dateFrom, DateTime dateTo)
        {
            var days = (dateTo.Date - dateFrom.Date).Days;

            return days;
        }

        /// <summary>
        /// 判断是否为闰年
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public static bool IsLeapYear(int year)
        {
            if ((year % 400 == 0) || (year % 4 == 0 && year % 100 != 0))
                return true;
            else
                return false;
        }

        /// <summary>
        /// 取得指定年份的天数
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public static int GetDayNumberOfYear(int year)
        {
            if (IsLeapYear(year))
                return 366;
            else
                return 365;
        }

        /// <summary>
        /// 取得指定时间段的累加日比率
        /// </summary>
        /// <param name="yearRate"></param>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <returns></returns>
        public static decimal AccumulatedDayRate(decimal yearRate, DateTime dateFrom, DateTime dateTo)
        {
            decimal result = 0;

            var startYear = dateFrom.Year;
            var endYear = dateTo.Year;

            var yearInterval = endYear - startYear;

            if (yearInterval == 0)
            {
                result = GetDayNumber(dateFrom, dateTo) * (yearRate / GetDayNumberOfYear(startYear));
            }
            else if (yearInterval > 0)
            {
                result = GetDayNumber(dateFrom, new DateTime(startYear, 12, 31)) * (yearRate / GetDayNumberOfYear(startYear))
                 + GetDayNumber(new DateTime(endYear, 1, 1), dateTo) * (yearRate / GetDayNumberOfYear(endYear))
                 + (yearInterval - 1) * yearRate;
            }

            return result;
        }

        /// <summary>
        /// 将毫秒格式化成：xx小时xx分钟xx秒
        /// </summary>
        /// <param name="milliseconds"></param>
        /// <returns></returns>
        public static string FormatMillisecondsToString(long milliseconds)
        {
            string result = string.Empty;

            long hour = 0;
            long minute = 0;
            long second = 0;
            long ms = 0;

            ms = milliseconds % 1000;

            second = milliseconds / 1000;

            if (second > 60)
            {
                minute = second / 60;
                second = second % 60;
            }
            if (minute > 60)
            {
                hour = minute / 60;
                minute = minute % 60;
            }
            return (hour.ToString() + "小时" + minute.ToString() + "分钟" + second.ToString() + "秒" + ms.ToString() + "毫秒");
        }

        /// <summary>
        /// 是否为日期型字符串
        /// </summary>
        /// <param name="value">日期字符串(2008-05-08)</param>
        /// <returns></returns>
        public static bool IsDate(string value)
        {
            return Regex.IsMatch(value, @"^(?:(?:1[6-9]|[2-9][0-9])[0-9]{2}([-/.]?)(?:(?:0?[1-9]|1[0-2])\1(?:0?[1-9]|1[0-9]|2[0-8])|(?:0?[13-9]|1[0-2])\1(?:29|30)|(?:0?[13578]|1[02])\1(?:31))|(?:(?:1[6-9]|[2-9][0-9])(?:0[48]|[2468][048]|[13579][26])|(?:16|[2468][048]|[3579][26])00)([-/.]?)0?2\2(?:29))$");
        }

        /// <summary>
        /// 是否为时间型字符串
        /// </summary>
        /// <param name="source">时间字符串(15:00:00)</param>
        /// <returns></returns>
        public static bool IsTime(string value)
        {
            return Regex.IsMatch(value, @"^((20|21|22|23|[0-1]?\d):[0-5]?\d:[0-5]?\d)$");
        }

        /// <summary>
        /// 是否为日期+时间型字符串
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsDateTime(string value)
        {
            return Regex.IsMatch(value, @"^(((((1[6-9]|[2-9]\d)\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})-0?2-(0?[1-9]|1\d|2[0-8]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29-)) (20|21|22|23|[0-1]?\d):[0-5]?\d:[0-5]?\d)$ ");
        }

        #endregion 【日期/时间】

        #region【格式/类型 转换】

        /// <summary>
        /// 字符串格式转化为时间格式
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime StringToDateTime(string value)
        {
            char[] jointCharArray = new char[] { '-', '/' };

            if (value.IndexOfAny(jointCharArray) < 0)
            {
                if (value.Length == 8)
                    value = value.Substring(0, 4) + "-" + value.Substring(4, 2) + "-" + value.Substring(6, 2);
                else if (value.Length == 6)
                    value = value.Substring(0, 4) + "-" + value.Substring(4, 1) + "-" + value.Substring(5, 1);
            }

            var result = Convert.ToDateTime(value, new DateTimeFormatInfo());

            return result;
        }

        /// <summary>
        /// 字符串转化为Decimal
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static decimal StringToDecimal(string value)
        {
            decimal result;

            try
            {
                result = Convert.ToDecimal(value);
            }
            catch (Exception)
            {
                result = 0;
            }

            return result;
        }

        /// <summary>
        /// 整数到字节数组的转换
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static byte[] intToByte(int number)
        {
            int temp = number;
            byte[] b = new byte[4];
            for (int i = b.Length - 1; i > -1; i--)
            {
                //将最高位保存在最低位
                b[i] = Convert.ToByte(temp & 0xff);

                //向右移8位
                temp = temp >> 8;
            }
            return b;
        }

        /// <summary>
        /// 字节数组到整数的转换
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static int byteToInt(byte[] b)
        {
            int s = 0;

            for (int i = 0; i < 3; i++)
            {
                if (b[i] >= 0)
                    s = s + b[i];
                else
                    s = s + 256 + b[i];
                s = s * 256;
            }

            //最后一个之所以不乘，是因为可能会溢出
            if (b[3] >= 0)
                s = s + b[3];
            else
                s = s + 256 + b[3];
            return s;
        }

        /// <summary>
        /// 数字转换日期格式
        /// </summary>
        /// <param name="numberString">数字</param>
        /// <returns>日期/时间格式</returns>
        public static string NumberStringToDate(string value)
        {
            if (value.Length < 6 && IsNumeric(value))
            {
                return DateTime.FromOADate(double.Parse(value)).ToShortDateString();
            }
            else if (value.Length > 10)
            {
                return value.Substring(0, 8);
            }

            return value;
        }

        /// <summary>
        /// 数字转换时间格式
        /// </summary>
        /// <param name="value">数字,如:42095.7069444444/0.650694444444444</param>
        /// <returns>日期/时间格式</returns>
        public static string NumberStringToTime(string value)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                decimal tempValue;

                //判断是否为数字
                if (decimal.TryParse(value, out tempValue))
                {
                    if (Math.Truncate(tempValue) > 50000)
                    {
                        if (value.Length == 8) value = "0" + value;

                        var resultTime = value.Substring(0, 2) + ":" + value.Substring(2, 2) + ":" + value.Substring(4, 2);

                        return resultTime;
                    }

                    //天数,取整
                    int day = Convert.ToInt32(Math.Truncate(tempValue));

                    //如果是小于32,则减1,否则减2
                    //日期从1900-01-01开始累加
                    // day = day < 32 ? day - 1 : day - 2;
                    DateTime dt = new DateTime(1900, 1, 1).AddDays(day < 32 ? (day - 1) : (day - 2));

                    //小时:减掉天数,这个数字转换小时:(* 24)
                    decimal hourTemp = (tempValue - day) * 24;//获取小时数
                    //取整.小时数
                    int hour = Convert.ToInt32(Math.Truncate(hourTemp));

                    //分钟:减掉小时,( * 60)
                    //这里舍入,否则取值会有1分钟误差.
                    decimal minuteTemp = Math.Round((hourTemp - hour) * 60, 2);//获取分钟数
                    int minute = Convert.ToInt32(Math.Truncate(minuteTemp));

                    //秒:减掉分钟,( * 60)
                    //这里舍入,否则取值会有1秒误差.
                    decimal secondTemp = Math.Round((minuteTemp - minute) * 60, 2);//获取秒数
                    int second = Convert.ToInt32(Math.Truncate(secondTemp));

                    //时间格式:00:00:00
                    string resultTimes = string.Format("{0}:{1}:{2}",
                            (hour < 10 ? ("0" + hour) : hour.ToString()),
                            (minute < 10 ? ("0" + minute) : minute.ToString()),
                            (second < 10 ? ("0" + second) : second.ToString()));

                    if (day > 0)
                        return string.Format("{0} {1}", dt.ToString("yyyy-MM-dd"), resultTimes);
                    else
                        return resultTimes;
                }
            }
            return value;
        }

        /// <summary>
        /// 将数组转换为SQL条件字符串
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ArrayListToSqlConditionString(IList<string> source)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            string result = string.Empty;

            foreach (var item in source)
            {
                result = result + "'" + item + "'" + ",";
            }

            result = result.Substring(0, result.Length - 1);

            return result;
        }

        /// <summary>
        /// 将数组转换为SQL条件字符串
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ArrayListToSqlConditionString(IList<int> source)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            string result = string.Empty;

            foreach (var item in source)
            {
                result = result + item + ",";
            }

            if (!string.IsNullOrEmpty(result))
                result = result.Substring(0, result.Length - 1);

            return result;
        }

        #endregion
    }
}