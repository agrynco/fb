namespace Services.Users.GetProfile;

using DAL.Abstract.Users;
using Serilog;
using Services.Core;

public class UsersGetProfileRequestHandler(
    ILogger logger,
    IUsersRepository usersRepository)
    : RequestHandler<UsersGetProfileRequest, UsersGetProfileResponse>(logger)
{
    protected override async  Task<UsersGetProfileResponse> DoOnHandle(UsersGetProfileRequest request)
    {
        var user = await usersRepository.GetById(request.UserId);

        return new UsersGetProfileResponse
        {
            General = new UserProfileGeneralDto
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username
            }
        };
    }
}