using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieReviewer.Logic;
using MovieReviewer.Models;
using Newtonsoft.Json;

namespace MovieReviewer.Pages.Movies
{
    public class FavouriteListModel : PageModel
    {
        [BindProperty]
        public List<FavouriteList> ListFavourite { get; set; }
        public void OnGet()
        {
            string json = HttpContext.Session.GetString("user");
            User user = null;
            if (json != null)
            {
                user = JsonConvert.DeserializeObject<User>(json);
                ListFavourite = MovieLogic.GetFavouriteIdListById(user.Id);
            }
            else
            {
                Redirect("Users/Login");
            }

        }
    }
}
