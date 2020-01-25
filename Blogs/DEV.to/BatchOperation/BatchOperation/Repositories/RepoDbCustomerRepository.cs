using BatchOperation.Models;
using RepoDb;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace BatchOperation.Repositories
{
    public class RepoDbCustomerRepository : BaseRepository<Customer, SqlConnection>
    {
        public RepoDbCustomerRepository()
            : base(@"Server=.;Database=BatchDB;Integrated Security=SSPI;")
        { }

        public int BatchInsert(IEnumerable<Customer> customers)
        {
            return InsertAll(customers, batchSize: 100);
        }

        public int BatchMerge(IEnumerable<Customer> customers)
        {
            return MergeAll(customers, batchSize: 100);
        }

        public int BatchUpdate(IEnumerable<Customer> customers)
        {
            return UpdateAll(customers, batchSize: 100);
        }

        public int Clear()
        {
            return Truncate();
        }
    }
}
