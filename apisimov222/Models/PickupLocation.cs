using System;
using System.Collections.Generic;

namespace apisimov222.Models;

public partial class PickupLocation
{
    public int PickupLocationId { get; set; }

    public int Index { get; set; }

    public string City { get; set; } = null!;

    public string Street { get; set; } = null!;

    public string House { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
