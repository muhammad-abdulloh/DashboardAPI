using System;
using System.Threading.Tasks;
using DashboardAPI.FunctionalTests.GenericTests;
using DashboardAPI.FunctionalTests.Helpers;
using DashboardAPI.Models.DTOs.Account;
using DashboardAPI.Models.DTOs.Category;
using DashboardAPI.Models.DTOs.Like;
using DashboardAPI.Models.DTOs.Post;
using DashboardDBAccess.Data;
using Xunit;

namespace DashboardAPI.FunctionalTests.Controllers
{
    [Collection("WebApplicationFactory")]
    public sealed class LikesController : AGenericTests<GetLikeDto, AddLikeDto, UpdateLikeDto>
    {
        protected override IEntityHelper<GetLikeDto, AddLikeDto, UpdateLikeDto> Helper { get; set; }

        private readonly UserHelper _userHelper;
        private readonly CategoryHelper _categoryHelper;
        private readonly PostHelper _postHelper;
        public override async Task<GetLikeDto> AddRandomEntity()
        {
            var user = new AddAccountDto()
            {
                Email = Guid.NewGuid() + "@user.com",
                Password = "0a1234A@",
                UserDescription = "My description",
                UserName = Guid.NewGuid().ToString("N")[..20]
            };
            var userId = (await _userHelper.AddEntity(user)).Id;
            var category = new AddCategoryDto()
            {
                Name = Guid.NewGuid().ToString("N")
            };
            var post = new AddPostDto()
            {
                Name = Guid.NewGuid().ToString("N"),
                Author = userId,
                Category = (await _categoryHelper.AddEntity(category)).Id,
                Content = "test POstDto"
            };
            var like = await Helper.AddEntity(new AddLikeDto()
            {
                Post = (await _postHelper.AddEntity(post)).Id, 
                LikeableType = LikeableType.Post,
                User = userId
            });
            return like;
        }

        public LikesController(TestWebApplicationFactory factory) : base(factory)
        {
            Helper = new LikeHelper(Client);
            _userHelper = new UserHelper(Client);
            _categoryHelper = new CategoryHelper(Client);
            _postHelper = new PostHelper(Client);
        }
    }
}
