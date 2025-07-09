using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using WebApplicationViajeCero.Models;
using WebApplicationViajeCero.Context;
using System.Text.Json;

namespace WebApplicationViajeCero.Seeders.Seeds
{
    public class ProvinceSeeder
    {
        public static void Seed(AppDbContext context)
        {
            string filePath = Path.GetFullPath(
                Path.Combine(Directory.GetCurrentDirectory(), "Seeders", "Data", "provinces.json")
            );

            if (!File.Exists(filePath))
                throw new FileNotFoundException("No se encontró el archivo JSON", filePath);

            string json = File.ReadAllText(filePath, System.Text.Encoding.UTF8);

            var provincesFromJson = JsonSerializer.Deserialize<List<Province>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            });

            if (provincesFromJson == null || !provincesFromJson.Any())
                return;

            var existingProvinceNames = context.Provinces
                            .Select(p => p.Name)
                            .ToHashSet();

            var newProvinces = provincesFromJson
                .Where(p => !existingProvinceNames.Contains(p.Name))
                .Select(p => new Province { Name = p.Name, Zone = p.Zone })
                .ToList();

            var newProvinceEntities = newProvinces
                .Select(p => new Province { Name = p.Name, Zone = p.Zone })
                .ToList();

            context.Provinces.AddRange(newProvinceEntities);
            context.SaveChanges();
        }

    }


}
