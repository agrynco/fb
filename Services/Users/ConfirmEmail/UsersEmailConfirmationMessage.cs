namespace Services.Users.ConfirmEmail;

public record UsersEmailConfirmationMessage
{
    public string EmailConfirmationToken { get; init; } = string.Empty;
}