using Academy.Products.Application.Products.Commands.CreateProduct;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Academy.Products.Presentation.Modules;

    public static class ProductModules
    {
        private const string BASE_URL = "api/v1/createProduct/";
        public static void AddProductModules(this IEndpointRouteBuilder app)
        {
            var customerGroup = app.MapGroup(BASE_URL);

            customerGroup.MapPost("", CreateCustomer);
        }

        private static async Task<IResult> CreateCustomer(
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
