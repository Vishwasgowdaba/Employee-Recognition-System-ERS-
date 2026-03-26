using Employee_Recognition_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Employee_Recognition_System.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Appreciation> Appreciations { get; set; }
        public DbSet<Nomination> Nominations { get; set; }
        public DbSet<AwardCategory> AwardCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 🔥 Disable cascade delete globally
            foreach (var relationship in modelBuilder.Model
                .GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.NoAction;
            }
        }


    }
}