using AtomicBatchBulkOperations.Models;
using Microsoft.Data.SqlClient;
using RepoDb;
using RepoDb.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AtomicBatchBulkOperations.Repositories
{
    public class PersonRepository : BaseRepository<Person, SqlConnection>
    {
        public PersonRepository() :
            base("Server=PC79000;Database=RepoDB;Integrated Security=SSPI;")
        { }

        public async Task<int> SaveAllAsync(IEnumerable<Person> people)
        {
            var items = people.AsList();
            var result = 0;
            if (items.Count <= 30)
            {
                foreach (var item in items)
                {
                    await InsertAsync(item);
                    ++result;
                }
            }
            else if (items.Count <= 1000)
            {
                result = await InsertAllAsync(items);
            }
            else
            {
                result = await this.BulkInsertAsync(items);
            }
            return result;
        }
    }
}
