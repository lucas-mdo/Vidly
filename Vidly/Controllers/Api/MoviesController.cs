using System;
using System.Linq;
using System.Web.Http;
using AutoMapper;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class MoviesController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        //GET /api/movies
        public IHttpActionResult GetMovies()
        {
            return Ok(_context.Movies.ToList().Select(Mapper.Map<Movie, MovieDto>));
        }

        //GET /api/movies/{id}
        public IHttpActionResult GetMovie(int id)
        {
            var movie = _context.Movies.SingleOrDefault(c => c.Id == id);

            if (movie == null)
                return NotFound();

            return Ok(Mapper.Map<Movie, MovieDto>(movie));
        }

        //POST /api/movies
        [HttpPost]
        public IHttpActionResult CreateMovie(MovieDto movieDto)
        {
            //Validate
            if (!ModelState.IsValid)
                return BadRequest();

            //Map to domain object
            var movie = Mapper.Map<MovieDto, Movie>(movieDto);

            movie.DateAdded = DateTime.Now;
            //Mark as added
            _context.Movies.Add(movie);
            //Save
            _context.SaveChanges();

            //Set Id of Dto as the newly created domain Movie
            movieDto.Id = movie.Id;

            return Created(new Uri(Request.RequestUri + "/" + movie.Id), movieDto);
        }

        //PUT /api/movies/{id}
        [HttpPut]
        public IHttpActionResult UpdateMovie(int id, MovieDto movieDto)
        {
            //Validate
            if (!ModelState.IsValid)
                return BadRequest();

            //Query for existing movieDto in db
            var movieInDb = _context.Movies.SingleOrDefault(c => c.Id == id);

            //Check for invalid id
            if (movieInDb == null)
                return NotFound();

            //Map to existing object in context (enable tracking changes)
            Mapper.Map(movieDto, movieInDb);

            _context.SaveChanges();

            return Ok();
        }

        //DELETE /api/movies/{id}
        [HttpDelete]
        public IHttpActionResult DeleteMovie(int id)
        {
            //Query for existing movieDto in db
            var movieInDb = _context.Movies.SingleOrDefault(c => c.Id == id);

            //Check for invalid id
            if (movieInDb == null)
                return NotFound();

            //Mark as removed
            _context.Movies.Remove(movieInDb);

            _context.SaveChanges();

            return Ok();
        }
    }
}
