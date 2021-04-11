using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

using TimesheetManager.Api.Models;
using TimesheetManager.Api.Repositories;
using System;
using TimesheetManager.Api.Exceptions;

namespace TimesheetManager.Api.Controllers
{
    [Route("v1/user")]
    public class UserController : Controller
    {

        private readonly UserRepository _userRepository;


        public UserController(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }





        [HttpGet]
        [Route("index")]
        [AllowAnonymous]
        public async Task<ActionResult<Response>> Index()
        {
            var list = await _userRepository.List();

            return Ok(new Response{
                Status = 200,
                Message = "",
                Data = list
            });   
        }





        [HttpGet]
        [Route("details")]
        public async Task<ActionResult<Response>> Details(int id)
        {

            var user = await _userRepository.GetById(id);

            if(user == null)
                return NotFound(new Response {
                    Status = 404,
                    Message = "Usuário não encontrado",
                    Data = null
                });

            
            return Ok(new Response{
                Status = 200,
                Message = "",
                Data = user
            });

        }









        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        public async Task<ActionResult<User>> Create([FromBody] User model)
        {
            int id = await _userRepository.Insert(user: model);

            User user = await _userRepository.GetById(model.Id);

            return Ok(new Response{ 
                Status = 200,
                Message = "Usuário cadastrado com sucesso.",
                Data = user
            });
        }







        [HttpPut]
        [Route("update")]
        public async Task<ActionResult<Response>> Update([FromBody] User model, [FromHeader] int id)
        {
            if(model.Id != id)
                return BadRequest(new Response{
                    Status = 409,
                    Message = "Id enviado e o objeto que deve-se fazer a alteração são distintos.",
                    Data = null
                });
            




            
            try
            {
                await _userRepository.Update(id, model);


                return Ok(new Response{
                    Status = 200,
                    Message = "Dados do usuário alterados com sucesso.",
                    Data = model
                });

            }
            catch(NotFoundException e)
            {
                return NotFound(new Response{
                    Status = 404,
                    Message = "Usuário não encontrado",
                    Data = null
                });
            }


        }

    }
}