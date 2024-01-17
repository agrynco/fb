namespace Services.Banks.Delete;

using DAL.Abstract.Banks;
using DAL.Abstract.Core;
using Serilog;
using Services.Core;

public class BanksDeleteMessageConsumer
    (
        ILogger logger,
        IBanksRepository banksRepository,
        IUnitOfWork unitOfWork)
    : MessageConsumer<BanksDeleteMessage>(logger)
{
    protected override async Task DoOnHandle(BanksDeleteMessage message)
    {
        int[] ownerIds = await banksRepository.GetOwnerIds(message.Ids);

        if (ownerIds.Length > 1 || ownerIds[0] != message.AuthorizedUserId)
        {
            throw new WrongBankOwnerException(message.Ids, message.AuthorizedUserId);
        }
        
        await new Func<Task>(async () => { await banksRepository.Delete(message.Ids); })
            .ExecuteInTransactionAsync(unitOfWork, message);
    }
}