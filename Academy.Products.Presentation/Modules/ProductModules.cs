using Academy.Products.Application.Products.Commands.CreateProduct;
using Academy.Products.Application.Products.Commands.UpdateProduct;
using Academy.Products.Application.Products.Commands.DeleteProduct;
using Academy.Products.Application.Products.Queries;
using Academy.Products.Application.Products.Queries.SearchProducts;
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

        // Search products (US 1 & 2)
        customerGroup.MapGet("search", SearchProducts);

        // Create product (US 3)
        customerGroup.MapPost("", CreateProduct);
        
        // Update product (US 3)
        customerGroup.MapPut("{productId:int}", UpdateProduct);
        
        // Delete product (US 3)
        customerGroup.MapDelete("{productId:int}", DeleteProduct);

        // Get product details (US 4)
        customerGroup.MapGet("{productId:int}", GetProductsDetails);
    }

    private static async Task<IResult> SearchProducts(
        [FromQuery] string? term, 
        [FromQuery] decimal? minPrice, 
        [FromQuery] decimal? maxPrice,
        ISender sender,
        CancellationToken cancellationToken)
    {
        SearchProductsQuery query = new(term ?? string.Empty, minPrice, maxPrice);
        var result = await sender.Send(query, cancellationToken);
        
        return Results.Ok(result.Value);
    }

    private static async Task<IResult> GetProductsDetails([FromRoute] int productId,
        ISender sender,
        CancellationToken cancellationToken)
    {
        GetProductsDetailsQuery query = new(productId);
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
            return Results.Content("Unable to create product");

        return Results.Created($"{BASE_URL}{result.Value.productId}", result.Value);
    }    
    
    private static async Task<IResult> UpdateProduct(
        [FromRoute] int productId,
        [FromBody] UpdateProductCommandRequest request,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var command = new UpdateProductCommand(productId, request);
        var result = await sender.Send(command, cancellationToken);

        if (!result.IsSuccess)
            return Results.NotFound(result.Error);

        return Results.NoContent();
    }
    
    private static async Task<IResult> DeleteProduct(
        [FromRoute] int productId,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var command = new DeleteProductCommand(productId);
        var result = await sender.Send(command, cancellationToken);

        if (!result.IsSuccess)
            return Results.NotFound(result.Error);

        return Results.NoContent();
    }
}
