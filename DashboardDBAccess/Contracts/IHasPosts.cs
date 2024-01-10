using System.Collections.Generic;
using DashboardDBAccess.Data;

namespace DashboardDBAccess.Contracts
{
    public interface IHasPosts
    {
        public ICollection<Post> Posts { get; set; }
    }
}
