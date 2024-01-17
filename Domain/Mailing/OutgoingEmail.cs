namespace Domain;

public class OutgoingEmail : Entity
{
    public required string Body { get; set; } = default!;
    public EmailSendingStatus Status { get; set; }
    public required string Subject { get; set; } = default!;
    public OutgoingEmailTemplate Template { get; set; } = default!;
    public int TemplateId { get; set; }
    public User User { get; set; } = default!;
    public int UserId { get; set; }
    public DateTime ValidUntil { get; set; }
    
}