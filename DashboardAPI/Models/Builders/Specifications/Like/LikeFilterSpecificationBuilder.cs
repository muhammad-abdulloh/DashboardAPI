using DashboardDBAccess.Data;
using DashboardDBAccess.Specifications.FilterSpecifications;
using DashboardDBAccess.Specifications.FilterSpecifications.Filters;

namespace DashboardAPI.Models.Builders.Specifications.Like
{
    /// <summary>
    /// Class used to generate <see cref="FilterSpecification{TEntity}"/> for <see cref="Like"/>.
    /// </summary>
    public class LikeFilterSpecificationBuilder
    {
        private readonly LikeableType? _likeableType;

        /// <summary>
        /// Initializes a new instance of the <see cref="LikeFilterSpecificationBuilder"/> class.
        /// </summary>
        /// <param name="likeableType"></param>
        public LikeFilterSpecificationBuilder(LikeableType? likeableType)
        {
            _likeableType = likeableType;
        }

        /// <summary>
        /// Get filter specification of <see cref="Like"/> based of internal properties defined.
        /// </summary>
        /// <returns></returns>
        public FilterSpecification<DashboardDBAccess.Data.Like> Build()
        {

            FilterSpecification<DashboardDBAccess.Data.Like> filter = null;

            if (_likeableType != null)
                filter = new LikeableTypeSpecification<DashboardDBAccess.Data.Like>(_likeableType.Value);

            return filter;
        }
    }
}
