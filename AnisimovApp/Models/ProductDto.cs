using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnisimovApp.Models;

public class ProductDto
{
    public string ProductName { get; set; } = "";

    public string Description { get; set; } = "";

    public decimal Amount { get; set; }

    public decimal Discount { get; set; }

    public decimal Count { get; set; }

    public string UnitOfMeasurement { get; set; } = "";

    public string Article { get; set; } = "";

    public int CategoryId { get; set; }

    public int ManufacturerId { get; set; }

    public int SupplierId { get; set; }
    public string? Photo { get; set; }
}
