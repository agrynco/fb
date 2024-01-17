namespace Services.Users;

public record GenerateRefreshTokenResult
{
    public DateTime Created { get; set; }
    public required string CreatedByIp { get; set; } = default!;
    public DateTime Expires { get; set; }
    public required string Token { get; set; } = default!;
}