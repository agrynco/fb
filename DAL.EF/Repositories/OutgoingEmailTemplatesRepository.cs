using DAL.Abstract;
using DAL.EF.Core;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.Repositories;

public class OutgoingEmailTemplatesRepository : BaseRepository<OutgoingEmailTemplate>, IOutgoingEmailTemplatesRepository
{
    public OutgoingEmailTemplatesRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
    {
    }

    public async Task<OutgoingEmailTemplate?> GetByType(EmailTemplateType type)
    {
        return await EfRepository.Get(e => e.Type == type).SingleOrDefaultAsync();
    }
}