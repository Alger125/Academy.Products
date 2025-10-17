
using Academy.Products.Application.Abstractions.Messaging;
using Academy.Products.Application.Products.Response;
using Academy.Products.Domain.Entities.ProductEntity.Models;
using Academy.Products.Domain.Entities.ProductEntity.Repositories;
using Academy.Products.Domain.Shared;

namespace Academy.Products.Application.Products.Queries;

/*
public class GetProductsDetailsHandler(IProductRepository productRepository) : IQueryHandler<GetProductsDetailsQuery, GetProductsDetailQueryResponse>
{
    private readonly IProductRepository _productRepository = productRepository;
    public Task<Result<GetProductsDetailQueryResponse>> Handle(GetProductsDetailsQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
*/

public class GetProductsDetailsHandler : IQueryHandler<GetProductsDetailsQuery, GetProductsDetailQueryResponse>
{
    private readonly IProductRepository _productRepository;

    public GetProductsDetailsHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Result<GetProductsDetailQueryResponse>> Handle(GetProductsDetailsQuery request, CancellationToken cancellationToken)
    {   
        // Validar entrada
        if (request.productId <= 0)
        {
            return Result<GetProductsDetailQueryResponse>.Failure("Invalid product ID");
        }

        // Usar objeto tipado (estándar de código)
        GetProductsDetailModel product = await _productRepository.GetProductsDetails(request.productId);
        if (product == null)
        {
            return Result<GetProductsDetailQueryResponse>.Failure("Product not found");
        }

        var response = new GetProductsDetailQueryResponse
        {
            productId = product.productId,
            name = product.name,
            description = product.description,
            price = product.price,
            category = product.category,
            imageURL = product.imageURL
        };

        return Result<GetProductsDetailQueryResponse>.Success(response);
    }
}

