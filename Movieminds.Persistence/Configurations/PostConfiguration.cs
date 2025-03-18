using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movieminds.Domain.Entities;

namespace Movieminds.Persistence.Configurations;

internal class PostConfiguration : BaseEntityConfiguration<Post>
{
	public override void Configure(EntityTypeBuilder<Post> builder)
	{
		base.Configure(builder);

		builder.HasOne(p => p.Movie)
			.WithMany()
			.HasForeignKey("MovieId")
			.IsRequired();

		builder.Property(p => p.Content)
			.IsRequired()
			.HasMaxLength(1000);
	}
}
