using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movieminds.Domain.Entities;

namespace Movieminds.Persistence.Configurations;

internal class WishListConfiguration : BaseEntityConfiguration<WishList>
{
	public override void Configure(EntityTypeBuilder<WishList> builder)
	{
		base.Configure(builder);

		builder.HasMany(s => s.Movies)
			.WithMany();
	}
}
