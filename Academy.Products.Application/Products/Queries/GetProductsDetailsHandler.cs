
using Academy.Products.Application.Abstractions.Messaging;
using Academy.Products.Application.Products.Response;
using Academy.Products.Domain.Entities.ProductEntity.Repositories;
using Academy.Products.Domain.Shared;

namespace Academy.Products.Application.Products.Queries;

public class GetProductsDetailsHandler(IProductRepository productRepository) : IQueryHandler<GetProductsDetailsQuery, GetProductsDetailQueryResponse>
{
    private readonly IProductRepository _productRepository = productRepository;
    public Task<Result<GetProductsDetailQueryResponse>> Handle(GetProductsDetailsQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

