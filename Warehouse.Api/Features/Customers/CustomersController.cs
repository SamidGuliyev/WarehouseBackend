using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Warehouse.Api.src.Infrastructure.DTOs;
using Warehouse.Api.src.Infrastructure.Services.Customers;

namespace Warehouse.Api.Features.Customers;

[Route("api/customers"), ApiController, Authorize]
public sealed class CustomersController(ICustomerService customerService, IValidator<AddCustomerDto> validator) : ControllerBase
{
    [HttpGet]
    public IActionResult GetAllCustomers([FromQuery] string? filter)
    {
        return Ok(customerService.GetAllCustomers(filter));
    }
    
    [HttpPost("search")]
    public IActionResult GetCustomer(GetCustomerRequest request)
    {
        return Ok(customerService.GetCustomers(request.Name));
    }
    
    [HttpPost("add")]
    public async Task<IActionResult> Insert(AddCustomerDto dto)
    {
        var validation = await validator.ValidateAsync(dto);
        if (!validation.IsValid) return BadRequest(validation.Errors);
        
        await customerService.AddCustomer(dto);
        return Created();
    }
    
    [HttpDelete("delete/{id:int}")]
    public IActionResult SoftDelete(int id)
    {
        customerService.SoftDelete(id);
        return NoContent();
    }
}