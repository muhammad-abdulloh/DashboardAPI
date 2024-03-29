﻿using System;
using System.Collections.Generic;
using System.Linq;
using DashboardDBAccess.Data;
using DashboardDBAccess.Data.JoiningEntity;
using DashboardDBAccess.Repositories.UnitOfWork;
using DashboardDBAccess.Repositories.User;

namespace DashboardDBAccess.Tests.Builders
{
    public class UserBuilder
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private string _userName;
        private string _emailAddress;
        private readonly List<Role> _roles = new();

        public UserBuilder(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public UserBuilder WithUserName(string userName)
        {
            _userName = userName;
            return this;
        }

        public UserBuilder WithEmailAddress(string emailAddress)
        {
            _emailAddress = emailAddress;
            return this;
        }

        public UserBuilder WithRole(Role role)
        {
            _roles.Add(role);
            return this;
        }

        public User Build()
        {
            var testUser = new User()
            {
                Email = Guid.NewGuid().ToString("N") + "@test.com",
                UserName = Guid.NewGuid().ToString()[..20],
                Password = "testPassword"
            };
            if (!string.IsNullOrEmpty(_userName))
                testUser.UserName = _userName;
            if (!string.IsNullOrEmpty(_emailAddress))
                testUser.Email = _emailAddress;
            if (_roles.Count > 0)
                testUser.UserRoles = _roles.Select(x => new UserRole() { User = testUser, Role = x }).ToList();
            _userRepository.Add(testUser);
            _unitOfWork.Save();
            return testUser;
        }
    }
}
