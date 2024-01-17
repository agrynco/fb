using DAL.Abstract;
using DAL.EF.Core;
using Domain;

namespace DAL.EF.Repositories;

using Microsoft.EntityFrameworkCore;

public class OutgoingEmailsRepository(ApplicationDbContext applicationDbContext)
    : BaseRepository<OutgoingEmail>(applicationDbContext), IOutgoingEmailsRepository
{
    public async Task<int> Add(OutgoingEmail email)
    {
        return (await EfRepository.AddAsync(email)).Id;
    }

    public async Task<OutgoingEmail> GetById(int id)
    {
        return await EfRepository.GetByIdAsync(id);
    }

    public async Task<OutgoingEmail?> GetBy(int userId, string subject)
    {
        return await EfRepository.Get(e => e.UserId == userId && e.Subject == subject).SingleOrDefaultAsync();
    }

    public async Task Update(OutgoingEmail email)
    {
        await EfRepository.UpdateAsync(email);
    }

    public async Task<User> GetUserByMailId(int id)
    {
        return await EfRepository.Get(e => e.Id == id)
            .Include(e => e.User)
            .Select(e => e.User).SingleOrDefaultAsync();
    }
}