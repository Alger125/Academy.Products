using Academy.Products.Domain.Shared;
using MediatR;

namespace Academy.Products.Application.Products.Commands.CreateProduct;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result<CreateProductCommandResponse>>
{
    public Task<Result<CreateProductCommandResponse>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
    
}
