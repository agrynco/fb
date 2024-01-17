namespace Web.API.Models.Accounts;

using System.ComponentModel.DataAnnotations;

public record CardAccountsCreateOrUpdateModel : CashAccountsCreateOrUpdateModel
{
    [Required(ErrorMessage = "{0} is mandatory.")]
    public string CardNumber { get; set; } = default!;

    [Range(1, int.MaxValue)]
    public int BankId { get; set; }
}