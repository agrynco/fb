namespace Services.Banks.GetById;

using SlimMessageBus;

public record BanksGetByIdRequest : IRequest<BanksGetByIdResponse>
{
    public int Id { get; init; }
    public int OwnerId { get; init; }
}