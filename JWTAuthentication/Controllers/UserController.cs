using JWTAuthentication.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.AccessControl;
using System.Security.Claims;
using System.Text;

namespace JWTAuthentication.Controllers
{

    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private IConfiguration _config;
        private IUserService _userService;

        public UserController(IConfiguration config, IUserService userservice)
        {
            _config = config;
            _userService = userservice;
        }


        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login(User user)
        {


            IActionResult result = Unauthorized();
            var isauth = _userService.Login(new DataModel.UserDMO()
            {
                Password = user.Password,
                Email = user.Email
            });
            if (isauth)
            {
                var token = GenerateJsonWebToken(user);
                return Ok(new { token = token });
            }

            return result;

        }
        [HttpPost]
        [Route("Save")]
        [AllowAnonymous]
        public IActionResult Save(User user)
        {
            var result = _userService.AddUser(new DataModel.UserDMO
            {
                Email = user.Email,
                Name = user.Name,
                Surname = user.Surname,
                Password = user.Password,
            });

            return Ok(result);
        }

        [HttpGet]

        [Route("Detail")]
        public IActionResult UserDetail()
        {

            var a = 10;
            return Ok(new { userid = 1 });

        }

        private string GenerateJsonWebToken(User user)
        {

            //return new JwtSecurityTokenHandler().WriteToken(token);


            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenhandler = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(tokenhandler);
        }


    }
}
