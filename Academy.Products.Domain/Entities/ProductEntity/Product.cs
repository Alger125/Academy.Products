using Academy.Products.Domain.Common.EntitiesBase;
using Academy.Products.Domain.Common.IdTypes;


namespace Academy.Products.Domain.Entities.ProductEntity;

public class Product : EntityBase<IntEntityId>, IAuditable
{  
    // Ya hereda la propiedad ID
    // public int productId { get; set; }
    public string name { get; set; }
    public string description { get; set; } 
    public decimal price { get; set; }
    public bool stock { get; set; }
    public string category { get; set; } 
    public string imageURL { get; set; }

}

