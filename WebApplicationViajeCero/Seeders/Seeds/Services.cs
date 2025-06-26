using ClosedXML.Excel;
using DocumentFormat.OpenXml.InkML;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using WebApplicationViajeCero.Context;
using WebApplicationViajeCero.Models;


namespace WebApplicationViajeCero.Seeders.Seeds
{
    public class ServicesSeeder
    {
        public static void Seed(AppDbContext context, string excelFilePath)
        {
                if (context.Services.Any()) return; 

                var institutions = context.Institutions.ToList();
                var services = new List<Service>();

                using var workbook = new XLWorkbook(excelFilePath);
                var worksheet = workbook.Worksheet(1);
                var lastRow = worksheet.LastRowUsed().RowNumber();

                for (int row = 2; row <= lastRow; row++) 
                {
                    var serviceName = worksheet.Cell(row, 2).GetString(); 
                    var acronym = worksheet.Cell(row, 7).GetString();    

                    if (!string.IsNullOrWhiteSpace(serviceName) && !string.IsNullOrWhiteSpace(acronym))
                    {
                        var institution = institutions.FirstOrDefault(i => i.Acronym == acronym);

                        if (institution != null)
                        {
                            services.Add(new Service
                            {
                                Name = serviceName,
                                InstitutionId = institution.Id
                            });
                        }
                    }
                }

                if (services.Any())
                {
                    context.Services.AddRange(services);
                    context.SaveChanges();
                    Console.WriteLine($" Seeded {services.Count} services desde Excel.");
                }    
            }
    }
 }

