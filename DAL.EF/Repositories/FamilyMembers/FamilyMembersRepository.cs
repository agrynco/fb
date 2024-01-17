namespace DAL.EF.Repositories.FamilyMembers;

using DAL.Abstract.FamilyMembers;
using DAL.EF.Core;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Domain;
using Microsoft.EntityFrameworkCore;

public class FamilyMembersRepository(
    ApplicationDbContext applicationDbContext)
    : BaseRepository<FamilyMember>(applicationDbContext), IFamilyMembersRepository
{
    public async Task<object> GetAll(int? ownerId, DataSourceLoadOptions dataSourceLoadOptions)
    {
        return await Task.FromResult(DataSourceLoader.Load(BuildQuery(ownerId), dataSourceLoadOptions));
    }

    public async Task<FamilyMemberDto[]> GetAll(int? ownerId)
    {
        return await BuildQuery(ownerId).ToArrayAsync();
    }

    public async Task<FamilyMember?> GetById(int id)
    {
        return await EfRepository.GetByIdAsync(id);
    }

    public async Task<int> Add(FamilyMember member)
    {
        return (await EfRepository.AddAsync(member)).Id;
    }

    public async Task Update(FamilyMember member)
    {
        await EfRepository.UpdateAsync(member);
    }

    public async Task Delete(int[] ids)
    {
        await EfRepository.DeleteAsync(ids);
    }

    public async Task<int[]> GetOwnerIds(int[] ids)
    {
        return await EfRepository.Get(e => ids.Contains(e.Id)).Select(e => e.OwnerId).Distinct().ToArrayAsync();
    }

    private IQueryable<FamilyMemberDto> BuildQuery(int? ownerId)
    {
        return EfRepository
            .Get(e => ownerId == null || e.OwnerId == ownerId)
            .Select(e => new FamilyMemberDto
            {
                Id = e.Id,
                Name = e.Name,
                OwnerId = e.OwnerId
            });
    }
}