using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using Vidly.Models;
using Vidly.ViewModels;

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

        public ActionResult New()
        {
            var membershipTypes = _context.MembershipTypes.ToList();
            var viewModel = new CustomerFormViewModel()
            {
                MembershipTypes = membershipTypes
            };

            //Override to NOT go to New
            return View("CustomerForm", viewModel);
        }

        //Ensure it is only acessible via POST
        [HttpPost]
        //Use Model Binding (as every form-data is prefixed with Customer, Customer can be used instead of CustomerFormViewModel)
        public ActionResult Create(Customer customer)
        {
            //Add customer to dbcontext
            _context.Customers.Add(customer);
            //Persist all changes
            _context.SaveChanges();

            return RedirectToAction("Index", "Customers");
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

        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return HttpNotFound();

            var viewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipTypes = _context.MembershipTypes.ToList()
            };

            //Override to NOT go to Edit
            return View("CustomerForm", viewModel);
        }
    }
}