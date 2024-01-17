namespace Services.Banks.GetById;

using AutoMapper;
using DAL.Abstract.Banks;
using Domain.Finances;
using Serilog;
using Services.Core;

public class GetByIdRequestHandler(
        ILogger logger,
        IBanksRepository banksRepository,
        IMapper mapper)
    : RequestHandler<BanksGetByIdRequest, BanksGetByIdResponse>(logger)
{
    protected override async  Task<BanksGetByIdResponse> DoOnHandle(BanksGetByIdRequest request)
    {
        Bank bank = (await banksRepository.GetById(request.Id))!;

        if (bank.OwnerId != request.OwnerId)
        {
            throw new WrongBankOwnerException(new []{request.Id}, request.OwnerId);
        };
        
        return mapper.Map<BanksGetByIdResponse>(bank);
    }
}