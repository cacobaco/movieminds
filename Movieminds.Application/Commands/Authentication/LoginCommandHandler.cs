using Movieminds.Application.Contracts;
using Movieminds.Application.Requests;
using Movieminds.Domain.Entities;
using Movieminds.Domain.Repositories;

namespace Movieminds.Application.Commands.Authentication;

public class LoginCommandHandler : ICommandHandler<LoginCommand, IResponse<LoginResponse>>
{
    private readonly IRepositoryAsync<User> _userRepository;
    private readonly IJwtProvider _jwtProvider;

    public LoginCommandHandler(IUnitOfWorkAsync unitOfWork, IJwtProvider jwtProvider)
    {
        _userRepository = unitOfWork.GetRepositoryAsync<User>();
        _jwtProvider = jwtProvider;
    }

    public async Task<IResponse<LoginResponse>> HandleAsync(LoginCommand request)
    {
        var user = await _userRepository.GetFirstOrDefaultAsync(predicate: u =>
            u.Email.ToLower() == request.Identifier.ToLower() ||
            u.Profile.Name.ToLower() == request.Identifier.ToLower()
        );
        if (user == null)
        {
            return Response.Fail<LoginResponse>("Invalid credentials");
        }

        // TODO: Implement password hashing
        if (user.Password != request.Password)
        {
            return Response.Fail<LoginResponse>("Invalid credentials");
        }

        _userRepository.Ensure(user, user => user.Profile);

        var token = _jwtProvider.Generate(user);
        return Response.Ok(new LoginResponse(token, user.Id));
    }
}
