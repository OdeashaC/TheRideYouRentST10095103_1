using System;
using System.Collections.Generic;

namespace TheRideYouRentST10095103_1.Models;

public partial class CarBodyType
{
    public int CarBodyTypeId { get; set; }

    public string CarBodyTypeDescription { get; set; } = null!;

    public virtual ICollection<Car> Cars { get; set; } = new List<Car>();
}
