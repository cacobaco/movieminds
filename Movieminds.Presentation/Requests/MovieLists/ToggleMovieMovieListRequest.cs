using System.ComponentModel.DataAnnotations;

namespace Movieminds.Presentation.Requests.MovieLists;

public class ToggleMovieMovieListRequest
{
    [Required]
    public int MovieId { get; set; }
}
