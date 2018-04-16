using System;
using System.Web;

namespace DotNet.Kit
{
    public class CookieHelper
    {
        /// <summary>
        /// 添加一个cookie
        /// </summary>
        /// <param name="cookieName">cookie名</param>
        /// <param name="cookieValue">cookie值</param>
        /// <param name="expires">过期时间 DateTime</param>
        public static void SetCookie(string cookieName, string cookieValue, DateTime expires)
        {
            HttpCookie cookie = new HttpCookie(cookieName)
            {
                Value = cookieValue,
                Expires = expires,
            };
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        /// <summary>
        /// 添加一个Cookie（24小时过期）
        /// </summary>
        /// <param name="cookieName">cookie名</param>
        /// <param name="cookieValue">cookie值</param>
        public static void SetCookie(string cookieName, string cookieValue)
        {
            SetCookie(cookieName, cookieValue, DateTime.Now.AddDays(1.0));
        }

        /// <summary>
        /// 获取指定Cookie值
        /// </summary>
        /// <param name="cookieName">cookie名</param>
        /// <returns></returns>
        public static string GetCookieValue(string cookieName)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[cookieName];
            string str = string.Empty;
            if (cookie != null)
            {
                str = HttpUtility.UrlDecode(cookie.Value);
            }
            return str;
        }

        /// <summary>
        /// 清除指定Cookie
        /// </summary>
        /// <param name="cookieName">cookie名</param>
        public static void ClearCookie(string cookieName)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[cookieName];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddYears(-3);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }
    }
}
