using Movieminds.Application.Requests;
using Movieminds.Domain.Entities;
using Movieminds.Domain.Repositories;

namespace Movieminds.Application.Queries.Messages;

public class GetUnreadMessagesCountQueryHandler : IQueryHandler<GetUnreadMessagesCountQuery, IResponse<GetUnreadMessagesCountResponse>>
{
    private readonly IRepositoryAsync<Profile> _profileRepository;
    private readonly IRepositoryAsync<Message> _messageRepository;

    public GetUnreadMessagesCountQueryHandler(IUnitOfWorkAsync unitOfWork)
    {
        _profileRepository = unitOfWork.GetRepositoryAsync<Profile>();
        _messageRepository = unitOfWork.GetRepositoryAsync<Message>();
    }

    public async Task<IResponse<GetUnreadMessagesCountResponse>> HandleAsync(GetUnreadMessagesCountQuery query)
    {
        try
        {
            var receiver = await _profileRepository.GetByIdAsync(query.ReceiverId);
            if (receiver == null)
            {
                return Response.Fail<GetUnreadMessagesCountResponse>("Receiver not found");
            }

            var messages = _messageRepository
                .GetAll()
                .Where(m => !m.IsRead && m.Receiver == receiver)
                .ToList();

            return Response.Ok(new GetUnreadMessagesCountResponse(messages.Count));
        }
        catch (Exception ex)
        {
            return Response.Fail<GetUnreadMessagesCountResponse>(ex.Message);
        }
    }
}

