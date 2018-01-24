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

        public DbSet<EssayCategory> EssayCategories { get; set; }
        public DbSet<InformalEssay> InformalEssays { get; set; }
        public DbSet<LeaveMessage> LeaveMessages { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<JobExperience> JobExperiences { get; set; }
        public DbSet<Sample> Samples { get; set; }
        public DbSet<ContentFlag> ContentFlags { get; set; }
        public DbSet<SwitchFlag> SwitchFlags { get; set; }
    }
}