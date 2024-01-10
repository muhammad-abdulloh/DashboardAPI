
using DashboardDBAccess.Data;

namespace DashboardDBAccess.Contracts
{
    public interface IHasUser
    {
        public User User { get; set; }
    }
}
