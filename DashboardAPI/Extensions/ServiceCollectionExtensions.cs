using DashboardAPI.Authorization.PermissionHandlers.Attributes;
using DashboardAPI.Authorization.PermissionHandlers.Dtos;
using DashboardAPI.Authorization.PermissionHandlers.Resources;
using DashboardAPI.Models.DTOs.Account;
using DashboardAPI.Models.DTOs.Category;
using DashboardAPI.Models.DTOs.Comment;
using DashboardAPI.Models.DTOs.Like;
using DashboardAPI.Models.DTOs.Post;
using DashboardAPI.Models.DTOs.Role;
using DashboardAPI.Models.DTOs.Tag;
using DashboardAPI.Services.CategoryService;
using DashboardAPI.Services.CommentService;
using DashboardAPI.Services.LikeService;
using DashboardAPI.Services.PostService;
using DashboardAPI.Services.RoleService;
using DashboardAPI.Services.TagService;
using DashboardAPI.Services.UrlService;
using DashboardAPI.Services.UserService;
using DashboardAPI.Validators.Account;
using DashboardAPI.Validators.Category;
using DashboardAPI.Validators.Comment;
using DashboardAPI.Validators.Like;
using DashboardAPI.Validators.Post;
using DashboardAPI.Validators.Role;
using DashboardAPI.Validators.Tag;
using DashboardDBAccess.Data;
using DashboardDBAccess.Repositories.Category;
using DashboardDBAccess.Repositories.Comment;
using DashboardDBAccess.Repositories.Like;
using DashboardDBAccess.Repositories.Post;
using DashboardDBAccess.Repositories.Role;
using DashboardDBAccess.Repositories.Tag;
using DashboardDBAccess.Repositories.UnitOfWork;
using DashboardDBAccess.Repositories.User;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;

namespace DashboardAPI.Extensions
{
    /// <summary> 
    /// Extension of <see cref="IServiceCollection"/> adding methods to inject DashboardAPI Services 
    /// </summary> 
    public static class ServiceCollectionExtensions
    {
        /// <summary> 
        /// class used to register repository services 
        /// </summary> 
        /// <param name="services"></param> 
        /// <returns></returns> 
        public static IServiceCollection RegisterRepositoryServices(this IServiceCollection services)
        {
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<ILikeRepository, LikeRepository>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<ITagRepository, TagRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }

        /// <summary> 
        /// class used to register resource services 
        /// </summary> 
        /// <param name="services"></param> 
        /// <returns></returns> 
        public static IServiceCollection RegisterResourceServices(this IServiceCollection services)
        {
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<ILikeService, LikeService>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<ITagService, TagService>();
            services.AddScoped<IUserService, UserService>();
            services.AddHttpClient<IUrlService, UrlService>();
            services.AddScoped<IUrlService, UrlService>();
            return services;
        }

        /// <summary> 
        /// class used to register resource validators
        /// </summary> 
        /// <param name="services"></param> 
        /// <returns></returns> 
        public static IServiceCollection RegisterDtoResourceValidators(this IServiceCollection services)
        {
            services.AddScoped<IValidator<ICategoryDto>, CategoryDtoValidator>();
            services.AddScoped<IValidator<ICommentDto>, CommentDtoValidator>();
            services.AddScoped<IValidator<ILikeDto>, LikeDtoValidator>();
            services.AddScoped<IValidator<IPostDto>, PostDtoValidator>();
            services.AddScoped<IValidator<IRoleDto>, RoleDtoValidator>();
            services.AddScoped<IValidator<ITagDto>, TagDtoValidator>();
            services.AddScoped<IValidator<IAccountDto>, AccountDtoValidator>();
            return services;
        }

        /// <summary> 
        /// class used to register resource services 
        /// </summary> 
        /// <param name="services"></param> 
        /// <returns></returns> 
        public static IServiceCollection RegisterAuthorizationHandlers(this IServiceCollection services)
        {
            // Resource Handlers
            services.AddScoped<IAuthorizationHandler, HasAllPermissionRangeAuthorizationHandler<Role>>();
            services.AddScoped<IAuthorizationHandler, HasAllPermissionRangeAuthorizationHandler<Tag>>();
            services.AddScoped<IAuthorizationHandler, HasAllPermissionRangeAuthorizationHandler<Category>>();
            services.AddScoped<IAuthorizationHandler, HasOwnOrAllPermissionRangeForHasAuthorEntityAuthorizationHandler<Comment>>();
            services.AddScoped<IAuthorizationHandler, HasOwnOrAllPermissionRangeForHasAuthorEntityAuthorizationHandler<Post>>();
            services.AddScoped<IAuthorizationHandler, HasOwnOrAllPermissionRangeForHasUserEntityAuthorizationHandler<Like>>();
            services.AddScoped<IAuthorizationHandler, HasOwnOrAllPermissionRangeForUserResourceAuthorizationHandler>();

            // DTO Handlers
            services.AddScoped<IAuthorizationHandler, HasOwnOrAllPermissionRangeForHasAuthorDtoAuthorizationHandler<ICommentDto>>();
            services.AddScoped<IAuthorizationHandler, HasOwnOrAllPermissionRangeForHasAuthorDtoAuthorizationHandler<IPostDto>>();
            services.AddScoped<IAuthorizationHandler, HasOwnOrAllPermissionRangeForHasUserDtoAuthorizationHandler<ILikeDto>>();

            // Resource Attribute Handler
            services.AddScoped<IAuthorizationHandler, PermissionWithRangeAuthorizationHandler>();
            
            return services;
        }

        public static IServiceCollection AddAllHttpLoggingInformationAvailable(this IServiceCollection services)
        {
            services.AddHttpLogging(logging =>
            {
                logging.LoggingFields = HttpLoggingFields.All;
                logging.RequestHeaders.Add(HeaderNames.Accept);
                logging.RequestHeaders.Add(HeaderNames.ContentType);
                logging.RequestHeaders.Add(HeaderNames.ContentDisposition);
                logging.RequestHeaders.Add(HeaderNames.ContentEncoding);
                logging.RequestHeaders.Add(HeaderNames.ContentLength);

                logging.MediaTypeOptions.AddText("application/json");
                logging.MediaTypeOptions.AddText("multipart/form-data");

                logging.RequestBodyLogLimit = 4096;
                logging.ResponseBodyLogLimit = 4096;
            });

            return services;
        }
    }
}
