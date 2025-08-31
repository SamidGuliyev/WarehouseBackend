namespace Warehouse.Api.src.Infrastructure.DTOs;

public record struct AddProductColorDto(string Name);
public record struct GetProductColorDto(int Id, string Name);
