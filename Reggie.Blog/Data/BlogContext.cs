using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Reggie.Blog.Models;

namespace Reggie.Blog.Data
{
    public class BlogContext : DbContext
    {
        public BlogContext(DbContextOptions<BlogContext> options) : base(options)
        {
        }

        public DbSet<InformalEssay> InformalEssays { get; set; }
        public DbSet<LeaveMessage> LeaveMessages { get; set; }
        public DbSet<Skill> Skills { get; set; }
    }
}