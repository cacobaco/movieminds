using Movieminds.Application.Requests;
using Movieminds.Domain.Entities;
using Movieminds.Domain.Repositories;

namespace Movieminds.Application.Commands.Messages;

public class RemoveMessageCommandHandler : ICommandHandler<RemoveMessageCommand>
{
	private readonly IUnitOfWorkAsync _unitOfWork;
	private readonly IRepositoryAsync<Message> _messageRepository;

	public RemoveMessageCommandHandler(IUnitOfWorkAsync unitOfWork)
	{
		_unitOfWork = unitOfWork;
		_messageRepository = unitOfWork.GetRepositoryAsync<Message>();
	}

	public async Task<IResponse> HandleAsync(RemoveMessageCommand request)
	{
		try
		{
			var message = await _messageRepository.GetByIdAsync(request.MessageId);
			if (message == null)
			{
				return Response.Fail("Message not found");
			}

			_messageRepository.SoftDelete(message);
			await _unitOfWork.SaveChangesAsync();
			return Response.Ok();
		}
		catch (Exception)
		{
			return Response.Fail("Failed to remove the message");
		}
	}
}
