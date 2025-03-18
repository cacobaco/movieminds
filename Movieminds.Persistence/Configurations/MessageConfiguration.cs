using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movieminds.Domain.Entities;

namespace Movieminds.Persistence.Configurations;

internal class MessageConfiguration : BaseEntityConfiguration<Message>
{
	public override void Configure(EntityTypeBuilder<Message> builder)
	{
		base.Configure(builder);

		builder.Property(m => m.Content)
			.IsRequired()
			.HasMaxLength(500);

		builder.Property(m => m.IsRead)
			.IsRequired();
	}
}
