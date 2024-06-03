using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Threading.Tasks;
using FluentValidation;
using MextFullstackSaaS.Application.Common.Interfaces;


namespace MextFullstackSaaS.Application.Features.UserAuth.Commands.Login
{
    public class UserAuthLoginCommandValidator:AbstractValidator<UserAuthLoginCommand>
    {
         private readonly IIdentityService _identityService;
    

    public UserAuthLoginCommandValidator(IIdentityService identityService )
    {
        _identityService = identityService;
        
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Email is not valid");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters");
        
        RuleFor(x => x.Email)
           .MustAsync((x, y, cancellationToken) => CheckPasswordSignInAsync(x.Email, x.Password, cancellationToken))
           .WithMessage("Your Email or password is incorrect.");

        RuleFor(x => x.Email)
           .MustAsync( CheckIfEmailVerifiedAsync)
           .WithMessage("Your Email is not verified.");
    }
    private  Task<bool> CheckIfUserExists(string email, CancellationToken cancellationToken )
    {
        return  _identityService.IsEmailExistsAsync(email, cancellationToken);
    }

    private async Task<bool> CheckPasswordSignInAsync(string email, string password, CancellationToken cancellationToken)
    {
        return await _identityService.CheckPasswordSignInAsync(email, password, cancellationToken);
    }
    private Task<bool> CheckIfEmailVerifiedAsync(string email,CancellationToken cancellationToken){
        return _identityService.IsEmailExistsAsync(email, cancellationToken);
    }

}

}