namespace Web.API.Models.Users;

using System.ComponentModel.DataAnnotations;

public record UsersUpdateUsernameInput
{
    [Required(ErrorMessage = "{0} is mandatory.")]
    public required string Username { get; set; } = default!;
}