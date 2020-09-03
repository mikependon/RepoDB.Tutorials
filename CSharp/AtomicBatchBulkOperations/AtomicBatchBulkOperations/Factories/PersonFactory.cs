using AtomicBatchBulkOperations.Models;
using System;
using System.Collections.Generic;

namespace AtomicBatchBulkOperations.Factories
{
    public class PersonFactory
    {
        public IEnumerable<Person> GetPeople(int count)
        {
            var random = new Random();
            for (var i = 0; i < count; i++)
            {
                yield return new Person
                {
                    Name = $"Name={i}",
                    DateOfBirth = DateTime.UtcNow.AddDays(random.Next(100)),
                    CreatedDateUtc = DateTime.UtcNow,
                    ExtendedInfo = $"ExtendedInfo-{i}",
                    Age = i,
                    IsActive = true
                };
            }
        }
    }
}
