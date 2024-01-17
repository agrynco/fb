namespace Services.Banks.Update;

using AutoMapper;
using DAL.Abstract.Banks;
using Domain.Finances;
using Serilog;
using Services.Core;

public class BanksUpdateMessageConsumer(
        ILogger logger,
        IBanksRepository banksRepository,
        IMapper mapper)
    : MessageConsumer<BanksUpdateMessage>(logger)
{
    protected override async Task DoOnHandle(BanksUpdateMessage message)
    {
        Bank bank = (await banksRepository.GetById(message.Id))!;
        
        if (bank.OwnerId != message.AuthorizedUserId)
        {
            throw new WrongBankOwnerException(new []{message.Id}, message.AuthorizedUserId);
        }
        
        mapper.Map(message, bank);
        
        await banksRepository.Update(bank);
    }
}