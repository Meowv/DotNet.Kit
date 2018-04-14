using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;

namespace DotNet.Kit
{
    public class HtmlHelper
    {
        #region 私有字段
        private static CookieContainer cc = new CookieContainer();
        private static string contentType = "application/x-www-form-urlencoded";
        private static string accept = "image/gif, image/x-xbitmap, image/jpeg, image/pjpeg, application/x-shockwave-flash, application/x-silverlight, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, application/x-ms-application, application/x-ms-xbap, application/vnd.ms-xpsdocument, application/xaml+xml, application/x-silverlight-2-b1, */*";
        private static string userAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1; .NET CLR 2.0.50727; .NET CLR 3.0.04506.648; .NET CLR 3.5.21022)";
        private static Encoding encoding = Encoding.GetEncoding("utf-8");
        private static int delay = 1000;
        private static int maxTry = 300;
        private static int currentTry = 0;
        #endregion

        #region 公有属性
        public static CookieContainer CookieContainer
        {
            get
            {
                return cc;
            }
        }

        public static Encoding Encoding
        {
            get
            {
                return encoding;
            }
            set
            {
                encoding = value;
            }
        }

        public static int NetworkDelay
        {
            get
            {
                Random r = new Random();
                return (r.Next(delay, delay * 2));
            }
            set
            {
                delay = value;
            }
        }

        public static int MaxTry
        {
            get
            {
                return maxTry;
            }
            set
            {
                maxTry = value;
            }
        }
        #endregion

        #region 获取HTML
        /// <summary>
        /// 获取HTML
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="postData">post 提交的字符串</param>
        /// <param name="isPost">是否post</param>
        /// <param name="cookieContainer">CookieContainer</param>
        /// <returns></returns>
        public static string GetHtml(string url, string postData, bool isPost, CookieContainer cookieContainer)
        {
            if (string.IsNullOrEmpty(postData))
            {
                return GetHtml(url, cookieContainer);
            }
            Thread.Sleep(NetworkDelay);
            currentTry++;
            HttpWebRequest httpWebRequest = null;
            HttpWebResponse httpWebResponse = null;
            try
            {
                byte[] byteRequest = Encoding.Default.GetBytes(postData);
                httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.CookieContainer = cookieContainer;
                httpWebRequest.ContentType = contentType;
                httpWebRequest.ServicePoint.ConnectionLimit = maxTry;
                httpWebRequest.Referer = url;
                httpWebRequest.Accept = accept;
                httpWebRequest.UserAgent = userAgent;
                httpWebRequest.Method = isPost ? "POST" : "GET";
                httpWebRequest.ContentLength = byteRequest.Length;
                Stream stream = httpWebRequest.GetRequestStream();
                stream.Write(byteRequest, 0, byteRequest.Length);
                stream.Close();
                httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                Stream responseStream = httpWebResponse.GetResponseStream();
                StreamReader streamReader = new StreamReader(responseStream, encoding);
                string html = streamReader.ReadToEnd();
                streamReader.Close();
                responseStream.Close();
                currentTry = 0;
                httpWebRequest.Abort();
                httpWebResponse.Close();
                return html;
            }
            catch (Exception)
            {
                if (currentTry <= maxTry)
                {
                    GetHtml(url, postData, isPost, cookieContainer);
                }
                currentTry--;
                if (httpWebRequest != null)
                {
                    httpWebRequest.Abort();
                }
                if (httpWebResponse != null)
                {
                    httpWebResponse.Close();
                }
                return string.Empty;
            }
        }

        /// <summary>
        /// 获取HTML
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="cookieContainer">cookieContainer</param>
        /// <returns></returns>
        public static string GetHtml(string url, CookieContainer cookieContainer)
        {
            Thread.Sleep(NetworkDelay);
            currentTry++;
            HttpWebRequest httpWebRequest = null;
            HttpWebResponse httpWebResponse = null;
            try
            {
                httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.CookieContainer = cookieContainer;
                httpWebRequest.ContentType = contentType;
                httpWebRequest.ServicePoint.ConnectionLimit = maxTry;
                httpWebRequest.Referer = url;
                httpWebRequest.Accept = accept;
                httpWebRequest.UserAgent = userAgent;
                httpWebRequest.Method = "GET";
                httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                Stream responseStream = httpWebResponse.GetResponseStream();
                StreamReader streamReader = new StreamReader(responseStream, encoding);
                string html = streamReader.ReadToEnd();
                streamReader.Close();
                responseStream.Close();
                currentTry--;
                httpWebRequest.Abort();
                httpWebResponse.Close();
                return html;
            }
            catch (Exception)
            {
                if (currentTry <= maxTry)
                {
                    GetHtml(url, cookieContainer);
                }
                currentTry--;
                if (httpWebRequest != null)
                {
                    httpWebRequest.Abort();
                }
                if (httpWebResponse != null)
                {
                    httpWebResponse.Close();
                }
                return string.Empty;
            }
        }
        #endregion

