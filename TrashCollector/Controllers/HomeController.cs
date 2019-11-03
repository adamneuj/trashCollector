using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrashCollector.Models;

namespace TrashCollector.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext db;
        public HomeController()
        {
            db = new ApplicationDbContext();
        }
        public ActionResult Index()
        {
            if (User.IsInRole("Employee"))
            {
                DateTime today = DateTime.Now;
                string dayOfWeek = today.DayOfWeek.ToString();
                string id = User.Identity.GetUserId();
                Employee employee = db.Employees.FirstOrDefault(e => e.ApplicationId == id);
                List<Customer> customersNotPickingUpToday = db.Customers.Where(c => c.ZipCode == employee.ZipCode && c.PickupDay != dayOfWeek).ToList();
                foreach(Customer customer in customersNotPickingUpToday)
                {
                    customer.PickupConfirmed = false;
                    db.SaveChanges();
                }
                return RedirectToAction("Index", "Employee");
            }

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}