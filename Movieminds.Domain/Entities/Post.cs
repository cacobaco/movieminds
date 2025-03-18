namespace Movieminds.Domain.Entities;

public class Post : BaseEntity
{
	public Profile Author { get; set; }
	public Movie Movie { get; set; }
	public string Content { get; set; }
	public List<Profile> LikedBy { get; set; } = [];
}
