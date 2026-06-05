using System;
using System.Collections.Generic;

namespace apisimov222.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public DateOnly OrderDate { get; set; }

    public DateOnly DeliveryDate { get; set; }

    public int ClientId { get; set; }

    public int Code { get; set; }

    public int StatusId { get; set; }

    public int PickupLocationId { get; set; }

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual Client Client { get; set; } = null!;

    public virtual PickupLocation PickupLocation { get; set; } = null!;

    public virtual Status Status { get; set; } = null!;
}
