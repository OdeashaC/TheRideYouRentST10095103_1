using System;
using System.Collections.Generic;

namespace TheRideYouRentST10095103_1.Models;

public partial class Car
{
    public string CarNo { get; set; } = null!;

    public int CarMakeId { get; set; }

    public int CarBodyTypeId { get; set; }

    public string CarModel { get; set; } = null!;

    public int KilometresTravelled { get; set; }

    public int ServiceKilometres { get; set; }

    public string Available { get; set; } = null!;

    public virtual CarBodyType CarBodyType { get; set; } = null!;

    public virtual CarMake CarMake { get; set; } = null!;

    public virtual ICollection<Rental> Rentals { get; set; } = new List<Rental>();

    public virtual ICollection<ReturnCar> ReturnCars { get; set; } = new List<ReturnCar>();
}
