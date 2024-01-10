using System.Threading;
using System.Threading.Tasks;

namespace DashboardAPI.Services.UrlService;

public interface IUrlService
{
    public bool IsUrl(string url);

    public Task<bool> IsUrlPicture(string url, CancellationToken token);
}