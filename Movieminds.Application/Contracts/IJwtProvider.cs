using Movieminds.Domain.Entities;

namespace Movieminds.Application.Contracts;

public interface IJwtProvider
{
	string Generate(User user);
}
