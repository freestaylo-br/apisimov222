using System;
using System.Collections.Generic;

namespace apisimov222.Models;

public partial class ProductName
{
    public int ProductNameId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
