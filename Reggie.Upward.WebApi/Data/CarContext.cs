using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Reggie.Upward.WebApi.Models;

namespace Reggie.Upward.WebApi.Data
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