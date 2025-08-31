using FluentValidation;
using Warehouse.Api.src.Infrastructure.DTOs;

namespace Warehouse.Api.Features.Customers.Validations;

public sealed class AddCustomerValidation : AbstractValidator<AddCustomerDto>
{
    public AddCustomerValidation()
    {
        RuleFor(c => c.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(c => c.Phone).MinimumLength(8);
    }
}