namespace Services.Banks.Create;

using AutoMapper;
using DAL.Abstract.Banks;
using Domain.Finances;
using Serilog;
using Services.Core;

public class BanksCreateRequestHandler(
        ILogger logger,
        IBanksRepository banksRepository,
        IMapper mapper)
    : RequestHandler<BanksCreateRequest, int>(logger)
{
    private IBanksRepository BanksRepository { get; } = banksRepository;
    private IMapper Mapper { get; } = mapper;

    protected override async Task<int> DoOnHandle(BanksCreateRequest request)
    {
        Bank bank = Mapper.Map<Bank>(request);

        bank.OwnerId = request.AuthorizedUserId;

        return await BanksRepository.Add(bank);
    }
}