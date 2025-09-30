using Academy.Products.Domain.Entities.ProductEntity.Models;

namespace Academy.Products.Domain.Entities.ProductEntity.Repositories;

public interface IProductRepository
{
    Task<GetProductsDetailModel> GetProductsDetails(int productId);
  
}
