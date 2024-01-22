using System;
using System.Data;
using System.Data.Common;
using Npgsql;

namespace SimpleNet.Standard.Data.PostgresSql
{
    public class PostgresSqlProvider : ISimpleDatabaseProvider
    {
        readonly string _connectionString;

        public PostgresSqlProvider(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DbCommand GetCommand()
        {
            return new NpgsqlCommand();
        }

        public DbConnection GetConnection()
        {
            var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            return connection;

            // NpgsqlDataSource datasource = NpgsqlDataSource.Create(_connectionString);
            // return datasource.CreateConnection();            
        }

        public DbParameter GetParameter(string name, object value)
        {
            return new NpgsqlParameter(name, value);
        }

        public DbParameter GetParameter(string name, object value, ParameterDirection direction)
        {
            return new NpgsqlParameter(name, value) { Direction = direction };
        }
    }
}

