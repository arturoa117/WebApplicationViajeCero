using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BCrypt.Net;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore;
using WebApplicationViajeCero.Context;
using WebApplicationViajeCero.DTOs;
using WebApplicationViajeCero.Models;

namespace WebApiViejaCero.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetUsersDTO>>> GetUsers()
        {
            var usersQuery = _context.Users
           .Include(u => u.Role)
           .Include(u => u.Province);
            

            var users = await usersQuery
            .Select(u => new GetUsersDTO
            {
             Identification = u.Identification,
             Name = u.Name,
             LastName = u.LastName,
             Email = u.Email,
             CellPhone = u.CellPhone,
             Uuid = u.Uuid,
             Role = u.Role.Description,
             Province = u.Province.Name
            })
            .ToListAsync();

            return Ok(users);
        }

        // GET: api/Users/5
        [HttpGet("{uuid}")]
        public async Task<ActionResult<GetUsersDTO>> GetUser(GetUsersDTO getUsersDTO)
        {
            var user = await _context.Users.FindAsync(getUsersDTO);
            var role = await _context.Roles.FirstOrDefaultAsync(r => r.Description == getUsersDTO.Role);
            var province = await _context.Provinces.FirstOrDefaultAsync(p => p.Name == getUsersDTO.Province);

            if (user == null)
            {
                return NotFound(new
                {
                    error = new { message = "Usuario no encontrado." }
                });
            }

            var userDTO = new GetUsersDTO
            {
                Identification = user.Identification,
                Name = user.Name,
                LastName = user.LastName,
                Email = user.Email,
                CellPhone = user.CellPhone,
                Uuid = user.Uuid,
                Role = role.Description,
                Province = province.Name
            };

            return Ok(userDTO);
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{uuid}")]
        public async Task<IActionResult> PutUser(Guid uuid, User user)
        {
            if (uuid != user.Uuid)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(uuid))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult<CreateUserDTO>> PostUser(CreateUserDTO userDTO)
        {
            if (await _context.Users.AnyAsync(u => u.Identification == userDTO.Identification))
                return BadRequest(new
                {
                    error = new { message = "La cédula ya está registrada." }
                });
        
            if (await _context.Users.AnyAsync(u => u.Email == userDTO.Email))
                return BadRequest(new
                {
                    error = new { message = "El correo ya está registrada." }
                });

            var province = await _context.Provinces.FirstOrDefaultAsync(p => p.Uuid == userDTO.ProvinceUuid);
            if (province == null) return BadRequest(new
            {
                error = new { message = "Provincia no valida." }
            });

            var role = await _context.Roles.FirstOrDefaultAsync(r => r.Description == userDTO.Role);
            if (role == null) return BadRequest(new
            {
                error = new { message = "Rol no valido." }
            });

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(userDTO.Password);

            var newUser = new User
            {
                Identification = userDTO.Identification,
                Name = userDTO.Name,
                LastName = userDTO.LastName,
                Email = userDTO.Email,
                CellPhone = userDTO.CellPhone,
                ProvinceId = province.Id,
                RoleId = role.Id
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(PostUser), new { id = newUser.Id }, userDTO);
        }

        // DELETE: api/Users/5
        [HttpDelete("{uuid}")]
        public async Task<IActionResult> DeleteUser(Guid uuid)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Uuid == uuid);
            if (user == null)
            {
                return NotFound(new
                {
                    error = new { message = "Usuario no encontrado." }
                });
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Usuario eliminado." 
            });
        }

        private bool UserExists(Guid uuid)
        {
            return _context.Users.Any(e => e.Uuid == uuid);
        }
    }
}
