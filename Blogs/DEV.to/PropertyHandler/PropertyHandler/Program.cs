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
            ClearPerson();
            InsertPerson();
            QueryPerson();
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
                var result = connection.InsertAll(people);
                connection.MergeAll(people);
                connection.UpdateAll(people);
            }
        }

        private static void QueryPerson()
        {
            using (var connection = GetConnection())
            {
                var person = connection.QueryAll<Person>();
            }
        }
    }
}
