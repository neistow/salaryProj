using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Salary.Core.Entities;

namespace Salary.Data.EntityConfigurations
{
    public class EmployeeConfig : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(e => e.Id);
            builder.HasOne(e => e.Position)
                .WithMany(ep => ep.Employees)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);


            builder.Property(e => e.Salary).IsRequired();
            builder.Property(e => e.DateEmployed).IsRequired();
            builder.Property(e => e.FirstName).IsRequired().HasMaxLength(256);
            builder.Property(e => e.LastName).IsRequired().HasMaxLength(256);
        }
    }
}