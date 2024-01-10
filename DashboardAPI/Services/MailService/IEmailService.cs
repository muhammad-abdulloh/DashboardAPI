using System.Threading;
using System.Threading.Tasks;
using DashboardAPI.Models.Mails;

namespace DashboardAPI.Services.MailService;

public interface IEmailService
{
    Task SendEmailAsync(Message message, CancellationToken token);
}