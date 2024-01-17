namespace Services.TransactionCategories.GetById;

using SlimMessageBus;

public record TransactionCategoriesGetByIdRequest : IRequest<TransactionCategoriesGetByIdResponse>
{
    public int Id { get; init; }
    public int OwnerId { get; init; }
}