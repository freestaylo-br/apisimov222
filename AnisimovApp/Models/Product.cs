namespace AnisimovApp.Models;

public class Product
{
    public int ProductId { get; set; }

    public string ProductName { get; set; } = "";

    public string Article { get; set; } = "";

    public string Description { get; set; } = "";

    public decimal Amount { get; set; }

    public decimal Discount { get; set; }

    public decimal Count { get; set; }

    public string Photo { get; set; } = "";

    public string UnitOfMeasurement { get; set; } = "";

    public string Category { get; set; } = "";

    public string Manufacturer { get; set; } = "";

    public string Supplier { get; set; } = "";
    public bool IsHighDiscount => Discount > 15;
    public bool IsOutOfStock => Count == 0;
    public bool HasDiscount => Discount > 0;
    public decimal FinalPrice => Amount - (Amount * Discount / 100);
}