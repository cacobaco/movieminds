namespace Movieminds.Domain.Entities;

public class Profile : BaseEntity
{
    public User Owner { get; set; }
    public string Name { get; set; }
    public string AvatarImageUrl { get; set; } = "img/default-avatar.png";
    public string BannerImageUrl { get; set; } = "img/default-banner.png";
    public bool IsPrivate { get; set; }
    public List<Message> SentMessages { get; set; } = [];
    public List<Message> ReceivedMessages { get; set; } = [];
    public List<Post> Posts { get; set; } = [];
    public List<Post> LikedPosts { get; set; } = [];
    public WishList WishList { get; set; }
    public SeenList SeenList { get; set; }
    public List<Profile> Followings { get; set; } = [];
    public List<Profile> Followers { get; set; } = [];
    public List<Profile> SentFollowRequests { get; set; } = [];
    public List<Profile> ReceivedFollowRequests { get; set; } = [];
}
