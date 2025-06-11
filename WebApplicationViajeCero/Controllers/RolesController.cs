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
        [HttpGet("{id}")]

        public async Task<ActionResult<Role>> GetRole(int id)
        {
            var role = await _context.Roles.FindAsync(id);

            if (role == null)
            {
                return NotFound();   
            }

            return role;
        }

        //Put api/Roles/5

        [HttpPut("{id}")]

        public async Task<ActionResult<Role>> PutRole(int id, Role role) 
        {
            if (id != role.Id)
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
                if (!RoleExists(id))
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

            return CreatedAtAction("GetRole", new {id = role.Id}, role);
        }

        //DELETE api/Roles/5

        [HttpDelete]

        public async Task<IActionResult> DeleteRole(int id)
        {
            var role = await _context.Roles.FindAsync();

            if (role == null)
            {
                return NotFound();
            }

            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public bool RoleExists(int id)
        {
            return _context.Roles.Any(x => x.Id == id);
        }
    }
}
