using System.Text.Json;
using WebApplicationViajeCero.Context;
using WebApplicationViajeCero.Models;

namespace WebApplicationViajeCero.Seeders.Seeds
{
    public class InstitutionSeeder
    {
        public static void Seed(AppDbContext context)
        {
            string filePath = Path.GetFullPath(
                Path.Combine(Directory.GetCurrentDirectory(), "Seeders", "Data", "Institutions.json")
            );

            if (!File.Exists(filePath))
                throw new FileNotFoundException("No se encontró el archivo JSON", filePath);

            string json = File.ReadAllText(filePath, System.Text.Encoding.UTF8);

            var institutionsFromJson = JsonSerializer.Deserialize<List<Institution>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            });

            if (institutionsFromJson == null || !institutionsFromJson.Any())
                return;

            var existingInstitutionNames = context.Institutions
            .Select(p => p.Name)
            .ToHashSet();

            var newInstitutions = institutionsFromJson
                .Where(p => !existingInstitutionNames.Contains(p.Name))
                .Select(p => new Institution
                {
                    Name = p.Name,
                    Acronym = p.Acronym
                })
                .ToList();

            context.Institutions.AddRange(newInstitutions);
            context.SaveChanges();
        }

    }
}
