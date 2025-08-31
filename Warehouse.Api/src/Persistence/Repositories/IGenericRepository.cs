using System;
using System.Linq.Expressions;
using Warehouse.Api.src.Domain.Entities;

namespace Warehouse.Api.src.Persistence.Repositories;

public interface IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey> where TKey : struct
{
    IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>>? predicate = null);
    Task<TEntity?> Get(Expression<Func<TEntity, bool>>? predicate = null);
    TEntity? GetById(TKey id);
    void Add(TEntity entity);
    void SoftDelete(TEntity entity);
    Task Update(TEntity entity);
    void Delete(TKey id);
}
