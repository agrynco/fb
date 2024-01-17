using AutoMapper;
using DAL.Abstract.Users;
using DAL.EF.Core;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.Repositories.Users;

using File = Domain.File;

public class UsersRepository(ApplicationDbContext dbContext, IMapper mapper)
    : BaseRepository<User>(dbContext), IUsersRepository
{
    public async Task<User?> GetByUsername(string username)
    {
        return await EfRepository.Get(e => e.Username == username).SingleOrDefaultAsync();
    }

    public async Task RemoveOldRefreshTokens(int userId, int refreshTokenTtl)
    {
        User user = await EfRepository.Get(u => u.Id == userId)
            .Include(u => u.RefreshTokens)
            .SingleAsync();

        user.RefreshTokens.RemoveAll(x =>
            !x.IsActive &&
            x.Created.AddDays(refreshTokenTtl) <= DateTime.UtcNow);

        await EfRepository.UpdateAsync(user);
    }

    public async Task AddRefreshTokenToUser(int userId, RefreshToken token)
    {
        User user = await EfRepository.Get(u => u.Id == userId)
            .Include(u => u.RefreshTokens)
            .SingleAsync();

        var refreshToken = mapper.Map<RefreshToken>(token);

        user.RefreshTokens.Add(refreshToken);

        await EfRepository.UpdateAsync(user);
    }

    public async Task<bool> IsUsernameBusy(string username)
    {
        return await EfRepository.Get(e => e.Username == username).AnyAsync();
    }

    public async Task<bool> IsEmailBusy(string email)
    {
        return await EfRepository.Get(e => e.Email == email).AnyAsync();
    }

    public async Task<int> Add(User user)
    {
        return (await EfRepository.AddAsync(user)).Id;
    }

    public async Task<User?> GetById(int id)
    {
        return await EfRepository.Get(e => e.Id == id).SingleOrDefaultAsync();
    }

    public async Task<User?> GetByEmailConfirmationToken(string emailConfirmationToken)
    {
        return await EfRepository.Get(e => e.EmailConfirmationToken == emailConfirmationToken).SingleOrDefaultAsync(); 
    }

    public async Task Update(User user)
    {
        await EfRepository.UpdateAsync(user);
    }

    public async Task<User?> GetByEmail(string email)
    {
        return await EfRepository.Get(e => e.Email == email).SingleOrDefaultAsync();
    }

    public async Task<File?> GetAvatar(int userId)
    {
        return await EfRepository.Get(e => e.Id == userId)
            .Include(e => e.Avatar)
            .Select(e => e.Avatar)
            .SingleOrDefaultAsync();
    }
}