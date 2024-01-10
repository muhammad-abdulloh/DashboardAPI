using DashboardAPI.Models.Sort;
using DashboardDBAccess.Specifications.SortSpecification;

namespace DashboardAPI.Models.Builders.Specifications.Post
{
    /// <summary>
    /// Class used to generate <see cref="SortSpecification{TEntity}"/> for <see cref="Post"/>.
    /// </summary>
    public class SortPostFilter
    {
        private readonly Order _order;
        private readonly PostSort _sort;

        /// <summary>
        /// Initializes a new instance of the <see cref="SortPostFilter"/> class.
        /// </summary>
        /// <param name="order"></param>
        /// <param name="sort"></param>
        public SortPostFilter(Order order, PostSort sort)
        {
            _order = order;
            _sort = sort;
        }

        /// <summary>
        /// Get sort specification of <see cref="Post"/> based of internal properties defined.
        /// </summary>
        /// <returns></returns>
        public SortSpecification<DashboardDBAccess.Data.Post> GetSorting()
        {
            var sort = _sort switch
            {
                PostSort.Likes => new SortSpecification<DashboardDBAccess.Data.Post>(
                    new OrderBySpecification<DashboardDBAccess.Data.Post>(x => x.Likes.Count),
                    _order == Order.Desc
                        ? SortingDirectionSpecification.Descending
                        : SortingDirectionSpecification.Ascending),
                PostSort.Name => new SortSpecification<DashboardDBAccess.Data.Post>(
                    new OrderBySpecification<DashboardDBAccess.Data.Post>(x => x.Name),
                    _order == Order.Desc
                        ? SortingDirectionSpecification.Descending
                        : SortingDirectionSpecification.Ascending),
                PostSort.Publication => new SortSpecification<DashboardDBAccess.Data.Post>(
                    new OrderBySpecification<DashboardDBAccess.Data.Post>(x => x.PublishedAt),
                    _order == Order.Desc
                        ? SortingDirectionSpecification.Descending
                        : SortingDirectionSpecification.Ascending),
                _ => new SortSpecification<DashboardDBAccess.Data.Post>(
                    new OrderBySpecification<DashboardDBAccess.Data.Post>(x => x.PublishedAt),
                    _order == Order.Desc
                        ? SortingDirectionSpecification.Descending
                        : SortingDirectionSpecification.Ascending)
            };
            return sort;
        }
    }
}
