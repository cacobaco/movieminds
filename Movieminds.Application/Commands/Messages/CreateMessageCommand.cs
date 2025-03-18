using Movieminds.Application.Requests;

namespace Movieminds.Application.Commands.Messages;

public sealed record CreateMessageCommand(
    string Content,
    int SenderId,
    int ReceiverId,
    bool IsRead
) : ICommand<IResponse<CreateMessageResponse>>;
