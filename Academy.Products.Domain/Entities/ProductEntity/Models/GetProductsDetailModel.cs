

namespace Academy.Products.Domain.Entities.ProductEntity.Models;
public class GetProductsDetailModel
{
    public int productId { get; init; }
    public string name { get; init; }
    public string description { get; init; } 
    public decimal price { get; init; }
    public string category { get; init; } 
    public string imageURL { get; init; }

}
