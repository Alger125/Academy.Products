using Academy.Products.Domain.Entities.ProductEntity;
using Academy.Products.Domain.Entities.ProductEntity.Models;

namespace Academy.Products.Domain.Entities.ProductEntity.Repositories;

public interface IProductRepository
{
    Task<GetProductsDetailModel> GetProductsDetails(int productId);
    Task<IEnumerable<GetProductsDetailModel>> SearchAsync(string term, decimal? minPrice, decimal? maxPrice);
    Task<int> AddAsync(Product product);
    Task<bool> UpdateAsync(Product product);
    Task<bool> DeleteAsync(int productId);
    Task<Product> GetByIdAsync(int productId);
}
