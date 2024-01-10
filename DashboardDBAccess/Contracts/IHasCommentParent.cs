using DashboardDBAccess.Data;

namespace DashboardDBAccess.Contracts
{
    public interface IHasCommentParent
    {
        public Comment CommentParent { get; set; }
    }
}
