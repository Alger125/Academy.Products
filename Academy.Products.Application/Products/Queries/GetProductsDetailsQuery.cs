using Academy.Products.Application.Abstractions.Messaging;
using Academy.Products.Application.Products.Response;
using Academy.Products.Domain.Shared;
using MediatR;

namespace Academy.Products.Application.Products.Queries
{
    // sealed record -> no se puede heredar de esta clase
    // record -> inmutabilidad, igualdad por valor, sintaxis concisa para definir tipos

    // se define un query para obtener detalles de productos, retornando un resultado que puede ser exitoso o fallido
    public sealed record GetProductsDetailsQuery(int productId) : IQuery<GetProductsDetailQueryResponse>;
}