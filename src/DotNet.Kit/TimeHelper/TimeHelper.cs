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
    }
}