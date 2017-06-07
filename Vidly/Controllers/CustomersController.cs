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
                Customer = new Customer(),
                MembershipTypes = membershipTypes
            };

            //Override to NOT go to New
            return View("CustomerForm", viewModel);
        }

        //Ensure it is only acessible via POST
        [HttpPost]
        //Security measure - Prevent CSRF attacks
        [ValidateAntiForgeryToken]
        //Use Model Binding (as every form-data is prefixed with Customer, Customer can be used instead of CustomerFormViewModel)
        public ActionResult Save(Customer customer)
        {
            //Validating fields
            if (!ModelState.IsValid)
            {
                //Keep filled data in the page
                var viewModel = new CustomerFormViewModel
                {
                    Customer = customer,
                    MembershipTypes = _context.MembershipTypes.ToList()
                };

                //Reload the form page with validation messages
                return View("CustomerForm", viewModel);
            }

            if (customer.Id == 0)
            {
                //New customer, add customer to db
                _context.Customers.Add(customer);
            }
            else
            {
                //Update customer to db
                var customerInDb = _context.Customers.Single(c => c.Id == customer.Id);

                //Update manually (security reasons)
                customerInDb.Name = customer.Name;
                customerInDb.Birthdate = customer.Birthdate;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
                customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
            }
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