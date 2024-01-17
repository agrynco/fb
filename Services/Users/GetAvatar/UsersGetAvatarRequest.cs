namespace Services.Users.GetAvatar;

using SlimMessageBus;

public class UsersGetAvatarRequest : IRequest<UsersGetAvatarResponse>
{
    public int UserId { get; init; }
}