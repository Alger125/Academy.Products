
namespace Academy.Products.Domain.Common.EntitiesBase;
public abstract class EntityBase<TEntityId> : Entity<TEntityId>
{
    public DateTime CreationDate { get; set; }

    public string CreatedBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? UpdatedBy { get; set; }
    public bool Show { get; set; }
    public bool Active { get; set; } 
    
}

