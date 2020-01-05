using InventoryAPI.Interfaces;
using InventoryAPI.Models;
using Microsoft.AspNetCore.Mvc;
using RepoDb.Extensions;
using System.Collections.Generic;

namespace InventoryAPI.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class CustomerController : ControllerBase
	{
		private ICustomerRepository m_customerRepository;

		public CustomerController(ICustomerRepository repository)
		{
			m_customerRepository = repository;
		}

        [HttpGet()]
        public ActionResult<IEnumerable<Customer>> Get()
        {
            return m_customerRepository.GetAll().AsList();
        }

        [HttpGet("{id}")]
        public ActionResult<Customer> Get(long id)
        {
            return m_customerRepository.GetById(id);
        }
    }
}