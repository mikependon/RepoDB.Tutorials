using CleanAPI.Managers;
using Microsoft.AspNetCore.Mvc;

namespace CleanAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Product : ControllerBase
    {
        private readonly ProductManager m_productManager;

        public Product(ProductManager productManager)
        {
            m_productManager = productManager;
        }

        [HttpGet("GetByOrderId/{orderId}")]
        public ActionResult<Product> GetByOrderId(int orderId)
        {
            var result = m_productManager.GetByOrderId(orderId);
            return new JsonResult(result);
        }

        [HttpGet("GetByCustomerId/{customerId}")]
        public ActionResult<Product> GetByCustomerId(int customerId)
        {
            var result = m_productManager.GetByCustomerId(customerId);
            return new JsonResult(result);
        }
    }
}
