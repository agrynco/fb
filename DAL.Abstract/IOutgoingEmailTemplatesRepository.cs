using Domain;

namespace DAL.Abstract;

public interface IOutgoingEmailTemplatesRepository
{
    Task<OutgoingEmailTemplate?> GetByType(EmailTemplateType type);
}