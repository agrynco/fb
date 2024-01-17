namespace Services.Users.GetProfile;

public record UserProfileGeneralDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
}