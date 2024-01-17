namespace Web.API.Models.Accounts;

using System.ComponentModel.DataAnnotations;

public record CashAccountsCreateOrUpdateModel
{
    public bool Closed { get; set; }

    [Range(1, int.MaxValue)]
    public int CurrencyId { get; set; }

    [DataType(DataType.Text, ErrorMessage = "No valid {0} entered.")]
    public string? Description { get; set; }

    [Required(ErrorMessage = "{0} is mandatory.")]
    public required string Name { get; set; } = default!;
}