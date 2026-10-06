using Academy.Products.Domain.Entities.ProductEntity.Repositories;
using Academy.Products.Domain.Shared;
using MediatR;

namespace Academy.Products.Application.Products.Commands.DeleteProduct;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Result<bool>>
{
    private readonly IProductRepository _productRepository;

    public DeleteProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Result<bool>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var success = await _productRepository.DeleteAsync(request.ProductId);
        if (!success)
        {
            return Result<bool>.Failure("Product not found");
        }

        return Result<bool>.Success(true);
    }
}
