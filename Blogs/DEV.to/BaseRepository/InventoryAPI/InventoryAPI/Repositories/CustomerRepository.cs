using InventoryAPI.Interfaces;
using InventoryAPI.Models;
using RepoDb;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace InventoryAPI.Repositories
{
    public class CustomerRepository : BaseRepository<Customer, SqlConnection>, ICustomerRepository
    {
        public CustomerRepository() :
            base(@"Server=.;Database=Inventory;Integrated Security=SSPI;")
        { }

        public int Delete(long id)
        {
            return Delete(id);
        }


        public IEnumerable<Customer> GetAll()
        {
            return QueryAll();
        }

        public Customer GetById(long id)
        {
            return Query(id).FirstOrDefault();
        }

        public Customer GetByName(string name)
        {
            return Query(e => e.Name == name).FirstOrDefault();
        }

        public long Insert(Customer customer)
        {
            return Insert<long>(customer);
        }

        public int Update(Customer customer)
        {
            return Update(customer);
        }
    }
}
