using System.Data;

namespace DotNet.Kit.Dapper
{
    /// <summary>
    /// 描述一个读取器，该读取器控制命令和读取器的生存期，并将下游命令/读取器公开为属性。
    /// </summary>
    public interface IWrappedDataReader : IDataReader
    {
        /// <summary>
        /// 获取基础读取器
        /// </summary>
        IDataReader Reader { get; }
        /// <summary>
        /// 获取基础命令
        /// </summary>
        IDbCommand Command { get; }
    }
}
