using Asp_.net_core_web_app__MVC_.Models;
using Microsoft.EntityFrameworkCore;

namespace Asp_.net_core_web_app__MVC_.Data
{
    public class ApplicationDbContext: DbContext  // it is a root class of entity framework core.
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base (options)
        {
            
        }

        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Action", DisplayOrder = 1 },
                new Category { Id = 2, Name = "Romantic", DisplayOrder = 2 },
                new Category { Id = 3, Name = "Horror", DisplayOrder = 3 }
                );
        }
    }
}
