using Warehouse.Api.src.Infrastructure.DTOs;

namespace Warehouse.Api.src.Infrastructure.Services.Customers;

public interface ICustomerService
{
    Task AddCustomer(AddCustomerDto dto);
    void SoftDelete(int id);
    IEnumerable<GetCustomerDto> GetAllCustomers(string? filter = null);
    IEnumerable<GetCustomerDto> GetCustomers(string name);
}