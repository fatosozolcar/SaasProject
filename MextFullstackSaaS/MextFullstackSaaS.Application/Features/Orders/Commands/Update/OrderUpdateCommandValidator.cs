using FluentValidation;
using MextFullstackSaaS.Application.Common.Interfaces;
using MextFullstackSaaS.Domain.Enums;

namespace MextFullstackSaaS.Application.Features.Orders.Commands.Update
{
    public class OrderUpdateCommandValidator : AbstractValidator<OrderUpdateCommand>
    {
        private readonly ICurrentUserService _currentUserService;

        public OrderUpdateCommandValidator(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;

            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("The order ID is required.");

            RuleFor(x => x.IconDescription)
                .NotEmpty()
                .MaximumLength(200)
                .WithMessage("The icon description must be less than 200 characters.");

            RuleFor(x => x.ColourCode)
                .NotEmpty()
                .MaximumLength(15)
                .WithMessage("The colour code must be less than 15 characters.");

            RuleFor(x => x.Model)
                .IsInEnum()
                .WithMessage("Please select a valid model.");

            RuleFor(x => x.DesignType)
                .IsInEnum()
                .WithMessage("Please select a valid design type.");

            RuleFor(x => x.Size)
                .IsInEnum()
                .WithMessage("Please select a valid size.");

            RuleFor(x => x.Shape)
                .IsInEnum()
                .WithMessage("Please select a valid shape.");

            RuleFor(x => x.Quantity)
                .GreaterThan(0)
                .LessThanOrEqualTo(6)
                .WithMessage("Please select a valid quantity.");

            RuleFor(x => x.Id)
                .Must(IsUserIdValid)
                .WithMessage("You need to be logged-in to update an order.");
        }

        private bool IsUserIdValid(Guid id) => _currentUserService.UserId != Guid.Empty;
    }
}
