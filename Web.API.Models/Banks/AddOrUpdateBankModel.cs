namespace Web.API.Models.Banks;

using System.ComponentModel.DataAnnotations;

public class AddOrUpdateBankModel
{
    public string? Description { get; init; }

    [Required]
    [MaxLength(200)]
    public string Name { get; init; } = string.Empty;
}