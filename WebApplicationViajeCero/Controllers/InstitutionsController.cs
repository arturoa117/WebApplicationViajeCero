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

        [HttpGet("{id}")]

        public async Task<ActionResult<Institution>> GetInstitution(int id)
        {
            var institutions = await _context.Institutions.FindAsync();

            if (institutions == null)
            {
                return NotFound();
            }

            return institutions;
        }

        //PUT api/Institutions

        [HttpPut("{id}")]

        public async Task<IActionResult> PutInstitution(int id, Institution institution)
        {
            if (id != institution.Id)
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
                if (!ProvinceExists(id))
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

            return CreatedAtAction("GetInstitution", new {id = institution.Id}, institution);
        }

        //DELETE api/Institutions/5

        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteInstitution(int id)
        {
            var institution = await _context.Institutions.FindAsync(id);

            if (institution == null)
            {
                return NotFound();
            }

            _context.Institutions.Remove(institution);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProvinceExists(int id)
        {
            return _context.Provinces.Any(e => e.Id == id);
        }
    }
}
