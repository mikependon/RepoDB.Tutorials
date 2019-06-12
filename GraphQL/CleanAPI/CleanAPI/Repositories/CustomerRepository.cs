using CleanAPI.Configuration;
using CleanAPI.Models;
using Microsoft.Extensions.Options;
using RepoDb;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CleanAPI.Repositories
{
    public class CustomerRepository : DbRepository<SqlConnection>
    {
        public CustomerRepository(IOptions<ApplicationConfig> appConfig)
            : base(appConfig.Value.ConnectionString)
        {
        }

        public async Task<Customer> GetByIdAsync(int customerId)
        {
            var result = await QueryMultipleAsync<Customer, Order>(c => c.Id == customerId,
                o => o.CustomerId == customerId);
            var customer = result.Item1.FirstOrDefault();
            if (customer != null)
            {
                customer.Orders = result.Item2;
            }
            return customer;
        }

        public async Task<Customer> GetByAccountNoAsync(string accountNo)
        {
            var customer = (await QueryAsync<Customer>(c => c.AccountNo == accountNo)).FirstOrDefault();
            if (customer != null)
            {
                customer.Orders = (await QueryAsync<Order>(o => o.CustomerId == customer.Id));
            }
            return customer;
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await QueryAllAsync<Customer>();
        }
    }
}
