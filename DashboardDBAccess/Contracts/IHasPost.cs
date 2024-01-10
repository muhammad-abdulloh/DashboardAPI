using DashboardDBAccess.Data;

namespace DashboardDBAccess.Contracts
{
    public interface IHasPost
    {
        public Post Post { get; set; }
    }
}
