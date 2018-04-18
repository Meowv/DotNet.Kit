using System;

namespace DotNet.Kit
{
    public class TimeHelper
    {
        /// <summary>
        /// 时间戳转为C#格式时间
        /// </summary>
        /// <param name="timestamp"></param>
        /// <returns></returns>
        public static DateTime GetDateTime(string timestamp)
        {
            DateTime dateTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timestamp + "0000000");
            TimeSpan time = new TimeSpan(lTime);
            return dateTime.Add(time);
        }

        /// <summary>
        /// DateTime时间格式转换为Unix时间戳格式
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static string ConvertDateTime(DateTime dateTime)
        {
            DateTime time = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            return Convert.ToString((int)(dateTime - time).TotalSeconds);
        }

        /// <summary>
        /// 获取以0点0分0秒开始的日期
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTime GetStartDateTime(DateTime dateTime)
        {
            if (dateTime.Hour != 0)
            {
                var year = dateTime.Year;
                var month = dateTime.Month;
                var day = dateTime.Day;
                var hour = "0";
                var minute = "0";
                var second = "0";
                dateTime = Convert.ToDateTime(string.Format("{0}-{1}-{2} {3}:{4}:{5}", year, month, day, hour, minute, second));
            }
            return dateTime;
        }

        /// <summary>
        /// 获取以23点59分59秒结束的日期
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTime GetEndDateTime(DateTime dateTime)
        {
            if (dateTime.Hour != 23)
            {
                var year = dateTime.Year;
                var month = dateTime.Month;
                var day = dateTime.Day;
                var hour = "23";
                var minute = "59";
                var second = "59";
                dateTime = Convert.ToDateTime(string.Format("{0}-{1}-{2} {3}:{4}:{5}", year, month, day, hour, minute, second));
            }
            return dateTime;
        }

        /// <summary>
        /// 返回每月的第一天和最后一天
        /// </summary>
        /// <param name="month"></param>
        /// <param name="firstDay"></param>
        /// <param name="lastDay"></param>
        public static void ReturnDateFormat(int month, out string firstDay, string lastDay)
        {
            int year = DateTime.Now.Year + month / 12;
            if (month != 12)
            {
                month = month % 12;
            }
            switch (month)
            {
                case 1:
                    firstDay = DateTime.Now.ToString(year + "-0" + month + "-01");
                    lastDay = DateTime.Now.ToString(year + "-0" + month + "-31");
                    break;
                case 2:
                    firstDay = DateTime.Now.ToString(year + "-0" + month + "-01");
                    if (DateTime.IsLeapYear(DateTime.Now.Year))
                        lastDay = DateTime.Now.ToString(year + "-0" + month + "-29");
                    else
                        lastDay = DateTime.Now.ToString(year + "-0" + month + "-28");
                    break;
                case 3:
                    firstDay = DateTime.Now.ToString(year + "-0" + month + "-01");
                    lastDay = DateTime.Now.ToString("yyyy-0" + month + "-31");
                    break;
                case 4:
                    firstDay = DateTime.Now.ToString(year + "-0" + month + "-01");
                    lastDay = DateTime.Now.ToString(year + "-0" + month + "-30");
                    break;
                case 5:
                    firstDay = DateTime.Now.ToString(year + "-0" + month + "-01");
                    lastDay = DateTime.Now.ToString(year + "-0" + month + "-31");
                    break;
                case 6:
                    firstDay = DateTime.Now.ToString(year + "-0" + month + "-01");
                    lastDay = DateTime.Now.ToString(year + "-0" + month + "-30");
                    break;
                case 7:
                    firstDay = DateTime.Now.ToString(year + "-0" + month + "-01");
                    lastDay = DateTime.Now.ToString(year + "-0" + month + "-31");
                    break;
                case 8:
                    firstDay = DateTime.Now.ToString(year + "-0" + month + "-01");
                    lastDay = DateTime.Now.ToString(year + "-0" + month + "-31");
                    break;
                case 9:
                    firstDay = DateTime.Now.ToString(year + "-0" + month + "-01");
                    lastDay = DateTime.Now.ToString(year + "-0" + month + "-30");
                    break;
                case 10:
                    firstDay = DateTime.Now.ToString(year + "-" + month + "-01");
                    lastDay = DateTime.Now.ToString(year + "-" + month + "-31");
                    break;
                case 11:
                    firstDay = DateTime.Now.ToString(year + "-" + month + "-01");
                    lastDay = DateTime.Now.ToString(year + "-" + month + "-30");
                    break;
                default:
                    firstDay = DateTime.Now.ToString(year + "-" + month + "-01");
                    lastDay = DateTime.Now.ToString(year + "-" + month + "-31");
                    break;
            }
        }

