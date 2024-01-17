namespace Services.Users.UpdateUsername;

public record UsersUpdateUsernameMessage
{
    public int UserId { get; set; }
    public string Username { get; set; }
}