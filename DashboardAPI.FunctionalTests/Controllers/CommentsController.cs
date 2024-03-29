﻿using System;
using System.Threading.Tasks;
using DashboardAPI.FunctionalTests.GenericTests;
using DashboardAPI.FunctionalTests.Helpers;
using DashboardAPI.Models.DTOs.Account;
using DashboardAPI.Models.DTOs.Category;
using DashboardAPI.Models.DTOs.Comment;
using DashboardAPI.Models.DTOs.Post;
using Xunit;

namespace DashboardAPI.FunctionalTests.Controllers
{
    [Collection("WebApplicationFactory")]
    public sealed class CommentsController : AGenericTests<GetCommentDto, AddCommentDto, UpdateCommentDto>
    {
        protected override IEntityHelper<GetCommentDto, AddCommentDto, UpdateCommentDto> Helper { get; set; }
        private readonly PostHelper _postHelper;
        private readonly UserHelper _userHelper;
        private readonly CategoryHelper _categoryHelper;

        public override async Task<GetCommentDto> AddRandomEntity()
        {
            var user = new AddAccountDto()
            {
                Email = Guid.NewGuid().ToString("N") + "@user.com",
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
            var comment = new AddCommentDto()
            {
                Author = userId,
                PostParent = (await _postHelper.AddEntity(post)).Id,
                Content = "test CommentDto"
            };
            return await Helper.AddEntity(comment);
        }

        public CommentsController(TestWebApplicationFactory factory) : base(factory)
        {
            Helper = new CommentHelper(Client);
            _postHelper = new PostHelper(Client);
            _userHelper = new UserHelper(Client);
            _categoryHelper = new CategoryHelper(Client);
        }
    }
}
