﻿using MextFullstackSaaS.Application.Common.Interfaces;
using MextFullstackSaaS.Application.Common.Models;
using MextFullstackSaaS.Application.Common.Models.Auth;
using MextFullstackSaaS.Application.Features.UserAuth.Commands.Login;
using MextFullstackSaaS.Application.Features.UserAuth.Commands.Register;
using MextFullstackSaaS.Application.Features.UserAuth.Commands.ResetPassword;
using MextFullstackSaaS.Application.Features.UserAuth.Commands.VerifyEmail;
using MextFullstackSaaS.Domain.Entities;
using MextFullstackSaaS.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Web;

namespace MextFullstackSaaS.Infrastructure.Services
{
    public class IdentityManager : IIdentityService
    {
        private readonly IJwtService _jwtService;
        private readonly UserManager<User> _userManager;

        public IdentityManager(UserManager<User> userManager, IJwtService jwtService)
        {
            _userManager = userManager;
            _jwtService = jwtService;
        }

        public async Task<UserAuthRegisterResponseDto> RegisterAsync(UserAuthRegisterCommand UserAuthRegisterCommand, CancellationToken cancellationToken)
        {
            var user = UserAuthRegisterCommand.ToUser(UserAuthRegisterCommand);
            var result = await _userManager.CreateAsync(user, UserAuthRegisterCommand.Password);

            if (!result.Succeeded)
            {
                throw new Exception("User registration failed");
            }

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            return new UserAuthRegisterResponseDto(user.Id, user.Email, user.FirstName, token);
        }

        public Task<JwtDto> SignInAsync(UserAuthRegisterCommand command, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<JwtDto> LoginAsync(UserAuthLoginCommand command, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(command.Email);

           var jwtDto = await _jwtService.GenerateTokenAsync(user.Id,user.Email,cancellationToken);
            return jwtDto;
        }
        public async Task<bool> IsEmailExistsAsync(string email, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user is not null)
                return true;
            return false;
        }
        public async Task<bool> CheckPasswordSignInAsync(string email, string password ,CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user is null) return true;
            return await _userManager.CheckPasswordAsync(user, password);
        }

        public async Task<bool> VerifyEmailAsync(UserAuthVerifyEmailCommand command, CancellationToken cancellationToken)
        {

            var user = await _userManager.FindByEmailAsync(command.Email);

            var result = await _userManager.ConfirmEmailAsync(user, command.Token);           

            if (!result.Succeeded)
            {
                throw new Exception("User's email verification failed");
            }

            return true;
        }

        public Task<bool> CheckIfEmailVerifiedAsync(string email, CancellationToken cancellationToken)
        {
           return _userManager.Users.AnyAsync(x => x.Email == email && x.EmailConfirmed, cancellationToken);
        }

        // ForgotPassword metodu
        public async Task<string> ForgotPasswordAsync(string email, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(email);            

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            return token;
        }

        // ResetPassword metodu
        public async Task<bool> ResetPasswordAsync(string email, string token, string newPassword, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(email);

            var decodedToken = HttpUtility.UrlDecode(token);

            var result = await _userManager.ResetPasswordAsync(user, decodedToken, newPassword);
            if (!result.Succeeded)
            {
                throw new Exception("Password reset failed");
            }
            return true;
        }

        Task<bool> IIdentityService.ForgotPasswordAsync(string email, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}