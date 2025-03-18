using Movieminds.Domain.Entities;

namespace Movieminds.Application.Queries.Messages;

public sealed record GetConversationMessagesResponse(IEnumerable<Message> Messages);
