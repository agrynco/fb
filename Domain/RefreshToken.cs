namespace Domain;

public class RefreshToken : Entity
{
    public string? CreatedByIp { get; set; }
    public DateTime Expires { get; set; }
    public bool IsActive => !IsRevoked && !IsExpired;
    public bool IsExpired => DateTime.UtcNow >= Expires;
    public bool IsRevoked => Revoked != null;
    public string? ReasonRevoked { get; set; }
    public string? ReplacedByToken { get; set; }
    public DateTime? Revoked { get; set; }
    public string? RevokedByIp { get; set; }
    public string? Token { get; set; }
}