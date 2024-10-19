
using Mc2.CrudTest.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Mc2.CrudTest.Application.Common.Interfaces;

namespace Mc2.CrudTest.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Customer>()
                .HasIndex(c => new { c.FirstName, c.LastName, c.DateOfBirth })
                .IsUnique();

            modelBuilder.Entity<Customer>()
                .HasIndex(c => c.Email)
                .IsUnique();


            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }
    }
}
