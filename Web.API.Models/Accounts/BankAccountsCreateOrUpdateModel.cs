namespace Web.API.Models.Accounts;

using System.ComponentModel.DataAnnotations;

public record BankAccountsCreateOrUpdateModel : CashAccountsCreateOrUpdateModel
{
    [Range(1, int.MaxValue)]
    public int BankId { get; set; }
    
    [Required]
    public string IBAN { get; set; } = default!;
}