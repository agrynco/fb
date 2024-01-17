namespace Web.API.Models.TransactionCategories;

using System.ComponentModel.DataAnnotations;

public class AddOrUpdateTransactionCategoryModel
{
    public string? Description { get; init; }

    [Required(ErrorMessage = "{0} is mandatory.")]
    public required string Name { get; init; } = default!;

    public int? ParentId { get; init; }
}