using System;
using System.Collections.Generic;

namespace TheRideYouRentST10095103_1.Models;

public partial class Inspector
{
    public string InspectorNo { get; set; } = null!;

    public string InspectorName { get; set; } = null!;

    public string InspectorEmail { get; set; } = null!;

    public string InspectorMobile { get; set; } = null!;

    public virtual ICollection<Rental> Rentals { get; set; } = new List<Rental>();

    public virtual ICollection<ReturnCar> ReturnCars { get; set; } = new List<ReturnCar>();
}
