using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TimesheetManager.Api.Models;
using Microsoft.Extensions.Options;

namespace TimesheetManager.Api.Controllers
{
    [Route("v1/user")]
    public class UserController : Controller
    {



        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        public async Task<ActionResult<User>> Create([FromBody] User model)
        {
            return Ok(new { message  = "Ok"});
        }

    }
}