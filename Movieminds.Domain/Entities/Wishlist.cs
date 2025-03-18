namespace Movieminds.Domain.Entities;

public class WishList : BaseEntity
{
	public Profile Owner { get; set; }
	public List<Movie> Movies { get; set; } = [];
}
 