using Academy.Products.Application.Abstractions.Messaging;
using Academy.Products.Domain.Entities.ProductEntity.Models;

namespace Academy.Products.Application.Products.Queries.SearchProducts;

public record SearchProductsQuery(string Term, decimal? MinPrice, decimal? MaxPrice) : IQuery<IEnumerable<GetProductsDetailModel>>;
