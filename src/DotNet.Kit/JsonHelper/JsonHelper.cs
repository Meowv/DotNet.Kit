using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace DotNet.Kit
{
    public class JsonHelper
    {
        #region List转换为Json
        /// <summary>
        /// List转换为Json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static string ListToJson<T>(IList<T> list)
        {
            var obj = list[0];
            return ListToJson<T>(list, obj.GetType().Name);
        }

        /// <summary>
        /// List转换成Json 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="jsonName"></param>
        /// <returns></returns>
        public static string ListToJson<T>(IList<T> list, string jsonName)
        {
            var json = new StringBuilder();
            if (string.IsNullOrEmpty(jsonName))
            {
                jsonName = list[0].GetType().Name;
            }
            json.Append("{\"" + jsonName + "\":[");
            if (list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    var obj = Activator.CreateInstance<T>();
                    var pi = obj.GetType().GetProperties();
                    json.Append("{");
                    for (int j = 0; j < pi.Length; j++)
                    {
                        var type = pi[j].GetValue(list[i], null).GetType();
                        json.Append("\"" + pi[j].Name.ToString() + "\":" + StringFormat(pi[j].GetValue(list[i], null).ToString(), type));

                        if (j < pi.Length - 1)
                        {
                            json.Append(",");
                        }
                    }
                    json.Append("}");
                    if (i < list.Count - 1)
                    {
                        json.Append(",");
                    }
                }
            }
            json.Append("]}");
            return json.ToString();
        }
        #endregion

        #region 对象转换为Json
        /// <summary>
        /// 对象转换为Json 
        /// </summary>
        /// <param name="jsonObject">对象</param>
        /// <returns>Json字符串</returns>
        public static string ToJson(object jsonObject)
        {
            var json = "{";
            var propertyInfo = jsonObject.GetType().GetProperties();
            for (int i = 0; i < propertyInfo.Length; i++)
            {
                var objectValue = propertyInfo[i].GetGetMethod().Invoke(jsonObject, null);
                var value = string.Empty;
                if (objectValue is DateTime || objectValue is Guid || objectValue is TimeSpan)
                {
                    value = "'" + objectValue.ToString() + "'";
                }
                else if (objectValue is string)
                {
                    value = "'" + ToJson(objectValue.ToString()) + "'";
                }
                else if (objectValue is IEnumerable)
                {
                    value = ToJson((IEnumerable)objectValue);
                }
                else
                {
                    value = ToJson(objectValue.ToString());
                }
                json += "\"" + ToJson(propertyInfo[i].Name) + "\":" + value + ",";
            }
            json.Remove(json.Length - 1, json.Length);
            return json + "}";
        }
        #endregion

        #region 集合对象转换为Json
        /// <summary>
        /// 集合对象转换为Json 
        /// </summary>
        /// <param name="array">集合对象</param>
        /// <returns>Json字符串</returns>
        public static string ToJson(IEnumerable array)
        {
            var json = "[";
            foreach (object item in array)
            {
                json += ToJson(item) + ",";
            }
            json.Remove(json.Length - 1, json.Length);
            return json + "]";
        }
        #endregion

        #region DataSet转换为Json 
        /// <summary>
        /// DataSet转换为Json 
        /// </summary>
        /// <param name="dataSet">DataSet对象</param>
        /// <returns>Json字符串</returns>
        public static string ToJson(DataSet dataSet)
        {
            string json = "{";
            foreach (DataTable table in dataSet.Tables)
            {
                json += "\"" + table.TableName + "\":" + ToJson(table) + ",";
            }
            json = json.TrimEnd(',');
            return json + "}";
        }
        #endregion

        #region Datatable转换为Json
        /// <summary>
        /// Datatable转换为Json 
        /// </summary>
        /// <param name="dt">Datatable对象</param>
        /// <returns>Json字符串</returns>
        public static string ToJson(DataTable dt)
        {
            var json = new StringBuilder();
            json.Append("[");
            var drc = dt.Rows;
            for (int i = 0; i < drc.Count; i++)
            {
                json.Append("{");
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    var strKey = dt.Columns[j].ColumnName;
                    var strValue = drc[i][j].ToString();
                    var type = dt.Columns[j].DataType;
                    json.Append("\"" + strKey + "\":");
                    strValue = StringFormat(strValue, type);
                    if (j < dt.Columns.Count - 1)
                    {
                        json.Append(strValue + ",");
                    }
                    else
                    {
                        json.Append(strValue);
                    }
                }
                json.Append("},");
            }
            json.Remove(json.Length - 1, 1);
            json.Append("]");
            return json.ToString();
        }

        /// <summary>
        /// DataTable转换为Json 
        /// </summary>
        /// <param name="dt">Datatable对象</param>
        /// <param name="jsonName"></param>
        /// <returns>Json字符串</returns>
        public static string ToJson(DataTable dt, string jsonName)
        {
            var Json = new StringBuilder();
            if (string.IsNullOrEmpty(jsonName)) jsonName = dt.TableName;
            Json.Append("{\"" + jsonName + "\":[");
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Json.Append("{");
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        var type = dt.Rows[i][j].GetType();
                        Json.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":" + StringFormat(dt.Rows[i][j].ToString(), type));
                        if (j < dt.Columns.Count - 1)
                        {
                            Json.Append(",");
                        }
                    }
                    Json.Append("}");
                    if (i < dt.Rows.Count - 1)
                    {
                        Json.Append(",");
                    }
                }
            }
            Json.Append("]}");
            return Json.ToString();
        }
        #endregion

        #region DataReader转换为Json
        /// <summary>
        /// DataReader转换为Json 
        /// </summary>
        /// <param name="dataReader">DataReader对象</param>
        /// <returns>Json字符串</returns>
        public static string ToJson(DbDataReader dataReader)
        {
            var json = new StringBuilder();
            json.Append("[");
            while (dataReader.Read())
            {
                json.Append("{");
                for (int i = 0; i < dataReader.FieldCount; i++)
                {
                    var type = dataReader.GetFieldType(i);
                    var strKey = dataReader.GetName(i);
                    var strValue = dataReader[i].ToString();
                    json.Append("\"" + strKey + "\":");
                    strValue = StringFormat(strValue, type);
                    if (i < dataReader.FieldCount - 1)
                    {
                        json.Append(strValue + ",");
                    }
                    else
                    {
                        json.Append(strValue);
                    }
                }
                json.Append("},");
            }
            dataReader.Close();
            json.Remove(json.Length - 1, 1);
            json.Append("]");
            return json.ToString();
        }
        #endregion

        #region JSON转换为对象实体
        /// <summary>
        /// JSON转换为对象实体
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="json">json字符串(eg.{"Id":"qix","Name":"阿星Plus"})</param>
        /// <returns>对象实体</returns>
        public static T DeserializeJsonToObject<T>(string json) where T : class
        {
            var serializer = new JsonSerializer();
            var sr = new StringReader(json);
            var o = serializer.Deserialize(new JsonTextReader(sr), typeof(T));
            var t = o as T;
            return t;
        }
        #endregion

        #region JSON转换为对象实体集合
        /// <summary>
        /// JSON转换为对象实体集合
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="json">json字符串(eg.{"Id":"qix","Name":"阿星Plus"})</param>
        /// <returns>对象实体集合</returns>
        public static List<T> DeserializeJsonToList<T>(string json) where T : class
        {
            var serializer = new JsonSerializer();
            var sr = new StringReader(json);
            var o = serializer.Deserialize(new JsonTextReader(sr), typeof(List<T>));
            var list = o as List<T>;
            return list;
        }
        #endregion

        #region 反序列化JSON到给定的匿名对象
        /// <summary>
        /// 反序列化JSON到给定的匿名对象
        /// </summary>
        /// <typeparam name="T">匿名对象类型</typeparam>
        /// <param name="json">json字符串</param>
        /// <param name="anonymousTypeObject">匿名对象</param>
        /// <returns>匿名对象</returns>
        public static T DeserializeAnonymousType<T>(string json, T anonymousTypeObject)
        {
            var t = JsonConvert.DeserializeAnonymousType(json, anonymousTypeObject);
            return t;
        }

        #endregion

        #region 私有方法
        /// <summary>
        /// 格式化字符型、日期型、布尔型
        /// </summary>
        /// <param name="str"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private static string StringFormat(string str, Type type)
        {
            if (type == typeof(string))
            {
                str = String2Json(str);
                str = "\"" + str + "\"";
            }
            else if (type == typeof(DateTime))
            {
                str = "\"" + str + "\"";
            }
            else if (type == typeof(bool))
            {
                str = str.ToLower();
            }
            else if (type != typeof(string) && string.IsNullOrEmpty(str))
            {
                str = "\"" + str + "\"";
            }
            return str;
        }

        /// <summary>
        /// 过滤特殊字符
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private static string String2Json(string s)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < s.Length; i++)
            {
                char c = s.ToCharArray()[i];
                switch (c)
                {
                    case '\"':
                        sb.Append("\\\""); break;
                    case '\\':
                        sb.Append("\\\\"); break;
                    case '/':
                        sb.Append("\\/"); break;
                    case '\b':
                        sb.Append("\\b"); break;
                    case '\f':
                        sb.Append("\\f"); break;
                    case '\n':
                        sb.Append("\\n"); break;
                    case '\r':
                        sb.Append("\\r"); break;
                    case '\t':
                        sb.Append("\\t"); break;
                    default:
                        sb.Append(c); break;
                }
            }
            return sb.ToString();
        }
        #endregion
    }
}