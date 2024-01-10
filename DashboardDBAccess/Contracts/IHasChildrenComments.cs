using System.Collections.Generic;
using DashboardDBAccess.Data;

namespace DashboardDBAccess.Contracts
{
    public interface IHasChildrenComments
    { 
        public ICollection<Comment> ChildrenComments { get; set; }
    }
}
