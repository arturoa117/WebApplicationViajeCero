using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplicationViajeCero.Models;
using WebApplicationViajeCero.Context;
using Microsoft.EntityFrameworkCore;

namespace WebApiViejaCero.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstitutionsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public InstitutionsController(AppDbContext context)
        {
            _context = context;
        }

        //GET api/Institutions

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Institution>>> GetInstitution()
        {
            return await _context.Institutions.ToListAsync();

        }

        //GET api/Institutions/5

        [HttpGet("{uuid}")]

        public async Task<ActionResult<Institution>> GetInstitution(Guid uuid)
        {
            var institutions = await _context.Institutions.FindAsync();

            if (institutions == null)
            {
                return NotFound();
            }

            return institutions;
        }

        //PUT api/Institutions

        [HttpPut("{uuid}")]

        public async Task<IActionResult> PutInstitution(Guid uuid, Institution institution)
        {
            if (uuid != institution.Uuid)
            {
                return BadRequest();
            }

            _context.Entry(institution).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InstitutionExists(uuid))
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

        //POST api/Institutions

        [HttpPost]

        public async Task<ActionResult<Institution>> PostInstitution(Institution institution)
        {
            _context.Institutions.Add(institution);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInstitution", new {uuid = institution.Uuid}, institution);
        }

        //DELETE api/Institutions/5

        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteInstitution(Guid uuid)
        {
            var institution = await _context.Institutions.FindAsync(uuid);

            if (institution == null)
            {
                return NotFound();
            }

            _context.Institutions.Remove(institution);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InstitutionExists(Guid uuid)
        {
            return _context.Institutions.Any(e => e.Uuid == uuid);
        }
    }
}
