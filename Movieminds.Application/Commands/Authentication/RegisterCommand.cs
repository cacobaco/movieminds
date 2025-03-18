using Movieminds.Application.Requests;
using Movieminds.Domain.Enums;

namespace Movieminds.Application.Commands.Authentication;

public sealed record RegisterCommand(
    string Name,
    Gender Gender,
    string Email,
    DateOnly BirthDate,
    string Username,
    string Password,
    string ConfirmPassword
) : ICommand<IResponse<RegisterResponse>>;
