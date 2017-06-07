using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        public IEnumerable<CustomerDto> GetCustomers()
        {
            return _context.Customers.ToList().Select(Mapper.Map<Customer, CustomerDto>);
        }

        //GET /api/customers/{id}
        public CustomerDto GetCustomer(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return Mapper.Map<Customer, CustomerDto>(customer);
        }

        //POST /api/customers
        [HttpPost]
        public CustomerDto CreateCustomer(CustomerDto customerDto)
        {
            //Validate
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            //Map to domain object
            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);
            //Mark as added
            _context.Customers.Add(customer);
            //Save
            _context.SaveChanges();

            //Set Id of Dto as the newly created domain Customer
            customerDto.Id = customer.Id;

            return customerDto;
        }

        //PUT /api/customers/{id}
        [HttpPut]
        public void UpdateCustomer(int id, CustomerDto customerDto)
        {
            //Validate
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            //Query for existing customerDto in db
            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);
            
            //Check for invalid id
            if (customerInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            //Map to existing object in context (enable tracking changes)
            Mapper.Map(customerDto, customerInDb);
            
            _context.SaveChanges();
        }

        //DELETE /api/customers/{id}
        [HttpDelete]
        public void DeleteCustomer(int id)
        {
            //Query for existing customerDto in db
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
