﻿using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using Vidly.Models;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Customers
        public ViewResult Index()
        {
            //Eager Loading
            var customers = _context.Customers.Include(c => c.MembershipType).ToList();

            return View(customers);
        }

        // GET: Customers/Details/{id}
        [Route("customers/details/{id}")]
        public ActionResult Details(int id)
        {
            //Eager Loading
            var customer = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return new HttpNotFoundResult();

            return View(customer);
        }

    }
}