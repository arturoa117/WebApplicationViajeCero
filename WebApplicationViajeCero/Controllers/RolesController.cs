using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplicationViajeCero.Context;
using WebApplicationViajeCero.Models;

namespace WebApiViejaCero.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RolesController(AppDbContext context)
        {
            _context = context;
        }

        //GET api/Roles
        [HttpGet]

        public async Task<ActionResult<IEnumerable<Role>>> GetRole()
        {
            return await _context.Roles.ToArrayAsync();
        }

        //GET api/Roles/5
        [HttpGet("{uuid}")]

        public async Task<ActionResult<Role>> GetRole(Guid uuid)
        {
            var role = await _context.Roles.FindAsync(uuid);

            if (role == null)
            {
                return NotFound();   
            }

            return role;
        }

        //Put api/Roles/5

        [HttpPut("{uuid}")]

        public async Task<ActionResult<Role>> PutRole(Guid uuid, Role role) 
        {
            if (uuid != role.Uuid)
            {
                return BadRequest();
            }

            _context.Entry(role).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoleExists(uuid))
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

        //POST api/Roles
        [HttpPost]

        public async Task<ActionResult<Role>> PostRole(Role role)
        {
            _context.Roles.Add(role);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRole", new {uuid = role.Uuid}, role);
        }

        //DELETE api/Roles/5

        [HttpDelete("{uuid}")]

        public async Task<IActionResult> DeleteRole(Guid uuid)
        {
            var role = await _context.Roles.FirstOrDefaultAsync(r => r.Uuid == uuid);

            if (role == null)
            {
                return NotFound(new
                {
                    error = new { message = "Rol no encontrado." }
                });
            }

            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Rol eliminado."
            });
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public bool RoleExists(Guid uuid)
        {
            return _context.Roles.Any(x => x.Uuid == uuid);
        }
    }
}
