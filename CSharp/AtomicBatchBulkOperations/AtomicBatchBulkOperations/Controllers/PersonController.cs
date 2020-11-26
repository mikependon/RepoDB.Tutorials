using AtomicBatchBulkOperations.Enumerations;
using AtomicBatchBulkOperations.Factories;
using AtomicBatchBulkOperations.Models;
using AtomicBatchBulkOperations.Repositories;
using Microsoft.AspNetCore.Mvc;
using RepoDb;
using RepoDb.Extensions;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace AtomicBatchBulkOperations.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly PersonRepository personRepository;
        private readonly PersonFactory personFactory;

        public PersonController(PersonRepository personRepository,
            PersonFactory personFactory)
        {
            this.personRepository = personRepository;
            this.personFactory = personFactory;
        }

        [HttpPost("clear")]
        public async Task<string> Clear()
        {
            var now = DateTime.UtcNow;
            var deletedRows = await personRepository.DeleteAllAsync();
            return $"The table Person has been cleared with '{deletedRows}' row(s) affected for '{(DateTime.UtcNow - now).TotalSeconds}' second(s).";
        }

        [HttpGet()]
        public async Task<IEnumerable<Person>> Get()
        {
            var orderBy = OrderField.Descending<Person>(p => p.Id).AsEnumerable();
            var topRows = 10;
            return (await personRepository.QueryAsync(what: null, top: topRows, orderBy: orderBy)).AsList();
        }

        [HttpPost("createatomic")]
        public async Task<string> CreateAtomic([FromBody] int count = 1000)
        {
            var people = personFactory.GetPeople(count);
            var now = DateTime.UtcNow;
            var insertedRows = 0;
            using (var connection = (DbConnection)await personRepository.CreateConnection().EnsureOpenAsync())
            {
                using (var transaction = await connection.BeginTransactionAsync())
                {
                    foreach (var person in people)
                    {
                        await personRepository.InsertAsync(person, transaction: transaction);
                        ++insertedRows;
                    }
                    transaction.Commit();
                }
            }
            return $"Atomic: Inserted '{insertedRows}' row(s) for '{(DateTime.UtcNow - now).TotalSeconds}' second(s).";
        }

        [HttpPost("createbatch")]
        public async Task<string> CreateBatch([FromBody] int count = 1000)
        {
            var people = personFactory.GetPeople(count);
            var now = DateTime.UtcNow;
            var insertedRows = await personRepository.InsertAllAsync(people, batchSize: 50);
            return $"Batch: Inserted '{insertedRows}' row(s) for '{(DateTime.UtcNow - now).TotalSeconds}' second(s).";
        }

        [HttpPost("createbulk")]
        public async Task<string> CreateBulk([FromBody] int count = 1000)
        {
            var people = personFactory.GetPeople(count);
            var now = DateTime.UtcNow;
            var insertedRows = await personRepository.BulkInsertAsync(people);
            return $"Bulk: Inserted '{insertedRows}' row(s) for '{(DateTime.UtcNow - now).TotalSeconds}' second(s).";
        }

        [HttpPost("createdynamic")]
        public async Task<string> CreateDynamic([FromBody] int count = 1000)
        {
            var people = personFactory.GetPeople(count);
            var now = DateTime.UtcNow;
            var insertedRows = await personRepository.SaveAllAsync(people);
            return $"Dynamic: Inserted '{insertedRows}' row(s) for '{(DateTime.UtcNow - now).TotalSeconds}' second(s).";
        }
    }
}
