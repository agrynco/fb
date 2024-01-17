namespace Web.API.Models.Users.ChangePassword;

using System.ComponentModel.DataAnnotations;

public record ChangePasswordInput
{
    [Required(ErrorMessage = "{0} is mandatory.")]
    [DataType(DataType.Password, ErrorMessage = "No valid {0} entered.")]
    [StringLength(30, ErrorMessage = "The password must be between {2} and {1} characters.", MinimumLength = 6)]
    public required string Password { get; init; }
    
    [Required]
    [MaxLength(100)]
    public required string Token { get; init; }
}