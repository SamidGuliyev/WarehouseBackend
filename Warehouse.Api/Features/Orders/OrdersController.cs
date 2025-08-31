using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Warehouse.Api.src.Infrastructure.DTOs;
using Warehouse.Api.src.Infrastructure.Services.Orders;

namespace Warehouse.Api.Features.Orders;

[Route("/api/orders"), ApiController, Authorize]
public sealed class OrdersController(IOrderService orderService, IValidator<AddOrderDto> validator) : ControllerBase
{
    [HttpPost("add")]
    public async Task<IActionResult> Insert(AddOrderDto dto)
    {
        var validation = await validator.ValidateAsync(dto);
        if (!validation.IsValid) return BadRequest(validation.Errors);
        
        var result = await orderService.AddOrder(dto);
        if (!result.Success) return BadRequest(result.Errors);
        return Created();
    }

    [HttpPatch("{id}/complete")]
    public async Task<IActionResult> CompleteOrder(long id)
    {
        if (id <= 0) return BadRequest("Invalid id");
        await orderService.CompleteOrder(id);
        return NoContent();
    }
    
    [HttpDelete("softdelete/{id}")]
    public IActionResult SoftDelete(long id)
    {
        orderService.Delete(id);
        return NoContent();
    }

    [HttpGet]
    public IActionResult GetOrders([FromQuery] string? filter = null)
    {
        var result =  orderService.GetOrders(filter); 
        return Ok(result); 
    }
}
