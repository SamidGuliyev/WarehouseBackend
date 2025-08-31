using Warehouse.Api.src.Domain.Entities;

namespace Warehouse.Api.src.Persistence.Repositories.Auth;

public interface IAuthRepository : IGenericRepository<User, Guid>
{
    
}