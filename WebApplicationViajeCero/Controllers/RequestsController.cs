using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Spreadsheet;
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
    public class RequestsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RequestsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Requests
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Request>>> GetRequests()
        {
            return await _context.Requests.ToListAsync();
        }

        // GET: api/Requests/5
        [HttpGet("{uuid}")]
        public async Task<ActionResult<Request>> GetRequest(Guid uuid)
        {
            var request = await _context.Requests.FindAsync(uuid);

            if (request == null)
            {
                return NotFound();
            }

            return request;
        }

        // PUT: api/Requests/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{uuid}")]
        public async Task<IActionResult> PutRequest(Guid uuid, Request request)
        {
            if (uuid != request.Uuid)
            {
                return BadRequest();
            }

            _context.Entry(request).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestExists(uuid))
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

        // POST: api/Requests
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Request>> PostRequest(CreateRequestDTO requestDTO)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Uuid == requestDTO.UserUuid);

            if (user == null)
                return BadRequest(new
                {
                    error = new { message = "Usuario no encontrado." }
                });

            var sex = await _context.Requests.FirstOrDefaultAsync(s => s.Sex == requestDTO.Sex);

            if (user == null)
                return BadRequest(new
                {
                    error = new { message = "Sexo no valido." }
                });

            var service = await _context.Services.FirstOrDefaultAsync(se => se.Uuid == requestDTO.ServiceUuid);

            if (service == null)
                return BadRequest(new
                {
                    error = new { message = "Servicio no encontrado."}
                });

            var province = await _context.Provinces.FirstOrDefaultAsync(p => p.Uuid == requestDTO.ProvinceUuid);

            if (province == null)
                return BadRequest(new
                {
                    error = new { message = "Provincia no encontrada." }
                });

            var newRequest = new Request
            {
                UserId = user.Id,
                Sex = requestDTO.Sex,
                ServiceId = service.Id,
                ProvinceId = province.Id


            };

            _context.Requests.Add(newRequest);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(PostRequest), new { id = newRequest.Id }, requestDTO);

        }

        // DELETE: api/Requests/5
        [HttpDelete("{uuid}")]
        public async Task<IActionResult> DeleteRequest(Guid uuid)
        {
            var request = await _context.Requests.FirstOrDefaultAsync(r => r.Uuid == uuid);
            if (request == null)
            {
                return NotFound();
            }

            _context.Requests.Remove(request);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RequestExists(Guid uuid)
        {
            return _context.Requests.Any(e => e.Uuid == uuid);
        }
    }
}
