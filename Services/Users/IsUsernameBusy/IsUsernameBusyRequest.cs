using SlimMessageBus;

namespace Services.Users.IsUsernameBusy;

public class IsUsernameBusyRequest : IRequest<bool>
{
    public required string Username { get; init; } = default!;
}