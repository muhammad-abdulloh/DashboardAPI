using System.Linq;
using System.Threading.Tasks;
using DashboardDBAccess.DataContext;
using Microsoft.EntityFrameworkCore;

namespace DashboardDBAccess.Repositories.Tag
{
    public class TagRepository : Repository<Data.Tag>, ITagRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TagRepository"/> class.
        /// </summary>
        /// <param name="context"></param>
        public TagRepository(DashboardDbContext context) : base(context)
        {
        }

        /// <inheritdoc />
        public async Task<bool> NameAlreadyExists(string name)
        {
            var tag = await _context.Set<Data.Tag>().Where(x => x.Name == name).FirstOrDefaultAsync();
            return tag != null;
        }
    }
}
