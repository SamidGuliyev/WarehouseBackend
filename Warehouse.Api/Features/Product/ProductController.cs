using System.Security.Claims;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Warehouse.Api.src.Infrastructure.DTOs;
using Warehouse.Api.src.Infrastructure.Providers.Services;
using Warehouse.Api.src.Infrastructure.Services.Products;

namespace Warehouse.Api.Features.Product
{
    [Route("api/product"), ApiController, Authorize]

    public sealed class ProductController(
    IProductService productService,
    TokenService tokenService,
    IValidator<AddProductDto> validator,
    IValidator<UpdateProductDto> updateValidator
    ) : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllProducts([FromQuery] string? filter)
        {
            return Ok(productService.GetAllProducts(filter));
        }
        
        [HttpGet("group/{name}")]
        public IActionResult GetGroupByName(string name)
        {
            return Ok(productService.GetAllProductsByName(name));
        }

        [HttpPost("image-upload")]
        public async Task<IActionResult> UploadImage([FromForm] IFormFile file)
        {
            if ((file.ContentType is not ("image/jpeg" or "image/jpg" or "image/png")) ||
                file.Length >= 3 * 1024 * 1024) return BadRequest("Invalid file type or file length");
            var tempPath = await productService.UploadProductImage(file);
            return Ok(new{ Thumbnail = tempPath });
        }


        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            return Ok(productService.GetProductById(id));
        }

        [HttpGet("join")]
        public IActionResult GetJoinProducts([FromQuery] string? filter)
        {
            return Ok(productService.GetProductJoin(filter));
        }

        [HttpPost("add")]
        public async Task<IActionResult> Insert(AddProductDto dto)
        {
            var validation = await validator.ValidateAsync(dto);
            if (!validation.IsValid) return BadRequest(validation.Errors);

            var id = tokenService.ParseJwtToken(Request.Headers.Authorization.FirstOrDefault()?.Split(" ")[1]!)!
                .FindFirstValue(ClaimTypes.PrimarySid);

            if (!Guid.TryParse(id, out var userId)) return BadRequest("Invalid user");
           
            await productService.AddProductAsync(dto, userId);
            return Created();
        }

        [HttpDelete("softdelete/{id:int}")]
        public IActionResult SoftDelete(int id)
        {
            productService.SoftDelete(id);
            return NoContent();
        }

        [HttpDelete("harddelete/{id:int}")]
        public IActionResult HardDelete(int id)
        {
            productService.HardDelete(id);
            return NoContent();
        }

        [HttpPatch("update/")]
        public async Task<IActionResult> UpdateProducts([FromForm] UpdateProductDto dto)
        {
            var validation = await updateValidator.ValidateAsync(dto);
            if (!validation.IsValid) return BadRequest(validation.Errors);
            
            await productService.UpdateProductAsync(dto);
            return Ok("Product was updated");
        }

    }
}
