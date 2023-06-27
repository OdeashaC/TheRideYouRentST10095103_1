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
    public class RentalController : Controller
    {
        private readonly TheRideUrRentContext _context;

        public RentalController(TheRideUrRentContext context)
        {
            _context = context;
        }

        // GET: Rental
        public async Task<IActionResult> Index()
        {
            var theRideUrRentContext = _context.Rentals.Include(r => r.CarNumber).Include(r => r.Driver).Include(r => r.InspectorNumber);
            return View(await theRideUrRentContext.ToListAsync());
        }

        // GET: Rental/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Rentals == null)
            {
                return NotFound();
            }

            var rental = await _context.Rentals
                .Include(r => r.CarNumber)
                .Include(r => r.Driver)
                .Include(r => r.InspectorNumber)
                .FirstOrDefaultAsync(m => m.RentalNo == id);
            if (rental == null)
            {
                return NotFound();
            }

            return View(rental);
        }

        // GET: Rental/Create
        public IActionResult Create()
        {
            ViewData["CarNo"] = new SelectList(_context.Cars, "CarNo", "CarNo");
            ViewData["Driverid"] = new SelectList(_context.Drivers, "DriverId", "DriverId");
            ViewData["InspectorNo"] = new SelectList(_context.Inspectors, "InspectorNo", "InspectorNo");
            return View();
        }

        // POST: Rental/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RentalNo,Driverid,InspectorNo,CarNo,RentalFee,StartDate,EndDate")] Rental rental)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(rental);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CarNo"] = new SelectList(_context.Cars, "CarNo", "CarNo", rental.CarNo);
            ViewData["Driverid"] = new SelectList(_context.Drivers, "DriverId", "DriverId", rental.Driverid);
            ViewData["InspectorNo"] = new SelectList(_context.Inspectors, "InspectorNo", "InspectorNo", rental.InspectorNo);
            return View(rental);
        }

        // GET: Rental/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Rentals == null)
            {
                return NotFound();
            }

            var rental = await _context.Rentals.FindAsync(id);
            if (rental == null)
            {
                return NotFound();
            }
            ViewData["CarNo"] = new SelectList(_context.Cars, "CarNo", "CarNo", rental.CarNo);
            ViewData["Driverid"] = new SelectList(_context.Drivers, "DriverId", "DriverId", rental.Driverid);
            ViewData["InspectorNo"] = new SelectList(_context.Inspectors, "InspectorNo", "InspectorNo", rental.InspectorNo);
            return View(rental);
        }

        // POST: Rental/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RentalNo,Driverid,InspectorNo,CarNo,RentalFee,StartDate,EndDate")] Rental rental)
        {
            if (id != rental.RentalNo)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(rental);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RentalExists(rental.RentalNo))
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
            ViewData["CarNo"] = new SelectList(_context.Cars, "CarNo", "CarNo", rental.CarNo);
            ViewData["Driverid"] = new SelectList(_context.Drivers, "DriverId", "DriverId", rental.Driverid);
            ViewData["InspectorNo"] = new SelectList(_context.Inspectors, "InspectorNo", "InspectorNo", rental.InspectorNo);
            return View(rental);
        }

        // GET: Rental/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Rentals == null)
            {
                return NotFound();
            }

            var rental = await _context.Rentals
                .Include(r => r.CarNumber)
                .Include(r => r.Driver)
                .Include(r => r.InspectorNumber)
                .FirstOrDefaultAsync(m => m.RentalNo == id);
            if (rental == null)
            {
                return NotFound();
            }

            return View(rental);
        }

        // POST: Rental/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Rentals == null)
            {
                return Problem("Entity set 'TheRideUrRentContext.Rentals'  is null.");
            }
            var rental = await _context.Rentals.FindAsync(id);
            if (rental != null)
            {
                _context.Rentals.Remove(rental);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RentalExists(int id)
        {
          return (_context.Rentals?.Any(e => e.RentalNo == id)).GetValueOrDefault();
        }
    }
}
