using Academy.Products.Domain.Common.IdTypes;
using Academy.Products.Domain.Entities.ProductEntity;
using Academy.Products.Domain.Entities.ProductEntity.Models;
using Academy.Products.Domain.Entities.ProductEntity.Repositories;
using Academy.Products.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Academy.Products.Infrastructure.Persistence.Repositories;

// public class ProductRepository(ApplicationDbContext) : IProductRepository
public class ProductRepository(ApplicationDbContextObjects context) : IProductRepository
{
    public async Task<GetProductsDetailModel> GetProductsDetails(int productId)
    {
        IntEntityId product = new IntEntityId(productId);
        return await (from p in context.Products
                      where p.Id == product
                        select new GetProductsDetailModel
                        {
                            name = p.name,
                            description = p.description,
                            price = p.price,
                            category = p.category,
                            imageURL = p.imageURL
                        }).FirstAsync();
    } 

}
