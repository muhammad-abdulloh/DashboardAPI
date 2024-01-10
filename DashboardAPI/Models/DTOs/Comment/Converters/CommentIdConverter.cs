using AutoMapper;
using DashboardDBAccess.Repositories.Comment;

namespace DashboardAPI.Models.DTOs.Comment.Converters
{
    /// <summary>
    /// AutoMapper converter used to enable the conversion of <see cref="Comment"/> to its resource Id.
    /// </summary>
    public class CommentIdConverter: ITypeConverter<int, DashboardDBAccess.Data.Comment>
    {
        private readonly ICommentRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommentIdConverter"/> class.
        /// </summary>
        /// <param name="repository"></param>
        public CommentIdConverter(ICommentRepository repository)
        {
            _repository = repository;
        }

        /// <inheritdoc />
        public DashboardDBAccess.Data.Comment Convert(int source, DashboardDBAccess.Data.Comment destination, ResolutionContext context)
        {
            return _repository.Get(source);
        }
    }
}
