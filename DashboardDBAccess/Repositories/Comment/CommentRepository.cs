using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DashboardDBAccess.DataContext;
using DashboardDBAccess.Exceptions;
using DashboardDBAccess.Specifications;
using DashboardDBAccess.Specifications.FilterSpecifications;
using DashboardDBAccess.Specifications.SortSpecification;
using Microsoft.EntityFrameworkCore;

namespace DashboardDBAccess.Repositories.Comment
{
    public class CommentRepository : Repository<Data.Comment>, ICommentRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommentRepository"/> class.
        /// </summary>
        /// <param name="context"></param>
        public CommentRepository(DashboardDbContext context) : base(context)
        {
        }

        /// <inheritdoc />
        public override async Task<IEnumerable<Data.Comment>> GetAsync(FilterSpecification<Data.Comment> filterSpecification = null,
            PagingSpecification pagingSpecification = null,
            SortSpecification<Data.Comment> sortSpecification = null)
        {
            var query = GenerateQuery(filterSpecification, pagingSpecification, sortSpecification);
            return await query.Include(x => x.Likes)
                .Include(x => x.PostParent)
                .Include(x => x.ChildrenComments)
                .Include(x => x.Author).ToListAsync();
        }

        /// <inheritdoc />
        public override async Task<Data.Comment> GetAsync(int id)
        {
            try
            {
                return await _context.Set<Data.Comment>()
                    .Include(x => x.Likes)
                    .Include(x => x.PostParent)
                    .Include(x => x.ChildrenComments)
                    .Include(x => x.Author).SingleAsync(x => x.Id == id);
            }
            catch
            {
                throw new ResourceNotFoundException("Comment doesn't exist.");
            }
        }

        /// <inheritdoc />
        public override Data.Comment Get(int id)
        {
            try
            {
                return _context.Set<Data.Comment>()
                    .Include(x => x.Likes)
                    .Include(x => x.PostParent)
                    .Include(x => x.ChildrenComments)
                    .Include(x => x.Author).Single(x => x.Id == id);
            }
            catch
            {
                throw new ResourceNotFoundException("Comment doesn't exist.");
            }
        }

        /// <inheritdoc />
        public override IEnumerable<Data.Comment> GetAll()
        {
            return _context.Set<Data.Comment>()
                .Include(x => x.Likes)
                .Include(x => x.PostParent)
                .Include(x => x.ChildrenComments)
                .Include(x => x.Author).ToList();
        }

        /// <inheritdoc />
        public override async Task<IEnumerable<Data.Comment>> GetAllAsync()
        {
            return await _context.Set<Data.Comment>()
                .Include(x => x.Likes)
                .Include(x => x.PostParent)
                .Include(x => x.ChildrenComments)
                .Include(x => x.Author).ToListAsync();
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Data.Comment>> GetCommentsFromPost(int id)
        {
            return await _context.Set<Data.Comment>()
                .Include(x => x.Likes)
                .Include(x => x.Author)
                .Include(x => x.ChildrenComments)
                .Include(x => x.PostParent).Where(x => x.PostParent.Id == id).ToListAsync();
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Data.Comment>> GetCommentsFromUser(int id)
        {
            return await _context.Set<Data.Comment>()
                .Include(x => x.Likes)
                .Include(x => x.Author)
                .Include(x => x.ChildrenComments)
                .Include(x => x.PostParent).Where(x => x.Author.Id == id).ToListAsync();
        }
    }
}
