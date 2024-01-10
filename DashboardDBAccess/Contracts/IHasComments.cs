using System.Collections.Generic;
using DashboardDBAccess.Data;

namespace DashboardDBAccess.Contracts
{
    public interface IHasComments
    {
        public ICollection<Comment> Comments { get; set; }
    }
}
