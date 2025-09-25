
namespace Academy.Products.Application.Products.Commands.CreateProduct;

public class CreateProductCommandResponse
{
    public string productName { get; set; } = string.Empty;

    public string productCategory { get; set; } = string.Empty;

    public int productId { get; set; } 

    public decimal productPrice { get; set; } 
}
