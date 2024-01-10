using Microsoft.AspNetCore.Identity;

namespace DashboardDBAccess.Data.JoiningEntity
{
    public class UserRole : IdentityUserRole<int>
    {
        public virtual User User { get; set; }

        public virtual Role Role { get; set; }
    }
}
