using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DashboardAPI.FunctionalTests.GenericTests;
using DashboardAPI.FunctionalTests.Helpers;
using DashboardAPI.Models.DTOs.Account;
using DashboardAPI.Models.DTOs.Category;
using DashboardAPI.Models.DTOs.Post;
using DashboardAPI.Models.DTOs.User;
using Xunit;

namespace DashboardAPI.FunctionalTests.Controllers
{
    [Collection("WebApplicationFactory")]
    public sealed class PostsController : AGenericTests<GetPostDto, AddPostDto, UpdatePostDto>
    {
        protected override IEntityHelper<GetPostDto, AddPostDto, UpdatePostDto> Helper { get; set; }

        private readonly UserHelper _userHelper;
        private readonly CategoryHelper _categoryHelper;

        public override async Task<GetPostDto> AddRandomEntity()
        {
            var user = new AddAccountDto()
            {
                Email = Guid.NewGuid() + "@user.com",
                Password = "0a1234A@",
                UserDescription = "My description",
                UserName = Guid.NewGuid().ToString("N")[..20]
            };
            var category = new AddCategoryDto()
            {
                Name = Guid.NewGuid().ToString("N")
            };
            var userAdded = await _userHelper.AddEntity(user);
            var categoryAdded = await _categoryHelper.AddEntity(category);
            var post = new AddPostDto()
            {
                Name = Guid.NewGuid().ToString("N"),
                Author = userAdded.Id,
                Category = categoryAdded.Id,
                Content = "test POstDto",
                Tags = new List<int>() {1},
                ThumbnailUrl = "https://www.facebook.com/images/fb_icon_325x325.png"
            };
            return await Helper.AddEntity(post);
        }

        public PostsController(TestWebApplicationFactory factory) : base(factory)
        {
            Helper = new PostHelper(Client);
            _categoryHelper = new CategoryHelper(Client);
            _userHelper = new UserHelper(Client);
        }
    }
}
