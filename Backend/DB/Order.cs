using System;
using System.Collections.Generic;

namespace Backend.DB;

public partial class Order
{
    public int Id { get; set; }

    public int Productid { get; set; }

    public int Clientid { get; set; }

    public DateOnly Date { get; set; }

    public int Amount { get; set; }

    public decimal Pricebyone { get; set; }

    public decimal Allprice { get; set; }

    public virtual Client Client { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
