using System;
using System.Threading.Tasks;
using DashboardAPI.Models.DTOs.Account;
using DashboardAPI.Models.DTOs.User;
using DashboardAPI.Services.UserService;

namespace DashboardAPI.Tests.Builders
{
    public class AccountBuilder
    {
        private readonly IUserService _userService;

        public AccountBuilder(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<GetAccountDto> Build()
        {
            var userToAdd = new AddAccountDto()
            {
                Email = Guid.NewGuid().ToString("N") + "@test.com",
                Password = "16453aA-007",
                UserName = Guid.NewGuid().ToString()[..20]
            };
            var user = await _userService.AddAccount(userToAdd);
            return user;
        }
    }
}
