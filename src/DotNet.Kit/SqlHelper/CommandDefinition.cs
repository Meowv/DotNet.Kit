using System;
using System.Data;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading;

namespace DotNet.Kit.Dapper
{
    /// <summary>
    /// 表示sql操作的关键方面
    /// </summary>
    public struct CommandDefinition
    {
        internal static CommandDefinition ForCallback(object parameters)
        {
            if (parameters is DynamicParameters)
            {
                return new CommandDefinition(parameters);
            }
            else
            {
                return default(CommandDefinition);
            }
        }

        internal void OnCompleted()
        {
            (Parameters as SqlMapper.IParameterCallbacks)?.OnCompleted();
        }

        /// <summary>
        /// 要执行的命令(sql或存储过程名称)
        /// </summary>
        public string CommandText { get; }

        /// <summary>
        /// 与命令关联的参数
        /// </summary>
        public object Parameters { get; }

        /// <summary>
        /// 命令的活动事务
        /// </summary>
        public IDbTransaction Transaction { get; }

        /// <summary>
        /// 命令的有效超时时间
        /// </summary>
        public int? CommandTimeout { get; }

        /// <summary>
        /// 命令文本表示的命令类型
        /// </summary>
        public CommandType? CommandType { get; }

        /// <summary>
        /// 应该在返回之前缓冲数据吗？
        /// </summary>
        public bool Buffered => (Flags & CommandFlags.Buffered) != 0;

        /// <summary>
        /// 应该缓存此查询的计划吗？
        /// </summary>
        internal bool AddToCache => (Flags & CommandFlags.NoCache) == 0;

        /// <summary>
        /// 针对此命令的其他状态标志
        /// </summary>
        public CommandFlags Flags { get; }

        /// <summary>
        /// 可以对异步查询进行流水线操作吗？
        /// </summary>
        public bool Pipelined => (Flags & CommandFlags.Pipelined) != 0;

        /// <summary>
        /// 初始化命令定义
        /// </summary>
        /// <param name="commandText">The text for this command.</param>
        /// <param name="parameters">The parameters for this command.</param>
        /// <param name="transaction">The transaction for this command to participate in.</param>
        /// <param name="commandTimeout">The timeout (in seconds) for this command.</param>
        /// <param name="commandType">The <see cref="CommandType"/> for this command.</param>
        /// <param name="flags">The behavior flags for this command.</param>
        /// <param name="cancellationToken">The cancellation token for this command.</param>
        public CommandDefinition(string commandText, object parameters = null, IDbTransaction transaction = null, int? commandTimeout = null,
                                 CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered
                                 , CancellationToken cancellationToken = default(CancellationToken)
            )
        {
            CommandText = commandText;
            Parameters = parameters;
            Transaction = transaction;
            CommandTimeout = commandTimeout;
            CommandType = commandType;
            Flags = flags;
            CancellationToken = cancellationToken;
        }

        private CommandDefinition(object parameters) : this()
        {
            Parameters = parameters;
        }

        /// <summary>
        /// 对于异步操作，取消令牌
        /// </summary>
        public CancellationToken CancellationToken { get; }

        internal IDbCommand SetupCommand(IDbConnection cnn, Action<IDbCommand, object> paramReader)
        {
            var cmd = cnn.CreateCommand();
            var init = GetInit(cmd.GetType());
            init?.Invoke(cmd);
            if (Transaction != null)
                cmd.Transaction = Transaction;
            cmd.CommandText = CommandText;
            if (CommandTimeout.HasValue)
            {
                cmd.CommandTimeout = CommandTimeout.Value;
            }
            else if (SqlMapper.Settings.CommandTimeout.HasValue)
            {
                cmd.CommandTimeout = SqlMapper.Settings.CommandTimeout.Value;
            }
            if (CommandType.HasValue)
                cmd.CommandType = CommandType.Value;
            paramReader?.Invoke(cmd, Parameters);
            return cmd;
        }

        private static SqlMapper.Link<Type, Action<IDbCommand>> commandInitCache;

        private static Action<IDbCommand> GetInit(Type commandType)
        {
            if (commandType == null)
                return null; // GIGO
            if (SqlMapper.Link<Type, Action<IDbCommand>>.TryGet(commandInitCache, commandType, out Action<IDbCommand> action))
            {
                return action;
            }
            var bindByName = GetBasicPropertySetter(commandType, "BindByName", typeof(bool));
            var initialLongFetchSize = GetBasicPropertySetter(commandType, "InitialLONGFetchSize", typeof(int));

            action = null;
            if (bindByName != null || initialLongFetchSize != null)
            {
                var method = new DynamicMethod(commandType.Name + "_init", null, new Type[] { typeof(IDbCommand) });
                var il = method.GetILGenerator();

                if (bindByName != null)
                {
                    // .BindByName = true
                    il.Emit(OpCodes.Ldarg_0);
                    il.Emit(OpCodes.Castclass, commandType);
                    il.Emit(OpCodes.Ldc_I4_1);
                    il.EmitCall(OpCodes.Callvirt, bindByName, null);
                }
                if (initialLongFetchSize != null)
                {
                    // .InitialLONGFetchSize = -1
                    il.Emit(OpCodes.Ldarg_0);
                    il.Emit(OpCodes.Castclass, commandType);
                    il.Emit(OpCodes.Ldc_I4_M1);
                    il.EmitCall(OpCodes.Callvirt, initialLongFetchSize, null);
                }
                il.Emit(OpCodes.Ret);
                action = (Action<IDbCommand>)method.CreateDelegate(typeof(Action<IDbCommand>));
            }
            // cache it
            SqlMapper.Link<Type, Action<IDbCommand>>.TryAdd(ref commandInitCache, commandType, ref action);
            return action;
        }

        private static MethodInfo GetBasicPropertySetter(Type declaringType, string name, Type expectedType)
        {
            var prop = declaringType.GetProperty(name, BindingFlags.Public | BindingFlags.Instance);
            if (prop?.CanWrite == true && prop.PropertyType == expectedType && prop.GetIndexParameters().Length == 0)
            {
                return prop.GetSetMethod();
            }
            return null;
        }
    }
}
