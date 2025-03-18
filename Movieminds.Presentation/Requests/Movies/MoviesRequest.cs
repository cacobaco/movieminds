namespace Movieminds.Presentation.Requests.Movies;

public class MoviesRequest
{
    public string Search { get; set; } = string.Empty;
    public string SortBy { get; set; } = "popularity.desc";
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}
