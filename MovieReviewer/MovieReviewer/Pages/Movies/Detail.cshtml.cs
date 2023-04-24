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
        public void OnGet(int id)
        {
            Movie = MovieLogic.GetMovieDetail(id);
            if (Movie != null)
            {

            }
        }
        public void OnGetFavourite(int id)
        {
            var setting = new JsonSerializerSettings();
            setting.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            string json = HttpContext.Session.GetString("user");
            User user = null;
            if (json != null)
            {
                user = JsonConvert.DeserializeObject<User>(json, setting);
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
        //public IActionResult OnPost(int id)
        //{
        //    var setting = new JsonSerializerSettings();
        //    setting.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        //    string json = HttpContext.Session.GetString("user");
        //    User user = null;
        //    if (json != null)
        //    {
        //        user = JsonConvert.DeserializeObject<User>(json, setting);
        //    }

        //    if (MovieLogic.CheckLoveMovie(user.Id, id))
        //    {
        //        MovieLogic.DeleteFavourite(user.Id, id);
        //    }
        //    else
        //    {
        //        MovieLogic.InsertFavourite(user.Id, id);
        //    }
        //    return Redirect("Movies/Detail?id=" + id);
        //}
    }
}
