using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TheRideYouRentST10095103_1.Models;

namespace TheRideYouRentST10095103_1.Controllers
{
    public class ReturnCarController : Controller
    {
        private readonly TheRideUrRentContext _context;

        public ReturnCarController(TheRideUrRentContext context)
        {
            _context = context;
        }

        // GET: ReturnCar
        public async Task<IActionResult> Index()
        {
            var theRideUrRentContext = _context.ReturnCars.Include(r => r.CarNumber).Include(r => r.Driver).Include(r => r.InspectorNumber).Include(r => r.RentalNumber);
            return View(await theRideUrRentContext.ToListAsync());
        }

        // GET: ReturnCar/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ReturnCars == null)
            {
                return NotFound();
            }

            var returnCar = await _context.ReturnCars
                .Include(r => r.CarNumber)
                .Include(r => r.Driver)
                .Include(r => r.InspectorNumber)
                .Include(r => r.RentalNumber)
                .FirstOrDefaultAsync(m => m.ReturnNo == id);
            if (returnCar == null)
            {
                return NotFound();
            }

            return View(returnCar);
        }

        // GET: ReturnCar/Create
        public IActionResult Create()
        {
            ViewData["CarNo"] = new SelectList(_context.Cars, "CarNo", "CarNo");
            ViewData["DriverId"] = new SelectList(_context.Drivers, "DriverId", "DriverId");
            ViewData["InspectorNo"] = new SelectList(_context.Inspectors, "InspectorNo", "InspectorNo");
            ViewData["RentalNo"] = new SelectList(_context.Rentals, "RentalNo", "RentalNo");
            return View();
        }

        // POST: ReturnCar/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReturnNo,DriverId,InspectorNo,CarNo,RentalNo,ReturnDate,ElapsedDate,Fine")] ReturnCar returnCar)
        {
            if (!ModelState.IsValid)
            {
                // Check if the rental exists based on RentalNo
                var rental = await _context.Rentals.FindAsync(returnCar.RentalNo);
                if (rental == null)
                {
                    ModelState.AddModelError("", "Invalid RentalNo. Please select a valid rental.");
                    ViewData["CarNo"] = new SelectList(_context.Cars, "CarNo", "CarNo", returnCar.CarNo);
                    return View(returnCar);
                }

                // Calculate penalty and assign values to ElapsedDate and Fine properties
                returnCar.ElapsedDate = (int)(returnCar.ReturnDate - rental.EndDate).TotalDays;
                returnCar.Fine = ReturnCar.Penalty(rental, returnCar);

                // If Fine is still null, initialize it to 0
                if (returnCar.Fine == null)
                {
                    returnCar.Fine = 0;
                }

                // Save the Rental object before creating the return
                _context.Rentals.Update(rental);
                await _context.SaveChangesAsync();

                _context.ReturnCars.Add(returnCar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CarNo"] = new SelectList(_context.Cars, "CarNo", "CarNo", returnCar.CarNo);
            ViewData["DriverId"] = new SelectList(_context.Drivers, "DriverId", "DriverId", returnCar.DriverId);
            ViewData["InspectorNo"] = new SelectList(_context.Inspectors, "InspectorNo", "InspectorNo", returnCar.InspectorNo);
            ViewData["RentalNo"] = new SelectList(_context.Rentals, "RentalNo", "RentalNo", returnCar.RentalNo);
            return View(returnCar);
        }

        // GET: ReturnCar/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ReturnCars == null)
            {
                return NotFound();
            }

            var returnCar = await _context.ReturnCars.FindAsync(id);
            if (returnCar == null)
            {
                return NotFound();
            }
            ViewData["CarNo"] = new SelectList(_context.Cars, "CarNo", "CarNo", returnCar.CarNo);
            ViewData["DriverId"] = new SelectList(_context.Drivers, "DriverId", "DriverId", returnCar.DriverId);
            ViewData["InspectorNo"] = new SelectList(_context.Inspectors, "InspectorNo", "InspectorNo", returnCar.InspectorNo);
            ViewData["RentalNo"] = new SelectList(_context.Rentals, "RentalNo", "RentalNo", returnCar.RentalNo);
            return View(returnCar);
        }

        // POST: ReturnCar/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReturnNo,DriverId,InspectorNo,CarNo,RentalNo,ReturnDate,ElapsedDate,Fine")] ReturnCar returnCar)
        {
            if (id != returnCar.ReturnNo)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(returnCar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReturnCarExists(returnCar.ReturnNo))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CarNo"] = new SelectList(_context.Cars, "CarNo", "CarNo", returnCar.CarNo);
            ViewData["DriverId"] = new SelectList(_context.Drivers, "DriverId", "DriverId", returnCar.DriverId);
            ViewData["InspectorNo"] = new SelectList(_context.Inspectors, "InspectorNo", "InspectorNo", returnCar.InspectorNo);
            ViewData["RentalNo"] = new SelectList(_context.Rentals, "RentalNo", "RentalNo", returnCar.RentalNo);
            return View(returnCar);
        }

        // GET: ReturnCar/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ReturnCars == null)
            {
                return NotFound();
            }

            var returnCar = await _context.ReturnCars
                .Include(r => r.CarNumber)
                .Include(r => r.Driver)
                .Include(r => r.InspectorNumber)
                .Include(r => r.RentalNumber)
                .FirstOrDefaultAsync(m => m.ReturnNo == id);
            if (returnCar == null)
            {
                return NotFound();
            }

            return View(returnCar);
        }

        // POST: ReturnCar/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ReturnCars == null)
            {
                return Problem("Entity set 'TheRideUrRentContext.ReturnCars'  is null.");
            }
            var returnCar = await _context.ReturnCars.FindAsync(id);
            if (returnCar != null)
            {
                _context.ReturnCars.Remove(returnCar);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReturnCarExists(int id)
        {
          return (_context.ReturnCars?.Any(e => e.ReturnNo == id)).GetValueOrDefault();
        }
    }
}
