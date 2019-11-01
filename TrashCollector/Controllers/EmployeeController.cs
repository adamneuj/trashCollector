using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrashCollector.Models;

namespace TrashCollector.Controllers
{
    public class EmployeeController : Controller
    {
        ApplicationDbContext db;
        double pickupCharge = 5;
        public EmployeeController()
        {
            db = new ApplicationDbContext();
        }
        // GET: Employee
        public ActionResult Index()
        {
            DateTime today = DateTime.Today;
            string dayOfWeek = today.DayOfWeek.ToString();
            string id = User.Identity.GetUserId();
            Employee employee = db.Employees.FirstOrDefault(e => e.ApplicationId == id);
            List<Customer> customersFromDb = db.Customers.Where(c => c.ZipCode == employee.ZipCode && c.PickupDay == dayOfWeek && c.PickupConfirmed != true).ToList();
            List<Customer> customersAdditionalPickup = db.Customers.Where(c => c.AdditionalPickupDay == today).ToList();
            List<Customer> customersSuspended = db.Customers.Where(c => c.SuspendStart <= today && c.SuspendEnd > today).ToList();
            customersFromDb.AddRange(customersAdditionalPickup);
            foreach(Customer customer in customersSuspended)
            {
                customersFromDb.Remove(customer);
            }
            return View(customersFromDb);
        }

        public ActionResult CompletedPickups()
        {
            DateTime day = DateTime.Today;
            string today = day.DayOfWeek.ToString();
            string id = User.Identity.GetUserId();
            Employee employee = db.Employees.FirstOrDefault(e => e.ApplicationId == id);
            List<Customer> customersFromDb = db.Customers.Where(c => c.PickupConfirmed == true && c.PickupDay == today).ToList();
            return View(customersFromDb);
        }

        public ActionResult ConfirmPickup(int id)
        {
            Customer customer = db.Customers.FirstOrDefault(c => c.Id == id);
            return View(customer);
        }

        [HttpPost]
        public ActionResult ConfirmPickup(int id, Customer customer)
        {
            try
            {
                Customer customerFromDb = db.Customers.FirstOrDefault(c => c.Id == id);
                customerFromDb.PickupConfirmed = customer.PickupConfirmed;
                customerFromDb.Balance += pickupCharge;
                customerFromDb.Balance = Math.Round(customerFromDb.Balance, 2);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(id);
            }
        }

        // GET: Employee/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Employee/Create
        public ActionResult Create()
        {
            Employee employee = new Employee();
            return View(employee);
        }

        // POST: Employee/Create
        [HttpPost]
        public ActionResult Create(Employee employee)
        {
            try
            {
                employee.ApplicationId = User.Identity.GetUserId();
                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Employee/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Employee/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Employee/Delete/5
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
