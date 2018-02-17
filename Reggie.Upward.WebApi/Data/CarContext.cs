using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace Reggie.Upward.WebApi.Data
{
    public class CarContext : DbContext
    {
        public CarContext(DbContextOptions<CarContext> options) : base(options)
        {
        }
    }
}