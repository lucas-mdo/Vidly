using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

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

        public ActionResult New()
        {
            //Get genres list from db
            var genres = _context.Genres.ToList();

            //Used for combobox
            var viewModel = new MovieFormViewModel
            {
                Genres = genres
            };

            return View("MoviesForm", viewModel);
        }

        public ActionResult Edit(int id)
        {
            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);

            if (movie == null)
                return HttpNotFound();

            var viewModel = new MovieFormViewModel(movie)
            {
                Genres = _context.Genres.ToList()
            };

            //Override to NOT go to Edit
            return View("MoviesForm", viewModel);
        }

        //Ensure it is only acessible via POST
        [HttpPost]
        //Security measure - Prevent CSRF attacks
        [ValidateAntiForgeryToken]
        //Use Model Binding (as every form-data is prefixed with Customer, Customer can be used instead of CustomerFormViewModel)
        public ActionResult Save(Movie movie)
        {
            //Validating fields
            if (!ModelState.IsValid)
            {
                //Keep filled data in the page
                var viewModel = new MovieFormViewModel(movie)
                {
                    Genres = _context.Genres.ToList()
                };

                //Reload the form page with validation messages
                return View("MoviesForm", viewModel);
            }

            if (movie.Id == 0)
            {
                movie.DateAdded = DateTime.Now;
                //New movie, add movie to db
                _context.Movies.Add(movie);
            }
            else
            {
                //Update customer to db
                var movieInDb = _context.Movies.Single(c => c.Id == movie.Id);

                //Update manually (security reasons)
                movieInDb.Name = movie.Name;
                movieInDb.ReleaseDate = movie.ReleaseDate;
                movieInDb.GenreId = movie.GenreId;
                movieInDb.QuantityInStock = movie.QuantityInStock;

            }
            //Persist all changes
            _context.SaveChanges();

            return RedirectToAction("Index", "Movies");
        }
    }
}