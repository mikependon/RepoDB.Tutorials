using EntityFrameworkRepoDbCombination.DbContexts;
using EntityFrameworkRepoDbCombination.Models;
using EntityFrameworkRepoDbCombination.Repositories;
using Microsoft.EntityFrameworkCore;
using RepoDb;
using RepoDb.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EntityFrameworkRepoDbCombination
{
    class Program
    {
        static void Main(string[] args)
        {
            var rows = 100000;

            // Initialize
            Initialize();

            // Truncate
            Console.WriteLine("First run with compilation!");
            Console.WriteLine(new string(char.Parse("-"), 75));
            TruncateRepoDb();
            BulkInsertRepoDb(rows);
            AddRangeEF(rows);
            InsertAllRepoDb(rows);
            QueryEF();
            QueryRepoDb();

            // Second Run
            Console.WriteLine("Second run without compilation!");
            Console.WriteLine(new string(char.Parse("-"), 75));
            TruncateRepoDb();
            BulkInsertRepoDb(rows);
            AddRangeEF(rows);
            InsertAllRepoDb(rows);
            QueryEF();
            QueryRepoDb();

            // Truncate
            Console.WriteLine(new string(char.Parse("-"), 75));
            TruncateRepoDb();
        }

        static void Initialize()
        {
            SqlServerBootstrap.Initialize();
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

        static void TruncateRepoDb()
        {
            using (var repository = new DatabaseRepository())
            {
                repository.Truncate<Person>();
                Console.WriteLine($"RepoDb.Truncate: Table '{ClassMappedNameCache.Get<Person>()}' has been truncated.");
            }
        }

        static void BulkInsertRepoDb(int count)
        {
            var people = GetPeople(count).ToList();
            var now = DateTime.UtcNow;
            using (var repository = new DatabaseRepository())
            {
                var affectedRows = repository.BulkInsert(people);
                Console.WriteLine($"RepoDb.BulkInsert: {people.Count()} row(s) affected for {(DateTime.UtcNow - now).TotalSeconds} second(s).");
            }
        }

        static void AddRangeEF(int count)
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

        static void UpdateInEF()
        {
            using (var context = new DatabaseContext())
            {
                //var person = context.People.FirstOrDefault(e => e.Id == 2);
                //person.ExtendedInfo = $"{person.ExtendedInfo}-Updated";
                context.People.Update(new Person
                {
                    Id = 2,
                    ExtendedInfo = $"{Guid.NewGuid()}-Updated"
                });
                context.SaveChanges();
            }
        }

        static void DeleteInEF()
        {
            using (var context = new DatabaseContext())
            {
                context.People.Remove(new Person { Id = 1 });
                context.People.Remove(context.People.FirstOrDefault(e => e.Id == 10));
                context.SaveChanges();
            }
        }

        static void InsertAllRepoDb(int count)
        {
            var people = GetPeople(count).ToList();
            var now = DateTime.UtcNow;
            using (var repository = new DatabaseRepository())
            {
                var affectedRows = repository.InsertAll(people);
                Console.WriteLine($"RepoDb.InsertAll: {people.Count()} row(s) affected for {(DateTime.UtcNow - now).TotalSeconds} second(s).");
            }
        }

        static void QueryEF()
        {
            var now = DateTime.UtcNow;
            using (var context = new DatabaseContext())
            {
                var people = context.People.FromSqlRaw("SELECT * FROM [dbo].[Person]").ToList();
                Console.WriteLine($"EF.People (Raw): {people.Count()} row(s) affected for {(DateTime.UtcNow - now).TotalSeconds} second(s).");
            }
            now = DateTime.UtcNow;
            using (var context = new DatabaseContext())
            {
                var people = context.People.ToList();
                Console.WriteLine($"EF.People: {people.Count()} row(s) affected for {(DateTime.UtcNow - now).TotalSeconds} second(s).");
            }
        }

        static void QueryRepoDb()
        {
            var now = DateTime.UtcNow;
            using (var repository = new DatabaseRepository())
            {
                var people = repository.ExecuteQuery<Person>("SELECT * FROM [dbo].[Person]").AsList();
                Console.WriteLine($"RepoDb.ExecuteQuery: {people.Count()} row(s) affected for {(DateTime.UtcNow - now).TotalSeconds} second(s).");
            }
            now = DateTime.UtcNow;
            using (var repository = new DatabaseRepository())
            {
                var people = repository.QueryAll<Person>().AsList(); ;
                Console.WriteLine($"RepoDb.QueryAll: {people.Count()} row(s) affected for {(DateTime.UtcNow - now).TotalSeconds} second(s).");
            }
            now = DateTime.UtcNow;
            using (var repository = new DatabaseRepository())
            {
                var people = repository.QueryAll<Person>(cacheKey: "People").AsList(); ;
                Console.WriteLine($"RepoDb.QueryAll (Cache): {people.Count()} row(s) affected for {(DateTime.UtcNow - now).TotalSeconds} second(s).");
            }
        }
    }
}
