using Microsoft.EntityFrameworkCore;
using WebApplicationViajeCero.Models;

namespace WebApplicationViajeCero.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        /*
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var str = "server=localhost; port = 3306; database = viajecero; user= root; password = root;";
            optionsBuilder.UseMySQL(str, ServerVersion.AutoDetect(str));
        }
        */
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Province>()
                .Property(p => p.Zone)
                .HasConversion<string>();

            modelBuilder.Entity<User>()
                .HasOne(u => u.Province)
                .WithMany(p => p.Users)
                .HasForeignKey(u => u.ProvinceId)
                .OnDelete(DeleteBehavior.Restrict); 


            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Institution> Institutions { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
