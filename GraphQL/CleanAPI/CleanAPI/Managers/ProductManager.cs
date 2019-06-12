using CleanAPI.Configuration;
using CleanAPI.Models;
using CleanAPI.Repositories;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

namespace CleanAPI.Managers
{
    public class ProductManager
    {
        private readonly ProductRepository m_productRepository;
        private readonly ApplicationConfig m_config;

        public ProductManager(IOptions<ApplicationConfig> config, ProductRepository customerRepository)
        {
            m_config = config.Value;
            m_productRepository = customerRepository;
        }

        public IEnumerable<Product> GetByOrderId(int orderId)
        {
            return m_productRepository.GetByOrderId(orderId).Result;
        }

        public IEnumerable<Product> GetByCustomerId(int customerId)
        {
            return m_productRepository.GetByCustomerId(customerId).Result;
        }
    }
}
