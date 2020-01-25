using BatchOperation.Models;
using System;
using System.Collections.Generic;

namespace BatchOperation.Factories
{
    public static class CustomerFactory
    {
        public static IEnumerable<Customer> CreateCustomers(int count)
        {
            for (var i = 0; i < count; i++)
            {
                yield return new Customer
                {
                    Name = $"Address{i}",
                    SSN = $"Address{i}",
                    Address = $"Address{i}",
                    CreatedUtc = DateTime.UtcNow,
                    ModifiedUtc = DateTime.UtcNow
                };
            }
        }
    }
}
