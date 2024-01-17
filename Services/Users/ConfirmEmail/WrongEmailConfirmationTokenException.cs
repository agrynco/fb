namespace Services.Users.ConfirmEmail;

using Services.Exceptions;

public class WrongEmailConfirmationTokenException(string emailConfirmationToken) : ServiceException(
    $"Wrong email confirmation token: {emailConfirmationToken}")
{
    public string EmailConfirmationToken { get; } = emailConfirmationToken;
}