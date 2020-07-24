using Microsoft.Data.SqlClient;
using RepoDb;
using RepoDbDemo.Interfaces;
using RepoDbDemo.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RepoDbDemo.Repositories
{
    public class SignalTypeRepository : BaseRepository<SignalType, SqlConnection>, ISignalTypeRepository
    {
        public SignalTypeRepository(string connectionString) :
            base(connectionString)
        { }

        public async Task<IEnumerable<SignalType>> GetAllAsync()
        {
            return await QueryAllAsync(cacheKey: "SignalTypes");
        }
    }
}
