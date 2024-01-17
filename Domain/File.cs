namespace Domain;

public class File : Entity
{
    public byte[] Data { get; set; } = Array.Empty<byte>();
    public string ContentType { get; set; } = default!;
    public string Name { get; set; } = default!;
}