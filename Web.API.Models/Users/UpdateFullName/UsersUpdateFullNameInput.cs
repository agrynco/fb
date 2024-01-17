namespace Web.API.Models.Users.UpdateFullName;

using System.ComponentModel.DataAnnotations;

public class UsersUpdateFullNameInput
{
    [Required(ErrorMessage = "{0} is mandatory.")]
    [DataType(DataType.Text, ErrorMessage = "No valid {0} entered.")]
    public string FirstName { get; set; }
    
    [Required(ErrorMessage = "{0} is mandatory.")]
    [DataType(DataType.Text, ErrorMessage = "No valid {0} entered.")]
    public string LastName { get; set; }
}