using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using UpwardApi.Models;

namespace UpwardApi.Data
{
    public class CarContext : DbContext
    {
        public CarContext(DbContextOptions<CarContext> options) : base(options)
        {
        }

        public DbSet<Brand> Brands { get; set; }
        public DbSet<Series> Serieses { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<CarItem> CarItems { get; set; }
    }
}