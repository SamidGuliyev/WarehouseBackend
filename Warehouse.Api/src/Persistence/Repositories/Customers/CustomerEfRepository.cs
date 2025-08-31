using Warehouse.Api.src.Domain;
using Warehouse.Api.src.Domain.Entities;

namespace Warehouse.Api.src.Persistence.Repositories.Customers;

public sealed class CustomerEfRepository(BaseDbContext db) : GenericEfRepository<Customer, int>(db), ICustomerRepository
{
    
}