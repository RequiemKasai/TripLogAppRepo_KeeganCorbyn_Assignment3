using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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

        // GET: Trip Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trip = await _context.Trips.FirstOrDefaultAsync(m => m.TripId == id);
            if (trip == null)
            {
                return NotFound();
            }

            return View(trip);
        }


        // GET: Add Trip - Page 1
        public IActionResult AddPage1()
        {
            return View();
        }

        // POST: Handle Page 1 Submission
        [HttpPost]
        public IActionResult FormPage1(string Destination, string? Accommodation, DateOnly StartDate, DateOnly EndDate)
        {
            // Validate end date is after start date
            if (EndDate <= StartDate)
            {
                ModelState.AddModelError("EndDate", "End Date must be after Start Date.");
            }


            if (ModelState.IsValid)
            {
                //assigns all of the form data to Tempdata
                TempData["Destination"] = Destination;
                TempData["Accommodation"] = Accommodation;
                //Temp data stores everything as a string
                TempData["StartDate"] = StartDate.ToString();
                TempData["EndDate"] = EndDate.ToString();

                //redirects to the next page
                return RedirectToAction(nameof(Page2));
            }
            else
            {
                //Reload Page2 view with validation errors
                return View("AddPage1"); 
            }
        }


        // GET: Add Accommodation Info - Page 2
        public IActionResult Page2()
        {
            return View();
        }

        // POST: Handle Page 2 Submission
        [HttpPost]
        public IActionResult FormPage2(string? AccommodationPhone, string? AccommodationEmail)
        {
            // Validate phone number format
            if (!string.IsNullOrEmpty(AccommodationPhone) &&
                !System.Text.RegularExpressions.Regex.IsMatch(AccommodationPhone, @"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$"))
            {
                ModelState.AddModelError("AccommodationPhone", "Invalid phone number format (e.g., 777-555-6666).");
            }

            if (ModelState.IsValid)
            {
                // Save data in TempData
                TempData["AccommodationPhone"] = AccommodationPhone;
                TempData["AccommodationEmail"] = AccommodationEmail;

                // Retain TempData values
                TempData.Keep("Destination");
                TempData.Keep("StartDate");
                TempData.Keep("EndDate");
                TempData.Keep("Accommodation");

                // Redirect to Page3
                return RedirectToAction(nameof(Page3));
            }

            //Reload Page2 view with validation errors
            return View("Page2");
        }

        // GET: Add Things to Do and TempData - Page 3
        public IActionResult Page3()
        {
            // Retrieve and retain TempData 
            ViewBag.Destination = TempData["Destination"];
            ViewBag.StartDate = TempData["StartDate"];
            ViewBag.EndDate = TempData["EndDate"];
            ViewBag.Accommodation = TempData["Accommodation"];
            ViewBag.AccommodationPhone = TempData["AccommodationPhone"];
            ViewBag.AccommodationEmail = TempData["AccommodationEmail"];

            TempData.Keep(); // Retains all TempData keys for the next request

            return View();
        }


        // POST: Handle Page 3 Submission
        [HttpPost]
        public async Task<IActionResult> FormPage3(string? ThingToDo1, string? ThingToDo2, string? ThingToDo3)
        {
            // Validate that at least one "Thing To Do" is provided
            if (string.IsNullOrEmpty(ThingToDo1) && string.IsNullOrEmpty(ThingToDo2) && string.IsNullOrEmpty(ThingToDo3))
            {
                ModelState.AddModelError("", "At least one 'Thing To Do' must be provided.");
            }

            // Validate that necessary TempData keys are present
            if (!TempData.ContainsKey("Destination") || !TempData.ContainsKey("StartDate") || !TempData.ContainsKey("EndDate"))
            {
                ModelState.AddModelError("", "An error occurred while processing the data. Please restart the process.");
            }
          
            if (ModelState.IsValid)
            {
                var trip = new Trip
                {
                    //Create a new Trip object from TempData and form data
                    Destination = TempData["Destination"]?.ToString(),
                    Accommodation = TempData["Accommodation"]?.ToString(),
                    StartDate = DateOnly.Parse(TempData["StartDate"]?.ToString()),
                    EndDate = DateOnly.Parse(TempData["EndDate"]?.ToString()),
                    AccommodationPhone = TempData["AccommodationPhone"]?.ToString(),
                    AccommodationEmail = TempData["AccommodationEmail"]?.ToString(),
                    ThingToDo1 = ThingToDo1,
                    ThingToDo2 = ThingToDo2,
                    ThingToDo3 = ThingToDo3
                };

                _context.Add(trip);
                await _context.SaveChangesAsync();

                TempData["AddedTrip"] = $"Trip to {trip.Destination} added successfully!";
                return RedirectToAction(nameof(Index));
            }

            //Retain TempData for view
            TempData.Keep();
            //Reload Page3 view with validation errors
            return View("Page3");
        }


        //Check if Trip Exists
        private bool TripExists(int id)
        {
            return _context.Trips.Any(e => e.TripId == id);
        }
    }
}