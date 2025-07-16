using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using MySqlX.XDevAPI.Common;
using System;
using WebApplicationViajeCero.Context;
using WebApplicationViajeCero.DTOs;
using WebApplicationViajeCero.Models;
using static WebApplicationViajeCero.Models.Pagination;

namespace WebApiViejaCero.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ServicesController(AppDbContext context)
        {

            _context = context;

        }

        //GET api/Services

        [HttpGet]

        public async Task<ActionResult<PagedResult<GetServiceDTO>>> GetServices([FromQuery] int page = 1, [FromQuery] int pageSize = 50, [FromQuery] string query = null)
        {
            if (page <= 0) page = 1;
            if (pageSize <= 0 || pageSize > 100) pageSize = 50;

            var institutionsQuery = _context.Institutions.AsQueryable();

            if (!string.IsNullOrEmpty(query))
            {
                institutionsQuery = institutionsQuery.Where(i => i.Name.Contains(query) || i.Acronym.Contains(query));

                var totalRecords = await _context.Services.CountAsync();

                var services = await _context.Services
                    .Include(s => s.Institution)
                    .OrderBy(S => S.Id)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .Select(s => new GetServiceDTO
                    {

                        Uuid = s.Uuid,
                        Name = s.Name,
                        InstitutionName = s.Institution.Name

                    })
                    .ToListAsync();
                var result = new PagedResult<GetServiceDTO>
                {
                    Total = totalRecords,
                    Page = page,
                    PageSize = pageSize,
                    Data = services
                };
                return Ok(result);
            }

            return Ok(new PagedResult<GetServiceDTO>
            {
                Total = 0,
                Page = page,
                PageSize = pageSize,
                Data = new List<GetServiceDTO>()
            });
        }

            //GET api/Services/5

            [HttpGet("{uuid}")]

            public async Task<ActionResult<Service>> GetService(Guid uuid)
            {
                var service = await _context.Services.FindAsync(uuid);

                if (service == null)
                {
                    return NotFound();
                }

                return service;
            }

            //PUT api/Services/5

            [HttpPut("{uuid}")]

            public async Task<IActionResult> PutService(Guid uuid, UpdateServiceDTO service)
            {
                var foundService = await _context.Services.FirstOrDefaultAsync(e => e.Uuid == uuid);

                if (foundService == null) return NotFound("Service not found");

                var institution = await _context.Institutions.FirstOrDefaultAsync(e => e.Uuid == service.InstitutionUuid);

                if (institution == null) return NotFound("Institution not found");

                _context.Entry(service).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServiceExists(uuid))
                    {
                        return NotFound(uuid);
                    }
                    else
                    {
                        throw;
                    }
                }

                return NoContent();
            }

            //POST api/Services

            [HttpPost]

            public async Task<ActionResult<CreateServiceDTO>> PostService(CreateServiceDTO service)
            {
                var institution = await _context.Institutions.FirstOrDefaultAsync(e => e.Uuid == service.InstitutionUuid);

                if (institution == null) return NotFound("Institution not found");

                _context.Services.Add(new Service { Name = service.Name, InstitutionId = institution.Id });
                await _context.SaveChangesAsync();

                return Ok("Service created successfully");
            }

            //DELETE api/Services/5

            [HttpDelete]

            public async Task<IActionResult> DeleteService(Guid uuid)
            {
                var service = await _context.Services.FindAsync(uuid);

                if (service == null)
                {
                    return NotFound();
                }
            
                _context.Services.Remove(service);
                await _context.SaveChangesAsync();

                return NoContent();
            }

        private bool ServiceExists(Guid uuid)
        {
            return _context.Services.Any(e => e.Uuid == uuid);
        }
    }
}
