using InventoryAPI.Models;
using System.Collections.Generic;

namespace InventoryAPI.Interfaces
{
    public interface ICustomerRepository
    {
        Customer GetById(long id);
        Customer GetByName(string name);
        IEnumerable<Customer> GetAll();
        long Insert(Customer customer);
        int Update(Customer customer);
        int Delete(long id);
    }
}
