using AutoMapper;
using DashboardDBAccess.Repositories.Like;

namespace DashboardAPI.Models.DTOs.Like.Converters
{
    /// <summary>
    /// AutoMapper converter used to enable the conversion of <see cref="Like"/> to its resource Id.
    /// </summary>
    public class LikeIdConverter : ITypeConverter<int, DashboardDBAccess.Data.Like>
    {
        private readonly ILikeRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="LikeIdConverter"/> class.
        /// </summary>
        /// <param name="repository"></param>
        public LikeIdConverter(ILikeRepository repository)
        {
            _repository = repository;
        }

        /// <inheritdoc />
        public DashboardDBAccess.Data.Like Convert(int source, DashboardDBAccess.Data.Like destination, ResolutionContext context)
        {
            return _repository.Get(source);
        }
    }
}
