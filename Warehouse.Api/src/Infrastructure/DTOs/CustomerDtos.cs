namespace Warehouse.Api.src.Infrastructure.DTOs;

public record struct AddCustomerDto(string Name, string? Phone);
public record struct GetCustomerDto(int Id, string Name, string Phone);

public record struct GetCustomerRequest(string Name);
