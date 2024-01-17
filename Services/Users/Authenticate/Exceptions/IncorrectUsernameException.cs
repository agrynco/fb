using Domain;
using Services.Exceptions;

namespace Services.Users.Authenticate.Exceptions;

[Serializable]
public class IncorrectUsernameException : ServiceException
{
    public IncorrectUsernameException(string username) : base(
        $"There is no user with {nameof(User.Username)} = '{username}'")
    {
        Username = username;
    }

    public string Username { get; }
}