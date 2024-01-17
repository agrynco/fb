namespace DAL.Abstract.FamilyMembers;

using DevExtreme.AspNet.Mvc;
using Domain;

public interface IFamilyMembersRepository
{
    Task<object> GetAll(int? ownerId, DataSourceLoadOptions dataSourceLoadOptions);
    Task<FamilyMemberDto[]> GetAll(int? ownerId);
    Task<FamilyMember?> GetById(int id);
    Task<int> Add(FamilyMember member);
    Task Update(FamilyMember member);
    Task Delete(int[] ids);
    Task<int[]> GetOwnerIds(int[] ids);
}