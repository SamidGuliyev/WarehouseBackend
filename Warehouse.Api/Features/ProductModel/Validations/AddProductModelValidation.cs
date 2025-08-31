using FluentValidation;
using Warehouse.Api.src.Infrastructure.DTOs;

namespace Warehouse.Api.Features.ProductModel.Validations;

public sealed class AddProductModelValidation : AbstractValidator<AddProductModelDto>
{
    public AddProductModelValidation()
    {
        RuleFor(p => p.Name).NotEmpty().MinimumLength(2);
    }
}