namespace Shop.Services
{
    using System.Threading.Tasks;

    public interface IMailService
    {
        Task SendEmailAsync(string toEmail, string subject, string content);
    }
}
