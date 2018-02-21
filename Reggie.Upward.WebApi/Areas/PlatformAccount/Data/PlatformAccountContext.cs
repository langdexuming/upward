using Microsoft.EntityFrameworkCore;
using Reggie.Upward.WebApi.Areas.PlatformAccount.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reggie.Upward.WebApi.Areas.PlatformAccount.Data
{
    public class PlatformAccountContext:DbContext
    {
        public PlatformAccountContext(DbContextOptions<PlatformAccountContext> options) : base(options)
        {
        }

        public DbSet<Models.PlatformAccount> PlatformAccounts { get; set; }
    }
}
