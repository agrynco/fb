namespace Services.Users;

using Services.Exceptions;

public class ThereIsNoUserWithSuchEmailException(string email, string ipAddress)
    : ServiceException($"There is no user with email {email} and IP address {ipAddress}")
{
    public string Email { get; } = email;
    public string IpAddress { get; } = ipAddress;
}