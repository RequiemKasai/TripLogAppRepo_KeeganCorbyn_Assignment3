using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TripsLogApp.Data;
using TripsLogApp.Models;

namespace TripLogApp_KeeganCorbyn_Assignment3.Controllers
{
    public class TripsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TripsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Trips
        public async Task<IActionResult> Index()
        {
            return View(await _context.Trips.ToListAsync());
        }

        // GET: Trips/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trip = await _context.Trips
                .FirstOrDefaultAsync(m => m.TripId == id);
            if (trip == null)
            {
                return NotFound();
            }

            return View(trip);
        }

        // GET: Trips/Create
        public IActionResult AddPage1()
        {
            TempData["FormData"] = new Trip();
            return View();
        }

        // POST: Trips/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
      //  [ValidateAntiForgeryToken]
        //public async Task<IActionResult> AddPage1([Bind("TripId,Destination,StartDate,EndDate,Accommodation,AccommodationPhone,AccommodationEmail,ThingToDo1,ThingToDo2,ThingToDo3")] Trip trip)
       // {
       //     if (ModelState.IsValid)
      //      {
                // _context.Add(trip);
                // await _context.SaveChangesAsync();
                
        //        return RedirectToAction(nameof(Page2));
       //     }
       //     return View(trip);
     //   }

        public IActionResult AddPage1(string Destination,string? Accommodation, DateOnly StartDate, DateOnly EndDate)
        {
            if (ModelState.IsValid)
            {
                var model = TempData["FormData"] as Trip ?? new Trip();
                model.Destination = Destination;
                model.Accommodation = Accommodation;
                model.StartDate = StartDate;
                model.EndDate = EndDate;

                TempData["FormData"] = model;

                return RedirectToAction(nameof(Page2));
            }else 
            {
                return View();
            }

        }


        public IActionResult Page2()
        {
            return View();
        }


        public IActionResult Page2(string? AccommodationPhone, string? AccommodationEmail)
        {

            if (ModelState.IsValid)
            {
                var model = TempData["FormData"] as Trip ?? new Trip();
                model.AccommodationPhone = AccommodationPhone;
                model.AccommodationEmail = AccommodationEmail;

                TempData["FormData"] = model;

                return RedirectToAction(nameof(Page3));
            }
            else
            {
                return View();
            }
        }

        public IActionResult Page3() { 
            return View();
        }

        public IActionResult Page3(string? ThingToDo1, string? ThingToDo2, string? ThingToDo3)
        {

            if (ModelState.IsValid)
            {
                var model = TempData["FormData"] as Trip ?? new Trip();
                model.ThingToDo1 = ThingToDo1;
                model.ThingToDo2 = ThingToDo2;
                model.ThingToDo3 = ThingToDo3;

                TempData["FormData"] = model;

                return RedirectToAction(nameof(Page3));
            }
            else
            {
                return View();
            }
        }

       

        // GET: Trips/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trip = await _context.Trips.FindAsync(id);
            if (trip == null)
            {
                return NotFound();
            }
            return View(trip);
        }

        // POST: Trips/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TripId,Destination,StartDate,EndDate,Accommodation,AccommodationPhone,AccommodationEmail,ThingToDo1,ThingToDo2,ThingToDo3")] Trip trip)
        {
            if (id != trip.TripId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trip);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TripExists(trip.TripId))
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
            return View(trip);
        }

        // GET: Trips/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trip = await _context.Trips
                .FirstOrDefaultAsync(m => m.TripId == id);
            if (trip == null)
            {
                return NotFound();
            }

            return View(trip);
        }

        // POST: Trips/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trip = await _context.Trips.FindAsync(id);
            if (trip != null)
            {
                _context.Trips.Remove(trip);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TripExists(int id)
        {
            return _context.Trips.Any(e => e.TripId == id);
        }
    }
}
