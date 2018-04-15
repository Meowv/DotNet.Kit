using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DotNet.Kit
{
    public class JavaScriptHelper
    {
        #region 弹出信息,并跳转指定页面
        /// <summary>
        /// 弹出信息，并跳转指定页面
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="toUrl"></param>
        public static void AlertAndRedirect(string msg, string toUrl)
        {
            string js = "<script>alert('{0}');window.location.replace('{1}')</script>";
            HttpContext.Current.Response.Write(string.Format(js, msg, toUrl));
            HttpContext.Current.Response.End();
        }
        #endregion

        #region 弹出信息,并返回历史页面
        /// <summary>
        /// 弹出信息,并返回历史页面
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="val"></param>
        public static void AlertAndGoHistory(string msg, int val)
        {
            string js = "<script>alert('{0}');history.go({1});</script>";
            HttpContext.Current.Response.Write(string.Format(js, msg, val));
            HttpContext.Current.Response.End();
        }
        #endregion

        #region 弹出信息,并指定到父窗口
        /// <summary>
        /// 弹出信息,并指定到父窗口
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="toUrl"></param>
        public static void AlertAndParentUrl(string msg,string toUrl)
        {
            string js = "<script>alert('{0}');window.top.location.replace('{1}')</script>";
            HttpContext.Current.Response.Write(string.Format(js, msg, toUrl));
        }
        #endregion

        #region 返回到父窗口
        /// <summary>
        /// 返回到父窗口
        /// </summary>
        /// <param name="toUrl"></param>
        public static void ParentRedirect(string toUrl)
        {
            string js = "<script>window.top.location.replace('{0}')</script>";
            HttpContext.Current.Response.Write(string.Format(js, toUrl));
        }
        #endregion

        #region 弹出信息
        /// <summary>
        /// 弹出信息
        /// </summary>
        /// <param name="msg"></param>
        public static void Alert(string msg)
        {
            string js = "<script>alert('{0}');</script>";
            HttpContext.Current.Response.Write(string.Format(js, msg));
        }
        #endregion

        #region 注册脚本快
        /// <summary>
        /// 注册脚本快
        /// </summary>
        /// <param name="page"></param>
        /// <param name="script"></param>
        public static void RegisterScriptBlock(Page page, string script)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "scriptblock", "<script>" + script + "</script>");
        }
        #endregion

        #region 返回把指定链接地址显示模态窗口的脚本
        /// <summary>
        /// 返回把指定链接地址显示模态窗口的脚本
        /// </summary>
        /// <param name="wid"></param>
        /// <param name="title"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetShowModalWindowScript(string wid, string title, int width, int height, string url)
        {
            return string.Format("setTimeout(\"showModalWindow('{0}','{1}',{2},{3},'{4}')\",100);", wid, title, width, height, url);
        }

        /// <summary>
        /// 把指定链接地址显示模态窗口
        /// </summary>
        /// <param name="wid"></param>
        /// <param name="title"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="url"></param>
        public static void ShowModalWindow(string wid,string title,int width,int height,string url)
        {
            WriteScript(GetShowModalWindowScript(wid, title, width, height, url));
        }

        /// <summary>
        /// 为指定控件绑定前台脚本：显示模态窗口
        /// </summary>
        /// <param name="wid"></param>
        /// <param name="control"></param>
        /// <param name="eventName"></param>
        /// <param name="title"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="url"></param>
        /// <param name="isScriptEnd"></param>
        public static void ShowClientModalWindow(string wid, WebControl control, string eventName, string title, int width, int height, string url, bool isScriptEnd)
        {
            string script = isScriptEnd ? "return false;" : "";
            control.Attributes[eventName] = string.Format("showModalWindow('{0}','{1}',{2},{3},'{4}');" + script, wid, title, width, height, url);
        }
        #endregion

        #region 写javascript脚本
        /// <summary>
        /// 写javascript脚本
        /// </summary>
        /// <param name="script"></param>
        public static void WriteScript(string script)
        {
            Page page = GetCurrentPage();
            page.ClientScript.RegisterStartupScript(page.GetType(), Guid.NewGuid().ToString(), script, true);
        }

        /// <summary>
        /// 得到当前页对象实例
        /// </summary>
        /// <returns></returns>
        public static Page GetCurrentPage()
        {
            return (Page)HttpContext.Current.Handler;
        }
        #endregion

        #region 显示客户端确认窗口
        public static void ShowCilentConfirm(WebControl control, string eventName, string message)
        {
            ShowCilentConfirm(control, eventName, "系统提示", 210, 125, message);
        }

        /// <summary>
        /// 显示客户端确认窗口
        /// </summary>
        /// <param name="control"></param>
        /// <param name="eventName"></param>
        /// <param name="title"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="message"></param>
        public static void ShowCilentConfirm(WebControl control, string eventName, string title, int width, int height, string message)
        {
            control.Attributes[eventName] = string.Format("return showConfirm('{0}',{1},{2},'{3}','{4}');", title, width, height, message, control.ClientID);
        }
        #endregion

        #region 回到历史页面
        /// <summary>
        /// 回到历史页面
        /// </summary>
        /// <param name="val"></param>
        public static void GoHistory(int val)
        {
            string js = "<script>history.go({0})</script>";
            HttpContext.Current.Response.Write(string.Format(js, val));
            HttpContext.Current.Response.End();
        }
        #endregion

        #region 关闭当前窗口
        /// <summary>
        /// 关闭当前窗口
        /// </summary>
        public static void CloseWindow()
        {
            string js = "<script>parent.opener=null;window.close();</script>";
            HttpContext.Current.Response.Write(js);
            HttpContext.Current.Response.End();
        }
        #endregion

        #region 刷新父窗口
        /// <summary>
        /// 刷新父窗口
        /// </summary>
        /// <param name="url"></param>
        public static void RefreshParent(string url)
        {
            string js = @"<script>try{top.location=""" + url + @"""}catch(e){location=""" + url + @"""}</script>";
            HttpContext.Current.Response.Write(js);
            HttpContext.Current.Response.End();
        }
        #endregion

        #region 刷新打开父窗口
        /// <summary>
        /// 刷新打开父窗口
        /// </summary>
        public static void RefreshOpener()
        {
            string js = "<script>opener.location.reload();</script>";
            HttpContext.Current.Response.Write(js);
            HttpContext.Current.Response.End();
        }
        #endregion

        #region 转向Url指定的页面
        /// <summary>
        /// 转向Url指定的页面
        /// </summary>
        /// <param name="url"></param>
        public static void JavaScriptLocationHref(string url)
        {
            string js = string.Format("<script>window.location.replace('{0}');</script>", url);
            HttpContext.Current.Response.Write(js);
            HttpContext.Current.Response.End();
        }
        #endregion

        #region 打开指定大小位置的模式对话框
        /// <summary>
        /// 打开指定大小位置的模式对话框
        /// </summary>
        /// <param name="webFormUrl">连接地址</param>
        /// <param name="width">宽</param>
        /// <param name="height">高</param>
        /// <param name="top">距离上位置</param>
        /// <param name="left">距离左位置</param>
        public static void ShowModalDialogWindow(string webFormUrl, int width, int height, int top, int left)
        {
            string features = "dialogWidth:" + width.ToString() + "px"
               + ";dialogHeight:" + height.ToString() + "px"
               + ";dialogLeft:" + left.ToString() + "px"
               + ";dialogTop:" + top.ToString() + "px"
               + ";center:yes;help=no;resizable:no;status:no;scroll=yes";
            ShowModalDialogWindow(webFormUrl, features);
        }
        #endregion

        #region 打开模式对话框
        /// <summary>
        /// 打开模式对话框
        /// </summary>
        /// <param name="webFormUrl">连接地址</param>
        /// <param name="features"></param>
        public static void ShowModalDialogWindow(string webFormUrl, string features)
        {
            string js = ShowModalDialogJavascript(webFormUrl, features);
            HttpContext.Current.Response.Write(js);
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// 打开模式对话框
        /// </summary>
        /// <param name="webFormUrl">连接地址</param>
        /// <param name="features"></param>
        /// <returns></returns>
        public static string ShowModalDialogJavascript(string webFormUrl, string features)
        {
            string js = @"<script>showModalDialog('" + webFormUrl + "','','" + features + "');</script>";
            return js;
        }
        #endregion

        #region 打开指定大小的新窗体
        /// <summary>
        /// 打开指定大小的新窗体
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="width">宽</param>
        /// <param name="heigth">高</param>
        /// <param name="top">距离上位置</param>
        /// <param name="left">距离左位置</param>
        public static void OpenWebFormSize(string url, int width, int heigth, int top, int left)
        {
            string js = @"<script>window.open('" + url + @"','','height=" + heigth + ",width=" + width + ",top=" + top + ",left=" + left + ",location=no,menubar=no,resizable=yes,scrollbars=yes,status=yes,titlebar=no,toolbar=no,directories=no');</script>";
            HttpContext.Current.Response.Write(js);
            HttpContext.Current.Response.End();
        }
        #endregion

        #region 页面跳转出框架
        /// <summary>
        /// 页面跳转出框架
        /// </summary>
        /// <param name="url"></param>
        public static void JavaScriptExitIfream(string url)
        {
            string js = string.Format("<script>parent.window.location.replace('{0}');</script>", url);
            HttpContext.Current.Response.Write(js);
            HttpContext.Current.Response.End();
        }
        #endregion
    }
}