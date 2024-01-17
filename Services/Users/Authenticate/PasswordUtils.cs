using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Services.Users.Authenticate;

public static class PasswordUtils
{
    public static string CreatePasswordHash(string password, byte[] salt)
    {
        return Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password,
            salt,
            KeyDerivationPrf.HMACSHA1,
            10000,
            256 / 8));
    }

    public static byte[] CreatePasswordSalt()
    {
        byte[] result = new byte[16];

        using var randomNumberGenerator = RandomNumberGenerator.Create();
        randomNumberGenerator.GetBytes(result);

        return result;
    }

    public static bool VerifyPasswordHash(string password, string passwordHash, byte[] passwordSalt)
    {
        string createdPasswordHash = CreatePasswordHash(password, passwordSalt);

        return passwordHash == createdPasswordHash;
    }
}