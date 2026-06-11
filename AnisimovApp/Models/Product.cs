namespace AnisimovApp.Models;

public class Product
{
    public int ProductId { get; set; }

    public int CategoryId { get; set; }

    public int ManufacturerId { get; set; }

    public int SupplierId { get; set; }

    public string ProductName { get; set; } = "";

    public string Article { get; set; } = "";

    public string Description { get; set; } = "";

    public decimal Amount { get; set; }

    public decimal Discount { get; set; }

    public decimal Count { get; set; }

    private string? photo;

    public string? Photo
    {
        get
        {
            if (string.IsNullOrWhiteSpace(photo))
                return null;

            return $"http://localhost:5282/images/{photo}";
        }
        set
        {
            photo = value;
        }
    }

    public string UnitOfMeasurement { get; set; } = "шт.";

    public string Category { get; set; } = "";

    public string Manufacturer { get; set; } = "";

    public string Supplier { get; set; } = "";
    public bool IsHighDiscount => Discount > 15;
    public bool IsOutOfStock => Count == 0;
    public bool HasDiscount => Discount > 0;
    public decimal FinalPrice
    {
        get
        {
            return Amount - (Amount * Discount / 100);
        }
    }
}
