using Academy.Products.Domain.Common.IdTypes;
using Academy.Products.Domain.Entities.ProductEntity;
using Academy.Products.Domain.Entities.ProductEntity.Models;
using Academy.Products.Domain.Entities.ProductEntity.Repositories;
using Academy.Products.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Academy.Products.Infrastructure.Persistence.Repositories;

// public class ProductRepository(ApplicationDbContext) : IProductRepository
public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _context;

    public ProductRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<GetProductsDetailModel> GetProductsDetails(int productId)
    {
        IntEntityId product = new IntEntityId(productId);
        return await _context.Products
            .Where(p => p.Id == product)
            .Select(p => new GetProductsDetailModel
            {
                productId = p.Id.Value,
                name = p.name,
                description = p.description,
                price = p.price,
                imageURL = p.imageUrl
            })
            .FirstAsync();
    } 

}