        /// <summary>
        /// 将时间格式化成 年月日 的形式,如果时间为null，返回当前系统时间
        /// </summary>
        /// <param name="dt">DateTime</param>
        /// <param name="separator">年月日分隔符</param>
        /// <returns></returns>
        public static string GetFormatDate(DateTime dt, char separator)
        {
            if (dt != null && !dt.Equals(DBNull.Value))
            {
                string date = string.Format($"yyyy{separator}MM{separator}dd");
                return dt.ToString(date);
            }
            else
            {
                return GetFormatDate(DateTime.Now, separator);
            }
        }

        /// <summary>
        /// 将时间格式化成 时分秒 的形式,如果时间为null，返回当前系统时间
        /// </summary>
        /// <param name="dt">DateTime</param>
        /// <param name="separator">年月日分隔符</param>
        /// <returns></returns>
        public static string GetFormatTime(DateTime dt, char separator)
        {
            if (dt != null && !dt.Equals(DBNull.Value))
            {
                string date = string.Format($"hh{separator}mm{separator}ss");
                return dt.ToString(date);
            }
            else
            {
                return GetFormatDate(DateTime.Now, separator);
            }
        }

        /// <summary>
        /// 把秒转换成分钟
        /// </summary>
        /// <param name="second"></param>
        /// <returns></returns>
        public static int Second2Minute(int second)
        {
            decimal mm = second / (decimal)60;
            return Convert.ToInt32(Math.Ceiling(mm));
        }

        /// <summary>
        /// 返回某年某月最后一天
        /// </summary>
        /// <param name="year">年份</param>
        /// <param name="month">月份</param>
        /// <returns></returns>
        public static int GetMonthLastDate(int year, int month)
        {
            DateTime lastDay = new DateTime(year, month, new System.Globalization.GregorianCalendar().GetDaysInMonth(year, month));
            int Day = lastDay.Day;
            return Day;
        }

        /// <summary>
        /// 返回时间差
        /// </summary>
        /// <param name="DateTime1"></param>
        /// <param name="DateTime2"></param>
        /// <returns></returns>
        public static string DateDiff(DateTime DateTime1, DateTime DateTime2)
        {
            string dateDiff = null;
            try
            {
                TimeSpan ts = DateTime2 - DateTime1;
                if (ts.Days >= 1)
                {
                    dateDiff = DateTime1.Month.ToString() + "月" + DateTime1.Day.ToString() + "日";
                }
                else
                {
                    if (ts.Hours > 1)
                    {
                        dateDiff = ts.Hours.ToString() + "小时前";
                    }
                    else
                    {
                        dateDiff = ts.Minutes.ToString() + "分钟前";
                    }
                }
            }
            catch
            {
            }
            return dateDiff;
        }

        /// <summary>
        /// 获得两个日期的间隔
        /// </summary>
        /// <param name="DateTime1"></param>
        /// <param name="DateTime2"></param>
        /// <returns></returns>
        public static TimeSpan DateDiff2(DateTime DateTime1, DateTime DateTime2)
        {
            TimeSpan ts1 = new TimeSpan(DateTime1.Ticks);
            TimeSpan ts2 = new TimeSpan(DateTime2.Ticks);
            TimeSpan ts = ts1.Subtract(ts2).Duration();
            return ts;
        }

