using EntityFrameworkRawSQL.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RepoDb;
using RepoDb.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EntityFrameworkRawSQL
{
    class Program
    {
        static void Main(string[] args)
        {
            Initialize();
            Truncate();
            Console.WriteLine("[ADD]");
            AddRangeForEF(10000);
            AddRangeForRepoDB(10000);

            Console.WriteLine("");
            Console.WriteLine("[QUERY]");
            QueryForEF();
            QueryForRepoDB();

            Console.WriteLine("");
            Console.WriteLine("[RAW-QUERY]");
            QueryFromSqlRawForEF();
            QueryFromSqlRawForRepoDB();
        }

        static void Initialize()
        {
            SqlServerBootstrap.Initialize();
        }

        static void Truncate()
        {
            using (var connection = new SqlConnection("Server=.;Database=TestDB;Integrated Security=SSPI;"))
            {
                connection.Truncate<Person>();
            }
        }

        static IEnumerable<Person> GetPeople(int count)
        {
            for (var i = 0; i < count; i++)
            {
                yield return new Person
                {
                    Age = new Random().Next(100),
                    CreatedDateUtc = DateTime.UtcNow,
                    DateOfBirth = DateTime.UtcNow.AddYears(-new Random().Next(100)),
                    ExtendedInfo = $"ExtendedInfo-{Guid.NewGuid().ToString()}",
                    IsActive = true,
                    Name = $"Name-{Guid.NewGuid().ToString()}"
                };
            }
        }

        static void AddRangeForEF(int count)
        {
            var people = GetPeople(count).ToList();
            var now = DateTime.UtcNow;
            using (var context = new DatabaseContext())
            {
                context.People.AddRange(people);
                context.SaveChanges();
                Console.WriteLine($"EF.AddRange: {people.Count()} row(s) affected for {(DateTime.UtcNow - now).TotalSeconds} second(s).");
            }
        }

        static void AddRangeForRepoDB(int count)
        {
            var people = GetPeople(count).ToList();
            var now = DateTime.UtcNow;
            using (var connection = new SqlConnection("Server=.;Database=TestDB;Integrated Security=SSPI;"))
            {
                connection.InsertAll(people);
                Console.WriteLine($"RepoDB.InsertAll: {people.Count()} row(s) affected for {(DateTime.UtcNow - now).TotalSeconds} second(s).");
            }
        }

        static void QueryForEF()
        {
            var now = DateTime.UtcNow;
            using (var context = new DatabaseContext())
            {
                var people = context.People.ToList();
                Console.WriteLine($"EF.People: {people.Count()} row(s) affected for {(DateTime.UtcNow - now).TotalSeconds} second(s).");
            }
        }

        static void QueryFromSqlRawForEF()
        {
            var now = DateTime.UtcNow;
            using (var context = new DatabaseContext())
            {
                var people = context.People.FromSqlRaw("SELECT * FROM [dbo].[Person];").ToList();
                Console.WriteLine($"EF.FromSqlRaw: {people.Count()} row(s) affected for {(DateTime.UtcNow - now).TotalSeconds} second(s).");
            }
        }

        static void QueryForRepoDB()
        {
            var now = DateTime.UtcNow;
            using (var connection = new SqlConnection("Server=.;Database=TestDB;Integrated Security=SSPI;"))
            {
                var people = connection.QueryAll<Person>();
                Console.WriteLine($"RepoDB.QueryAll: {people.Count()} row(s) affected for {(DateTime.UtcNow - now).TotalSeconds} second(s).");
            }
        }

        static void QueryFromSqlRawForRepoDB()
        {
            var now = DateTime.UtcNow;
            using (var connection = new SqlConnection("Server=.;Database=TestDB;Integrated Security=SSPI;"))
            {
                var people = connection.ExecuteQuery<Person>("SELECT * FROM [dbo].[Person];").AsList();
                Console.WriteLine($"RepoDB.ExecuteQuery: {people.Count()} row(s) affected for {(DateTime.UtcNow - now).TotalSeconds} second(s).");
            }
        }
    }
}
