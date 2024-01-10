using System;
using DashboardDBAccess.Data;
using DashboardDBAccess.Repositories.Role;
using DashboardDBAccess.Repositories.UnitOfWork;

namespace DashboardDBAccess.Tests.Builders
{
    public class RoleBuilder
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RoleBuilder(IRoleRepository roleRepository, IUnitOfWork unitOfWork)
        {
            _roleRepository = roleRepository;
            _unitOfWork = unitOfWork;
        }

        public Role Build()
        {
            var testRole = new Role()
            {
                Name = Guid.NewGuid().ToString()[..20]
            };
            _roleRepository.Add(testRole);
            _unitOfWork.Save();
            return testRole;
        }
    }
}
