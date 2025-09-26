using Academy.Products.Domain.Shared;
using MediatR;

namespace Academy.Products.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
    
}
