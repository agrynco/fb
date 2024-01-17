namespace Services.Users.Authenticate.Exceptions;

[Serializable]
public class IncorrectPasswordException : IncorrectUsernameException
{
    public IncorrectPasswordException(string username) : base(username)
    {
    }
}