using System.Collections.Generic;
using System.Threading.Tasks;
using DashboardAPI.Models.DTOs.Post;
using DashboardDBAccess.Data;
using DashboardDBAccess.Specifications;
using DashboardDBAccess.Specifications.FilterSpecifications;
using DashboardDBAccess.Specifications.SortSpecification;

namespace DashboardAPI.Services.PostService
{
    public interface IPostService
    {
        Task<IEnumerable<GetPostDto>> GetAllPosts();

        public Task<IEnumerable<GetPostDto>> GetPosts(FilterSpecification<Post> filterSpecification = null,
            PagingSpecification pagingSpecification = null,
            SortSpecification<Post> sortSpecification = null);

        public Task<int> CountPostsWhere(FilterSpecification<Post> filterSpecification = null);

        Task<IEnumerable<GetPostDto>> GetPostsFromUser(int id);

        Task<IEnumerable<GetPostDto>> GetPostsFromTag(int id);

        Task<IEnumerable<GetPostDto>> GetPostsFromCategory(int id);

        Task<GetPostDto> GetPost(int id);

        Task<GetPostDto> AddPost(AddPostDto post);

        Task UpdatePost(UpdatePostDto post);

        Task DeletePost(int id);
    }
}
