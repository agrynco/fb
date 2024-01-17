using DAL.Abstract.Users;
using Serilog;
using Services.Core;

namespace Services.Users.IsUsernameBusy;

public class IsUsernameBusyRequestHandler : RequestHandler<IsUsernameBusyRequest,
    bool>
{
    private readonly IUsersRepository _usersRepository;

    public IsUsernameBusyRequestHandler(ILogger logger,
        IUsersRepository usersRepository) : base(logger)
    {
        _usersRepository = usersRepository;
    }

    protected override Task<bool> DoOnHandle(IsUsernameBusyRequest request)
    {
        return _usersRepository.IsUsernameBusy(request.Username);
    }
}