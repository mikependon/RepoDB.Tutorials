using EntityFrameworkRepoDbCombination.DbContexts;
using EntityFrameworkRepoDbCombination.Models;
using EntityFrameworkRepoDbCombination.Repositories;
using RepoDb;
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
            Initialize();

            // First Run
            Console.WriteLine("First run with compilation!");
            BulkInsertRepoDb(rows);
            AddRangeEF(rows);
            InsertAllRepoDb(rows);
            QueryEF();
            QueryRepoDb();

            // Second Run
            Console.WriteLine(new string(char.Parse("-"), 100));
            Console.WriteLine("Second run without compilation!");
            BulkInsertRepoDb(rows);
            AddRangeEF(rows);
            InsertAllRepoDb(rows);
            QueryEF();
            QueryRepoDb();
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
                var people = context.People.ToList();
                Console.WriteLine($"EF.People: {people.Count()} row(s) affected for {(DateTime.UtcNow - now).TotalSeconds} second(s).");
            }
        }

        static void QueryRepoDb()
        {
            var now = DateTime.UtcNow;
            using (var repository = new DatabaseRepository())
            {
                var people = repository.QueryAll<Person>();
                Console.WriteLine($"RepoDb.QueryAll: {people.Count()} row(s) affected for {(DateTime.UtcNow - now).TotalSeconds} second(s).");
            }
        }
    }
}
