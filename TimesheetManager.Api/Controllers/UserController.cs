using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

using TimesheetManager.Api.Models;
using TimesheetManager.Api.Repositories;

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
        public async Task<ActionResult<List<User>>> Index()
        {
            var list = await _userRepository.List();

            return Ok(list);   
        }


        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        public async Task<ActionResult<User>> Create([FromBody] User model)
        {
            await _userRepository.Insert(user: model);

            return Ok("Usuario Inserido com Sucesso!");
        }

    }
}