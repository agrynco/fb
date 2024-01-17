namespace Services.Users.GetProfile;

using SlimMessageBus;

public record UsersGetProfileRequest : IRequest<UsersGetProfileResponse>
{
    public required int UserId { get; init; } = default!;
}