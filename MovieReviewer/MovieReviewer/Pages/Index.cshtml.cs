using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieReviewer.Logic;
using MovieReviewer.Models;

namespace MovieReviewer.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }
        public List<Movie> ListAllMovie { get; set; }
        public void OnGet(string query)
        {
            if (query==null)
            {
                ListAllMovie = MovieLogic.GetAllMovies();
            }
            else
            {
                ListAllMovie = MovieLogic.SearchMovies(query);
            }
            
        }
        public IActionResult OnGetSearch(string query)
        {
            List<Movie> movies = MovieLogic.SearchMovies(query);
            return Partial("_MovieList", movies);
        }
    }
}