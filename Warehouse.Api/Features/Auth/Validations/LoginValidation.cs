using FluentValidation;
using Warehouse.Api.src.Infrastructure.DTOs;

namespace Warehouse.Api.Features.Auth.Validations
{
    public sealed class LoginValidation : AbstractValidator<LoginDto>
    {
        public LoginValidation()
        {
            RuleFor(p => p.Email).NotEmpty().EmailAddress();
            RuleFor(p => p.Password).NotEmpty().MinimumLength(6);
        }
    }
}
