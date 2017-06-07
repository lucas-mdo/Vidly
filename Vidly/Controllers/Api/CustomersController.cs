using System;
using System.Linq;
using System.Web.Http;
using AutoMapper;
using Vidly.Dtos;
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
        public IHttpActionResult GetCustomers()
        {
            return Ok(_context.Customers.ToList().Select(Mapper.Map<Customer, CustomerDto>));
        }

        //GET /api/customers/{id}
        public IHttpActionResult GetCustomer(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return NotFound();

            return Ok(Mapper.Map<Customer, CustomerDto>(customer));
        }

        //POST /api/customers
        [HttpPost]
        public IHttpActionResult CreateCustomer(CustomerDto customerDto)
        {
            //Validate
            if (!ModelState.IsValid)
                return BadRequest();

            //Map to domain object
            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);
            //Mark as added
            _context.Customers.Add(customer);
            //Save
            _context.SaveChanges();

            //Set Id of Dto as the newly created domain Customer
            customerDto.Id = customer.Id;

            return Created(new Uri(Request.RequestUri + "/" + customer.Id), customerDto);
        }

        //PUT /api/customers/{id}
        [HttpPut]
        public IHttpActionResult UpdateCustomer(int id, CustomerDto customerDto)
        {
            //Validate
            if (!ModelState.IsValid)
                return BadRequest();

            //Query for existing customerDto in db
            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);
            
            //Check for invalid id
            if (customerInDb == null)
                return NotFound();

            //Map to existing object in context (enable tracking changes)
            Mapper.Map(customerDto, customerInDb);
            
            _context.SaveChanges();

            return Ok();
        }

        //DELETE /api/customers/{id}
        [HttpDelete]
        public IHttpActionResult DeleteCustomer(int id)
        {
            //Query for existing customerDto in db
            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            //Check for invalid id
            if (customerInDb == null)
                return NotFound();

            //Mark as removed
            _context.Customers.Remove(customerInDb);

            _context.SaveChanges();

            return Ok();
        }
    }
}
