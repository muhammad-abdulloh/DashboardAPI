using System.Collections.Generic;
using System.Threading.Tasks;
using DashboardAPI.Models.DTOs.Comment;
using DashboardDBAccess.Data;
using DashboardDBAccess.Specifications;
using DashboardDBAccess.Specifications.FilterSpecifications;
using DashboardDBAccess.Specifications.SortSpecification;

namespace DashboardAPI.Services.CommentService
{
    public interface ICommentService
    {
        Task<IEnumerable<GetCommentDto>> GetAllComments();

        public Task<IEnumerable<GetCommentDto>> GetComments(FilterSpecification<Comment> filterSpecification = null,
            PagingSpecification pagingSpecification = null,
            SortSpecification<Comment> sortSpecification = null);

        public Task<int> CountCommentsWhere(FilterSpecification<Comment> filterSpecification = null);

        Task<GetCommentDto> GetComment(int id);

        Task<GetCommentDto> AddComment(AddCommentDto comment);

        Task UpdateComment(UpdateCommentDto comment);

        Task DeleteComment(int id);

        Task<IEnumerable<GetCommentDto>> GetCommentsFromUser(int id);

        Task<IEnumerable<GetCommentDto>> GetCommentsFromPost(int id);
    }
}
