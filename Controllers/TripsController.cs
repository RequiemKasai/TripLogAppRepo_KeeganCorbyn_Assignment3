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
            
            return View();
        }

        // POST: Trips/Create
        [HttpPost]

        public async Task<IActionResult> Page3([Bind("TripId,Destination,StartDate,EndDate,Accommodation,AccommodationPhone,AccommodationEmail,ThingToDo1,ThingToDo2,ThingToDo3")] Trip trip)
        {

            //no need to check the model state because the page 3 model has no real input validation 
            //because the input are nullable and plain text
            _context.Add(trip);
             await _context.SaveChangesAsync();
             TempData["AddedTrip"] = "Trip to" +TempData["Destination"] ;
                
                return RedirectToAction(nameof(Index));
            
            
            
        }

        public IActionResult FormPage1(string Destination,string? Accommodation, DateOnly StartDate, DateOnly EndDate)
        {
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
            }else 
            {
                //if the model fails sends the user back to page 1 
                return View();
            }

        }


        public IActionResult Page2()
        {
            return View();
        }

        //takes inputs from the page two form as parameters 
        public IActionResult FormPage2(string? AccommodationPhone, string? AccommodationEmail)
        {

            if (ModelState.IsValid)
            {
                //Assigns the form inputs to tempdata
                TempData["AccommodationPhone"] = AccommodationPhone;
                TempData["AccommodationEmail"] = AccommodationEmail;

                

                return RedirectToAction(nameof(Page3));
            }
            else
            { //if the model is invalid return to the privious view
                return View();
            }
        }

        // the opening for page 3 
        public IActionResult Page3() {

            // cpmverts both the TempData that need to be Dateonly's to variables
            string? StartDate = TempData["StartDate"] as string;
            string? EndDate = TempData["EndDate"] as string;
            //try catches to assign each tempdata to a viewbag, if the try fails the viewbag is assigned as null
            try
            {
                ViewBag.Destination = TempData["Destination"];
            }
            catch
            {
                ViewBag.Destination = null;
            }

            try
            {
                ViewBag.StartDate = DateOnly.Parse(StartDate);
            }
            catch
            {
                ViewBag.StartDate = null;  
            }


            try
            {
                ViewBag.EndDate = DateOnly.Parse(EndDate);
            }
            catch 
            { 
                ViewBag.EndDate = null; 
            }
            try
            {
                ViewBag.Accommodation = TempData["Accommodation"];
            }
            catch
            { 
                ViewBag.Accommodation = null;
            }
            try
            {
                ViewBag.AccommodationPhone = TempData["AccommodationPhone"];
            }
            catch
            {
                ViewBag.AccommodationPhone = null;
            }

            try
            {
                ViewBag.AccommodationEmail = TempData["AccommodationEmail"];
            }
            catch
            {
                ViewBag.AccommodationEmail = null;
            }
            
            

            return View();
        }

        

       



       

       



        private bool TripExists(int id)
        {
            return _context.Trips.Any(e => e.TripId == id);
        }
    }
}
