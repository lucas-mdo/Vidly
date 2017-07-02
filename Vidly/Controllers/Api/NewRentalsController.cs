using System;
using System.Linq;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class NewRentalsController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public NewRentalsController()
        {
            _context = new ApplicationDbContext();
        }

        //POST /api/rentals
        [HttpPost]
        public IHttpActionResult CreateNewRentals(NewRentalDto newRental)
        {
            //if (newRental.MovieIds.Count == 0)
            //    return BadRequest("No Movie Ids have been given.");

            //Get customer from db
            var customer = _context.Customers.Single(c => c.Id == newRental.CustomerId);

            //if (customer == null)
            //    return BadRequest("CustomerId is not valid.");

            //Get movies from db
            var movies = _context.Movies.Where(m => newRental.MovieIds.Contains(m.Id)).ToList();

            //if (movies.Count != newRental.MovieIds.Count)
            //    return BadRequest("One or more Movie Ids are invalid.");

            //Create a rental for each movie
            foreach (var movie in movies)
            {
                if (movie.QuantityAvailable == 0)
                    return BadRequest("Movie is not available.");

                //Decrease the quantity available
                movie.QuantityAvailable--;

                var rental = new Rental
                {
                    Customer = customer,
                    Movie = movie,
                    DateRented = DateTime.Now
                };

                //Add it to the db context
                _context.Rentals.Add(rental);
            }

            _context.SaveChanges();

            return Ok();

        }
    }
}
