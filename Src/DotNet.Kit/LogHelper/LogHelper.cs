﻿using System;
using System.IO;
using System.Web;

namespace DotNet.Kit.LogHelper
{
    public class LogHelper
    {
        /// <summary>
        /// 向日志文件写入调试信息
        /// </summary>
        /// <param name="className">类名</param>
        /// <param name="content">写入内容</param>
        public static void Debug(string className, string content)
        {
            WriteLog("DEBUG", className, content);
        }

        /// <summary>
        /// 向日志文件写入运行时信息
        /// </summary>
        /// <param name="className">类名</param>
        /// <param name="content">写入内容</param>
        public static void Info(string className, string content)
        {
            WriteLog("INFO", className, content);
        }

        /// <summary>
        /// 向日志文件写入出错信息
        /// </summary>
        /// <param name="className">类名</param>
        /// <param name="content">写入内容</param>
        public static void Error(string className, string content)
        {
            WriteLog("ERROR", className, content);
        }

        /// <summary>
        /// 实际的写日志操作
        /// </summary>
        /// <param name="type">日志记录类型</param>
        /// <param name="className">类名</param>
        /// <param name="content">写入内容</param>
        protected static void WriteLog(string type,string className,string content)
        {
            //在网站根目录下创建logs目录
            var path = HttpContext.Current.Request.PhysicalApplicationPath + "logs";

            //如果logs目录不存在就创建
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            //获取当前系统时间
            var time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            //用日期对日志文件命名
            var logName = $"{path}/{DateTime.Now.ToString("yyyy-MM-dd")}.log";

            //创建打开日志文件，向日志文件添加记录
            var sw = File.AppendText(logName);

            //向日志文件写入内容
            var write_content = $"{time} {type} {className} : {content}";
            sw.WriteLine(write_content);

            //关闭日志文件
            sw.Close();
        }
    }
}