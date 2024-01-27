using System;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using Npgsql;

namespace SimpleNet.Standard.Data.PostgresSql
{
    public class PostgresSqlProvider : ISimpleDatabaseProvider
    {
        readonly string _connectionString;
        readonly NpgsqlDataSource datasource;


        public PostgresSqlProvider(string connectionString)
        {
            _connectionString = connectionString;
            datasource = NpgsqlDataSource.Create(_connectionString);
        }

        public DbCommand GetCommand()
        {
            return new NpgsqlCommand();
        }

        public DbConnection GetConnection()
        {
            var conn =  datasource.CreateConnection();
            conn.Open();
            return conn;
        }

        public async Task<DbConnection> GetConnectionAsync()
        {
            var conn = datasource.CreateConnection();
            await conn.OpenAsync();

            return conn;
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

