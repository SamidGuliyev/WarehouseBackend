using FluentValidation;
using Warehouse.Api.src.Infrastructure.DTOs;

namespace Warehouse.Api.Features.ProductColor.Validations;

public class AddProductColorValidation : AbstractValidator<AddProductColorDto>
{
    public AddProductColorValidation()
    {
        RuleFor(p => p.Name).NotEmpty().MinimumLength(2);
    }
}