using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Warehouse.Api.src.Infrastructure.DTOs;
using Warehouse.Api.src.Infrastructure.Services.ProductModel;

namespace Warehouse.Api.Features.ProductModel;

[Route("/api/productmodel"), ApiController,Authorize]
public sealed class ProductModelController(IProductModelService productModelService, IValidator<AddProductModelDto> validator) : Controller
{
    [HttpPost("add")]
    public async Task<IActionResult> AddProductModel(AddProductModelDto dto)
    {
        var validation = await validator.ValidateAsync(dto);
        if (!validation.IsValid) return BadRequest(validation.Errors);

        await productModelService.AddProductModelAsync(dto);
        return Created();
    }
    
    [HttpGet]
    public IActionResult GetAllProductModels([FromQuery] string? filter)
    {
        return Ok(productModelService.GetAllProductModel(filter));
    }
   
    [HttpGet("{id:int}")]
    public IActionResult GetById(int id)
    {
        return Ok(productModelService.GetProductModelById(id));
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        productModelService.DeleteProductModel(id);
        return NoContent();
    }
}