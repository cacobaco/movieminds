using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movieminds.Domain.Entities;

namespace Movieminds.Persistence.Configurations;

internal class UserConfiguration : BaseEntityConfiguration<User>
{
	public override void Configure(EntityTypeBuilder<User> builder)
	{
		base.Configure(builder);

		builder.Property(u => u.Name)
			.IsRequired()
			.HasMaxLength(100);

		builder.Property(u => u.Email)
			.IsRequired()
			.HasMaxLength(320);

		builder.Property(u => u.Password)
			.IsRequired()
			.HasMaxLength(100);

		builder.Property(u => u.Role)
			.IsRequired()
			.HasColumnType("int")
			.HasConversion<int>();

		builder.Property(u => u.Gender)
			.IsRequired()
			.HasColumnType("int")
			.HasConversion<int>();

		builder.Property(u => u.BirthDate)
			.IsRequired()
			.HasColumnType("date");

		builder.HasOne(u => u.Profile)
			.WithOne(p => p.Owner)
			.HasForeignKey<Profile>()
			.IsRequired();

		builder.HasIndex(u => u.Email).IsUnique();
	}
}
