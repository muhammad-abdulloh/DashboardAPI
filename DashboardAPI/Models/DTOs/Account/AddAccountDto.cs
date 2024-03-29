﻿namespace DashboardAPI.Models.DTOs.Account
{
    /// <summary>
    /// Add Dto type of <see cref="User"/>.
    /// </summary>
    public class AddAccountDto : IAccountDto
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string UserDescription { get; set; }
        
        public string ProfilePictureUrl { get; set; }
    }
}
