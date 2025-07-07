using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{uuid}")]
        public async Task<ActionResult<User>> GetUser(Guid uuid)
        {
            var user = await _context.Users.FindAsync(uuid);

            if (user == null)
            {
                return NotFound();
            }

            return user;
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
        public async Task<ActionResult<CreateUserDTO>> PostUser(CreateUserDTO userDto)
        {
            if (await _context.Users.AnyAsync(u => u.Identification == userDto.Identification))
                return BadRequest("La cédula ya está registrada.");
            if (await _context.Users.AnyAsync(u => u.UserName == userDto.UserName))
                return BadRequest("El nombre de usuario ya existe.");
            if (await _context.Users.AnyAsync(u => u.Email == userDto.Email))
                return BadRequest("El correo ya está registrado.");

            var province = await _context.Provinces.FirstOrDefaultAsync(p => p.Id == userDto.ProvinceId);
            if (province == null) return BadRequest("Provincia no válida.");

            var role = await _context.Roles.FirstOrDefaultAsync(r => r.Id == userDto.RoleId);
            if (role == null) return BadRequest("Rol no válido.");

            var newUser = new User
            {
                Identification = userDto.Identification,
                Name = userDto.Name,
                LastName = userDto.LastName,
                Email = userDto.Email,
                CellPhone = userDto.CellPhone,
                Zone = userDto.Zone,
                UserName = userDto.UserName,
                Password = userDto.Password, 
                ProvinceId = province.Id,
                RoleId = role.Id
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(PostUser), new { id = newUser.Id }, userDto);
        }

        // DELETE: api/Users/5
        [HttpDelete("{uuid}")]
        public async Task<IActionResult> DeleteUser(Guid uuid)
        {
            var user = await _context.Users.FindAsync(uuid);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(Guid uuid)
        {
            return _context.Users.Any(e => e.Uuid == uuid);
        }
    }
}
