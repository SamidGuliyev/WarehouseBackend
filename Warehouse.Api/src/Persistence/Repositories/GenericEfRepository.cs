using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Warehouse.Api.src.Domain.Entities;

namespace Warehouse.Api.src.Persistence.Repositories;

public class GenericEfRepository<TEntity, TKey>(DbContext db) : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey> where TKey : struct
{
    private DbSet<TEntity> Table => db.Set<TEntity>();
    public void Add(TEntity entity)
    {
        Table.Add(entity);
    }

    public void Delete(TKey id)
    {
        var objDeletable = Table.Find(id) ?? throw new NotImplementedException();
        Table.Remove(objDeletable);
    }

    public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>>? predicate = null)
    {
        return predicate is not null ? Table.Where(predicate) : Table;
    }
    
    public async Task<TEntity?> Get(Expression<Func<TEntity, bool>>? predicate = null)
    {
        return await GetAll(predicate).FirstOrDefaultAsync();
    }

    public TEntity? GetById(TKey id)
    {
        return Table.Find(id) ?? null;
    }


    public void SoftDelete(TEntity entity)
    {
        entity.IsDeleted = true;
    }

    public async Task Update(TEntity entity)
    {
        var id = entity.GetType().GetProperties().FirstOrDefault(p => p.Name == "Id")?.GetValue(entity);

        if (id is not null)
        {
            var foundEntity = await Table.FindAsync(id);
            if (foundEntity is not null)
            {
                await Task.Run(() =>
                {
                    var entry = db.Entry(foundEntity);
                    var foreignKeys = db.Model.FindEntityType(typeof(TEntity))?.GetForeignKeyProperties().Select(p => p.Name) ?? [];
                    foreach (var property in typeof(TEntity).GetProperties())
                    {
                        bool isForeignKey = foreignKeys.Contains(property.Name);

                        if (property.Name is "Id" or "CreatedAt"
                        || isForeignKey
                        || (!property.PropertyType.IsValueType && property.PropertyType != typeof(string))
                        || Equals(property.GetValue(entity), entry.Property(property.Name).CurrentValue)
                        || (property.GetValue(entity) is string str && str.Trim().Equals(string.Empty))
                        ) continue;

                        entry.Property(property.Name).CurrentValue = property.GetValue(entity);
                    }
                });
            }
        }
    }
}
