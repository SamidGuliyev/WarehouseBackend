using FluentValidation;
using Warehouse.Api.src.Infrastructure.DTOs;

namespace Warehouse.Api.Features.Auth.Validations
{
    public sealed class RegistValidation : AbstractValidator<RegistrDto>
    {
        public RegistValidation()
        {
            RuleFor(p => p.Fullname).NotEmpty().MinimumLength(5);
            RuleFor(p => p.Email).NotEmpty().EmailAddress();
            RuleFor(p => p.Password).NotEmpty().MinimumLength(6);
        }
    }
}
