namespace Movieminds.Application.Queries.Movies;

public sealed record GetMovieResponse(
    int Id,
    string Title,
    string Description,
    string PosterImageUrl,
    double Rating,
    // IEnumerable<GetGenreResponse> Genres,
    // IEnumerable<GetMovieContributorResponse> Contributors,
    DateOnly? ReleaseDate
);
