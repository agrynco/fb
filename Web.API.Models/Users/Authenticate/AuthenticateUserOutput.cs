namespace Web.API.Models.Users.Authenticate;

public class AuthenticateUserOutput
{
    public required string Email { get; init; } = default!;
    public required string FirstName { get; init; } = default!;
    public int Id { get; init; }
    public required string JwtToken { get; init; } = default!;
    public required string LastName { get; init; } = default!;
    public required string RefreshToken { get; init; } = default!;
}