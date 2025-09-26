using Academy.Products.Application.Products.Commands.CreateProduct;
using Academy.Products.Application.Products.Queries;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Academy.Products.Presentation.Modules;

    public static class ProductModules
    {
        private const string BASE_URL = "api/v1/products/";
    public static void AddProductModules(this IEndpointRouteBuilder app)
    {
        var customerGroup = app.MapGroup(BASE_URL);

        // Endpoint para crear un nuevo producto
        customerGroup.MapPost("", CreateProduct);
        customerGroup.MapGet("{productId:int}", GetProductsDetails);

        }

    private static async Task<IResult> GetProductsDetails([FromRoute] int productId,
            ISender sender,
            CancellationToken cancellationToken)
    {
        GetProductsDetailsQuery query = new GetProductsDetailsQuery(productId);
        var result = await sender.Send(query, cancellationToken);

        if (!result.IsSuccess)
        {
            return Results.NotFound(result.Error);
        }

        return Results.Ok(result.Value);
    }

    private static async Task<IResult> CreateProduct(
            [FromBody] CreateProductCommandRequest request,
            ISender sender,
            CancellationToken cancellationToken)
        {
            var command = new CreateProductCommand(request);
            var result = await sender.Send(command, cancellationToken);

            if (result.Value == null)
                return Results.Content("Unable to create cart");

            return Results.Created($"{BASE_URL}{result.Value.productId}", result.Value);
        }    
    }
