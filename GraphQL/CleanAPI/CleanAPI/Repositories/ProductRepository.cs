using CleanAPI.Configuration;
using CleanAPI.Models;
using Microsoft.Extensions.Options;
using RepoDb;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace CleanAPI.Repositories
{
    public class ProductRepository : DbRepository<SqlConnection>
    {
        public ProductRepository(IOptions<ApplicationConfig> appConfig)
            : base(appConfig.Value.ConnectionString)
        {
        }

        public async Task<IEnumerable<Product>> GetByOrderId(int orderId)
        {
            return await ExecuteQueryAsync<Product>("SELECT P.* " +
                "FROM [dbo].[Product] P " +
                "INNER JOIN [dbo].[Order] O ON O.ProductId = P.Id " +
                "WHERE O.Id = @OrderId;", new { OrderId = orderId });
        }

        public async Task<IEnumerable<Product>> GetByCustomerId(int customerId)
        {
            return await ExecuteQueryAsync<Product>("SELECT P.* " +
                "FROM [dbo].[Product] P " +
                "INNER JOIN [dbo].[Order] O ON O.ProductId = P.Id " +
                "WHERE O.CustomerId = @CustomerId;", new { CustomerId = customerId });
        }
    }
}
