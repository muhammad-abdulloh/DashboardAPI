using DashboardDBAccess.Data;

namespace DashboardDBAccess.Contracts
{
    public interface IHasCategory
    {
        public Category Category { get; set; }
    }
}
