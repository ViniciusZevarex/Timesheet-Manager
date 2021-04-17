using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TimesheetManager.Api.Models;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System;
using Microsoft.Extensions.Options;
using TimesheetManager.Api.Repositories;

namespace TimesheetManager.Api.Controllers
{
    [Route("v1/auth")]
    public class AuthController : Controller
    {

        private readonly JWTSettings _jwtsettings;
        private readonly UserRepository _userRepository;



        public AuthController(IOptions<JWTSettings> jwtsettings, UserRepository userRepository)
        {
            this._jwtsettings = jwtsettings.Value;
            this._userRepository = userRepository;
        }









        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<UserToken>> Authenticate([FromBody] User model)
        {
            var user = await _userRepository.Login(model.Email, model.Password);

            if(user == null)
                return NotFound(new { message = "Email ou senha inválidos"});

            var token = GenerateAccessToken(user);

            return new UserToken {
                User = user,
                Token = token
            };
            
        }






        private string GenerateAccessToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(this._jwtsettings.SecretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]{
                    new Claim(ClaimTypes.Name, user.Email.ToString()),
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha384Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }


    }
}