using Academy.Products.Application.Abstractions.Messaging;
using Academy.Products.Domain.Entities.ProductEntity.Models;
using Academy.Products.Domain.Entities.ProductEntity.Repositories;
using Academy.Products.Domain.Shared;

namespace Academy.Products.Application.Products.Queries.SearchProducts;

public class SearchProductsQueryHandler : IQueryHandler<SearchProductsQuery, IEnumerable<GetProductsDetailModel>>
{
    private readonly IProductRepository _productRepository;

    public SearchProductsQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Result<IEnumerable<GetProductsDetailModel>>> Handle(SearchProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await _productRepository.SearchAsync(request.Term, request.MinPrice, request.MaxPrice);
        return Result<IEnumerable<GetProductsDetailModel>>.Success(products);
    }
}
