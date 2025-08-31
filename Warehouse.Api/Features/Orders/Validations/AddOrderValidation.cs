using FluentValidation;
using Warehouse.Api.src.Domain.Entities;
using Warehouse.Api.src.Infrastructure.DTOs;

namespace Warehouse.Api.Features.Orders.Validations;

public class AddOrderValidation : AbstractValidator<AddOrderDto>
{
    public AddOrderValidation()
    {
        RuleFor(o => o.CustomerId).GreaterThan(0).WithMessage("Customer cannot be empty");
        RuleFor(x => x.ProductItems)    
            .ForEach(rule =>
            {
                rule.Must(item =>
                {
                    if (ProductUnit.Box != item.Unit && ProductUnit.Block != item.Unit) return false;
                    return item is not { Id: <= 0, Quantity: <= 0, Price: <= 0 };
                }).WithMessage("All item properties must be completed");
            });
    }
}
