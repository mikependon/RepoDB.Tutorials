using CleanAPI.Configuration;
using CleanAPI.Models;
using Microsoft.Extensions.Options;
using RepoDb;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace CleanAPI.Repositories
{
    public class OrderRepository : DbRepository<SqlConnection>
    {
        public OrderRepository(IOptions<ApplicationConfig> appConfig)
            : base(appConfig.Value.ConnectionString)
        {
        }

        public async Task<IEnumerable<Order>> GetByCustomerId(int customerId)
        {
            return await QueryAsync<Order>(o => o.CustomerId == customerId);
        }


        public async Task<IEnumerable<Order>> GetByProductId(int productId)
        {
            return await QueryAsync<Order>(o => o.ProductId == productId);
        }
    }
}
