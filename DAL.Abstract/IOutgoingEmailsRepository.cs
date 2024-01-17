namespace DAL.Abstract;

using Domain;

public interface IOutgoingEmailsRepository
{
    Task<int> Add(OutgoingEmail email);
    Task<OutgoingEmail> GetById(int id);
    Task<OutgoingEmail?> GetBy(int userId, string subject);
    Task Update(OutgoingEmail email);
    Task<User> GetUserByMailId(int id);
}