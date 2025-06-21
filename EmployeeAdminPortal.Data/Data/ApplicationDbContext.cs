using EmployeeAdminPortal.Data.Models.Entities;
using EmployeeAdminPortal.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAdminPortal.Data.Data
{
    public class ApplicationDbContext : DbContext
    {
        //ctro type panna constructor create ahagum
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        //prop for creATE A PROPERTY
        public DbSet<Employee> Employees{ get; set; }
        public DbSet<Manager> Managers { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Manager)
                .WithOne(m => m.Employee)
                .HasForeignKey<Employee>(e => e.ManagerId)
                .OnDelete(DeleteBehavior.Cascade); // Optional: decide what happens on delete
        }

    }
}
