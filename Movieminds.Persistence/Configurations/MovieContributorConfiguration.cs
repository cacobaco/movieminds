using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movieminds.Domain.Entities;

namespace Movieminds.Persistence.Configurations;

internal class MovieContributorConfiguration : BaseEntityConfiguration<MovieContributor>
{
    public override void Configure(EntityTypeBuilder<MovieContributor> builder)
    {
        base.Configure(builder);

        builder.Property(mc => mc.Name)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(mc => mc.Role)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(mc => mc.ImageUrl)
            .IsRequired()
            .HasMaxLength(255);
    }
}
