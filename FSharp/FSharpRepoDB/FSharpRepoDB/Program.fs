// Learn more about F# at http://fsharp.org

open FSharpRepoDB
open Microsoft.Data.SqlClient
open RepoDb
open RepoDb.Extensions
open System

[<EntryPoint>]
let main argv =

    // Initialize the SQL Server
    SqlServerBootstrap.Initialize()

    // Open the connection
    let connection = (new SqlConnection("Server=.;Database=TestDB;Integrated Security=SSPI;")).EnsureOpen()
    
    // Get the fields
    let personType = typedefof<Person>
    let dbFields = DbFieldCache.Get(connection, "Person", null).AsList()
    let fields = FieldCache.Get(personType).AsList()

    // Create the type
    let person = new Person (Convert.ToInt64(0), "John Doe", 32, "New York", true)
    
    // Insert (Generic-Based)
    let id = connection.Insert<Person, int64>(person)
    Console.WriteLine(Convert.ToString(id))
    
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
