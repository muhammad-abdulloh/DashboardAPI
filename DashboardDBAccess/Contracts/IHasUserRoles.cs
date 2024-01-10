using System.Collections.Generic;
using DashboardDBAccess.Data.JoiningEntity;

namespace DashboardDBAccess.Contracts
{
    public interface IHasUserRoles
    {
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
