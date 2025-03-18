using Movieminds.Application.Requests;
using Movieminds.Domain.Entities;
using Movieminds.Domain.Repositories;

namespace Movieminds.Application.Commands.Messages;

public class MarkMessagesAsSeenCommandHandler : ICommandHandler<MarkMessagesAsSeenCommand>
{
	private readonly IUnitOfWorkAsync _unitOfWork;
	private readonly IRepositoryAsync<Message> _messageRepository;

	public MarkMessagesAsSeenCommandHandler(IUnitOfWorkAsync unitOfWork)
	{
		_unitOfWork = unitOfWork;
		_messageRepository = unitOfWork.GetRepositoryAsync<Message>();
	}

	public async Task<IResponse> HandleAsync(MarkMessagesAsSeenCommand command)
	{
		try
		{
			var messagesToMarkAsSeen = _messageRepository
				.GetAll()
				.Where(m => m.Sender.Id == command.SenderId && m.Receiver.Id == command.ReceiverId)
				.ToList();

			messagesToMarkAsSeen.ForEach(m => m.IsRead = true);

			_messageRepository.BulkUpdate(messagesToMarkAsSeen);

			await _unitOfWork.SaveChangesAsync();
			return Response.Ok();
		}
		catch (Exception ex)
		{
			return Response.Fail(ex.Message);
		}
	}
}
