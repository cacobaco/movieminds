namespace Movieminds.Domain.Entities;

public class MovieContributor : BaseEntity
{
	public string Name { get; set; }
	public string Role { get; set; }
	public string ImageUrl { get; set; }
}
