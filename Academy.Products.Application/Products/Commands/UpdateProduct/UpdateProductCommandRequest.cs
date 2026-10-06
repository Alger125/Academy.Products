namespace Academy.Products.Application.Products.Commands.UpdateProduct;

public class UpdateProductCommandRequest
{
    public string productName { get; set; } = string.Empty;
    public string productCategory { get; set; } = string.Empty;
    public decimal productPrice { get; set; }
    public string description { get; set; } = string.Empty;
    public string imageUrl { get; set; } = string.Empty;
}
