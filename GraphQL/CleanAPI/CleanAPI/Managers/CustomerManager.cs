using CleanAPI.Configuration;
using CleanAPI.Models;
using CleanAPI.Repositories;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

namespace CleanAPI.Managers
{
    public class CustomerManager
    {
        private readonly CustomerRepository m_customerRepository;
        private readonly ApplicationConfig m_config;

        public CustomerManager(IOptions<ApplicationConfig> config, CustomerRepository customerRepository)
        {
            m_config = config.Value;
            m_customerRepository = customerRepository;
        }

        public IEnumerable<Customer> GetAll()
        {
            return m_customerRepository.GetAllAsync().Result;
        }

        public Customer GetById(int customerId)
        {
            return m_customerRepository.GetByIdAsync(customerId).Result;
        }

        public Customer GetByAccountNo(string accountNo)
        {
            return m_customerRepository.GetByAccountNoAsync(accountNo).Result;
        }
    }
}
