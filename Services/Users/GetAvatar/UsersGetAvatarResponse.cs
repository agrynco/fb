namespace Services.Users.GetAvatar;

using SlimMessageBus;

public record UsersGetAvatarResponse
{
    public byte[] Data { get; set; } = Array.Empty<byte>();
    public string ContentType { get; set; } = default!;
    public string Name { get; set; } = default!;
}