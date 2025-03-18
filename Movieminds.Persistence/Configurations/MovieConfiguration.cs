using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movieminds.Domain.Entities;

namespace Movieminds.Persistence.Configurations;

internal class MovieConfiguration : BaseEntityConfiguration<Movie>
{
    public override void Configure(EntityTypeBuilder<Movie> builder)
    {
        base.Configure(builder);

        builder.Property(m => m.Title)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(m => m.Description)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(m => m.PosterImageUrl)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(m => m.Rating)
            .IsRequired();

        builder.HasMany(m => m.Genres)
            .WithMany();

        builder.HasMany(m => m.Contributors)
            .WithMany();

        builder.Property(m => m.ReleaseDate)
            .HasColumnType("date");
    }
}
