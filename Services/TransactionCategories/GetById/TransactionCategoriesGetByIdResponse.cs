namespace Services.TransactionCategories.GetById;

public record TransactionCategoriesGetByIdResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public int? ParentId { get; set; }
    public string? Description { get; set; }
}