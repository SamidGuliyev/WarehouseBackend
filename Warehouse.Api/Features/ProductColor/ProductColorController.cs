using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Warehouse.Api.src.Infrastructure.DTOs;
using Warehouse.Api.src.Infrastructure.Services.ProductColors;

namespace Warehouse.Api.Features.ProductColor;

[Route("/api/productcolor"), ApiController,Authorize]
public sealed class ProductColorController(IProductColorService productColorService, IValidator<AddProductColorDto> validator) : Controller
{
   [HttpPost("add")]
   public async Task<IActionResult> AddProductModel(AddProductColorDto dto)
   {
      var validation = await validator.ValidateAsync(dto);
      if (!validation.IsValid) return BadRequest(validation.Errors);

      await productColorService.AddProductColorAsync(dto);
      return Created();
   }
   
   [HttpGet]
   public IActionResult GetAllProductColors([FromQuery] string? filter)
   {
      return Ok(productColorService.GetAllProductColor(filter));
   }
   
   [HttpGet("{id:int}")]
   public IActionResult GetById(int id)
   {
      return Ok(productColorService.GetProductColorById(id));
   }

   [HttpDelete("{id:int}")]
   public IActionResult Delete(int id)
   {
      productColorService.DeleteProductColor(id);
      return NoContent();
   }
   
}