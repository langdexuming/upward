using Microsoft.EntityFrameworkCore;
using Reggie.Upward.WebApi.Areas.PlatformAccount.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Reggie.Upward.WebApi.Areas.ResourceManage.Models;

namespace Reggie.Upward.WebApi.Areas.PlatformAccount.Data
{
    public class PlatformAccountContext:DbContext
    {
        public PlatformAccountContext(DbContextOptions<PlatformAccountContext> options) : base(options)
        {
        }

        public DbSet<Models.PlatformAccount> PlatformAccounts { get; set; }

        public DbSet<Models.Platform> Platforms { get; set; }

        public DbSet<Reggie.Upward.WebApi.Areas.ResourceManage.Models.FileModel> FileModel { get; set; }
    }
}
