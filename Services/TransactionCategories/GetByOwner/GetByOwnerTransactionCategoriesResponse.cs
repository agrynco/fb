namespace Services.TransactionCategories.GetByOwner;

public class GetByOwnerTransactionCategoriesResponse(GetByOwnerTransactionCategoryItem[] items)
{
    public GetByOwnerTransactionCategoryItem[] Items { get; } = items;
}