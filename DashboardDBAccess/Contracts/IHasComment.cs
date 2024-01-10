using DashboardDBAccess.Data;

namespace DashboardDBAccess.Contracts
{
    public interface IHasComment
    {
        public Comment Comment { get; set; }
    }
}
