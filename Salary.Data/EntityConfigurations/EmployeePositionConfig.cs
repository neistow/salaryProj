using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Salary.Core.Entities;

namespace Salary.Data.EntityConfigurations
{
    public class EmployeePositionConfig : IEntityTypeConfiguration<EmployeePosition>
    {
        public void Configure(EntityTypeBuilder<EmployeePosition> builder)
        {
            builder.HasKey(ep => ep.Id);
            builder.HasMany(ep => ep.Employees)
                .WithOne(e => e.Position)
                .IsRequired().OnDelete(DeleteBehavior.Restrict);


            builder.Property(ep => ep.Name).HasMaxLength(256);
        }
    }
}