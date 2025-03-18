namespace Movieminds.Domain.Entities;

public class SeenList : BaseEntity
{
	public Profile Owner { get; set; }
	public List<Movie> Movies { get; set; } = [];
}
