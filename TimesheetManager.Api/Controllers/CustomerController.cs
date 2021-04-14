using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

using TimesheetManager.Api.Models;
using TimesheetManager.Api.Repositories;
using TimesheetManager.Api.Exceptions;

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
        public async Task<ActionResult<Response>> Index()
        {
            var customers = await _customerRepository.List();
            return Ok(new Response{
                Status = 200,
                Message = "",
                Data = customers
            });
        }










        [HttpGet]
        [Route("details")]
        public async Task<ActionResult<Response>> Details(int id)
        {

            var customer = await _customerRepository.GetById(id);

            if(customer == null)
                return NotFound(new Response {
                    Status = 404,
                    Message = "Cliente não encontrado",
                    Data = null
                });

            
            return Ok(new Response{
                Status = 200,
                Message = "",
                Data = customer
            });

        }










        [HttpPost]
        [Route("create")]
        public async Task<ActionResult<User>> Create([FromBody] Customer model)
        {
            await _customerRepository.Insert(customer: model);
            return Ok("Cliente Inserido com Sucesso!");
        }








        [HttpPut]
        [Route("update")]
        public async Task<ActionResult<Response>> Update([FromBody] Customer model, [FromHeader] int id)
        {
            if(model.Id != id)
                return BadRequest(new Response{
                    Status = 409,
                    Message = "Id enviado e o objeto que deve-se fazer a alteração são distintos.",
                    Data = null
                });
            




            
            try
            {
                await _customerRepository.Update(id, model);


                return Ok(new Response{
                    Status = 200,
                    Message = "Dados do cliente alterados com sucesso.",
                    Data = model
                });

            }
            catch(NotFoundException e)
            {
                return NotFound(new Response{
                    Status = 404,
                    Message = "Cliente não encontrado",
                    Data = null
                });
            }


        }









        
    }
}