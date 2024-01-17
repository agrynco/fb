namespace Services.Users.ChangePassword;

public record UsersChangePasswordMessage
{
    public required string Password { get; init; } = default!;
    public required string Token { get; init; } = default!;
}