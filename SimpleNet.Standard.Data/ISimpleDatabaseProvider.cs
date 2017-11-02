using System.Data;
using System.Data.Common;

namespace SimpleNet.Standard.Data
{
    public interface ISimpleDatabaseProvider
    {
        /// <summary>
        /// Returns a new database connection.
        /// </summary>
        DbConnection GetConnection();


        /// <summary>
        /// Returns a new database command.
        /// </summary>
        DbCommand GetCommand();
        

        /// <summary>
        /// Returns a new database parameter
        /// </summary>
        DbParameter GetParameter(string name, object value);


        /// <summary>
        /// Returns a new database parameter
        /// </summary>
        DbParameter GetParameter(string name, object value, ParameterDirection direction);

    }
}
