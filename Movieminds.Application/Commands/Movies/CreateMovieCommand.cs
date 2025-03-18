using Movieminds.Domain.Entities;

namespace Movieminds.Application.Commands.Posts;

public sealed record CreateMovieCommand(
    int Id,
    string Title,
    string Description,
    string PosterImageUrl,
    double Rating,
    IEnumerable<Genre>? Genres,
    IEnumerable<MovieContributor>? Contributors,
    DateOnly? ReleaseDate
) : ICommand;
