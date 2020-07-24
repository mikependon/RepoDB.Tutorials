using Microsoft.Data.SqlClient;
using RepoDb;
using RepoDbDemo.Interfaces;
using RepoDbDemo.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoDbDemo.Repositories
{
    public class SignalRepository : BaseRepository<Signal, SqlConnection>, ISignalRepository
    {
        public SignalRepository(string connectionString) :
            base(connectionString)
        { }

        public async Task<int> SaveAllAsAtomicAsync(IEnumerable<Signal> signals)
        {
            var count = 0;
            foreach (var item in signals)
            {
                await InsertAsync(item);
                ++count;
            }
            return count;
        }

        public async Task<int> SaveAsync(Signal signal)
        {
            return await InsertAsync<int>(signal);
        }

        public async Task<int> SaveAllAsync(IEnumerable<Signal> signals)
        {
            var count = signals.Count();

            if (count <= 30)
            {
                return await SaveAllAsAtomicAsync(signals);
            }
            else if (count <= 1000)
            {
                return await InsertAllAsync(signals);
            }
            else
            {
                return await this.BulkInsertAsync(signals);
            }
        }

        public async Task<Signal> GetAsync(int id)
        {
            return (await QueryAsync(id)).FirstOrDefault();
        }

        public async Task<IEnumerable<Signal>> GetAllAsync()
        {
            return await QueryAllAsync();
        }
    }
}
