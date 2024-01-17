namespace Services.Accounts.GetWideById;

using DAL.Abstract.Accounts;
using SlimMessageBus;

public record AccountsGetWideByIdRequest : IRequest<WideAccountDto>
{
    public required int Id { get; set; }
    public required int AuthenticatedUserId { get; set; }
}