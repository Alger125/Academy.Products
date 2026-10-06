using Academy.Products.Domain.Common.IdTypes;
using Academy.Products.Domain.Entities.ProductEntity;
using Academy.Products.Domain.Entities.ProductEntity.Models;
using Academy.Products.Domain.Entities.ProductEntity.Repositories;
using Academy.Products.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Academy.Products.Infrastructure.Persistence.Repositories;

public class ProductRepository(ApplicationDbContext context) : IProductRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<GetProductsDetailModel> GetProductsDetails(int productId)
    {
        IntEntityId product = new(productId);
        return await _context.Products
            .Where(p => p.Id == product)
            .Select(p => new GetProductsDetailModel
            {
                productId = p.Id.Value,
                name = p.Name,
                description = p.description,
                price = p.price,
                category = p.Category,
                imageURL = p.imageUrl
            })
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<GetProductsDetailModel>> SearchAsync(string term, decimal? minPrice, decimal? maxPrice)
    {
        var query = _context.Products.AsQueryable();

        if (!string.IsNullOrWhiteSpace(term))
        {
            query = query.Where(p => p.Name.Contains(term) || p.Category.Contains(term));
        }

        if (minPrice.HasValue)
        {
            query = query.Where(p => p.price >= minPrice.Value);
        }

        if (maxPrice.HasValue)
        {
            query = query.Where(p => p.price <= maxPrice.Value);
        }

        return await query.Select(p => new GetProductsDetailModel
        {
            productId = p.Id.Value,
            name = p.Name,
            description = p.description,
            price = p.price,
            category = p.Category,
            imageURL = p.imageUrl
        }).ToListAsync();
    }

    public async Task<int> AddAsync(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return product.Id.Value;
    }

    public async Task<bool> UpdateAsync(Product product)
    {
        _context.Products.Update(product);
        var result = await _context.SaveChangesAsync();
        return result > 0;
    }

    public async Task<bool> DeleteAsync(int productId)
    {
        var product = await _context.Products.FindAsync(new IntEntityId(productId));
        if (product == null) return false;

        _context.Products.Remove(product);
        var result = await _context.SaveChangesAsync();
        return result > 0;
    }

    public async Task<Product> GetByIdAsync(int productId)
    {
        return await _context.Products.FindAsync(new IntEntityId(productId));
    }
}
