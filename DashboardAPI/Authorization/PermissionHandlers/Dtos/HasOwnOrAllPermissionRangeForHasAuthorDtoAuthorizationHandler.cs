﻿using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DashboardAPI.Authorization.Permissions;
using DashboardAPI.Models.DTOs.Contracts;
using DashboardAPI.Models.DTOs.Permission;
using DashboardAPI.Services.RoleService;
using DashboardAPI.Services.UserService;
using DashboardDBAccess.Data.Permission;
using Microsoft.AspNetCore.Authorization;

namespace DashboardAPI.Authorization.PermissionHandlers.Dtos
{
    /// <summary>
    /// Authorization Handler that verifies if user has a role with <see cref="PermissionRange.All"/> or <see cref="PermissionRange.Own"/>
    /// corresponding to the resource (<see cref="PermissionTarget"/>) and if it can realize the action it is asking (<see cref="PermissionAction"/>).
    /// </summary>
    /// <example>
    /// In case of <see cref="PermissionRange.All"/>, the user can realize <see cref="PermissionAction"/> on all <see cref="PermissionTarget"/>. 
    /// In case of <see cref="PermissionRange.Own"/>, the user can realize <see cref="PermissionAction"/> only on its own <see cref="PermissionTarget"/> (it is the author of this resource).
    /// </example>
    /// <typeparam name="TDto"></typeparam>
    public class HasOwnOrAllPermissionRangeForHasAuthorDtoAuthorizationHandler<TDto> : AuthorizationHandler<PermissionRequirement, TDto>
    where TDto : IHasAuthor
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;

        /// <inheritdoc />
        public HasOwnOrAllPermissionRangeForHasAuthorDtoAuthorizationHandler(IUserService userService, IRoleService roleService, IMapper mapper)
        {
            _userService = userService;
            _roleService = roleService;
            _mapper = mapper;
        }

        /// <inheritdoc />
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement, TDto resource)
        {
            var userId = int.Parse(context.User.Claims
                .First(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value);

            var account = await _userService.GetAccount(userId);
            if (account.Roles.Any())
            {
                var requirementAction = _mapper.Map<PermissionActionDto>(requirement.Permission);
                var requirementTarget = _mapper.Map<PermissionTargetDto>(requirement.PermissionTarget);

                foreach (var role in account.Roles)
                {
                    var permissions = await _roleService.GetPermissionsAsync(role);

                    if (permissions != null && permissions.Any(permission =>
                            requirementAction.Id == permission.PermissionAction.Id &&
                            requirementTarget.Id == permission.PermissionTarget.Id &&
                            ((permission.PermissionRange.Id == (int)PermissionRange.Own && resource.Author == userId)
                             || permission.PermissionRange.Id == (int)PermissionRange.All)))
                    {
                        context.Succeed(requirement);
                        return;
                    }
                }
            }
        }
    }
}
