using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movieminds.Domain.Entities;

namespace Movieminds.Persistence.Configurations;

internal class GenreConfiguration : BaseEntityConfiguration<Genre>
{
	public override void Configure(EntityTypeBuilder<Genre> builder)
	{
		base.Configure(builder);

		builder.Property(g => g.Name)
			.IsRequired()
			.HasMaxLength(50);
	}
}
