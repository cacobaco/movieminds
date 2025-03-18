using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movieminds.Domain.Entities;

namespace Movieminds.Persistence.Configurations;

internal abstract class BaseEntityConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseEntity
{
	public virtual void Configure(EntityTypeBuilder<T> builder)
	{
		builder.HasKey(e => e.Id);

		builder.Property(e => e.CreatedAt)
			.IsRequired()
			.HasColumnType("datetime")
			.ValueGeneratedOnAdd();

		builder.Property(e => e.UpdatedAt)
			.HasColumnType("datetime")
			.ValueGeneratedOnUpdate();

		builder.Property(e => e.DeletedAt)
			.HasColumnType("datetime");

		builder.Ignore(e => e.IsDeleted);
	}
}
