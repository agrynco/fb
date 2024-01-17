namespace Web.API.Models.Users.Register;

using System.ComponentModel.DataAnnotations;

public class RegisterUserInput
{
    [EmailAddress(ErrorMessage = "No valid {0} entered.")]
    public required string Email { get; set; } = default!;

    [Required(ErrorMessage = "{0} is mandatory.")]
    [DataType(DataType.Text, ErrorMessage = "No valid {0} entered.")]
    public required string FirstName { get; set; } = default!;

    [Required(ErrorMessage = "{0} is mandatory.")]
    [DataType(DataType.Text, ErrorMessage = "No valid {0} entered.")]
    public required string LastName { get; set; } = default!;

    [Required(ErrorMessage = "{0} is mandatory.")]
    [DataType(DataType.Password, ErrorMessage = "No valid {0} entered.")]
    [StringLength(30, ErrorMessage = "The password must be between {2} and {1} characters.", MinimumLength = 6)]
    public required string Password { get; set; } = default!;

    [Required(ErrorMessage = "{0} is mandatory.")]
    [DataType(DataType.Text, ErrorMessage = "No valid {0} entered.")]
    public required string Username { get; set; } = default!;
}