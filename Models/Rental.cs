using System;
using System.Collections.Generic;

namespace TheRideYouRentST10095103_1.Models;

public partial class Rental
{
    public int RentalNo { get; set; }

    public int Driverid { get; set; }

    public string InspectorNo { get; set; } = null!;

    public string CarNo { get; set; } = null!;

    public int RentalFee { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public virtual Car CarNumber { get; set; } = null!;

    public virtual Driver Driver { get; set; } = null!;

    public virtual Inspector InspectorNumber { get; set; } = null!;

    public virtual ICollection<ReturnCar> ReturnCars { get; set; } = new List<ReturnCar>();
}
