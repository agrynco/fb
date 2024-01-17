namespace Mailing;

using System.Threading.Tasks;

public interface IMailSender
{
    Task<bool> Send(string to, string subject, string body);
}