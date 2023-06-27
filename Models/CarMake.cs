using System;
using System.Collections.Generic;

namespace TheRideYouRentST10095103_1.Models;

public partial class CarMake
{
    public int CarMakeId { get; set; }

    public string CarMakeDescription { get; set; } = null!;

    public virtual ICollection<Car> Cars { get; set; } = new List<Car>();
}
