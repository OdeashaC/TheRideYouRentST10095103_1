using System;
using System.Collections.Generic;

namespace TheRideYouRentST10095103_1.Models;

public partial class Driver
{
    public int DriverId { get; set; }

    public string DriverName { get; set; } = null!;

    public string DriverAddress { get; set; } = null!;

    public string DriverEmail { get; set; } = null!;

    public string DriverMobile { get; set; } = null!;

    public virtual ICollection<Rental> Rentals { get; set; } = new List<Rental>();

    public virtual ICollection<ReturnCar> ReturnCars { get; set; } = new List<ReturnCar>();
}
