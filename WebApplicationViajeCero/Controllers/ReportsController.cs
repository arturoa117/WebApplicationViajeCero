using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplicationViajeCero.Context;
using WebApplicationViajeCero.Filters;
using WebApplicationViajeCero.Models;
using WebApplicationViajeCero.Utils;
using static WebApplicationViajeCero.Models.Pagination;

namespace WebApiViejaCero.Controllers

{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ReportsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("excel")]
        public async Task<IActionResult> GenerateExcel([FromQuery] FilterReport filterReport)
        {
            IQueryable <Request> data = _context.Requests
                .Include(r => r.User)
                .Include(r => r.Service)
                .Include(r => r.ExtraOption)
                .Include(r => r.Province);
            
            if(filterReport.from.HasValue || filterReport.to.HasValue)
            {
                data = Report.FilterByDateRange(data, filterReport.from, filterReport.to);
            }

            if (filterReport.Gender != null && filterReport.Gender.Length > 0)
            {
                data = data.Where(r => filterReport.Gender.Contains(r.Sex));
            }

            var dataList = data.ToList();

            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Solicitudes");

            // Encabezados
            worksheet.Cell(1, 1).Value = "ID Solicitud";
            worksheet.Cell(1, 2).Value = "Servicio";
            worksheet.Cell(1, 3).Value = "Opciones Adicionales";
            worksheet.Cell(1, 4).Value = "No Disponible (GOB)";
            worksheet.Cell(1, 5).Value = "Total Solicitado";
            worksheet.Cell(1, 6).Value = "Género";
            worksheet.Cell(1, 7).Value = "Provincia";
            worksheet.Cell(1, 8).Value = "Región";
            worksheet.Cell(1, 9).Value = "Fecha";

            worksheet.Row(1).Style.Font.SetBold(true);

            for (int i = 0; i < dataList.Count; i++)
            {
                worksheet.Cell(i + 2, 1).Value = dataList[i].Id;
                worksheet.Cell(i + 2, 2).Value = dataList[i]?.Service?.Name ?? "N/A";
                worksheet.Cell(i + 2, 3).Value = dataList[i]?.ExtraOption?.Name ?? "N/A";
                worksheet.Cell(i + 2, 4).Value = dataList[i]?.Unavailable ?? "N/A";
                worksheet.Cell(i + 2, 5).Value = dataList.Count(r => r.Service?.Name == dataList[i].Service?.Name);
                worksheet.Cell(i + 2, 6).Value = dataList[i].Sex.ToString();
                worksheet.Cell(i + 2, 7).Value = dataList[i].Province?.Name;
                worksheet.Cell(i + 2, 8).Value = dataList[i].Province.Zone.ToString();
                worksheet.Cell(i + 2, 9).Value = dataList[i].DateCreated.ToString();
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
