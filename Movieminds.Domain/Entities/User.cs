using Movieminds.Domain.Enums;

namespace Movieminds.Domain.Entities;

public class User : BaseEntity
{
	public string Name { get; set; }
	public string Email { get; set; }
	public string Password { get; set; }
	public Role Role { get; set; } = Role.User;
	public Gender Gender { get; set; }
	public DateOnly BirthDate { get; set; }
	public Profile Profile { get; set; }
}
