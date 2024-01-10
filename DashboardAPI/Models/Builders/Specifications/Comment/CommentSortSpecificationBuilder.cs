using DashboardAPI.Models.Sort;
using DashboardDBAccess.Specifications.SortSpecification;

namespace DashboardAPI.Models.Builders.Specifications.Comment
{
    /// <summary>
    /// Class used to generate <see cref="SortSpecification{TEntity}"/> for <see cref="Comment"/>.
    /// </summary>
    public class CommentSortSpecificationBuilder
    {
        private readonly Order _order;
        private readonly CommentSort _sort;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommentSortSpecificationBuilder"/> class.
        /// </summary>
        /// <param name="order"></param>
        /// <param name="sort"></param>
        public CommentSortSpecificationBuilder(Order order, CommentSort sort)
        {
            _order = order;
            _sort = sort;
        }

        /// <summary>
        /// Get sort specification of <see cref="Comment"/> based of internal properties defined.
        /// </summary>
        /// <returns></returns>
        public SortSpecification<DashboardDBAccess.Data.Comment> Build()
        {
            var sort = _sort switch
            {
                CommentSort.Likes => new SortSpecification<DashboardDBAccess.Data.Comment>(
                    new OrderBySpecification<DashboardDBAccess.Data.Comment>(x => x.Likes.Count),
                    _order == Order.Desc
                        ? SortingDirectionSpecification.Descending
                        : SortingDirectionSpecification.Ascending),
                CommentSort.Comments => new SortSpecification<DashboardDBAccess.Data.Comment>(
                    new OrderBySpecification<DashboardDBAccess.Data.Comment>(x => x.ChildrenComments.Count),
                    _order == Order.Desc
                        ? SortingDirectionSpecification.Descending
                        : SortingDirectionSpecification.Ascending),
                _ => new SortSpecification<DashboardDBAccess.Data.Comment>(
                    new OrderBySpecification<DashboardDBAccess.Data.Comment>(x => x.PublishedAt),
                    _order == Order.Desc
                        ? SortingDirectionSpecification.Descending
                        : SortingDirectionSpecification.Ascending)
            };
            return sort;
        }
    }
}
