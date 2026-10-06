using MediatR;
using Academy.Products.Domain.Shared;

namespace Academy.Products.Application.Products.Commands.UpdateProduct;

public record UpdateProductCommand(int ProductId, UpdateProductCommandRequest Request) : IRequest<Result<bool>>;
