namespace Movieminds.Presentation.Responses.Movies;

public sealed record MovieResponse(
    int Id,
    string Title,
    string Description,
    string PosterImageUrl,
    double Rating,
    // IEnumerable<GetGenreResponse> Genres,
    // IEnumerable<GetMovieContributorResponse> Contributors,
    DateOnly? ReleaseDate
);
