namespace Services.Users.Authenticate.Exceptions;

[Serializable]
public class AccountIsNotActivatedException : IncorrectUsernameException
{
    public AccountIsNotActivatedException(string username) : base(username)
    {
    }
}