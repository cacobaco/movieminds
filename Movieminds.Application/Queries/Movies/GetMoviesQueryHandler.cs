using Movieminds.Application.Commands.Movies;
using Movieminds.Application.Requests;
using Movieminds.Domain.Repositories;

namespace Movieminds.Application.Queries.Movies;

public class GetMoviesQueryHandler : ChainedRequestHandler<GetMoviesQuery, IPaginatedResponse<GetMovieResponse>>
{
    private readonly IUnitOfWorkAsync _unitOfWork;

    public GetMoviesQueryHandler(IUnitOfWorkAsync unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public override async Task<IPaginatedResponse<GetMovieResponse>> HandleAsync(GetMoviesQuery request)
    {
        try
        {
            IPaginatedResponse<GetMovieResponse> nextResponse = await base.HandleAsync(request);
            if (!nextResponse.Success || nextResponse.Data is null)
            {
                return nextResponse;
            }

            var nextResponseMovie = nextResponse.Data;

            var saveCommand = new SaveExternalMoviesCommand(nextResponseMovie);
            var saveHandler = new SaveExternalMoviesCommandHandler(_unitOfWork);
            await saveHandler.HandleAsync(saveCommand);

            return nextResponse;
        }
        catch (NotImplementedException)
        {
            return PaginatedResponse<GetMovieResponse>.Fail("Movies not found");
        }
    }
}
