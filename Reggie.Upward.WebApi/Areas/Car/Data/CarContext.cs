using Microsoft.EntityFrameworkCore;
using Reggie.Upward.WebApi.Areas.Car.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Reggie.Upward.WebApi.Areas.PlatformAccount.Models;

namespace Reggie.Upward.WebApi.Areas.Car.Data
{
    public class CarContext:DbContext
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
