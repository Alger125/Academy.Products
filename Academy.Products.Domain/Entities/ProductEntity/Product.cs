using Academy.Products.Domain.Common.EntitiesBase;
using Academy.Products.Domain.Common.IdTypes;


namespace Academy.Products.Domain.Entities.ProductEntity;

public class Product : EntityBase<IntEntityId>, IAuditable
{  
    // Ya hereda la propiedad ID
    // public int productId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string description { get; set; } = string.Empty;
    public decimal price { get; set; }
    public int stock { get; set; }
    public string imageUrl { get; set; } = string.Empty;

}

