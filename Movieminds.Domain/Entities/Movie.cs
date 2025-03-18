namespace Movieminds.Domain.Entities;

public class Movie : BaseEntity
{
	public string Title { get; set; }
	public string Description { get; set; }
	public string PosterImageUrl { get; set; } = "img/default-movie-poster.jpg";
	public double Rating { get; set; }
	public List<Genre> Genres { get; set; } = [];
	public List<MovieContributor> Contributors { get; set; } = [];
	public DateOnly? ReleaseDate { get; set; } = null;
}
