using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using SimpleNet.Standard.Data.Mappers;

namespace SimpleNet.Standard.Data
{
    public interface ISimpleDataAccessLayer
    {
        /// <summary>
        /// Database Provider... 
        /// This can be used to get DbParameter instances...
        /// </summary>
        ISimpleDatabaseProvider DatabaseProvider { get; }



        /// <summary>
        /// Executes a select statement and transposes them to an array of <typeparam name="T"></typeparam>
        /// using <param name="mapper"></param>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="mapper">A call that converts a DbDataRecord to 
        ///                         <typeparam name="T"></typeparam></param>
        /// <param name="commandText"></param>
        /// <param name="commandType"></param>
        /// <param name="parameters"></param>
        /// <returns>A IList of <typeparam name="T"></typeparam></returns>
        IList<T> Read<T>(IRowMapper<T> mapper,
                        string commandText,
                        CommandType commandType,
                        DbParameter[] parameters);



        /// <summary>
        /// Executes a select statement and transposes them to an array of <typeparam name="T"></typeparam>
        /// using <param name="mapper"></param>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connection"></param>
        /// <param name="mapper">A call that converts a DbDataRecord to 
        ///                         <typeparam name="T"></typeparam></param>
        /// <param name="commandText"></param>
        /// <param name="commandType"></param>
        /// <param name="parameters"></param>
        /// <param name="transaction"></param>
        /// <returns>A IList of <typeparam name="T"></typeparam></returns>
        IList<T> Read<T>(
            DbConnection connection,
            IRowMapper<T> mapper,
            string commandText,
            CommandType commandType,
            DbParameter[] parameters,
            DbTransaction transaction = null);


        /// <summary>
        /// Executes a select statement and transposes them to an array of <typeparam name="T"></typeparam>
        /// using <param name="mapper"></param>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="mapper">A call that converts a DbDataRecord to 
        ///                         <typeparam name="T"></typeparam></param>
        /// <param name="commandText"></param>
        /// <param name="commandType"></param>
        /// <param name="parameters"></param>
        /// <returns>A IList of <typeparam name="T"></typeparam></returns>
        Task<IList<T>> ReadAsync<T>(IRowMapper<T> mapper,
            string commandText,
            CommandType commandType,
            DbParameter[] parameters);




        /// <summary>
        /// Executes a select statement and transposes them to an array of <typeparam name="T"></typeparam>
        /// using <param name="mapper"></param>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connection"></param>
        /// <param name="mapper">A call that converts a DbDataRecord to 
        ///                         <typeparam name="T"></typeparam></param>
        /// <param name="commandText"></param>
        /// <param name="commandType"></param>
        /// <param name="parameters"></param>
        /// <param name="transaction"></param>
        /// <returns>A IList of <typeparam name="T"></typeparam></returns>
        Task<IList<T>> ReadAsync<T>(
            DbConnection connection,
            IRowMapper<T> mapper,
            string commandText,
            CommandType commandType,
            DbParameter[] parameters,
            DbTransaction transaction = null);


        /// <summary>
        /// Execute a statement that does not return a value.
        /// Example a DELETE or an UPDATE statement
        /// </summary>
        /// <param name="commandText">SQL Statement of procedure name to run</param>
        /// <param name="commandType">Sql Text or Proc</param>
        /// <param name="parameters">Array of parameters to bind to the command text</param>
        /// <returns></returns>
        Task<int> ExecuteNonQueryAsync( string commandText, 
            CommandType commandType, 
            DbParameter[] parameters);

        /// <summary>
        /// Execute a statement that does not return a value.
        /// Example a DELETE or an UPDATE statement
        /// </summary>
        Task<int> ExecuteNonQueryAsync(DbConnection connection,
            string commandText,
            CommandType commandType,
            DbParameter[] parameters,
            DbTransaction transaction = null);




        /// <summary>
        /// Execute a statement that does not return a value.
        /// Example a DELETE or an UPDATE statement
        /// </summary>
        /// <param name="commandText">SQL Statement of procedure name to run</param>
        /// <param name="commandType">Sql Text or Proc</param>
        /// <param name="parameters">Array of parameters to bind to the command text</param>
        /// <returns></returns>
        int ExecuteNonQuery(string commandText,
            CommandType commandType,
            DbParameter[] parameters);

        /// <summary>
        /// Execute a statement that does not return a value.
        /// Example a DELETE or an UPDATE statement
        /// </summary>
        int ExecuteNonQuery(DbConnection connection,
            string commandText,
            CommandType commandType,
            DbParameter[] parameters,
            DbTransaction transaction = null);





        /// <summary>
        /// Executes a statement and returns the first data element in the first row.
        /// </summary>
        /// <param name="commandText">SQL Statement of procedure name to run</param>
        /// <param name="commandType">Sql Text or Proc</param>
        /// <param name="parameters">Array of parameters to bind to the command text</param>
        /// <returns></returns>
        Task<object> ExecuteScalarAsync(string commandText, 
            CommandType commandType, 
            DbParameter[] parameters);


        /// <summary>
        /// Executes a statement and returns the first data element in the first row.
        /// </summary>
        /// <param name="connection">Instance of the connection object</param>
        /// <param name="commandText">SQL Statement of procedure name to run</param>
        /// <param name="commandType">Sql Text or Proc</param>
        /// <param name="parameters">Array of parameters to bind to the command text</param>
        /// <param name="transaction">Instance of the transaction object</param>
        /// <returns></returns>
        Task<object> ExecuteScalarAsync(DbConnection connection, 
            string commandText, 
            CommandType commandType, 
            DbParameter[] parameters, 
            DbTransaction transaction = null);




        /// <summary>
        /// Executes a statement and returns the first data element in the first row.
        /// </summary>
        /// <param name="commandText">SQL Statement of procedure name to run</param>
        /// <param name="commandType">Sql Text or Proc</param>
        /// <param name="parameters">Array of parameters to bind to the command text</param>
        /// <returns></returns>
        object ExecuteScalar(string commandText,
            CommandType commandType,
            DbParameter[] parameters);

        /// <summary>
        /// Executes a statement and returns the first data element in the first row.
        /// </summary>
        /// <param name="connection">Instance of the connection object</param>
        /// <param name="commandText">SQL Statement of procedure name to run</param>
        /// <param name="commandType">Sql Text or Proc</param>
        /// <param name="parameters">Array of parameters to bind to the command text</param>
        /// <param name="transaction">Instance of the transaction object</param>
        /// <returns></returns>
        object ExecuteScalar(DbConnection connection,
            string commandText,
            CommandType commandType,
            DbParameter[] parameters,
            DbTransaction transaction = null);
    }
}