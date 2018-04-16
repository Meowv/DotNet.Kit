using System.Data;
using System.IO;
using System.Text;

namespace DotNet.Kit
{
    public class CsvHelper
    {
        /// <summary>
        /// 导出报表为Csv
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <param name="filePath">物理路径</param>
        /// <param name="tableHeader">表头</param>
        /// <param name="columName">字段标题,逗号分隔</param>
        /// <returns></returns>
        public static bool Dt2Csv(DataTable dt, string filePath, string tableHeader, string columName)
        {
            try
            {
                var bufferLine = "";
                var sw = new StreamWriter(filePath, false, Encoding.UTF8);
                sw.WriteLine(tableHeader);
                sw.WriteLine(columName);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    bufferLine = "";
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        if (j > 0)
                        {
                            bufferLine += ",";
                        }
                        bufferLine += dt.Rows[i][j].ToString();
                    }
                    sw.WriteLine(bufferLine);
                }
                sw.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 将Csv读入DataTable
        /// </summary>
        /// <param name="filePath">csv文件路径</param>
        /// <param name="n">表示第n行是字段title,第n+1行是记录开始</param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DataTable Csv2Dt(string filePath, int n, DataTable dt)
        {
            var sr = new StreamReader(filePath, Encoding.UTF8, false);
            int i = 0, m = 0;
            sr.Peek();
            while (sr.Peek() > 0)
            {
                m = m + 1;
                var str = sr.ReadLine();
                if (m >= n + 1)
                {
                    string[] split = str.Split(',');

                    DataRow dr = dt.NewRow();
                    for (i = 0; i < split.Length; i++)
                    {
                        dr[i] = split[i];
                    }
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }
    }
}