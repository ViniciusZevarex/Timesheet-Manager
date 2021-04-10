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

namespace TimesheetManager.Api.Controllers
{
    [Route("v1/auth")]
    public class AuthController : Controller
    {

        private readonly JWTSettings _jwtsettings;



        public AuthController(IOptions<JWTSettings> jwtsettings)
        {
            this._jwtsettings = jwtsettings.Value;
        }




        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<UserToken>> Authenticate([FromBody] User model)
        {
            var user = new User{
                Id = 1,
                Name = "Vinicius.Zevarex",
                Email = "vinicius.zevarex2002@gmail.com",
                Avatar = "/images/avatars/avatar_1.jpeg"
            };

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