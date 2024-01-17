namespace Services.Users;

public interface IJwtUtils
{
    string GenerateJwtToken(int userId, string secret, int liveDuration);
    int? ValidateJwtToken(string token, string secret);
    GenerateRefreshTokenResult GenerateRefreshToken(string ipAddress);
}