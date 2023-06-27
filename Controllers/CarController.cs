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
    public class CarController : Controller
    {
        private readonly TheRideUrRentContext _context;

        public CarController(TheRideUrRentContext context)
        {
            _context = context;
        }

        // GET: Car
        public async Task<IActionResult> Index()
        {
            var theRideUrRentContext = _context.Cars.Include(c => c.CarBodyType).Include(c => c.CarMake);
            return View(await theRideUrRentContext.ToListAsync());
        }

        // GET: Car/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Cars == null)
            {
                return NotFound();
            }

            var car = await _context.Cars
                .Include(c => c.CarBodyType)
                .Include(c => c.CarMake)
                .FirstOrDefaultAsync(m => m.CarNo == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // GET: Car/Create
        public IActionResult Create()
        {
            ViewData["CarBodyTypeId"] = new SelectList(_context.CarBodyTypes, "CarBodyTypeId", "CarBodyTypeId");
            ViewData["CarMakeId"] = new SelectList(_context.CarMakes, "CarMakeId", "CarMakeId");
            return View();
        }

        // POST: Car/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CarNo,CarMakeId,CarBodyTypeId,CarModel,KilometresTravelled,ServiceKilometres,Available")] Car car)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(car);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CarBodyTypeId"] = new SelectList(_context.CarBodyTypes, "CarBodyTypeId", "CarBodyTypeId", car.CarBodyTypeId);
            ViewData["CarMakeId"] = new SelectList(_context.CarMakes, "CarMakeId", "CarMakeId", car.CarMakeId);
            return View(car);
        }

        // GET: Car/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Cars == null)
            {
                return NotFound();
            }

            var car = await _context.Cars.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }
            ViewData["CarBodyTypeId"] = new SelectList(_context.CarBodyTypes, "CarBodyTypeId", "CarBodyTypeId", car.CarBodyTypeId);
            ViewData["CarMakeId"] = new SelectList(_context.CarMakes, "CarMakeId", "CarMakeId", car.CarMakeId);
            return View(car);
        }

        // POST: Car/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("CarNo,CarMakeId,CarBodyTypeId,CarModel,KilometresTravelled,ServiceKilometres,Available")] Car car)
        {
            if (id != car.CarNo)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(car);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarExists(car.CarNo))
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
            ViewData["CarBodyTypeId"] = new SelectList(_context.CarBodyTypes, "CarBodyTypeId", "CarBodyTypeId", car.CarBodyTypeId);
            ViewData["CarMakeId"] = new SelectList(_context.CarMakes, "CarMakeId", "CarMakeId", car.CarMakeId);
            return View(car);
        }

        // GET: Car/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Cars == null)
            {
                return NotFound();
            }

            var car = await _context.Cars
                .Include(c => c.CarBodyType)
                .Include(c => c.CarMake)
                .FirstOrDefaultAsync(m => m.CarNo == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // POST: Car/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Cars == null)
            {
                return Problem("Entity set 'TheRideUrRentContext.Cars'  is null.");
            }
            var car = await _context.Cars.FindAsync(id);
            if (car != null)
            {
                _context.Cars.Remove(car);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarExists(string id)
        {
          return (_context.Cars?.Any(e => e.CarNo == id)).GetValueOrDefault();
        }
    }
}
