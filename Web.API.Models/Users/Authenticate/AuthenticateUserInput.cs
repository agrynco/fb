namespace Web.API.Models.Users.Authenticate;

using System.ComponentModel.DataAnnotations;

public class AuthenticateUserInput
{
    /// <summary>
    ///     Password
    /// </summary>
    [Required(ErrorMessage = "{0} is mandatory.")]
    [DataType(DataType.Password, ErrorMessage = "No valid {0} entered.")]
    [StringLength(30, ErrorMessage = "The password must be between {2} and {1} characters.", MinimumLength = 6)]
    public required string Password { get; set; } = default!;

    /// <summary>
    ///     Login
    /// </summary>
    [Required(ErrorMessage = "{0} is mandatory.")]
    [DataType(DataType.Text, ErrorMessage = "No valid {0} entered.")]
    public required string Username { get; init; } = default!;
}