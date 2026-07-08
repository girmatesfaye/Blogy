using Microsoft.EntityFrameworkCore;
using Blogy_MVC.Models.Domain;  
namespace Blogy_MVC.Data
{
    public class BlogyDbContext : DbContext
    {
     public BlogyDbContext(DbContextOptions<BlogyDbContext> options) : base(options)
        {
        }

        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Tag> Tags { get; set; }
    }
}
