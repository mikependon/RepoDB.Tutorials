// Learn more about F# at http://fsharp.org

open System
open Microsoft.Data.SqlClient
open RepoDb
open RepoDb.Extensions
open FSharpRepoDB.Types

let getUrl (argv: string array) =
    let getFromEnv =
        System.Environment.GetEnvironmentVariable("MSSQL_DB_URL")
        |> Option.ofObj

    let fromArgsOrEnv = 
        argv 
        |> Array.tryFind(fun arg -> arg.Contains "--dburl~")
        |> Option.map (fun f -> f.Split('~') |> Seq.tryLast)
        |> Option.flatten
        |> Option.orElse getFromEnv

    fromArgsOrEnv |> Option.defaultValue "Server=.;Database=TestDB;Integrated Security=SSPI;"

[<EntryPoint>]
let main argv =
    // you can set "MSSQL_DB_URL" env variable or run the following command to use a Custom URL for testing purposes
    /// dotnet run -p FSharpRepoDB -- "--dburl~Server=127.0.0.1;Database=TestDB;User Id=sa;Password=Password1!;"

    // Initialize the SQL Server
    SqlServerBootstrap.Initialize()

    // Open the connection
    let url = getUrl argv
    let connection = (new SqlConnection(url)).EnsureOpen()
    
    // Get the fields (Testing)
    // let personType = typedefof<Person>
    // let dbFields = DbFieldCache.Get(connection, "Person", null).AsList()
    // let fields = FieldCache.Get(personType).AsList()
    
    // Truncate
    let affectedRows = connection.Truncate<Person>();
    Console.WriteLine("Truncated")

    // Insert<TEntity>
    let person = { Id = 0L; Name = "John Doe"; Address = "New York"; Age = 32; IsActive = true }
    let id = connection.Insert<Person, int64>(person)
    Console.WriteLine("Insert<TEntity>: Generated Id = {0}", Convert.ToString(id))
    
    // Insert(TableName)
    let person = {| Name = "James Smith"; Age = 32; Address = "Washington"; IsActive = false|}
    let id = connection.Insert<int64>(ClassMappedNameCache.Get<Person>(), person)
    Console.WriteLine("Insert(TableName): Generated Id = {0}", Convert.ToString(id))
    
    // QueryAll<TEntity>
    let result = connection.QueryAll<Person>().AsList()
    Console.WriteLine("QueryAll<TEntity>: Count = {0}", result.Count)

    // QueryAll(TableName)
    let result = connection.QueryAll(ClassMappedNameCache.Get<Person>()).AsList()
    Console.WriteLine("QueryAll(TableName): Count = {0}", result.Count)

    // Query with anonymous records, pull only what you need
    let result = 
        connection.ExecuteQuery<{| Name: string; Age: int |}>(
            @"SELECT Name, Age FROM [dbo].[Person] WHERE IsActive = @isactive",
            dict(["@isactive", box false ])
        ).AsList()
    printfn "ExecuteQuery(SQL query): Count = %i\n\tFirst Value =\n\t'%A'" result.Count (result |> Seq.tryHead)

    // Dispose the connection
    connection.Dispose()

    // Return values
    let line = Console.ReadLine();
    0
