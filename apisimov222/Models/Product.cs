using System;
using System.Collections.Generic;

namespace apisimov222.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public int ProductNameId { get; set; }

    public string UnitOfMeasurement { get; set; } = null!;

    public decimal Amount { get; set; }

    public int SupplierId { get; set; }

    public int ManufacturerId { get; set; }

    public int CategoryId { get; set; }

    public decimal Discount { get; set; }

    public decimal Count { get; set; }

    public string Description { get; set; } = null!;

    public string Photo { get; set; } = null!;

    public string Article { get; set; } = null!;

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual Category Category { get; set; } = null!;

    public virtual Manufacturer Manufacturer { get; set; } = null!;

    public virtual ProductName ProductName { get; set; } = null!;

    public virtual Supplier Supplier { get; set; } = null!;
}