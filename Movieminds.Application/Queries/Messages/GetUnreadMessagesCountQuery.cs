using Movieminds.Application.Requests;

namespace Movieminds.Application.Queries.Messages;

public sealed record GetUnreadMessagesCountQuery(
    int ReceiverId
) : IQuery<IResponse<GetUnreadMessagesCountResponse>>;