        /// <summary>
        /// 格式化日期时间
        /// </summary>
        /// <param name="dateTime1">日期时间</param>
        /// <param name="dateMode">显示模式</param>
        /// <returns></returns>
        public static string FormatDate(DateTime dateTime1, string dateMode)
        {
            switch (dateMode)
            {
                case "0":
                    return dateTime1.ToString("yyyy-MM-dd");
                case "1":
                    return dateTime1.ToString("yyyy-MM-dd HH:mm:ss");
                case "2":
                    return dateTime1.ToString("yyyy/MM/dd");
                case "3":
                    return dateTime1.ToString("yyyy年MM月dd日");
                case "4":
                    return dateTime1.ToString("MM-dd");
                case "5":
                    return dateTime1.ToString("MM/dd");
                case "6":
                    return dateTime1.ToString("MM月dd日");
                case "7":
                    return dateTime1.ToString("yyyy-MM");
                case "8":
                    return dateTime1.ToString("yyyy/MM");
                case "9":
                    return dateTime1.ToString("yyyy年MM月");
                default:
                    return dateTime1.ToString();
            }
        }

        /// <summary>
        /// 得到随机日期
        /// </summary>
        /// <param name="time1">起始日期</param>
        /// <param name="time2">结束日期</param>
        /// <returns>间隔日期之间的 随机日期</returns>
        public static DateTime GetRandomTime(DateTime time1, DateTime time2)
        {
            Random random = new Random();
            DateTime minTime = new DateTime();
            DateTime maxTime = new DateTime();

            TimeSpan ts = new TimeSpan(time1.Ticks - time2.Ticks);

            // 获取两个时间相隔的秒数
            double dTotalSecontds = ts.TotalSeconds;
            int iTotalSecontds = 0;

            if (dTotalSecontds > System.Int32.MaxValue)
            {
                iTotalSecontds = System.Int32.MaxValue;
            }
            else if (dTotalSecontds < System.Int32.MinValue)
            {
                iTotalSecontds = System.Int32.MinValue;
            }
            else
            {
                iTotalSecontds = (int)dTotalSecontds;
            }


            if (iTotalSecontds > 0)
            {
                minTime = time2;
                maxTime = time1;
            }
            else if (iTotalSecontds < 0)
            {
                minTime = time1;
                maxTime = time2;
            }
            else
            {
                return time1;
            }

            int maxValue = iTotalSecontds;

            if (iTotalSecontds <= System.Int32.MinValue)
                maxValue = System.Int32.MinValue + 1;

            int i = random.Next(Math.Abs(maxValue));

            return minTime.AddSeconds(i);
        }

        /// <summary>
        /// 获取两个时间差：返回 年
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static double GetYearsOfTwoDate(DateTime? startDate, DateTime? endDate)
        {
            if (startDate == null || endDate == null) return 0;
            int months = GetMonthsOfTwoDate(startDate, endDate);

            double totalyears = months / 12d;
            return double.Parse(string.Format("{0:N2}", totalyears));
        }

        /// <summary>
        /// 获取两个时间差：返回 月
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static int GetMonthsOfTwoDate(DateTime? startDate, DateTime? endDate)
        {
            if (startDate == null || endDate == null) return 0;
            int months = ((endDate.Value.Year - startDate.Value.Year) * 12) + (endDate.Value.Month - startDate.Value.Month);
            //如果天数还没到，月数要减1
            if (endDate.Value.Day < startDate.Value.Day)
            {
                months--;
            }
            return months;
        }

        /// <summary>
        /// 获取两个时间差：返回 天
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static string GetDaysOfTwoDate(DateTime? startDate, DateTime? endDate)
        {
            if (startDate == null || endDate == null) return string.Empty;
            TimeSpan TimSpan = endDate.Value.Subtract(startDate.Value);
            return TimSpan.Days.ToString();
        }
    }
}