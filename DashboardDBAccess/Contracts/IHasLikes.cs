using System.Collections.Generic;
using DashboardDBAccess.Data;

namespace DashboardDBAccess.Contracts
{
    public interface IHasLikes
    {
        public ICollection<Like> Likes { get; set; }
    }
}
