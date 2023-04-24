using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieReviewer.Logic;
using MovieReviewer.Models;
using Newtonsoft.Json;

namespace MovieReviewer.Pages.Movies
{
    public class DetailModel : PageModel
    {
        [BindProperty]
        public Movie Movie { get; set; }
        [BindProperty]
        public Rate Rate { get; set; }
        public void OnGet(int id)
        {
            string json = HttpContext.Session.GetString("user");
            User user = null;
            if (json != null)
            {
                user = JsonConvert.DeserializeObject<User>(json);
            }
            Movie = MovieLogic.GetMovieDetail(id);
            if (Movie != null && user!=null)
            {
                Rate = MovieLogic.GetRate(user.Id, id);
            }
            
        }
        public void OnGetFavourite(int id)
        {
           
            string json = HttpContext.Session.GetString("user");
            User user = null;
            if (json != null)
            {
                user = JsonConvert.DeserializeObject<User>(json);
            }

            if (MovieLogic.CheckLoveMovie(user.Id, id))
            {
                MovieLogic.DeleteFavourite(user.Id, id);
            }
            else
            {
                MovieLogic.InsertFavourite(user.Id, id);
            }

        }
        public IActionResult OnPostRate(int rating , string contentRate,int movieId)
        {
            string json = HttpContext.Session.GetString("user");
            User user = null;
            if (json != null)
            {
                user = JsonConvert.DeserializeObject<User>(json);
            }
            else
            {
                return Redirect("/Users/Login");
            }
            if (MovieLogic.CheckRate(user.Id,movieId))
            {
                MovieLogic.UpdateRate(user.Id, rating, contentRate, movieId);
            }
            else
            {
                MovieLogic.Rate(user.Id, rating, contentRate, movieId);
            }
            
            return Redirect("/Movies/Detail?id=" + movieId);
        }
    }
}
