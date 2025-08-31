using FluentValidation;
using Warehouse.Api.src.Infrastructure.DTOs;

namespace Warehouse.Api.Features.Product.Validations;

public sealed class UpdateProductValidation : AbstractValidator<UpdateProductDto>
{
    public UpdateProductValidation()
    {
        RuleFor(p => p.Size).NotEmpty().MinimumLength(2);
        RuleFor(p => p.BlockNumber).GreaterThan(0).WithMessage("BlockNumber must be greater than 0");
        RuleFor(p => p.PieceNumber).GreaterThan(0).WithMessage("PieceNumber must be greater than 0");
        RuleFor(p => p.Stock).GreaterThanOrEqualTo(0);
        RuleFor(p => p.Thumbnail)
            .Must(f => Path.GetExtension(f.FileName) is ".jpg" or ".png")
            .WithMessage("You can upload only .jpg or .png files")
            .Must(f => f.ContentType is "image/jpeg" or "image/jpg" or "image/png")
            .WithMessage("Only JPEG or PNG image formats are allowed")
            .Must(f => f.Length < 3 * 1024 * 1024)
            .WithMessage("File size must be less than 3MB")
            .When(f => f.Thumbnail is not null);
    }
}