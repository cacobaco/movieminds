using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movieminds.Domain.Entities;

namespace Movieminds.Persistence.Configurations;

internal class SeenListConfiguration : BaseEntityConfiguration<SeenList>
{
	public override void Configure(EntityTypeBuilder<SeenList> builder)
	{
		base.Configure(builder);

		builder.HasMany(s => s.Movies)
			.WithMany();
	}
}
