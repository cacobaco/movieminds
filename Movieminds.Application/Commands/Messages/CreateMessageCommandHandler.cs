using Movieminds.Application.Requests;
using Movieminds.Domain.Entities;
using Movieminds.Domain.Repositories;

namespace Movieminds.Application.Commands.Messages;

public class CreateMessageCommandHandler : ICommandHandler<CreateMessageCommand, IResponse<CreateMessageResponse>>
{
    private readonly IUnitOfWorkAsync _unitOfWork;
    private readonly IRepositoryAsync<Profile> _profileRepository;
    private readonly IRepositoryAsync<Message> _messageRepository;

    public CreateMessageCommandHandler(IUnitOfWorkAsync unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _profileRepository = unitOfWork.GetRepositoryAsync<Profile>();
        _messageRepository = unitOfWork.GetRepositoryAsync<Message>();
    }

    public async Task<IResponse<CreateMessageResponse>> HandleAsync(CreateMessageCommand request)
    {
        var sender = await _profileRepository.GetFirstOrDefaultAsync(predicate: p => p.Id == request.SenderId);
        if (sender == null)
        {
            return Response.Fail<CreateMessageResponse>("Sender not found");
        }

        var receiver = await _profileRepository.GetFirstOrDefaultAsync(predicate: p => p.Id == request.ReceiverId);
        if (receiver == null)
        {
            return Response.Fail<CreateMessageResponse>("Receiver not found");
        }

        var message = new Message
        {
            Content = request.Content,
            Sender = sender,
            Receiver = receiver,
            IsRead = request.IsRead
        };

        await _messageRepository.InsertAsync(message);
        await _unitOfWork.SaveChangesAsync();
        return Response.Ok(new CreateMessageResponse(message));
    }
}
