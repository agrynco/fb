namespace Domain;

public class OutgoingEmailTemplate : Entity
{
    public required string BodyTemplate { get; set; } = default!;
    public required string SubjectTemplate { get; set; } = default!;
    public EmailTemplateType Type { get; set; }
}