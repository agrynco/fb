namespace Domain;

using System.Text.Json.Serialization;

public class User : Entity
{
    public bool Activated { get; set; }
    public required string Email { get; init; } = default!;
    public required string? EmailConfirmationToken { get; set; }
    public required string FirstName { get; set; } = default!;
    public required string LastName { get; set; } = default!;

    [JsonIgnore]
    public required string PasswordHash { get; set; } = default!;

    [JsonIgnore]
    public byte[] PasswordSalt { get; set; } = Array.Empty<byte>();

    [JsonIgnore]
    public List<RefreshToken> RefreshTokens { get; private set; } = [];

    public string Username { get; set; } = default!;

    public File Avatar { get; set; } = default!;
    public int? AvatarId { get; set; }
}