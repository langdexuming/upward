using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace Reggie.Upward.WebApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
    }
}