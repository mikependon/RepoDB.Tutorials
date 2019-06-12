using CleanAPI.Managers;
using Microsoft.AspNetCore.Mvc;

namespace CleanAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Order : ControllerBase
    {
        private readonly OrderManager m_orderManager;

        public Order(OrderManager orderManager)
        {
            m_orderManager = orderManager;
        }

        [HttpGet("GetByCustomerId/{customerId}")]
        public ActionResult<Order> GetByCustomerId(int customerId)
        {
            var result = m_orderManager.GetByCustomerId(customerId);
            return new JsonResult(result);
        }

        [HttpGet("GetByProductId/{productId}")]
        public ActionResult<Order> GetByProductId(int productId)
        {
            var result = m_orderManager.GetByProductId(productId);
            return new JsonResult(result);
        }
    }
}
