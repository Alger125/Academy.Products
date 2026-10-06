using MediatR;
using Academy.Products.Domain.Shared;

namespace Academy.Products.Application.Products.Commands.DeleteProduct;

public record DeleteProductCommand(int ProductId) : IRequest<Result<bool>>;
