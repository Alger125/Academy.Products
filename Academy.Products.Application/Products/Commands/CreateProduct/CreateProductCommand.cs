using Academy.Products.Domain.Shared;
using MediatR;

namespace Academy.Products.Application.Products.Commands.CreateProduct;

public sealed record CreateProductCommand(CreateProductCommandRequest Request) : IRequest<Result<CreateProductCommandResponse>>;