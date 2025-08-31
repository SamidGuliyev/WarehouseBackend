using Microsoft.EntityFrameworkCore;
using Warehouse.Api.src.Domain;
using Warehouse.Api.src.Domain.Entities;

namespace Warehouse.Api.src.Persistence.Repositories.Auth;

public sealed class AuthEfRepository(BaseDbContext db) : GenericEfRepository<User, Guid>(db), IAuthRepository
{
    
}