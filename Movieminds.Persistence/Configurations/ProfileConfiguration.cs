using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movieminds.Domain.Entities;

namespace Movieminds.Persistence.Configurations;

internal class ProfileConfiguration : BaseEntityConfiguration<Profile>
{
    public override void Configure(EntityTypeBuilder<Profile> builder)
    {
        base.Configure(builder);

        builder.Property(u => u.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(p => p.AvatarImageUrl)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(p => p.BannerImageUrl)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(p => p.IsPrivate)
            .IsRequired();

        builder.HasMany(p => p.SentMessages)
            .WithOne(m => m.Sender)
            .HasForeignKey("SenderId")
            .IsRequired();

        builder.HasMany(p => p.ReceivedMessages)
            .WithOne(m => m.Receiver)
            .HasForeignKey("ReceiverId")
            .IsRequired();

        builder.HasMany(p => p.Posts)
            .WithOne(p => p.Author)
            .HasForeignKey("AuthorId")
            .IsRequired();

        builder.HasMany(p => p.LikedPosts)
            .WithMany(p => p.LikedBy);

        builder.HasOne(p => p.WishList)
            .WithOne(w => w.Owner)
            .HasForeignKey<WishList>()
            .IsRequired();

        builder.HasOne(p => p.SeenList)
            .WithOne(s => s.Owner)
            .HasForeignKey<SeenList>()
            .IsRequired();

        builder.HasMany(p => p.Followings)
            .WithMany(p => p.Followers);

        builder.HasMany(p => p.SentFollowRequests)
            .WithMany(p => p.ReceivedFollowRequests);

        builder.HasIndex(p => p.Name).IsUnique();
    }
}

