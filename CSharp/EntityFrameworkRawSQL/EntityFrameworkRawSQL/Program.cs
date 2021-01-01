using EntityFrameworkRawSQL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EntityFrameworkRawSQL
{
    class Program
    {
        static void Main(string[] args)
        {
            AddRange(1000);
            TruncateFromSqlRaw();
            QueryFromSqlRaw();
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

        static void AddRange(int count)
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

        static void TruncateFromSqlRaw()
        {
            using (var context = new DatabaseContext())
            {
                context.People.FromSqlRaw("TRUNCATE TABLE [dbo].[Person];");
                context.SaveChanges();
            }
        }

        static void QueryFromSqlRaw()
        {
            var now = DateTime.UtcNow;
            using (var context = new DatabaseContext())
            {
                var people = context.People.FromSqlRaw("SELECT * FROM [dbo].[Person];").ToList();
                Console.WriteLine($"EF.People (Raw): {people.Count()} row(s) affected for {(DateTime.UtcNow - now).TotalSeconds} second(s).");
            }
        }
    }
}
