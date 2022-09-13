using Application.Models;

namespace Application.Services
{
    public interface IMailService
    {
        Task SendAsync(MailRequest request);
    }
}
