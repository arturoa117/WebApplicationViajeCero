using WebApplicationViajeCero.Context;
using WebApplicationViajeCero.Models;

namespace WebApplicationViajeCero.Seeders.Seeds
{
    public static class RoleSeeder
    {
        public static void Seed(AppDbContext context)
        {
            var predefinedRoles = new List<string>
            {
                "Administrador",
                "Representante",
            };

            var existingRoleNames = context.Roles
                .Select(r => r.Description)
                .ToHashSet();

            var newRoles = predefinedRoles
                .Where(roleDescription => !existingRoleNames.Contains(roleDescription))
                .Select(roleDescription => new Role { Description = roleDescription })
                .ToList();

            if (newRoles.Any())
            {
                context.Roles.AddRange(newRoles);
                context.SaveChanges();
            }
        }
    }
}
