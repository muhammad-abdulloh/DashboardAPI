using DashboardDBAccess.Data;

namespace DashboardDBAccess.Contracts
{
    public interface IHasPostParent
    {
        public Post PostParent { get; set; }
    }
}
