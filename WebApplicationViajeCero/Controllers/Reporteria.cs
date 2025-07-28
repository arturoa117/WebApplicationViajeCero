using ClosedXML.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplicationViajeCero.Context;
using WebApplicationViajeCero.Models;
using static WebApplicationViajeCero.Models.Pagination;

namespace WebApiViejaCero.Controllers

{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ReportController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("excel")]
        public async Task<IActionResult> GenerateExcel()
        {
            var data = await _context.Requests
                .Include(r => r.User)
                .Include(r => r.Service)
                .Include(r => r.Province)
                .ToListAsync();

            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Solicitudes");

            // Encabezados
            worksheet.Cell(1, 1).Value = "ID Solicitud";
            worksheet.Cell(1, 2).Value = "Servicio";
            worksheet.Cell(1, 3).Value = "Total Solicitado";
            worksheet.Cell(1, 4).Value = "Género";
            worksheet.Cell(1, 5).Value = "Provincia";
            worksheet.Cell(1, 6).Value = "Región";
            worksheet.Cell(1, 7).Value = "Fecha";

            foreach (var item in data)
            {
                if (string.IsNullOrWhiteSpace(item.Unavailable) || item.Unavailable == "NULL")
                {
                    item.Unavailable = "N/A";
                    item.ExtraOptionId = null;
                    item.Incident = "N/A";
                }
            }

            for (int i = 0; i < data.Count; i++)
            {
                worksheet.Cell(i + 2, 1).Value = data[i].Id;
                worksheet.Cell(i + 2, 2).Value = data[i].Service?.Name;
                worksheet.Cell(i + 2, 3).Value = data.Where(r => r.Service?.Name == data[i].Service?.Name).Count();
                worksheet.Cell(i + 2, 4).Value = data[i].Sex.ToString();
                worksheet.Cell(i + 2, 5).Value = data[i].Province?.Name;
                worksheet.Cell(i + 2, 6).Value = data[i].Province.Zone.ToString();
                worksheet.Cell(i + 2, 7).Value = data[i].DateCreated.ToString();
            }

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Position = 0;

            return File(stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "Solicitudes.xlsx");
        }
    }
}
