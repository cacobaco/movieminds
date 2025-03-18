using Movieminds.Application.Requests;

namespace Movieminds.Application.Queries.Users;

public sealed record GetUsersByNameQuery(
	string Query
) : IQuery<IResponse<GetUsersByNameResponse>>;
