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
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]

        public async Task<IActionResult> Page3([Bind("TripId,Destination,StartDate,EndDate,Accommodation,AccommodationPhone,AccommodationEmail,ThingToDo1,ThingToDo2,ThingToDo3")] Trip trip)
        {

            Console.WriteLine("test");
            _context.Add(trip);
             await _context.SaveChangesAsync();
             TempData["AddedTrip"] = "Trip to" +TempData["Destination"] ;
                
                return RedirectToAction(nameof(Index));
            
            
            
        }

        public IActionResult FormPage1(string Destination,string? Accommodation, DateOnly StartDate, DateOnly EndDate)
        {
            if (ModelState.IsValid)
            {
                Debug.Write("works?");
                TempData["Destination"] = Destination;
                TempData["Accommodation"] = Accommodation;
                TempData["StartDate"] = StartDate.ToString();
                TempData["EndDate"] = EndDate.ToString();

                

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


        public IActionResult FormPage2(string? AccommodationPhone, string? AccommodationEmail)
        {

            if (ModelState.IsValid)
            {

                TempData["AccommodationPhone"] = AccommodationPhone;
                TempData["AccommodationEmail"] = AccommodationEmail;

                

                return RedirectToAction(nameof(Page3));
            }
            else
            {
                return View();
            }
        }

        public IActionResult Page3() {


            string? StartDate = TempData["StartDate"] as string;
            string? EndDate = TempData["EndDate"] as string;

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


       

       



        private bool TripExists(int id)
        {
            return _context.Trips.Any(e => e.TripId == id);
        }
    }
}
