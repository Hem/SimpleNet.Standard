# SimpleNet.Standard


SimpleNet.Standard.Data is a .NET Standard implementation of a DAL (Data Access Layer) with the hopes of simlifying execution of SQL Statements.

# SimpleNet.Standard.Data.SqlServer

An implementation of ISimpleDatabaseProvider for SQL Server databases.


## How To 

  1. Include SimpleNet.Standard.Data and SimpleNet.Standard.Data.SqlServer in your project
     
  2. Create an instance of ISimpleDatabaseProvider ()
 
 ` 
 var dbProvider = new SqlServerProvider("Your SQL server Connection String");
 var dbProvider = new PostgresSqlProvider("Your connection string"); --- Yes! I know spelling error!!
 `
 
  3. Create an instance of ISimpleDataAccessLayer 

` var dal = new SimpleDataAccessLayer(dbProvider); `

  4. Execute SQL statements

```
     dal.ExecuteNonQuery (  
            "UPDATE Table SET Col1=@param1 WHERE Id = @Id ",
            CommandType.Text,
            new []{ 
                dbProvider.GetParameter("@param1", someValue) ,
                 dbProvider.GetParameter("@id", someId) 
            });


    dal.ExecuteScalar (  
        "SELECT SomeValue FROM TABLE WHERE Id = @Id ",
        CommandType.Text,
        new []{ 
             dbProvider.GetParameter("@id", someId) 
        });
```

  5. Selecting records from the database?
 
We prefer working with objects instead of datatables, to enable this functionality we require and implementation of IRowMapper<T> to be provided when we Read data.
We are using the IRowMapper implementation as originally implemented by Enterprise Library's Data Access Block
      
      ``` 
       const string SQL = @"SELECT Id, Code, Name FROM STATE s where s.Id = @Id";
		var records = dal.Read<State>(StateMapper, SQL, CommandType.Text, new[]
				{
					dbProvider.GetDbParameter("@Id", id)
				});
      ```

### IRowMapper<T> Examples
 
Note: Please review documentation from Enterprise Libary Data Access Block to Find out more about IRowMapper

```
 // Map all properties on the object.
private static readonly IRowMapper<State> StateMapper = MapBuilder<State>.BuildAllProperties(); 

 // Map all properties by name ... override how we map name
private static readonly IRowMapper<State> StateRowMapper = MapBuilder<State>
            .MapAllProperties()
            .Map(x => x.Name).WithFunc(x => x["Name"].ToString())
            .Build(); 

 // Manually map some/all properties
private static readonly IRowMapper<State> StateRowMapper2 = MapBuilder<State>
        .MapNoProperties()
        .MapByName(x => x.Id)
        .Map(x => x.Code).ToColumn("Code")
        .Map(x => x.Name).WithFunc(x => x["Name"].ToString())
        .Build(); 
```

### Working with IRepository<T> and AbstractSimpleRepository

  1. Create a BaseRepository that extends AbstractSimpleRepository
 
```
    public class BaseSqlRepository : AbstractSimpleRepository
    {
        public override sealed ISimpleDataAccessLayer Database { get; set; }

         public BaseSqlRepository()
         {
            var dbProvider = new SqlServerProvider("Your SQL server Connection String");
            
            Database = new SimpleDataAccessLayer(dbProvider); 
         }
    }
```


  2. Create your Repository class by extending BaseSqlRepository

```
    public class StateRepository : BaseSqlRepository, IStateRepository
    {
         // Please review documentation from Enterprise Libary Db Accessors to Find out more about IRowMapper
         
         private static readonly IRowMapper<State> StateMapper = MapBuilder<State>.BuildAllProperties();

         // sample 2
         private static readonly IRowMapper<State> StateRowMapper = MapBuilder<State>.MapAllProperties().Build();

          // sample 3 for mapping
         private static readonly IRowMapper<State> StateRowMapper2 = MapBuilder<State>
                   .MapNoProperties()
                   .MapByName(x => x.Id)
                   .Map(x => x.Code).ToColumn("Code")
                   .Map(x => x.Name).WithFunc(x => x["Name"].ToString())
                   .Build();



		public State GetById(int id)
		{
				const string SQL = @"SELECT Id, Code, Name FROM STATE s where s.Id = @Id";

				return Read<State>(StateMapper, SQL, CommandType.Text, new[]
				{
					GetDbParameter("@Id", id)
				}).FirstOrDefault();
		}


		public State GetAll(int id)
         	{
			const string SQL = @"SELECT Id, Code, Name FROM STATE s  ORDER BY s.Name ";

			return Read<State>(StateMapper, SQL, CommandType.Text, null).FirstOrDefault();
         	}


		public DataTable ReadById(int id)
		{
			const string SQL = @"SELECT Id, Code, Name FROM STATE s where s.Id = @Id";

			return Read(SQL, CommandType.Text, new[]
			{
				GetDbParameter("@Id", id)
			});
		}


		public State Create(State state)
		{
			const string SQL = @"INSERT INTO STATE (Code, Name)
								 VALUES (@Code, @Name);
								 SELECT SCOPE_IDENTITY()
									 ";

			var id = ExecuteScalar(SQL, CommandType.Text, new[]
			{
				GetDbParameter("@Code", state.Code),
				GetDbParameter("@Name", state.Name)
			});

			state.Id = Convert.ToInt32(id);

			return state;
		}


		public State Update(State state)
		{
			const string SQL = @" UPDATE STATE
			SET Code = @Code,
			Name = @Name
			WHERE Id = @Id
			";

			var id = ExecuteNonQuery(SQL, CommandType.Text, new[]
			{
				GetDbParameter("@Code", state.Code),
				GetDbParameter("@Name", state.Name),
				GetDbParameter("@Id", state.Id)
			});

			return state;
		}

    }

```


## Working with database transactions



```
    using (var connection = dal.GetConnection())
    {
        using (var transaction = connection.BeginTransaction())
        {
            try{
        
                dal.ExecuteNonQuery(connection, ..... , transaction);
                ...     
                dal.ExecuteScalar(connection, ..... , transaction);
                ...
                transaction.Commit();

            }catch(){
                transaction.Rollback();
            }
        }
    }
```





