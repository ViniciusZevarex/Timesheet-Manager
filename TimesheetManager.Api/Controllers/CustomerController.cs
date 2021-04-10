using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

using TimesheetManager.Api.Models;
using TimesheetManager.Api.Repositories;

namespace TimesheetManager.Api.Controllers
{

    [Route("v1/customer")]
    public class CustomerController : Controller
    {

        private readonly CustomerRepository _customerRepository;


        public CustomerController(CustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        

        [HttpGet]
        [Route("index")]
        public async Task<ActionResult<List<Customer>>> Index()
        {
            var customers = await _customerRepository.List();
            return  Ok(customers);
        }


        [HttpPost]
        [Route("create")]
        public async Task<ActionResult<User>> Create([FromBody] Customer model)
        {
            await _customerRepository.Insert(customer: model);
            return Ok("Cliente Inserido com Sucesso!");
        }


    }
}