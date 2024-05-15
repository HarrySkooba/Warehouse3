using System;
using System.Collections.Generic;

namespace Backend.DB;

public partial class Supply
{
    public int Id { get; set; }

    public int Productid { get; set; }

    public int Providerid { get; set; }

    public DateOnly Date { get; set; }

    public int Amount { get; set; }

    public decimal Pricebyone { get; set; }

    public decimal Allprice { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual Provider Provider { get; set; } = null!;
}
