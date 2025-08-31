using FluentValidation;
using Warehouse.Api.src.Infrastructure.DTOs;

namespace Warehouse.Api.Features.Auth.Validations;

public sealed class ChangePasswordValidation : AbstractValidator<ChangePasswordDto>
{
    public ChangePasswordValidation()
    {
        RuleFor(x => x.Email)
            .NotNull().WithMessage("Email is required")
            .NotEmpty().WithMessage("Email cannot be empty")
            .EmailAddress().WithMessage("Email is not valid");
        RuleFor(x => x.OldPassword)
            .NotNull().WithMessage("Old password is required")
            .NotEmpty().WithMessage("Old password cannot be empty")
            .MinimumLength(6).WithMessage("Old password must be at least 6 characters");
        RuleFor(x => x.NewPassword)
            .NotNull().WithMessage("New password is required")
            .NotEmpty().WithMessage("New password cannot be empty")
            .MinimumLength(6).WithMessage("New password must be at least 6 characters");
    }
}