﻿using System.Collections.Generic;
using System.Threading.Tasks;
using DashboardAPI.Authorization.Attributes;
using DashboardAPI.Models.Builders.Specifications;
using DashboardAPI.Models.Builders.Specifications.Tag;
using DashboardAPI.Models.DTOs.Post;
using DashboardAPI.Models.DTOs.Tag;
using DashboardAPI.Models.Queries;
using DashboardAPI.Responses;
using DashboardAPI.Services.PostService;
using DashboardAPI.Services.TagService;
using DashboardDBAccess.Data.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DashboardAPI.Controllers
{
    /// <summary>
    /// Controller used to expose Tag resources of the API.
    /// </summary>
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class TagsController : ControllerBase
    {
        private readonly ITagService _tagService;
        private readonly IPostService _postService;

        /// <summary>
        /// Initializes a new instance of the <see cref="TagsController"/> class.
        /// </summary>
        /// <param name="tagService"></param>
        /// <param name="postService"></param>
        public TagsController(ITagService tagService, IPostService postService)
        {
            _tagService = tagService;
            _postService = postService;
        }

        /// <summary>
        /// Get list of tags.
        /// </summary>
        /// <remarks>
        /// Get list of tags. The endpoint uses pagination and sort. Filter(s) can be applied for research.
        /// </remarks>
        /// <param name="parameters"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(PagedBlogResponse<GetTagDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTags([FromQuery] GetTagQueryParameters parameters)
        {
            var pagingSpecificationBuilder = new PagingSpecificationBuilder(parameters.Page, parameters.PageSize);
            var filterSpecification = new TagQueryFilter(parameters.InName).Build();
            var data = await _tagService.GetTags(filterSpecification,
                pagingSpecificationBuilder.Build(), new TagSortSpecificationBuilder(parameters.OrderBy).Build());

            return Ok(new PagedBlogResponse<GetTagDto>(data, pagingSpecificationBuilder.Page, pagingSpecificationBuilder.Limit,
                await _tagService.CountTagsWhere(filterSpecification)));
        }

        /// <summary>
        /// Get a tag by giving its Id.
        /// </summary>
        /// <remarks>
        /// Get a tag by giving its Id.
        /// </remarks>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(GetTagDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BlogErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _tagService.GetTag(id));
        }

        /// <summary>
        /// Add a tag.
        /// </summary>
        /// <remarks>
        /// Add a tag.
        /// </remarks>
        [HttpPost]
        [PermissionWithPermissionRangeAllRequired(PermissionAction.CanCreate, PermissionTarget.Tag)]
        [ProducesResponseType(typeof(GetTagDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BlogErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BlogErrorResponse), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> AddTag(AddTagDto user)
        {
            return Ok(await _tagService.AddTag(user));
        }

        /// <summary>
        /// Update a tag.
        /// </summary>
        /// <remarks>
        /// Update a tag.
        /// </remarks>
        /// <param name="tag"></param>
        /// <returns></returns>
        [HttpPut]
        [PermissionWithPermissionRangeAllRequired(PermissionAction.CanUpdate, PermissionTarget.Tag)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BlogErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BlogErrorResponse), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> UpdateTag(UpdateTagDto tag)
        {
            if (await _tagService.GetTag(tag.Id) == null)
                return NotFound();
            await _tagService.UpdateTag(tag);
            return Ok();
        }

        /// <summary>
        /// Delete a tag by giving its id.
        /// </summary>
        /// <remarks>
        /// Delete a tag by giving its id.
        /// </remarks>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        [PermissionWithPermissionRangeAllRequired(PermissionAction.CanDelete, PermissionTarget.Tag)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BlogErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (await _tagService.GetTag(id) == null)
                return NotFound();
            await _tagService.DeleteTag(id);
            return Ok();
        }

        /// <summary>
        /// Get posts from a tag by giving tag's id.
        /// </summary>
        /// <remarks>
        /// Get posts from a tag by giving tag's id.
        /// </remarks>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}/Posts/")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(IEnumerable<GetPostDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BlogErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPostsFromTag(int id)
        {
            return Ok(await _postService.GetPostsFromTag(id));
        }
    }
}
