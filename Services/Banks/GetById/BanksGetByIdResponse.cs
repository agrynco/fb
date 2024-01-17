namespace Services.Banks.GetById;

public record BanksGetByIdResponse
{
    public string? Description { get; set; }
    public int Id { get; set; }
    public string Name { get; init; } = String.Empty;
    public int Owner { get; set; }
}