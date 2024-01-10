using AutoMapper;
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
using DashboardDBAccess.Repositories.Category;
using DashboardDBAccess.Repositories.Comment;
using DashboardDBAccess.Repositories.Like;
using DashboardDBAccess.Repositories.Post;
using DashboardDBAccess.Repositories.Role;
using DashboardDBAccess.Repositories.Tag;
using DashboardDBAccess.Repositories.User;

namespace DashboardAPI.Tests
{
    public class AutoMapperTestProfile : Profile
    {
        public AutoMapperTestProfile(ILikeRepository likeRepository,
            IUserRepository userRepository,
            ICategoryRepository categoryRepository,
            ICommentRepository commentRepository,
            IRoleRepository roleRepository,
            IPostRepository postRepository,
            ITagRepository tagRepository)
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

            CreateMap<Like, int>().ConvertUsing(x => x.Id);
            CreateMap<Comment, int>().ConvertUsing(x => x.Id);
            CreateMap<Post, int>().ConvertUsing(x => x.Id);
            CreateMap<User, int>().ConvertUsing(x => x.Id);
            CreateMap<Category, int>().ConvertUsing(x => x.Id);
            CreateMap<Role, int>().ConvertUsing(x => x.Id);
            CreateMap<Tag, int>().ConvertUsing(x => x.Id);

            CreateMap<int, Like>().ConvertUsing(new LikeIdConverter(likeRepository));
            CreateMap<int, Comment>().ConvertUsing(new CommentIdConverter(commentRepository));
            CreateMap<int, Post>().ConvertUsing(new PostIdConverter(postRepository));
            CreateMap<int, User>().ConvertUsing(new UserIdConverter(userRepository));
            CreateMap<int, Category>().ConvertUsing(new CategoryIdConverter(categoryRepository));
            CreateMap<int, Role>().ConvertUsing(new RoleIdConverter(roleRepository));
            CreateMap<int, Tag>().ConvertUsing(new TagIdConverter(tagRepository));

            CreateMap<UpdateCategoryDto, Category>().ConvertUsing(new UpdateCategoryConverter());
            CreateMap<UpdateAccountDto, User>().ConvertUsing(new UpdateAccountConverter());
            CreateMap<UpdateCommentDto, Comment>().ConvertUsing(new UpdateCommentConverter(commentRepository, postRepository, userRepository));
            CreateMap<UpdateLikeDto, Like>().ConvertUsing(new UpdateLikeConverter(commentRepository, postRepository, userRepository));
            CreateMap<UpdatePostDto, Post>().ConvertUsing(new UpdatePostConverter(userRepository, categoryRepository));

            CreateMap<Permission, PermissionDto>();
            CreateMap<PermissionAction, PermissionActionDto>().ConvertUsing(new PermissionActionConverter());
            CreateMap<PermissionRange, PermissionRangeDto>().ConvertUsing(new PermissionRangeConverter());
            CreateMap<PermissionTarget, PermissionTargetDto>().ConvertUsing(new PermissionTargetConverter());
        }
    }
}
