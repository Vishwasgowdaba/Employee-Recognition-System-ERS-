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

            // ✅ Employee unique email
            modelBuilder.Entity<Employee>()
                .HasIndex(e => e.Email)
                .IsUnique();

            // ✅ Appreciation Relationships
            modelBuilder.Entity<Appreciation>()
                .HasOne(a => a.Sender)
                .WithMany()
                .HasForeignKey(a => a.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Appreciation>()
                .HasOne(a => a.Receiver)
                .WithMany()
                .HasForeignKey(a => a.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);

            // ✅ Nomination Relationships
            modelBuilder.Entity<Nomination>()
                .HasOne(n => n.Employee)
                .WithMany()
                .HasForeignKey(n => n.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Nomination>()
                .HasOne(n => n.NominatedBy)
                .WithMany()
                .HasForeignKey(n => n.NominatedById)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Nomination>()
                .HasOne(n => n.AwardCategory)
                .WithMany()
                .HasForeignKey(n => n.AwardCategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            // ✅ Default values
            modelBuilder.Entity<Appreciation>()
                .Property(a => a.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            modelBuilder.Entity<Nomination>()
                .Property(n => n.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");
        }
    }
}