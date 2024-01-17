namespace Web.API.Models.Users.ForgotPassword;

using System.ComponentModel.DataAnnotations;

public record ForgotPasswordInput
{
    [Required]
    [EmailAddress]
    [MaxLength(256)]
    public required string Email { get; set; }
}