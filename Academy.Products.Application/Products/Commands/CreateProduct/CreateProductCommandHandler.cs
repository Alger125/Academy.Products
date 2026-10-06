using Academy.Products.Domain.Entities.ProductEntity;
using Academy.Products.Domain.Entities.ProductEntity.Repositories;
using Academy.Products.Domain.Shared;
using MediatR;

namespace Academy.Products.Application.Products.Commands.CreateProduct;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result<CreateProductCommandResponse>>
{
    private readonly IProductRepository _productRepository;

    public CreateProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Result<CreateProductCommandResponse>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Name = request.request.productName,
            Category = request.request.productCategory,
            price = request.request.productPrice,
            description = "Descripción pendiente", // Default or from request if added
            stock = 10,
            imageUrl = ""
        };

        var newId = await _productRepository.AddAsync(product);

        var response = new CreateProductCommandResponse
        {
            productId = newId
        };

        return Result<CreateProductCommandResponse>.Success(response);
    }
}
