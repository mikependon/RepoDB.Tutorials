using Microsoft.Data.SqlClient;
using PropertyHandler.Factories;
using PropertyHandler.Models;
using RepoDb;
using RepoDb.Extensions;
using System;

namespace PropertyHandler
{
    class Program
    {
        static void Main(string[] args)
        {
            // Intialize the RepoDb for SQL Server
            RepoDb.SqlServer.SqlServerBootstrap.Initialize();

            // Calls to the method
            ClearPerson();
            InsertPerson();
            QueryPerson();

            // Exit the console
            Console.WriteLine("Press any key to exit.");
            Console.ReadLine();
        }

        private static SqlConnection GetConnection()
        {
            return new SqlConnection("Server=.;Database=TestDB;Integrated Security=SSPI;");
        }

        private static void ClearPerson()
        {
            using (var connection = GetConnection())
            {
                connection.DeleteAll<Person>();
            }
        }

        private static void InsertPerson()
        {
            using (var connection = GetConnection())
            {
                var people = PersonFactory.CreateMultiple(100).AsList();
                connection.InsertAll(people);
            }
        }

        private static void QueryPerson()
        {
            using (var connection = GetConnection())
            {
                var people = connection.QueryAll<Person>();
            }
        }
    }
}
