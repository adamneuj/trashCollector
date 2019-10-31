using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrashCollector.Models;

namespace TrashCollector.Controllers
{
    public class CustomerController : Controller
    {
        ApplicationDbContext db;
        string[] daysOfWeek;
        public CustomerController()
        {
            daysOfWeek = new string[7] { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };
            db = new ApplicationDbContext();
        }
        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }

        // GET: Customer/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Customer/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.Name = new SelectList(daysOfWeek);
            Customer customer = new Customer();
            return View(customer);
        }

        // POST: Customer/Create
        [HttpPost]
        public ActionResult Create(Customer customer)
        {
            try
            {
                var pickupDay = new SelectList(new[]
                {
                    new {value = 1, text = "Sunday"},
                    new {value = 2, text = "Monday"},
                    new {value = 3, text = "Tuesday"},
                    new {value = 4, text = "Wednesday"},
                    new {value = 5, text = "Thursday"},
                    new {value = 6, text = "Friday"},
                    new {value = 7, text = "Saturday"}
                });
                ViewBag.Name = pickupDay;
                customer.ApplicationId = User.Identity.GetUserId();
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Pickup()
        {
            string id = User.Identity.GetUserId();
            Customer customer = db.Customers.FirstOrDefault(c => c.ApplicationId == id);
            ViewBag.Name = new SelectList(daysOfWeek);
            return View(customer);
        }

        [HttpPost]
        public ActionResult Pickup(int id, Customer customer)
        {
            try
            {
                var pickupDay = new SelectList(new[]
                {
                    new {value = 1, text = "Sunday"},
                    new {value = 2, text = "Monday"},
                    new {value = 3, text = "Tuesday"},
                    new {value = 4, text = "Wednesday"},
                    new {value = 5, text = "Thursday"},
                    new {value = 6, text = "Friday"},
                    new {value = 7, text = "Saturday"}
                });
                ViewBag.Name = pickupDay;
                Customer customerFromDb = db.Customers.FirstOrDefault(c => c.Id == id);
                customerFromDb.PickupDay = customer.PickupDay;
                customerFromDb.AdditionalPickupDay = customer.AdditionalPickupDay;
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View(id);
            }
        }

        // GET: Customer/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.Name = new SelectList(daysOfWeek);
            Customer customer = db.Customers.FirstOrDefault(c => c.Id == id);
            return View(customer);
        }

        // POST: Customer/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Customer customer)
        {
            try
            {
                Customer customerFromDb = db.Customers.FirstOrDefault(c => c.Id == id);
                customerFromDb.FirstName = customer.FirstName;
                customerFromDb.LastName = customer.LastName;
                customerFromDb.StreetAddress = customer.StreetAddress;
                customerFromDb.City = customer.City;
                customerFromDb.State = customer.State;
                customerFromDb.ZipCode = customer.ZipCode;
                customerFromDb.Balance = customer.Balance;
                customerFromDb.PickupDay = customer.PickupDay;
                customerFromDb.AdditionalPickupDay = customer.AdditionalPickupDay;
                customerFromDb.PickupConfirmed = customer.PickupConfirmed;
                customerFromDb.SuspendStart = customer.SuspendStart;
                customerFromDb.SuspendEnd = customer.SuspendEnd;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(id);
            }
        }

        // GET: Customer/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Customer/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
