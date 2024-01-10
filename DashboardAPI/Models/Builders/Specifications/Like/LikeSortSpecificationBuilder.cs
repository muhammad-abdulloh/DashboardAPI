using DashboardDBAccess.Specifications.SortSpecification;

namespace DashboardAPI.Models.Builders.Specifications.Like
{
    /// <summary>
    ///  Class used to generate <see cref="SortSpecification{TEntity}"/> for <see cref="Like"/>.
    /// </summary>
    public class SortLikeBuilder
    {
        private readonly Order _order;

        /// <summary>
        /// Initializes a new instance of the <see cref="SortLikeBuilder"/> class.
        /// </summary>
        /// <param name="order"></param>
        public SortLikeBuilder(Order order)
        {
            _order = order;
        }

        /// <summary>
        /// Get sort specification of <see cref="Like"/> based of internal properties defined.
        /// </summary>
        /// <returns></returns>
        public SortSpecification<DashboardDBAccess.Data.Like> Build()
        {
            var sort = new SortSpecification<DashboardDBAccess.Data.Like>(
                new OrderBySpecification<DashboardDBAccess.Data.Like>(x => x.PublishedAt),
                _order == Order.Desc
                    ? SortingDirectionSpecification.Descending
                    : SortingDirectionSpecification.Ascending);
            return sort;
        }
    }
}
