using Movieminds.Application.Requests;

namespace Movieminds.Application.Queries.Messages;

public sealed record GetConversationMessagesQuery(
    int User1Id,
    int User2Id
) : IQuery<IResponse<GetConversationMessagesResponse>>;
