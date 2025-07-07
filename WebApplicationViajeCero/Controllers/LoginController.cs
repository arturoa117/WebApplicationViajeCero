using Microsoft.AspNetCore.Mvc;
using WebApplicationViajeCero.Models;
using WebApplicationViajeCero.Context;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using NuGet.Configuration;

namespace WebApplicationViajeCero.Controllers
{
    [ApiController]
    [Route("api/auth")]

    public class LoginController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly TokenJWT _tokenJWT;

        public LoginController(AppDbContext appContext, TokenJWT tokenJWT)
        {
            _appDbContext = appContext;
            _tokenJWT = tokenJWT;
        }

        [HttpPost("Login")]

        public IActionResult Login([FromBody] User request)
        {
            
            var user = _appDbContext.Users.FirstOrDefault(u => u.UserName == request.UserName && u.Password == request.Password);

            if (user == null) 
            {
                return Unauthorized(new {Message = "Credenciales incorrectas"});
            }

            var token = _tokenJWT.GenerateToken(user.Identification, user.Role.Description);
            return Ok(new { token });
        }
        

    }
}
