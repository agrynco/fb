namespace Services.Banks.GetAll;

using DAL.Abstract.Banks;
using Serilog;
using Services.Core;

public class GetAllBanksRequestHandler(
        ILogger logger,
        IBanksRepository banksRepository)
    : RequestHandler<GetAllBanksRequest, object>(logger)
{
    protected override async Task<object> DoOnHandle(GetAllBanksRequest request)
    {
        return await Task.FromResult(await banksRepository.GetAll(request.OwnerId, request.LoadOptions));
    }
}