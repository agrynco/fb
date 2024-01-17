namespace DAL.Abstract.Users;

using Domain;

public interface IUsersRepository
{
    Task<User?> GetByUsername(string username);
    Task RemoveOldRefreshTokens(int userId, int refreshTokenTtl);
    Task AddRefreshTokenToUser(int userId, RefreshToken token);
    Task<bool> IsUsernameBusy(string username);
    Task<bool> IsEmailBusy(string email);
    Task<int> Add(User user);
    Task<User?> GetById(int id);
    Task<User?> GetByEmailConfirmationToken(string emailConfirmationToken);
    Task Update(User user);
    Task<User?> GetByEmail(string email);
    Task<File?> GetAvatar(int userId);
}