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
    
    // Get the fields
    let personType = typedefof<Person>
    let dbFields = DbFieldCache.Get(connection, "Person", null).AsList()
    let fields = FieldCache.Get(personType).AsList()

    let person = { Id = 0L; Name = "John Doe"; Address = "New York"; Age = 32; IsActive = true }
    
    // Insert (Generic-Based)
    let id = connection.Insert<Person, int64>(person)
    Console.WriteLine(Convert.ToString(id))
    
    // Create PersonLike Record the type
    let person = {| Name = "John Doe"; Age = 32; Address = "New York"; IsActive = true|}
    // Insert (Table-Based)
    let id = connection.Insert<int64>(ClassMappedNameCache.Get<Person>(), person)
    Console.WriteLine(Convert.ToString(id))

    // QueryAll
    let result = connection.QueryAll<Person>().AsList()
    Console.WriteLine(result.Count)

    // Dispose the connection
    connection.Dispose()

    // Return values
    0
