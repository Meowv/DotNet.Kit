using System.Web;

namespace DotNet.Kit
{
    public class IpHelper
    {
        /// <summary>
        /// 获得用户IP
        /// </summary>
        /// <returns></returns>
        public static string GetUserIp()
        {
            string ip;
            string[] temp;
            bool isErr = false;
            if (HttpContext.Current.Request.ServerVariables["HTTP_X_ForWARDED_For"] == null)
            {
                ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
            }
            else
            {
                ip = HttpContext.Current.Request.ServerVariables["HTTP_X_ForWARDED_For"].ToString();
            }

            if (ip.Length > 15)
            {
                isErr = true;
            }
            else
            {
                temp = ip.Split('.');
                if (temp.Length == 4)
                {
                    for (int i = 0; i < temp.Length; i++)
                    {
                        if (temp[i].Length > 3)
                        {
                            isErr = true;
                        }
                    }
                }
                else
                {
                    isErr = true;
                }
            }

            if (isErr)
            {
                return "1.1.1.1";
            }
            else
            {
                return ip;
            }
        }
    }
}
