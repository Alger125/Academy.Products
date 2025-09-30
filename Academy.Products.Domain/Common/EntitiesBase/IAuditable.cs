namespace Academy.Products.Domain.Common.EntitiesBase;

public interface IAuditable
{
    public DateTime CreationDate { get; set; }

    public string CreatedBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? UpdatedBy { get; set; }
}
