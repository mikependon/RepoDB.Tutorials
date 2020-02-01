using PropertyHandler.Models;
using System;
using System.Collections.Generic;

namespace PropertyHandler.Factories
{
    public static class PersonFactory
    {
        public static Person Create()
        {
            return new Person
            {
                Name = $"Name-{Guid.NewGuid().ToString()}",
                Age = $"{(short)(new Random().Next(100))} years old",
                ExtendedInfo = new PersonExtendedInfo
                {
                    Address = $"Address-{Guid.NewGuid().ToString()}",
                    Affiliations = $"Affiliations-{Guid.NewGuid().ToString()}",
                    Biography = $"Biography-{Guid.NewGuid().ToString()}",
                    Certifications = $"Certifications-{Guid.NewGuid().ToString()}"
                },
                CreatedDateUtc = DateTime.UtcNow
            };
        }

        public static IEnumerable<Person> CreateMultiple(int count)
        {
            for (var i = 0; i < count; i++)
            {
                yield return Create();
            }
        }
    }
}
