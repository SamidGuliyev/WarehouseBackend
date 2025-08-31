using Microsoft.EntityFrameworkCore;
using Warehouse.Api.src.Domain.Entities;
using Warehouse.Api.src.Infrastructure.DTOs;
using Warehouse.Api.src.Persistence.UnitOfWork;

namespace Warehouse.Api.src.Infrastructure.Services.Customers;

public sealed class CustomerService(IUnitOfWork unitOfWork) : ICustomerService
{
    public async Task AddCustomer(AddCustomerDto dto)
    {
        var customer = new Customer
        {
            Name = dto.Name,
            Phone = dto.Phone,
            CreatedAt = DateTime.UtcNow,
        };
        try
        {
            unitOfWork.CustomerRepository.Add(customer);
            await unitOfWork.SaveAsync();
        }
        catch (DbUpdateException e)
        {
            throw new DbUpdateException(e.Message);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public void SoftDelete(int id)
    {
        var customer = unitOfWork.CustomerRepository.GetById(id);
        if (customer == null) throw new NullReferenceException("Customer not found");
        unitOfWork.CustomerRepository.SoftDelete(customer);
        unitOfWork.Save();
    }

    public IEnumerable<GetCustomerDto> GetAllCustomers(string? filter = null)
    {
        IEnumerable<Customer> customers = unitOfWork.CustomerRepository.GetAll(c => !c.IsDeleted);
        switch (filter)
        {
            case "all":
                customers = unitOfWork.CustomerRepository.GetAll();
                break;
            case "is_deleted":
                customers = unitOfWork.CustomerRepository.GetAll(c => c.IsDeleted);
                break;
        }
        return 
            customers.Select(c => new GetCustomerDto(c.Id, c.Name, c.Phone ?? string.Empty));
    }

    public IEnumerable<GetCustomerDto> GetCustomers(string name)
    {
        return unitOfWork.CustomerRepository.GetAll(x => x.Name.ToLower().StartsWith(name.ToLower()) && x.IsDeleted == false)
            .Select(c => new GetCustomerDto(c.Id, c.Name, c.Phone ?? string.Empty));

    }
}