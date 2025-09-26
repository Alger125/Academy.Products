namespace Academy.Products.Application.Products.Response;

public class GetProductsDetailQueryResponse
{
    public int productId { get; set; }
    public string name { get; set; } = string.Empty;
    public string description { get; set; } = string.Empty;
    public decimal price { get; set; }
    public bool stock { get; set; }
    public string category { get; set; } = string.Empty;
    public string imageURL { get; set; } = string.Empty;
}
