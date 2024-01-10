
using DashboardDBAccess.Data;

namespace DashboardDBAccess.Contracts
{
    public interface IHasAuthor
    {
        public User Author { get; set; }
    }
}
