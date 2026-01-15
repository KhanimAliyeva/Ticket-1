using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Simulation_1Mpa201.Models;

namespace Simulation_1Mpa201.Configurations
{
    public class BranchConfiguration : IEntityTypeConfiguration<Branch>
    {
        public void Configure(EntityTypeBuilder<Branch> builder)
        {
            builder.Property(p => p.Name).IsRequired().HasMaxLength(256);

        }

    }
}
