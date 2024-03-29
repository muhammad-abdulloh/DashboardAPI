﻿using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DashboardAPI.Authorization.Permissions;
using DashboardAPI.Models.DTOs;
using DashboardAPI.Models.DTOs.Account;
using DashboardAPI.Models.DTOs.Immutable;
using DashboardAPI.Models.Exceptions;
using DashboardAPI.Models.Mails;
using DashboardAPI.Responses;
using DashboardAPI.Services.MailService;
using DashboardAPI.Services.TokenService;
using DashboardAPI.Services.UserService;
using DashboardDBAccess.Data;
using DashboardDBAccess.Data.Permission;
using DashboardDBAccess.Specifications.FilterSpecifications.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DashboardAPI.Controllers
{
    /// <summary>
    /// Controller used to enables account action such as login / log out.
    /// </summary>
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;
        private readonly IAuthorizationService _authorizationService;
        private readonly IEmailService _emailService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UsersController"/> class.
        /// </summary>
        /// <param name="userService"></param>
        /// <param name="tokenService"></param>
        /// <param name="authorizationService"></param>
        /// <param name="emailService"></param>
        public AccountController(IUserService userService, ITokenService tokenService,
            IAuthorizationService authorizationService, IEmailService emailService)
        {
            _userService = userService;
            _tokenService = tokenService;
            _authorizationService = authorizationService;
            _emailService = emailService;
        }

        /// <summary>
        /// Confirm account email by giving the token received by email
        /// </summary>
        [HttpGet("Email/Confirmation")]
        [AllowAnonymous]
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BlogErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BlogErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ConfirmAccountEmail(string emailValidationToken, int userId)
        {
            var emailConfirmation = await _userService.ConfirmEmail(emailValidationToken, userId);
            if (!emailConfirmation)
                return BadRequest(new BlogErrorResponse(nameof(InvalidRequestException), "Bad email validation token or user Id."));
            return Ok();
        }
        
        /// <summary>
        /// Reset account password by giving the token received by email
        /// </summary>
        [HttpGet("Password/Reset")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BlogErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BlogErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ResetAccountPassword(string passwordResetToken, int userId, string newPassword)
        {
            await _userService.ResetPassword(passwordResetToken, userId, newPassword);
            return Ok();
        }
        
        /// <summary>
        /// Send an email to reset password account
        /// </summary>
        [HttpPost("ForgotPassword")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BlogErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BlogErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ForgotPassword(ResetPasswordDto resetPassword, CancellationToken token)
        {
            var user = (await _userService.GetUsers(new EmailEqualsSpecification<User>(resetPassword.Email))).FirstOrDefault();
            if (user == null)
                return Ok();
            var accountGet = await _userService.GetAccount(user.Id);
            var passwordResetToken = await _userService.GeneratePasswordResetToken(user.Id);
            await _emailService.SendEmailAsync(new Message(new List<EmailIdentity>() {new(user.UserName, accountGet.Email)}, "Reset your password", 
                $"Hello {accountGet.UserName},<br/><br/>Here is a password reset token needed to reset your password on the API: {passwordResetToken}." +
                $"<br/><br/>If you have not requested to reset your password, you can ignore this email."), token);
            return Ok();
        }

        /// <summary>
        /// Create an account (a user).
        /// </summary>
        /// <remarks>
        /// Create an account (a user).
        /// </remarks>
        /// <param name="account"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost("SignUp")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(GetAccountDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BlogErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SignUp(AddAccountDto account, CancellationToken token)
        {
           var accountGet = await _userService.AddAccount(account);
           
           var emailValidationToken = await _userService.GenerateConfirmEmailToken(accountGet.Id);
           var confirmationLink = Url.Action("ConfirmAccountEmail", "Account", new { emailValidationToken, userId = accountGet.Id }, Request.Scheme);
           await _emailService.SendEmailAsync(new Message(new List<EmailIdentity>() {new(accountGet.UserName, accountGet.Email)}, "Confirm your email", 
               $"Hello {accountGet.UserName},<br/><br/>To confirm your registration, please verify your email by clicking on this link:<br/>{confirmationLink}."), token);
           
           return Ok(accountGet);
        }

        /// <summary>
        /// Sign In as a user.
        /// </summary>
        /// <remarks>
        /// Sign In as a user.
        /// </remarks>
        /// <param name="accountLogin"></param>
        /// <returns></returns>
        [HttpPost("SignIn")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(TokenResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BlogErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SignIn(AccountLoginDto accountLogin)
        {
            if (!await _userService.SignIn(accountLogin))
                return BadRequest(new BlogErrorResponse(nameof(InvalidRequestException),"Bad username or password."));
            
            var user = await _userService.GetAccount(accountLogin.UserName);
            if (!await _userService.EmailIsConfirmed(user.Id))
                return BadRequest(new BlogErrorResponse(nameof(InvalidRequestException),"Email must be confirmed before you can sign in."));
            
            var accessToken = await _tokenService.GenerateJwtAccessToken(user.Id);
            var refreshToken = await _tokenService.GenerateRefreshToken(user.Id);
            
            return Ok(new TokenResponse(accessToken, refreshToken));
        }

        /// <summary>
        /// Revoke refresh token of a user
        /// </summary>
        /// <remarks>
        /// Revoke refresh token of a user
        /// </remarks>
        [HttpPost("RevokeRefreshToken")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> RevokeRefreshToken(int userId)
        {
            var userEntity = await _userService.GetUserEntity(userId);
            var authorized = await _authorizationService.AuthorizeAsync(User, userEntity, new PermissionRequirement(PermissionAction.CanUpdate, PermissionTarget.Account));
            if (!authorized.Succeeded)
                return Forbid();
            
            await _userService.RevokeRefreshToken(userId);
            return Ok();
        }

        /// <summary>
        /// Generate a new refresh token and access token using a valid refresh token
        /// </summary>
        /// <remarks>
        /// Sign In as a user.
        /// </remarks>
        [HttpPost("RefreshToken")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(TokenResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BlogErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Refresh(RefreshTokenDto refreshTokenModel)
        {
            var userEntity = await _userService.GetUserEntity(refreshTokenModel.UserId);
            var authorized = await _authorizationService.AuthorizeAsync(User, userEntity, new PermissionRequirement(PermissionAction.CanUpdate, PermissionTarget.Account));
            if (!authorized.Succeeded)
                return Forbid();

            var validRefreshToken = await _tokenService.IsAuthenticAndValidRefreshToken(refreshTokenModel);
            if (!validRefreshToken)
                return BadRequest("Invalid refresh token.");
            
            var accessToken = await _tokenService.GenerateJwtAccessToken(refreshTokenModel.UserId);
            var refreshToken = await _tokenService.GenerateRefreshToken(refreshTokenModel.UserId);

            return Ok(new TokenResponse(accessToken, refreshToken));
            
        }

        /// <summary>
        /// Update an account (a user).
        /// </summary>
        /// <remarks>
        /// Update an account (a user).
        /// </remarks>
        /// <param name="account"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BlogErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BlogErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BlogErrorResponse), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> UpdateAccount(UpdateAccountDto account)
        {
            if (await _userService.GetAccount(account.Id) == null)
                return NotFound();

            var userEntity = await _userService.GetUserEntity(account.Id);
            var authorized = await _authorizationService.AuthorizeAsync(User, userEntity, new PermissionRequirement(PermissionAction.CanUpdate, PermissionTarget.Account));
            if (!authorized.Succeeded)
                return Forbid();

            await _userService.UpdateAccount(account);
            return Ok();
        }

        /// <summary>
        /// Get an account by giving its Id.
        /// </summary>
        /// <remarks>
        /// Get an account by giving its Id.
        /// </remarks>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("{userId:int}")]
        [ProducesResponseType(typeof(GetAccountDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BlogErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAccount(int userId)
        {
            var userEntity = await _userService.GetUserEntity(userId);
            var authorized = await _authorizationService.AuthorizeAsync(User, userEntity, new PermissionRequirement(PermissionAction.CanRead, PermissionTarget.Account));
            if (!authorized.Succeeded)
                return Forbid();
            return Ok(await _userService.GetAccount(userId));
        }

        /// <summary>
        /// Delete an account (a user) by giving its id.
        /// </summary>
        /// <remarks>
        /// Delete an account (a user) by giving its id.
        /// </remarks>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpDelete("{userId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BlogErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            if (await _userService.GetUser(userId) == null)
                return NotFound();

            var userEntity = await _userService.GetUserEntity(userId);
            var authorized = await _authorizationService.AuthorizeAsync(User, userEntity, new PermissionRequirement(PermissionAction.CanDelete, PermissionTarget.Account));
            if (!authorized.Succeeded)
                return Forbid();

            await _userService.DeleteAccount(userId);
            return Ok();
        }
    }
}
