using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace Reggie.Blog.Data
{
    public class BlogContext : DbContext
    {
        public BlogContext(DbContextOptions<BlogContext> options) : base(options)
        {
        }
    }
}