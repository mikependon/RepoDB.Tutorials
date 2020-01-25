using BatchOperation.Factories;
using BatchOperation.Repositories;
using RepoDb.Extensions;
using System;

namespace BatchOperation
{
    class Program
    {
        static void Main(string[] args)
        {
            // Clear
            Clear();

            // 1K
            Console.WriteLine("Executing via RepoDb. First execution is with compilations.");
            RepoDbBatchInsert(1000);
            Console.WriteLine("Executing via ADO.NET.");
            AdoBatchInsert(1000);
            Console.WriteLine("Executing via RepoDb. AOT compilation.");
            RepoDbBatchInsert(1000);

            // 10K
            Console.WriteLine(new string('-', 50));
            Console.WriteLine("Executing via ADO.NET.");
            AdoBatchInsert(10000);
            Console.WriteLine("Executing via RepoDb. AOT compilation.");
            RepoDbBatchInsert(10000);

            // 100K
            Console.WriteLine(new string('-', 50));
            Console.WriteLine("Executing via ADO.NET.");
            AdoBatchInsert(100000);
            Console.WriteLine("Executing via RepoDb. AOT compilation.");
            RepoDbBatchInsert(100000);

            // Exit
            Console.WriteLine("Press any key to exit.");
            Console.ReadLine();
        }

        private static void Clear()
        {
            new RepoDbCustomerRepository().Clear();
        }

        private static void AdoBatchInsert(int count)
        {
            var customers = CustomerFactory.CreateCustomers(count).AsList();
            var now = DateTime.UtcNow;
            var repository = new AdoCustomerRepository();
            repository.BatchInsert(customers);
            Console.WriteLine($"{(DateTime.UtcNow - now).TotalSeconds} second(s) for ADO.NET with {customers.Count} customers.");
        }

        private static void RepoDbBatchInsert(int count)
        {
            var customers = CustomerFactory.CreateCustomers(count).AsList();
            var now = DateTime.UtcNow;
            var repository = new RepoDbCustomerRepository();
            repository.BatchInsert(customers);
            Console.WriteLine($"{(DateTime.UtcNow - now).TotalSeconds} second(s) for RepoDb with {customers.Count} customers.");
        }
    }
}
