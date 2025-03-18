using Movieminds.Application.Contracts;
using Movieminds.Application.Requests;
using Movieminds.Domain.Entities;
using Movieminds.Domain.Repositories;

namespace Movieminds.Application.Commands.Authentication;

public class RegisterCommandHandler : ICommandHandler<RegisterCommand, IResponse<RegisterResponse>>
{
    private readonly IUnitOfWorkAsync _unitOfWork;
    private readonly IRepositoryAsync<User> _userRepository;
    private readonly IRepositoryAsync<Profile> _profileRepository;
    private readonly IRepositoryAsync<WishList> _wishListRepository;
    private readonly IRepositoryAsync<SeenList> _seenListRepository;
    private readonly IJwtProvider _jwtProvider;

    public RegisterCommandHandler(IUnitOfWorkAsync unitOfWork, IJwtProvider jwtProvider)
    {
        _unitOfWork = unitOfWork;
        _userRepository = unitOfWork.GetRepositoryAsync<User>();
        _profileRepository = unitOfWork.GetRepositoryAsync<Profile>();
        _wishListRepository = unitOfWork.GetRepositoryAsync<WishList>();
        _seenListRepository = unitOfWork.GetRepositoryAsync<SeenList>();
        _jwtProvider = jwtProvider;
    }

    public async Task<IResponse<RegisterResponse>> HandleAsync(RegisterCommand request)
    {
        var existingUser = await _userRepository.GetFirstOrDefaultAsync(predicate: u =>
            u.Email.ToLower() == request.Email.ToLower() ||
            u.Profile.Name.ToLower() == request.Username.ToLower()
        );
        if (existingUser != null)
        {
            return Response.Fail<RegisterResponse>("User already exists");
        }

        // TODO: Implement password hashing
        var user = new User
        {
            Name = request.Name,
            Email = request.Email,
            Password = request.Password,
            Gender = request.Gender,
            BirthDate = request.BirthDate
        };

        var profile = new Profile
        {
            Owner = user,
            Name = request.Username
        };

        var wishList = new WishList
        {
            Owner = profile
        };

        var seenList = new SeenList
        {
            Owner = profile
        };

        profile.WishList = wishList;
        profile.SeenList = seenList;

        user.Profile = profile;

        await _userRepository.InsertAsync(user);
        await _profileRepository.InsertAsync(profile);
        await _wishListRepository.InsertAsync(wishList);
        await _seenListRepository.InsertAsync(seenList);

        await _unitOfWork.SaveChangesAsync();

        var token = _jwtProvider.Generate(user);
        return Response.Ok(new RegisterResponse(token, user.Id));
    }
}