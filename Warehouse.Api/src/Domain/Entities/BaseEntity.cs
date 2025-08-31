namespace Warehouse.Api.src.Domain.Entities;

public abstract class BaseEntity<TypeId> where TypeId : struct
{
    // [Key] // Birincil açar (Primary Key)
    // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public TypeId Id { get; set; } 
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public bool IsDeleted { get; set; }
}

public abstract class BaseUpdatedEntity<TypeId> : BaseEntity<TypeId> where TypeId : struct
{
    public required DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
