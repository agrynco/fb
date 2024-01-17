using DAL.Abstract.Users;
using Serilog;
using Services.Core;

namespace Services.Users.IsEmailBusy;

public class IsEmailAlreadyTakenRequestHandler : RequestHandler<IsEmailAlreadyTakenRequest, bool>
{
    private readonly IUsersRepository _usersRepository;

    public IsEmailAlreadyTakenRequestHandler(ILogger logger, IUsersRepository usersRepository) : base(logger)
    {
        _usersRepository = usersRepository;
    }

    protected override async Task<bool> DoOnHandle(IsEmailAlreadyTakenRequest request)
    {
        return await _usersRepository.IsEmailBusy(request.Email);
    }
}