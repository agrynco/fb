namespace Services.Users.GetProfile;

public record UsersGetProfileResponse
{
    public UserProfileGeneralDto General { get; init; } = default!;
}