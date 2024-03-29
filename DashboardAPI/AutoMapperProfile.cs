﻿using AutoMapper;
using DashboardAPI.Models.DTOs.Account;
using DashboardAPI.Models.DTOs.Account.Converters;
using DashboardAPI.Models.DTOs.Category;
using DashboardAPI.Models.DTOs.Category.Converters;
using DashboardAPI.Models.DTOs.Comment;
using DashboardAPI.Models.DTOs.Comment.Converters;
using DashboardAPI.Models.DTOs.Like;
using DashboardAPI.Models.DTOs.Like.Converters;
using DashboardAPI.Models.DTOs.Permission;
using DashboardAPI.Models.DTOs.Permission.Converters;
using DashboardAPI.Models.DTOs.Post;
using DashboardAPI.Models.DTOs.Post.Converters;
using DashboardAPI.Models.DTOs.Role;
using DashboardAPI.Models.DTOs.Role.Converters;
using DashboardAPI.Models.DTOs.Tag;
using DashboardAPI.Models.DTOs.Tag.Converters;
using DashboardAPI.Models.DTOs.User;
using DashboardAPI.Models.DTOs.User.Converters;
using DashboardDBAccess.Data;
using DashboardDBAccess.Data.Permission;

namespace DashboardAPI
{  
    /// <inheritdoc />
    public class AutoMapperProfile : Profile
    {
        /// <inheritdoc />
        public AutoMapperProfile()
        {
            CreateMap<AddCategoryDto, Category>();
            CreateMap<AddCommentDto, Comment>();
            CreateMap<AddLikeDto, Like>();
            CreateMap<AddPostDto, Post>();
            CreateMap<AddRoleDto, Role>();
            CreateMap<AddTagDto, Tag>();
            CreateMap<AddAccountDto, User>();

            CreateMap<Category, GetCategoryDto>();
            CreateMap<Comment, GetCommentDto>();
            CreateMap<Like, GetLikeDto>();
            CreateMap<Post, GetPostDto>();
            CreateMap<Role, GetRoleDto>();
            CreateMap<Tag, GetTagDto>();
            CreateMap<User, GetUserDto>();
            CreateMap<User, GetAccountDto>();

            CreateMap<GetCategoryDto, UpdateCategoryDto>();
            CreateMap<GetCommentDto, UpdateCommentDto>();
            CreateMap<GetLikeDto, UpdateLikeDto>();
            CreateMap<GetPostDto, UpdatePostDto>();
            CreateMap<GetRoleDto, UpdateRoleDto>();
            CreateMap<GetTagDto, UpdateTagDto>();
            CreateMap<GetAccountDto, UpdateAccountDto>();
            
            CreateMap<UpdateCategoryDto, GetCategoryDto>();
            CreateMap<UpdateCommentDto, GetCommentDto>();
            CreateMap<UpdateLikeDto, GetLikeDto>();
            CreateMap<UpdatePostDto, GetPostDto>();
            CreateMap<UpdateRoleDto, GetRoleDto>();
            CreateMap<UpdateTagDto, GetTagDto>();
            CreateMap<UpdateAccountDto, GetAccountDto>();

            CreateMap<Like, int>().ConvertUsing(x => x.Id);
            CreateMap<Comment, int>().ConvertUsing(x => x.Id);
            CreateMap<Post, int>().ConvertUsing(x => x.Id);
            CreateMap<User, int>().ConvertUsing(x => x.Id);
            CreateMap<Category, int>().ConvertUsing(x => x.Id);
            CreateMap<Role, int>().ConvertUsing(x => x.Id);
            CreateMap<Tag, int>().ConvertUsing(x => x.Id);

            CreateMap<int, Like>().ConvertUsing<LikeIdConverter>();
            CreateMap<int, Comment>().ConvertUsing<CommentIdConverter>();
            CreateMap<int, Post>().ConvertUsing<PostIdConverter>();
            CreateMap<int, User>().ConvertUsing<UserIdConverter>();
            CreateMap<int, Category>().ConvertUsing<CategoryIdConverter>();
            CreateMap<int, Role>().ConvertUsing<RoleIdConverter>();
            CreateMap<int, Tag>().ConvertUsing<TagIdConverter>();

            CreateMap<UpdateCategoryDto, Category>().ConvertUsing<UpdateCategoryConverter>();
            CreateMap<UpdateAccountDto, User>().ConvertUsing(new UpdateAccountConverter());
            CreateMap<UpdateCommentDto, Comment>().ConvertUsing<UpdateCommentConverter>();
            CreateMap<UpdateLikeDto, Like>().ConvertUsing<UpdateLikeConverter>();
            CreateMap<UpdatePostDto, Post>().ConvertUsing<UpdatePostConverter>();

            CreateMap<Permission, PermissionDto>();
            CreateMap<PermissionAction, PermissionActionDto>().ConvertUsing(new PermissionActionConverter());
            CreateMap<PermissionRange, PermissionRangeDto>().ConvertUsing(new PermissionRangeConverter());
            CreateMap<PermissionTarget, PermissionTargetDto>().ConvertUsing(new PermissionTargetConverter());
        }
    }
}
