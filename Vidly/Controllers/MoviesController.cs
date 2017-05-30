using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Vidly.Models;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Movies
        public ActionResult Index()
        {
            //Eager Loading
            var movies = _context.Movies.Include(m => m.Genre).ToList();

            //Do NOT use ViewData
            //Do NOT use ViewBag
            return View(movies);
        }

        // GET: Movies/Details/{id}
        [Route("movies/details/{id}")]
        public ActionResult Details(int id)
        {
            //Eager Loading
            var movie = _context.Movies.Include(m => m.Genre).SingleOrDefault(m => m.Id == id);

            if (movie == null)
                return new HttpNotFoundResult();

            return View(movie);
        }

    }
}