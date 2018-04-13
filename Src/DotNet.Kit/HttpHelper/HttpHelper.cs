using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace DotNet.Kit.HttpHelper
{
    public class HttpHelper
    {
        /// <summary>
        /// HttpGet
        /// </summary>
        /// <param name="url">url地址</param>
        /// <returns>HttpGet</returns>
        public static string Get(string url)
        {
            var res = "";
            try
            {
                var client = new WebClient { Encoding = Encoding.UTF8 };
                res = client.DownloadString(url);
            }
            catch (Exception e)
            {
                res = e.Message;
            }

            return res;
        }

        /// <summary>
        /// HttpPost
        /// </summary>
        /// <param name="url">url地址</param>
        /// <param name="dic">post参数</param>
        /// <returns>HttpPost</returns>
        public static string Post(string url, Dictionary<string, string> dic)
        {
            var result = "";
            try
            {
                var req = (HttpWebRequest)WebRequest.Create(url);
                req.Accept = "text/html";
                req.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/65.0.3325.146 Safari/537.36";
                req.Method = "POST";
                req.ContentType = "application/x-www-form-urlencoded";

                var builder = new StringBuilder();
                int i = 0;
                dic.ToList().ForEach(x =>
                {
                    if (i > 0)
                    {
                        builder.Append("&");
                    }
                    builder.AppendFormat("{0}={1}", x.Key, x.Value);
                    i++;
                });
                var data = Encoding.UTF8.GetBytes(builder.ToString());
                req.ContentLength = data.Length;
                using (var reqStream = req.GetRequestStream())
                {
                    reqStream.Write(data, 0, data.Length);
                    reqStream.Close();
                }

                var res = (HttpWebResponse)req.GetResponse();
                var stream = res.GetResponseStream();
                using (var reader = new StreamReader(stream, Encoding.UTF8))
                {
                    result = reader.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                result = e.Message;
            }

            return result;
        }
    }
}