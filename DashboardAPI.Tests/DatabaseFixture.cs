using System.Threading.Tasks;
using DashboardDBAccess;
using DashboardDBAccess.Data;
using DashboardDBAccess.DataContext;
using DashboardDBAccess.Repositories.Category;
using DashboardDBAccess.Repositories.Comment;
using DashboardDBAccess.Repositories.Like;
using DashboardDBAccess.Repositories.Post;
using DashboardDBAccess.Repositories.Role;
using DashboardDBAccess.Repositories.Tag;
using DashboardDBAccess.Repositories.UnitOfWork;
using DashboardDBAccess.Repositories.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace DashboardAPI.Tests
{
    public class DatabaseFixture : IAsyncLifetime
    {
        public DashboardDbContext Db { get; }
        public IUnitOfWork UnitOfWork { get; }
        public AutoMapperTestProfile MapperProfile { get; }

        public UserManager<User> UserManager { get; }
        public RoleManager<Role> RoleManager { get; }

        public async Task InitializeAsync()
        {
            await DbInitializer.SeedWithDefaultValues(Db, RoleManager, UserManager);
        }

        public Task DisposeAsync()
        {
            Db.Dispose();
            UnitOfWork.Dispose();
            return Task.CompletedTask;
        }

        public DatabaseFixture()
        {
            var provider = TestBootstrapper.GetProvider();

            UserManager = provider.GetRequiredService<UserManager<User>>();
            RoleManager = provider.GetRequiredService<RoleManager<Role>>();
            Db = provider.GetRequiredService<DashboardDbContext>();
            UnitOfWork = new UnitOfWork(Db);

            MapperProfile = new AutoMapperTestProfile(new LikeRepository(Db), new UserRepository(Db, UserManager), new CategoryRepository(Db),
                new CommentRepository(Db), new RoleRepository(Db, RoleManager), new PostRepository(Db), new TagRepository(Db));
        }
    }
}
