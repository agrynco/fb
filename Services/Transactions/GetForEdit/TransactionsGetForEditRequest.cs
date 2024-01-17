namespace Services.Transactions.GetForCreateOrEdit;

using SlimMessageBus;

public class TransactionsGetForEditRequest : IRequest<TransactionsGetForEditResponse>
{
    public int Id { get; init; }
    public int AuthorizedUserId { get; init; }
}