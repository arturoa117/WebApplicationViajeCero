using Microsoft.AspNetCore.Mvc;
using WebApplicationViajeCero.Models;
using WebApplicationViajeCero.Context;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using NuGet.Configuration;
using WebApplicationViajeCero.DTOs;
using Microsoft.EntityFrameworkCore;

namespace WebApplicationViajeCero.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

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

        public IActionResult Login([FromBody] LoginResponseDTO request)
        {

            var user = _appDbContext.Users.Include(u => u.Role).FirstOrDefault(u => u.Identification == request.Identification);

            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.Password)) 
            {
                return Unauthorized(new {Message = "Credenciales incorrectas"});
            }

            var token = _tokenJWT.GenerateToken(user.Identification, user.Role.Description);
            return Ok(new
            {
                Token = token,
                Expiration = DateTime.UtcNow.AddDays(1)

            });
        }
        

    }
}
