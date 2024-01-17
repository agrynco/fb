using SlimMessageBus;

namespace Services.Users.IsEmailBusy;

public record IsEmailAlreadyTakenRequest : IRequest<bool>
{
    public required string Email { get; init; } = default!;
}