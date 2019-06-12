using System.Collections.Generic;
using CleanAPI.Managers;
using Microsoft.AspNetCore.Mvc;
using RepoDb.Extensions;

namespace CleanAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Customer : ControllerBase
    {
        private readonly CustomerManager m_customerManager;

        public Customer(CustomerManager customerManager)
        {
            m_customerManager = customerManager;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Customer>> All()
        {
            var result = m_customerManager.GetAll().AsList();
            return new JsonResult(result);
        }

        [HttpGet("GetById/{customerId}")]
        public ActionResult<Customer> GetById(int customerId)
        {
            var result = m_customerManager.GetById(customerId);
            return new JsonResult(result);
        }

        [HttpGet("GetByAccountNo/{accountNo}")]
        public ActionResult<Customer> GetByAccountNo(string accountNo)
        {
            var result = m_customerManager.GetByAccountNo(accountNo);
            return new JsonResult(result);
        }
    }
}
