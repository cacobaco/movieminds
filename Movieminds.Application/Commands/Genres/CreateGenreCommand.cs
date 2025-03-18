using Movieminds.Application.Requests;

namespace Movieminds.Application.Commands.Genres;

public sealed record CreateGenreCommand(
    string Name
) : ICommand<IResponse<CreateGenreResponse>>;
