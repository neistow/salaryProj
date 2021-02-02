using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Salary.Core.Entities;
using Salary.Core.Entities.Identity;

namespace Salary.Data
{
    public class SalaryDbContext : IdentityDbContext<User, Role, int>
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeePosition> EmployeePositions { get; set; }

        public SalaryDbContext(DbContextOptions<SalaryDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(SalaryDbContext).Assembly);
            base.OnModelCreating(builder);
        }
    }
}