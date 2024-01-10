using System.Threading;
using System.Threading.Tasks;
using DashboardAPI.Models.Mails;
using DashboardAPI.Services.MailService;

namespace DashboardAPI.FunctionalTests.Models;

public class EmailServiceMock : IEmailService
{
    public Task SendEmailAsync(Message message, CancellationToken token)
    {
        return Task.CompletedTask;
    }
}