﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DashboardAPI.Models.DTOs.Permission;
using DashboardAPI.Models.DTOs.Role;
using DashboardAPI.Models.Exceptions;
using DashboardDBAccess.Data;
using DashboardDBAccess.Data.Permission;
using DashboardDBAccess.Repositories.Role;
using DashboardDBAccess.Repositories.UnitOfWork;
using DashboardDBAccess.Specifications;
using DashboardDBAccess.Specifications.FilterSpecifications;
using DashboardDBAccess.Specifications.SortSpecification;
using FluentValidation;

namespace DashboardAPI.Services.RoleService
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<IRoleDto> _dtoValidator;

        public RoleService(IRoleRepository repository, IMapper mapper, IUnitOfWork unitOfWork, IValidator<IRoleDto> dtoValidator)
        {
            _repository = repository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _dtoValidator = dtoValidator;
        }

        public async Task<IEnumerable<GetRoleDto>> GetAllRoles()
        {
            return (await _repository.GetAllAsync()).Select(c =>
            {
                var roleDto = _mapper.Map<GetRoleDto>(c);
                roleDto.Users = c.UserRoles != null && c.UserRoles.Any() ? c.UserRoles.Select(x => x.UserId) :
                        new List<int>();
                return roleDto;
            }).ToList();
        }

        public async Task<IEnumerable<GetRoleDto>> GetRoles(FilterSpecification<Role> filterSpecification = null, PagingSpecification pagingSpecification = null,
            SortSpecification<Role> sortSpecification = null)
        {
            return (await _repository.GetAsync(filterSpecification, pagingSpecification, sortSpecification)).Select(x =>
            {
                var roleDto = _mapper.Map<GetRoleDto>(x);
                roleDto.Users = x.UserRoles != null && x.UserRoles.Any() ? x.UserRoles.Select(y => y.UserId) : new List<int>();
                return roleDto;
            });
        }

        public async Task<int> CountRolesWhere(FilterSpecification<Role> filterSpecification = null)
        {
            return await _repository.CountWhereAsync(filterSpecification);
        }

        public async Task<GetRoleDto> GetRole(int id)
        {
            var role = await _repository.GetAsync(id);
            var roleDto = _mapper.Map<GetRoleDto>(role);
            roleDto.Users = role.UserRoles.Select(x => x.UserId);
            return roleDto;
        }

        public async Task<Role> GetRoleEntity(int id)
        {
            return await _repository.GetAsync(id);
        }

        public async Task CheckRoleValidity(AddRoleDto role)
        {
            if (await _repository.NameAlreadyExists(role.Name))
                throw new InvalidRequestException("Name already exists.");
        }

        public async Task CheckRoleValidity(UpdateRoleDto role)
        {
            if (await _repository.NameAlreadyExists(role.Name) &&
                (await _repository.GetAsync(role.Id)).Name != role.Name)
                throw new InvalidRequestException("Name already exists.");
        }

        public async Task<GetRoleDto> AddRole(AddRoleDto role)
        {
            await _dtoValidator.ValidateAndThrowAsync(role);
            await CheckRoleValidity(role);
            var result = await _repository.AddAsync(_mapper.Map<Role>(role));
            _unitOfWork.Save();
            return _mapper.Map<GetRoleDto>(result);
        }

        public async Task UpdateRole(UpdateRoleDto role)
        {
            await _dtoValidator.ValidateAndThrowAsync(role);
            if (await RoleAlreadyExistsWithSameProperties(role))
                return;
            await CheckRoleValidity(role);
            var roleEntity = await _repository.GetAsync(role.Id);
            roleEntity.Name = role.Name;
            _unitOfWork.Save();
        }

        public async Task DeleteRole(int id)
        {
            await _repository.RemoveAsync(await _repository.GetAsync(id));
            _unitOfWork.Save();
        }

        private async Task<bool> RoleAlreadyExistsWithSameProperties(UpdateRoleDto role)
        {
            var roleDb = await _repository.GetAsync(role.Id);
            return role.Name == roleDb.Name;
        }

        public async Task AddPermissionAsync(int roleId, Permission permission)
        {
            await _repository.AddPermissionAsync(roleId, permission);
        }

        public async Task RemovePermissionAsync(int roleId, Permission permission)
        {
            await _repository.RemovePermissionAsync(roleId, permission);
        }

        public async Task<IEnumerable<PermissionDto>> GetPermissionsAsync(int roleId)
        {
            var permissions = await _repository.GetPermissionsAsync(roleId);
            return permissions.Select(x => _mapper.Map<PermissionDto>(x)).ToList();
        }
    }
}
