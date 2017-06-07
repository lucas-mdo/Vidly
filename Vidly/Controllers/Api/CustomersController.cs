using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class CustomersController : ApiController
    {
        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }
        //GET /api/customers
        public IEnumerable<Customer> GetCustomers()
        {
            return _context.Customers.ToList();
        }

        //GET /api/customers/{id}
        public Customer GetCustomer(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return customer;
        }

        //POST /api/customers
        [HttpPost]
        public Customer CreateCustomer(Customer customer)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            _context.Customers.Add(customer);
            _context.SaveChanges();

            return customer;
        }

        //PUT /api/customers/{id}
        [HttpPut]
        public void UpdateCustomer(int id, Customer customer)
        {
            //Validate
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            //Query for existing customer in db
            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);
            
            //Check for invalid id
            if (customerInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            customerInDb.Name = customer.Name;
            customerInDb.Birthdate = customer.Birthdate;
            customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
            customerInDb.MembershipTypeId = customer.MembershipTypeId;

            _context.SaveChanges();
        }

        //DELETE /api/customers/{id}
        [HttpDelete]
        public void DeleteCustomer(int id)
        {
            //Query for existing customer in db
            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            //Check for invalid id
            if (customerInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            //Mark as removed
            _context.Customers.Remove(customerInDb);

            _context.SaveChanges();
        }
    }
}
