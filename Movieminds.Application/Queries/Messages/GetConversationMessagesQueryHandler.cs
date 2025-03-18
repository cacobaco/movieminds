using Movieminds.Application.Requests;
using Movieminds.Domain.Entities;
using Movieminds.Domain.Repositories;

namespace Movieminds.Application.Queries.Messages;

public class GetConversationMessagesQueryHandler : IQueryHandler<GetConversationMessagesQuery, IResponse<GetConversationMessagesResponse>>
{
	private readonly IRepositoryAsync<Profile> _profileRepository;
	private readonly IRepositoryAsync<Message> _messageRepository;

	public GetConversationMessagesQueryHandler(IUnitOfWorkAsync unitOfWork)
	{
		_profileRepository = unitOfWork.GetRepositoryAsync<Profile>();
		_messageRepository = unitOfWork.GetRepositoryAsync<Message>();
	}

	public async Task<IResponse<GetConversationMessagesResponse>> HandleAsync(GetConversationMessagesQuery query)
	{
		try
		{
			var user1 = await _profileRepository.GetByIdAsync(query.User1Id);
			if (user1 == null)
			{
				return Response.Fail<GetConversationMessagesResponse>("User 1 not found");
			}

			var user2 = await _profileRepository.GetByIdAsync(query.User2Id);
			if (user2 == null)
			{
				return Response.Fail<GetConversationMessagesResponse>("User 2 not found");
			}

			var messages = _messageRepository
				.GetAll()
				.Where(m => m.Sender == user1 && m.Receiver == user2 || m.Sender == user2 && m.Receiver == user1)
				.OrderBy(m => m.CreatedAt)
				.ToList();

			return Response.Ok(new GetConversationMessagesResponse(messages));
		}
		catch (Exception ex)
		{
			return Response.Fail<GetConversationMessagesResponse>(ex.Message);
		}
	}
}
