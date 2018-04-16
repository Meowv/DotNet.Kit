using System.Web;

namespace DotNet.Kit
{
    public class SessionHelper
    {
        /// <summary>
        /// 添加Session
        /// </summary>
        /// <param name="sessionName">Session对象名称</param>
        /// <param name="sessionValue">Session值</param>
        /// <param name="expires">调动有效期（分钟）</param>
        public static void Addsession(string sessionName, string sessionValue, int expires)
        {
            HttpContext.Current.Session[sessionName] = sessionValue;
            HttpContext.Current.Session.Timeout = expires;
        }

        /// <summary>
        /// 添加Session，调动有效期为20分钟
        /// </summary>
        /// <param name="sessionName">Session对象名称</param>
        /// <param name="sessionValue">Session值</param>
        public static void Addsession(string sessionName, string sessionValue)
        {
            Addsession(sessionName, sessionValue, 20);
        }

        /// <summary>
        /// 添加Session数组
        /// </summary>
        /// <param name="sessionName">Session对象名称</param>
        /// <param name="sessionValue">Session对象值</param>
        /// <param name="expires">调动有效期</param>
        public static void Addsessions(string sessionName, string[] sessionValue, int expires)
        {
            HttpContext.Current.Session[sessionName] = sessionValue;
            HttpContext.Current.Session.Timeout = expires;
        }

        /// <summary>
        /// 添加Session数组，调动有效期为20分钟
        /// </summary>
        /// <param name="sessionName">Session对象名称</param>
        /// <param name="sessionValue">Session对象值</param>
        public static void Addsessions(string sessionName, string[] sessionValue)
        {
            Addsessions(sessionName, sessionValue, 20);
        }

        /// <summary>
        /// 读取某个Session对象值
        /// </summary>
        /// <param name="sessionName">Session对象名称</param>
        /// <returns>Session对象值</returns>
        public static string GetSession(string sessionName)
        {
            if (HttpContext.Current.Session[sessionName] == null)
            {
                return null;
            }
            else
            {
                return HttpContext.Current.Session[sessionName].ToString();
            }
        }

        /// <summary>
        /// 读取某个Session对象值数组
        /// </summary>
        /// <param name="sessionName">Session对象名称</param>
        /// <returns>Session对象值数组</returns>
        public static string[] GetSessions(string sessionName)
        {
            if (HttpContext.Current.Session[sessionName] == null)
            {
                return null;
            }
            else
            {
                return (string[])HttpContext.Current.Session[sessionName];
            }
        }

        /// <summary>
        /// 删除指定Session对象
        /// </summary>
        /// <param name="sessionName"></param>
        public static void DelSession(string sessionName)
        {
            HttpContext.Current.Session[sessionName] = null;
            HttpContext.Current.Session.Remove(sessionName);
        }

        /// <summary>
        /// 清空删除所有的Session
        /// </summary>
        public static void ClearSession()
        {
            HttpContext.Current.Session.Clear();
            HttpContext.Current.Session.RemoveAll();
        }
    }
}