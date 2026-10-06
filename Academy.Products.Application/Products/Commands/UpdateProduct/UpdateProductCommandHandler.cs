using Academy.Products.Domain.Entities.ProductEntity.Repositories;
using Academy.Products.Domain.Shared;
using MediatR;

namespace Academy.Products.Application.Products.Commands.UpdateProduct;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Result<bool>>
{
    private readonly IProductRepository _productRepository;

    public UpdateProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Result<bool>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.ProductId);
        if (product == null)
        {
            return Result<bool>.Failure("Product not found");
        }

        product.Name = request.Request.productName;
        product.Category = request.Request.productCategory;
        product.price = request.Request.productPrice;
        product.description = request.Request.description;
        product.imageUrl = request.Request.imageUrl;

        var success = await _productRepository.UpdateAsync(product);
        return success ? Result<bool>.Success(true) : Result<bool>.Failure("Failed to update product");
    }
}
