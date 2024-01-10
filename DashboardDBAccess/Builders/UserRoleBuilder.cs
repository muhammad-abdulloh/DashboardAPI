using System.Linq;
using DashboardDBAccess.Data;
using DashboardDBAccess.Data.JoiningEntity;
using DashboardDBAccess.DataContext;

namespace DashboardDBAccess.Builders
{
    public class UserRoleBuilder
    {
        private readonly DashboardDbContext _context;
        private Role _role;
        private User _user;

        public UserRoleBuilder(DashboardDbContext context)
        {
            _context = context;
        }

        public UserRoleBuilder WithUser(string userName)
        {
            _user = _context.Users.Single(x => x.UserName == userName);
            return this;
        }

        public UserRoleBuilder WithRole(string roleName)
        {
            _role = _context.Roles.Single(x => x.Name == roleName);
            return this;
        }

        public UserRole Build()
        {
            return new UserRole() { Role = _role, User = _user };
        }
    }
}
