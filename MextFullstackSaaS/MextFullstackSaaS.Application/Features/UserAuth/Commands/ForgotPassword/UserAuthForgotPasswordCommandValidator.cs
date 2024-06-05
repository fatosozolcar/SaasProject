using FluentValidation;
using MextFullstackSaaS.Application.Common.FluentValidation.BaseValidators;
using MextFullstackSaaS.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace MextFullstackSaaS.Application.Features.UserAuth.Commands.ForgotPassword
{
    public class UserAuthForgotPasswordCommandValidator : UserAuthValidatorBase<UserAuthForgotPasswordCommand>
    {
        public UserAuthForgotPasswordCommandValidator(IIdentityService identityService) : base(identityService)
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Email is not valid.");

            RuleFor(x => x.Email)
                .MustAsync(CheckIfUserExists).WithMessage("User with this email does not exist.");
        }

        private async Task<bool> CheckIfUserExists(string email, CancellationToken cancellationToken)
        {
            return await _identityService.IsEmailExistsAsync(email, cancellationToken);
        }
    }
}