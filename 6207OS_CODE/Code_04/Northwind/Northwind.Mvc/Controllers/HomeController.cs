using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Northwind.Core;
using Northwind.Mvc.Models;


namespace Northwind.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICustomerRepository repository;

        public HomeController(ICustomerRepository repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }

            this.repository = repository;
        }

        [Log(LogLevel = "Info")]
        public ActionResult Index()
        {
            var customers = repository.GetAll();
            return View(customers);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                repository.Add(customer);
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
