using Microsoft.AspNetCore.Mvc;
using TripsLogApp.Models;

namespace TripsLogApp.Controllers
{
    public class TripController : Controller
    {
        //Display list of trips
        public IActionResult Index()
        {
            //Fetch trips from database and pass to view
            return View();
        }

        //Step 1: Add Trip - Destination and Dates
        public IActionResult Add()
        {
            return View();
        }

        // Step 2: Add Trip - Accommodations
        public IActionResult AddPage2()
        {
            return View();
        }

        // Step 3: Add Trip - Things to Do
        public IActionResult AddPage3()
        {
            return View();
        }

        // Save and complete the trip addition process
        [HttpPost]
        public IActionResult Save(Trip trip)
        {
            // Save trip to database
            return RedirectToAction("Index");
        }

        // Cancel adding a trip
        public IActionResult Cancel()
        {
            TempData.Clear();
            return RedirectToAction("Index");
        }
    }
}
