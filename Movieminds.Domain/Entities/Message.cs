namespace Movieminds.Domain.Entities;

public class Message : BaseEntity
{
	public Profile Sender { get; set; }
	public Profile Receiver { get; set; }
	public string Content { get; set; }
	public bool IsRead { get; set; } = false;
}
