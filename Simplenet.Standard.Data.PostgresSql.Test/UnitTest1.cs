﻿using System.Data.SqlTypes;
using System.Diagnostics;
using SimpleNet.Standard.Data.Mappers;
using SimpleNet.Standard.Data.PostgresSql;
using SimpleNet.Standard.Data.Repository;

namespace Simplenet.Standard.Data.PostgresSql.Test;

public class PostgresSqlConnectionTest
{

    readonly PostgresSqlProvider sqlProvider;
    readonly SimpleDataAccessLayer dataAccessLayer;


    public PostgresSqlConnectionTest()
    {
        const string connectionString = "User ID=admin;Password=password;Host=localhost;Port=5432;Database=test_db;Pooling=true;";

        sqlProvider = new PostgresSqlProvider(connectionString);

        dataAccessLayer = new SimpleDataAccessLayer(sqlProvider);

    }

    public class StateDto
    {
        public string StateCode { get; set; }
        public string Name { get; set; }
    }


    [Fact]
    public void TestReadFromDatabase()
    {
        var mapper = MapBuilder<StateDto>
                                    .MapNoProperties()
                                    .MapByName(x=>x.Name)
                                    .Map(x=>x.StateCode)
                                    .ToColumn("st_code")
                                    .Build();

        const string sql = @"select  st_code
                                    ,name
                                    ,abbrev
                                    ,statefp
                                    from tiger.state_lookup";
        

        using(var conn = dataAccessLayer.DatabaseProvider.GetConnection())
        {
            var records = dataAccessLayer.Read(conn, mapper, sql, System.Data.CommandType.Text, null);

            foreach(var r in records)
            {
                Assert.True(r.Name != String.Empty);
                Console.WriteLine(r.Name);
            }

        }
        
        
    }
}
