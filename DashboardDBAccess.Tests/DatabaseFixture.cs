using System;
using DashboardDBAccess.Data;
using DashboardDBAccess.DataContext;
using DashboardDBAccess.Repositories.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace DashboardDBAccess.Tests
{
    public sealed class DatabaseFixture : IDisposable
    {
        public DashboardDbContext Db { get; }
        public RoleManager<Role> RoleManager { get; }
        public UserManager<User> UserManager { get; }
        public IUnitOfWork UnitOfWork { get; }

        public DatabaseFixture()
        {
            var provider = TestBootstrapper.GetProvider();

            UserManager = provider.GetRequiredService<UserManager<User>>();
            RoleManager = provider.GetRequiredService<RoleManager<Role>>();
            Db = provider.GetRequiredService<DashboardDbContext>();
            UnitOfWork = new UnitOfWork(Db);
        }

        public void Dispose()
        {
            Db.Dispose();
            UnitOfWork.Dispose();
        }
    }
}
