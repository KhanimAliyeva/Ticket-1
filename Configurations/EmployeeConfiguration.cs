using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Simulation_1Mpa201.Models;

namespace Simulation_1Mpa201.Configurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(p => p.Name).IsRequired().HasMaxLength(256);
            builder.Property(p => p.Description).IsRequired().HasMaxLength(512);
            builder.Property(p => p.ImageUrl).IsRequired().HasMaxLength(512);
            builder.HasOne(x => x.Branch).WithMany(x => x.Employees).HasForeignKey(x => x.BranchId).HasPrincipalKey(x => x.Id);
        }
    }
}
