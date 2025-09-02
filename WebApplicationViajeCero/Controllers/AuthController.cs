using Microsoft.AspNetCore.Mvc;
using WebApplicationViajeCero.Models;
using WebApplicationViajeCero.Context;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using NuGet.Configuration;
using WebApplicationViajeCero.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace WebApplicationViajeCero.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly TokenJWT _tokenJWT;

        public AuthController(AppDbContext appContext, TokenJWT tokenJWT)
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

        [ProducesResponseType(typeof(GetUsersDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize]
        [HttpGet("Me")]
        
        public IActionResult Me()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized(new
                {
                    error = new { message = "Unauthorized" }
                });
            }

            var user = _appDbContext.Users.Include(u => u.Role).Include(u => u.Province).FirstOrDefault(u => u.Identification == userId);
            if (user == null)
            {
                return NotFound(new { message = "Usuario no encontrado" });
            }

            var userDto = new GetUsersDTO
            {
                Identification = user.Identification,
                Name = user.Name,
                LastName = user.LastName,
                Email = user.Email,
                CellPhone = user.CellPhone,
                Uuid = user.Uuid,
                Role = user.Role.Description,
                Province = user.Province.Name // Assuming Province is optional
            };

            return Ok(userDto);
        }

    }
}
