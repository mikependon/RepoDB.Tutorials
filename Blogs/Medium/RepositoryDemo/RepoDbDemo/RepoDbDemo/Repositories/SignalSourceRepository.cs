using Microsoft.Data.SqlClient;
using RepoDb;
using RepoDbDemo.Interfaces;
using RepoDbDemo.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RepoDbDemo.Repositories
{
    public class SignalSourceRepository : BaseRepository<SignalSource, SqlConnection>, ISignalSourceRepository
    {
        public SignalSourceRepository(string connectionString) :
            base(connectionString)
        { }

        public async Task<IEnumerable<SignalSource>> GetAllAsync()
        {
            return await QueryAllAsync(cacheKey: "SignalSources");
        }
    }
}
