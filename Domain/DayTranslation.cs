namespace Domain;

public class DayTranslation : Entity
{
    public required string Value { get; set; } = default!;
    public Language Language { get; set; }
    public required int LanguageId { get; set; }
    public int DayNumber { get; set; }
}