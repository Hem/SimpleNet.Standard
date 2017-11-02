using SimpleNet.Standard.Data.Mappers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleNet.Standard.Data.Repository
{
    public class SimpleDataAccessLayer : ISimpleDataAccessLayer
    {
        public ISimpleDatabaseProvider DatabaseProvider { get; }
        
        public SimpleDataAccessLayer(ISimpleDatabaseProvider databaseProvider)
        {
            DatabaseProvider = databaseProvider;
        }


        #region Read

        public IList<T> Read<T>(IRowMapper<T> mapper, string commandText, CommandType commandType, DbParameter[] parameters)
        {
            using (var connection = DatabaseProvider.GetConnection())
            {
                return Read(connection, mapper, commandText, commandType, parameters);
            }
        }

        public IList<T> Read<T>(DbConnection connection, IRowMapper<T> mapper, string commandText, CommandType commandType, DbParameter[] parameters, DbTransaction transaction = null)
        {
            var records = new List<T>();

            using (var command = DatabaseProvider.GetCommand())
            {
                command.Connection = connection;
                command.CommandText = commandText;
                command.CommandType = commandType;

                if (transaction != null)
                    command.Transaction = transaction;

                // Add parameters
                if (parameters != null)
                    command.Parameters.AddRange(parameters.ToArray());

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        records.Add(mapper.MapRow(reader));
                    }
                }

                command.Parameters.Clear();
            }

            return records;
        }

        #endregion


        #region ReadAsync


        public async Task<IList<T>> ReadAsync<T>(       IRowMapper<T> mapper, 
                                                        string commandText, 
                                                        CommandType commandType,
                                                        DbParameter[] parameters  )
        {
            using (var connection = DatabaseProvider.GetConnection())
            {
                return await ReadAsync(connection, mapper, commandText, commandType, parameters);
            }
        }
        
        public async Task<IList<T>> ReadAsync<T>(   DbConnection connection, 
                                                    IRowMapper<T> mapper, 
                                                    string commandText, 
                                                    CommandType commandType,
                                                    DbParameter[] parameters, 
                                                    DbTransaction transaction = null  )
        {
            var records = new List<T>();

            using (var command = DatabaseProvider.GetCommand())
            {
                command.Connection = connection;
                command.CommandText = commandText;
                command.CommandType = commandType;
                
                if (transaction != null)
                    command.Transaction = transaction;
                
                // Add parameters
                if (parameters != null)
                    command.Parameters.AddRange(parameters.ToArray());

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        records.Add(mapper.MapRow(reader));
                    }
                }

                command.Parameters.Clear();
            }

            return records;
        }


        #endregion



        #region ExecuteNotQuery


        /// <summary>
        /// Use this to execute a command against the database when a response is NOT needed 
        /// or we only need the count of number of records affected
        /// </summary>
        /// <param name="commandText">The sql or procedure name to execute</param>
        /// <param name="commandType">The command type</param>
        /// <param name="parameters">An array || a list of parameters to pass to the command</param>
        /// <returns>The count of number of records affected.</returns>
        public int ExecuteNonQuery(string commandText, CommandType commandType, DbParameter[] parameters)
        {
            using (var connection = DatabaseProvider.GetConnection())
            {
                return  ExecuteNonQuery(connection, commandText, commandType, parameters);
            }
        }


        /// <summary>
        /// Use this to execute a command against the database when a response is NOT needed 
        /// or we only need the count of number of records affected
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="commandText">The sql or procedure name to execute</param>
        /// <param name="commandType">The command type</param>
        /// <param name="parameters">An array || a list of parameters to pass to the command</param>
        /// <param name="transaction"></param>
        /// <returns>The count of number of records affected.</returns>
        public int ExecuteNonQuery(DbConnection connection, string commandText, CommandType commandType, DbParameter[] parameters, DbTransaction transaction = null)
        {
            int value;

            try
            {
                using (var command = DatabaseProvider.GetCommand())
                {
                    command.Connection = connection;
                    command.CommandText = commandText;
                    command.CommandType = commandType;


                    if (transaction != null) command.Transaction = transaction;

                    // Add parameters
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters.ToArray());
                    }

                    value = command.ExecuteNonQuery();

                    command.Parameters.Clear();
                }

            }
            catch (Exception ex)
            {
                ex.Data.Add("CommandText", commandText);
                ex.Data.Add("CommandType", commandType.ToString());

                if (parameters != null)
                {
                    if (commandType == CommandType.StoredProcedure)
                    {
                        // ex.Data.Add("SqlCommand", parameters.FormatToExecuteProcedure(commandText));
                        ex.Data.Add("SqlCommand", commandText);
                    }

                    foreach (var parameter in parameters)
                    {
                        ex.Data.Add(parameter.ParameterName, parameter.Value);
                    }
                }

                // ex.TraceException();
                throw;
            }

            return value;
        }
        #endregion



        #region ExecuteNonQueryAsync


        /// <summary>
        /// Use this to execute a command against the database when a response is NOT needed 
        /// or we only need the count of number of records affected
        /// </summary>
        /// <param name="commandText">The sql or procedure name to execute</param>
        /// <param name="commandType">The command type</param>
        /// <param name="parameters">An array || a list of parameters to pass to the command</param>
        /// <returns>The count of number of records affected.</returns>
        public async Task<int> ExecuteNonQueryAsync(string commandText, CommandType commandType, DbParameter[] parameters)
        {
            using (var connection = DatabaseProvider.GetConnection())
            {
                return await ExecuteNonQueryAsync(connection, commandText, commandType, parameters);
            }
        }


        /// <summary>
        /// Use this to execute a command against the database when a response is NOT needed 
        /// or we only need the count of number of records affected
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="commandText">The sql or procedure name to execute</param>
        /// <param name="commandType">The command type</param>
        /// <param name="parameters">An array || a list of parameters to pass to the command</param>
        /// <param name="transaction"></param>
        /// <returns>The count of number of records affected.</returns>
        public async Task<int> ExecuteNonQueryAsync(DbConnection connection, string commandText, CommandType commandType, DbParameter[] parameters, DbTransaction transaction = null)
        {
            int value;

            try
            {
                using (var command = DatabaseProvider.GetCommand())
                {
                    command.Connection = connection;
                    command.CommandText = commandText;
                    command.CommandType = commandType;


                    if (transaction != null) command.Transaction = transaction;

                    // Add parameters
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters.ToArray());
                    }

                    value = await command.ExecuteNonQueryAsync();

                    command.Parameters.Clear();
                }

            }
            catch (Exception ex)
            {
                ex.Data.Add("CommandText", commandText);
                ex.Data.Add("CommandType", commandType.ToString());

                if (parameters != null)
                {
                    if (commandType == CommandType.StoredProcedure)
                    {
                        // ex.Data.Add("SqlCommand", parameters.FormatToExecuteProcedure(commandText));
                        ex.Data.Add("SqlCommand", commandText);
                    }

                    foreach (var parameter in parameters)
                    {
                        ex.Data.Add(parameter.ParameterName, parameter.Value);
                    }
                }

                // ex.TraceException();
                throw;
            }

            return value;
        }

        #endregion


        #region ExecuteScalarAsync

        /// <summary>
        /// Executes a command against the dataset and returns the first value received.
        /// </summary>
        /// <param name="commandText">The sql or procedure name to execute</param>
        /// <param name="commandType">The command type</param>
        /// <param name="parameters">An array || a list of parameters to pass to the command</param>
        /// <returns>The first value returned</returns>
        public async Task<object> ExecuteScalarAsync(string commandText, CommandType commandType, DbParameter[] parameters)
        {
            using (var connection = DatabaseProvider.GetConnection())
            {
                return await ExecuteScalarAsync(connection, commandText, commandType, parameters);
            }
        }

        /// <summary>
        /// Executes a command against the dataset and returns the first value received.
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="commandText">The sql or procedure name to execute</param>
        /// <param name="commandType">The command type</param>
        /// <param name="parameters">An array || a list of parameters to pass to the command</param>
        /// <param name="transaction"></param>
        /// <returns>The first value returned</returns>
        public async Task<object> ExecuteScalarAsync(DbConnection connection, string commandText, CommandType commandType, DbParameter[] parameters, DbTransaction transaction = null)
        {
            object value;

            try
            {
                using (var command = DatabaseProvider.GetCommand())
                {
                    command.Connection = connection;
                    command.CommandText = commandText;
                    command.CommandType = commandType;

                    if (transaction != null) command.Transaction = transaction;

                    // Add parameters
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters.ToArray());
                    }

                    value = await command.ExecuteScalarAsync();

                    command.Parameters.Clear();
                }

            }
            catch (Exception ex)
            {
                ex.Data.Add("CommandText", commandText);
                ex.Data.Add("CommandType", commandType.ToString());

                if (parameters != null)
                {
                    if (commandType == CommandType.StoredProcedure)
                        ex.Data.Add("SqlCommand", commandText);
                    // ex.Data.Add("SqlCommand", parameters.FormatToExecuteProcedure(commandText));

                    foreach (var parameter in parameters)
                    {
                        ex.Data.Add(parameter.ParameterName, parameter.Value);
                    }
                }

                //ex.TraceException();
                throw;
            }

            return value;
        }


        #endregion


        #region Execute Scalar

        public object ExecuteScalar(string commandText, CommandType commandType, DbParameter[] parameters)
        {
            using (var connection = DatabaseProvider.GetConnection())
            {
                return ExecuteScalar(connection, commandText, commandType, parameters);
            }
        }

        public object ExecuteScalar(DbConnection connection, string commandText, CommandType commandType, DbParameter[] parameters, DbTransaction transaction = null)
        {
            object value;

            try
            {
                using (var command = DatabaseProvider.GetCommand())
                {
                    command.Connection = connection;
                    command.CommandText = commandText;
                    command.CommandType = commandType;

                    if (transaction != null) command.Transaction = transaction;

                    // Add parameters
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters.ToArray());
                    }

                    value = command.ExecuteScalar();

                    command.Parameters.Clear();
                }

            }
            catch (Exception ex)
            {
                ex.Data.Add("CommandText", commandText);
                ex.Data.Add("CommandType", commandType.ToString());

                if (parameters != null)
                {
                    if (commandType == CommandType.StoredProcedure)
                        ex.Data.Add("SqlCommand", commandText);
                    // ex.Data.Add("SqlCommand", parameters.FormatToExecuteProcedure(commandText));

                    foreach (var parameter in parameters)
                    {
                        ex.Data.Add(parameter.ParameterName, parameter.Value);
                    }
                }

                //ex.TraceException();
                throw;
            }

            return value;
        }

        #endregion

    }
}