        #region 获取字符流
        /// <summary>
        /// 获取字符流
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="cookieContainer">cookieContainer</param>
        /// 
        /// <returns></returns>
        public static Stream GetStream(string url, CookieContainer cookieContainer)
        {
            currentTry++;

            HttpWebRequest httpWebRequest = null;
            HttpWebResponse httpWebResponse = null;

            try
            {
                httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);
                httpWebRequest.CookieContainer = cookieContainer;
                httpWebRequest.ContentType = contentType;
                httpWebRequest.ServicePoint.ConnectionLimit = maxTry;
                httpWebRequest.Referer = url;
                httpWebRequest.Accept = accept;
                httpWebRequest.UserAgent = userAgent;
                httpWebRequest.Method = "GET";

                httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                Stream responseStream = httpWebResponse.GetResponseStream();
                currentTry--;
                return responseStream;
            }
            catch (Exception)
            {
                if (currentTry <= maxTry)
                {
                    GetHtml(url, cookieContainer);
                }

                currentTry--;

                if (httpWebRequest != null)
                {
                    httpWebRequest.Abort();
                }
                if (httpWebResponse != null)
                {
                    httpWebResponse.Close();
                }
                return null;
            }
        }
        #endregion

        #region 清除HTML标记
        /// <summary>
        /// 清除HTML标记
        /// </summary>
        /// <param name="Htmlstring">包括HTML的源码</param>
        /// <returns>已经去除后的文本</returns>
        public static string NoHTML(string Htmlstring)
        {
            //删除脚本   
            Htmlstring = Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);

            //删除HTML   
            Regex regex = new Regex("<.+?>", RegexOptions.IgnoreCase);
            Htmlstring = regex.Replace(Htmlstring, "");
            Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);

            Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", "   ", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", "", RegexOptions.IgnoreCase);

            Htmlstring.Replace("<", "");
            Htmlstring.Replace(">", "");
            Htmlstring.Replace("\r\n", "");

            return Htmlstring;
        }
        #endregion

        #region 删除文本中带的HTML标记
        /// <summary>
        /// 删除文本中带的HTML标记
        /// </summary>
        /// <param name="InString">输入要删除带HTML的字符串</param>
        /// <returns>返回处理过的字符串</returns>
        public static string DelHtmlCode(string InString)
        {
            string strTemp = InString;
            int htmlBeginNum = 0;
            int htmlEndNum = 0;
            while (strTemp.Contains("<"))
            {
                if (!strTemp.Contains(">")) { break; } //当字符串内不包含">"时退出循环
                htmlBeginNum = strTemp.IndexOf("<");
                htmlEndNum = strTemp.IndexOf(">");

                //删除从"<"到">"之间的所有字符串
                strTemp = strTemp.Remove(htmlBeginNum, htmlEndNum - htmlBeginNum + 1);
            }
            strTemp = strTemp.Replace("\n", "");
            strTemp = strTemp.Replace("\r", "");
            strTemp = strTemp.Replace("\n\r", "");
            strTemp = strTemp.Replace("&nbsp;", "");
            strTemp = strTemp.Replace(" ", "");
            strTemp = strTemp.Trim();
            return strTemp;
        }
        #endregion

        #region 匹配页面的链接
        /// <summary>
        /// 匹配页面的链接
        /// </summary>
        /// <param name="HtmlCode"></param>
        /// <returns></returns>
        public string GetHref(string HtmlCode)
        {
            string MatchVale = "";
            string Reg = @"(h|H)(r|R)(e|E)(f|F) *= *('|"")?((\w|\\|\/|\.|:|-|_)+)[\S]*";
            foreach (Match m in Regex.Matches(HtmlCode, Reg))
            {
                MatchVale += (m.Value).ToLower().Replace("href=", "").Trim() + "|";
            }
            return MatchVale;
        }
        #endregion

        #region 匹配页面的图片地址
        /// <summary>
        /// 匹配页面的图片地址
        /// </summary>
        /// <param name="HtmlCode"></param>
        /// <param name="imgHttp"></param>
        /// <returns></returns>
        public static string GetImgSrc(string HtmlCode, string imgHttp)
        {
            string MatchVale = "";
            string Reg = @"<img.+?>";
            foreach (Match m in Regex.Matches(HtmlCode.ToLower(), Reg))
            {
                MatchVale += GetImg((m.Value).ToLower().Trim(), imgHttp) + "|";
            }

            return MatchVale;
        }

        /// <summary>
        /// 匹配<img src="" />中的图片路径实际链接
        /// </summary>
        /// <param name="ImgString">字符串</param>
        /// <param name="imgHttp"></param>
        /// <returns></returns>
        public static string GetImg(string ImgString, string imgHttp)
        {
            string MatchVale = "";
            string Reg = @"src=.+\.(bmp|jpg|gif|png|)";
            foreach (Match m in Regex.Matches(ImgString.ToLower(), Reg))
            {
                MatchVale += (m.Value).ToLower().Trim().Replace("src=", "");
            }
            if (MatchVale.IndexOf(".net") != -1 || MatchVale.IndexOf(".com") != -1 || MatchVale.IndexOf(".org") != -1 || MatchVale.IndexOf(".cn") != -1 || MatchVale.IndexOf(".cc") != -1 || MatchVale.IndexOf(".info") != -1 || MatchVale.IndexOf(".biz") != -1 || MatchVale.IndexOf(".tv") != -1)
                return (MatchVale);
            else
                return (imgHttp + MatchVale);
        }
        #endregion

        #region 压缩HTML输出
        /// <summary>
        /// 压缩HTML输出
        /// </summary>
        /// <param name="Html"></param>
        /// <returns></returns>
        public static string ZipHtml(string Html)
        {
            Html = Regex.Replace(Html, @">\s+?<", "><");
            Html = Regex.Replace(Html, @"\r\n\s*", "");
            Html = Regex.Replace(Html, @"<body([\s|\S]*?)>([\s|\S]*?)</body>", @"<body$1>$2</body>", RegexOptions.IgnoreCase);
            return Html;
        }
        #endregion

        #region 过滤指定HTML标签
        /// <summary>
        /// 过滤指定HTML标签
        /// </summary>
        /// <param name="s_TextStr">要过滤的字符</param>
        /// <param name="html_Str">a img p div</param>
        /// <returns></returns>
        public static string DelHtml(string s_TextStr, string html_Str)
        {
            string rStr = "";
            if (!string.IsNullOrEmpty(s_TextStr))
            {
                rStr = Regex.Replace(s_TextStr, "<" + html_Str + "[^>]*>", "", RegexOptions.IgnoreCase);
                rStr = Regex.Replace(rStr, "</" + html_Str + ">", "", RegexOptions.IgnoreCase);
            }
            return rStr;
        }
        #endregion

        #region 加载文件块
        /// <summary>
        /// 加载文件块
        /// </summary>
        /// <param name="Path"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public static string File(string Path, System.Web.UI.Page p)
        {
            return @p.ResolveUrl(Path);
        }
        #endregion

        #region 加载CSS样式文件
        /// <summary>
        /// 加载CSS样式文件
        /// </summary>
        /// <param name="cssPath"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public static string CSS(string cssPath, System.Web.UI.Page p)
        {
            return @"<link href=""" + p.ResolveUrl(cssPath) + @""" rel=""stylesheet"" type=""text/css"" />" + "\r\n";
        }
        #endregion

        #region 加载JavaScript脚本文件
        /// <summary>
        /// 加载JavaScript脚本文件
        /// </summary>
        /// <param name="jsPath"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public static string JS(string jsPath, System.Web.UI.Page p)
        {
            return @"<script src=""" + p.ResolveUrl(jsPath) + @"""></script>" + "\r\n";
        }
        #endregion

        #region URL编码解码
        /// <summary>
        /// 解码Url
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string UrlDecode(string url)
        {
            return HttpContext.Current.Server.UrlDecode(url);
        }

        /// <summary>
        /// 编码Url
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string UrlEncode(string url)
        {
            return HttpContext.Current.Server.UrlEncode(url);
        }
        #endregion

        #region Html编码解码
        /// <summary>
        /// 解码Html
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string HtmlDecode(string html)
        {
            return HttpContext.Current.Server.HtmlDecode(html);
        }

        /// <summary>
        /// 编码Html
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string HtmlEncode(string html)
        {
            return HttpContext.Current.Server.HtmlEncode(html);
        }
        #endregion
    }
}