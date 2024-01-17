namespace Services.Users.GetAvatar;

using DAL.Abstract.Users;
using Domain;
using Serilog;
using Services.Core;

public class UsersGetAvatarRequestHandler(
    ILogger logger,
    IUsersRepository usersRepository)
    : RequestHandler<UsersGetAvatarRequest, UsersGetAvatarResponse>(logger)
{
    protected override async Task<UsersGetAvatarResponse> DoOnHandle(UsersGetAvatarRequest request)
    {
        File? file = (await usersRepository.GetAvatar(request.UserId));
        
        if (file == null)
        {
            return null;
        }
        
        return new UsersGetAvatarResponse
        {
            Data = file.Data,
            ContentType = file.ContentType,
            Name = file.Name
        };
    }
}