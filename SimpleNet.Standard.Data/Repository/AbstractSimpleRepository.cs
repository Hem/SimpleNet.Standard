using System.Collections.Generic;
using System.Data;
using System.Data.Common;

using System.Threading.Tasks;
using SimpleNet.Standard.Data.Mappers;

namespace SimpleNet.Standard.Data.Repository
{
    public abstract class AbstractSimpleRepository
    {
        public abstract ISimpleDataAccessLayer Database { get; set; }



        protected DbParameter GetParameter(string name, object value)
        {
            return Database.DatabaseProvider.GetParameter(name, value);
        }
        protected DbParameter GetParameter(string name, object value, ParameterDirection direction)
        {
            return Database.DatabaseProvider.GetParameter(name, value, direction);
        }



        protected IList<T> Read<T>(IRowMapper<T> mapper, string commandText,
                                                CommandType commandType, DbParameter[] parameters)
        {
            return Database.Read(mapper, commandText, commandType, parameters);
        }

        protected IList<T> Read<T>(DbConnection connection, IRowMapper<T> mapper, string commandText,
                                                CommandType commandType, DbParameter[] parameters, DbTransaction transaction = null)
        {
            return Database.Read(connection, mapper, commandText, commandType, parameters, transaction);
        }

        protected int ExecuteNonQuery(string commandText, CommandType commandType, DbParameter[] parameters)
        {
            return Database.ExecuteNonQuery(commandText, commandType, parameters);
        }

        protected int ExecuteNonQuery(DbConnection connection, string commandText, CommandType commandType,
                                                        DbParameter[] parameters, DbTransaction transaction = null)
        {
            return Database.ExecuteNonQuery(connection, commandText, commandType, parameters, transaction);
        }

        protected object ExecuteScalar(string commandText, CommandType commandType, DbParameter[] parameters)
        {
            return Database.ExecuteScalar(commandText, commandType, parameters);
        }

        protected object ExecuteScalar(DbConnection connection, string commandText, CommandType commandType,
                                                        DbParameter[] parameters, DbTransaction transaction = null)
        {
            return Database.ExecuteScalar(connection, commandText, commandType, parameters, transaction);
        }


        // -- Async stuff here!!!


        protected async Task<IList<T>> ReadAsync<T>(IRowMapper<T> mapper, string commandText,
                                                CommandType commandType, DbParameter[] parameters)
        {
            return await Database.ReadAsync(mapper, commandText, commandType, parameters);
        }

        protected async Task<IList<T>> ReadAsync<T>(DbConnection connection, IRowMapper<T> mapper, string commandText,
                                                CommandType commandType, DbParameter[] parameters, DbTransaction transaction = null)
        {
            return await Database.ReadAsync(connection, mapper, commandText, commandType, parameters, transaction);
        }

        protected async Task<int> ExecuteNonQueryAsync(string commandText, CommandType commandType, DbParameter[] parameters)
        {
            return await Database.ExecuteNonQueryAsync(commandText, commandType, parameters);
        }

        protected async Task<int> ExecuteNonQueryAsync(DbConnection connection, string commandText, CommandType commandType,
                                                        DbParameter[] parameters, DbTransaction transaction = null)
        {
            return await Database.ExecuteNonQueryAsync(connection, commandText, commandType, parameters, transaction);
        }

        protected async Task<object> ExecuteScalarAsync(string commandText, CommandType commandType, DbParameter[] parameters)
        {
            return await Database.ExecuteScalarAsync(commandText, commandType, parameters);
        }

        protected async Task<object> ExecuteScalarAsync(DbConnection connection, string commandText, CommandType commandType,
                                                        DbParameter[] parameters, DbTransaction transaction = null)
        {
            return await Database.ExecuteScalarAsync(connection, commandText, commandType, parameters, transaction);
        }


    }
}
