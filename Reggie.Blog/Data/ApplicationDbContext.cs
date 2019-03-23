using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Reggie.Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reggie.Blog.Data
{
    public class ApplicationDbContext: IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>().Property(x => x.TwoFactorEnabled).HasColumnType("bit(1)");
            builder.Entity<ApplicationUser>().Property(x => x.PhoneNumberConfirmed).HasColumnType("bit(1)");
            builder.Entity<ApplicationUser>().Property(x => x.EmailConfirmed).HasColumnType("bit(1)");
            builder.Entity<ApplicationUser>().Property(x => x.LockoutEnabled).HasColumnType("bit(1)");
        }
    }
}
