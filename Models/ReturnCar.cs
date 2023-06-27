using System;
using System.Collections.Generic;

namespace TheRideYouRentST10095103_1.Models;

public partial class ReturnCar
{
    public int ReturnNo { get; set; }

    public int DriverId { get; set; }

    public string InspectorNo { get; set; } = null!;

    public string CarNo { get; set; } = null!;

    public int RentalNo { get; set; }

    public DateTime ReturnDate { get; set; }

    public int? ElapsedDate { get; set; }

    public int? Fine { get; set; }

    public virtual Car CarNumber { get; set; } = null!;

    public virtual Driver Driver { get; set; } = null!;

    public virtual Inspector InspectorNumber { get; set; } = null!;

    public virtual Rental RentalNumber { get; set; } = null!;
    public static int? Penalty(Rental rental, ReturnCar returncar)
    {

        var dates = new List<DateTime>();

        for (var dt = rental.EndDate; dt <= returncar.ReturnDate.Date; dt = dt.AddDays(1))
        {
            dates.Add(dt);
        }
        int? newFine = (returncar.Fine = 500 * dates.Count);
        return newFine;
    }

    internal static int? Penalty(DateTime endDate, DateTime returnDate)
    {
        throw new NotImplementedException();
    }
}

