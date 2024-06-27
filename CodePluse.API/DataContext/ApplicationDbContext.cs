using CodePluse.API.Model.Domain;
using Microsoft.EntityFrameworkCore;

namespace CodePluse.API.DataContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<BlogPost> Blogs { get; set; }
    }
}
