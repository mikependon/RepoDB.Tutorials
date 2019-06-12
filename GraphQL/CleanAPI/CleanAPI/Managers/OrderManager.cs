using CleanAPI.Configuration;
using CleanAPI.Models;
using CleanAPI.Repositories;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

namespace CleanAPI.Managers
{
    public class OrderManager
    {
        private readonly OrderRepository m_orderRepository;
        private readonly ApplicationConfig m_config;

        public OrderManager(IOptions<ApplicationConfig> config, OrderRepository customerRepository)
        {
            m_config = config.Value;
            m_orderRepository = customerRepository;
        }

        public IEnumerable<Order> GetByCustomerId(int customerId)
        {
            return m_orderRepository.GetByCustomerId(customerId).Result;
        }

        public IEnumerable<Order> GetByProductId(int productId)
        {
            return m_orderRepository.GetByProductId(productId).Result;
        }
    }
}
