using System.Configuration;

namespace DotNet.Kit
{
    public class ConfigHelper
    {
        /// <summary>
        /// 获取Web.Config connectionStrings 配置的数据库连接
        /// </summary>
        /// <param name="key">配置的数据库连接名称</param>
        /// <returns>连接字符串</returns>
        public static string GetConnectionString(string key)
        {
            return ConfigurationManager.ConnectionStrings[key].ConnectionString;
        }

        /// <summary>
        /// 获取Web.Config AppSettings 配置节的信息
        /// </summary>
        /// <param name="key">key</param>
        /// <returns>appSettings</returns>
        public static string GetAppSettings(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        /// <summary>
        /// 获取Web.Config AppSettings 配置节的信息,提供默认值
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="defaultValue">defaultValue</param>
        /// <returns>GetAppSettings</returns>
        public static string GetAppSettings(string key, string defaultValue)
        {
            try
            {
                return GetAppSettings(key) ?? defaultValue;
            }
            catch
            {
                return defaultValue;
            }
        }
    }
}