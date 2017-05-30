using System.Collections.Generic;
using System.Web.Mvc;
using Vidly.Models;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        // GET: Movies
        public ActionResult Index()
        {
            var movies = GetMovies();

            //Do NOT use ViewData
            //Do NOT use ViewBag
            return View(movies);
        }

        private IEnumerable<Movie> GetMovies()
        {
            return new List<Movie>()
            {
                new Movie() { Id = 1, Name = "Shrek!"},
                new Movie() { Id = 2, Name = "Harry Potter"}
            };
        }

    }
}