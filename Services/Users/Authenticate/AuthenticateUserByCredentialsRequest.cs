using SlimMessageBus;

namespace Services.Users.Authenticate;

public class AuthenticateUserByCredentialsRequest : IRequest<AuthenticateUserResponse>
{
    public required string IpAddress { get; init; } = default!;
    public required string JwtKey { get; init; } = default!;
    public required string Password { get; init; } = default!;
    public int RefreshTokenTtl { get; init; }
    public int TokenLiveDuration { get; init; }
    public required string Username { get; init; } = default!;
}