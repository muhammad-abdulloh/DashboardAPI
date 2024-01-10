using AutoMapper;
using DashboardDBAccess.Repositories.Post;

namespace DashboardAPI.Models.DTOs.Post.Converters
{
    /// <summary>
    /// AutoMapper converter used to enable the conversion of <see cref="Post"/> to its resource Id.
    /// </summary>
    public class PostIdConverter : ITypeConverter<int, DashboardDBAccess.Data.Post>
    {
        private readonly IPostRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="PostIdConverter"/> class.
        /// </summary>
        /// <param name="repository"></param>
        public PostIdConverter(IPostRepository repository)
        {
            _repository = repository;
        }

        /// <inheritdoc />
        public DashboardDBAccess.Data.Post Convert(int source, DashboardDBAccess.Data.Post destination, ResolutionContext context)
        {
            return _repository.Get(source);
        }
    }
}
