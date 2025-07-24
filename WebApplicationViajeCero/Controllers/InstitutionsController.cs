using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplicationViajeCero.Models;
using WebApplicationViajeCero.Context;
using Microsoft.EntityFrameworkCore;
using static WebApplicationViajeCero.Models.Pagination;

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
        public async Task<ActionResult<PagedResult<Institution>>> GetInstitution([FromQuery] int page = 1, [FromQuery] int pagesize = 50, [FromQuery] string query = null)
        {
            if (page <= 0) page = 0;
            if (pagesize <= 0 || pagesize > 100) pagesize = 50;

            var institutionsQuery = _context.Services.AsQueryable();

            if (!string.IsNullOrEmpty(query))
            {
                institutionsQuery = institutionsQuery.Where(i => i.Name.Contains(query) || i.Institution.Acronym.Contains(query));
            }   

            var totalRecords = await _context.Institutions.CountAsync();

            var institutions = await _context.Institutions
                .OrderBy(i => i.Name)
                .Skip((page - 1) * pagesize)
                .Take(pagesize)
                .Select(i => new Institution
                {

                    Uuid = i.Uuid,
                    Name = i.Name,
                    Acronym = i.Acronym

                })
                .ToListAsync();
            var result = new PagedResult<Institution>
            {
                Total = totalRecords,
                Page = page,
                PageSize = pagesize,
                Data = institutions
            };

            return Ok(result);
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

        [HttpDelete("{uuid}")]

        public async Task<IActionResult> DeleteInstitution(Guid uuid)
        {
            var institution = await _context.Institutions.FirstOrDefaultAsync(i => i.Uuid == uuid);

            if (institution == null)
            {
                return NotFound(new
                {
                    error = new { message = "Institución no encontrada." }
                });
            }

            _context.Institutions.Remove(institution);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Institución eliminada."
            });
        }

        private bool InstitutionExists(Guid uuid)
        {
            return _context.Institutions.Any(e => e.Uuid == uuid);
        }
    }
}
