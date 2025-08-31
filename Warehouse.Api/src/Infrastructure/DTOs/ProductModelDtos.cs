namespace Warehouse.Api.src.Infrastructure.DTOs;

public record struct AddProductModelDto(string Name);
public record struct GetProductModelDto(int Id, string Name);