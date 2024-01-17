namespace Services.FamilyMembers.Delete;

using DAL.Abstract.Core;
using DAL.Abstract.FamilyMembers;
using Serilog;
using Services.Core;

public class FamilyMembersDeleteMessageConsumer(
    ILogger logger,
    IFamilyMembersRepository familyMembersRepository,
    IUnitOfWork unitOfWork)
    : MessageConsumer<FamilyMembersDeleteMessage>(logger)
{
    protected override async Task DoOnHandle(FamilyMembersDeleteMessage message)
    {
        int[] ownerIds = await familyMembersRepository.GetOwnerIds(message.Ids);
        
        if (ownerIds.Length > 1 || ownerIds[0] != message.AuthorizedUserId)
        {
            throw new WrongFamilyMemberOwnerException(message.Ids, message.AuthorizedUserId);
        }
        
        await new Func<Task>(async () => { await familyMembersRepository.Delete(message.Ids); })
            .ExecuteInTransactionAsync(unitOfWork, message);
    }
}