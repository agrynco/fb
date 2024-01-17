namespace Domain;

public class MonthTranslation : Entity
{
    public required string Value { get; set; } = default!;
    public Language Language { get; set; }
    public required int LanguageId { get; set; }
    public int MonthNumber { get; set; }
}