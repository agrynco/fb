namespace Services.Users.ForgotPassword;

public record UsersForgotPasswordMessage
{
    public required string Email { get; init; } = string.Empty;
    public required string IpAddress { get; init; } = string.Empty;
}